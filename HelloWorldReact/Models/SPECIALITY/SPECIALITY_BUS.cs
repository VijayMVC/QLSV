using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        public List<SPECIALITY_OBJ> searchSpeciality(string dataSearch)
        {
            List<SPECIALITY_OBJ> lidata = new List<SPECIALITY_OBJ>();
            string sql = "SELECT * FROM SPECIALITY WHERE SPECIALITYNAME LIKE '%"+ dataSearch + "%' OR CODEVIEW LIKE '%" + dataSearch + "%'";
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

        public List<SPECIALITY_OBJ> getListByDepartmentCode(string departmentCode)
        {
            List<SPECIALITY_OBJ> lidata = new List<SPECIALITY_OBJ>();
            string sql = "SELECT * FROM SPECIALITY WHERE DEPARTMENTCODE = @departmentCode";
            SqlCommand cm = new SqlCommand();
            sql += " ORDER BY CODEVIEW";
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add("@departmentCode", SqlDbType.VarChar).Value = departmentCode;
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

        public List<SPECIALITY_OBJ> FillToOBJ(DataSet ds)
        {
            List<SPECIALITY_OBJ> lidata = new List<SPECIALITY_OBJ>();
            foreach (DataRow dr in ds.Tables["Tmp"].Rows)
            {
                SPECIALITY_OBJ obj = new SPECIALITY_OBJ();

                Type myTableObject = typeof(SPECIALITY_OBJ);
                System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                Type myObjectType = typeof(SPECIALITY_OBJ);
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
                        SPECIALITY_OBJ objid;
                        objid = (SPECIALITY_OBJ)info.GetValue(obj, null);
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