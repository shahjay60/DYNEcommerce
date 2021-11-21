using DataAccessLayer;
using Domain;
using DYNEcommerce.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerDashbord

        private string Host = ConfigurationManager.AppSettings["Host"];
        private string Port = ConfigurationManager.AppSettings["Port"];
        private string Email = ConfigurationManager.AppSettings["Email"];
        private string Password = ConfigurationManager.AppSettings["Password"];

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
                    if (Session["LastURL"] != null)
                    {
                        string url = Session["LastURL"].ToString();
                        string fullURL = Request.Url.Authority + url;
                        Response.Redirect(url);

                    }
                    return RedirectToAction("Index", "Home");

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

                    Log.Info("Register Mail started...");
                    try
                    {

                        #region Send Email to new customer
                        MailMessage msgs = new MailMessage();
                        msgs.To.Add(model.Email);
                        MailAddress address = new MailAddress(Email);
                        msgs.From = address;
                        msgs.Subject = "Welcome to Buynoor";
                        //   msgs.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        string htmlBody = msgs.Subject = "Welcome to Think software! Take a tour of your Amazon account features here: http://buynoor.stockde.com/";
                        msgs.Body = htmlBody;
                        msgs.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient();
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;

                        client.EnableSsl = false;

                        //client.Host = "smtp.gmail.com";
                        //client.Port = 587;
                        client.Host = Host;
                        client.Port = Convert.ToInt32(Port);

                        // client.Credentials = new System.Net.NetworkCredential("email@gmail.com", "pass@");
                        NetworkCredential credentials = new NetworkCredential(Email, Password);
                        client.UseDefaultCredentials = false;
                        client.Credentials = credentials;
                        //Send the msgs  
                        client.Send(msgs);
                        #endregion

                        #region Send Email to Admin
                        msgs = new MailMessage();
                        msgs.To.Add(Email);
                        address = new MailAddress(model.Email);
                        msgs.From = address;
                        msgs.Subject = "New customer Register";
                        //   msgs.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        htmlBody = msgs.Subject = "There is one new customer " + model.FirstName + " " + model.LastName + " register on " + DateTime.Now + "";
                        msgs.Body = htmlBody;
                        msgs.IsBodyHtml = true;
                        client = new SmtpClient();
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;

                        client.EnableSsl = false;

                        //client.Host = "smtp.gmail.com";
                        //client.Port = 587;
                        client.Host = Host;
                        client.Port = Convert.ToInt32(Port);

                        // client.Credentials = new System.Net.NetworkCredential("email@gmail.com", "pass@");
                        credentials = new NetworkCredential(Email, Password);
                        client.UseDefaultCredentials = false;
                        client.Credentials = credentials;
                        //Send the msgs  
                        client.Send(msgs);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Register Mail Failed..."+ex.Message.ToString());
                    }

                    Log.Info("Register Mail end...");


                }
                return View();
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
            Session.Timeout = 60;
            Session.RemoveAll();
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

        [HttpGet]
        public ActionResult ForGetPassword()
        {
            return View();
        }
        public ActionResult ForGetPassword(CustomerModel model)
        {

            if (string.IsNullOrEmpty(model.Email))
            {
                var data = CustomerCartCRUD.GetPassWordByEmailId(model.Email);

                var userPassword = data[0].Password;
                try
                {

                    #region Send Email to forget Password
                    MailMessage msgs = new MailMessage();
                    msgs.To.Add(model.Email);
                    MailAddress address = new MailAddress(Email);
                    msgs.From = address;
                    msgs.Subject = "Forget Password";
                    //   msgs.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    string htmlBody = msgs.Subject = "Use below password for Login";
                    htmlBody += "Your Password" + userPassword;
                    msgs.Body = htmlBody;
                    msgs.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    client.EnableSsl = false;

                    //client.Host = "smtp.gmail.com";
                    //client.Port = 587;
                    client.Host = Host;
                    client.Port = Convert.ToInt32(Port);

                    // client.Credentials = new System.Net.NetworkCredential("email@gmail.com", "pass@");
                    NetworkCredential credentials = new NetworkCredential(Email, Password);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credentials;
                    //Send the msgs  
                    client.Send(msgs);
                    #endregion
                }
                catch (Exception ex)
                {
                    Log.Error("Forget Mail Failed..." + ex.Message.ToString());
                }
            }
            return View();
        }

        public ActionResult UpdateCustomer(CustomerDomain model)
        {
            var result = CustomerCRUD.UpdateCustomerById(model);
            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}