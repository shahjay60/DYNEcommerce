using DataAccessLayer;
using DataAccessLayer.Admin;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class ProductController : Controller
    {
        protected static int TotalDataCount;
        // GET: Product
        public ActionResult Index(string catId, int? page)
        {
            ViewBag.CatId = catId;
            ViewBag.Brands = BrandmasterCRUD.GetBrandByCatId(catId);
            ViewBag.Attributes = AttributeCRUD.GetAttributesData();
            return View();
            //return PartialView("_ProductListing", viewModel);
        }
        public ActionResult GetProductList(string catId, int? page, string record1, string ShortBy, string[] checkedValues, string[] checkedcolorValues, string[] checkedsizeValues)
        {
            try
            {
                int pageSize = 0;
                if (string.IsNullOrEmpty(record1))
                {
                    pageSize = Convert.ToInt32(record1);
                }
                else
                {
                    pageSize = Convert.ToInt32(record1);
                }
                var Products = ITMMASTCRUD.GetProductByCatId(catId.Trim());

                ViewBag.RecordPerPage = pageSize;
                ViewBag.TotalRecord = Products.Count();

                if (ShortBy == "Low to High")
                {
                    Products = Products.OrderBy(x => x.Sale_Price).ToList();
                }

                else if (checkedValues != null)
                {
                    Products = Products.Where(x => checkedValues.Any(a => a.ToString() == x.BrandId)).ToList();
                }
                else if (checkedcolorValues != null)
                {
                    Products = Products.Where(x => checkedcolorValues.Any(a => a.ToString() == x.AttributeValue)).ToList();
                }
                else if (checkedsizeValues != null)
                {
                    Products = Products.Where(x => checkedsizeValues.Any(a => a.ToString() == x.AttributeValue)).ToList();
                }
                Products = Products.Where(x => x.viewon == "List" || x.viewon == string.Empty).ToList();
                Products = Products.GroupBy(o => o.Item_ID)
                                   .Select(o => o.FirstOrDefault()).ToList();

                var pager = new Pager(Products.Count(), page, pageSize);
                pager.catId = catId;

                var viewModel = new IndexViewModel
                {
                    Items = Products.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize).ToList(),
                    Pager = pager
                };
                return PartialView("_ProductListing", viewModel);

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
        public ActionResult ProductDetail(string ProdId, string catId)
        {
            try
            {
                string url2 = Request.Url.AbsoluteUri;
                Session["LastURL"] = url2;

                var data = ITMMASTCRUD.GetProductById(ProdId);

                var productImageData = ProductImageCRUD.GetProductImageAll().Where(x => x.Pid == ProdId).ToList();
                var productAttribute = ProductAttributeCRUD.GetProduct_AttributeAll().Where(x => x.ITEM_CD == ProdId).ToList();

                ITMMASTDomain mITMMASTDomain = new ITMMASTDomain();
                mITMMASTDomain.ProductImages = new List<string>();
                mITMMASTDomain.ProductAttributes = new List<Product_Attribute>();

                foreach (var item in productAttribute)
                {
                    Product_Attribute mProduct_Attribute = new Product_Attribute();
                    mProduct_Attribute.AttributeName = item.AttributeName;
                    mProduct_Attribute.AttributeValue = item.AttributeValue;
                    mProduct_Attribute.AttributeId = item.AttributeId;
                    mITMMASTDomain.ProductAttributes.Add(mProduct_Attribute);
                }
                mITMMASTDomain.DetailDesc = data[0].DetailDesc;
                mITMMASTDomain.GRP_CD = data[0].GRP_CD;
                mITMMASTDomain.Item_CD = data[0].Item_CD;
                mITMMASTDomain.Item_Desc = data[0].Item_Desc;
                mITMMASTDomain.Offer_Price = data[0].Offer_Price;
                mITMMASTDomain.Sale_Price = data[0].Sale_Price;
                mITMMASTDomain.AttributeName = data[0].AttributeName;
                mITMMASTDomain.AttributeValue = data[0].AttributeValue;

                if (Session["QTY"] != null)
                    mITMMASTDomain.Qty = Session["QTY"].ToString();
                else
                    mITMMASTDomain.Qty = "1";

                foreach (var item in productImageData)
                {
                    if (item.Viewon == "Detail")
                    {
                        mITMMASTDomain.ProductImages.Add(item.Image);
                    }

                }
                var relatedProduct = ITMMASTCRUD.GetProductByCatId(catId.Trim());

                ViewBag.relatedProducts = relatedProduct.SkipWhile(a => a.Item_CD != ProdId).ToList();

                return View(mITMMASTDomain);
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
        public ActionResult AddToCart(string prodId, string price, string qty, string[] checkedcolorValues, string[] checkedsizeValues)
        {


            try
            {
                var myList = new List<string>();

                if (checkedcolorValues != null)
                    myList.AddRange(checkedcolorValues);
                if (checkedsizeValues != null)
                    myList.AddRange(checkedsizeValues);
                if (Session["idUser"] != null)
                {
                    Session["QTY"] = qty;
                    CustomerCartDomain mcustomerCart = new CustomerCartDomain();

                    mcustomerCart.Amount = Convert.ToDecimal(price) * Convert.ToInt32(qty);
                    mcustomerCart.CreatedDatetime = DateTime.Now;
                    mcustomerCart.CustomerId = Session["idUser"].ToString();
                    mcustomerCart.IsDeleted = "False";
                    mcustomerCart.Quantity = Convert.ToInt32(qty);
                    mcustomerCart.ProductId = prodId;
                    mcustomerCart.IsPlace = false;
                    int LastAddedCartID = CustomerCartCRUD.AddToCart(mcustomerCart);

                    foreach (var item in myList)
                    {
                        CartAttribute mCartAttribute = new CartAttribute();
                        mCartAttribute.cartId = LastAddedCartID;
                        mCartAttribute.AttributeValueID = item;
                        CustomerCartCRUD.AddToCartAttribute(mCartAttribute);
                    }

                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "AddToCart";
                obj.ControllerName = "Product";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }
        public ActionResult AddToWishList(string prodId, string price)
        {
            try
            {
                if (Session["idUser"] != null)
                {
                    CustomerWishlistDomain mCustomerWishlist = new CustomerWishlistDomain();

                    mCustomerWishlist.Amount = Convert.ToDecimal(price);
                    mCustomerWishlist.CreatedDateTime = DateTime.Now;
                    mCustomerWishlist.CustomerId = Convert.ToInt32(Session["idUser"]);
                    mCustomerWishlist.ProductId = prodId;

                    CustmorWishlistCRUD.AddToWishlist(mCustomerWishlist);

                    return Json(true);
                }
                else
                {
                    return RedirectToAction("Login", "Customer");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "AddToWishList";
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