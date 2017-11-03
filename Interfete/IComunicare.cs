using System.Threading.Tasks;

namespace Interfete
{
    public interface IComunicare
    {
        Task Write(byte[] data); //in caz dat Task poate fi considerat ca void
        Task<byte[]> Read();
    }
}
