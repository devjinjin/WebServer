using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebServer.Data.Products;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IWebHostEnvironment environment,  IProductRepository repository, ILogger<ProductsController> logger)
        {
            _environment = environment;
            _repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProductParameters productParameters)
        {
            var products = await _repository.GetProducts(productParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

            return Ok(products);
        }

        // 상세
        // GET api/Notices/1
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var model = await _repository.GetProduct(id);
            return Ok(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
        {
            if (product == null) {
                return BadRequest();
            }

            await _repository.CreateProduct(product);

            return Created("", product);
        }
      
    }
}
