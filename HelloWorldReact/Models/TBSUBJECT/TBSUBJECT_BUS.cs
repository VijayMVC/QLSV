using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public List<TBSUBJECT_OBJ> getListByDepartmentCode(TranferObj obj)
        {
            List<TBSUBJECT_OBJ> lidata = new List<TBSUBJECT_OBJ>();
            string sql = "GET_SUBJECT_BY_SPECIALITY";
            SqlCommand cm = new SqlCommand();
            cm.CommandText = sql;
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@specialitycode", obj.speciality);
            cm.Parameters.AddWithValue("@leveleducation", obj.leveleducation);
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
        public List<TBSUBJECT_OBJ> FillToOBJ(DataSet ds)
        {
            List<TBSUBJECT_OBJ> lidata = new List<TBSUBJECT_OBJ>();
            foreach (DataRow dr in ds.Tables["Tmp"].Rows)
            {
                TBSUBJECT_OBJ obj = new TBSUBJECT_OBJ();

                Type myTableObject = typeof(TBSUBJECT_OBJ);
                System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                Type myObjectType = typeof(TBSUBJECT_OBJ);
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
                        TBSUBJECT_OBJ objid;
                        objid = (TBSUBJECT_OBJ)info.GetValue(obj, null);
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