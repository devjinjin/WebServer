using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Data.Products
{
    public interface IProductRepository
    {
        Task<PagedList<ProductModel>> GetProducts(ProductParameters productParameters);
        Task CreateProduct(ProductModel product);

        Task<ProductModel> GetProduct(string id);

        Task<bool> EditAsync(ProductRequestModel model); // 수정
    }
}
