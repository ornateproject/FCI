using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class DocumentUploadForCMS
    {
        public string DocumentName { get; set; }
        public int DocumentID { get; set; }
        public string DocumentLink { get; set; }
        public DateTime CreatedDate { get; set; }
        public HttpPostedFileBase Document { get; set; }


        public List<DocumentUploadForCMS> GetDocumentUploadForCMSList()
        {
            List<DocumentUploadForCMS> _DocumentUploadForCMSList = new List<DocumentUploadForCMS>();
            SqlHelper osqlhelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@DocumentID", 0);
            DataSet ds = new DataSet();
            ds = osqlhelper.ExecuteProcudere("PROC_GET_DOCUMENT_UPLOAD_INDEX_AND_GET_BY_ID", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable ResultTable = ds.Tables[0];
                    if (ResultTable != null)
                    {
                        if (ResultTable.Rows.Count > 0)
                        {
                            for (int i = 0; i < ResultTable.Rows.Count; i++)
                            {
                                _DocumentUploadForCMSList.Add(
                new DocumentUploadForCMS
                {
                    DocumentName = ResultTable.Rows[i]["DocumentName"].ToString(),
                    DocumentLink = ResultTable.Rows[i]["DocumentLink"].ToString(),
                    DocumentID = (ResultTable.Rows[i]["DocumentID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["DocumentID"])),
                    CreatedDate = (ResultTable.Rows[i]["CreatedDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["CreatedDate"]))
                });
                            }

                            return _DocumentUploadForCMSList;
                        }


                    }
                }
            }

            return null;

        }


        public DocumentUploadForCMS() {


        }


        public DocumentUploadForCMS(int DocumentID)
        {
            SqlHelper osqlhelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@DocumentID", DocumentID);
            DataSet ds = new DataSet();
            ds = osqlhelper.ExecuteProcudere("PROC_GET_DOCUMENT_UPLOAD_INDEX_AND_GET_BY_ID", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable ResultTable = ds.Tables[0];
                    CreatedDate = (ResultTable.Rows[0]["CreatedDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["CreatedDate"]));
                    DocumentID = (ResultTable.Rows[0]["DocumentID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["DocumentID"]));
                    DocumentName = ResultTable.Rows[0]["DocumentName"].ToString();
                    DocumentLink = ResultTable.Rows[0]["DocumentLink"].ToString();

                }
            }
        }



        public int SaveUpdate(string Case)
        {
            SqlHelper osqlhelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@DocumentName", DocumentName);
            ht.Add("@DocumentLink", DocumentLink);
            ht.Add("@DocumentID", DocumentID);
            ht.Add("@CreatedDate", CreatedDate);
            ht.Add("@Case", Case);
            int result = osqlhelper.ExecuteQuery("PROC_INSERT_UPDATE_DOCUMENT_MASTER", ht);
            return result;
        }

    }

}