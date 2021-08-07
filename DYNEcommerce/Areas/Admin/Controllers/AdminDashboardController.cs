using DataAccessLayer.Admin;
using Domain;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class AdminDashboardController : Controller
    {
        // GET: Admin/AdminDashboard
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                if (Session["userdata"] != null)
                {
                    int total = 0;
                    int sum = 0;
                    DateTime dt = DateTime.Now;
                    var userdata = (AdminLoginDomain)Session["userdata"];
                    ViewBag.UserName = userdata.UserName;
                    var dataProduct = Admin_OrderdataCRUD.Get_OrderDeialsProduct();
                    ViewBag.CurrentMonthOrder = dataProduct.GroupBy(o => o.OrderId)
                                       .Select(o => o.FirstOrDefault())
                                       .Where(x => x.OrderDate.Month == dt.Month)
                                       .ToList()
                                       .Count();

                    ViewBag.TotalOrder = dataProduct.GroupBy(o => o.OrderId)
                                       .Select(o => o.FirstOrDefault()).ToList().Count();
                    foreach (var item in dataProduct)
                    {
                        var res = Convert.ToDecimal(item.OrderAmount);
                        total = total + Convert.ToInt32(res);
                    }
                    ViewBag.TotalAmount = total;


                    return View();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }
    }
}