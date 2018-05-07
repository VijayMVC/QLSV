using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.Menu
{
    public class MENU_OBJ : DATAINFOBASE
    {
        [Display(Name = "Mã cha")]
        [StringLength(100)]
        public string MenuIdCha { get; set; }

        [Display(Name = "Mã")]
        [StringLength(100)]
        public string MenuId { get; set; }

        [Display(Name = "Tiêu đề")]
        [StringLength(200)]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [StringLength(500)]
        public string Url { get; set; }

        [Display(Name = "Số thứ tự")]
        public int Sort { get; set; }

        [Display(Name = "Trạng thái")]
        public int TrangThai { get; set; }

        public List<MENU_OBJ> ChildrenMenu;
    }
}