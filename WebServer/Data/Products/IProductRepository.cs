using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Data.Products
{
    public interface IProductRepository
    {
        Task<PagedList<ProductModel>> GetProducts(ProductParameters productParameters);
        Task CreateProduct(ProductModel product);

        Task<ProductModel> GetProduct(string id);
    }
}
