using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Categorywiseproduct
    {
        public List<GRP_MASTERDomain> CategoryList { get; set; }
        public List<ITMMASTDomain> ProductList { get; set; }
        public List<BrandmasterDomain> BrandMasterList { get; set; }


    }
}
