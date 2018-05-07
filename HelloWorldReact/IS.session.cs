using System;
using System.Collections.Generic;
using HelloWorldReact.Models.Menu;
/// <summary>
/// Summary description for lang
/// </summary>
namespace IS.Sess
{
    /// <summary>
    /// LOGINED STAFF
    /// </summary>
    public class STAFF_INFO
    {
        public STAFF_INFO()
        {

        }
        /// <summary>
        /// Gán các thông tin cho đối tượng hiện tại khi khởi tạo đối tượng
        /// </summary>
       
        public STAFF_INFO( string code , string codeview ,string username, string employees, string employeesname, string number, int sex, string position
            , int level, string parentcode, string unitcode , DateTime logtime , List<MENU_OBJ> menu , List<MENU_OBJ> menuCha)
        {
            CODE = code;
            CODEVIEW = codeview;
            USERMAME = username;
            EMPLOYEESCODE = employees;
            EMPLOYEESNAME = employeesname;
            NUMBER = number;
            SEX = sex;
            POSITION = position;
            LEVEL = level;
            PARENTUNITCODE = parentcode;
            UNITCODE = unitcode;
            LOGTIME = logtime;
            MENU = menu;
            MENUCHA = menuCha;
        }
        public string CODE { get; set; }

        public string CODEVIEW { get; set; }

        public string USERMAME { get; set; }

        public string EMPLOYEESCODE { get; set; }

        public string EMPLOYEESNAME { get; set; }

        public string NUMBER { get; set; }
        
        public int SEX { get; set; }

        public string POSITION { get; set; }

        public Nullable<int> LEVEL { get; set; }

        public string PARENTUNITCODE { get; set; }

        public string UNITCODE { get; set; }

        public DateTime LOGTIME { get; set; }

        public List<MENU_OBJ> MENUCHA { get; set; }

        public List<MENU_OBJ> MENU { get; set; }

    }
    /// <summary>
    /// manage all sesion information
    /// </summary>

    public class session : System.Web.UI.Page
    {
        public session()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Thiết lập giá trị thông qua key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int setKey(string key, object value)
        {
            Context.Session[key] = value;
            return 0;
        }
        /// <summary>
        /// Lấy giá trị key, trong trường hợp không tồn tại sẽ trả về defaultValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public object getKey(string key, object defaultValue)
        {
            if (Context.Session[key] == null)
            {
                return defaultValue;
            }
            return Context.Session[key];
        }

        #region 4 login
        public int login(STAFF_INFO staff)
        {
            Context.Session["STAFF_INFO"] = staff;
            //assign for later
            return 0;
        }
 
        /// <summary>
        /// Remove login information
        /// </summary>
        /// <returns></returns>
        public int logout()
        {
            Context.Session["STAFF_INFO"] = null;
            return 0;
        }
        /// <summary>
        /// Check the login status, 0 is logined
        /// </summary>
        /// <returns>-1: not yet, 0 is logged</returns>
        public int isLogin()
        {
            if (Context.Session["STAFF_INFO"] == null)
            {
                return -1;
            }
            STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
            if (staff.CODE == "")
            {
                return -1;
            }
            return 0;
        }
        #endregion
        #region login and logined information
        /// <summary>
        /// Tên đăng nhập - username
        /// </summary>
        public string loginName
        {
            get
            {
                if (Context.Session["STAFF_INFO"] == null)
                    return "";
                STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
                return staff.CODEVIEW;
            }
            //set
            //{
            //    if (Context.Session["STAFF_INFO"] != null)
            //    {
            //        STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
            //        staff.CODEVIEW=value;
            //        Context.Session["STAFF_INFO"] = staff;
            //    }
            //}
        }
        /// <summary>
        /// get the login full name
        /// </summary>
        public string loginFullName
        {
            get
            {
                if (Context.Session["STAFF_INFO"] == null)
                    return "";
                STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
                return staff.EMPLOYEESNAME;
            }
            //set
            //{
            //    if (Context.Session["STAFF_INFO"] != null)
            //    {
            //        STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
            //        staff.NAME = value;
            //        Context.Session["STAFF_INFO"] = staff;
            //    }
            //}
        }
        /// <summary>
        /// ảnh của người đăng nhập hiện tại
        /// </summary>
        //public string loginImg
        //{
        //    get
        //    {
        //        if (Context.Session["STAFF_INFO"] == null)
        //            return "";
        //        STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
        //        return staff.IMG;
        //    }
        //    //set
        //    //{
        //    //    if (Context.Session["STAFF_INFO"] != null)
        //    //    {
        //    //        STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
        //    //        staff.NAME = value;
        //    //        Context.Session["STAFF_INFO"] = staff;
        //    //    }
        //    //}
        //}
        /// <summary>
        /// code of staff
        /// </summary>
        public string loginCode
        {
            get
            {
                if (Context.Session["STAFF_INFO"] == null)
                    return "";
                STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
                return staff.CODE;
            }
            //set
            //{
            //    if (Context.Session["STAFF_INFO"] != null)
            //    {
            //        STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
            //        staff.NAME = value;
            //        Context.Session["STAFF_INFO"] = staff;
            //    }
            //}
        }

        /// <summary>
        /// Nhân viên đang đăng nhập
        /// </summary>
        public STAFF_INFO STAFF
        {
            get
            {
                if (Context.Session["STAFF_INFO"] == null)
                    return null;
                STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
                return staff;
            }
            set
            {
                if (Context.Session["STAFF_INFO"] != null)
                {
                    STAFF_INFO staff = (STAFF_INFO)Context.Session["STAFF_INFO"];
                    Context.Session["STAFF_INFO"] = value;
                }
            }
        }

        #endregion

        public int func(string key)
        {
            return 15;
        }
    }
}