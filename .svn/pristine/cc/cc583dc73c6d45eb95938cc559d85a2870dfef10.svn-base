using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            try
            {
                if (Session["idUser"] != null)
                {
                    int customerID = Convert.ToInt32(Session["idUser"]);
                    var orders = CustomerOrderCRUD.GetCustomerOrderByCustomerId(customerID);

                    List<Domain.CustomerCartDomain> customercartList = new List<Domain.CustomerCartDomain>();
                    var customerOrders = CustomerCartCRUD.GetCartByALLCustomerId(Convert.ToInt32(Session["idUser"]));
                    for (int i = 0; i < customerOrders.Count(); i++)
                    {
                        Domain.CustomerCartDomain mObj = new Domain.CustomerCartDomain();
                        mObj.Amount = customerOrders[i].Amount;
                        mObj.Quantity = customerOrders[i].Quantity;
                        mObj.ITEM_DESC = customerOrders[i].ProductName;
                        mObj.Id = customerOrders[i].cartId;
                        customercartList.Add(mObj);
                    }
                    ViewBag.CustomerscartList = customercartList;

                    return View(orders);
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
                obj.ControllerName = "Order";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }

        }

    }
}