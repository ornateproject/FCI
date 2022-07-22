using CaptchaMvc.HtmlHelpers;
using CWC_CMS.Models;
using SIDCUL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System.Drawing;

namespace CWC_CMS.Controllers
{
   [CustomExceptionHandlerFilter]
    public class VigilanceApplicationFormController : Controller
    {
        //
        // GET: /VigilanceApplicationForm/
        public ActionResult Index()
        {
            
            if (Session["User_recno"] != null)
            {
                
              
                if (Session["User_recno"].ToString() != Token.DecryptString(Request.QueryString["eid"]))
                {
                    return (RedirectToAction("Index", "Login", new { UserValid = "Invalid User....Please Try Again" }));
                }
            }
            if (Session["Role"] != null)
            {
                if (TempData["SaveResult"] != null && TempData["SaveUpdateMessage"] != null)
                {
                    ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResult"]);
                    ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessage"].ToString();
                }
                string UserName = "";
                if (Session["userdetails"] != null)
                {
                    Uri myurl = Request.UrlReferrer;   //Added by Renu 15 Jan 2020
                    if (myurl != null)
                    {
                        string userdetails = null;
                        string[] details = null;

                        userdetails = Session["userdetails"].ToString();
                        details = userdetails.Split('(', '@', '#', '$', ')');
                        // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

                        UserName = details[20];

                        Session["UserName"] = UserName;
                    }
                    else
                    {
                        return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
                    }
                }
                SessionFixation();
                List<VigilanceComplaintModel> VigilanceComplaintModelList = new List<VigilanceComplaintModel>();
                VigilanceComplaintModel _VigilanceComplaintModel = new VigilanceComplaintModel();

                VigilanceComplaintModelList = _VigilanceComplaintModel.GetCWCVigilanceListForIndex(UserName, Session["Role"].ToString());

                return View(VigilanceComplaintModelList);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public void ExportToExcel()
        {
            string UserName = "";
            List<VigilanceComplaintModel> VigilanceComplaintModelList = new List<VigilanceComplaintModel>();
            VigilanceComplaintModel _VigilanceComplaintModel = new VigilanceComplaintModel();
            VigilanceComplaintModelList = _VigilanceComplaintModel.GetCWCVigilanceListForIndex(Session["UserName"].ToString(), Session["Role"].ToString());
            //List<EmployeeViewModel> emplist = db.EmployeeInfoes.Select(x => new EmployeeViewModel
            //{
            //    EmployeeId = x.EmployeeId,
            //    EmployeeName = x.EmployeeName,
            //    Email = x.Email,
            //    Phone = x.Phone,
            //    Experience = x.Experience
            //}).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "COMPLAINT REF NO";
            ws.Cells["B6"].Value = "COMPLAINT TYPE";
            ws.Cells["C6"].Value = "DATE";
            ws.Cells["D6"].Value = "SUBJECT";
            ws.Cells["E6"].Value = "DETAILS";
            ws.Cells["F6"].Value = "STATUS";


            int rowStart = 7;
            foreach (var item in VigilanceComplaintModelList)
            {


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.VigilanceRefno;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.ComplaintType;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.DateOfComplaint.ToString("dd-MMM-yyyy");
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Organisation;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.DetailsOfAllegation;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.ComplaintStatus;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        #region  Session Fixation added by renu 27 Jan 
        public ActionResult SessionFixation()
        {
            string _sessionIPAdress = string.Empty;
            string _sessionBrowserInfo = string.Empty;

            if (HttpContext.Session != null)
            {
                string _encryptedString = Convert.ToString(Session["encryptedSession"]);
                byte[] _encodedAsBytes = System.Convert.FromBase64String(_encryptedString);
                string _decryptedString = System.Text.ASCIIEncoding.ASCII.GetString(_encodedAsBytes);
                char[] _separator = new char[] { '^' };
                if (_decryptedString != string.Empty && _decryptedString != "" && _decryptedString != null)
                {
                    string[] _splitStrings = _decryptedString.Split(_separator);
                    if (_splitStrings.Count() > 0)
                    {
                        if (_splitStrings[1].Count() > 0)
                        {
                            string[] _userBrowserInfo = _splitStrings[2].Split('~');
                            if (_userBrowserInfo.Count() > 0)
                            {
                                _sessionBrowserInfo = _userBrowserInfo[0];
                                _sessionIPAdress = _userBrowserInfo[1];
                            }
                        }
                    }
                }
                string _currentuseripAddress;
                if (string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARD_FOR"]))
                {
                    _currentuseripAddress = Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    _currentuseripAddress = Request.ServerVariables["HTTP_X_FORWARD_FOR"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                }
                System.Net.IPAddress result;
                if (!System.Net.IPAddress.TryParse(_currentuseripAddress, out result))
                {
                    result = System.Net.IPAddress.None;
                }

                string _currentBrowserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent;

                if (_sessionIPAdress != "" && _sessionIPAdress != string.Empty)
                {
                    if (_sessionIPAdress != _currentuseripAddress || _sessionBrowserInfo != _currentBrowserInfo)
                    {
                        Session.RemoveAll();
                        Session.Clear();
                        Session.Abandon();
                        Response.Cookies["ASP.Net_Session_Id"].Expires = DateTime.Now.AddSeconds(-30);
                        Response.Cookies.Add(new HttpCookie("ASP.Net_Session_Id", ""));
                    }
                    else
                    {

                    }

                }
                else
                {
                    TempData["SaveResultOTP"] = 1;
                    TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                    return (RedirectToAction("Index", "Login", new { UserValid = "" }));
                }
                return View();
            }
            else
            {
                TempData["SaveResultOTP"] = 1;
                TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                return (RedirectToAction("Index", "Login", new { UserValid = "" }));
            }

        }
        #endregion Session Fixation added by renu 27 Jan 

        public ActionResult CheckExistingUserName(string UserName)
        {
            try
            {
                #region Renu 03 Feb 2020 For Mysql Start 
                // Renu 03 Feb 2020 For Mysql Start 
                DataSet ds = new DataSet();
                SqlHelper sql = new SqlHelper();
                MySqlParameter[] spmLogin = {
                                              new MySqlParameter("UserName",  UserName),
                                              new MySqlParameter("UserNameExists_out",  "No")
                                            };
                string result = sql.execStoredProcudureInString("PROC_CHECK_IF_USER_NAME_EXISTS", spmLogin);
                #endregion

                //SqlHelper osqlHelper = new SqlHelper();
                //Hashtable ht = new Hashtable();
                //ht.Add("@UserName", UserName);
                //ht.Add("@UserNameExists_out", "No");
                //string result = osqlHelper.ExecuteQueryWithOutParamINString("PROC_CHECK_IF_USER_NAME_EXISTS", ht);
                if (result == "Yes")
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }


            }

            catch (Exception ex)
            {

                return Json(false, JsonRequestBehavior.AllowGet);

            }

        }

        private bool ValidatePassword(string password)
        {
            string patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{7,15}$";
            if (!string.IsNullOrEmpty(password))
            {
                if (!Regex.IsMatch(password, patternPassword))
                {
                    return false;
                }

            }
            return true;
        }

        private bool ValidateAadhar(string aadhar)
        {
            string patternaadhar = @"^[0-9]{12}$";
            if (!string.IsNullOrEmpty(aadhar))
            {
                if (!Regex.IsMatch(aadhar, patternaadhar))
                {
                    return false;
                }

            }
            return true;
        }

        private bool ValidatePanCard(string pancard)
        {
            string patternpancard = @"^[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}$";
            if (!string.IsNullOrEmpty(pancard))
            {
                if (!Regex.IsMatch(pancard, patternpancard))
                {
                    return false;
                }

            }
            return true;
        }



        [EncryptedActionParameter]
        public ActionResult Create()
        {
            try
            {
                object VigilanceID = "0";
                string UserName = "";
                if (VigilanceID.ToString() == "System.Object")
                {
                    VigilanceID = 0;
                }

                if (Session["userdetails"] != null)
                {
                    string userdetails = null;
                    string[] details = null;

                    userdetails = Session["userdetails"].ToString();
                    details = userdetails.Split('(', '@', '#', '$', ')');
                    // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

                    UserName = details[20];
                }


                if (TempData["SaveResultOTP"] != null && TempData["SaveUpdateMessageOTP"] != null)
                {
                    ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResultOTP"]);
                    ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessageOTP"].ToString();
                }
                else
                {
                    ViewBag.IsNewForm = "Yes";
                }

                if (VigilanceID != null)
                {
                    int VigilanceIDParam = Convert.ToInt32(VigilanceID);
                    ViewBag.FillSecurityQuestion = Master.FillSecurityQuestion();
                    ViewBag.FillSalutation = Master.FillSalutation();
                    ViewBag.FillState = Master.FillState();
                    ViewBag.FillCity = Master.FillCity();
                    ViewBag.FillPincode = Master.FillPincode();

                    if (VigilanceIDParam == 0)
                    {
                        TempData["VigilanceID"] = VigilanceIDParam;
                        TempData["IsNewForm"] = "Yes";
                        // return View();
                        VigilanceApplicationFormModel vigilanceApplicationFormModel = new VigilanceApplicationFormModel();
                        return View(vigilanceApplicationFormModel);
                    }
                    else
                    {
                        VigilanceApplicationFormModel vigilanceApplicationFormModel = new VigilanceApplicationFormModel(VigilanceIDParam, UserName);
                        TempData["VigilanceID"] = VigilanceIDParam;
                        TempData["IsNewForm"] = "No";
                        ViewBag.IsNewForm = "No";
                        return View(vigilanceApplicationFormModel);
                    }
                }
                else
                {

                    return RedirectToAction("Index", "HomePage");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [EncryptedActionParameter]
        public ActionResult ViewConsolidatedApplication(object VigilanceComplaintID)
        {
            string[] Gentoken = VigilanceComplaintID.ToString().Split('G');

            string _VigilanceComplaintID = Gentoken[0];
            string Uid = Gentoken[1];
            if (Gentoken[1] == Session["Uid"].ToString())
            {
                if (Session["Role"] != null)
                {

                    ViewBag.FillSecurityQuestion = Master.FillSecurityQuestion();
                    ViewBag.FillSalutation = Master.FillSalutation();
                    ViewBag.FillState = Master.FillState();
                    ViewBag.FillCity = Master.FillCity();
                    ViewBag.FillPincode = Master.FillPincode();
                    ViewBag.FillSubComplaint = Master.FillSubComplaint();
                    ViewBag.FillSubComplaint = FillComplaintStatus();
                    Session["_VigilanceComplaintID"] = Convert.ToInt32(_VigilanceComplaintID);
                    string UserName = "";
                    if (Session["userdetails"] != null)
                    {
                        string userdetails = null;
                        string[] details = null;

                        userdetails = Session["userdetails"].ToString();
                        details = userdetails.Split('(', '@', '#', '$', ')');
                        // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

                        UserName = details[20];
                    }
                    int VigilanceComplaintIDParam = Convert.ToInt32(_VigilanceComplaintID);
                    ViewBag.VigilanceComplaintid = VigilanceComplaintIDParam;
                    int VigilanceID = Master.GetVigilanceIDByVigilanceComplaintID(VigilanceComplaintIDParam);
                    VigilanceViewModel _VigilanceViewModel = new VigilanceViewModel();
                    _VigilanceViewModel._VigilanceApplicationFormModel = new VigilanceApplicationFormModel(VigilanceID, UserName);
                    _VigilanceViewModel._VigilanceComplaintModel = new VigilanceComplaintModel(VigilanceComplaintIDParam, UserName);

                    return View(_VigilanceViewModel);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        [EncryptedActionParameter]
        public ActionResult VerifyAComplaint(int VigilanceComplaintID)
        {
            string UserName = "";
            if (Session["userdetails"] != null)
            {
                string userdetails = null;
                string[] details = null;

                userdetails = Session["userdetails"].ToString();
                details = userdetails.Split('(', '@', '#', '$', ')');
                // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

                UserName = details[20];
            }
            VigilanceComplaintModel _VigilanceComplaintModel = new VigilanceComplaintModel(VigilanceComplaintID, UserName);
            ViewBag.IsNewForm = "Yes";
            return View(_VigilanceComplaintModel);


        }


        public ActionResult CheckIfOtpEnteredIsValid()
        {
            if (TempData["SaveResultOTP"] != null && TempData["SaveUpdateMessageOTP"] != null)
            {
                ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResultOTP"]);
                ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessageOTP"].ToString();
            }
            GenerateOTP _GenerateOTP = new GenerateOTP();

            if (TempData.Peek("OTP") == null)
            {
                TempData["OTP"] = _GenerateOTP.Generate();
            }

            if (TempData["DisplayNameForOTPMail"] != null && TempData["EmailForOTPMail"] != null && TempData.Peek("OTP") != null)
            {
                _GenerateOTP.GenerateMailFormatForOTP(TempData["DisplayNameForOTPMail"].ToString(), TempData["EmailForOTPMail"].ToString(), TempData.Peek("OTP").ToString());
            }

            return View(_GenerateOTP);

        }

        [ActionName("CheckIfOtpEnteredIsValid")]
        [HttpPost]
        public ActionResult CheckIfOtpEnteredIsValid_Post()
        {
            VigilanceApplicationFormModel _VigilanceApplicationFormModel = new VigilanceApplicationFormModel();
            GenerateOTP _GenerateOTP = new GenerateOTP();
            TryUpdateModel(_VigilanceApplicationFormModel);
            if (TempData.Peek("OTP").ToString() == _VigilanceApplicationFormModel.OTP)
            {
                int VigilanceID = Convert.ToInt32(TempData.Peek("VigilanceIDForOTP"));
                string UserNamePassword = _GenerateOTP.ActionsPerformedWhenOTPIsValid(VigilanceID);
                string[] UserNamePasswordArray = UserNamePassword.Split('#');
                string UserName = UserNamePasswordArray[0];
                string Password = UserNamePasswordArray[1];
                string DisplayName = UserNamePasswordArray[3];
                string EmailID = UserNamePasswordArray[2];
                _GenerateOTP.GenerateMailFormat(DisplayName, EmailID, UserName, Password);

                TempData["SaveResultOTP"] = 1;
                TempData["SaveUpdateMessageOTP"] = "<h5> OTP Verification Completed Successfully !!! <h5 /><br /> Your Login Details and Registration Information has been sent successfully to you on your EMAIL.";
                return (RedirectToAction("Index", "Login", new { }));
            }
            else
            {
                ViewBag.FillSecurityQuestion = Master.FillSecurityQuestion();
                ViewBag.FillSalutation = Master.FillSalutation();
                ViewBag.FillState = Master.FillState();
                ViewBag.FillCity = Master.FillCity();
                ViewBag.FillPincode = Master.FillPincode();
                ViewBag.SaveResult = 1;
                ViewBag.SaveUpdateMessage = " OTP Entered Is Invalid. !!! Kindly try again.";
                _VigilanceApplicationFormModel = new VigilanceApplicationFormModel();
                _VigilanceApplicationFormModel = (VigilanceApplicationFormModel)TempData.Peek("vigilanceApplicationFormModel");
                return View("Create", _VigilanceApplicationFormModel);
            }


        }


        [EncryptedActionParameter]
        public ActionResult AddAComplaint()
        {
            string UserName = "";
            if (Session["userdetails"] != null)
            {
                string userdetails = null;
                string[] details = null;

                userdetails = Session["userdetails"].ToString();
                details = userdetails.Split('(', '@', '#', '$', ')');
                // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

                UserName = details[20];
            }
            //Praveen Kumar 20 May 2020
            //for Session Check Start
            else
            {
                return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
            }
            //Praveen Kumar 20 May 2020
            //for Session Check End
            VigilanceComplaintModel vigilanceComplaintModel = new VigilanceComplaintModel();
            ViewBag.FillSubComplaint = Master.FillSubComplaint();
            ViewBag.IsNewForm = "Yes";
            return View(vigilanceComplaintModel);

        }


        public ActionResult ViewAComplaint(int VigilanceComplaintID)
        {
            string UserName = "";
            if (Session["userdetails"] != null)
            {
                string userdetails = null;
                string[] details = null;

                userdetails = Session["userdetails"].ToString();
                details = userdetails.Split('(', '@', '#', '$', ')');
                // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

                UserName = details[20];
            }

            VigilanceComplaintModel _VigilanceComplaintModel = new VigilanceComplaintModel(VigilanceComplaintID, UserName);
            ViewBag.IsNewForm = "Yes";
            return View(_VigilanceComplaintModel);

        }

        [HttpPost]
        [ActionName("AddAComplaint")]
        public ActionResult AddAComplaint_Post()
        {
            VigilanceComplaintModel _VigilanceComplaintModel = new VigilanceComplaintModel();
            TryUpdateModel(_VigilanceComplaintModel);
            string UserName = "";
            if (Session["userdetails"] != null)
            {
                string userdetails = null;
                string[] details = null;

                userdetails = Session["userdetails"].ToString();
                details = userdetails.Split('(', '@', '#', '$', ')');
                // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

                UserName = details[20];
            }
            //Praveen Kumar 20 May 2020
            //for Session Check Start
            else
            {
                return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
            }
            //Praveen Kumar 20 May 2020
            //for Session Check End


            string EmailID = Master.GetEmailIDByLoginName(UserName);
            string MobileNo = Master.GetMobileNOByLoginName(UserName);
            GenerateOTP _GenerateOTP = new GenerateOTP();
            int VigilanceID = Master.GetVigilanceIDByLoginName(UserName);
            string Result = "NA";
            Result = _VigilanceComplaintModel.SaveUpdate(VigilanceID);

            if (Result != "NA" && Result != null)
            {
                if (Result != "FT" && Result != "FE")
                {
                    TempData["SaveResult"] = 1;
                    TempData["SaveUpdateMessage"] = "<h5> Your Complaint has been Submitted Successfully !!! <h5 /> <br /> Your Complaint RefNo " + Result;
                    _GenerateOTP.GenerateMailFormatForApplicationRefno(UserName, EmailID, Result, DateTime.Now.ToString("dd-MMM-yyyy"));

                    _GenerateOTP.SMSFORComplaint(UserName, MobileNo, Result);
                    string ComplainyMobileNo = MobileNo;
                    string ComplainyEmailId = EmailID;
                    if (_VigilanceComplaintModel.ComplaintType == "G")
                    {
                        string personalEmailId = "project.manger2@cewacor.nic.in";
                        _GenerateOTP.GenerateMailFormatForPersonalDivision(UserName, Result, _VigilanceComplaintModel.Organisation, _VigilanceComplaintModel.DetailsOfAllegation, _VigilanceComplaintModel.DateOfComplaint, personalEmailId, DateTime.Now.ToString("dd-MMM-yyyy"), ComplainyMobileNo, ComplainyEmailId);
                    }
                    if (_VigilanceComplaintModel.ComplaintType == "E")
                    {
                        string personalEmailId = "project.manger2@cewacor.nic.in";
                        _GenerateOTP.GenerateMailFormatForEWCDivision(UserName, Result, _VigilanceComplaintModel.Organisation, _VigilanceComplaintModel.DetailsOfAllegation, _VigilanceComplaintModel.DateOfComplaint, personalEmailId, DateTime.Now.ToString("dd-MMM-yyyy"), ComplainyMobileNo, ComplainyEmailId);
                    }


                    ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResult"]);
                    ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessage"].ToString();
                }
                else if (Result == "FT" && Result != "FE")
                {
                    TempData["SaveResult"] = 1;
                    TempData["SaveUpdateMessage"] = "<h5> Dear User,<br/> Uploaded Document is not a proper pdf file. please check pdf file and try again.";
                    ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResult"]);
                    ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessage"].ToString();
                }
                else if (Result != "FT" && Result == "FE")
                {
                    TempData["SaveResult"] = 1;
                    TempData["SaveUpdateMessage"] = "Dear User,<br/> Please select only .pdf,.jpg, .jpeg and .png only.";
                    ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResult"]);
                    ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessage"].ToString();
                }
                else
                {
                    //nothing
                }
            }
            else
            {
                TempData["SaveResult"] = 1;
                TempData["SaveUpdateMessage"] = "Dear User, Unable to process your Request. Please try again.";
                ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResult"]);
                ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessage"].ToString();
            }

            return (RedirectToAction("Index", new {eid = Models.Token.EncryptString(Session["User_recno"].ToString()) } ));


        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(string CaptchaText)
        {
            try
            {
                ViewBag.FillSecurityQuestion = Master.FillSecurityQuestion();
                ViewBag.FillSalutation = Master.FillSalutation();
                ViewBag.FillState = Master.FillState();
                ViewBag.FillCity = Master.FillCity();
                ViewBag.FillPincode = Master.FillPincode();
                VigilanceApplicationFormModel vigilanceApplicationFormModel = new VigilanceApplicationFormModel();
                TryUpdateModel(vigilanceApplicationFormModel);
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();

                if (vigilanceApplicationFormModel.Password != null)
                {
                    if (ValidatePassword(CommonDAL.Decrypt(vigilanceApplicationFormModel.Password)))
                    {
                        // insert opertion in Db
                    }
                    else
                    {
                        //TempData["SaveResult"] = 1;
                        //TempData["SaveUpdateMessage"] = "Password must be at least 7 characters, no more than 15 characters, and must include at least one upper case letter, one lower case letter, one numeric digit and one special character.";
                        ViewBag.IsNewForm = "Yes";
                        ViewBag.PasswordResult = "1";
                        return View("Create", vigilanceApplicationFormModel);
                    }
                }

                if (vigilanceApplicationFormModel.AadharCard != null)
                {
                    if (ValidateAadhar(CommonDAL.Decrypt(vigilanceApplicationFormModel.AadharCard)))
                    {
                        // insert opertion in Db
                    }
                    else
                    {
                        ViewBag.IsNewForm = "Yes";
                        ViewBag.AadharResult = "1";
                        return View("Create", vigilanceApplicationFormModel);
                    }
                }

                if (vigilanceApplicationFormModel.PanCard != null)
                {
                    if (ValidatePanCard(CommonDAL.Decrypt(vigilanceApplicationFormModel.PanCard)))
                    {
                        // insert opertion in Db
                    }
                    else
                    {
                        ViewBag.IsNewForm = "Yes";
                        ViewBag.PanCardResult = "1";
                        return View("Create", vigilanceApplicationFormModel);
                    }
                }

                int nameResult = vigilanceApplicationFormModel.CheckUserName();
                if (nameResult > 0)
                {
                    ViewBag.IsNewForm = "Yes";
                    ViewBag.UserNameResult = "1";
                    return View("Create", vigilanceApplicationFormModel);

                }
                else { }


                if (ModelState.IsValid)
                {

                    //  if (this.IsCaptchaValid("Captcha is not valid"))
                    if (CaptchaText == HttpContext.Session["captchastring"].ToString())
                    {
                        if (TempData.Peek("IsNewForm").ToString() == "Yes")
                        {
                            VigilanceApplicationFormModel cls = new VigilanceApplicationFormModel();
                            if (cls.Password != null)
                            {
                                if (ValidatePassword(CommonDAL.Decrypt(cls.Password)))
                                {
                                    // insert opertion in Db
                                }
                                else
                                {
                                    TempData["SaveResultOTP"] = 1;
                                    TempData["SaveUpdateMessageOTP"] = "Password must be at least 7 characters, no more than 15 characters, and must include at least one upper case letter, one lower case letter, one numeric digit and one special character.";

                                    return View("Create", vigilanceApplicationFormModel);
                                }
                            }


                            string Result = vigilanceApplicationFormModel.SaveUpdate("INSERT", Convert.ToInt32(TempData.Peek("VigilanceID")));
                            if (Result != "NA")
                            {
                                DateTime dt = DateTime.Now;
                                string Year = dt.Year.ToString();
                                string Month = dt.Month.ToString();
                                string Day = dt.Day.ToString();
                                string StringToReplaceFromRefno = "CWC_" + Year + "_" + Month + "_" + Day + "_VIG_";
                                int VigilanceID = Convert.ToInt32(Result.Replace(StringToReplaceFromRefno, ""));
                                TempData["VigilanceIDForOTP"] = VigilanceID;
                                TempData["DisplayNameForOTPMail"] = vigilanceApplicationFormModel.Name;
                                TempData["EmailForOTPMail"] = vigilanceApplicationFormModel.Email;
                                TempData["ForOTPMobileNo"] = vigilanceApplicationFormModel.MobileNo;
                                //TempData["SaveResult"] = 1;
                                //TempData["SaveUpdateMessage"] = "<h5> You Complaint has been Submited Successfully !!! <h5 /> <br /> Application RefNo" + Result;

                                TempData["SaveResultOTP"] = 1;
                                TempData["SaveUpdateMessageOTP"] = "Please Enter the OTP Sent to your Mail and SMS on Phone to confirm your Mail  and Proceed. !!! If OTP is not verified, Your form will not be considered by the department";
                                if (TempData["SaveResultOTP"] != null && TempData["SaveUpdateMessageOTP"] != null)
                                {
                                    ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResultOTP"]);
                                    ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessageOTP"].ToString();
                                }

                                GenerateOTP _GenerateOTP = new GenerateOTP();

                                if (TempData.Peek("OTP") == null)
                                {
                                    TempData["OTP"] = _GenerateOTP.Generate();
                                }

                                if (TempData.Peek("DisplayNameForOTPMail") != null && TempData.Peek("EmailForOTPMail") != null && TempData.Peek("OTP") != null)
                                {
                                    _GenerateOTP.GenerateMailFormatForOTP(TempData["DisplayNameForOTPMail"].ToString(), TempData["EmailForOTPMail"].ToString(), TempData.Peek("OTP").ToString());
                                    _GenerateOTP.SMSFOROTP(TempData["DisplayNameForOTPMail"].ToString(), TempData["ForOTPMobileNo"].ToString(), TempData.Peek("OTP").ToString());
                                    ViewBag.IsNewForm = "No";
                                }


                                TempData["vigilanceApplicationFormModel"] = vigilanceApplicationFormModel;
                                return View(vigilanceApplicationFormModel);
                            }
                            return (RedirectToAction("Create", new { @result = "Failed" }));

                        }
                        else
                        {
                            string Result = vigilanceApplicationFormModel.SaveUpdate("UPDATE", Convert.ToInt32(TempData.Peek("VigilanceID")));
                            return (RedirectToAction("Index", new { @result = "UpdateSuccess" }));
                        }
                    }
                    //ViewBag.SaveResult = 1;
                    //ViewBag.SaveUpdateMessage = "<h5> Captcha Entered Is Invalid. !!! <h5 /><br /> Kindly try again.";
                    ViewBag.IsNewForm = "Yes";
                    return View("Create", vigilanceApplicationFormModel);
                }
                else
                {
                    ViewBag.IsNewForm = "Yes";
                    // string q = MyExtensions.Encrypt("VigilanceID=" + 0);
                    // return View("Create", new { q });
                    return View("Create", vigilanceApplicationFormModel);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }



        public JsonResult FillCityByStateID(int StateID)
        {
            return Json(new SelectList(Master.FillCityByStateID(StateID), "Value", "Text"), JsonRequestBehavior.AllowGet);

        }

        public JsonResult FillPincodeByCityID(int CityID)
        {
            return Json(new SelectList(Master.FillPincodeByCityID(CityID), "Value", "Text"), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Encrypt(string plainText)
        {
            string EncryptedPass = string.Empty;
            if (!string.IsNullOrEmpty(plainText))
            {
                EncryptedPass = CommonDAL.Encrypt(plainText);
            }
            return Json(EncryptedPass, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            return Redirect("Login");
        }

        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = false;
            string fullpath = string.Empty;
            string extension = string.Empty;
            HttpPostedFileBase files = Request.Files["file"];
            if (files != null)
            {
                if (files.ContentLength > 0)
                {
                    extension = System.IO.Path.GetExtension(files.FileName);
                    string filename = files.FileName;
                    // check the file is openable or not
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Temp")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Temp"));
                    }
                    fullpath = Path.Combine(Server.MapPath("~/Temp/"), filename);
                    files.SaveAs(fullpath);

                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(fullpath);

                                oPdfReader.Close();
                                IsImg = true;
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                        else
                        {
                            System.Drawing.Image newImage = System.Drawing.Image.FromFile(fullpath);
                            IsImg = true;
                            if (System.IO.File.Exists(fullpath))
                            {
                                try
                                {
                                    System.IO.File.Delete(fullpath);
                                }
                                catch (Exception exs)
                                {
                                }
                            }
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior.AllowGet);
        }

        [EncryptedActionParameter]
        public void Document_View(string Filepath, int Complaintid)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlHelper sql = new SqlHelper();
                MySqlParameter[] spmLogin = {
                                          new MySqlParameter("P_COMPLAINT_ID", Complaintid)
                                        };
                ds = sql.getDataSet("PROC_GET_DOCUMENT_INFO", spmLogin, "");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["FILE_DATA"] != null)
                            {
                                string DocName = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
                                Byte[] bytes = (Byte[])ds.Tables[0].Rows[0]["FILE_DATA"];
                                Response.Buffer = true;
                                Response.Charset = "";
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                Response.ContentType = ds.Tables[0].Rows[0]["CONTENT_TYPE"].ToString();
                                Response.AddHeader("content-disposition", "attachment;filename=" + DocName + ds.Tables[0].Rows[0]["EXTENSION"].ToString());
                                Response.BinaryWrite(bytes);
                                Response.Flush();

                                HttpContext.ApplicationInstance.CompleteRequest();
                                Response.Close();

                            }
                            else
                            {
                                //CommonBLL.DisplayPopUpMessage(this, "No Attachments Found..", "");
                            }
                        }
                    }
                    else
                    {
                        //CommonDAL.DisplayPopUpMessage(this, "No Attachments Found..", "");
                        //msg
                    }
                }
                else
                {
                    //CommonDAL.DisplayPopUpMessage(this, "No Attachments Found..", "");
                    //msg
                }

            }
            catch (Exception ex)
            {

            }

        }

        //Praveen Kumar

        public static SelectList FillComplaintStatus()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataTable dt = new DataTable();
            dt.Columns.Add("StatusValue", typeof(string));
            dt.Columns.Add("StatusName", typeof(string));
            #endregion
            string[] arr2 = { "Registered", "Verification awaited", "Under Process", "Action Taken & Closed", "Forwarded to concerned Division" };            //SqlHelper oSqlHelper = new SqlHelper()
                                                                                                                                                              //Hashtable ht = new Hashtable();
                                                                                                                                                              //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_PINCODE", ht);
            for (int a = 0; a < arr2.Length; a++)
            {
                DataRow dr2 = dt.NewRow();
                dr2["StatusValue"] = arr2[a];
                dr2["StatusName"] = arr2[a];
                dt.Rows.Add(dr2);
            }
            SelectList sl = new SelectList(dt.AsDataView(), "StatusValue", "StatusName");
            return sl;
        }
        [HttpPost]
        public ActionResult UpdateStatus(VigilanceViewModel _VigilanceViewModel)
        {
            GenerateOTP _GenerateOTP = new GenerateOTP();
            if (Session["_VigilanceComplaintID"] != null)
            {
                if (_VigilanceViewModel._VigilanceApplicationFormModel.ComplaintStatus != "SELECT" && _VigilanceViewModel._VigilanceApplicationFormModel.ComplaintStatus != null)
                {
                    string UserName = _VigilanceViewModel._VigilanceApplicationFormModel.UserName;
                    string EmailID = Master.GetEmailIDByLoginName(UserName);
                    string MobileNo = Master.GetMobileNOByLoginName(UserName);
                    var VigilanceComplaintID = Session["_VigilanceComplaintID"];
                    string ApplicationRefNo = "CWC_2020_VIG_" + VigilanceComplaintID;
                    DataSet ds = new DataSet();
                    SqlHelper sql = new SqlHelper();
                    MySqlParameter[] spmLogin = {
                                              new MySqlParameter("P_VigilanceComplaintID",  VigilanceComplaintID),
                                              new MySqlParameter("P_ComplaintStatus",_VigilanceViewModel._VigilanceApplicationFormModel.ComplaintStatus),
                                               new MySqlParameter("P_Remarks",_VigilanceViewModel._VigilanceApplicationFormModel.Remarks)

                    };
                    string result = sql.execStoredProcudureInString("PROC_CHANGE_COMPLAINT_STATUS", spmLogin);
                    _GenerateOTP.GenerateMailForChangeStatus(ApplicationRefNo, _VigilanceViewModel._VigilanceApplicationFormModel.Name, EmailID, _VigilanceViewModel._VigilanceApplicationFormModel.Remarks, _VigilanceViewModel._VigilanceApplicationFormModel.ComplaintStatus, DateTime.Now.ToString("dd-MMM-yyyy"));

                    Session["_VigilanceComplaintID"] = null;

                }
                var uid = Session["Uid"];
               // var userdetails = Session["userdetails"].ToString();
               //var  details = userdetails.Split('(', '@', '#', '$', ')');
               // // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

               // var UserName1 = details[20];
                return RedirectToAction("index", new { eid = Models.Token.EncryptString(Convert.ToString(uid)) });
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}