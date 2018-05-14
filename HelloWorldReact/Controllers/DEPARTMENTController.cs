using HelloWorldReact.Models.DEPARTMENT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorldReact.Controllers
{
    public class DEPARTMENTController : Controller
    {
        public JsonResult getlistbylvCode(string lveducationCode)
        {
            List<DEPARTMENT_OBJ> li = null;
            //Khai báo lấy dữ liệu
            DEPARTMENT_BUS bus = new DEPARTMENT_BUS();
            //Thêm điều kiện lọc theo codeview nếu có nhập
            li = bus.getListByLvCode(lveducationCode);
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