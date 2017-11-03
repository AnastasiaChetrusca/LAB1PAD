using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interfete;

namespace Client
{
    class Networking :IComunicare
    {
        private readonly NetworkStream networkStream;

        public Networking(TcpClient client)
        {
            networkStream = client.GetStream(); //canal
        }

        public async Task Write(byte[] data)
        {
            await networkStream.WriteAsync(data, 0, data.Length);
        }

        public async Task<byte[]> Read()
        {
            byte[] bytes = new byte[1024];
            await networkStream.ReadAsync(bytes, 0, bytes.Length);

            string data = System.Text.Encoding.Unicode.GetString(bytes);
            data = data.Replace("\0", string.Empty);

            return System.Text.Encoding.Unicode.GetBytes(data);
        }
    }
}
