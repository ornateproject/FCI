using SIDCUL.Areas.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Models
{
    public class CWCCppCorrigendumModel
    {

        public int CorrigendumID { get; set; }
        public int CorrigendumType { get; set; }
        public string CorrigendumTitle { get; set; }
        public string CorrigendumDescription { get; set; }
        public HttpPostedFileBase CorrigendumDocument { get; set; }

        public string CorrigendumDocumentFilePath { get; set; }

        public int CorrigendumReason { get; set; }
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
            DataTable ResultTable = new DataTable();
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_GET_TENDER_XML", spmLogin, "");
            #endregion
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
        public CWCCppCorrigendumModel(int TenderID, int CorrigendumID)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("TenderID", TenderID),
                                            new MySqlParameter("CorrigendumID", CorrigendumID)
                                        };
            ds = sql.getDataSet("PROC_GET_TENDER_CORRIGENDUM_DETAILS_BY_ID", spmLogin, "");

            DataTable ResultTable = new DataTable();
            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //ht.Add("@TenderID", TenderID);
            //ht.Add("@CorrigendumID", CorrigendumID);
            //DataSet ds = new DataSet();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_TENDER_CORRIGENDUM_DETAILS_BY_ID", ht);
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
                                CorrigendumID = (ResultTable.Rows[0]["CorrigendumID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["CorrigendumID"]));
                                CorrigendumType = (ResultTable.Rows[0]["CorrigendumType"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["CorrigendumType"]));
                                CorrigendumReason = (ResultTable.Rows[0]["CorrigendumReason"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["CorrigendumReason"]));
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
                                CorrigendumTitle = ResultTable.Rows[0]["CorrigendumTitle"].ToString();
                                CorrigendumDescription = ResultTable.Rows[0]["CorrigendumDescription"].ToString();
                                CorrigendumDocumentFilePath = ResultTable.Rows[0]["CorrigendumDocumentFilePath"].ToString();
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


        public CWCCppCorrigendumModel()
        {

        }

        public List<CWCCppCorrigendumModel> GetCWCTenderListForIndex()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                        };
            ds = sql.getDataSet("PROC_GET_TENDER_CORRIGENDUM_INDEX", spmLogin, "");

            DataTable ResultTable = new DataTable();
            if (ds != null)
                ResultTable = ds.Tables[0];
            else
                ResultTable = null;

            //DataTable ResultTable = new DataTable();
            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //ResultTable = osqlHelper.ExecuteProcudereReturnDataTable("PROC_GET_TENDER_CORRIGENDUM_INDEX", ht);
            #endregion


            List<CWCCppCorrigendumModel> CWCCppCorrigendumModelList = new List<CWCCppCorrigendumModel>();
            if (ResultTable != null)
            {
                if (ResultTable.Rows.Count > 0)
                {
                    for (int i = 0; i < ResultTable.Rows.Count; i++)
                    {
                        CWCCppCorrigendumModelList.Add(new CWCCppCorrigendumModel
                        {
                            CorrigendumID = (ResultTable.Rows[i]["CorrigendumID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["CorrigendumID"])),
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
                    return CWCCppCorrigendumModelList;
                }
            }
            return null;
        }


        public int SaveUpdate(string Case, int TenderIDParam)
        {
            int Result = 0;
            int CorrigendumTableIdentity = Master.GetCurrentIdentityOfCorrigendumTable();
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_TenderReferenceNo", TenderReferenceNo),
                                            new MySqlParameter("P_TenderType", TenderType),
                                            new MySqlParameter("P_FormOfContract", FormOfContract),
                                            new MySqlParameter("P_NoOfCover", NoOfCover),
                                            new MySqlParameter("P_TenderCategory", TenderCategory),
                                            new MySqlParameter("P_WorkOrItemTitle", WorkOrItemTitle),
                                            new MySqlParameter("P_LocationDetail", LocationDetail),
                                            new MySqlParameter("P_WorkOrItemDescription", WorkOrItemDescription),
                                            new MySqlParameter("P_Pincode", Pincode),
                                            new MySqlParameter("P_HasPreBidMeeting", HasPreBidMeeting),
                                            new MySqlParameter("P_PreBidMeetingPlace", (PreBidMeetingPlace == null ? "NA" : PreBidMeetingPlace)),
                                            new MySqlParameter("P_PreBidMeetingAddress", (PreBidMeetingAddress == null ? "NA" : PreBidMeetingAddress)),
                                            new MySqlParameter("P_PreQualificationDetails", PreQualificationDetails),
                                            new MySqlParameter("P_ProductCategory", ProductCategory),
                                            new MySqlParameter("P_ProductSubCategory", ProductSubCategory),
                                            new MySqlParameter("P_BidOpeningPlace", BidOpeningPlace),
                                            new MySqlParameter("P_StateID", StateID),
                                            new MySqlParameter("P_ContractType", ContractType),
                                            new MySqlParameter("P_TendererClass", TendererClass),
                                            new MySqlParameter("P_TenderCurrency", TenderCurrency),
                                            new MySqlParameter("P_TenderValue", TenderValue),
                                            new MySqlParameter("P_InvitingOfficer", InvitingOfficer),
                                            new MySqlParameter("P_InvitingOfficerAddress", InvitingOfficerAddress),
                                            new MySqlParameter("P_DelieveryPeriod", DelieveryPeriod),
                                            new MySqlParameter("P_BidValidityDays", BidValidityDays),
                                            new MySqlParameter("P_FeePaymentMode", FeePaymentMode),
                                            new MySqlParameter("P_TenderFee", TenderFee),
                                            new MySqlParameter("P_IsExemptionAllowed", IsExemptionAllowed),
                                            new MySqlParameter("P_IsEMDFeeFixedOrPercentage", IsEMDFeeFixedOrPercentage),
                                            new MySqlParameter("P_EmdAmount", EmdAmount),
                                            new MySqlParameter("P_EmdECVPercentage", EmdECVPercentage),
                                            new MySqlParameter("P_IsEmdExceptionAllowed", IsEmdExceptionAllowed),
                                            new MySqlParameter("P_EMDFeePayableTo", EMDFeePayableTo),
                                            new MySqlParameter("P_EMDFeePayableAt", EMDFeePayableAt),
                                            new MySqlParameter("P_TenderFeePayableTo", TenderFeePayableTo),
                                            new MySqlParameter("P_TenderFeePayableAt", TenderFeePayableAt),
                                            new MySqlParameter("P_PublishingDate", PublishingDate),
                                            new MySqlParameter("P_DocumentDownloadOrSaleStartDate", DocumentDownloadOrSaleStartDate),
                                            new MySqlParameter("P_SeekClarificationStartDate", (SeekClarificationStartDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : SeekClarificationStartDate)),
                                            new MySqlParameter("P_SeekClarificationEndDate", (SeekClarificationEndDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : SeekClarificationEndDate)),
                                            new MySqlParameter("P_PreBidMeetingDate", (PreBidMeetingDate == System.DateTime.MinValue ? Convert.ToDateTime("1753-01-01") : PreBidMeetingDate)),
                                            new MySqlParameter("P_BidSubmissionStartDate", BidSubmissionStartDate),
                                            new MySqlParameter("P_BidSubmissionClosingDate", BidSubmissionClosingDate),
                                            new MySqlParameter("P_BidOpeningDate", BidOpeningDate),
                                            new MySqlParameter("P_TenderID", TenderIDParam),
                                            new MySqlParameter("P_CorrigendumType", CorrigendumType),
                                            new MySqlParameter("P_CorrigendumTitle", CorrigendumTitle),
                                            new MySqlParameter("P_CorrigendumDescription", CorrigendumDescription),
                                            new MySqlParameter("P_CorrigendumDocumentFilePath", (CorrigendumDocument == null  ? "NA" : SWCS_Integration.Upload_Single_pdf_File_new(CorrigendumDocument, CorrigendumTableIdentity + 1, "CorrigendumDocument", 1))),
                                            new MySqlParameter("P_CorrigendumReason", CorrigendumReason),
                                            new MySqlParameter("P_CorrigendumID", CorrigendumID),
                                            new MySqlParameter("P_Case", Case),
                                        };
            Result = sql.execStoredProcudure("PROC_SAVE_UPDATE_DELETE_CORRIGENDUM", spmLogin);
            return Result;
            #endregion
            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
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
            //ht.Add("@CorrigendumType", CorrigendumType);
            //ht.Add("@CorrigendumTitle", CorrigendumTitle);
            //ht.Add("@CorrigendumDescription", CorrigendumDescription);
            //int CorrigendumTableIdentity = Master.GetCurrentIdentityOfCorrigendumTable();
            //if (CorrigendumDocument != null)
            //{
            //    CorrigendumDocumentFilePath = SWCS_Integration.Upload_Single_pdf_File_new(CorrigendumDocument, CorrigendumTableIdentity + 1, "CorrigendumDocument", 1);
            //}
            //else
            //{
            //    CorrigendumDocumentFilePath = "NA";
            //}
            //ht.Add("@CorrigendumDocumentFilePath", CorrigendumDocumentFilePath);
            //ht.Add("@CorrigendumReason", CorrigendumReason);
            //ht.Add("@CorrigendumID", CorrigendumID);


            //ht.Add("@Case", Case);

            //Result = osqlHelper.ExecuteQuery("PROC_SAVE_UPDATE_DELETE_CORRIGENDUM", ht);
            return 0;
        }


        public int Delete(int CorrigendumIDParam)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_CorrigendumID", CorrigendumIDParam)
                                        };
            int result = sql.execStoredProcudure("PROC_DELETE_CORRIGENDUM_DETAILS", spmLogin);
            #endregion	

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@CorrigendumID", CorrigendumIDParam);
            //int result = osqlHelper.ExecuteQuery("PROC_DELETE_CORRIGENDUM_DETAILS", ht);
            return result;
        }

    }

}