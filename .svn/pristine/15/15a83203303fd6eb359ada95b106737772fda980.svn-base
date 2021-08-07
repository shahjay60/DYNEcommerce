using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using DataAccessLayer.Admin;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Web.Security;
using DataAccessLayer;
using System.Web.Helpers;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class CategoryImageController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                var data = CategoryImageCRUD.GetCategoryImageAll();
                ViewBag.CategoryImage = data;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
           
        }
        public ActionResult GetCategoryImageList()
        {
            try
            {
                if (Session["Authorized"] != null)
                {
                    var data = CategoryImageCRUD.GetCategoryImageAll();
                    ViewBag.CategoryImage = data;

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
        public ActionResult AddCategoryImage()
        {
            if (Session["Authorized"] != null)
            {
                var Categorist = GRP_MASTERCRUD.GetAllMenu();

                var countriesSelectList = Categorist.Select(a => new SelectListItem
                {
                    Text = a.GRP_NAME,
                    Value = a.GRP_CD.ToString()
                });

                var viewModel = new CategoryImageDomain
                {
                    Categories = new SelectList(countriesSelectList, "Value", "Text")
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult AddCategoryImage(CategoryImageDomain model)
        {
            try
            {
                CategoryImageCRUD.AddToCategoryImage(model);
                ModelState.Clear();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Categorist = CategoryImageCRUD.GetCategoryImage(id);
            CategoryImageDomain mobj = new CategoryImageDomain();
            foreach (var item in Categorist)
            {
                mobj.Id = item.Id;
                mobj.ImageName = item.ImageName;
                mobj.CategoryName = item.CategoryName;
            }
            return View(mobj);
        }

        [HttpPost]
        public ActionResult Edit(CategoryImageDomain model, int? Id, HttpPostedFileBase ImageName)
        {
          
            try
            {

               var data= CategoryImageCRUD.UpdateCategoryImage(model);
                ModelState.Clear();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DelteCategoryImage(int id)
        {
            var result = CategoryImageCRUD.DeleteCategoryImage(id);
            return Json(result);
        }

        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    _imgname = "Category_" + _imgname + _ext;

                    string _comPath = Path.Combine(Server.MapPath("~/CategoryImageUploadfiles"), _imgname);

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

    }
}
