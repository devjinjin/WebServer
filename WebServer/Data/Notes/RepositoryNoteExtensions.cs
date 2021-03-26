using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using WebServer.Models.Product;
using WebServer.Models.Notes;

namespace WebServer.Data.Notes
{
    public static class RepositoryNoteExtensions
    {
        public static IQueryable<NoteModel> Search(this IQueryable<NoteModel> notes, string searchTearm)
        {
            if (string.IsNullOrWhiteSpace(searchTearm))
                return notes;

            var lowerCaseSearchTerm = searchTearm.Trim().ToLower();

            return notes.Where(p => p.Title.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<NoteModel> Sort(this IQueryable<NoteModel> notes, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return notes.OrderBy(e => e.Title);

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(NoteModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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
                return notes.OrderBy(e => e.Title);
            }
             

            return notes.OrderBy(orderQuery);
        }
    }
}
