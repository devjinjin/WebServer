using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using WebServer.Models.Product;
using WebServer.Models.Places;

namespace WebServer.Data.Place
{
    public static class RepositoryPlaceInfoExtensions
    {
        public static IQueryable<PlaceInfo> Search(this IQueryable<PlaceInfo> PlaceInfos, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return PlaceInfos;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return PlaceInfos.Where(p => p.Title.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<PlaceInfo> Sort(this IQueryable<PlaceInfo> PlaceInfos, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return PlaceInfos.OrderBy(e => e.Title);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(PlaceInfo).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery)) {
                return PlaceInfos.OrderBy(e => e.Title);
            }
             

            return PlaceInfos.OrderBy(orderQuery);
        }
    }
}
