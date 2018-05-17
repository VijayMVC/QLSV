using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HelloWorldReact.Models.FACULTY
{
    public class FACULTY_OBJ : DATAINFOBASE
    {
        [Display(Name = "Mã")]
        [StringLength(100)]
        public string CODEVIEW { get; set; }

        [Display(Name = "Tên khoa")]
        [StringLength(200)]
        public string FACULTYNAME { get; set; }

        [Display(Name = "Mô tả khoa")]
        [StringLength(200)]
        public string FACULTYDESCRIPTION { get; set; }
    }
}