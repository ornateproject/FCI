using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class RTIApplicationFormModel
    {

        [Required(ErrorMessage = "Applicant Name is required")]
        public string ApplicantName { get; set; }

        [Required(ErrorMessage = "TelePhone Number is required")]
        public string TelephoneNo { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Fax No is required")]
        public string FaxNo { get; set; }

        [Required(ErrorMessage = "Email ID is required")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Complaint Box Can't Be Left Empty")]
        public string Complaints { get; set; }

       
        



        public RTIApplicationFormModel()
        {

        }


        public RTIApplicationFormModel(string ApplicationRefnoParam)
        {
            DataTable ResultTable = new DataTable();
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationRefno", ApplicationRefnoParam);

            DataSet ds = new DataSet();
            ds = osqlHelper.ExecuteProcudere("PROC_GET_RTI_DETAILS_FOR_INDEX_AND_GET_BY_ID", ht);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ResultTable = ds.Tables[0];
                        if (ResultTable != null)
                        {
                            if (ResultTable.Rows.Count > 0)
                            {
                                ApplicantName = ResultTable.Rows[0]["ApplicantName"].ToString();
                                TelephoneNo = ResultTable.Rows[0]["TelephoneNo"].ToString();
                                MobileNo = ResultTable.Rows[0]["MobileNo"].ToString();
                                FaxNo = ResultTable.Rows[0]["FaxNo"].ToString();
                                EmailID = ResultTable.Rows[0]["EmailID"].ToString();
                                Address = ResultTable.Rows[0]["Address"].ToString();
                                Complaints = ResultTable.Rows[0]["Complaints"].ToString();

                            }
                        }
                    }

                }
            }
        }
        public List<RTIApplicationFormModel> GetRTIListForIndex()
        {
            DataTable ResultTable = new DataTable();
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationRefno", "NA");
            DataSet ds = new DataSet();
            List<RTIApplicationFormModel> RTIApplicationFormModelList = new List<RTIApplicationFormModel>();
            ds = osqlHelper.ExecuteProcudere("PROC_GET_RTI_DETAILS_FOR_INDEX_AND_GET_BY_ID", ht);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ResultTable = ds.Tables[0];
                        if (ResultTable != null)
                        {
                            if (ResultTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < ResultTable.Rows.Count; i++)
                                {
                                    RTIApplicationFormModelList.Add(new RTIApplicationFormModel
                                    {
                                        ApplicantName = ResultTable.Rows[i]["ApplicantName"].ToString(),
                                        TelephoneNo = ResultTable.Rows[i]["TelephoneNo"].ToString(),
                                        MobileNo = ResultTable.Rows[i]["MobileNo"].ToString(),
                                        FaxNo = ResultTable.Rows[i]["FaxNo"].ToString(),
                                        EmailID = ResultTable.Rows[i]["EmailID"].ToString(),
                                        Address = ResultTable.Rows[i]["Address"].ToString(),
                                        Complaints = ResultTable.Rows[i]["Complaints"].ToString()

                                    });
                                }

                                return RTIApplicationFormModelList;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public string SaveUpdateDelete(string Case)
        {
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicantName", ApplicantName);
            ht.Add("@TelephoneNo", TelephoneNo);
            ht.Add("@MobileNo", MobileNo);
            ht.Add("@FaxNo", FaxNo);
            ht.Add("@EmailID", EmailID);
            ht.Add("@Address", Address);
            ht.Add("@Complaints", Complaints);
           
            
            ht.Add("@ApplicationRefno_out", "");

            string returnbyproc = osqlHelper.ExecuteQueryWithOutParamINString("PROC_INSERT_UPDATE_RTI_DETAILS", ht);
            return returnbyproc;
        }
    }
}