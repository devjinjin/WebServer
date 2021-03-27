using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Category;

namespace WebServer.Service.Common.Categories
{
    public interface ICategoryHttpRepository
    {
        Task<List<CategoryModel>> GetCategories();
    }
}
