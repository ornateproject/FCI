using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class CWCTenders
    {
        public string TenderReferenceNo { get; set; }
        public int TenderType { get; set; }
        public int ContractForm { get; set; }
        public int NoOfCover { get; set; }
        public int TenderCategory { get; set; }
        public string DocumentDescription { get; set; }
    }
}