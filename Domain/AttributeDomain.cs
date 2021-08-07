using System.Collections.Generic;
using System.Web.Mvc;

namespace Domain
{
    public class AttributeDomain
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

        public IList<SelectListItem> AttributeValues { get; set; }
    }
}
