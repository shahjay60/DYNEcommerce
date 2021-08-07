using DataAccessLayer;
using DataAccessLayer.Admin;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string searchString, string attrValue = "")
        {
            try
            {
                ITMMASTDomain mITMMASTDomain = new ITMMASTDomain();

                if (!string.IsNullOrEmpty(searchString))
                {
                    string ProdId = ITMMASTCRUD.GETPRODUCTNAME_ID("").Where(x => x.Name == searchString).Select(x => x.Id).FirstOrDefault();

                    var data = ITMMASTCRUD.GetProductById(ProdId);
                    var productImageData = ProductImageCRUD.GetProductImageAll().Where(x => x.Pid == ProdId).ToList();
                    var productAttribute = ProductAttributeCRUD.GetAttributeForProductDetail(ProdId);
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
                    if (!string.IsNullOrEmpty(attrValue))
                    {
                        string price = ProductAttributeCRUD.GetProduct_AttributeAll()
                                                           .Where(x => x.AttributeValue.Trim().Contains(attrValue.Trim()))
                                                           .Select(x => x.OfferPrice).FirstOrDefault();
                        mITMMASTDomain.Offer_Price = Convert.ToDouble(price);

                        //int AttributeValueId= ProductAttributeCRUD.GetProductAttribute
                    }
                    else
                    {
                        mITMMASTDomain.Offer_Price = data[0].Offer_Price;
                    }
                    if (!string.IsNullOrEmpty(attrValue))
                    {
                        string price = ProductAttributeCRUD.GetProduct_AttributeAll()
                                                           .Where(x => x.AttributeValue.Trim().Contains(attrValue.Trim()))
                                                           .Select(x => x.Price).FirstOrDefault();
                        mITMMASTDomain.Sale_Price = Convert.ToDouble(price);

                        //int AttributeValueId= ProductAttributeCRUD.GetProductAttribute
                    }
                    else
                    {
                        mITMMASTDomain.Sale_Price = data[0].Sale_Price;
                    }
                    mITMMASTDomain.AttributeName = data[0].AttributeName;
                    mITMMASTDomain.AttributeValue = data[0].AttributeValue;

                    if (Session["QTY"] != null)
                        mITMMASTDomain.Qty = Session["QTY"].ToString();
                    else
                        mITMMASTDomain.Qty = "1";

                    if (string.IsNullOrEmpty(attrValue))
                    {
                        foreach (var item in productImageData)
                        {
                            if (item.Viewon == "Detail")
                            {
                                mITMMASTDomain.ProductImages.Add(item.Image);
                            }

                        }
                    }
                    else
                    {
                        foreach (var item in productImageData.Where(x => x.AttrValue.ToLower().Trim() == attrValue.ToLower().Trim()))
                        {
                            mITMMASTDomain.SelectedAttributeValue = attrValue;
                            if (item.Viewon == "Detail")
                            {
                                mITMMASTDomain.ProductImages.Add(item.Image);
                            }

                        }
                    }
                    mITMMASTDomain.SelectedAttributeValue = attrValue;
                    var relatedProduct = ITMMASTCRUD.GetProductByCatId(data[0].GRP_CD.Trim());

                    ViewBag.relatedProducts = relatedProduct.SkipWhile(a => a.Item_CD != ProdId && a.viewon == "List").ToList();
                    return View(mITMMASTDomain);

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain obj = new ExceptionLogDomain();
                obj.MethodName = "Index";
                obj.ControllerName = "Search";
                obj.ErrorText = ex.Message;
                obj.StackTrace = ex.StackTrace;
                obj.Datetime = DateTime.Now;

                ExceptionLogCRUD.AddToExceptionLog(obj);
                return RedirectToAction("Index", "Error");
            }
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var Products = ITMMASTCRUD.GETPRODUCTNAME_ID(prefix);
            var data = Products.Where(x => x.Name.ToLower().Contains(prefix.ToLower())).ToList();
            var ProductsList = data.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            });
            return Json(ProductsList);
        }
    }
}