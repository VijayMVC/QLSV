using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorldReact.Models;
using HelloWorldReact.Models.FACULTY;

namespace HelloWorldReact.Controllers
{
    public class FacultyController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetData()
        {
            var obj = new FACULTY_BUS();
            var model = obj.GetFacultyList("");
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDataByKey(string key)
        {
            var obj = new FACULTY_BUS();
            var model = obj.GetFacultyList(key);
            return Json(new { data = model });
        }

        public ActionResult Delete(string id)
        {
            var obj = new FACULTY_BUS();
            var model = (FACULTY_OBJ)obj.GetFaculty(id)[0];
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(string id)
        {
            var obj = new FACULTY_BUS();
            obj.DeleteFaculty(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var obj = new FACULTY_BUS();
            var model = (FACULTY_OBJ)obj.GetFaculty(id)[0];
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Update(string id, FACULTY_OBJ obj)
        {
            var obj1 = new FACULTY_BUS();
            obj1.UpdateFaculty(id, obj);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirm(FACULTY_OBJ obj)
        {
            var obj1 = new FACULTY_BUS();
            obj1.CreateFaculty(obj);
            return RedirectToAction("Index");
        }

    }
}