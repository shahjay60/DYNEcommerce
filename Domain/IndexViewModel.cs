using System;
using System.Collections.Generic;
using System.Configuration;

namespace Domain
{
    public class IndexViewModel
    {
        public List<ITMMASTDomain> Items { get; set; }
        public List<GRP_MASTERDomain> Categories { get; set; }

        public Pager Pager { get; set; }
    }
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize)
        {
            if (pageSize == 0)
            {
                pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
            }
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public string catId { get; set; }
    }
}
