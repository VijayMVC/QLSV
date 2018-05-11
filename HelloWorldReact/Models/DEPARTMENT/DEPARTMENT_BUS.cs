using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public List<DEPARTMENT_OBJ> getList()
        {
            List<DEPARTMENT_OBJ> lidata = new List<DEPARTMENT_OBJ>();
            string sql = "SELECT * FROM DEPARTMENT";
            SqlCommand cm = new SqlCommand();
            sql += " ORDER BY CODEVIEW";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            int ret = db.getCommand(ref ds, "Tmp", cm);
            if (ret < 0)
            {
                return null;
            }
            else
            {
                lidata = FillToOBJ(ds);
            }
            return lidata;
        }
        public List<DEPARTMENT_OBJ> FillToOBJ(DataSet ds)
        {
            List<DEPARTMENT_OBJ> lidata = new List<DEPARTMENT_OBJ>();
            foreach (DataRow dr in ds.Tables["Tmp"].Rows)
            {
                DEPARTMENT_OBJ obj = new DEPARTMENT_OBJ();

                Type myTableObject = typeof(DEPARTMENT_OBJ);
                System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                Type myObjectType = typeof(DEPARTMENT_OBJ);
                System.Reflection.PropertyInfo[] fieldInfo = myObjectType.GetProperties();

                //set object value
                foreach (System.Reflection.PropertyInfo info in selectFieldInfo)
                {
                    if (info.Name != "_ID")
                    {
                        if (dr.Table.Columns.Contains(info.Name))
                        {
                            if (!dr.IsNull(info.Name))
                            {
                                info.SetValue(obj, dr[info.Name], null);
                            }
                        }
                    }
                    else
                    {
                        //set id value
                        DEPARTMENT_OBJ objid;
                        objid = (DEPARTMENT_OBJ)info.GetValue(obj, null);
                        foreach (System.Reflection.PropertyInfo info1 in fieldInfo)
                        {
                            if (dr.Table.Columns.Contains(info1.Name))
                            {
                                info1.SetValue(objid, dr[info1.Name], null);
                            }
                        }
                        info.SetValue(obj, objid, null);
                    }
                }
                lidata.Add(obj);
            }
            return lidata;
        }

        public int Open()
        {
            return db.Open();
        }
        public void CloseConnection()
        {
            db.Close();
        }
    }
}