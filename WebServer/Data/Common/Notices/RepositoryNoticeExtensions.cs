using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using WebServer.Models.Notices;

namespace WebServer.Data.Common.Notices
{
    public static class RepositoryNoticeExtensions
    {


        public static IQueryable<NoticeModel> Search(this IQueryable<NoticeModel> items, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return items;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return items.Where(p => p.Title.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<NoticeModel> Sort(this IQueryable<NoticeModel> items, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return items.OrderBy(e => e.Title);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(NoticeModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                //var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(orderQuery)) {
                return items.OrderBy(e => e.Title);
            }
             

            return items.OrderBy(orderQuery);
        }
    }
}
