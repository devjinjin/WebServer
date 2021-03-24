using System.Net.Http;
using System.Threading.Tasks;

namespace WebServer.Service.Notes
{
    public interface IProductHttpRepository
    {
        //Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters);
        //Task CreateProduct(Product product);
        Task<string> UploadProductImage(MultipartFormDataContent content);
    }
}
