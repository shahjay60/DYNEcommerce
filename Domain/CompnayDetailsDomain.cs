using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
  public  class CompnayDetailsDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
       
        public string EmailAddress { get; set; }
        public string Logo { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string FaxNo { get; set; }



    }
}
