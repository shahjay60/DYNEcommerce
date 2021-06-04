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
    public class CompnayDetailsController : Controller
    {
        public ActionResult Index()
        {

            if (Session["Authorized"] != null)
            {
                var data = CompnayDetailsCRUD.GetCompnayDetailsAll();
                ViewBag.CompnayDetails = data;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult GetCompnayDetailsList()
        {
            try
            {
                if (Session["Authorized"] != null)
                {
                    var data = CompnayDetailsCRUD.GetCompnayDetailsAll();
                    ViewBag.CompnayDetails = data;

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
        public ActionResult AddCompnayDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCompnayDetails(CompnayDetailsDomain model)
        {
            try
            {
                CompnayDetailsCRUD.AddToCompnayDetails(model);
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }
            return Json("Success", JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var CompanyDetails = CompnayDetailsCRUD.GetCompnayDetails(Id);
            CompnayDetailsDomain mobj = new CompnayDetailsDomain();
            foreach (var item in CompanyDetails)
            {
                mobj.Id = item.Id;
                mobj.Name = item.Name;
                mobj.Address = item.Address;
                mobj.PhoneNumber = item.PhoneNumber;
                mobj.PhoneNumber2 = item.PhoneNumber2;
                mobj.EmailAddress = item.EmailAddress;
                mobj.Logo = item.Logo;
                mobj.FacebookLink = item.FacebookLink;
                mobj.InstagramLink = item.InstagramLink;
                mobj.FaxNo = item.FaxNo;
            }
            return View(mobj);
        }

        [HttpPost]
        public ActionResult Edit(CompnayDetailsDomain model, int? Id, HttpPostedFileBase ImageName)
        {
            try
            {
                var data = CompnayDetailsCRUD.UpdateCompnayDetails(model);
                ModelState.Clear();
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DelteCompnayDetails(int Id)
        {
            var result = CompnayDetailsCRUD.DeleteCompnayDetails(Id);
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
                    _imgname = "LOGO_" + _imgname + _ext;

                    string _comPath = Path.Combine(Server.MapPath("~/LogoUploadfiles"), _imgname);

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }
    }
}