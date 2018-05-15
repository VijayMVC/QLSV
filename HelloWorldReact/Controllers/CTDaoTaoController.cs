using HelloWorldReact.Models;
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
    }
}