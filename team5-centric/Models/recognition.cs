using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using team5_centric.Controllers;

namespace team5_centric.Models
{
    public class recognition
    {
        [Key]
        public Guid recId { get; set; }
        public Guid userId { get; set; }
        public virtual userData userDatas { get; set; }
       
        public Guid valueId { get; set; }
        public virtual values values { get; set; }
        [Display (Name = "Reason for Recognition")]
        public string valueComment { get; set; }
        [Display (Name = "Recognition")]
        public recognition recognitions { get; set; }
        
        
    }
}