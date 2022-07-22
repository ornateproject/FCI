using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage ="Login Name is Required")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Selection is Required")]
        public string ChangePasswordMethod { get; set; }
    }
}