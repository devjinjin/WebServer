using BlazorInputFile;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebServer.Service.Notes
{
    public class ProductHttpRepository: IProductHttpRepository
    {
        private readonly HttpClient _client;

        public ProductHttpRepository(HttpClient client)
        {
            _client = client;
        }

        //public async Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters)
        //{
        //    var queryStringParam = new Dictionary<string, string>
        //    {
        //        ["pageNumber"] = productParameters.PageNumber.ToString(),
        //        ["searchTerm"] = productParameters.SearchTerm == null ? "" : productParameters.SearchTerm,
        //        ["orderBy"] = productParameters.OrderBy
        //    };

        //    var response = await _client.GetAsync(QueryHelpers.AddQueryString("https://localhost:5011/api/products", queryStringParam));
        //    var content = await response.Content.ReadAsStringAsync();
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new ApplicationException(content);
        //    }

        //    var pagingResponse = new PagingResponse<Product>
        //    {
        //        Items = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
        //        MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        //    };

        //    return pagingResponse;
        //}

        //public async Task CreateProduct(Product product)
        //{
        //    var content = JsonSerializer.Serialize(product);
        //    var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    var postResult = await _client.PostAsync("https://localhost:5011/api/products", bodyContent);
        //    var postContent = await postResult.Content.ReadAsStringAsync();

        //    if (!postResult.IsSuccessStatusCode)
        //    {
        //        throw new ApplicationException(postContent);
        //    }
        //}

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("https://localhost:5011/api/files", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5011/files", postContent);
                return imgUrl;
            }
        }

    }
}
