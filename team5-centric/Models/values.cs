using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace team5_centric.Models
{
    public class values
    {
        [Key]
        public Guid valueId { get; set; }
        [Display (Name = "Value")]
        public string valueName { get; set; }
        [Display (Name = "Value Description")]
        public string valueDescription { get; set; }
        [Display (Name = "Value Icon")]
        public byte[] valueIcon { get; set; }
        [Display (Name = "Value Weight")]
        public int valueWeight { get; set; }
        public ICollection<recognition> recognitions { get; set; }
    }
}