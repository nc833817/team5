using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace team5_centric.Models
{
    public class recognition
    {
        [Key]
        public Guid recId { get; set; }
        public Guid userId { get; set; }
        public userData userData { get; set; }
        public Guid valueId { get; set; }
        public recognition recognitions { get; set; }
    }
}