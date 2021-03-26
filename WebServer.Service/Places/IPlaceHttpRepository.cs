using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebServer.Models.Features;
using WebServer.Models.Places;

namespace WebServer.Service.Places
{
    public interface IPlaceHttpRepository : IHttpRepository<PlaceInfo>
    {
        Task<PagingResponse<PlaceInfo>> GetItems(PageParameters noteParameters);
    }
}
