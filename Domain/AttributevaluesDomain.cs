using System.Collections.Generic;
using System.Web.Mvc;

namespace Domain
{
    public class AttributevaluesDomain
    {
        public string AttributeValue { get; set; }
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public IEnumerable<SelectListItem> Attributes { get; set; }
    }
}
