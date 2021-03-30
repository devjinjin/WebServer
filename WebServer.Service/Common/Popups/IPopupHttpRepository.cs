using System.Collections.Generic;
using System.Threading.Tasks;
using WebServer.Models.Popup;

namespace WebServer.Service.Common.Popups
{
    public interface IPopupHttpRepository
    {
        Task<List<PopupModel>> GetPopups();
    }
}
