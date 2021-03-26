using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebServer.Service.Upload;

namespace WebServer.Service.Products
{
    public class UploadHttpRepository : IUploadHttpRepository
    {
        private readonly HttpClient _client;
 
        public UploadHttpRepository(HttpClient client)
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
