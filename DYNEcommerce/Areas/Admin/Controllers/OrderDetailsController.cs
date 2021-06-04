using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using DataAccessLayer.Admin;
using DataAccessLayer;
using System.Web.Security;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        public ActionResult OrderDetails()
        {
            if (Session["Authorized"] != null)
            {
                var dataProduct = Admin_OrderdataCRUD.Get_OrderDeialsProduct();
                ViewBag.OrderDetailsProduct = dataProduct;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}