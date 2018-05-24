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

        /// <summary>
        /// Hiển thị dữ liệu theo tên
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDataByKey(string key)
        {
            var obj = new FACULTY_BUS();
            var model = obj.GetFacultyList(key);
            return Json(new { data = model });
        }

        /// <summary>
        /// Hiển thị dữ liệu theo mã
        /// </summary>
        /// <param name="codeview"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDataByCodeView(string codeview)
        {
            var bus = new FACULTY_BUS();
            var obj = (FACULTY_OBJ)bus.GetFaculty(codeview)[0];
            return Json(obj);
        }

        /// <summary>
        /// Xóa theo mã
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void Delete(string codeview)
        {
            var bus = new FACULTY_BUS();
            bus.DeleteFaculty(codeview);
        }

        /// <summary>
        /// Sửa theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <param name="newcode"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        [HttpPost]
        public void Edit(string code, string newcode, string name, string desc)
        {
            var obj = new FACULTY_OBJ(newcode, name, desc);
            var bus = new FACULTY_BUS();
            bus.UpdateFaculty(code, obj);
        }

        /// <summary>
        /// Tạo mới
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        [HttpPost]
        public void Create(string code, string name, string desc)
        {
            var obj = new FACULTY_OBJ(code, name, desc);
            var bus = new FACULTY_BUS();
            bus.CreateFaculty(obj);
        }

    }
}