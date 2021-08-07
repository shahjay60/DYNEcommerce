using System.Web.Mvc;

namespace Domain
{
    public class AdminCategory
    {
        public string GRP_CD { get; set; }
        public string GRP_SNAME { get; set; }
        public string GRP_NAME { get; set; }
        public string GROUP_YN { get; set; }
        public string FOR_GRP_CD { get; set; }
        public string LEVEL_TEXT { get; set; }
        public int BrandId { get; set; }
        public bool Isonhomepage { get; set; }
        public bool Isonmenu { get; set; }
        public string BrandName { get; set; }
        public SelectList Brands { get; set; }
        public SelectList CategoryList { get; set; }


    }
}
