using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Drawing;
using System.Linq;
using System.Web;

namespace team5_centric.Models
{
    public class userData
    {
        [Key]
        public Guid userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string fullName 
        {
            get
            {
                return lastName + ", " + firstName;
            }
        }
        public string officeLocation { get; set; }
        public string position { get; set; }
        public DateTime startDate { get; set; }
        public byte[] avatar { get; set; }
        public ICollection<recognition> recognitions { get; set; }
    }
}