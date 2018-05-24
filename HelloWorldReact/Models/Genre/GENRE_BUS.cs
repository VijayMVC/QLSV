using HelloWorldReact.Models.GENRE;
using IS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.GENRE
{
    public class GENRE_BUS
    {
        public DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public GENRE_BUS()
        {
        }
        public GENRE_OBJ createObject()
        {
            GENRE_OBJ obj = new GENRE_OBJ();
            return obj;
        }
        public GENRE_OBJ createNull()
        {
            return null;
        }

        /// <summary>
        /// Lấy danh sách bộ môn
        /// </summary>
        /// <returns></returns>
        public List<GENRE_OBJ> GetGenreList(string name, string facultycode)
        {
            List<GENRE_OBJ> lidata = new List<GENRE_OBJ>();
            string sql = "GetGenreList '" + name + "','" + facultycode + "'";
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

        /// <summary>
        /// Lấy thông tin bộ theo mã
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GENRE_OBJ> GetGenre(string codeview)
        {
            List<GENRE_OBJ> lidata = new List<GENRE_OBJ>();
            string sql = "GetGenre '" + codeview + "'";
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

        /// <summary>
        /// Xóa một bộ môn
        /// Cập nhật lại mã bộ môn ở môn học liên quan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteGenre(string codeview)
        {
            int ret = 0;
            string sql = "DeleteGenre '" + codeview + "'";
            SqlCommand cm = new SqlCommand();
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            ret = db.doCommand(ref cm);
            return ret;
        }

        /// <summary>
        /// Cập nhật thông tin một bộ môn
        /// Cập nhật môn học liên quan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateGenre(string codeview, GENRE_OBJ obj)
        {
            int ret = 0;
            string sql = "UpdateGenre '" + codeview + "','" + obj.CODEVIEW + "',N'" + obj.GENRENAME + "',N'" + obj.GENREDESCRIPTION + "','" + obj.FACULTYCODE + "'";
            SqlCommand cm = new SqlCommand();
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            ret = db.doCommand(ref cm);
            return ret;
        }

        /// <summary>
        /// Thêm mới một bộ môn
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CreateGenre(GENRE_OBJ obj)
        {
            int ret = 0;
            string sql = "CreateGenre '" + obj.CODE + "','" + obj.CODEVIEW + "',N'" + obj.GENRENAME + "',N'" + obj.GENREDESCRIPTION + "','" + obj.FACULTYCODE + "'";
            SqlCommand cm = new SqlCommand();
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            ret = db.doCommand(ref cm);
            return ret;
        }

        /// <summary>
        /// Chuyển dữ liệu thành List
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public List<GENRE_OBJ> FillToOBJ(DataSet ds)
        {
            List<GENRE_OBJ> lidata = new List<GENRE_OBJ>();
            foreach (DataRow dr in ds.Tables["Tmp"].Rows)
            {
                GENRE_OBJ obj = new GENRE_OBJ();

                Type myTableObject = typeof(GENRE_OBJ);
                System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                Type myObjectType = typeof(GENRE_OBJ);
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
                        GENRE_OBJ objid;
                        objid = (GENRE_OBJ)info.GetValue(obj, null);
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