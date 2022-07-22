using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(12)]
        [MinLength(1)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "UserID must be numeric")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string EmailID { get; set; }
        //public LoginModel()
        //{
        //    UserName = "NA";
        //}

        
    }
   
}