using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Models.Popup;
using WebServer.Service.Common.Popups;

namespace WebServer.Client.Pages
{
    public partial class Index
    {
        [Inject]
        IPopupHttpRepository popupHttpRepository { get; set; }

        List<PopupModel> popups;
        protected override async Task OnInitializedAsync()
        {
            popups = await popupHttpRepository.GetPopups();
            //시작 popup 관련
        }
    }
}
