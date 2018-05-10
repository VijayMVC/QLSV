using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.SPECIALITY
{
    public class SPECIALITY_OBJ : DATAINFOBASE
    {
        [Display(Name = "Mã")]
        [StringLength(100)]
        public string CODEVIEW { get; set; }

        [Display(Name = "Tên")]
        [StringLength(100)]
        public string SPECIALITYNAME { get; set; }

        [Display(Name = "Mã ngành học")]
        [StringLength(200)]
        public string DEPARTMENTCODE { get; set; }

        [Display(Name = "Url")]
        [StringLength(500)]
        public string SUBJECTCODE { get; set; }
    }
}