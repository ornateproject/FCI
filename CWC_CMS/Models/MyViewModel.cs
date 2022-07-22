using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class MyViewModel
    {
        [Required]
        public HttpPostedFileBase MyExcelFile { get; set; }

        public string MSExcelTable { get; set; }

        [Required]
        public string ExamName { get; set; }

        [Required(ErrorMessage = "Please select Image File.")]
        public HttpPostedFileBase[] ResultImageFiles { get; set; }
    }


    public class OfferLetter
    {
        public List<string> ColumnName { get; set; }

        public List<string> ColumnValue { get; set; }
    }
}