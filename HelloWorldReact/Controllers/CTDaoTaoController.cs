using HelloWorldReact.Models;
using HelloWorldReact.Models.SPECIALITY;
using HelloWorldReact.Models.TBSUBJECT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorldReact.Controllers
{
    public class CTDaoTaoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAllSubject(TranferObj obj)
        {
            TBSUBJECT_BUS bus = new TBSUBJECT_BUS();
            string message = string.Empty;
            List<TBSUBJECT_OBJ> li = null;
            li = bus.getListByDepartmentCode(obj);
            bus.CloseConnection();
            if(obj.semester != null)
            {
                message = "Danh sách môn học theo kỳ : " + obj.semester;
            }
            else if(obj.speciality!= null)
            {
                message = "Danh sách môn học theo theo chuyên ngành";
            }
            return Json(new
            {
                data = li,//Danh sách
                message = message,
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchData(string datasearch ,string filter)
        {
            List<TBSUBJECT_OBJ> li = null;
            List<SPECIALITY_OBJ> lispe = null;
            if (filter == "1")
            {
                TBSUBJECT_BUS bus = new TBSUBJECT_BUS();
                li = bus.searchSubject(datasearch);
                bus.CloseConnection();
                return Json(new
                {
                    data = li,
                    message = "Tìm kiếm thành công",
                    ret = 0,
                });
            }
            else if(filter == "2")
            {
                SPECIALITY_BUS bus = new SPECIALITY_BUS();
                lispe = bus.searchSpeciality(datasearch);
                bus.CloseConnection();
                return Json(new {
                    data = lispe,
                    message = "Tìm kiếm thành công",
                    ret = 0,
                });
            }
            else
            {
                TBSUBJECT_BUS bus = new TBSUBJECT_BUS();
                li = bus.searchSubjectRequre(datasearch);
                bus.CloseConnection();
                return Json(new
                {
                    data = li,
                    message = "Tìm kiếm thành công",
                    ret = 0,
                });
            }
        }

        [HttpPost]
        public JsonResult SearchRequreSubject(string datasearch)
        {
            List<TBSUBJECT_OBJ> li = null;
            TBSUBJECT_BUS bus = new TBSUBJECT_BUS();
            li = bus.searchSubjectRequreBySpeciality(datasearch);
            bus.CloseConnection();
            return Json(new
            {
                data = li,
                message = "Tìm kiếm thành công",
                ret = 0,
            });
        }
    }
}