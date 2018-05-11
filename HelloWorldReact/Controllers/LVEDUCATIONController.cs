using HelloWorldReact.Models.LevelEducation;
using IS.Sess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorldReact.Controllers
{
    public class LVEDUCATIONController : Controller
    {
        session ses = new session();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getlist()
        {
            List<LVEDUCATION_OBJ> li = null;
            //Khai báo lấy dữ liệu
            LVEDUCATION_BUS bus = new LVEDUCATION_BUS();
            //Thêm điều kiện lọc theo codeview nếu có nhập
            li = bus.getList();
            bus.CloseConnection();
            //Chỉ số đầu tiên của trang hiện tại (đã trừ -1)
            //Trả về client
            return Json(new
            {
                data = li,//Danh sách
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);

        }
    }
}