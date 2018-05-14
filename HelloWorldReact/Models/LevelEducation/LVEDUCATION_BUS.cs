using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        public List<LVEDUCATION_OBJ> getList()
        {
            List<LVEDUCATION_OBJ> lidata = new List<LVEDUCATION_OBJ>();
            string sql = "SELECT * FROM LEVELEDUCATION";
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

        public int getNumberYear(string codeview)
        {
            int NBY = 0;
            string sql = "SELECT NUMBERYEAR FROM LEVELEDUCATION WHERE CODEVIEW = "+"'"+ codeview + "'";
            SqlCommand cm = new SqlCommand();
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            SqlDataReader reader = db.getCommand(cm);
            while (reader.Read())
            {
                NBY = Convert.ToInt16(reader["NUMBERYEAR"]);
            }
            return NBY;
        }

        public List<LVEDUCATION_OBJ> FillToOBJ(DataSet ds)
        {
            List<LVEDUCATION_OBJ> lidata = new List<LVEDUCATION_OBJ>();
            foreach (DataRow dr in ds.Tables["Tmp"].Rows)
            {
                LVEDUCATION_OBJ obj = new LVEDUCATION_OBJ();

                Type myTableObject = typeof(LVEDUCATION_OBJ);
                System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                Type myObjectType = typeof(LVEDUCATION_OBJ);
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
                        LVEDUCATION_OBJ objid;
                        objid = (LVEDUCATION_OBJ)info.GetValue(obj, null);
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