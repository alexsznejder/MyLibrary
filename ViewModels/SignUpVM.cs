using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLibrary.ViewModels
{
    public class SignUpVM
    {
        [Key]
        public int userId { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Required field!")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required field!")]
        public string password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required field!")]
        public string confirmedpassword { get; set; }
    }
}