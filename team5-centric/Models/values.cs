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
        public string valueName { get; set; }
        public string valueDescription { get; set; }
        public byte[] valueIcon { get; set; }
        public int valueWeight { get; set; }
        public ICollection<recognition> recognitions { get; set; }
    }
}