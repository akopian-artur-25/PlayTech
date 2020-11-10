using System;
using System.Collections.Generic;
using System.Text;

namespace PlayTech.Shared.CQS.Models
{
    public class BaseListQueryFilter
    {
        protected BaseListQueryFilter()
        {
            PageIndex = 0;
            PageSize = 25;
        }

        private int _pageIndex;

        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                if (value < 0)
                    value = 0;
                _pageIndex = value;
            }
        }

        private int _pageSize;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1)
                    value = 1;
                else if (value > MaxPageSize)
                    value = MaxPageSize;

                _pageSize = value;
            }
        }

        protected virtual int MaxPageSize => 10000;
    }
}
