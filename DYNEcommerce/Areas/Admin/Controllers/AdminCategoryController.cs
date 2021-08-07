using DataAccessLayer;
using DataAccessLayer.Admin;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class AdminCategoryController : Controller
    {
        // GET: Admin/AdminCategory
        public ActionResult Index()
        {
            List<OrderVM> allOrder = new List<OrderVM>();
            if (Session["Authorized"] != null)
            {
                var categoryList = GRP_MASTERCRUD.GetCategories().Skip(1);
                foreach (var item in categoryList)
                {

                    var subCate = categoryList.Where(x => x.FOR_GRP_CD == item.GRP_CD && x.GROUP_YN == "Y").ToList();
                    allOrder.Add(new OrderVM { order = item, orderDetails = subCate });

                }
                return View(categoryList.ToList());
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            AdminCategory model = new AdminCategory();
            var brandSelectList = Admin_BrandCRUD.GetBrandMasterAll("SelectAllBrand").Select(a => new SelectListItem
            {
                Text = a.BrandName,
                Value = a.BrandId.ToString()
            });
            var Categorist = GRP_MASTERCRUD.GetAllCategory();
            var countriesSelectList = Categorist.Select(a => new SelectListItem
            {
                Text = a.GRP_NAME,
                Value = a.GRP_CD.ToString()
            });
            model.CategoryList = new SelectList(countriesSelectList, "Value", "Text");
            model.Brands = new SelectList(brandSelectList, "Value", "Text");
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(AdminCategory model)
        {
            try
            {
                int last2digits = 0;
                var lastCategoryId = GRP_MASTERCRUD.GetLastCategoryID();
                if (!string.IsNullOrEmpty(lastCategoryId))
                {
                    string idwithoutspace = lastCategoryId.Trim();
                    string output = idwithoutspace.ToString().PadLeft(6, '0');
                    last2digits = Convert.ToInt32(idwithoutspace.Substring(idwithoutspace.Length - 2));
                    if (last2digits == 99)
                    {
                        model.GRP_CD = "000" + (last2digits + 1);
                    }
                    else
                    {
                        last2digits = Convert.ToInt32(idwithoutspace.Substring(idwithoutspace.Length - 3));
                        model.GRP_CD = "00" + (last2digits + 1);
                    }
                }
                model.LEVEL_TEXT = model.FOR_GRP_CD + "" + model.GRP_CD;
                var allCategory = GRP_MASTERCRUD.GetAllCategory();
                var exists = allCategory.Where(x => x.GRP_NAME.ToLower().Trim() == model.GRP_NAME.ToLower().Trim()).Select(x => x.GRP_CD).FirstOrDefault();

                GRP_MASTERCRUD.AddToCategoryMaster(model);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Edit(string catId)
        {
            AdminCategory model = new AdminCategory();
            var brandSelectList = Admin_BrandCRUD.GetBrandMasterAll("SelectAllBrand").Select(a => new SelectListItem
            {
                Text = a.BrandName,
                Value = a.BrandId.ToString()
            });
            var Categorist = GRP_MASTERCRUD.GetAllCategory();
            var countriesSelectList = Categorist.Select(a => new SelectListItem
            {
                Text = a.GRP_NAME,
                Value = a.GRP_CD.ToString().Trim()
            });

            try
            {
                var data = GRP_MASTERCRUD.GetCategoryById(catId.Trim());
                foreach (var item in data)
                {
                    model.BrandId = item.BrandId;
                    model.FOR_GRP_CD = item.FOR_GRP_CD.Trim();
                    model.GROUP_YN = item.GROUP_YN;
                    model.GRP_NAME = item.GRP_NAME;
                    model.GRP_SNAME = item.GRP_SNAME;
                    model.Isonhomepage = item.Isonhomepage;
                    model.Isonmenu = item.Isonmenu;
                    model.GRP_CD = item.GRP_CD.Trim();
                }
            }
            catch (Exception ex)
            {

            }

            model.CategoryList = new SelectList(countriesSelectList, "Value", "Text");
            model.Brands = new SelectList(brandSelectList, "Value", "Text");
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AdminCategory model)
        {
            try
            {
                model.LEVEL_TEXT = model.FOR_GRP_CD + "" + model.GRP_CD;
                var allCategory = GRP_MASTERCRUD.GetAllCategory();
                var exists = allCategory.Where(x => x.GRP_NAME.ToLower().Trim() == model.GRP_NAME.ToLower().Trim()).Select(x => x.GRP_CD).FirstOrDefault();
                if (string.IsNullOrEmpty(exists))
                {
                    var result = GRP_MASTERCRUD.UpdateCategoryMaster(model);
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(string catId)
        {
            try
            {
                var result = GRP_MASTERCRUD.Delete(catId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}