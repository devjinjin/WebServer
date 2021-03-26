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
        private string ImageUrl = "";
        private PlaceSuccessNotification _notification;

        [Inject]
        public IPlaceHttpRepository repository { get; set; }

        [Inject]
        public IUploadHttpRepository imageRepository { get; set; }

        private async Task Create()
        {
            if (ImageUrl != null)
            {
                _placeInfo.PlaceId = 0;
                _placeInfo.Title = "string";
                _placeInfo.Discription = "string";
                _placeInfo.Content = "string";
                _placeInfo.OpenTime = DateTime.Now;
                _placeInfo.CloseTime = DateTime.Now;
                _placeInfo.CloseDay = "string";
                _placeInfo.Price = 0;
                _placeInfo.PlaceNotice = "string";
                _placeInfo.Latitude = 0;
                _placeInfo.Longitude = 0;
                _placeInfo.Address = "string";
                _placeInfo.PostAddress = "string";
                _placeInfo.OriginPrice = 0;
                _placeInfo.CompanyId = 0;
                _placeInfo.Company = "string";
                _placeInfo.Manager = "string";
                _placeInfo.Tel = "string";
                _placeInfo.HomePage = "string";
                _placeInfo.Email = "string";
                _placeInfo.RegistDate = DateTime.Now;
                _placeInfo.KeepCount = 0;


                await repository.Create(_placeInfo);


                _notification.Show();
            } 
        }

        private void AssignImageUrl(string imgUrl)
        {
            ImageUrl = imgUrl;
        }
    }
}
