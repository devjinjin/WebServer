using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Service.Places
{
    public class PlaceImageHttpRespository : IPlaceImageHttpRespository
    {
        private readonly HttpClient _client;

        public PlaceImageHttpRespository(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> UploadImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync("/api/files", content);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = postContent;
                return imgUrl;
            }
        }
    }
}
