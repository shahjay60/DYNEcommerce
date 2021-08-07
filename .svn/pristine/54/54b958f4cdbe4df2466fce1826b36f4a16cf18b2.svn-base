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
    public class AttributeController : Controller
    {
        // GET: Admin/Attribute
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                var data = AttributeCRUD.GetAttributeAll();
                ViewBag.Attribute = data;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult GetAttributeList()
        {
            try
            {
                if (Session["Authorized"] != null)
                {
                    var data = AttributeCRUD.GetAttributeAll();
                    ViewBag.Attribute = data;

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
        public ActionResult AddAttribute()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddAttribute(AttributeDomain model, int? Id)
        {

            try
            {

                AttributeCRUD.AddAttribute(model);
                ModelState.Clear();
                return RedirectToAction("Index", "Attribute");
            }
            catch (Exception ex)
            {
                return RedirectToAction("AddAttribute", "Attribute");
            }



        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Categorist = AttributeCRUD.GetAttribute(id);
            AttributeDomain mobj = new AttributeDomain();
            foreach (var item in Categorist)
            {
                mobj.AttributeId = item.AttributeId;
                mobj.AttributeName = item.AttributeName;

            }
            return View(mobj);
        }



        [HttpPost]
        public ActionResult Edit(AttributeDomain model, int? Id)
        {

            try
            {

                var data = AttributeCRUD.UpdateAttribute(model);
                ModelState.Clear();
                return RedirectToAction("Index", "Attribute");

            }
            catch (Exception ex)
            {
                return RedirectToAction("AddAttribute", "Attribute");
            }
        }

        public JsonResult DelteAttribute(int AttributeId)
        {
            var result = AttributeCRUD.DeleteAttribute(AttributeId);
            return Json(result);
        }
    }
}