using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.User
{
    public class USER_OBJ : DATAINFOBASE
    {
        [Display(Name = "Tên Đăng nhập")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Mã nhân viên")]
        public string MaNhanVien { get; set; }

        [Display(Name = "Tên nhân viên")]
        public string TenNhanVien { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Chứng minh thư")]
        public string ChungMinhThu { get; set; }

        [Display(Name = "Giới tính")]
        public int GioiTinh { get; set; }

        [Display(Name = "Mã phòng")]
        public string MaPhong { get; set; }

        [Display(Name = "Chức vụ")]
        public string ChucVu { get; set; }

        [Display(Name = "Cấp độ")]
        public Nullable<int> Level { get; set; }

        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; }

        [Display(Name = "Mã đơn vị cha")]
        public string ParentUnitcode { get; set; }
    }
}