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

        [HttpPost]
        public JsonResult GetDataByKey(string key)
        {
            var obj = new FACULTY_BUS();
            var model = obj.GetFacultyList(key);
            return Json(new { data = model });
        }

        [HttpPost]
        public JsonResult GetDataByCodeView(string codeview)
        {
            var bus = new FACULTY_BUS();
            var obj = (FACULTY_OBJ)bus.GetFaculty(codeview)[0];
            return Json(obj);
        }

        [HttpPost]
        public void Delete(string id)
        {
            var bus = new FACULTY_BUS();
            bus.DeleteFaculty(id);
        }

        [HttpPost]
        public void Edit(string code, string newcode, string name, string desc)
        {
            var obj = new FACULTY_OBJ(code, name, desc);
            var bus = new FACULTY_BUS();
            bus.UpdateFaculty(code, obj);
        }

        [HttpPost]
        public void Create(string code, string name, string desc)
        {
            var obj = new FACULTY_OBJ(code, name, desc);
            var bus = new FACULTY_BUS();
            bus.CreateFaculty(obj);
        }

    }
}