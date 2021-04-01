using System.Net.Http;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Service.Products
{
    public interface IProductHttpRepository : IHttpRepository<ProductRequestModel>
    {
        Task<PagingResponse<ProductModel>> GetItems(ProductParameters productParameters);

        Task<PagingResponse<ProductModel>> GetNewProducts(ProductParameters productParameters);
    }
}
