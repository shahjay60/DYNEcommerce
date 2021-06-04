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
    public class ProductAttributeController : Controller
    {
        // GET: Admin/ProductAttribute
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                var data = ProductAttributeCRUD.GetProduct_AttributeAll();
                ViewBag.ProductAttribute = data;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult GetProductAttributeList()
        {
            try
            {
                if (Session["Authorized"] != null)
                {
                    var data = ProductAttributeCRUD.GetProduct_AttributeAll();
                    ViewBag.ProductAttribute = data;

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
        public ActionResult AddProductAttribute()
        {
            var Products = ITMMASTCRUD.GetAllProducts().OrderBy(x => x.Item_Desc).ToList();

            var countriesSelectList = Products.Select(a => new SelectListItem
            {
                Text = a.Item_Desc,
                Value = a.Item_CD.ToString()
            });

            var Atttibutest = AttributeCRUD.GetAttributeAll();
            var countriesSelectList1 = Atttibutest.Select(a => new SelectListItem
            {
                Text = a.AttributeName,
                Value = a.AttributeId.ToString()
            });

            var viewModel = new Product_Attribute
            {
                Products = countriesSelectList.Select(x => new SelectListItem { Value = x.Value, Text = x.Text }),
                Attributes = countriesSelectList1.Select(x => new SelectListItem { Value = x.Value, Text = x.Text })
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddProductAttribute(Product_Attribute model, int? PaId)
        {
            try
            {
                ProductAttributeCRUD.AddProductAttribute(model);
                ModelState.Clear();
                return RedirectToAction("Index", "ProductAttribute");
            }
            catch (Exception ex)
            {
                return RedirectToAction("AddProductAttribute", "ProductAttribute");
            }
        }
        [HttpGet]
        public ActionResult Edit(int PaId)
        {
            var Categorist = ProductAttributeCRUD.GetProductAttribute(PaId);
            Product_Attribute mobj = new Product_Attribute();
            var Products = ITMMASTCRUD.GetAllProducts().OrderBy(x => x.Item_Desc).ToList();

            var countriesSelectList = Products.Select(a => new SelectListItem
            {
                Text = a.Item_Desc,
                Value = a.Item_CD.ToString()
            });

            var Atttibutest = AttributeCRUD.GetAttributeAll();
            var countriesSelectList1 = Atttibutest.Select(a => new SelectListItem
            {
                Text = a.AttributeName,
                Value = a.AttributeId.ToString()
            });


            ViewBag.Products = countriesSelectList.Select(x => new SelectListItem { Value = x.Value, Text = x.Text });
            ViewBag.Attributes = countriesSelectList1.Select(x => new SelectListItem { Value = x.Value, Text = x.Text });

            foreach (var item in Categorist)
            {
                mobj.PaId = item.PaId;
                mobj.ITEM_CD = item.ITEM_CD;
                mobj.AttributeId = item.AttributeId;
                //mobj.AttributeValue = item.AttributeValue;
            }
            return View(mobj);
        }

        [HttpPost]
        public ActionResult Edit(Product_Attribute model, int? PaId)
        {

            try
            {

                var data = ProductAttributeCRUD.UpdateProduct_Attribute(model);
                ModelState.Clear();
                return RedirectToAction("Index", "ProductAttribute");

            }
            catch (Exception ex)
            {
                return RedirectToAction("AddProductAttribute", "ProductAttribute");
            }
        }

        public JsonResult DelteProductAttribute(int PaId)
        {
            var result = ProductAttributeCRUD.DeleteProduct_Attribute(PaId);
            return Json(result);
        }
    }
}