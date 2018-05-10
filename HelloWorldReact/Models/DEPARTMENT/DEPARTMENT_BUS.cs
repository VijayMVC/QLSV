using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.DEPARTMENT
{
    public class DEPARTMENT_BUS
    {
        public DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public DEPARTMENT_BUS()
        {
        }
        public DEPARTMENT_OBJ createObject()
        {
            DEPARTMENT_OBJ obj = new DEPARTMENT_OBJ();
            return obj;
        }
        public DEPARTMENT_OBJ createNull()
        {
            return null;
        }
    }
}