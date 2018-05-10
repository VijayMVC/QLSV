using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.Menu
{
    public class MENU_BUS
    {
        public DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public MENU_BUS()
        {
        }
        public MENU_OBJ createObject()
        {
            MENU_OBJ obj = new MENU_OBJ();
            return obj;
        }
        public MENU_OBJ createNull()
        {
            return null;
        }
        public List<MENU_OBJ> GetMenu(params spParam[] listFilter)
        {
            List<MENU_OBJ> lidata = new List<MENU_OBJ>();
            string sql = "SELECT * FROM AU_MENU";
            string swhere = "";
            SqlCommand cm = new SqlCommand();
            foreach (var item in listFilter)
            {
                if (swhere != "")
                {
                    swhere += " OR ";
                }
                if (item.data == null)
                {
                    //cm.Parameters.Add("@" + f.Name, st);
                    //cm.Parameters["@" + f.Name].Value = DBNull.Value;
                    swhere += "[" + item.name + "]" + " is null";
                }
                else
                {
                    if (item.searchtype == 0)
                    {
                        swhere += "[" + item.name + "]= @" + item.name;
                        cm.Parameters.Add(new SqlParameter("@" + item.name, item.data));
                    }
                    else
                    {
                        swhere += "[" + item.name + "] LIKE @" + item.name;
                        cm.Parameters.Add(new SqlParameter("@" + item.name, "%" + item.data + "%"));
                    }
                }
            }
            if (swhere != "")
            {
                sql += " WHERE " + swhere;
            }
            sql += " ORDER BY SORT";
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

        public List<MENU_OBJ> GetParent()
        {
            List<MENU_OBJ> lidata = new List<MENU_OBJ>();
            string sql = "SELECT * FROM AU_MENU WHERE MENUIDCHA IS NULL";
            SqlCommand cm = new SqlCommand();
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

        public List<MENU_OBJ> FillToOBJ(DataSet ds)
        {
            List<MENU_OBJ> lidata = new List<MENU_OBJ>();
            foreach (DataRow dr in ds.Tables["Tmp"].Rows)
            {
                MENU_OBJ obj = new MENU_OBJ();

                Type myTableObject = typeof(MENU_OBJ);
                System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                Type myObjectType = typeof(MENU_OBJ);
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
                        MENU_OBJ objid;
                        objid = (MENU_OBJ)info.GetValue(obj, null);
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

        public MENU_OBJ GetMenuByCode(string code)
        {
            MENU_OBJ lidata = new MENU_OBJ();
            string sql = "SELECT * FROM AU_MENU WHERE MENUID = '" + code + "'";
            SqlCommand cm = new SqlCommand();
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
                List<MENU_OBJ> temp = new List<MENU_OBJ>();
                temp = FillToOBJ(ds);
                if (temp.Count != 0)
                {
                    lidata = temp[0];
                }
                else
                {
                    lidata = null;
                }
            }
            return lidata;
        }

        public int Insert(MENU_OBJ obj)
        {
            int ret = 0;
            string sql = "INSERT INTO AU_MENU(ID , MENUID, MENUIDCHA , TITLE , URL , SORT , TRANGTHAI , I_CREATE_DATE) VALUES(@id,@menuid, @menuidcha, @title, @url,@sort,@trangthai,@create_Date)";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Parameters.Add("@id", SqlDbType.VarChar).Value = obj.ID;
            com.Parameters.Add("@menuid", SqlDbType.VarChar).Value = obj.MenuId;
            com.Parameters.Add("@menuidcha", SqlDbType.VarChar).Value = obj.MenuIdCha;
            com.Parameters.Add("@title", SqlDbType.NVarChar).Value = obj.Title;
            com.Parameters.Add("@url", SqlDbType.NVarChar).Value = obj.Url;
            com.Parameters.Add("@trangthai", SqlDbType.Int).Value = obj.TrangThai;
            com.Parameters.Add("@sort", SqlDbType.Int).Value = obj.Sort;
            com.Parameters.Add("@create_Date", SqlDbType.DateTime).Value = DateTime.Now;
            ret = db.doCommand(ref com);
            return ret;
        }
        public int Update(MENU_OBJ obj)
        {
            int ret = 0;
            string sql = @"UPDATE AU_MENU SET 
                    MENUIDCHA=@menuidcha
                    , TITLE=@title
                    , URL=@url
                    ,SORT=@sort
                    ,TRANGTHAI=@trangthai
                    , I_CREATE_DATE=@create_Date
                    ,I_UPDATE_DATE = @update_Date
                    WHERE MENUID = @menuid";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Parameters.Add("@menuid", SqlDbType.VarChar).Value = obj.MenuId ?? (object)DBNull.Value;
            com.Parameters.Add("@menuidcha", SqlDbType.VarChar).Value = obj.MenuIdCha ?? (object)DBNull.Value;
            com.Parameters.Add("@title", SqlDbType.NVarChar).Value = obj.Title ?? (object)DBNull.Value;
            com.Parameters.Add("@url", SqlDbType.NVarChar).Value = obj.Url ?? (object)DBNull.Value;
            com.Parameters.Add("@trangthai", SqlDbType.Int).Value = obj.TrangThai;
            com.Parameters.Add("@sort", SqlDbType.Int).Value = obj.Sort;
            com.Parameters.Add("@create_Date", SqlDbType.DateTime).Value = obj.ICREATEDATE ?? (object)DBNull.Value;
            com.Parameters.Add("@update_Date", SqlDbType.DateTime).Value = obj.IUPDATEDTAE ?? (object)DBNull.Value;
            ret = db.doCommand(ref com);
            return ret;
        }
        public int Delete(MENU_OBJ obj)
        {
            int ret = 0;
            string sql = @"DELETE FROM AU_MENU  WHERE MENUID=@code_key";
            SqlCommand com = new SqlCommand();
            com.CommandText = sql;
            com.CommandType = CommandType.Text;
            com.Parameters.Add("@code_key", SqlDbType.VarChar).Value = obj.MenuId;
            ret = db.doCommand(ref com);
            return ret;
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