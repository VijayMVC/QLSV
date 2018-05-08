using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IS.uni;
using IS.Base;
using IS.authen;
using IS.Sess;
using HelloWorldReact.Models.EntitiesVM;
using HelloWorldReact.Utils.Helper;
using HelloWorldReact.Models.User;
using HelloWorldReact.Models.Menu;

namespace HelloWorldReact.Controllers
{
    public class HomeController : Controller
    {
        session ses = new session();
        private USER_BUS _serviceUser = new USER_BUS();
        private MENU_BUS _serviceMenu = new MENU_BUS();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult dologin(AccountVM account)
        {
            //Kiểm tra mật khâu cơ sở dữ liệu ở đây.
            USER_OBJ user_LogedIn = checkLogin(account);
            if (user_LogedIn != null)
            {
                STAFF_INFO staff = new STAFF_INFO();
                staff.CODE = user_LogedIn.CODE;
                staff.USERMAME = user_LogedIn.Username;
                staff.LOGTIME = DateTime.Now;
                List<MENU_OBJ> lidata = _serviceMenu.GetMenu();
                staff.MENUCHA = lidata.Where(x => x.MenuIdCha == null).OrderBy(x=>x.Sort).ToList();
                staff.MENU = lidata.Where(x => x.MenuIdCha != null).OrderBy(x => x.Sort).ToList();
                _serviceMenu.CloseConnection();
                ses.login(staff);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Logout()
        {
            ses.logout();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Nation()
        {
            NATION_BUS bus = new NATION_BUS();
            List<NATION_OBJ> li = bus.getAll(); 
            return View(li);
        }
        [CustomAuthentication]
        public ActionResult Nationreact()
        {
            return View();
        }

        public JsonResult GetName()
        {
            return Json(new { name = "World from server side" }, JsonRequestBehavior.AllowGet);
        }
        private USER_OBJ checkLogin(AccountVM account)
        {
            string password = MD5Encrypt.MD5Hash(account.Password);
            USER_OBJ user = _serviceUser.GetForLogin(account);
            _serviceUser.CloseConnection();
            if (password.Equals(user.Password))
                return user ;
            return null;
        }
    }
}