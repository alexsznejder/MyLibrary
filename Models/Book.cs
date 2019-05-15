using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLibrary.Models
{
    public class Book
    {
        [Key]
        public int bookId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter the title.")]
        public string title { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "Please enter the author.")]
        public string author { get; set; }

        [Display(Name = "Year")]
        public int year { get; set; }

        [Display(Name = "Read")]
        [DefaultValue(false)]
        public bool read { get; set; }

        [Display(Name = "Like")]
        [DefaultValue(false)]
        public bool like { get; set; }

        public int userId { get; set; }
    }
}