using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models.Popup;

namespace WebServer.Service.Common.Popups
{
    public class PopupHttpRepository : IPopupHttpRepository
    {
        private readonly HttpClient _client;

        public PopupHttpRepository(HttpClient client)
        {
            _client = client;
        }


        public async Task<List<PopupModel>> GetPopups()
        {
            var response = await _client.GetAsync("/api/popup");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var popups = JsonSerializer.Deserialize<List<PopupModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return popups;
        }
    }
}
