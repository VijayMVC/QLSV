using HelloWorldReact.Models.SPECIALITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorldReact.Controllers
{
    public class SPECIALITYController : Controller
    {
        public JsonResult getListByDepartmentCode(string departmentCode)
        {
            List<SPECIALITY_OBJ> li = null;
            //Khai báo lấy dữ liệu
            SPECIALITY_BUS bus = new SPECIALITY_BUS();
            //Thêm điều kiện lọc theo codeview nếu có nhập
            li = bus.getListByDepartmentCode(departmentCode);
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