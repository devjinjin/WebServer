using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.Places;
using WebServer.Service;
using WebServer.Service.Places;
using WebServer.Service.Upload;

namespace WebServer.Client.Pages.Place
{
    public partial class PlaceCreate
    {

        private PlaceInfo _placeInfo = new PlaceInfo();
        private TimeSample _timeSample = new TimeSample();
        private PlaceSuccessNotification _notification;

        [Inject]
        public IPlaceHttpRepository repository { get; set; }

        [Inject]
        public IUploadHttpRepository imageRepository { get; set; }

        public class TimeSample
        {
            public DateTime date { get; set; }
            public DateTime time { get; set; }
            public string text { get; set; }
        };

                 

        private async Task Create()
        {
            if (_placeInfo.MainImage.Length > 0)
            {
                _placeInfo.Latitude = 37.60747646883645;
                _placeInfo.Longitude = 127.15007826373763;
                
                await repository.Create(_placeInfo);
                _notification.Show();
            } 
        }

        private void AssignImageUrl(string fileName)
        {
            _placeInfo.MainImage = fileName;
        }
    }
}
