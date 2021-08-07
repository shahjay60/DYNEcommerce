using System.Web.Mvc;

namespace Domain
{
    public class ProductImageDomain
    {
        public int Id { get; set; }
        public string Pid { get; set; }
        public string Viewon { get; set; }
        public string Image { get; set; }
        public SelectList Products { get; set; }
        public string ProductName { get; set; }
        public string AttrValue { get; set; }

        public SelectList Attributes { get; set; }
        public SelectList AttributeValue { get; set; }
        public int AttributeId { get; set; }
        public int AttributevalueId { get; set; }
        public string[] ImageNames { get; set; }

    }
}
