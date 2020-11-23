using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace team5_centric.Models
{

    public class userData
    {
        [Key]
        public Guid userId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is a required field")]
        [StringLength(20)]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is a required field")]
        [StringLength(20)]
        public string lastName { get; set; }

        [Display(Name = "Email Address")]
        //[Required(ErrorMessage = "Email Address is a required field")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        public string email { get; set; }

        [Display(Name = "Full Name")]
        public string fullName 
        {
            get
            {
                return lastName + ", " + firstName;
            }
        }
        [Display(Name = "Full Name Normal")]
        public string fullNameNormal
        {
            get
            {
                return firstName + " " + lastName ;
            }
        }

        [Display(Name = "Office Location")]
        [Required(ErrorMessage = "Office Location is a required field")]
        [StringLength(20)]
        public string officeLocation { get; set; }
       

        [Display(Name = "Position at Centic")]
        [Required(ErrorMessage = "Position at Centric is a required field")]
        [StringLength(20)]
        public string position { get; set; }

        [Display(Name = "Starting Date")]
        [Required(ErrorMessage = "Starting Date is a required field")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startDate { get; set; }
        public string avatar { get; set; }
        public ICollection<recognition> recognitions { get; set; }
        

        
    }
    public enum office
    {
        Male,
        Female
    }

   

}