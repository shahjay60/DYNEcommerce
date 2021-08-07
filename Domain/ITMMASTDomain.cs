using System.Collections.Generic;
using System.Web.Mvc;

namespace Domain
{
    public class ITMMASTDomain
    {
        public string GRP_CD { get; set; }
        public string Item_CD { get; set; }
        public string Item_ID { get; set; }
        public string Item_Desc { get; set; }
        public double Sale_Price { get; set; }
        public string DetailDesc { get; set; }
        public double? Offer_Price { get; set; }
        public string Product_Image { get; set; }
        public string Qty { get; set; }
        public string BrandId { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeName { get; set; }
        public string viewon { get; set; }
        public List<string> ProductImages { get; set; }
        public List<Product_Attribute> ProductAttributes { get; set; }
        public bool Isnewarriaval { get; set; }
        public string GRP_NAME { get; set; }
        public string Bar_Code { get; set; }
        public string SelectedAttributeValue { get; set; }

        public SelectList Categories { get; set; }
    }

}
