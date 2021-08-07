using System.Web.Mvc;

namespace Domain
{
    public class BrandCategoryDomain
    {
        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }

        public int brandId { get; set; }
        public string GRP_CD { get; set; }
    }
}
