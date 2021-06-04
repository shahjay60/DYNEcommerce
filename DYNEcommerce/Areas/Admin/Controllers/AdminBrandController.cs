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
    public class AdminBrandController : Controller
    {

        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                var data = Admin_BrandCRUD.GetBrandMasterAll("SelectAllBrand");
                ViewBag.Brand = data;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult GetBrandList()
        {
            try
            {
                if (Session["Authorized"] != null)
                {
                    var data = Admin_BrandCRUD.GetBrandMasterAll("SelectAllBrand");
                    ViewBag.Brand = data;
                    return View("Index", data);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
                return Content("Error");
            }
        }
        [HttpGet]
        public ActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBrand(BrandmasterDomain model, int? BrandId)
        {
            try
            {
                Admin_BrandCRUD.AddToBrandMaster(model);
                return Json("Success", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int id)
        {
            var data = Admin_BrandCRUD.GetBrandMaster(id);
            BrandmasterDomain mBrandmasterDomain = new BrandmasterDomain();
            mBrandmasterDomain.BrandId = data[0].BrandId;
            mBrandmasterDomain.BrandName = data[0].BrandName;
            mBrandmasterDomain.IsOnHomePage = data[0].IsOnHomePage;
            mBrandmasterDomain.ISOnWeb = data[0].ISOnWeb;
            return View(mBrandmasterDomain);
        }

        [HttpPost]
        public ActionResult Edit(BrandmasterDomain model, int? BrandId)
        {
            try
            {
                Admin_BrandCRUD.UpdateBrandmaster(model);
                return Json("Success", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteBrandmasterById(int id)
        {
            var result = Admin_BrandCRUD.DeleteBrandmaster(id);
            return Json(result);
        }

    }
}