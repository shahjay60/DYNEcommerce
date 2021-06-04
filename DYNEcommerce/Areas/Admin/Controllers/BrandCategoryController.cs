using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using DataAccessLayer.Admin;
using DataAccessLayer;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class BrandCategoryController : Controller
    {
        // GET: Admin/BrandCategory
        public ActionResult Index()
        {
            var data = Admin_BrandCRUD.GetBrandMasterAll("SelctBrandWiseCategory");
            ViewBag.Brand = data;
            return View();
        }

        public ActionResult Add()
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
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(BrandCategoryDomain model, int? BrandId)
        {
            try
            {
                var result = GRP_MASTERCRUD.UpdateCategoryByBrandId(model);
                return RedirectToAction("Index", "BrandCategory");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Add", "BrandCategory");
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

                var result = GRP_MASTERCRUD.UpdateCategoryByBrandId(mBrandCategoryDomain);
                return RedirectToAction("Index", "BrandCategory");

            }
            catch (Exception ex)
            {
                return RedirectToAction("AddBrand", "BrandCategory");
            }
        }

    }
}