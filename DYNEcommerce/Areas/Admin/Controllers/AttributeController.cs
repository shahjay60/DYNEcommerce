using DataAccessLayer;
using Domain;
using System;
using System.Linq;
using System.Web.Mvc;

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

                return RedirectToAction("Index", "AdminLogin");
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
        public ActionResult AddAttribute()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAttribute(string attributeName)
        {
            try
            {
                var data = AttributeCRUD.GetAttributeAll()
                    .Where(x => x.AttributeName.ToLower().Trim() == attributeName.ToLower().Trim())
                    .Select(x => x.AttributeId).FirstOrDefault();

                if (data == 0)
                {
                    AttributeCRUD.AddAttribute(attributeName);
                    ModelState.Clear();
                    return Json("Success", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "Attribute";
                mobj.MethodName = "Add Post";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
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
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "Attribute";
                mobj.MethodName = "Edit Get";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }
        }

        [HttpPost]
        public ActionResult Edit(AttributeDomain model, int? Id)
        {
            try
            {
                var data = AttributeCRUD.GetAttributeAll()
                   .Where(x => x.AttributeName.ToLower().Trim() == model.AttributeName.ToLower().Trim())
                   .Select(x => x.AttributeId).FirstOrDefault();

                if (data == 0)
                {
                    var data1 = AttributeCRUD.UpdateAttribute(model);
                    ModelState.Clear();
                    return Json("Success", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "Attribute";
                mobj.MethodName = "Edit Post";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;

                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");
            }
        }

        public JsonResult DelteAttribute(int AttributeId)
        {
            var result = AttributeCRUD.DeleteAttribute(AttributeId);
            return Json(result);
        }
    }
}