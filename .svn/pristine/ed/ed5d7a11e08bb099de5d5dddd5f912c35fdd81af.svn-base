using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: checkout
        public ActionResult Index()
        {
            try
            {
                if (Session["idUser"] != null)
                {

                    CheckoutDomain mCheckoutDomain = new CheckoutDomain();
                    List<CustomerCartDomain> customercartList = new List<CustomerCartDomain>();

                    var customerCartOrders = CustomerCartCRUD.GetCartByALLCustomerId(Convert.ToInt32(Session["idUser"]));


                    mCheckoutDomain.Id = customerCartOrders[0].Id;
                    mCheckoutDomain.FirstName = customerCartOrders[0].FirstName;
                    mCheckoutDomain.LastName = customerCartOrders[0].LastName;
                    mCheckoutDomain.Email = customerCartOrders[0].Email;
                    mCheckoutDomain.Phone = customerCartOrders[0].Phone;
                    mCheckoutDomain.Country = customerCartOrders[0].Country;
                    mCheckoutDomain.State = customerCartOrders[0].State;
                    mCheckoutDomain.City = customerCartOrders[0].City;
                    mCheckoutDomain.Pincode = customerCartOrders[0].Pincode;
                    mCheckoutDomain.ShippingAddress = customerCartOrders[0].ShippingAddress;
                    mCheckoutDomain.BillingAddress = customerCartOrders[0].BillingAddress;

                    for (int i = 0; i < customerCartOrders.Count(); i++)
                    {
                        CustomerCartDomain mObj = new CustomerCartDomain();
                        mObj.Amount = customerCartOrders[i].Amount;
                        mObj.Quantity = customerCartOrders[i].Quantity;
                        mObj.ITEM_DESC = customerCartOrders[i].ProductName;
                        mObj.Id = customerCartOrders[i].cartId;
                        mObj.IsPlace = customerCartOrders[i].IsPlaced;

                        customercartList.Add(mObj);
                    }
                    ViewBag.CustomerscartList = customercartList.Where(x=>x.IsPlace==false).ToList();
                    Session["customerCartOrders"] = customercartList;

                    ViewBag.Subtotal = customercartList.Where(x => x.IsPlace == false).Sum(x => x.Amount);
                    return View(mCheckoutDomain);
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
                obj.ControllerName = "Checkout";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");

            }
        }

        public ActionResult PlaceOrder(CustomerOrderDomain model)
        {
            try
            {
                var orders = CustomerOrderCRUD.GetCustomerOrderByCustomerId(0);

                int lastOrder = 0;
                if (orders.Count > 1)
                    lastOrder = orders[0].Id;

                model.OrderDate = DateTime.Now;
                model.PaymentType = "COD";
                model.TransactionId = "Trans00" + lastOrder + 1;

                CustomerOrderCRUD.AddToCustomerOrder(model);

                List<CustomerCartDomain> sessionCustomerCartOrders = (List<CustomerCartDomain>)Session["customerCartOrders"];
                if (sessionCustomerCartOrders != null)
                {
                    foreach (var item in sessionCustomerCartOrders)
                    {
                        bool res = CustomerCartCRUD.UpdateDetails(item.Id, true);
                    }
                }
                else
                {
                    var customerCartOrders = CustomerCartCRUD.GetCartByALLCustomerId(Convert.ToInt32(Session["idUser"]));
                    foreach (var item in customerCartOrders)
                    {
                        bool res = CustomerCartCRUD.UpdateDetails(item.Id, true);
                    }

                }
                return Json("True", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "PlaceOrder";
                obj.ControllerName = "Checkout";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;
                ExceptionLogCRUD.AddToExceptionLog(obj);

                return Json("False", JsonRequestBehavior.AllowGet);
            }
        }
    }
}