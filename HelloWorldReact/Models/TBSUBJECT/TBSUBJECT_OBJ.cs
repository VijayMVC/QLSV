using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.TBSUBJECT
{
    public class TBSUBJECT_OBJ : DATAINFOBASE
    {
        [Display(Name = "Mã")]
        [StringLength(100)]
        public string CODEVIEW { get; set; }

        [Display(Name = "Tên")]
        [StringLength(100)]
        public string SUBJECTNAME { get; set; }

        [Display(Name = "Mã cha")]
        [StringLength(200)]
        public string PARENTCODE { get; set; }

        [Display(Name = "NUMBEROFCREDIT")]
        public int NUMBEROFCREDIT { get; set; }

        [Display(Name = "EXPECTEDSEMESTER")]
        public int EXPECTEDSEMESTER { get; set; }

        public int NUMBEROFCLASS { get; set; }

        [Display(Name = "SUBJECTTYPE")]
        public int SUBJECTTYPE { get; set; }

        [Display(Name = "PREVIOUSSUBJECT")]
        [StringLength(10)]
        public string PREVIOUSSUBJECT { get; set; }

        [Display(Name = "Tỷ lệ")]
        [StringLength(10)]
        public string RATIO { get; set; }
        
        [Display(Name = "GENRECODE")]
        [StringLength(10)]
        public string GENRECODE { get; set; }

        [Display(Name = "Mã chuyên ngành")]
        [StringLength(10)]
        public string SPECIALITYCODE { get; set; }

        [Display(Name = "Mã ngành")]
        [StringLength(10)]
        public string LEVELEDUCATIONCODE { get; set; }
    }
}