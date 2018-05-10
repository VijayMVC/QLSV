using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models
{
    public class DATAINFOBASE
    {
        [Key]
        [Display(Name = "Mã")]
        public string CODE { get; set; }
    }
}