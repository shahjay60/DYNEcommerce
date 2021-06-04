using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain
{
    public class BrandmasterDomain
    {
        public int BrandId { get; set; }
        public string SelectedBrandId { get; set; }

        public string GRP_CD { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }

        public bool IsOnHomePage { get; set; }
        public bool ISOnWeb { get; set; }
        public SelectList Brands { get; set; }

        public SelectList Categories { get; set; }


    }
}
