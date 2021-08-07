using System.Collections.Generic;
using System.Web.Mvc;

namespace Domain
{
    public class Product_Attribute
    {
        public int PaId { get; set; }
        public string ITEM_CD { get; set; }
        public string ProductName { get; set; }
        public int AttributeId { get; set; }
        public int AttributeValueId { get; set; }

        public string AttributeValue { get; set; }
        public string AttributeName { get; set; }
        public string Price { get; set; }
        public string OfferPrice { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        public IEnumerable<SelectListItem> Attributes { get; set; }




    }
}
