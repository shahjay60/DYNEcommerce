using DataAccessLayer.Admin;
using Domain;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

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
            bool IsValidUser = false;
            Session["userdata"] = userdata;

            if (userdata == null)
            {
                IsValidUser = true;
                string Message = "Invalid email or password";
                return Json(Message, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (model.RememberMe)
                {
                    if (IsValidUser)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "AdminDashboard");
                    }
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
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}