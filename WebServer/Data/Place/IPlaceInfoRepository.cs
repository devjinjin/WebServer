using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Features;
using WebServer.Models.Places;

namespace WebServer.Data.Place
{
    public interface IPlaceInfoRepository
    {
        Task<PagedList<PlaceInfo>> GetAllAsync(PageParameters parameters);
        Task AddAsync(PlaceInfo placeInfo);
    }
}
