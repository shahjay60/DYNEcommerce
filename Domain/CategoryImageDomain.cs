using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain
{
   public class CategoryImageDomain
    {
        public int Id { get; set; }
        public string GRP_CD { get; set; }

        [Required]
        public string ImageName { get; set; }
        [Required]

        public string CategoryName { get; set; }

        public SelectList Categories { get; set; }
    }
}
