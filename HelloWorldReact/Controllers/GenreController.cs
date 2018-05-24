using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloWorldReact.Models;
using HelloWorldReact.Models.GENRE;

namespace HelloWorldReact.Controllers
{
    public class GenreController : Controller
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
        public JsonResult GetDataByKey(string key, string facultycode)
        {
            var bus = new GENRE_BUS();
            var model = bus.GetGenreList(key, facultycode);
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
            var bus = new GENRE_BUS();
            var obj = (GENRE_OBJ)bus.GetGenre(codeview)[0];
            return Json(obj);
        }

        /// <summary>
        /// Xóa theo mã
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void Delete(string codeview)
        {
            var bus = new GENRE_BUS();
            bus.DeleteGenre(codeview);
        }

        /// <summary>
        /// Sửa theo mã
        /// </summary>
        /// <param name="code"></param>
        /// <param name="newcode"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        [HttpPost]
        public void Edit(string code, string newcode, string name, string desc, string facultycode)
        {
            var obj = new GENRE_OBJ(code, name, desc, facultycode);
            var bus = new GENRE_BUS();
            bus.UpdateGenre(code, obj);
        }

        /// <summary>
        /// Tạo mới
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        [HttpPost]
        public void Create(string code, string name, string desc, string facultycode)
        {
            var obj = new GENRE_OBJ(code, name, desc, facultycode);
            var bus = new GENRE_BUS();
            bus.CreateGenre(obj);
        }

    }
}