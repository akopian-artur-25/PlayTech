﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlayTech.Shared.Data.Interfaces
{
    public interface IPagedList<T>
    {       
        /// <summary>
        /// Page index
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Total count
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Has next age
        /// </summary>
        bool HasNextPage { get; }

        List<T> Data { get; set; }
    }
}
