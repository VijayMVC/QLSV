using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.SPECIALITY
{
    public class SPECIALITY_BUS
    {
        public DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public SPECIALITY_BUS()
        {
        }
        public SPECIALITY_OBJ createObject()
        {
            SPECIALITY_OBJ obj = new SPECIALITY_OBJ();
            return obj;
        }
        public SPECIALITY_OBJ createNull()
        {
            return null;
        }
    }
}