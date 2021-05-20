using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
using Domain;
namespace DYNEWEBAPI.Controllers
{
    public class grp_masterAPIController : ApiController
    {

        public IHttpActionResult getgrp_master()
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            grp = GRP_MASTERCRUD.GetAllMenu();
            return Ok(grp);                 
        }

        public IHttpActionResult getgrp_masterById(string GRP_CDs)
        {
            List<GRP_MASTERDomain> grp = new List<GRP_MASTERDomain>();
            grp = GRP_MASTERCRUD.GetMenuById(GRP_CDs);
            return Ok(grp);
        }
    }
}
