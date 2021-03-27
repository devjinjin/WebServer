using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models.Category;

namespace WebServer.Service.Common.Categories
{
    public class CategoryHttpRepository : ICategoryHttpRepository
    {
        private readonly HttpClient _client;

        public CategoryHttpRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<CategoryModel>> GetCategories()
        {

            var response = await _client.GetAsync("/api/category");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var categories = JsonSerializer.Deserialize<List<CategoryModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
           
            return categories;
        }
    }
}
