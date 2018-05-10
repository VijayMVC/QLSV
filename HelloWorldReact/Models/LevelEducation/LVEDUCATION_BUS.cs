using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.LevelEducation
{
    public class LVEDUCATION_BUS
    {
        public DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public LVEDUCATION_BUS()
        {
        }
        public LVEDUCATION_OBJ createObject()
        {
            LVEDUCATION_OBJ obj = new LVEDUCATION_OBJ();
            return obj;
        }
        public LVEDUCATION_OBJ createNull()
        {
            return null;
        }
    }
}