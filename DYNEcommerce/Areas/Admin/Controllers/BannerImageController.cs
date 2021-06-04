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

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class BannerImageController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                var data = BannerImageCRUD.GetBannerImageAll();
                ViewBag.BannerImage = data;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult GetProductImageList()
        {
            try
            {
                if (Session["Authorized"] != null)
                {
                    var data = BannerImageCRUD.GetBannerImageAll();
                    ViewBag.BannerImage = data;

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
        public ActionResult AddBannerImage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddBannerImage(BannerImageDomain model, int? Id, HttpPostedFileBase ImageName)
        {
            try
            {
                model.IsDeleted = false;
                BannerImageCRUD.AddToBannerImage(model);
                ModelState.Clear();
                return Json("Success", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Categorist = BannerImageCRUD.GetBannerImage(Id);
            BannerImageDomain mobj = new BannerImageDomain();
            foreach (var item in Categorist)
            {
                mobj.Id = Id;
                mobj.ImageName = item.ImageName;
                mobj.text1 = item.text1;
                mobj.text2 = item.text2;
                mobj.text3 = item.text3;
                mobj.IsDeleted = item.IsDeleted;
            }
            return View(mobj);
        }


        [HttpPost]
        public ActionResult Edit(BannerImageDomain model, int? Id, HttpPostedFileBase ImageName)
        {
            try
            {

                var data = BannerImageCRUD.UpdateBanerimage(model);
                ModelState.Clear();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteBannerImage(int Id)
        {
            var result = BannerImageCRUD.DeleteBannerImage(Id);
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
                    _imgname = "Banner_" + _imgname + _ext;

                    string _comPath = Path.Combine(Server.MapPath("~/BannerImageUploadfiles"), _imgname);

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