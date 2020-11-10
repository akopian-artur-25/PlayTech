using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayTech.Shared.Data.Interfaces;

namespace PlayTech.WebAPI.Models.Shared.Data
{
    public class PagedListVM<T> : IPagedList<T>
    {
        public PagedListVM()
        {
            Data = new List<T>();
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public List<T> Data { get; set; }
    }
}
