﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using team5_centric.Controllers;

namespace team5_centric.Models
{
    public class recognition
    {
        [Key]
        public Guid recId { get; set; }

        [Display (Name = "Full Name")]
        public Guid userId { get; set; }
        [ForeignKey("userId")]
        
        public virtual userData userDatas { get; set; }

        
        public Guid valueId { get; set; }
        [Display (Name = "Core Value")]
        public virtual values values { get; set; }

        [Display (Name = "Reason for Recognition")]
        public string valueComment { get; set; }
        //[Display (Name = "Recognition")]
        ////[StringLength(250)]
        
        public Guid recognizerId { get; set; }
        [ForeignKey("recognizerId")]
        //[Display (Name = "Recognizer")]
     
        public virtual userData recognizer { get; set; }
        
        
    }
}