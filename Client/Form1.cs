using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Client
{
    public partial class Form1 : Form
    {
        private static readonly int BROKER_PORT = 8888; //portul Broker'ului
        private static readonly string BROKER_IP = "127.0.0.1"; //adresa IP a Broker'ului
        private static readonly int PORT = 7777; //toti clienti vor asculta la port 7777
        private readonly Guid clientId;
        private TcpClient socket;

        public Form1()
        {
            InitializeComponent();
            clientId = Guid.NewGuid();//se genereaza un ID
        }

        private void trimiteBtn_Click(object sender, EventArgs e)
        {
            if (clientListBox.SelectedIndex > -1)
                //daca e adevarat atunci cel putin un element a fost selectat din lista
            {
                if (msgBox.Text != "")
                {
                    List<string> clientiSelectati = new List<string>();
                    foreach (var clientIp in clientListBox.SelectedItems)
                    {
                        clientiSelectati.Add(clientIp.ToString());
                    }

                    XElement data = ConverteazaMesajInXml(msgBox.Text, clientiSelectati);
                    var xmlToByteArray = ConvertXmlToByteArray(data, Encoding.Unicode);

                    Networking retea = new Networking(socket);
                    retea.Write(xmlToByteArray);
                }
                msgBox.Text = "";
            }
        }

        private XElement ConverteazaMesajInXml(string mesaj, List<string> clientiSelectati)
        {
            XElement data = new XElement("Data");

            XElement sursa = new XElement("Sursa");
            XElement idSursa = new XElement("IDSursa", clientId.ToString());
            XElement ipAddrSursa = new XElement("IPAddrSursa", listIPAddr.GetItemText(listIPAddr.SelectedItem));
            XElement portSursa = new XElement("PortSursa", PORT);
            XElement mesajSursa = new XElement("Mesaj", mesaj);

            XElement destinatie = new XElement("Destinatie");

            sursa.Add(idSursa, ipAddrSursa, portSursa, mesajSursa);
            foreach (var client in clientiSelectati)
            {
                destinatie.Add(new XElement("IpDestinatie", client));
            }

            data.Add(sursa);
            data.Add(destinatie);

            return data;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trimiteBtn.Enabled = false;
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

        private XElement ConvertByteArrayToXml(byte[] data, Encoding encoding)
        {
            // Interpret the byte array according to a specific encoding
            using (var stream = new MemoryStream(data))
            using (var reader = new StreamReader(stream, encoding, false))
            {
                return XElement.Load(reader);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private async void Register()
        {
            try
            {
                var endPoint = new IPEndPoint(IPAddress.Parse(listIPAddr.GetItemText(listIPAddr.SelectedItem)), PORT); //setam adresa locala a clientului
                socket = new TcpClient(endPoint);
                socket.Connect(BROKER_IP, BROKER_PORT);//connectam la broker
                AscultaRaspuns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nu este posibil conexiunea la broker \n" + ex.Message, "Erroare detectata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Networking retea = new Networking(socket);

            //formam mesajul in XML
            XElement data = new XElement("Data");
            XElement inregistrare = new XElement("Inregistrare");
            XElement idSursa = new XElement("IDSursa", clientId.ToString());
            XElement ipAddrSursa = new XElement("IPAddrSursa", listIPAddr.GetItemText(listIPAddr.SelectedItem));
            XElement portSursa = new XElement("PortSursa", PORT);

            inregistrare.Add(idSursa, ipAddrSursa, portSursa);
            data.Add(inregistrare);

            var xmlToByteArray = ConvertXmlToByteArray(data, Encoding.Unicode);
            await retea.Write(xmlToByteArray);

            trimiteBtn.Enabled = true;
            InregistrareBtn.Enabled = false;

        //    socket.Close();
        //    AscultaRaspuns();
        }

        private void InregistrareBtn_Click(object sender, EventArgs e)
        {
            if (listIPAddr.SelectedIndex > -1) //verificam daca este selectat vriun element din lista, indexare de la 0
            {
                Register();
                listIPAddr.Enabled = false;
            }
            else
            {
                MessageBox.Show("Alege adresa local", "Erroare detectata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void AscultaRaspuns()
        {
            Networking retea = new Networking(socket);

            while (true)
            {
                byte[] data = await retea.Read();

                XElement byteArrayToXml = ConvertByteArrayToXml(data, Encoding.Unicode);
                List<string> listaClienti = new List<string>();

                if (byteArrayToXml.Elements("ClientID").Any()) //daca se face Inregistrare
                {
                    clientListBox.Items.Clear();
                    foreach (var clientID in byteArrayToXml.Elements("ClientID"))
                    {
                        clientListBox.Items.Add(clientID.Value);
                    }
                }
                else if (byteArrayToXml.Elements("Mesaj").Any())
                {
                    string mesaj = byteArrayToXml.Element("Mesaj").Value;
                    richTextBox1.Text += "Mesaj> " + mesaj + "\n";

                    SalveazaInFisier(byteArrayToXml);
                }

            }
        }

        private void SalveazaInFisier(XElement mesaj)
        {
            XElement root;
            string fisier = clientId + ".xml";

            if (File.Exists(fisier)) //daca fisieru deja exista
                root = XElement.Load(fisier);
            else
                root = new XElement("Data");

            root.Add(mesaj);
            root.Save(fisier);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (socket != null)
                socket.Close();
        }

        //este apelata o singura data dupa ce forma aplicatiei a parut pe ecran
        private void Form1_Shown(object sender, EventArgs e)
        {

        }
    }
}