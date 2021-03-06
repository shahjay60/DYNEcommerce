using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int brandId = 0)
        {
            ViewBag.BrandId = brandId;
            ViewBag.Brands = BrandmasterCRUD.GetBrandMaster();
            //var categories = GRP_MASTERCRUD.GetCategoryByBrandId(brandId);
            return View();
        }


        public ActionResult GetCategoryList(string brandId, int? page, string record1, string ShortBy, string[] checkedValues, string SearchString)
        {
            try
            {
                int pageSize = 0;
                ViewBag.path = Path.Combine(Server.MapPath(@"~/CategoryImageUploadfiles"));

                List<GRP_MASTERDomain> categories = new List<GRP_MASTERDomain>();

                if (string.IsNullOrEmpty(record1))
                {
                    pageSize = Convert.ToInt32(record1);
                }
                else
                {
                    pageSize = Convert.ToInt32(record1);
                }
                if (brandId == "0" || brandId == null)
                {
                    categories = GRP_MASTERCRUD.GetAllCategory().Skip(1).ToList();
                }


                if (checkedValues == null)
                {
                    if (brandId != "0" || brandId == null)
                    {
                        categories = GRP_MASTERCRUD.GetCategoryByBrandId(Convert.ToInt32(brandId));
                        ViewBag.RecordPerPage = pageSize;
                        ViewBag.TotalRecord = categories.Count();
                    }
                }

                else
                {
                    categories = GRP_MASTERCRUD.GetAllCategory().Where(x => checkedValues.Any(a => a.ToString() == x.BrandId)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchString))
                {
                    categories = GRP_MASTERCRUD.GetAllCategory().Where(x => x.GRP_NAME.ToLower().Trim().Contains(SearchString.ToLower().Trim())).ToList();
                }
                var pager = new Pager(categories.Count(), page, pageSize);
                pager.catId = brandId;

                var viewModel = new IndexViewModel
                {
                    Categories = categories.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                    Pager = pager
                };
                return PartialView("_categoryListing", viewModel);

            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "GetProductList";
                obj.ControllerName = "Product";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }

    }
}