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

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        // GET: Admin/ProductImage
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                var data = ProductImageCRUD.GetProductImageAll();
                ViewBag.ProductImage = data;
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
                    var data = ProductImageCRUD.GetProductImageAll();
                    ViewBag.ProductImage = data;
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
        public ActionResult AddProductImage()
        {
            var Products = ITMMASTCRUD.GetAllProducts().OrderBy(x => x.Item_Desc).ToList();

            if (Session["Msg"] != null)
            {
                ViewBag.Message = Session["Msg"].ToString();
            }
            var countriesSelectList = Products.Select(a => new SelectListItem
            {
                Text = a.Item_Desc,
                Value = a.Item_CD.ToString()
            });

            var viewModel = new ProductImageDomain
            {
                Products = new SelectList(countriesSelectList, "Value", "Text")
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddProductImage(ProductImageDomain model, int? Id, List<HttpPostedFileBase> postedFiles)
        {
            try
            {
                foreach (var item in model.ImageNames)
                {
                    model.Image = item;
                    ProductImageCRUD.AddToProductImage(model);
                }
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
            var ProductImage = ProductImageCRUD.GetProductImage(id);
            ProductImageDomain mobj = new ProductImageDomain();
            foreach (var item in ProductImage)
            {
                mobj.Id = id;
                mobj.Image = item.Image;
                mobj.ProductName = item.ProductName;
                mobj.Viewon = item.Viewon;
                mobj.Pid = item.Pid;

            }
            return View(mobj);
        }

        [HttpPost]
        public ActionResult Edit(ProductImageDomain model, int? Id, List<HttpPostedFileBase> postedFiles)
        {
            try
            {
                foreach (var item in model.ImageNames)
                {
                    model.Image = item;
                    ProductImageCRUD.UpdateProductImage(model);
                }
                return Json("Success", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult DelteProductImage(int id)
        {
            var result = ProductImageCRUD.DeleteProductImage(id);
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
                    _imgname = "Product_" + _imgname + _ext;

                    string folderPath = Server.MapPath("~/ProductImageUploadfiles/");
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(folderPath);
                    }

                    string _comPath = Path.Combine(Server.MapPath("~/ProductImageUploadfiles"), _imgname);

                    var dir = _comPath;  // folder location
                    var path = dir;

                    // Saving Image in Original Mode
                    pic.SaveAs(folderPath + _imgname);
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadMultipleFile()
        {
            string _imgname = string.Empty;

            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                var fileName = Path.GetFileName(file.FileName);
                var _ext = Path.GetExtension(file.FileName);

                _imgname = Guid.NewGuid().ToString();
                _imgname = "Product_" + _imgname + _ext;
                string _comPath = Path.Combine(Server.MapPath("~/ProductImageUploadfiles"), _imgname);

                file.SaveAs(_comPath);
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }

    }
}