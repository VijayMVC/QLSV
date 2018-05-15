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
            DEPARTMENT_BUS bus = new DEPARTMENT_BUS();
            li = bus.getListByLvCode(lveducationCode);
            bus.CloseConnection();
            return Json(new
            {
                data = li,//Danh sách
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);

        }
    }
}