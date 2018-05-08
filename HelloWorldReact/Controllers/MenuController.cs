using HelloWorldReact.Models.Menu;
using IS.Base;
using IS.Sess;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorldReact.Controllers
{
    public class MenuController : Controller
    {
        session ses = new session();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Menu
        public JsonResult getlist(string keysearch, int? page, int? pageCur)
        {
            List<MENU_OBJ> li = null;
            int pageSize = page ?? 10;
            int pageCurrent = pageCur ?? 1;
            //Khai báo lấy dữ liệu
            MENU_BUS bus = new MENU_BUS();
            List<spParam> lipa = new List<spParam>();
            //Thêm điều kiện lọc theo codeview nếu có nhập
            if (keysearch != "")
            {
                lipa.Add(new spParam("MENUID", System.Data.SqlDbType.VarChar, keysearch, 1));//search on codeview
                lipa.Add(new spParam("TITLE", System.Data.SqlDbType.NVarChar, keysearch, 1));//search on codeview
            }
            int countpage = 0;
            //order by theorder, with pagesize and the page
            li = bus.GetMenu(lipa.ToArray());
            bus.CloseConnection();
            //Chỉ số đầu tiên của trang hiện tại (đã trừ -1)
            //Trả về client
            return Json(new
            {
                data = li,//Danh sách
                total = countpage,//số lượng trang
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult getParent()
        {
            List<MENU_OBJ> li = null;

            MENU_BUS bus = new MENU_BUS();
            li = bus.GetParent();
            int countpage = 0;
            bus.CloseConnection();
            return Json(new
            {
                data = li,//Danh sách
                total = countpage,//số lượng trang
                ret = 0//ok
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        /// <summary>
        /// Cập nhật một bản ghi được gửi lên từ phía client
        /// </summary>
        public JsonResult create(MENU_OBJ obj)
        {
            MENU_BUS bus = new MENU_BUS();
            int ret = 0;
            var data = bus.GetMenuByCode(obj.MenuId);
            if (data == null)
            {
                ret = 0;
                obj.CODE = Guid.NewGuid().ToString();
                ret = bus.Insert(obj);
            }
            else
            {
                ret = -1;
            }
            bus.CloseConnection();

            //some thing like that
            return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        /// <summary>
        /// Cập nhật một bản ghi được gửi lên từ phía client
        /// </summary>
        public JsonResult update(MENU_OBJ obj)
        {
            MENU_BUS bus = new MENU_BUS();
            int ret = 0;
            var data = bus.GetMenuByCode(obj.MenuId);
            if (data != null)
            {
                ret = 0;
                obj.IUPDATEDTAE = DateTime.Now;
                ret = bus.Update(obj);
            }
            else
            {
                ret = -1;
            }
            bus.CloseConnection();

            //some thing like that
            return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult delete(string id)
        {
            int ret = 0;
            MENU_BUS bus = new MENU_BUS();

            MENU_OBJ obj = bus.GetMenuByCode(id);
            if (obj == null)
            {
                ret = -1;
            }
            if (ret >= 0)
            {
                ret = bus.Delete(obj);
            }
            bus.CloseConnection();
            return Json(new { sussess = ret }, JsonRequestBehavior.AllowGet);
        }
    }
}