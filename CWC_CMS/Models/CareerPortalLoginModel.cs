using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class CareerPortalLoginModel
    {

        [Required]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "UserID must be numeric")]
        public string RegistrationNo { get; set; }
        [Required]
        public string RollNo { get; set; }

        [Required]
        public DateTime DOB { get; set; }
        

        [Required]
        public string LoginUsingRollNoOrRegNo { get; set; }


    }
}