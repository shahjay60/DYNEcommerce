using DataAccessLayer;
using Domain;
using PagedList;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        // GET: Admin/AdminProduct
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Authorized"] != null)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Item_CD" : "";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                try
                {
                    var data = ITMMASTCRUD.GetAllProducts();

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        data = data.Where(s => s.Item_Desc.Contains(searchString)).ToList();
                    }

                    if (!String.IsNullOrEmpty(sortOrder))
                    {
                        data = data.OrderBy(x => x.Item_Desc).ToList();
                    }
                    int pageSize = 10;
                    int pageNumber = (page ?? 1);
                    return View(data.ToPagedList(pageNumber, pageSize));
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }

        public ActionResult Add()
        {
            var Categorist = GRP_MASTERCRUD.GetAllMenu();
            ITMMASTDomain model = new ITMMASTDomain();
            var countriesSelectList = Categorist.Select(a => new SelectListItem
            {
                Text = a.GRP_NAME,
                Value = a.GRP_CD.ToString()
            });
            model.Categories = new SelectList(countriesSelectList, "Value", "Text");

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ITMMASTDomain model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Item_CD))
                {
                    var lastid = ITMMASTCRUD.GetLastProductID();
                    if (string.IsNullOrEmpty(lastid))
                    {
                        model.Item_CD = "Z0000" + 1;
                    }
                    else
                    {
                        var prefix = Regex.Match(lastid, "^\\D+").Value;
                        var number = Regex.Replace(lastid, "^\\D+", "");
                        var i = int.Parse(number) + 1;
                        var newString = prefix + i.ToString(new string('0', number.Length));
                        model.Item_CD = newString;
                    }
                }
                ITMMASTCRUD.AddProduct(model);
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Edit(string pId)
        {
            var Categorist = GRP_MASTERCRUD.GetAllMenu();
            ITMMASTDomain model = new ITMMASTDomain();
            var countriesSelectList = Categorist.Select(a => new SelectListItem
            {
                Text = a.GRP_NAME,
                Value = a.GRP_CD.ToString().Trim()
            });

            var productdetail = ITMMASTCRUD.GetProductById(pId);
            model.Categories = new SelectList(countriesSelectList, "Value", "Text");
            foreach (var item in productdetail)
            {
                model.Bar_Code = item.Bar_Code;
                model.GRP_CD = item.GRP_CD.Trim();
                model.Item_CD = item.Item_CD;
                model.Item_ID = item.Item_ID;
                model.Item_Desc = item.Item_Desc;
                model.Sale_Price = item.Sale_Price;
                model.Offer_Price = item.Offer_Price;
                model.Bar_Code = item.Bar_Code;
                model.Isnewarriaval = item.Isnewarriaval;
                model.DetailDesc = item.DetailDesc;
            }
            return View(model);
        }
        public ActionResult Edit(ITMMASTDomain model)
        {
            try
            {
                var result = ITMMASTCRUD.UpdateProduct(model);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Delete(string pId)
        {
            try
            {
                var result = ITMMASTCRUD.Delete(pId);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}