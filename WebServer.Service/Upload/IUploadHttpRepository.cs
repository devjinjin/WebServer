using System.Net.Http;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Service.Upload
{
    public interface IUploadHttpRepository
    {
        Task<string> UploadImage(MultipartFormDataContent content);
    }
}
