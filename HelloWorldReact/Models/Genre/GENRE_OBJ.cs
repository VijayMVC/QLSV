﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models.GENRE
{
    public class GENRE_OBJ : DATAINFOBASE
    {
        [Display(Name = "Mã")]
        [StringLength(100)]
        public string CODEVIEW { get; set; }

        [Display(Name = "Tên bộ môn")]
        [StringLength(100)]
        public string GENRENAME { get; set; }

        [Display(Name = "Mô tả bộ môn")]
        [StringLength(500)]
        public string GENREDESCRIPTION { get; set; }

        [Display(Name = "Mã khoa")]
        [StringLength(100)]
        public string FACULTYCODE { get; set; }

        public GENRE_OBJ() { }

        public GENRE_OBJ(string codeview, string genrename, string genredescription, string facultycode)
        {
            this.CODEVIEW = codeview;
            this.GENRENAME = genrename;
            this.GENREDESCRIPTION = genredescription;
            this.FACULTYCODE = facultycode;
        }
    }
}