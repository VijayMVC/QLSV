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
            List<TBSUBJECT_OBJ> li = null;
            li = bus.getListByDepartmentCode(obj);
            bus.CloseConnection();
            return Json(new
            {
                data = li,//Danh sách
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);
        }
    }
}