using SIDCUL.Areas.Services.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Models
{
  
    public class VigilanceApplicationFormModel
    {

        public int VigilanceID { get; set; }
        public int VigilanceComplaintID { get; set; }

        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Remote("CheckExistingUserName", "VigilanceApplicationForm", ErrorMessage = "User Name already exists!")]
        [Required(ErrorMessage = "User Name is required")]
        [MaxLength(50, ErrorMessage = "UserName cannot be longer than 50 characters.")]
        public string UserName { get; set; }

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password Doesn't meet the requirements")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Date Of Complaint is required")]
        public DateTime DateOfComplaint { get; set; }
        //[Required(ErrorMessage = "Complaint Description is required")]
        public string ComplaintDesciption { get; set; }


        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password Doesn't meet the requirements")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Required(ErrorMessage = "Password Confirmation is required")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "SecurityQuestion is required")]
        public int SecurityQuestion { get; set; }
        [Required(ErrorMessage = "Answer is required")]
        public string Answer { get; set; }
        [Required(ErrorMessage = "Salutation is required")]
        public string Salutation { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [RegularExpression(@"^(\d{2})$", ErrorMessage = "Please Enter a Valid 2 digit Age ")]
        public int Age { get; set; }
        public string IsGovernmentIDPanCardOrAadharCard { get; set; }

        //[RegularExpression(@"^[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}$", ErrorMessage = "Please specify valid pan number")]
        public string PanCard { get; set; }

        //[RegularExpression(@"^(\d{12})$", ErrorMessage = "Please Enter a Valid 12 digit Aadhar Number")]
        public string AadharCard { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "State is required")]
        public int State { get; set; }

        public string StateName { get; set; }
        [Required(ErrorMessage = "City is required")]
        public int City { get; set; }
        public string CityName { get; set; }
        public string ComplaintStatus { get; set; }
        [Required(ErrorMessage = "Pincode is required")]
        public int Pincode { get; set; }
        public string PincodeName { get; set; }
        public string Remarks { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is Required")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter a Valid 10 digit Mobile Number")]
        public string MobileNo { get; set; }


        [RegularExpression(@"^([0-9]{3})$", ErrorMessage = "Please Enter a Valid 3 digit STD Code")]
        public string LandlineStdCode { get; set; }


        [RegularExpression(@"^([0-9]{8})$", ErrorMessage = "Please Enter a Valid 8 digit Landline No")]
        public string LandlineNo { get; set; }

        // [Required(ErrorMessage = "Otp is required")]
        public string OTP { get; set; }




        public VigilanceApplicationFormModel()
        {

        }


        public VigilanceApplicationFormModel(int VigilanceIDParam, string UserNameParam)
        {

            DataTable ResultTable = new DataTable();
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                           new MySqlParameter("VigilanceID",  VigilanceIDParam),
                                           new MySqlParameter("UserName",  UserNameParam)
                                        };
            ds = sql.getDataSet("PROC_GET_VIGILANCE_DETAILS_INDEX_AND_GET_BY_ID", spmLogin, "");
            #endregion

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@VigilanceID", VigilanceIDParam);
            //ht.Add("@UserName", UserNameParam);
            //DataSet ds = new DataSet();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_VIGILANCE_DETAILS_INDEX_AND_GET_BY_ID", ht);
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
                                SecurityQuestion = (ResultTable.Rows[0]["SecurityQuestion"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["SecurityQuestion"]));
                                Age = (ResultTable.Rows[0]["Age"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["Age"]));
                                DateOfComplaint = (ResultTable.Rows[0]["DateOfComplaint"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["DateOfComplaint"]));
                                State = (ResultTable.Rows[0]["State"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["State"]));
                                City = (ResultTable.Rows[0]["City"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["City"]));
                                Pincode = (ResultTable.Rows[0]["Pincode"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["Pincode"]));
                                VigilanceID = (ResultTable.Rows[0]["VigilanceID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["VigilanceID"]));
                                UserName = ResultTable.Rows[0]["UserName"].ToString();
                                Password = ResultTable.Rows[0]["Password"].ToString();
                                ConfirmPassword = ResultTable.Rows[0]["ConfirmPassword"].ToString();
                                Answer = ResultTable.Rows[0]["Answer"].ToString();
                                Salutation = ResultTable.Rows[0]["Salutation"].ToString();
                                Name = ResultTable.Rows[0]["Name"].ToString();
                                PanCard = ResultTable.Rows[0]["PanCard"].ToString();
                                AadharCard = ResultTable.Rows[0]["AadharCard"].ToString();
                                Address = ResultTable.Rows[0]["Address"].ToString();
                                Email = ResultTable.Rows[0]["Email"].ToString();
                                MobileNo = ResultTable.Rows[0]["MobileNo"].ToString();
                                LandlineStdCode = ResultTable.Rows[0]["LandlineStdCode"].ToString();
                                LandlineNo = ResultTable.Rows[0]["LandlineNo"].ToString();
                                IsGovernmentIDPanCardOrAadharCard = ResultTable.Rows[0]["IsGovernmentIDPanCardOrAadharCard"].ToString();
                                ComplaintDesciption = ResultTable.Rows[0]["ComplaintDesciption"].ToString();

                            }
                        }
                    }
                }

            }
        }


        public List<VigilanceApplicationFormModel> GetCWCVigilanceListForIndex(string UserNameParam)
        {
            DataTable ResultTable = new DataTable();
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@VigilanceID", 0);
            ht.Add("@UserName", UserNameParam);
            DataSet ds = new DataSet();
            List<VigilanceApplicationFormModel> VigilanceApplicationFormModelList = new List<VigilanceApplicationFormModel>();
            ds = osqlHelper.ExecuteProcudere("PROC_GET_VIGILANCE_DETAILS_INDEX_AND_GET_BY_ID", ht);
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
                                    VigilanceApplicationFormModelList.Add(new VigilanceApplicationFormModel
                                    {
                                        SecurityQuestion = (ResultTable.Rows[i]["SecurityQuestion"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["SecurityQuestion"])),
                                        Age = (ResultTable.Rows[i]["Age"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["Age"])),
                                        StateName = ResultTable.Rows[i]["StateName"].ToString(),
                                        CityName = ResultTable.Rows[i]["CityName"].ToString(),
                                        PincodeName = ResultTable.Rows[i]["Pincode"].ToString(),
                                        VigilanceID = (ResultTable.Rows[i]["VigilanceID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["VigilanceID"])),
                                        UserName = ResultTable.Rows[i]["UserName"].ToString(),
                                        Password = ResultTable.Rows[i]["Password"].ToString(),
                                        ConfirmPassword = ResultTable.Rows[i]["ConfirmPassword"].ToString(),
                                        Answer = ResultTable.Rows[i]["Answer"].ToString(),
                                        Salutation = ResultTable.Rows[i]["Salutation"].ToString(),
                                        Name = ResultTable.Rows[i]["Name"].ToString(),
                                        PanCard = ResultTable.Rows[i]["PanCard"].ToString(),
                                        AadharCard = ResultTable.Rows[i]["AadharCard"].ToString(),
                                        Address = ResultTable.Rows[i]["Address"].ToString(),
                                        Email = ResultTable.Rows[i]["Email"].ToString(),
                                        MobileNo = ResultTable.Rows[i]["MobileNo"].ToString(),
                                        LandlineStdCode = ResultTable.Rows[i]["LandlineStdCode"].ToString(),
                                        LandlineNo = ResultTable.Rows[i]["LandlineNo"].ToString(),
                                        IsGovernmentIDPanCardOrAadharCard = ResultTable.Rows[i]["IsGovernmentIDPanCardOrAadharCard"].ToString()

                                    });
                                }

                                return VigilanceApplicationFormModelList;
                            }
                        }
                    }
                }

            }

            return null;
        }




        public string SaveUpdate(string Case, int VigilanceIDParam)
        {

            string Result = "";

            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_UserName", UserName),
                                            new MySqlParameter("P_Password", CommonDAL.Decrypt(Password)),
                                            new MySqlParameter("P_ConfirmPassword", CommonDAL.Decrypt(ConfirmPassword)),
                                            new MySqlParameter("P_Answer", CommonDAL.Decrypt(Answer)),
                                            new MySqlParameter("P_Salutation", Salutation),
                                            new MySqlParameter("P_Name", Name),
                                            new MySqlParameter("P_PanCard", (PanCard == null ? "NA" : CommonDAL.Decrypt(PanCard))),
                                            new MySqlParameter("P_AadharCard", (AadharCard == null ? "NA" : CommonDAL.Decrypt(AadharCard))),
                                            new MySqlParameter("P_Address", Address),
                                            new MySqlParameter("P_Email", Email),
                                            new MySqlParameter("P_MobileNo", MobileNo),
                                            new MySqlParameter("P_LandlineStdCode", (LandlineStdCode == null ? "NA" : LandlineStdCode)),
                                            new MySqlParameter("P_LandlineNo", (LandlineNo == null ? "NA" : LandlineNo)),
                                            new MySqlParameter("P_SecurityQuestion", SecurityQuestion),
                                            new MySqlParameter("P_Age", Age),
                                            new MySqlParameter("P_State", State),
                                            new MySqlParameter("P_City", City),
                                            new MySqlParameter("P_Pincode", Pincode),
                                            new MySqlParameter("P_VigilanceID", VigilanceIDParam),
                                            new MySqlParameter("P_Case", Case),
                                            new MySqlParameter("P_IsGovernmentIDPanCardOrAadharCard", (PanCard == null &&  AadharCard == null ? "NA" : IsGovernmentIDPanCardOrAadharCard))
                                        };
            Result = sql.execStoredProcudureInString("PROC_SAVE_UPDATE_VIGILANCE_FORM_BASIC_DETAILS", spmLogin);
            return Result;
            #endregion

            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //ht.Add("@UserName", UserName);
            //ht.Add("@Password", CommonDAL.Decrypt(Password));
            //ht.Add("@ConfirmPassword", CommonDAL.Decrypt(ConfirmPassword));
            //ht.Add("@Answer", CommonDAL.Decrypt(Answer));
            //ht.Add("@Salutation", Salutation);
            //ht.Add("@Name", Name);
            //ht.Add("@PanCard", (PanCard == null ? "NA" : CommonDAL.Decrypt(PanCard)));
            //ht.Add("@AadharCard", (AadharCard == null ? "NA" : CommonDAL.Decrypt(AadharCard)));
            //ht.Add("@Address", Address);
            //ht.Add("@Email", Email);
            //ht.Add("@MobileNo", MobileNo);
            //ht.Add("@LandlineStdCode", (LandlineStdCode == null ? "NA" : LandlineStdCode));
            //ht.Add("@LandlineNo", (LandlineNo == null ? "NA" : LandlineNo));
            //ht.Add("@SecurityQuestion", SecurityQuestion);
            //ht.Add("@Age", Age);
            //ht.Add("@State", State);
            //ht.Add("@City", City);
            //ht.Add("@Pincode", Pincode);
            //ht.Add("@VigilanceID", VigilanceIDParam);
            //ht.Add("@Case", Case);
            //ht.Add("@VigilanceRefno_out", "NA");
            //if (PanCard == null && AadharCard == null)
            //{
            //    ht.Add("@IsGovernmentIDPanCardOrAadharCard", "NA");
            //}
            //else
            //{
            //    ht.Add("@IsGovernmentIDPanCardOrAadharCard", IsGovernmentIDPanCardOrAadharCard);
            //}


            //Result = osqlHelper.ExecuteQueryWithOutParamINString("PROC_SAVE_UPDATE_VIGILANCE_FORM_BASIC_DETAILS", ht);
            //return Result;

        }

        internal int CheckUserName()
        {
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_UserName", UserName)
                                        };
            int Result = sql.execStoredProcudure("PROC_CHECK_USER_NAME", spmLogin);
            return Result;
        }
    }


    public class VigilanceComplaintModel
    {

        public string VigilanceRefno { get; set; }
        public int? VigilanceComplaintID { get; set; }
        [Required(ErrorMessage = "Date Of Complaint is required")]
        public DateTime DateOfComplaint { get; set; }

        public List<AccussedPersonForComplaint> AccussedPersonForComplaintList { get; set; }

        [RegularExpression(@"[^a-zA-Z0-9\s]", ErrorMessage = "Subject must not contain any special chartacter")]
        [StringLength(80, ErrorMessage = "Subject must not be more than 80 characters")]
        public string Organisation { get; set; }

        [Required(ErrorMessage = "Details Of Allegation is required")]
        [StringLength(3000, ErrorMessage = "Details Of Allegation must not be more than 3000 characters")]
        public string DetailsOfAllegation { get; set; }
        public HttpPostedFileBase DocumentAccompanyingAllegation { get; set; }
        public string DocumentAccompanyingAllegationPath { get; set; }
        public string ComplaintType { get; set; }
        public string ComplaintStatus { get; set; }

        public int SubcomplaintType { get; set; }

        public string SaveUpdate(int VigilanceIDParam)
        {
            string Result = "";
            bool checkfile = true;
            bool flagApp_Doc = true;
            Byte[] bytes = null;
            string contenttype = "";
            string filename = "";
            string extension = "";
            string DocumentfilePath = "";
            //Security Audit Point 15/01/2019 added by Sachin
            if (DocumentAccompanyingAllegation != null)
            {
                string Extension = System.IO.Path.GetExtension(DocumentAccompanyingAllegation.FileName);

                if (Extension.ToLower() == ".pdf")
                {
                    checkfile = CheckMimeType(DocumentAccompanyingAllegation);
                    if (!checkfile)
                    {
                        return Result = "FT"; //File Type Incorrect.
                    }


                }
                else if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg")
                {
                   
                }
                else
                {

                    return Result = "FE"; //File Extension Incorrect.
                }
               


                string path = HttpContext.Current.Server.MapPath("~\\Temp\\");
                bool FolderExists = Directory.Exists(path);
                if (!FolderExists)
                {
                    Directory.CreateDirectory(path);
                }
                DocumentAccompanyingAllegation.SaveAs(path + DocumentAccompanyingAllegation.FileName);
                DocumentfilePath = path + DocumentAccompanyingAllegation.FileName;
                filename = Path.GetFileName(DocumentAccompanyingAllegation.FileName);
                extension = Path.GetExtension(filename);
                contenttype = DocumentAccompanyingAllegation.ContentType;//   PostedFile.ContentType;
                FileStream fs = new FileStream(DocumentfilePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                FileInfo doc = new FileInfo(DocumentfilePath);
                try { br.Close(); fs.Close(); if (doc.Exists) { doc.Delete(); } }
                catch (Exception) { throw; }
            }

            //

            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            int ComplaintTableIdentity = Master.GetCurrentIdentityOfComplaintTable();
            DataSet ds = new DataSet();
            ds = null;
            Result = null;
            if (ComplaintType != null && DetailsOfAllegation != null && ComplaintType.ToString() != string.Empty && DetailsOfAllegation.ToString() != string.Empty)
            {
                SqlHelper sql = new SqlHelper();
                MySqlParameter[] spmLogin = {
                                            new MySqlParameter("p_VigilanceID", VigilanceIDParam),
                                            new MySqlParameter("p_Organisation", Organisation),
                                            new MySqlParameter("p_DetailsOfAllegation", DetailsOfAllegation),
                                            new MySqlParameter("p_SubComplaintType", SubcomplaintType),
                                            new MySqlParameter("p_ComplaintType", ComplaintType),
                                            new MySqlParameter("p_DocumentAccompanyingAllegationPath", DocumentAccompanyingAllegation != null ? (SWCS_Integration.Upload_Single_pdf_File_new(DocumentAccompanyingAllegation, ComplaintTableIdentity + 1, "VigilanceComplaintDocument", 1)) : "NA"),
                                            new MySqlParameter("p_FILE_DATA", bytes),
                                            new MySqlParameter("p_CONTENT_TYPE", contenttype),
                                            new MySqlParameter("p_EXTENSION", extension),
                                            new MySqlParameter("p_FILE_NAME", filename),
                                            new MySqlParameter("p_DateOfComplaint", DateOfComplaint)
                                        };
                ds = sql.getDataSet("PROC_SAVE_UPDATE_VIGILANCE_FORM_COMPLAINT_DETAILS", spmLogin, "");

            }

            if (ds != null && ds.Tables.Count > 0)
            {
                Result = ds.Tables[0].Rows[0]["ApplicantRefNo"].ToString();
                int VigilanceComplaintID = Convert.ToInt32(ds.Tables[0].Rows[0]["VigilanceComplaintid"]);

                if (AccussedPersonForComplaintList != null)
                {
                    for (int i = 0; i < AccussedPersonForComplaintList.Count; i++)
                    {
                        SqlHelper sql1 = new SqlHelper();
                        MySqlParameter[] spmLogin1 = {
                                            new MySqlParameter("P_VigilanceComplaintID", VigilanceComplaintID),
                                            new MySqlParameter("P_ComplaintAgainstName", AccussedPersonForComplaintList[i].ComplaintAgainstName),
                                            new MySqlParameter("P_DesignationOfAccused", AccussedPersonForComplaintList[i].DesignationOfAccused)
                                        };
                        string result1 = sql1.execStoredProcudureInString("PROC_SAVE_UPDATE_VIGILANCE_FORM_COMPLAINT_DETAILS_repeater", spmLogin1);

                    }
                }

            }

            #endregion
            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //DataTable AccussedDetailsTable = new DataTable();
            //AccussedDetailsTable.Columns.Add("ComplaintAgainstName", typeof(string));
            //AccussedDetailsTable.Columns.Add("DesignationOfAccused", typeof(string));
            //if (AccussedPersonForComplaintList != null)
            //{
            //    for (int i = 0; i < AccussedPersonForComplaintList.Count; i++)
            //    {
            //        AccussedDetailsTable.Rows.Add(AccussedPersonForComplaintList[i].ComplaintAgainstName, AccussedPersonForComplaintList[i].DesignationOfAccused);
            //    }
            //}
            //ht.Add("@AccussedDetailsTable", AccussedDetailsTable);
            //ht.Add("@VigilanceID", VigilanceIDParam);
            //ht.Add("@Organisation", Organisation);
            //ht.Add("@DetailsOfAllegation", DetailsOfAllegation);
            //ht.Add("@SubComplaintType", SubcomplaintType);
            //ht.Add("@ComplaintType", ComplaintType);
            //int ComplaintTableIdentity = Master.GetCurrentIdentityOfComplaintTable();
            //if (DocumentAccompanyingAllegation != null)
            //{
            //    DocumentAccompanyingAllegationPath = SWCS_Integration.Upload_Single_pdf_File_new(DocumentAccompanyingAllegation, ComplaintTableIdentity + 1, "VigilanceComplaintDocument", 1);
            //}
            //else
            //{
            //    DocumentAccompanyingAllegationPath = "NA";
            //}
            //ht.Add("@DocumentAccompanyingAllegationPath", DocumentAccompanyingAllegationPath);
            //ht.Add("@DateOfComplaint", DateOfComplaint);
            //ht.Add("@VigilanceRefno_out", "NA");
            //Result = osqlHelper.ExecuteQueryWithOutParamINString("PROC_SAVE_UPDATE_VIGILANCE_FORM_COMPLAINT_DETAILS", ht);
            return Result;
        }

        public bool CheckMimeType(HttpPostedFileBase posted_file)
        {
            bool flag = true;

            try
            {
                string path = HttpContext.Current.Server.MapPath("~\\Temp\\");
                bool FolderExists = Directory.Exists(path);
                if (!FolderExists)
                {
                    Directory.CreateDirectory(path);
                }
                posted_file.SaveAs(path + posted_file.FileName);
                string filePath = path + posted_file.FileName;

                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(filePath);

                oPdfReader.Close();

                FileInfo doc = new FileInfo(filePath);
                if (doc.Exists)
                {
                    doc.Delete();
                }
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return flag;
        }


        public VigilanceComplaintModel(int VigilanceComplaintIDParam, string UserNameParam)
        {
            DataTable ResultTable = new DataTable();
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                          new MySqlParameter("VigilanceComplaintID", VigilanceComplaintIDParam),
                                          new MySqlParameter("UserName", UserNameParam)
                                        };
            ds = sql.getDataSet("PROC_GET_VIGILANCE_COMPLAINT_DETAILS_INDEX_AND_GET_BY_ID_New", spmLogin, "");
            #endregion

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@VigilanceComplaintID", VigilanceComplaintIDParam);
            //ht.Add("@UserName", UserNameParam);
            //DataSet ds = new DataSet();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_VIGILANCE_COMPLAINT_DETAILS_INDEX_AND_GET_BY_ID", ht);
            if (ds.Tables[0] != null)
            {
                ResultTable = ds.Tables[0];
                if (ResultTable != null)
                {
                    if (ResultTable.Rows.Count > 0)
                    {
                        VigilanceRefno = ResultTable.Rows[0]["VigilanceRefno"].ToString();
                        DateOfComplaint = (ResultTable.Rows[0]["DateOfComplaint"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["DateOfComplaint"]));
                        Organisation = ResultTable.Rows[0]["Organisation"].ToString();
                        DetailsOfAllegation = ResultTable.Rows[0]["DetailsOfAllegation"].ToString();
                        DocumentAccompanyingAllegationPath = ResultTable.Rows[0]["DocumentAccompanyingAllegationPath"].ToString();
                    }
                }
            }

            if (ds.Tables[1] != null)
            {
                AccussedPersonForComplaintList = new List<AccussedPersonForComplaint>();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    DataTable ResultTable1 = new DataTable();
                    ResultTable1 = ds.Tables[1];
                    for (int i = 0; i < ResultTable1.Rows.Count; i++)
                    {
                        AccussedPersonForComplaintList.Add(new AccussedPersonForComplaint
                        {
                            ComplaintAgainstName = ResultTable1.Rows[i]["ComplaintAgainstName"].ToString(),
                            DesignationOfAccused = ResultTable1.Rows[i]["DesignationOfAccused"].ToString()
                        });
                    }
                }
                else
                {
                    AccussedPersonForComplaintList = null;
                }
            }
        }


        public VigilanceComplaintModel()
        {

        }


        public List<VigilanceComplaintModel> GetCWCVigilanceListForIndex(string UserNameParam, string Role)
        {
            DataTable ResultTable = new DataTable();
            List<VigilanceComplaintModel> VigilanceApplicationFormModelList = new List<VigilanceComplaintModel>();
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("VigilanceComplaintID", "0"),
                                            new MySqlParameter("UserName", UserNameParam)
                                        };
            if (Role == "0")
            {
                ds = sql.getDataSet("PROC_GET_VIGILANCE_COMPLAINT_FOR_VIG", spmLogin, "");
            }
            else
            {
                ds = sql.getDataSet("PROC_GET_VIGILANCE_COMPLAINT_DETAILS_INDEX_AND_GET_BY_ID", spmLogin, "");
            }
            #endregion

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@VigilanceComplaintID", 0);
            //ht.Add("@UserName", UserNameParam);
            //DataSet ds = new DataSet();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_VIGILANCE_COMPLAINT_DETAILS_INDEX_AND_GET_BY_ID", ht);
            if (ds != null && ds.Tables.Count > 0)
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
                                    VigilanceApplicationFormModelList.Add(new VigilanceComplaintModel
                                    {
                                        VigilanceRefno = ResultTable.Rows[i]["VigilanceRefno"].ToString(),
                                        VigilanceComplaintID = (ResultTable.Rows[i]["VigilanceComplaintID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["VigilanceComplaintID"])),
                                        DateOfComplaint = (ResultTable.Rows[i]["DateOfComplaint"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["DateOfComplaint"])),
                                        //ComplaintAgainstName = ResultTable.Rows[i]["ComplaintAgainstName"].ToString(),
                                        //DesignationOfAccused = ResultTable.Rows[i]["DesignationOfAccused"].ToString(),
                                        Organisation = ResultTable.Rows[i]["Organisation"].ToString(),
                                        DetailsOfAllegation = ResultTable.Rows[i]["DetailsOfAllegation"].ToString(),
                                        DocumentAccompanyingAllegationPath = ResultTable.Rows[i]["DocumentAccompanyingAllegationPath"].ToString(),
                                        ComplaintStatus = ResultTable.Rows[i]["ComplaintStatus"].ToString(),
                                        ComplaintType = ResultTable.Rows[i]["ComplaintType"] == DBNull.Value ? "NA" : ResultTable.Rows[i]["ComplaintType"].ToString() == "G" ? "General" : ResultTable.Rows[i]["ComplaintType"].ToString() == "E" ? "EWC" : "Vigilance"

                                    });
                                }

                                return VigilanceApplicationFormModelList;
                            }
                        }
                    }
                }

            }

            return null;
        }



    }



    public class AccussedPersonForComplaint
    {
        [Required(ErrorMessage = "Complaint Against Name is required")]
        public string ComplaintAgainstName { get; set; }
        [Required(ErrorMessage = "Designation Of Accused is required")]
        public string DesignationOfAccused { get; set; }
    }


    public class VigilanceViewModel
    {
        public VigilanceApplicationFormModel _VigilanceApplicationFormModel { get; set; }

        public VigilanceComplaintModel _VigilanceComplaintModel { get; set; }
    }
}