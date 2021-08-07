using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}