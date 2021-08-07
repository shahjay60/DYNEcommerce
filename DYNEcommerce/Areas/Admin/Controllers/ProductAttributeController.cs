using DataAccessLayer;
using DataAccessLayer.Admin;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
                return RedirectToAction("Index", "AdminLogin");
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
                    return RedirectToAction("Index", "AdminLogin");
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

            var Atttibutes = AttributeCRUD.GetAttributeAll();
            var AttributeSelectList = Atttibutes.Select(a => new SelectListItem
            {
                Text = a.AttributeName,
                Value = a.AttributeId.ToString()
            });

            var viewModel = new Product_Attribute
            {
                Products = countriesSelectList.Select(x => new SelectListItem { Value = x.Value, Text = x.Text }),
                Attributes = AttributeSelectList.Select(x => new SelectListItem { Value = x.Value, Text = x.Text })
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddProductAttribute(Product_Attribute[] membersDetails, int? PaId)
        {
            try
            {
                var data = ProductAttributeCRUD.GetProduct_AttributeAll();

                foreach (var item in membersDetails)
                {
                    int AttributeValueId = Convert.ToInt32(item.AttributeValue);
                    int id = data.Where(x => x.AttributeValueId == AttributeValueId && x.ITEM_CD == item.ITEM_CD)
                                 .Select(x => x.PaId)
                                 .FirstOrDefault();
                    if (id == 0)
                        ProductAttributeCRUD.AddProductAttribute(item);
                }
                ModelState.Clear();
                return Json(true);
            }
            catch (Exception ex)
            {

                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "ProductAttribute";
                mobj.MethodName = "Add Post";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }
        }
        [HttpGet]
        public ActionResult Edit(int PaId)
        {
            try
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


                var AtttibutestVaulues = AttributeValuesCRUD.GetAttValyByAttId(Categorist[0].AttributeId);
                var countriesSelectList2 = AtttibutestVaulues.Select(a => new SelectListItem
                {
                    Text = a.AttributeValue,
                    Value = a.Id.ToString()
                });

                ViewBag.Products = countriesSelectList.Select(x => new SelectListItem { Value = x.Value, Text = x.Text });
                ViewBag.Attributes = countriesSelectList1.Select(x => new SelectListItem { Value = x.Value, Text = x.Text });
                ViewBag.AttributeValues = countriesSelectList2.Select(x => new SelectListItem { Value = x.Value, Text = x.Text });
                foreach (var item in Categorist)
                {
                    mobj.PaId = item.PaId;
                    mobj.ITEM_CD = item.ITEM_CD;
                    mobj.AttributeId = item.AttributeId;
                    mobj.AttributeValue = item.AttributeValue;
                    mobj.Price = item.Price;
                    mobj.OfferPrice = item.OfferPrice;
                }
                return View(mobj);
            }
            catch (Exception ex)
            {

                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "ProductAttribute";
                mobj.MethodName = "Edit Get";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }
        }

        [HttpPost]
        public ActionResult Edit(Product_Attribute[] membersDetails, int? PaId)
        {
            try
            {
                var data = ProductAttributeCRUD.GetProduct_AttributeAll();

                foreach (var item in membersDetails)
                {
                    int AttributeValueId = Convert.ToInt32(item.AttributeValue);
                    int id = data.Where(x => x.AttributeValueId == AttributeValueId && x.ITEM_CD == item.ITEM_CD)
                                 .Select(x => x.PaId)
                                 .FirstOrDefault();
                    if (id == 0)
                    {
                        var data1 = ProductAttributeCRUD.UpdateProduct_Attribute(item);
                    }
                }
                ModelState.Clear();
                return Json(true);
            }
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "ProductAttribute";
                mobj.MethodName = "Add Post";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }
        }

        public JsonResult DelteProductAttribute(int PaId)
        {
            var result = ProductAttributeCRUD.DeleteProduct_Attribute(PaId);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetAttValyByAttId(string AttId)
        {
            int statId;
            List<SelectListItem> districtNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(AttId))
            {
                statId = Convert.ToInt32(AttId);
                var attVAlue = AttributeValuesCRUD.GetAttValyByAttId(statId);
                attVAlue.ForEach(x =>
                {
                    districtNames.Add(new SelectListItem { Text = x.AttributeValue, Value = x.Id.ToString() });
                });
            }
            return Json(districtNames, JsonRequestBehavior.AllowGet);
        }
    }
}