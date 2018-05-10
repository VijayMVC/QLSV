using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.DEPARTMENT
{
    public class DEPARTMENT_OBJ : DATAINFOBASE
    {
        [Display(Name = "Mã")]
        [StringLength(100)]
        public string CODEVIEW { get; set; }

        [Display(Name = "Tên")]
        [StringLength(100)]
        public string DEPARTMENTNAME { get; set; }

        [Display(Name = "Mã bậc học")]
        [StringLength(200)]
        public string LEVELEDUCATIONCODE { get; set; }
    }
}