using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Broker
{
    public partial class Form1 : Form
    {
        private TcpListener serverSocket;
        private TcpClient clientConectat;
        private static int BROKER_PORT = 8888; //portul Broker'ului
        private static string BROKER_IP = "127.0.0.1"; //adresa IP a Broker'ului
        private static Dictionary<String, TcpClient> clientList = new Dictionary<string, TcpClient>();

        private static Object Semafor = new Object();

        private delegate void AppendToDictionary(string ID, TcpClient tcpClient);

        private delegate void AppendToList(string clientID);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            serverSocket = new TcpListener(IPAddress.Parse(BROKER_IP), BROKER_PORT);
            AscultaClienti();
        }

        private async void AscultaClienti()
        {
            serverSocket.Start();

            while (true)
            {
                clientConectat = await serverSocket.AcceptTcpClientAsync();

                ComunicareClient(clientConectat);

                //Thread clientThead = new Thread(() => ComunicareClient(clientConectat)); //cream un fir pt client conectat; metoda executa pe alt fir.
                //clientThead.Start();
            }
        }

        private async void ComunicareClient(TcpClient client)
        {
            Networking retea = new Networking(client);

            while (true)
            {
                byte[] data = await retea.Read(); ;//aceasta nu se executa pana cand retea.Read nu-si termina lucrul, desi firul nu se opreste aici,
                                                   // isi continuie lucru si revine cand retea.Read a terminat.

                XElement xData = ConvertByteArrayToXml(data, Encoding.Unicode);

                string connectedUser = "";
                string connectedUserIp = "";

                if (xData.Elements("Inregistrare").Any()) //daca se face Inregistrare
                {
                    connectedUserIp = xData.Element("Inregistrare").Element("IPAddrSursa").Value;

                    AppendToDictionary dictionary = new AppendToDictionary(this.AddUserToDictionary);
                    AppendToList list = new AppendToList(UpdateListUI);

                    Invoke(dictionary, new object[] { connectedUserIp, client });//findca se executa din alt fir
                    Invoke(list, new object[] { connectedUserIp });

                    RaspundeCuListaDeClientiConectati();
                }
                else if (xData.Elements("Sursa").Any())
                {
                    string mesaj = xData.Element("Sursa").Element("Mesaj").Value;
                    string idDestinatie = xData.Element("Destinatie").Element("IpDestinatie").Value;

                    foreach (var element in xData.Element("Destinatie").Elements("IpDestinatie"))
                    {
                        if (clientList.ContainsKey(element.Value)) //daca exista client cu asa ID inregistrat
                        {
                            TransmiteMesajLaDestinatie(mesaj, element.Value);
                        }
                    }

                } 

            }
        }

        private void TransmiteMesajLaDestinatie(string mesaj, string ipDestinatie)
        {
            XElement data = ConverteazaMesajInXml(mesaj);
            var xmlToByteArray = ConvertXmlToByteArray(data, Encoding.Unicode);

            Networking retea = new Networking(clientList[ipDestinatie]);
            retea.Write(xmlToByteArray);
        }

        private XElement ConverteazaMesajInXml(string mesaj)
        {
            XElement data = new XElement("Data");
            XElement mesajSursa = new XElement("Mesaj", mesaj);

            data.Add(mesajSursa);

            return data;
        }

        private XElement ConvertByteArrayToXml(byte[] data, Encoding encoding)
        {
            // Interpret the byte array according to a specific encoding
            using (var stream = new MemoryStream(data))
            using (var reader = new StreamReader(stream, encoding, false))
            {
                return XElement.Load(reader);
            }
        }

        private byte[] ConvertXmlToByteArray(XElement xml, Encoding encoding)
        {
            using (var stream = new MemoryStream())
            {
                var settings = new XmlWriterSettings();
                // Add formatting and other writer options here if desired
                settings.Encoding = encoding;
                settings.OmitXmlDeclaration = true; // No prolog
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    xml.Save(writer);
                }
                return stream.ToArray();
            }
        }

        private async void RaspundeCuListaDeClientiConectati()
        {
            XElement listaClienti = new XElement("ListaClienti");
            List<TcpClient> tcpClient = new List<TcpClient>();

            foreach (var cheie in clientList.Keys)
            {
                listaClienti.Add(new XElement("ClientID", cheie));
                tcpClient.Add(clientList[cheie]);
            }

            foreach (var client in tcpClient)
            {
                Networking retea = new Networking(client);
                await retea.Write(ConvertXmlToByteArray(listaClienti, Encoding.Unicode));
            }
        }

        private void AddUserToDictionary(string ID, TcpClient tcpClient)
        {
            lock (Semafor)
            {
                clientList.Add(ID, tcpClient);
            }
        }

        private void UpdateListUI(string client)
        {
            clientListBox.Items.Add(client);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverSocket.Stop();
            clientConectat.Close();
        }
    }
}
