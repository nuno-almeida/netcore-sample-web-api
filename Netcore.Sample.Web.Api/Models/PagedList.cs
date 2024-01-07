using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Netcore.Sample.Web.Api.Models
{
    public class PagedList<T>
    {
        public int TotalItems { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public bool HasNextPage => PageIndex * PageSize < TotalItems;

        public bool HasPreviousPage => PageIndex > 1;

        public List<T> Items { get; private set; }

        public PagedList(List<T> items, int totalItems, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItems = totalItems;
            Items = items;
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
