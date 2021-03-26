using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Places;

namespace WebServer.Service.Places
{
    public class PlaceHttpRepository : IPlaceHttpRepository
    {
        private readonly HttpClient _client;

        public PlaceHttpRepository(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// 생성
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task Create(PlaceInfo item)
        {
            var content = JsonSerializer.Serialize(item);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _client.PostAsync("/api/placeinfo", bodyContent);


            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public Task Edit(PlaceInfo item)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResponse<PlaceInfo>> GetItems(PageParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = parameters.PageNumber.ToString(),
                ["searchTerm"] = parameters.SearchTerm == null ? "" : parameters.SearchTerm,
                ["orderBy"] = parameters.OrderBy
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("/api/placeinfo", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<PlaceInfo>
            {
                Items = JsonSerializer.Deserialize<List<PlaceInfo>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }
    }
}
