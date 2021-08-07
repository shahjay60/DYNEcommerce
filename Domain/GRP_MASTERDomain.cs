using System.Collections.Generic;

namespace Domain
{
    public class GRP_MASTERDomain
    {
        public string GRP_CD { get; set; }
        public string GRP_NAME { get; set; }
        public string GRP_SNAME { get; set; }
        public string FOR_GRP_CD { get; set; }
        public string LEVEL_TEXT { get; set; }
        public string GROUP_YN { get; set; }
        public string BrandName { get; set; }
        public string ImageName { get; set; }
        public bool? Isonhomepage { get; set; }
        public bool? Isonmenu { get; set; }
        public string BrandId { get; set; }

        public string ProductName { get; set; }
        public string Pid { get; set; }
        public string ProductImage { get; set; }
        public string ProductPrice { get; set; }


    }

    public class SubCategory
    {
        public int Grp_Id { get; set; }
        public string SubCategoryName { get; set; }
    }

    public class OrderVM
    {
        public GRP_MASTERDomain order { get; set; }
        public List<GRP_MASTERDomain> orderDetails { get; set; }
    }
}
