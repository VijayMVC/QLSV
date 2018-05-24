using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IS.Base;
using System.Configuration;

namespace HelloWorldReact.Models.FACULTY
{
    public class FACULTY_BUS
    {
        public DBBase db = new DBBase(ConfigurationManager.ConnectionStrings["QLSV"].ConnectionString);
        public FACULTY_BUS()
        {
        }
        public FACULTY_OBJ createObject()
        {
            FACULTY_OBJ obj = new FACULTY_OBJ();
            return obj;
        }
        public FACULTY_OBJ createNull()
        {
            return null;
        }

        /// <summary>
        /// Lấy danh sách khoa theo tên
        /// Lấy toàn bộ danh sách nếu tên rỗng
        /// </summary>
        /// <returns></returns>
        public List<FACULTY_OBJ> GetFacultyList(string name)
        {
            List<FACULTY_OBJ> lidata = new List<FACULTY_OBJ>();
            string sql = "GetFacultyList N'" + name + "'";
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
        /// Lấy thông tin khoa theo mã
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FACULTY_OBJ> GetFaculty(string codeview)
        {
            List<FACULTY_OBJ> lidata = new List<FACULTY_OBJ>();
            string sql = "GetFaculty '" + codeview + "'";
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
        /// Xóa một khoa
        /// Cập nhật lại mã khoa ở bộ môn liên quan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteFaculty(string codeview)
        {
            int ret = 0;
            string sql = "DeleteFaculty '" + codeview + "'";
            SqlCommand cm = new SqlCommand();
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            ret = db.doCommand(ref cm);
            return ret;
        }

        /// <summary>
        /// Cập nhật thông tin một khoa
        /// Cập nhật bộ môn liên quan
        /// </summary>
        /// <param name="id"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateFaculty(string codeview, FACULTY_OBJ obj)
        {
            int ret = 0;
            string sql = "UpdateFaculty '" + codeview + "','" + obj.CODEVIEW + "',N'" + obj.FACULTYNAME + "',N'" + obj.FACULTYDESCRIPTION + "'";
            SqlCommand cm = new SqlCommand();
            cm.CommandText = sql;
            cm.CommandType = CommandType.Text;
            ret = db.doCommand(ref cm);
            return ret;
        }

        /// <summary>
        /// Thêm mới một khoa
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CreateFaculty(FACULTY_OBJ obj)
        {
            int ret = 0;
            string sql = "CreateFaculty '" + obj.CODE + "','" + obj.CODEVIEW + "',N'" + obj.FACULTYNAME + "',N'" + obj.FACULTYDESCRIPTION + "'";
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
        public List<FACULTY_OBJ> FillToOBJ(DataSet ds)
        {
            List<FACULTY_OBJ> lidata = new List<FACULTY_OBJ>();
            foreach (DataRow dr in ds.Tables["Tmp"].Rows)
            {
                FACULTY_OBJ obj = new FACULTY_OBJ();

                Type myTableObject = typeof(FACULTY_OBJ);
                System.Reflection.PropertyInfo[] selectFieldInfo = myTableObject.GetProperties();

                Type myObjectType = typeof(FACULTY_OBJ);
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
                        FACULTY_OBJ objid;
                        objid = (FACULTY_OBJ)info.GetValue(obj, null);
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