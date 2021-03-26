using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Notes;

namespace WebServer.Service.Notes
{
    public class NoteHttpRepository : INoteHttpRepository
    {
        private readonly HttpClient _client;

        public NoteHttpRepository(HttpClient client)
        {
            _client = client;
        }


        public async Task Create(NoteRequest note)
        {
            var content = JsonSerializer.Serialize(note);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _client.PostAsync("/api/notes", bodyContent);


            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

 
        public async Task<bool> UpdateNote(NoteRequest note)
        {
            var content = JsonSerializer.Serialize(note);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var result = await _client.PutAsync("/api/notes", bodyContent);




            return result.IsSuccessStatusCode;
        }

        public async Task<PagingResponse<NoteModel>> GetNotes(NoteParameters noteParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = noteParameters.PageNumber.ToString(),
                ["searchTerm"] = noteParameters.SearchTerm == null ? "" : noteParameters.SearchTerm,
                ["orderBy"] = noteParameters.OrderBy
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("/api/notes", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<NoteModel>
            {
                Items = JsonSerializer.Deserialize<List<NoteModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }

        public async Task UploadDelete(string path)
        {
            var postResult = await _client.DeleteAsync($"/api/files/{path}");

            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
          
        }

      
    }
}
