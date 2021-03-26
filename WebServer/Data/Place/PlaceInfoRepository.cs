using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebServer.Data.Common;
using WebServer.Models.Features;
using WebServer.Models.Places;

namespace WebServer.Data.Place
{
    public class PlaceInfoRepository : IPlaceInfoRepository
    {
        private readonly ApplicationDbContext context; 
        private readonly ILogger logger;

        public PlaceInfoRepository(ApplicationDbContext context, ILoggerFactory loggerFactory) {
            this.context = context;
            this.logger = loggerFactory.CreateLogger(nameof(PlaceInfoRepository));
        }

        public async Task AddAsync(PlaceInfo placeInfo)
        {
            try
            {
                placeInfo.Created = DateTime.Now;
                context.PlaceInfo.Add(placeInfo);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError($"ERROR({nameof(AddAsync)}): {e.Message}");
            }
        }

        public async Task<PagedList<PlaceInfo>> GetAllAsync(PageParameters parameters)
        {
            var notes = await context.PlaceInfo
                .Search(parameters.SearchTerm)
                .Sort(parameters.OrderBy)
                .ToListAsync();

            return PagedList<PlaceInfo>
                .ToPagedList(notes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
