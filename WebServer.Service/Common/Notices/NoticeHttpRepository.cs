using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notices;

namespace WebServer.Service.Common.Notices
{
    public class NoticeHttpRepository : INoticeHttpRepository
    {
        private readonly HttpClient _client;

        public NoticeHttpRepository(HttpClient client)
        {
            _client = client;
        }
        public async Task Create(NoticeModel item)
        {
            var content = JsonSerializer.Serialize(item);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _client.PostAsync("/api/notices", bodyContent);


            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task Edit(NoticeModel item)
        {
            var content = JsonSerializer.Serialize(item);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var putResult = await _client.PutAsync($"/api/notices/{item.Id}", bodyContent);


            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }

        public async Task<PagingResponse<NoticeModel>> GetItems(NoticParameters noteParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = noteParameters.PageNumber.ToString(),
                ["searchTerm"] = noteParameters.SearchTerm == null ? "" : noteParameters.SearchTerm,
                ["orderBy"] = noteParameters.OrderBy
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("/api/notices", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<NoticeModel>
            {
                Items = JsonSerializer.Deserialize<List<NoticeModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }
    }
}
