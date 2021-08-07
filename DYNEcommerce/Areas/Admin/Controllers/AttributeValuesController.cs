using DataAccessLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DYNEcommerce.Areas.Admin.Controllers
{
    public class AttributeValuesController : Controller
    {
        // GET: Admin/AttributeValues
        public ActionResult Index()
        {
            if (Session["Authorized"] != null)
            {
                List<AttributevaluesDomain> model = new List<AttributevaluesDomain>();
                model = AttributeValuesCRUD.GetAttributeValueAll();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "AdminLogin");
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            var Atttibutes = AttributeCRUD.GetAttributeAll();
            var AttributeSelectList = Atttibutes.Select(a => new SelectListItem
            {
                Text = a.AttributeName,
                Value = a.AttributeId.ToString()
            });

            AttributevaluesDomain model = new AttributevaluesDomain();
            model.Attributes = AttributeSelectList.Select(x => new SelectListItem { Value = x.Value, Text = x.Text });
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AttributevaluesDomain[] membersDetails)
        {
            try
            {
                var attData = AttributeValuesCRUD.GetAttributeValueAll();

                foreach (var item in membersDetails)
                {
                    int id = attData.Where(x => x.AttributeValue.Trim().ToLower() == item.AttributeValue.Trim().ToLower())
                                    .Select(x => x.Id)
                                    .FirstOrDefault();
                    if (id == 0)
                        AttributeValuesCRUD.AddAttributeValue(item);
                }
                ModelState.Clear();
                return Json(true);
            }
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "AttributeValues";
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
            var Atttibutes = AttributeCRUD.GetAttributeAll();
            var AttributeSelectList = Atttibutes.Select(a => new SelectListItem
            {
                Text = a.AttributeName,
                Value = a.AttributeId.ToString()
            });

            AttributevaluesDomain model = new AttributevaluesDomain();
            model.Attributes = AttributeSelectList.Select(x => new SelectListItem { Value = x.Value, Text = x.Text });
            var arrivaluedata = AttributeValuesCRUD.GetAttributeValue(id);

            model.AttributeId = arrivaluedata[0].AttributeId;
            model.AttributeValue = arrivaluedata[0].AttributeValue;
            model.Id = id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AttributevaluesDomain model)
        {
            try
            {
                var attData = AttributeValuesCRUD.GetAttributeValueAll();

                int id = attData.Where(x => x.AttributeValue.Trim().ToLower() == model.AttributeValue.Trim().ToLower())
                                    .Select(x => x.Id)
                                    .FirstOrDefault();
                if (id == 0)
                {
                    var res = AttributeValuesCRUD.UpdateAttributeValue(model);
                }

                ViewBag.Success = "Success";
                return RedirectToAction("Edit", "AttributeValues", model.Id);


            }
            catch (Exception ex)
            {
                ExceptionLogDomain mobj = new ExceptionLogDomain();
                mobj.ControllerName = "AttributeValues";
                mobj.MethodName = "Edit Post";
                mobj.ErrorText = ex.Message;
                mobj.StackTrace = ex.StackTrace;
                TempData["ExceptionLogDomain"] = mobj;

                return RedirectToAction("Index", "AdminError");

            }
        }
        public ActionResult Delete(int Id)
        {
            var result = AttributeValuesCRUD.DeleteAttributeValue(Id);
            return Json(result);
        }
    }
}