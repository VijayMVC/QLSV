using HelloWorldReact.Models.EntitiesVM;
using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.User
{
    public class USER_BUS
    {
        private DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public USER_BUS()
        {
        }

        public USER_OBJ GetForLogin(AccountVM user)
        {
            USER_OBJ _user = new USER_OBJ();
            string sql = @"SELECT * FROM AU_NGUOIDUNG  WHERE USERNAME = @username";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Parameters.Add("@username", SqlDbType.VarChar).Value = user.Username;
            SqlDataReader reader = db.getCommand(com);
            if(reader != null){
                var dataTable = new DataTable();
                dataTable.Load(reader);
                _user = FillToOBJ(dataTable.Rows[0]);
            }
            return _user;
        }

        public USER_OBJ FillToOBJ(DataRow dr)
        {
            USER_OBJ obj = new USER_OBJ();

            Type myTableObject = typeof(USER_OBJ);
            System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

            Type myObjectType = typeof(USER_OBJ);
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
                    USER_OBJ objid;
                    objid = (USER_OBJ)info.GetValue(obj, null);
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
            return obj;
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