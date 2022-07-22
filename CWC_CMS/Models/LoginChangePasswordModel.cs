using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;


namespace CWC_CMS.Models
{
    public class LoginChangePasswordModel
    {
        [Required(ErrorMessage = "Enter your user name")]
        [Display(Name = "Enter your user name")]

        public string User_name { get; set; }


        [Required(ErrorMessage = "Enter current password")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Enter your new password")]
        [DataType(DataType.Password)]
        //[RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Passwords must be at least 7 but Upto 15 characters and contain Following:upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]   //"Passwords must be at least 7 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter your confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}