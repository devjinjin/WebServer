using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Product;

namespace WebServer.Data.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<ProductModel>> GetProducts(ProductParameters productParameters)
        {
            var products = await _context.Products
                .Search(productParameters.SearchTerm)
                .Sort(productParameters.OrderBy)
                .ToListAsync();

            return PagedList<ProductModel>
                .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task CreateProduct(ProductModel product)
        {
            product.Created = DateTime.Now;
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductModel> GetProduct(string id)
        {
            var _Guid = new Guid(id);
            var model = await _context.Products.FirstOrDefaultAsync(m => m.Id == _Guid);

            model.ReadCnt = model.ReadCnt + 1;

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
