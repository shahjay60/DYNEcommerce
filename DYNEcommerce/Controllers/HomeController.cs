using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.BrandOnHomePage = BrandmasterCRUD.GetBrandMaster().Where(x => x.IsOnHomePage == true);
            if (Session["idUser"] != null)
            {
                ViewBag.totalItemsInCart = CustomerCartCRUD.GetCartByCustomerId(Convert.ToInt32(Session["idUser"])).Where(x => x.IsPlace == false).Count();
                ViewBag.totalItemsInWishList = CustmorWishlistCRUD.GetWishlistByCustomerId(Convert.ToInt32(Session["idUser"])).Count();
            }
            else
            {
                ViewBag.totalItemsInCart = 0;
                ViewBag.totalItemsInWishList = 0;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}