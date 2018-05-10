using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloWorldReact.Models
{
    public class DBINFOMENU
    {
        [Key]
        [Display(Name = "Mã")]
        public string ID { get; set; }

        [Display(Name = "Ngày khởi tạo")]
        public virtual DateTime? ICREATEDATE { get; set; }

        [Display(Name = "Người khởi tạo")]
        public virtual string ICREATEBY { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public virtual DateTime? IUPDATEDTAE { get; set; }

        [Display(Name = "Người cập nhật")]
        public virtual string IUPDATEBY { get; set; }

        [Display(Name = "Trạng thái")]
        public virtual string ISTATE { get; set; }

        [Display(Name = "Mã đơn vị")]
        public string UNITCODE { get; set; }
    }
}