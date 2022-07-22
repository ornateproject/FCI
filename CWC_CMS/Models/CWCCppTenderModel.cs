using SIDCUL.Areas.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Models
{
    public class CWCCppTenderModel
    {
        public int? TenderID { get; set; }
        [Required(ErrorMessage = "Tender Reference No is required")]
        public string TenderReferenceNo { get; set; }
        [Required(ErrorMessage = "Tender Type is required")]
        public int TenderType { get; set; }
        [Required(ErrorMessage = "State is required")]
        public int StateID { get; set; }
        [Required(ErrorMessage = "Form Of Contract is required")]
        public int FormOfContract { get; set; }
        [Required(ErrorMessage = "No Of Cover is required")]
        public int NoOfCover { get; set; }
        [Required(ErrorMessage = "Tender Category is required")]
        public int TenderCategory { get; set; }

        public List<DocumentCover> DocumentCoverList { get; set; }
        public List<TenderDocuments> TenderDocumentsList { get; set; }
        public List<WorkItemDocuments> WorkItemDocumentsList { get; set; }
        [Required(ErrorMessage = "Work Or Item Title is required")]
        public string WorkOrItemTitle { get; set; }
        [Required(ErrorMessage = "Location Detail is required")]
        public string LocationDetail { get; set; }
        [Required(ErrorMessage = "Work Or Item Description is required")]
        public string WorkOrItemDescription { get; set; }
        [Required(ErrorMessage = "Pincode is required")]
        public string Pincode { get; set; }
        [Required(ErrorMessage = "Has Pre Bid Meeting is required")]
        public string HasPreBidMeeting { get; set; }
        [Required(ErrorMessage = "Pre BidMeeting Place is required")]
        public string PreBidMeetingPlace { get; set; }
        [Required(ErrorMessage = "Pre BidMeeting Address is required")]
        public string PreBidMeetingAddress { get; set; }

        public string PreQualificationDetails { get; set; }
        [Required(ErrorMessage = "Product Category is required")]
        public int ProductCategory { get; set; }
        public string ProductSubCategory { get; set; }
        [Required(ErrorMessage = "Bid Opening Place is required")]
        public string BidOpeningPlace { get; set; }
        [Required(ErrorMessage = "Contract Type is required")]
        public int ContractType { get; set; }
        [Required(ErrorMessage = "Tenderer Class is required")]
        public int TendererClass { get; set; }
        [Required(ErrorMessage = "Tender Currency is required")]
        public int TenderCurrency { get; set; }
        [Required(ErrorMessage = "Tender Value is required")]
        public decimal TenderValue { get; set; }
        [Required(ErrorMessage = "Inviting Officer is required")]
        public string InvitingOfficer { get; set; }
        [Required(ErrorMessage = "Inviting Officer Address is required")]
        public string InvitingOfficerAddress { get; set; }
        [Required(ErrorMessage = "Delievery Period is required")]
        public int DelieveryPeriod { get; set; }
        [Required(ErrorMessage = "Bid Validity Days is required")]
        public int BidValidityDays { get; set; }

        public int BidValidityDaysInCaseOfOther { get; set; }
        [Required(ErrorMessage = "Fee Payment Mode is required")]
        public string FeePaymentMode { get; set; }
        [Required(ErrorMessage = "Tender Fee is required")]
        public decimal TenderFee { get; set; }
        [Required(ErrorMessage = "Please Select If Exemption is Allowed or Not")]
        public string IsExemptionAllowed { get; set; }
        [Required(ErrorMessage = "Please Select If EMD is Fixed or Percentage")]
        public string IsEMDFeeFixedOrPercentage { get; set; }
        [Required(ErrorMessage = "Emd Amount is required")]
        public decimal EmdAmount { get; set; }
        [Required(ErrorMessage = "Emd ECV Percentage is required")]
        public decimal EmdECVPercentage { get; set; }
        [Required(ErrorMessage = "Please Select If EMD Exception is Allowed or Not")]
        public string IsEmdExceptionAllowed { get; set; }
        [Required(ErrorMessage = "EMD Fee Payable To is required")]
        public string EMDFeePayableTo { get; set; }
        [Required(ErrorMessage = "EMD Fee Payable At is required")]
        public string EMDFeePayableAt { get; set; }
        [Required(ErrorMessage = "Tender Fee Payable To is required")]
        public string TenderFeePayableTo { get; set; }
        [Required(ErrorMessage = "Tender Fee Payable At is required")]
        public string TenderFeePayableAt { get; set; }
        [Required(ErrorMessage = "Offline Instruments is required")]
        public List<int> SelectedOfflineInstruments { get; set; }
        [Required(ErrorMessage = "Publishing Date is required")]
        public DateTime PublishingDate { get; set; }
        [Required(ErrorMessage = "Document Download/Sale Start Date is required")]
        public DateTime DocumentDownloadOrSaleStartDate { get; set; }
        public DateTime SeekClarificationStartDate { get; set; }
        public DateTime PreBidMeetingDate { get; set; }
        public DateTime SeekClarificationEndDate { get; set; }
        [Required(ErrorMessage = "Bid Submission Start Date is required")]
        public DateTime BidSubmissionStartDate { get; set; }
        [Required(ErrorMessage = "Bid Submission Closing Date is required")]
        public DateTime BidSubmissionClosingDate { get; set; }
        [Required(ErrorMessage = "Bid Opening Date is required")]
        public DateTime BidOpeningDate { get; set; }


        public void ConnectionXML()
        {

            string XMLFilePath = HttpContext.Current.Server.MapPath("~/FileTest/Testdo.xml");
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_GET_TENDER_XML", spmLogin, "");
            string backslash = @"\";

            //for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            //{
            //    string T_DOC_START_DATE = (ds.Tables[0].Rows[j]["T_DOC_START_DATE"].ToString());
            //    string T_DOC_END_DATE = (ds.Tables[0].Rows[j]["T_DOC_END_DATE"].ToString());

            //    if(T_DOC_START_DATE == "01/01/1753 00:00:00")
            //    {
            //        T_DOC_START_DATE = "";
            //        ds.Tables[0].Rows[j]["T_DOC_START_DATE"] = string.Empty;
            //    }
            //    else
            //    {
            //        ds.Tables[0].Rows[j]["T_DOC_START_DATE"] = T_DOC_START_DATE.ToString();
            //    }
            //    if (T_DOC_END_DATE == "01/01/1753 00:00:00")
            //    {
            //        T_DOC_END_DATE = "";
            //        ds.Tables[0].Rows[j]["T_DOC_END_DATE"] = T_DOC_END_DATE.ToString();
            //    }
            //    else
            //    {
            //        ds.Tables[0].Rows[j]["T_DOC_END_DATE"] = T_DOC_END_DATE.ToString();
            //    }
            //}
                if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    DateTime T_DOC_START_DATE = Convert.ToDateTime(ds.Tables[0].Rows[j]["T_DOC_START_DATE"].ToString());
                    string T_DOC_END_DATE = (ds.Tables[0].Rows[j]["T_DOC_END_DATE"].ToString());
                    string T_RETURN_URL = ds.Tables[0].Rows[j]["T_RETURN_URL"].ToString();
                    #region Renu  2 Feb 2020 To change document path
                    //To Replace string start
                    if (T_RETURN_URL != string.Empty)
                    {
                        //string backslash = @"\";
                        string Item;
                        string ItemList;
                        int DelimIndex = 0;
                        ItemList = T_RETURN_URL;

                        DelimIndex = ItemList.IndexOf(backslash);

                        string FinalString = "";
                        int i = 0;
                        while (DelimIndex > 0)
                        {


                            Item = ItemList.Substring(0, DelimIndex);


                            if (i > 2)
                            {
                                if (i == 2)
                                {
                                    FinalString = FinalString + Item;
                                }
                                else
                                {
                                    FinalString = FinalString + '/' + Item;
                                }
                            }

                            i = i + 1;
                            int INDELI = (DelimIndex + 1);
                            int len = (ItemList.Length);
                            int increlent = ((ItemList.Length) - DelimIndex);
                            ItemList = ItemList.Substring(INDELI, increlent - 1);
                            DelimIndex = ItemList.IndexOf(backslash, 0);

                        }
                        FinalString = "http://49.50.87.108:8080" + FinalString + "/" + ItemList;

                        T_RETURN_URL = FinalString.ToString();
                    }

                    //end
                    #endregion Renu  2 Feb 2020 To change document path

                    #region Renu  2 Feb 2020 To change DOC Date


                    #endregion Renu  2 Feb 2020 To change DOC Date
                    ds.Tables[0].Rows[j]["T_RETURN_URL"] = T_RETURN_URL.ToString();

                }
            }

            #endregion

            DataTable ResultTable = new DataTable();
            ResultTable = ds.Tables[0];
            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //DataSet ds = new DataSet();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_TENDER_XML", ht);

            //RemoveTimezoneForDataSet(ds);
            // Apply the WriteXml method to write an XML document
            if (System.IO.File.Exists(XMLFilePath))
            {
                System.IO.File.Delete(XMLFilePath);
            }
            // Get a FileStream object
            StreamWriter xmlDoc = new StreamWriter(XMLFilePath, false);
            ds.WriteXml(xmlDoc);
            xmlDoc.Close();
            string content = System.IO.File.ReadAllText(XMLFilePath);
            content = content.Replace("Table>", "TENDERS>");
            content = content.Replace("NewDataSet>", "CPPPTENDER>");
            content = content.Replace("<TENDERS>", "<TENDERS><XML_USER_ID>PS10185</XML_USER_ID>");
            content = "<?xml version = '1.0' encoding = 'UTF-8'?>" + content;
            System.IO.File.Delete(XMLFilePath);
            FileStream fs = null;
            if (!System.IO.File.Exists(XMLFilePath))
            {
                using (fs = System.IO.File.Create(XMLFilePath))
                {

                }
            }


            if (System.IO.File.Exists(XMLFilePath))
            {
                using (StreamWriter sw = new StreamWriter(XMLFilePath))
                {
                    sw.Write(content);
                }
            }


        }


        public static void RemoveTimezoneForDataSet(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {

                    if (dc.DataType == typeof(DateTime))
                    {
                        dc.DataType = typeof(string);
                    }
                }
            }
        }
        public CWCCppTenderModel(int TenderID)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_TenderID", TenderID)
                                        };
            ds = sql.getDataSet("PROC_GET_TENDER_DETAILS_INDEX_AND_GET_BY_ID", spmLogin, "");


            DataTable ResultTable = new DataTable();
            if (ds != null)
                ResultTable = ds.Tables[0];
            else
                ResultTable = null;

            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //ht.Add("@TenderID", TenderID);
            //DataSet ds = new DataSet();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_TENDER_DETAILS_INDEX_AND_GET_BY_ID", ht);
            #endregion
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

                                TenderID = (ResultTable.Rows[0]["TenderID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["TenderID"]));
                                PublishingDate = (ResultTable.Rows[0]["PublishingDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["PublishingDate"]));
                                DocumentDownloadOrSaleStartDate = (ResultTable.Rows[0]["DocumentDownloadOrSaleStartDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["DocumentDownloadOrSaleStartDate"]));
                                SeekClarificationStartDate = (ResultTable.Rows[0]["SeekClarificationStartDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["SeekClarificationStartDate"]));
                                SeekClarificationEndDate = (ResultTable.Rows[0]["SeekClarificationEndDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["SeekClarificationEndDate"]));
                                BidSubmissionStartDate = (ResultTable.Rows[0]["BidSubmissionStartDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["BidSubmissionStartDate"]));
                                BidSubmissionClosingDate = (ResultTable.Rows[0]["BidSubmissionClosingDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["BidSubmissionClosingDate"]));
                                PreBidMeetingDate = (ResultTable.Rows[0]["PreBidMeetingDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["PreBidMeetingDate"]));
                                BidOpeningDate = (ResultTable.Rows[0]["BidOpeningDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["BidOpeningDate"]));
                                TenderValue = (ResultTable.Rows[0]["TenderValue"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[0]["TenderValue"]));
                                TenderFee = (ResultTable.Rows[0]["TenderFee"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[0]["TenderFee"]));
                                EmdAmount = (ResultTable.Rows[0]["EmdAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[0]["EmdAmount"]));
                                EmdECVPercentage = (ResultTable.Rows[0]["EmdECVPercentage"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[0]["EmdECVPercentage"]));
                                TenderType = (ResultTable.Rows[0]["TenderType"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["TenderType"]));
                                DelieveryPeriod = (ResultTable.Rows[0]["DelieveryPeriod"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["DelieveryPeriod"]));
                                FormOfContract = (ResultTable.Rows[0]["FormOfContract"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["FormOfContract"]));
                                NoOfCover = (ResultTable.Rows[0]["NoOfCover"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["NoOfCover"]));
                                TenderCategory = (ResultTable.Rows[0]["TenderCategory"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["TenderCategory"]));
                                ProductCategory = (ResultTable.Rows[0]["ProductCategory"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["ProductCategory"]));
                                ContractType = (ResultTable.Rows[0]["ContractType"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["ContractType"]));
                                TendererClass = (ResultTable.Rows[0]["TendererClass"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["TendererClass"]));
                                TenderCurrency = (ResultTable.Rows[0]["TenderCurrency"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["TenderCurrency"]));
                                BidValidityDays = (ResultTable.Rows[0]["BidValidityDays"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["BidValidityDays"]));
                                StateID = (ResultTable.Rows[0]["StateID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["StateID"]));
                                TenderReferenceNo = ResultTable.Rows[0]["TenderReferenceNo"].ToString();
                                WorkOrItemTitle = ResultTable.Rows[0]["WorkOrItemTitle"].ToString();
                                LocationDetail = ResultTable.Rows[0]["LocationDetail"].ToString();
                                WorkOrItemDescription = ResultTable.Rows[0]["WorkOrItemDescription"].ToString();
                                Pincode = ResultTable.Rows[0]["Pincode"].ToString();
                                HasPreBidMeeting = ResultTable.Rows[0]["HasPreBidMeeting"].ToString();
                                PreBidMeetingPlace = ResultTable.Rows[0]["PreBidMeetingPlace"].ToString();
                                PreBidMeetingAddress = ResultTable.Rows[0]["PreBidMeetingAddress"].ToString();
                                PreQualificationDetails = ResultTable.Rows[0]["PreQualificationDetails"].ToString();
                                ProductSubCategory = ResultTable.Rows[0]["ProductSubCategory"].ToString();
                                BidOpeningPlace = ResultTable.Rows[0]["BidOpeningPlace"].ToString();
                                InvitingOfficer = ResultTable.Rows[0]["InvitingOfficer"].ToString();
                                InvitingOfficerAddress = ResultTable.Rows[0]["InvitingOfficerAddress"].ToString();
                                FeePaymentMode = ResultTable.Rows[0]["FeePaymentMode"].ToString();
                                IsExemptionAllowed = ResultTable.Rows[0]["IsExemptionAllowed"].ToString();
                                IsEMDFeeFixedOrPercentage = ResultTable.Rows[0]["IsEMDFeeFixedOrPercentage"].ToString();
                                IsEmdExceptionAllowed = ResultTable.Rows[0]["IsEmdExceptionAllowed"].ToString();
                                EMDFeePayableTo = ResultTable.Rows[0]["EMDFeePayableTo"].ToString();
                                EMDFeePayableAt = ResultTable.Rows[0]["EMDFeePayableAt"].ToString();
                                TenderFeePayableTo = ResultTable.Rows[0]["TenderFeePayableTo"].ToString();
                                TenderFeePayableAt = ResultTable.Rows[0]["TenderFeePayableAt"].ToString();

                            }
                        }
                    }
                }


                if (ds.Tables[1] != null)
                {
                    DocumentCoverList = new List<DocumentCover>();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataTable ResultTable1 = new DataTable();
                        ResultTable1 = ds.Tables[1];
                        for (int i = 0; i < ResultTable1.Rows.Count; i++)
                        {
                            DocumentCoverList.Add(new DocumentCover
                            {
                                CoverType = ResultTable1.Rows[i]["CoverType"].ToString(),
                                DocumentCoverName = ResultTable1.Rows[i]["CoverName"].ToString(),
                                DocumentDescriptionOfCover = ResultTable1.Rows[i]["DocumentDescription"].ToString(),
                                DocumentTypeOfCover = ResultTable1.Rows[i]["DocumentType"].ToString()
                            });
                        }
                    }
                    else
                    {
                        DocumentCoverList = null;
                    }
                }


                if (ds.Tables[2] != null)
                {
                    TenderDocumentsList = new List<TenderDocuments>();
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        DataTable ResultTable1 = new DataTable();
                        ResultTable1 = ds.Tables[2];
                        for (int i = 0; i < ResultTable1.Rows.Count; i++)
                        {
                            TenderDocumentsList.Add(new TenderDocuments
                            {
                                DocumentypeOfTender = ResultTable1.Rows[i]["Documentype"].ToString(),
                                DocumentDescriptionOfTender = ResultTable1.Rows[i]["DocumentDescription"].ToString(),
                                UploadedDateOfTender = ResultTable1.Rows[i]["UploadedDateOfTender"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable1.Rows[i]["UploadedDateOfTender"]),
                                TenderDocumentPath = ResultTable1.Rows[i]["TenderDocumentPath"].ToString(),
                                VerifiedBy = ResultTable1.Rows[i]["VerifiedBy"].ToString()
                            });

                        }
                    }
                    else
                    {
                        TenderDocumentsList = null;
                    }
                }

                if (ds.Tables[3] != null)
                {
                    WorkItemDocumentsList = new List<WorkItemDocuments>();
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        DataTable ResultTable1 = new DataTable();
                        ResultTable1 = ds.Tables[3];
                        for (int i = 0; i < ResultTable1.Rows.Count; i++)
                        {
                            WorkItemDocumentsList.Add(new WorkItemDocuments
                            {
                                Documentype = ResultTable1.Rows[i]["Documentype"].ToString(),
                                DocumentDescription = ResultTable1.Rows[i]["DocumentDescription"].ToString(),
                                UploadedDate = ResultTable1.Rows[i]["UploadedDateOfTender"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable1.Rows[i]["UploadedDateOfTender"]),
                                WorkItemDocumentPath = ResultTable1.Rows[i]["TenderDocumentPath"].ToString()
                            });
                        }
                    }
                }


                if (ds.Tables[4] != null)
                {
                    SelectedOfflineInstruments = new List<int>();
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        DataTable ResultTable1 = new DataTable();
                        ResultTable1 = ds.Tables[4];
                        SelectedOfflineInstruments = (from row in ResultTable1.AsEnumerable() select Convert.ToInt32(row["OfflineInstrumentID"])).ToList();
                    }
                    else
                    {
                        SelectedOfflineInstruments = null;
                    }
                }
            }



        }


        public CWCCppTenderModel()
        {

        }

        public List<CWCCppTenderModel> GetCWCTenderListForIndex()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            int tenderRec = 0;
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_TenderID", tenderRec)
                                        };
            ds = sql.getDataSet("PROC_GET_TENDER_DETAILS_INDEX_AND_GET_BY_ID", spmLogin, "");


            DataTable ResultTable = new DataTable();
            if (ds != null)
                ResultTable = ds.Tables[0];
            else
                ResultTable = null;
            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //ht.Add("@TenderID", 0);
            //ResultTable = osqlHelper.ExecuteProcudereReturnDataTable("PROC_GET_TENDER_DETAILS_INDEX_AND_GET_BY_ID", ht);
            #endregion
            List<CWCCppTenderModel> CWCCppTenderModelList = new List<CWCCppTenderModel>();
            if (ResultTable != null)
            {
                if (ResultTable.Rows.Count > 0)
                {
                    for (int i = 0; i < ResultTable.Rows.Count; i++)
                    {
                        CWCCppTenderModelList.Add(new CWCCppTenderModel
                        {
                            TenderID = (ResultTable.Rows[i]["TenderID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["TenderID"])),
                            PublishingDate = (ResultTable.Rows[i]["PublishingDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["PublishingDate"])),
                            DocumentDownloadOrSaleStartDate = (ResultTable.Rows[i]["DocumentDownloadOrSaleStartDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["DocumentDownloadOrSaleStartDate"])),
                            SeekClarificationStartDate = (ResultTable.Rows[i]["SeekClarificationStartDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["SeekClarificationStartDate"])),
                            SeekClarificationEndDate = (ResultTable.Rows[i]["SeekClarificationEndDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["SeekClarificationEndDate"])),
                            BidSubmissionStartDate = (ResultTable.Rows[i]["BidSubmissionStartDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["BidSubmissionStartDate"])),
                            BidSubmissionClosingDate = (ResultTable.Rows[i]["BidSubmissionClosingDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["BidSubmissionClosingDate"])),
                            PreBidMeetingDate = (ResultTable.Rows[i]["PreBidMeetingDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["PreBidMeetingDate"])),
                            BidOpeningDate = (ResultTable.Rows[i]["BidOpeningDate"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["BidOpeningDate"])),
                            TenderValue = (ResultTable.Rows[i]["TenderValue"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[i]["TenderValue"])),
                            TenderFee = (ResultTable.Rows[i]["TenderFee"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[i]["TenderFee"])),
                            EmdAmount = (ResultTable.Rows[i]["EmdAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[i]["EmdAmount"])),
                            EmdECVPercentage = (ResultTable.Rows[i]["EmdECVPercentage"] == DBNull.Value ? 0 : Convert.ToDecimal(ResultTable.Rows[i]["EmdECVPercentage"])),
                            TenderType = (ResultTable.Rows[i]["TenderType"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["TenderType"])),
                            FormOfContract = (ResultTable.Rows[i]["FormOfContract"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["FormOfContract"])),
                            NoOfCover = (ResultTable.Rows[i]["NoOfCover"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["NoOfCover"])),
                            TenderCategory = (ResultTable.Rows[i]["TenderCategory"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["TenderCategory"])),
                            StateID = (ResultTable.Rows[i]["StateID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["StateID"])),
                            TenderReferenceNo = ResultTable.Rows[i]["TenderReferenceNo"].ToString(),
                            WorkOrItemTitle = ResultTable.Rows[i]["WorkOrItemTitle"].ToString(),
                            LocationDetail = ResultTable.Rows[i]["LocationDetail"].ToString(),
                            WorkOrItemDescription = ResultTable.Rows[i]["WorkOrItemDescription"].ToString(),
                            Pincode = ResultTable.Rows[i]["Pincode"].ToString(),
                            HasPreBidMeeting = ResultTable.Rows[i]["HasPreBidMeeting"].ToString(),
                            PreBidMeetingPlace = ResultTable.Rows[i]["PreBidMeetingPlace"].ToString(),
                            PreBidMeetingAddress = ResultTable.Rows[i]["PreBidMeetingAddress"].ToString(),
                            PreQualificationDetails = ResultTable.Rows[i]["PreQualificationDetails"].ToString(),
                            ProductSubCategory = ResultTable.Rows[i]["ProductSubCategory"].ToString(),
                            BidOpeningPlace = ResultTable.Rows[i]["BidOpeningPlace"].ToString(),
                            InvitingOfficer = ResultTable.Rows[i]["InvitingOfficer"].ToString(),
                            InvitingOfficerAddress = ResultTable.Rows[i]["InvitingOfficerAddress"].ToString(),
                            FeePaymentMode = ResultTable.Rows[i]["FeePaymentMode"].ToString(),
                            IsExemptionAllowed = ResultTable.Rows[i]["IsExemptionAllowed"].ToString(),
                            IsEMDFeeFixedOrPercentage = ResultTable.Rows[i]["IsEMDFeeFixedOrPercentage"].ToString(),
                            IsEmdExceptionAllowed = ResultTable.Rows[i]["IsEmdExceptionAllowed"].ToString(),
                            EMDFeePayableTo = ResultTable.Rows[i]["EMDFeePayableTo"].ToString(),
                            EMDFeePayableAt = ResultTable.Rows[i]["EMDFeePayableAt"].ToString()
                        });


                    }
                    return CWCCppTenderModelList;
                }
            }
            return null;
        }


        public int SaveUpdate(string Case, int TenderIDParam)
        {
            int Result = 0;

            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            int ComplaintTableIdentity = Master.GetCurrentIdentityOfComplaintTable();
            int TenderTableCurrentIdentity = Master.GetCurrentIdentityOfTenderTable();
            //int TenderTableCurrentIdentity = Master.GetCurrentIdentityOfTenderTable();


            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("p_TenderReferenceNo", TenderReferenceNo),
                                            new MySqlParameter("p_TenderType", TenderType),
                                            new MySqlParameter("p_FormOfContract", FormOfContract),
                                            new MySqlParameter("p_NoOfCover", NoOfCover),
                                            new MySqlParameter("p_TenderCategory", TenderCategory),
                                            new MySqlParameter("p_WorkOrItemTitle", WorkOrItemTitle),
                                            new MySqlParameter("p_LocationDetail", LocationDetail),
                                            new MySqlParameter("p_WorkOrItemDescription", WorkOrItemDescription),
                                            new MySqlParameter("p_Pincode", Pincode),
                                            new MySqlParameter("p_HasPreBidMeeting", HasPreBidMeeting),
                                            new MySqlParameter("p_PreBidMeetingPlace", (PreBidMeetingPlace == null ? "NA" : PreBidMeetingPlace)),
                                            new MySqlParameter("p_PreBidMeetingAddress", (PreBidMeetingAddress == null ? "NA" : PreBidMeetingAddress)),
                                            new MySqlParameter("p_PreQualificationDetails", PreQualificationDetails),
                                            new MySqlParameter("p_ProductCategory", ProductCategory),
                                            new MySqlParameter("p_ProductSubCategory", ProductSubCategory),
                                            new MySqlParameter("p_BidOpeningPlace", BidOpeningPlace),
                                            new MySqlParameter("p_StateID", StateID),
                                            new MySqlParameter("p_ContractType", ContractType),
                                            new MySqlParameter("p_TendererClass", TendererClass),
                                            new MySqlParameter("p_TenderCurrency", TenderCurrency),
                                            new MySqlParameter("p_TenderValue", TenderValue),
                                            new MySqlParameter("p_InvitingOfficer", InvitingOfficer),
                                            new MySqlParameter("p_InvitingOfficerAddress", InvitingOfficerAddress),
                                            new MySqlParameter("p_DelieveryPeriod", DelieveryPeriod),
                                            new MySqlParameter("p_BidValidityDays", BidValidityDays),
                                            new MySqlParameter("p_FeePaymentMode", FeePaymentMode),
                                            new MySqlParameter("p_TenderFee", TenderFee),
                                            new MySqlParameter("p_IsExemptionAllowed", IsExemptionAllowed),
                                            new MySqlParameter("p_IsEMDFeeFixedOrPercentage", IsEMDFeeFixedOrPercentage),
                                            new MySqlParameter("p_EmdAmount", EmdAmount),
                                            new MySqlParameter("p_EmdECVPercentage", EmdECVPercentage),
                                            new MySqlParameter("p_IsEmdExceptionAllowed", IsEmdExceptionAllowed),
                                            new MySqlParameter("p_EMDFeePayableTo", EMDFeePayableTo),
                                            new MySqlParameter("p_EMDFeePayableAt", EMDFeePayableAt),
                                            new MySqlParameter("p_TenderFeePayableTo", TenderFeePayableTo),
                                            new MySqlParameter("p_TenderFeePayableAt", TenderFeePayableAt),
                                            new MySqlParameter("p_PublishingDate", PublishingDate),
                                            new MySqlParameter("p_DocumentDownloadOrSaleStartDate", DocumentDownloadOrSaleStartDate),
                                            new MySqlParameter("p_SeekClarificationStartDate", (SeekClarificationStartDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : SeekClarificationStartDate)),
                                            new MySqlParameter("p_SeekClarificationEndDate", (SeekClarificationEndDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : SeekClarificationEndDate)),
                                            new MySqlParameter("p_PreBidMeetingDate", (PreBidMeetingDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : PreBidMeetingDate)),
                                            new MySqlParameter("p_BidSubmissionStartDate", BidSubmissionStartDate),
                                            new MySqlParameter("p_BidSubmissionClosingDate", BidSubmissionClosingDate),
                                            new MySqlParameter("p_BidOpeningDate", BidOpeningDate),
                                            new MySqlParameter("p_TenderID", TenderIDParam),
                                            new MySqlParameter("p_Case", Case),
                                        };
            int TenderidOut = sql.execStoredProcudure("PROC_SAVE_UPDATE_DELETE_TENDER", spmLogin);


            //DataTable For Cover Add
            if (TenderidOut > 0)
            {
                for (int i = 0; i < DocumentCoverList.Count; i++)
                {
                    SqlHelper sql1 = new SqlHelper();
                    MySqlParameter[] spmLogin1 = {
                                            new MySqlParameter("p_TenderID", TenderidOut),
                                            new MySqlParameter("p_CoverType", DocumentCoverList[i].CoverType),
                                            new MySqlParameter("p_CoverName", DocumentCoverList[i].DocumentCoverName),
                                            new MySqlParameter("p_DocumentDescription", DocumentCoverList[i].DocumentDescriptionOfCover),
                                            new MySqlParameter("p_DocumentType", DocumentCoverList[i].DocumentTypeOfCover),
                                            new MySqlParameter("p_Case", Case)
                                        };
                    Result = sql1.execStoredProcudure("PROC_SAVE_UPDATE_DELETE_TENDER_COVER_DETAILS", spmLogin1);

                }
            }
            // End DataTable For Cover Add

            //DataTable For Tender Document
            if (TenderidOut > 0)
            {
                for (int i = 0; i < TenderDocumentsList.Count; i++)
                {
                    if (TenderDocumentsList[i].TenderDocumentPath == null)
                    {
                        TenderDocumentsList[i].TenderDocumentPath = SWCS_Integration.Upload_Single_pdf_File_new(TenderDocumentsList[i].TenderDocument, TenderTableCurrentIdentity + 1, "Tender_Documents", i + 1);
                    }
                    SqlHelper sql2 = new SqlHelper();
                    MySqlParameter[] spmLogin2 = {
                                            new MySqlParameter("p_TenderID", TenderidOut),
                                            new MySqlParameter("p_Documentype", TenderDocumentsList[i].DocumentypeOfTender),
                                            new MySqlParameter("p_DocumentDescription", TenderDocumentsList[i].DocumentDescriptionOfTender),
                                            new MySqlParameter("p_UploadedDateOfTender", TenderDocumentsList[i].UploadedDateOfTender),
                                            new MySqlParameter("p_TenderDocumentPath", TenderDocumentsList[i].TenderDocumentPath),
                                            new MySqlParameter("p_VerifiedBy", TenderDocumentsList[i].VerifiedBy),
                                            new MySqlParameter("p_Case", Case)
                                        };
                    Result = sql2.execStoredProcudure("PROC_SAVE_UPDATE_DELETE_TENDER_DOCUMENT_DETAILS", spmLogin2);

                }
            }
            // End DataTable For Tender Document

            //DataTable For Work Document
            if (TenderidOut > 0)
            {
                for (int i = 0; i < WorkItemDocumentsList.Count; i++)
                {
                    if (WorkItemDocumentsList[i].WorkItemDocumentPath == null)
                    {
                        WorkItemDocumentsList[i].WorkItemDocumentPath = SWCS_Integration.Upload_Single_pdf_File_new(TenderDocumentsList[i].TenderDocument, TenderTableCurrentIdentity + 1, "Work_Documents", i + 1);

                    }
                    SqlHelper sql3 = new SqlHelper();
                    MySqlParameter[] spmLogin3 = {
                                            new MySqlParameter("p_TenderID", TenderidOut),
                                            new MySqlParameter("p_Documentype", WorkItemDocumentsList[i].Documentype),
                                            new MySqlParameter("p_DocumentDescription", WorkItemDocumentsList[i].DocumentDescription),
                                            new MySqlParameter("p_UploadedDateOfTender", WorkItemDocumentsList[i].UploadedDate),
                                            new MySqlParameter("p_TenderDocumentPath", WorkItemDocumentsList[i].WorkItemDocumentPath),
                                            new MySqlParameter("p_Case", Case)
                                        };
                    Result = sql3.execStoredProcudure("PROC_SAVE_UPDATE_DELETE_TENDER_WORK_DOCUMENT", spmLogin3);

                }
            }

            // End DataTable For Work Document

            // DataTable For Offline Instrument
            if (TenderidOut > 0)
            {
                for (int i = 0; i < SelectedOfflineInstruments.Count; i++)
                {
                    SqlHelper sql4 = new SqlHelper();
                    MySqlParameter[] spmLogin4 = {
                                            new MySqlParameter("p_TenderID", TenderidOut),
                                            new MySqlParameter("p_OfflineInstrumentID", SelectedOfflineInstruments[i]),
                                            new MySqlParameter("p_Case", Case),
                                        };
                    Result = sql4.execStoredProcudure("PROC_SAVE_UPDATE_DELETE_TENDER_OFFLINE_INSTRUMENT", spmLogin4);

                }
            }

            // END DataTable For Offline Instrument


            #endregion


            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();


            ////DataTable For Cover Add
            //DataTable CoverDetailsTable = new DataTable();
            //CoverDetailsTable.Columns.Add("CoverType", typeof(string));
            //CoverDetailsTable.Columns.Add("CoverName", typeof(string));
            //CoverDetailsTable.Columns.Add("DocumentDescription", typeof(string));
            //CoverDetailsTable.Columns.Add("DocumentType", typeof(string));
            //if (DocumentCoverList != null)
            //{
            //    for (int i = 0; i < DocumentCoverList.Count; i++)
            //    {
            //        CoverDetailsTable.Rows.Add(DocumentCoverList[i].CoverType, DocumentCoverList[i].DocumentCoverName, DocumentCoverList[i].DocumentDescriptionOfCover, DocumentCoverList[i].DocumentTypeOfCover);
            //    }
            //}
            //ht.Add("@CoverDetailsTable", CoverDetailsTable);
            //// End DataTable For Cover Add


            ////DataTable For Tender Document
            //DataTable NITDocumentTable = new DataTable();
            //NITDocumentTable.Columns.Add("Documentype", typeof(string));
            //NITDocumentTable.Columns.Add("DocumentDescription", typeof(string));
            //NITDocumentTable.Columns.Add("UploadedDateOfTender", typeof(DateTime));
            //NITDocumentTable.Columns.Add("TenderDocumentPath", typeof(string));
            //NITDocumentTable.Columns.Add("VerifiedBy", typeof(string));

            //int TenderTableCurrentIdentity = Master.GetCurrentIdentityOfTenderTable();
            //if (TenderDocumentsList != null)
            //{
            //    for (int i = 0; i < TenderDocumentsList.Count; i++)
            //    {
            //        if (TenderDocumentsList[i].TenderDocumentPath == null)
            //        {
            //            TenderDocumentsList[i].TenderDocumentPath = SWCS_Integration.Upload_Single_pdf_File_new(TenderDocumentsList[i].TenderDocument, TenderTableCurrentIdentity + 1, "Tender_Documents", i + 1);
            //        }
            //        NITDocumentTable.Rows.Add(TenderDocumentsList[i].DocumentypeOfTender, TenderDocumentsList[i].DocumentDescriptionOfTender, TenderDocumentsList[i].UploadedDateOfTender, TenderDocumentsList[i].TenderDocumentPath, TenderDocumentsList[i].VerifiedBy);
            //    }
            //}
            //ht.Add("@NITDocumentTable", NITDocumentTable);
            //// End DataTable For Tender Document


            ////DataTable For Work Document
            //DataTable WorkDocument = new DataTable();
            //WorkDocument.Columns.Add("Documentype", typeof(string));
            //WorkDocument.Columns.Add("DocumentDescription", typeof(string));
            //WorkDocument.Columns.Add("UploadedDateOfTender", typeof(DateTime));
            //WorkDocument.Columns.Add("TenderDocumentPath", typeof(string));

            //TenderTableCurrentIdentity = Master.GetCurrentIdentityOfTenderTable();
            //if (WorkItemDocumentsList != null)
            //{
            //    for (int i = 0; i < WorkItemDocumentsList.Count; i++)
            //    {

            //        if (WorkItemDocumentsList[i].WorkItemDocumentPath == null)
            //        {
            //            WorkItemDocumentsList[i].WorkItemDocumentPath = SWCS_Integration.Upload_Single_pdf_File_new(TenderDocumentsList[i].TenderDocument, TenderTableCurrentIdentity + 1, "Work_Documents", i + 1);

            //        }
            //        WorkDocument.Rows.Add(WorkItemDocumentsList[i].Documentype, WorkItemDocumentsList[i].DocumentDescription, WorkItemDocumentsList[i].UploadedDate, WorkItemDocumentsList[i].WorkItemDocumentPath);
            //    }
            //}
            //ht.Add("@WorkDocumentTable", WorkDocument);
            //// End DataTable For Work Document


            //// DataTable For Offline Instrument
            //DataTable OffLineInstrumentsTable = new DataTable();
            //OffLineInstrumentsTable.Columns.Add("OfflineInstrumentID", typeof(Int32));

            //if (SelectedOfflineInstruments != null)
            //{
            //    for (int i = 0; i < SelectedOfflineInstruments.Count; i++)
            //    {
            //        OffLineInstrumentsTable.Rows.Add(SelectedOfflineInstruments[i]);
            //    }
            //}

            //ht.Add("@OffLineInstrumentsTable", OffLineInstrumentsTable);

            //// END DataTable For Offline Instrument

            //ht.Add("@TenderReferenceNo", TenderReferenceNo);
            //ht.Add("@TenderType", TenderType);
            //ht.Add("@FormOfContract", FormOfContract);
            //ht.Add("@NoOfCover", NoOfCover);
            //ht.Add("@TenderCategory", TenderCategory);
            //ht.Add("@WorkOrItemTitle", WorkOrItemTitle);
            //ht.Add("@LocationDetail", LocationDetail);
            //ht.Add("@WorkOrItemDescription", WorkOrItemDescription);
            //ht.Add("@Pincode", Pincode);
            //ht.Add("@HasPreBidMeeting", HasPreBidMeeting);
            //ht.Add("@PreBidMeetingPlace", (PreBidMeetingPlace == null ? "NA" : PreBidMeetingPlace));
            //ht.Add("@PreBidMeetingAddress", (PreBidMeetingAddress == null ? "NA" : PreBidMeetingAddress));
            //ht.Add("@PreQualificationDetails", PreQualificationDetails);
            //ht.Add("@ProductCategory", ProductCategory);
            //ht.Add("@ProductSubCategory", ProductSubCategory);
            //ht.Add("@BidOpeningPlace", BidOpeningPlace);
            //ht.Add("@StateID", StateID);
            //ht.Add("@ContractType", ContractType);
            //ht.Add("@TendererClass", TendererClass);
            //ht.Add("@TenderCurrency", TenderCurrency);
            //ht.Add("@TenderValue", TenderValue);
            //ht.Add("@InvitingOfficer", InvitingOfficer);
            //ht.Add("@InvitingOfficerAddress", InvitingOfficerAddress);
            //ht.Add("@DelieveryPeriod", DelieveryPeriod);
            //ht.Add("@BidValidityDays", BidValidityDays);
            //ht.Add("@FeePaymentMode", FeePaymentMode);
            //ht.Add("@TenderFee", TenderFee);
            //ht.Add("@IsExemptionAllowed", IsExemptionAllowed);
            //ht.Add("@IsEMDFeeFixedOrPercentage", IsEMDFeeFixedOrPercentage);
            //ht.Add("@EmdAmount", EmdAmount);
            //ht.Add("@EmdECVPercentage", EmdECVPercentage);
            //ht.Add("@IsEmdExceptionAllowed", IsEmdExceptionAllowed);
            //ht.Add("@EMDFeePayableTo", EMDFeePayableTo);
            //ht.Add("@EMDFeePayableAt", EMDFeePayableAt);
            //ht.Add("@TenderFeePayableTo", TenderFeePayableTo);
            //ht.Add("@TenderFeePayableAt", TenderFeePayableAt);
            //ht.Add("@PublishingDate", PublishingDate);
            //ht.Add("@DocumentDownloadOrSaleStartDate", DocumentDownloadOrSaleStartDate);
            //ht.Add("@SeekClarificationStartDate", (SeekClarificationStartDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : SeekClarificationStartDate));
            //ht.Add("@SeekClarificationEndDate", (SeekClarificationEndDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : SeekClarificationEndDate));
            //ht.Add("@PreBidMeetingDate", (PreBidMeetingDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : PreBidMeetingDate));
            //ht.Add("@BidSubmissionStartDate", BidSubmissionStartDate);
            //ht.Add("@BidSubmissionClosingDate", BidSubmissionClosingDate);
            //ht.Add("@BidOpeningDate", BidOpeningDate);
            //ht.Add("@TenderID", TenderIDParam);


            //ht.Add("@Case", Case);

            //Result = osqlHelper.ExecuteQuery("PROC_SAVE_UPDATE_DELETE_TENDER", ht);
            return 0;
        }


        public int Delete(int TenderIDParam)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_TenderID", TenderIDParam)
                                        };
            int result = sql.execStoredProcudure("PROC_DELETE_TENDER_DETAILS", spmLogin);
            #endregion	

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@TenderID", TenderIDParam);
            //int result = osqlHelper.ExecuteQuery("PROC_DELETE_TENDER_DETAILS", ht);
            return result;
        }

    }




    public class DocumentCover
    {
        public string CoverType { get; set; }
        public string DocumentCoverName { get; set; }
        public string DocumentDescriptionOfCover { get; set; }
        public string DocumentTypeOfCover { get; set; }
    }

    public class TenderDocuments
    {
        public string DocumentypeOfTender { get; set; }
        public string DocumentDescriptionOfTender { get; set; }
        public DateTime UploadedDateOfTender { get; set; }
        public string VerifiedBy { get; set; }
        public HttpPostedFileBase TenderDocument { get; set; }
        public string TenderDocumentPath { get; set; }

    }

    public class WorkItemDocuments
    {
        public string Documentype { get; set; }
        public DateTime UploadedDate { get; set; }
        public string DocumentDescription { get; set; }
        public HttpPostedFileBase WorkItemDocument { get; set; }
        public string WorkItemDocumentPath { get; set; }

    }

}