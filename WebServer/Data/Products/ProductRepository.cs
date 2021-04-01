using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Product;

namespace WebServer.Data.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProductRepository(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            this._logger = loggerFactory.CreateLogger(nameof(ProductRepository));
        }

        public async Task<PagedList<ProductModel>> GetProducts(ProductParameters productParameters)
        {
            var products = await _context.Products
                .WithSoldOut(productParameters.WithSoldOut)
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

        public async Task<bool> EditAsync(ProductRequestModel model)
        {
            try
            {
       
                var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == model.Id);

                product.Id = model.Id;
                product.Name = model.Name;
                product.Discription = model.Discription;
                product.Supplier = model.Supplier;
                product.Price = model.Price;
                product.ImageUrl = model.ImageUrl;
                product.Modified = DateTime.Now;
                product.TotalCount = model.TotalCount;
                product.IsSoldOut = model.IsSoldOut;

                _context.Products.Attach(product);
                _context.Entry(product).State = EntityState.Modified;


                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR({nameof(EditAsync)}): {e.Message}");
            }

            return false;
        }
    }
}
