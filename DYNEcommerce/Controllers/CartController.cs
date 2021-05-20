using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            try
            {
                if (Session["idUser"] != null)
                {
                    var cartData = CustomerCartCRUD.GetCartByCustomerId(Convert.ToInt32(Session["idUser"]));
                    cartData = cartData.Where(x => x.IsPlace == false).ToList();
                    ViewBag.totalAmount = cartData.Sum(x => x.Amount).ToString();

                    return View(cartData);
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
                obj.ControllerName = "Cart";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");

            }

        }
        public ActionResult DeleteCartItem(int cartId)
        {
            try
            {
                var result = CustomerCartCRUD.DeleteCartItem(cartId);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "Index";
                obj.ControllerName = "Cart";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");

            }
        }
    }
}