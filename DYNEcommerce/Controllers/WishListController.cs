using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {
            try
            {
                if (Session["idUser"] != null)
                {
                    var wishListData = CustmorWishlistCRUD.GetWishlistByCustomerId(Convert.ToInt32(Session["idUser"]));
                    return View(wishListData);
                }
                else
                {
                    return RedirectToAction("Login", "Customer");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "Index";
                obj.ControllerName = "WishList";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

        public ActionResult DeleteWishListItem(int wishListId)
        {
            //var wishListData = CustmorWishlistCRUD.GetWishlistByWishlistId(wishListId);
            try
            {
                var result = CustmorWishlistCRUD.DeleteWhishListItem(wishListId);
                if (result == true)
                {
                    return Json(result);
                }
                else
                {
                    return RedirectToAction("Index", "Error");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "DeleteWishListItem";
                obj.ControllerName = "WishList";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

    }
}