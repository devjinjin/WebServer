using System.Net.Http;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Service.Notes
{
    public interface IProductHttpRepository
    {
        Task<PagingResponse<ProductModel>> GetProducts(ProductParameters productParameters);
        Task CreateProduct(ProductModel product);
        Task<string> UploadProductImage(MultipartFormDataContent content);
    }
}
