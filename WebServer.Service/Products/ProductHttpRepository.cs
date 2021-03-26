using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Product;

namespace WebServer.Service.Products
{
    public class ProductHttpRepository : IProductHttpRepository
    {
        private readonly HttpClient _client;

        public ProductHttpRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<PagingResponse<ProductModel>> GetProducts(ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["searchTerm"] = productParameters.SearchTerm == null ? "" : productParameters.SearchTerm,
                ["orderBy"] = productParameters.OrderBy
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("/api/products", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<ProductModel>
            {
                Items = JsonSerializer.Deserialize<List<ProductModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task Create(ProductModel product)
        {
            var content = JsonSerializer.Serialize(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _client.PostAsync("/api/products", bodyContent);


            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public Task Edit(ProductModel item)
        {
            throw new NotImplementedException();
        }
    }
}
