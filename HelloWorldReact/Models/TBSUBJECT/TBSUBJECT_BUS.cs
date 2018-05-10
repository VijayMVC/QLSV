using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.TBSUBJECT
{
    public class TBSUBJECT_BUS
    {
        public DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public TBSUBJECT_BUS()
        {
        }
        public TBSUBJECT_OBJ createObject()
        {
            TBSUBJECT_OBJ obj = new TBSUBJECT_OBJ();
            return obj;
        }
        public TBSUBJECT_OBJ createNull()
        {
            return null;
        }
    }
}