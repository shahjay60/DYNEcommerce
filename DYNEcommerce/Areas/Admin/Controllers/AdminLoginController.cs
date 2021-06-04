using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

using DataAccessLayer.Admin;
using DataAccessLayer;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AdminLoginDomain model)
        {
            var userdata = AdminLoginCRUD.GetAdminLoginAll().Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
            if (userdata == null)
            {
                string Message = "Invalid email or password";
                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (model.RememberMe)
                {

                }
                else
                {
                }
                Session["Authorized"] = "True";
                return Json("Success", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}