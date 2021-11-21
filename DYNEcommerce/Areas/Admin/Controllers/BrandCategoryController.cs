using DataAccessLayer;
using DataAccessLayer.Admin;
using Domain;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class BrandCategoryController : Controller
    {
        // GET: Admin/BrandCategory
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                var data = Admin_BrandCRUD.GetBrandMasterAll("SelctBrandWiseCategory");
                ViewBag.Brand = data;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }

        public ActionResult Add(string res = "")
        {
            BrandCategoryDomain mBrandCategoryDomain = new BrandCategoryDomain();

            var countriesSelectList = GRP_MASTERCRUD.GetAllMenu().Select(a => new SelectListItem
            {
                Text = a.GRP_NAME,
                Value = a.GRP_CD.ToString()
            });
            var brandSelectList = Admin_BrandCRUD.GetBrandMasterAll("SelectAllBrand").Select(a => new SelectListItem
            {
                Text = a.BrandName,
                Value = a.BrandId.ToString()
            });
            var viewModel = new BrandCategoryDomain
            {
                Categories = new SelectList(countriesSelectList, "Value", "Text"),
                Brands = new SelectList(brandSelectList, "Value", "Text"),
            };

            if (Session["Message"] != null)
                ViewBag.Message = Session["Message"];
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(BrandCategoryDomain model, int? BrandId)
        {
            try
            {
                var data = Admin_BrandCRUD.GetBrandMasterAll("SelctBrandWiseCategory");
                var chkexists = data.Where(x => x.GRP_CD == model.GRP_CD && x.BrandId == model.brandId).FirstOrDefault();
                //if (chkexists != null)
                //{
                    var result = GRP_MASTERCRUD.UpdateCategoryByBrandId(model);
                    Session["Message"] = "Success";
                   return RedirectToAction("Add", "BrandCategory", "Success");
                //}
                //else
                //{
                //    Session["Message"] = "Error";
                //    return RedirectToAction("Add", "BrandCategory", "Error");
                //}

            }
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "AttributeValues";
                mobj.MethodName = "Edit Post";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }
        }

        public ActionResult Edit(string id)
        {

            var brandSelectList = Admin_BrandCRUD.GetBrandMasterAll("SelectAllBrand").Select(a => new SelectListItem
            {
                Text = a.BrandName,
                Value = a.BrandId.ToString()
            });

            var data = Admin_BrandCRUD.GetBrandCategoryByCatId(id);
            BrandmasterDomain mBrandmasterDomain = new BrandmasterDomain();
            mBrandmasterDomain.BrandId = data[0].BrandId;
            mBrandmasterDomain.SelectedBrandId = Convert.ToString(data[0].BrandId);
            mBrandmasterDomain.BrandName = data[0].BrandName;
            mBrandmasterDomain.GRP_CD = data[0].GRP_CD;
            mBrandmasterDomain.CategoryName = data[0].CategoryName;
            mBrandmasterDomain.Brands = new SelectList(brandSelectList, "Value", "Text");
            if (Session["Message1"] != null)
                ViewBag.Message = Session["Message1"];
            return View(mBrandmasterDomain);
        }

        [HttpPost]
        public ActionResult Edit(BrandmasterDomain model, int? BrandId)
        {
            try
            {
                BrandCategoryDomain mBrandCategoryDomain = new BrandCategoryDomain();
                mBrandCategoryDomain.brandId = model.BrandId;
                mBrandCategoryDomain.GRP_CD = model.GRP_CD;
                var data = Admin_BrandCRUD.GetBrandMasterAll("SelctBrandWiseCategory");
                //var chkexists = data.Where(x => x.GRP_CD == model.GRP_CD.Trim() && x.BrandId == model.BrandId).FirstOrDefault();
                //if (chkexists != null)
                //{
                    var result = GRP_MASTERCRUD.UpdateCategoryByBrandId(mBrandCategoryDomain);
                Session["Message1"] = "Success";
                    return RedirectToAction("Edit", "BrandCategory", "Success");
                //}
                //else
                //{
                //    Session["Message1"] = "Error";
                //    return RedirectToAction("Edit", "BrandCategory", "Error");
                //}

            }
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "AttributeValues";
                mobj.MethodName = "Edit Post";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }
        }

        public ActionResult DeleteBrandCategorymasterById(string grp_cd)
        {
            var res = GRP_MASTERCRUD.DeleteBrandCategorymasterById(grp_cd);
            return Json(res);
        }

    }
}