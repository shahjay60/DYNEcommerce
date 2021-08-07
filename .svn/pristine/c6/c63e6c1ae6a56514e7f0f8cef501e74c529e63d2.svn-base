using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DYNEcommerce.Models;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer;
using Domain;
namespace DYNEcommerce.Controllers
{
    public class grp_masterController : ApiController
    {

        //public IHttpActionResult getgrp_master()
        //{
        //    List<grp_masterTbl> grp = new List<grp_masterTbl>();
        //    string mainconn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        //    SqlConnection sqlconn = new SqlConnection(mainconn);
        //    string sqlquery = "select * from GRP_MASTER";
        //    SqlCommand sqlcmd = new SqlCommand(sqlquery, sqlconn);
        //    SqlDataReader sdr = sqlcmd.ExecuteReader();
        //    while (sdr.Read())
        //    {
        //        grp.Add(new grp_masterTbl()
        //        {
        //            GRP_CD = sdr.GetValue(0).ToString(),
        //            GRP_NAME = sdr.GetValue(1).ToString(),
        //            FOR_GRP_CD = sdr.GetValue(2).ToString(),
        //            LEVEL_TEXT = sdr.GetValue(3).ToString(),
        //            GROUP_YN = sdr.GetValue(4).ToString()
        //        });
        //    }
        //    return Ok(grp);
        //}
        public IHttpActionResult getgrp_masters()
        {
          
            HttpResponseMessage response;
            try
            {
                var detailsResponse = GRP_MASTERCRUD.GetAllMenu();
                if (detailsResponse != null)
                    response = Request.CreateResponse<List<GRP_MASTERDomain>>(HttpStatusCode.OK, detailsResponse);
                else
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return (IHttpActionResult)response;
        }


    }
}
