using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class CWCTenderModel
    {
        public int SrNo { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string TenderDownloadLink { get; set; }
        public DateTime LastDateOfSubmission { get; set; }

        public List<CWCTenderModel> GetTenderDataForIndex()
        {
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();

            return null;
        }
    }


    public class DocumentAttachment
    {
        public string DocumentName { get; set; }
        public HttpPostedFileBase Document{ get; set;}
    }
}