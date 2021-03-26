using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models.Places;

namespace WebServer.Service.Places
{
    public class PlaceImageHttpRespository : IPlaceImageHttpRespository
    {
        private readonly HttpClient _client;

        public PlaceImageHttpRespository(HttpClient client)
        {
            _client = client;
        }

        public async Task<PlaceImageResponse> UploadImage(MultipartFormDataContent item)
        {
            //var content = System.Text.Json.JsonSerializer.Serialize(item);
            //var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            ////var postResult = await _client.PostAsync("/api/placeinfo", bodyContent);


            ////var postContent = await postResult.Content.ReadAsStringAsync();

            ////if (!postResult.IsSuccessStatusCode)
            ////{
            ////    throw new ApplicationException(postContent);
            ////}


            var postResult = await _client.PostAsync("/api/PlaceImageInfo", item);
            var postContent = await postResult.Content.ReadAsStringAsync();
            PlaceImageResponse response = JsonConvert.DeserializeObject<PlaceImageResponse>(postContent);
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                return response;
            }
        }
    }
}
