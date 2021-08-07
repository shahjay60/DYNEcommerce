using DataAccessLayer;
using DataAccessLayer.Admin;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var BrandOnHomePage = BrandmasterCRUD.GetBrandMaster().Where(x => x.IsOnHomePage == true);

            Categorywiseproduct mCategorywiseproduct = new Categorywiseproduct();
            mCategorywiseproduct.BrandMasterList = new List<BrandmasterDomain>();
            mCategorywiseproduct.CategoryList = new List<GRP_MASTERDomain>();
            mCategorywiseproduct.ProductList = new List<ITMMASTDomain>();

            mCategorywiseproduct.BrandMasterList.AddRange(BrandOnHomePage.ToList());

            var CategoryWiseProduct = GRP_MASTERCRUD.GetProductByCategory();
            var uniqueCategory = CategoryWiseProduct
                              .GroupBy(o => new { o.GRP_CD, o.GRP_NAME })
                              .Select(o => o.FirstOrDefault());

            mCategorywiseproduct.CategoryList.AddRange(uniqueCategory.ToList());
            foreach (var item in CategoryWiseProduct)
            {
                ITMMASTDomain mITMMASTDomain = new ITMMASTDomain();
                mITMMASTDomain.Product_Image = item.ProductImage;
                mITMMASTDomain.Item_Desc = item.ProductName;
                mITMMASTDomain.Item_CD = item.Pid;
                mITMMASTDomain.GRP_CD = item.GRP_CD;
                mITMMASTDomain.Sale_Price = Convert.ToDouble(item.ProductPrice);

                mCategorywiseproduct.ProductList.Add(mITMMASTDomain);
            }

            ViewBag.companydetail = CompnayDetailsCRUD.GetCompnayDetailsAll();
            ViewBag.NewArrialProducts = ITMMASTCRUD.GetAllProducts().Where(x => x.Isnewarriaval == true).ToList();
            ViewBag.BannerData = BannerImageCRUD.GetBannerImageAll();


            if (Session["idUser"] != null)
            {
                ViewBag.totalItemsInCart = CustomerCartCRUD.GetCartByCustomerId(Convert.ToInt32(Session["idUser"])).Where(x => x.IsPlace == false).Count();
                ViewBag.totalItemsInWishList = CustmorWishlistCRUD.GetWishlistByCustomerId(Convert.ToInt32(Session["idUser"])).Count();
            }
            else
            {
                ViewBag.totalItemsInCart = 0;
                ViewBag.totalItemsInWishList = 0;
            }
            return View(mCategorywiseproduct);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }

}