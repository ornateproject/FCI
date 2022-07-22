using CaptchaMvc.HtmlHelpers;
using SIDCUL.Models;
using CWC_CMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Services;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Web.Mvc;
using CWC_CMS.Common;
using NLog;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Controllers
{
    [Serializable()]
    [CustomExceptionHandlerFilter]

    public class LoginController : Controller
    {
        //
        // GET: /Login/
        private static Logger logger = LogManager.GetCurrentClassLogger();
        CommonDAL oCommonDAL = new CommonDAL();
        public ActionResult Index()
        {
            LoginModel _loginModel = new LoginModel();
            try
            {
                Response.Cookies["ASP.Net_Session_Id"].Expires = DateTime.Now.AddSeconds(-30);
                Response.Cookies.Add(new HttpCookie("ASP.Net_Session_Id", Guid.NewGuid().ToString()));
                string LoginName = string.Empty;
                if (TempData["PasswordInputChance"] != null)
                {
                    LoginName = TempData["PasswordInputChance"].ToString();
                }
                else
                {
                    LoginName = "0";
                }
                ViewBag.buttonstatus = CheckPasswordValidation(LoginName);

                if (TempData["SaveResultOTP"] != null && TempData["SaveUpdateMessageOTP"] != null)
                {
                    ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResultOTP"]);
                    ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessageOTP"].ToString();
                }
                if (Request["UserValid"] != null)
                {

                    ViewBag.Invalid = Request["UserValid"].ToString();

                    if (TempData["UserLogin"] != null)
                    {
                        ViewBag.abc = "yes";



                        string empid = TempData["employeeid"].ToString();
                        string UserLogin = TempData["UserLogin"].ToString();

                        ViewBag.empid = empid;
                        ViewBag.UserLogin = UserLogin;
                    }

                }

              
            }
            catch (Exception ex)
            {
                string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                Response.Write(lineNumber);
                logger.Error("LoginIndex and line no:" + lineNumber + Environment.NewLine, ex.Message);
            }
            return View(_loginModel);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(LoginModel _login, string CaptchaText)
        {
        
            try
            {
                CheckSession();
                string PassMatch = "";
                string invalid = "";
                string username = "";
                string RoleName = "";
                int employeeid = 0;
                string UserLogin = "";
                string DecrptPass = string.Empty;
                string SaltKey = string.Empty;
                if (Session["SaltKey"] != null)
                {
                    SaltKey = Session["SaltKey"].ToString();
                    DecrptPass = CommonDAL.DecryptWithSaltKey(_login.Password, SaltKey);
                }
                int RoleID = 0;
                if (Session["captchastring"] == null)
                {
                    Session["captchastring"] = "";
                }
                if (CaptchaText == HttpContext.Session["captchastring"].ToString())
                {

                    // Renu 03 Feb 2020 For Mysql
                    SqlHelper sql = new SqlHelper();
                    DataSet ds = new DataSet();
                    MySqlParameter[] spmLogin = { new MySqlParameter("p_UserName",  (_login.UserName).ToLower()),
                                                  new MySqlParameter("p_P_PASSWORD", DecrptPass)
                                                };
                    DataSet dsmenupass = sql.getDataSet("PROC_GET_PASSWORD", spmLogin, "");

                    //MySave _mySave = new MySave("PROC_GET_PASSWORD");
                    //_mySave.AddParameter("@UserName", _login.UserName);
                    //_mySave.AddParameter("@P_PASSWORD", DecrptPass);

                    //DataSet dsmenupass = _mySave.GetDataByProcedure();
                    DataTable dtmenupass;
                    if (dsmenupass != null)
                        dtmenupass = dsmenupass.Tables[0];
                    else
                        dtmenupass = null;
                    if (dtmenupass != null && dtmenupass.Rows.Count > 0)
                    {
                        // Renu 16 Jan 2020 for lock status maintain and messages IF(
                        if (dtmenupass.Rows[0]["STATUS"].ToString() == "-5")
                        {
                            ViewBag.buttonstatus = 5;
                            TempData["PasswordInputChance"] = _login.UserName;
                            TempData["status"] = null;
                            TempData["SaveResultOTP"] = 1;
                            TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                            invalid = "Invalid User....Please Try Again";
                            return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
                        }
                        else if (dtmenupass.Rows[0]["STATUS"].ToString() == "-4")
                        {
                            ViewBag.buttonstatus = -4;
                            TempData["PasswordInputChance"] = _login.UserName;
                            TempData["SaveResultOTP"] = 1;
                            TempData["SaveUpdateMessageOTP"] = "Your account is Locked Please Unlock it";
                            invalid = "Dear User, Your account is Locked, Please Click on Unlock butoon or Contact to System Administrator!!";
                            return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
                        }
                        else
                        {
                            ViewBag.buttonstatus = 0;
                            PassMatch = dtmenupass.Rows[0]["Password"].ToString();
                            username = dtmenupass.Rows[0]["UserName"].ToString();
                            employeeid = Convert.ToInt32(dtmenupass.Rows[0]["EmployeeID"]);
                            UserLogin = dtmenupass.Rows[0]["LOGIN_NAME"].ToString();
                            RoleID = dtmenupass.Rows[0]["ROLE_RECNO"].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dtmenupass.Rows[0]["ROLE_RECNO"]);
                            RoleName = dtmenupass.Rows[0]["ROLE_NAME"].ToString();
                            Session["UserName"] = username;
                            // Start Added By Renu For Session Fixation on 27 Jan 2020
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            // now create a new cookie with this guid value 
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            string sessionauth = Session["AuthToken"].ToString();
                            string cookeauth = Request.Cookies["AuthToken"].Value;
                            if (Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
                            {
                                if (Request.Cookies["ASP.NET_SessionId"] != null && Request.Cookies["ASP.NET_SessionId"].Value != null)
                                {
                                    string newSessionID = Request.Cookies["ASP.NET_SessionId"].Value;
                                    Request.Cookies["ASP.NET_SessionId"].Value = Request.Cookies["ASP.NET_SessionId"].Value.Substring(0, 24);
                                    string _browserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent + "~" + Request.ServerVariables["REMOTE_ADDR"];
                                    string _sessionValue = Convert.ToString(Session["UserId"]) + "^" + DateTime.Now.Ticks + "^" + _browserInfo + "^" + System.Guid.NewGuid();
                                    byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
                                    string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
                                    Session["encryptedSession"] = _encryptedString;

                                }
                                else
                                {
                                    TempData["SaveResultOTP"] = 1;
                                    TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                                    return (RedirectToAction("Index", "Login", new { }));
                                }
                            }
                            else
                            {
                                TempData["SaveResultOTP"] = 1;
                                TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                                return (RedirectToAction("Index", "Login", new { }));
                            }
                            // End Added By Renu For Session Fixation on 27 Jan 2020
                            if (UserLogin.Equals((_login.UserName).ToLower()))
                            {

                            }
                            else
                            {
                                TempData["SaveResultOTP"] = 1;
                                TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                                invalid = "Invalid User....Please Try Again";
                                return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
                            }
                            ViewBag.employeeid = employeeid;
                            ViewBag.UserLogin = UserLogin;
                            TempData["employeeid"] = employeeid;
                            Session["Uid"] = employeeid;
                            TempData["UserLogin"] = UserLogin;
                            Session["User_recno"] = employeeid;
                            Session["User_Name"] = username;
                            Session["userdetails"] = employeeid + "(@#$)" + username + "(@#$)" + RoleID + "(@#$)" + RoleName + "(@#$)" + UserLogin;
                            Session["Role"] = RoleID;

                            if (DecrptPass == PassMatch)
                            {
                                string hostName = Dns.GetHostName();
                                String UserLogin1 = TempData["UserLogin"].ToString();
                                string CurrentURL = Request.Url.AbsoluteUri;
                                string LoginStatusDetails = "Success";
                                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                                int result = oCommonDAL.AuditTrail(UserLogin1, CurrentURL, myIP, 1, LoginStatusDetails);
                                if (result > 0)
                                {
                                    if (RoleID == 4)
                                    {
                                        
                                        return (RedirectToAction("Index", "VigilanceApplicationForm", new { eid = Models.Token.EncryptString(employeeid.ToString()) }));
                                    }
                                    else if (RoleID == 2)
                                    {
                                        return (RedirectToAction("Index", "VigilanceApplicationForm", new { eid = Models.Token.EncryptString(employeeid.ToString()) }));
                                    }
                                }
                                return (RedirectToAction("Index", "VigilanceApplicationForm", new { eid = Models.Token.EncryptString(employeeid.ToString()) }));
                            }
                            else
                            {

                                TempData["SaveResultOTP"] = 1;
                                TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                                invalid = "Invalid User....Please Try Again";

                            }
                            return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
                        }
                    }
                    else
                    {
                        invalid = "Invalid User....Please Try Again";
                        TempData["PasswordInputChance"] = _login.UserName;
                        return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
                    }
                    //End

                }
                else
                {
                    TempData["SaveResultOTP"] = 1;
                    TempData["SaveUpdateMessageOTP"] = "Invalid Captcha....Please Try Again";
                    invalid = "Invalid User....Please Try Again";
                    return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
                }
                //End
            }
            catch (Exception ex)
            {

                string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                Response.Write(lineNumber);
                logger.Error("LoginCreate and line no:" + lineNumber + Environment.NewLine, ex.Message);
                return (RedirectToAction("Index", "Login", new { UserValid = "" }));
            }

        }

        private void CheckSession()
        {
            string _browserInfo = Request.Browser.Browser
                        + Request.Browser.Version
                        + Request.UserAgent + "~"
                        + Request.ServerVariables["REMOTE_ADDR"];

            string _sessionValue = Convert.ToString(Session["UserId"]) + "^"
                                    + DateTime.Now.Ticks + "^"
                                    + _browserInfo + "^"
                                    + System.Guid.NewGuid();

            byte[] _encodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(_sessionValue);
            string _encryptedString = System.Convert.ToBase64String(_encodeAsBytes);
            Session["encryptedSession"] = _encryptedString;
            //Response.Cookies["ASPNET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["ASPNET_SessionId"].Expires = DateTime.Now.AddSeconds(-30);
            Response.Cookies.Add(new HttpCookie("ASPNET_SessionId", _encryptedString));
        }

        //praveen Kumar
        //15 Jan 2019 
        //For Forget Password
        //Start

        public ActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }
        public CaptchaCodeAlphaNumeric ShowCaptchaImage()
        {
            return new CaptchaCodeAlphaNumeric();
        }
        [HttpPost]
        public ActionResult ForgetPassword(LoginModel login, string CaptchaText)
        {
            SqlHelper sql = new SqlHelper();
            string invalid;
            if (Session["captchastring"] == null)
            {
                Session["captchastring"] = "";
            }
            if (CaptchaText == HttpContext.Session["captchastring"].ToString())
            {
                // Renu 03 Feb 2020 For Mysql
                DataSet ds = new DataSet();
                MySqlParameter[] spmLogin = { new MySqlParameter("p_UserName",  (login.UserName).ToLower())
                                                };
                ds = sql.getDataSet("PROC_GET_FORGET_PASSWORD", spmLogin, "");

                //Hashtable ht = new Hashtable();
                //ht.Add("@UserName", login.UserName);

                //DataSet ds = sql.ExecuteProcudere("PROC_GET_FORGET_PASSWORD", ht);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //string Password = ds.Tables[0].Rows[0]["Password"].ToString();
                    TempData["SaveResultOTP"] = 1;
                    ////TempData["SaveUpdateMessageOTP"] = "Your Password is " + Password;
                    ////invalid = "Your Password is " + Password;
                    TempData["SaveUpdateMessageOTP"] = "Your Password is sent to your registered mail";
                    invalid = "Your Password is sent to your registered mail";


                    //Renu Mail for password 20 Jan 2020

                    GenerateOTP _GenerateOTP = new GenerateOTP();

                    string DisplayName = ds.Tables[0].Rows[0]["DISPLAY_NAME"].ToString().Trim();
                    string LoginName = ds.Tables[0].Rows[0]["LOGIN_NAME"].ToString().Trim();
                    string EmailID = ds.Tables[0].Rows[0]["EMAILID"].ToString().Trim();
                    string Password = ds.Tables[0].Rows[0]["PASSWORD"].ToString().Trim();
                    string DecrptPass = (Password);

                    // string Password1 = DecryptStringAES(Password);
                    // GenerateMailFormat(DisplayName, LoginName, EmailID, oCommonDAL.DecodeString(Password), Action_type);
                    _GenerateOTP.GenerateMailFormatForChangePassword(DisplayName, EmailID, LoginName, (Password));

                    return (RedirectToAction("Index", "Login", new { UserValid = "" }));
                }
                else
                {
                    TempData["SaveResultOTP"] = 1;
                    TempData["SaveUpdateMessageOTP"] = "Invalid Username....Please Try Again";
                    invalid = "Invalid Username....Please Try Again";
                    return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
                }
            }
            else
            {
                TempData["SaveResultOTP"] = 1;
                TempData["SaveUpdateMessageOTP"] = "Invalid Captcha....Please Try Again";
                invalid = "Invalid User....Please Try Again";
                return (RedirectToAction("Index", "Login", new { UserValid = invalid }));
            }
        }

        //End


        // Renu 16 Jan 2020 for Unlock Account 
        //Start
        [EncryptedActionParameter]
        public ActionResult UnlockAccount(object name)
        {
            LoginModel _lg = new LoginModel();
            TryValidateModel(_lg);
            string username = (string)name;

            // Renu 03 Feb 2020 For Mysql
            SqlHelper sql = new SqlHelper();
            DataSet ds = new DataSet();
            MySqlParameter[] spmLogin = { new MySqlParameter("p_P_USER_NAME",  username)
                                                };
            int result = sql.execStoredProcudure("PROC_UNLOCK_ACCOUNT_UPDATE", spmLogin);

            if (result > 0)
            {
                TempData["SaveResultOTP"] = 1;
                TempData["SaveUpdateMessageOTP"] = "Your Account is Unlocked !! Please try Login again..  ";
                return (RedirectToAction("Index", "Login", new { }));
            }
            else
            {
                TempData["SaveResultOTP"] = 1;
                TempData["PasswordInputChance"] = username;
                TempData["SaveUpdateMessageOTP"] = "No Such user exist, Please Check your user name!";
                return (RedirectToAction("Index", "Login", new { }));
            }

        }

        public static int CheckPasswordValidation(string loginname)
        {
            if (loginname == "0")
            {
                return 0;
            }
            else
            {
                // Renu 03 Feb 2020 For Mysql
                SqlHelper sql = new SqlHelper();
                DataSet ds = new DataSet();
                MySqlParameter[] spmLogin = { new MySqlParameter("p_USER_NAME",  loginname)
                                                };
                ds = sql.getDataSet("PROC_LOCKED_STATUS", spmLogin, "");


                //Hashtable ht = new Hashtable();
                //ht.Add("@USER_NAME", loginname);
                //DataSet ds = sql.ExecuteProcudere("PROC_LOCKED_STATUS", ht);

                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "-4")
                    return -4;
                else
                    return 0;


            }
        }

        [HttpPost]
        public JsonResult Encrypt(string plainText)
        {
            string EncryptedPass = string.Empty;
            string SaltKey = CreateSalt(8);
            if (!string.IsNullOrEmpty(plainText))
            {
                Session["SaltKey"] = SaltKey;
                TempData["SaltKey"] = SaltKey;
                EncryptedPass = CommonDAL.EncryptWithSalt(plainText, SaltKey);
            }
            return Json(EncryptedPass, JsonRequestBehavior.AllowGet);
        }

        private string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private string GenerateHashKey()
        {

            StringBuilder myStr = new StringBuilder();
            myStr.Append(Request.Browser.Browser);
            myStr.Append(Request.Browser.Platform);
            myStr.Append(Request.Browser.MajorVersion);
            myStr.Append(Request.Browser.MinorVersion);
            //myStr.Append(Request.LogonUserIdentity.User.Value);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
            return Convert.ToBase64String(hashdata);

        }

        public ActionResult LogoutFun()
        {
            LoginModel _loginModel = new LoginModel();
            if (Session["UserName"] != null)
            {
                string hostName = Dns.GetHostName();
                String UserLogin = TempData["UserLogin"].ToString();
                string CurrentURL = Request.Url.AbsoluteUri;
                string LoginStatusDetails = "Success";
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                int result = oCommonDAL.AuditTrail(UserLogin, CurrentURL, myIP, 2, LoginStatusDetails);
                if (result > 0)
                {
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Clear();
                    if (Request.Cookies["ASPNET_SessionId"] != null)
                    {
                        string any = Response.Cookies["ASPNET_SessionId"].Value;
                        Response.Cookies["ASPNET_SessionId"].Value = string.Empty;
                        Response.Cookies["ASPNET_SessionId"].Value = Request.Cookies["ASPNET_SessionId"].Value + GenerateHashKey();
                        Response.Cookies["ASPNET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    if (Request.Cookies["AuthToken"] != null)
                    {

                        string any1 = Response.Cookies["AuthToken"].Value;
                        string any11 = Response.Cookies["AuthToken"].ToString();
                        Response.Cookies["AuthToken"].Value = string.Empty;
                        Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    if (Request.Cookies["__AntiXsrfToken"] != null)
                    {
                        Response.Cookies["__AntiXsrfToken"].Value = string.Empty;
                        Response.Cookies["__AntiXsrfToken"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    string newSessionID = Request.Cookies["ASPNET_SessionId"].Value;
                    return (RedirectToAction("Index", "Login", new { UserValid = "" }));
                }
                else
                {
                    Session.RemoveAll();
                    Session.Abandon();
                    Session.Clear();
                    if (Request.Cookies["ASPNET_SessionId"] != null)
                    {
                        string any = Response.Cookies["ASPNET_SessionId"].Value;
                        Response.Cookies["ASPNET_SessionId"].Value = string.Empty;
                        Response.Cookies["ASPNET_SessionId"].Value = Request.Cookies["ASPNET_SessionId"].Value + GenerateHashKey();
                        Response.Cookies["ASPNET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    if (Request.Cookies["AuthToken"] != null)
                    {

                        string any1 = Response.Cookies["AuthToken"].Value;
                        string any11 = Response.Cookies["AuthToken"].ToString();
                        Response.Cookies["AuthToken"].Value = string.Empty;
                        Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    if (Request.Cookies["__AntiXsrfToken"] != null)
                    {
                        Response.Cookies["__AntiXsrfToken"].Value = string.Empty;
                        Response.Cookies["__AntiXsrfToken"].Expires = DateTime.Now.AddMonths(-20);
                    }
                    string newSessionID = Request.Cookies["ASPNET_SessionId"].Value;
                    return (RedirectToAction("Index", "Login", new { UserValid = "" }));
                }

                //Added by sachin 23-01-2020
                Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                Response.Cache.SetValidUntilExpires(false);
                Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();

                //Clear cookies
                string[] cookies = Request.Cookies.AllKeys;
                foreach (string cookie in cookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }
                //

            }
            else
            {
                //return View(_loginModel);
                return RedirectToAction("Index");
            }

        }
        //End
    }
}