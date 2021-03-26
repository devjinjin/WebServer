using System.Net.Http;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Service.Products
{
    public interface IProductHttpRepository : IHttpRepository<ProductModel>
    {
        Task<PagingResponse<ProductModel>> GetProducts(ProductParameters productParameters);
    }
}
