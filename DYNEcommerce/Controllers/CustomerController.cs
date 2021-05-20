using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerDashbord


        public ActionResult Index()
        {
            int custid = Convert.ToInt32(Session["idUser"]);
            if (custid != 0)
            {
                var CustomerDetails = CustomerCRUD.GetCustomerById(custid);

                return View(CustomerDetails);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(CustomerModel model)
        {
            try
            {
                var result = CustomerCRUD.CustomerLogin(model.Email, model.Password);

                if (result.Count() > 0)
                {
                    Session["FullName"] = result[0].FirstName + " " + result[0].LastName;
                    Session["idUser"] = result[0].Id;

                    return RedirectToAction("Index", "Menu");

                }
                else
                {
                    ViewBag.error = "Login failed";
                    return View();
                }

            }
            catch (Exception ex)
            {

                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "Login";
                obj.ControllerName = "Customer";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

        [HttpGet]
        public ActionResult Register(string message = "")
        {
            ViewBag.Success = message;
            return View();
        }

        [HttpPost]
        public ActionResult Register(CustomerDomain model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TO DO
                    CustomerCRUD.AddToCustomer(model);
                    return View();
                }
                return View("Registered");
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "Register";
                obj.ControllerName = "Customer";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

        public JsonResult ChkEmailExistsOrNot(string emailId)
        {
            try
            {
                var result = CustomerCRUD.ChkEmailExistsOrNot(emailId);
                return Json(result);
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "ChkEmailExistsOrNot";
                obj.ControllerName = "Customer";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return Json(false);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        public ActionResult CustomersOrders()
        {
            int custid = Convert.ToInt32(Session["idUser"]);
            if (custid != 0)
            {
                var data = CustomerCartCRUD.GetCartByCustomerId(custid);
                return View(data);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CustomersWishlist()
        {
            int custid = Convert.ToInt32(Session["idUser"]);
            var data = CustmorWishlistCRUD.GetWishlistByCustomerId(custid);
            return View(data);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChkPasswordExistsOrNot(string password)
        {
            var result = CustomerCRUD.ChkPasswordExistsOrNot(password);
            return View(result);
        }
        [HttpPost]
        public ActionResult ChangePassword(string password)
        {
            int custid = Convert.ToInt32(Session["idUser"]);

            if (custid != 0)
            {
                var result = CustomerCRUD.UpdateCustomerPassword(custid, password);
                return Json(result);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult MyAccount()
        {
            int custid = Convert.ToInt32(Session["idUser"]);
            if (custid != 0)
            {
                var CustomerDetails = CustomerCRUD.GetCustomerById(Convert.ToInt32(Session["idUser"]));

                CustomerDomain Customer = new CustomerDomain();

                Customer.Id = CustomerDetails[0].Id;
                Customer.FirstName = CustomerDetails[0].FirstName;
                Customer.LastName = CustomerDetails[0].LastName;
                Customer.Email = CustomerDetails[0].Email;
                Customer.Phone = CustomerDetails[0].Phone;
                Customer.Country = CustomerDetails[0].Country;
                Customer.State = CustomerDetails[0].State;
                Customer.City = CustomerDetails[0].City;
                Customer.Pincode = CustomerDetails[0].Pincode;
                Customer.ShippingAddress = CustomerDetails[0].ShippingAddress;
                Customer.BillingAddress = CustomerDetails[0].BillingAddress;
                return View(Customer);
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        public ActionResult UpdateCustomer(CustomerDomain model)
        {
            var result = CustomerCRUD.UpdateCustomerById(model);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}