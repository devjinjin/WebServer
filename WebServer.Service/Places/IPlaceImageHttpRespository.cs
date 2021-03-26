using System.Net.Http;
using System.Threading.Tasks;
using WebServer.Models.Places;

namespace WebServer.Service.Places
{
    public interface IPlaceImageHttpRespository
    {
        Task<PlaceImageResponse> UploadImage(MultipartFormDataContent item);
    }
}
