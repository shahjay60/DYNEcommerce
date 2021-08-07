using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    var cartData = CustomerCartCRUD.GetCartByCustomerId(1).Where(x => x.IsPlace == false).ToList();
                    cartData = cartData.GroupBy(x => x.ITEM_DESC).Select(x => x.FirstOrDefault()).ToList();
                    //cartData = cartData.GroupBy(x => x.ITEM_DESC).Select(x => x.FirstOrDefault()).ToList();
                    List<Cartattribute> Cartattributeobj = new List<Cartattribute>();
                    var res = cartData.SelectMany(
                                                    art => art.AttributeValue.Split(',').Select(n =>
                                                    new Cartattribute
                                                    {
                                                        AttributeValue = n.Trim()
                                                    }
                                                 ));
                    foreach (var item in res)
                    {
                        Cartattribute mobj = new Cartattribute();
                        var arrtibuteType = AttributeCRUD.GetAttributesData().Where(x => x.AttributeValue.Contains(item.AttributeValue.ToLower()))
                                                                             .Select(x => x.AttributeName)
                                                                             .FirstOrDefault();
                        mobj.AttributeValue = item.AttributeValue;
                        mobj.AttributeType = arrtibuteType;
                        Cartattributeobj.Add(mobj);
                    }
                    ViewBag.totalAmount = cartData.Sum(x => x.Amount * x.Quantity).ToString();
                    ViewBag.AttributeValues = Cartattributeobj;

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