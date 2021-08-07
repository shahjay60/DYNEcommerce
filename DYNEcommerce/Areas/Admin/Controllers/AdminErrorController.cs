using Domain;
using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class AdminErrorController : Controller
    {
        // GET: Admin/AdminError
        public ActionResult Index()
        {
            ExceptionLogDomain mobj = (ExceptionLogDomain)TempData["ExceptionLogDomain"];
            return View(mobj);
        }
    }
}