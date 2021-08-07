using DataAccessLayer.Admin;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        public ActionResult OrderDetails()
        {
            if (Session["Authorized"] != null)
            {
                var dataProduct = Admin_OrderdataCRUD.Get_OrderDeialsProduct();
                dataProduct = dataProduct.GroupBy(o => o.OrderId)
                                   .Select(o => o.FirstOrDefault()).OrderByDescending(x => x.OrderId).ToList();

                ViewBag.OrderDetailsProduct = dataProduct;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }
    }
}