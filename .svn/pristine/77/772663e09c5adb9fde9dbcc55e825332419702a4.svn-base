using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class MenuController : Controller
    {
        TMATS00012020Entities objEntity = new TMATS00012020Entities();
        // GET: Menu
        public ActionResult Index()
        {
            //"1" is customerid

            if (Session["idUser"] != null)
            {
                ViewBag.totalItemsInWishList = CustmorWishlistCRUD.GetWishlistByCustomerId(Convert.ToInt32(Session["idUser"])).Count();
            }
            else
            {
                ViewBag.totalItemsInWishList = 0;
            }
            return View();
        }

        public ActionResult GetMenuList()
        {
            try
            {
                var data = GRP_MASTERCRUD.GetAllMenu();
                ViewBag.totalItemsInWishList = CustmorWishlistCRUD.GetWishlistByCustomerId(Convert.ToInt32(Session["idUser"])).Count();

                return PartialView("Menu", data);
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "GetMenuList";
                obj.ControllerName = "Menu";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }


        public ActionResult GetBrandList()
        {
            try
            {
                var data = BrandmasterCRUD.GetBrandMaster().Where(x => x.IsOnHomePage == true).ToList();
                return PartialView("_BrandList", data);
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "GetMenuList";
                obj.ControllerName = "Menu";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult GetMenuSubMenuList()
        {
            try
            {
                var data = GRP_MASTERCRUD.GetAllMenu();
                return PartialView("Menu_Submenu", data);
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "GetMenuSubMenuList";
                obj.ControllerName = "Menu";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult CartItem()
        {
            try
            {
                if (Session["idUser"] != null)
                {
                    var cartData = CustomerCartCRUD.GetCartByCustomerId(Convert.ToInt32(Session["idUser"]));
                    cartData = cartData.Where(x => x.IsPlace == false).ToList();
                    ViewBag.totalAmount = cartData.Sum(x => x.Amount).ToString();
                    return PartialView("_CartMenu", cartData);
                }
                else
                {
                    List<CustomerCartDomain> mData = new List<CustomerCartDomain>();
                    ViewBag.totalAmount = 0;
                    return PartialView("_CartMenu", mData);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "CartItem";
                obj.ControllerName = "Menu";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult WishlistItem()
        {
            try
            {
                if (Session["idUser"] != null)
                {
                    var wishListData = CustmorWishlistCRUD.GetWishlistByCustomerId(Convert.ToInt32(Session["idUser"]));
                    return PartialView("_WishListItems", wishListData);
                }
                else
                {
                    List<CustomerWishlistDomain> mData = new List<CustomerWishlistDomain>();

                    return PartialView("_WishListItems", mData);

                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "WishlistItem";
                obj.ControllerName = "Menu";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }


        public ActionResult TopSearchBar()
        {
            try
            {
                //"1" is customerid
                if (Session["idUser"] != null)
                {
                    ViewBag.totalItemsInCart = CustomerCartCRUD.GetCartByCustomerId(Convert.ToInt32(Session["idUser"])).Where(x=>x.IsPlace==false).Count();
                    ViewBag.totalItemsInWishList = CustmorWishlistCRUD.GetWishlistByCustomerId(Convert.ToInt32(Session["idUser"])).Count();
                }
                else
                {
                    ViewBag.totalItemsInCart = 0;
                    ViewBag.totalItemsInWishList = 0;
                }
                return PartialView("_TopSearchMenu");
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "TopSearchBar";
                obj.ControllerName = "Menu";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult MyAccount()
        {
            try
            {
                return PartialView("_MyAccount");
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "MyAccount";
                obj.ControllerName = "Menu";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }
    }
}