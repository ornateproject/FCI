using CWC_CMS.Models;
using SIDCUL.Models;
using CaptchaMvc.HtmlHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
using System.Text.RegularExpressions;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Controllers
{
    [CustomExceptionHandlerFilter]
    public class LoginChangePasswordController : Controller
    {
        // GET: Default
        public ActionResult Index(object NameRef)
        {
            if (TempData["SaveResultOTP"] != null && TempData["SaveUpdateMessageOTP"] != null)
            {
                ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResultOTP"]);
                ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessageOTP"].ToString();
            }

            if (TempData["SaveUpdateMessage"] != null)
            {
                ViewBag.SaveResult = 1;
                ViewBag.SaveUpdateMessage = "Password must be at least 7 characters, no more than 15 characters, and must include at least one upper case letter(A-Z), one lower case letter(a-z), one numeric digit(0-9) and one special character.(@,<,>,%,&,$ etc.)";
            }
            if (Session["userdetails"] != null)
            {
                Uri myurl = Request.UrlReferrer;   //Added by Renu 15 Jan 2020
                if (myurl != null)
                {
                    if (NameRef != null)
                    {
                        string test = NameRef.ToString();
                        TempData["CancelMsg"] = NameRef.ToString();
                    }
                    return View();
                }
                else
                {
                    return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
                }
            }
            else
            {
                return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
            }

        }


        [HttpPost]
        public ActionResult Submit(LoginChangePasswordModel cls, string CaptchaText)
        {
            if (Session["captchastring"] == null)
            {
                Session["captchastring"] = "";
            }
            if (CaptchaText == HttpContext.Session["captchastring"].ToString())
            {
                if (ValidatePassword(CommonDAL.Decrypt(cls.NewPassword)))
                {
                    // insert opertion in Db
                }
                else
                {
                    TempData["SaveResultOTP"] = 1;
                    TempData["SaveUpdateMessageOTP"] = "Password must be at least 7 characters, no more than 15 characters, and must include at least one upper case letter, one lower case letter, one numeric digit and one special character.";

                    return (RedirectToAction("Index", "LoginChangePassword"));

                }
                int i = 0;
                int userrecno = Convert.ToInt32(Session["User_recno"]);
                string username = (Session["User_Name"]).ToString();

                #region Renu 03 Feb 2020 For Mysql Start 
                // Renu 03 Feb 2020 For Mysql Start 
                SqlHelper sql = new SqlHelper();
                MySqlParameter[] spmLogin = { new MySqlParameter("P_USER_RECNO",  Convert.ToInt32(Session["User_recno"])),
                                              new MySqlParameter("P_OLD_PASSWORD",  CommonDAL.Decrypt(cls.OldPassword)),
                                              new MySqlParameter("P_NEW_PASSWORD",  CommonDAL.Decrypt(cls.NewPassword)),
                                              new MySqlParameter("P_CHANGE_PASSWORD",  "F"),
                                              new MySqlParameter("P_CREATED_BY",  (Session["User_Name"]).ToString())
                                            };
                DataSet dsmenupass = sql.getDataSet("PROC_CHANGE_USER_PASSWORD", spmLogin, "");

                //MySave _mySave = new MySave("PROC_CHANGE_USER_PASSWORD");
                //_mySave.AddParameter("@P_USER_RECNO", Convert.ToInt32(Session["User_recno"]));
                //_mySave.AddParameter("@P_OLD_PASSWORD", CommonDAL.Decrypt(cls.OldPassword));
                //_mySave.AddParameter("@P_NEW_PASSWORD", CommonDAL.Decrypt(cls.NewPassword));
                //_mySave.AddParameter("@P_CHANGE_PASSWORD", "F");
                //_mySave.AddParameter("@P_CREATED_BY", (Session["User_Name"]).ToString());
                //DataSet dsmenupass = _mySave.GetDataByProcedure();

                /*For connection string on page only*/
                //string conquery = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
                //SqlConnection con = new SqlConnection(conquery);
                //SqlCommand com = new SqlCommand("proc_changepass", con);
                //con.Open();
                //com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("@user_name", cls.User_name);
                //com.Parameters.AddWithValue("@Oldpassword", cls.OldPassword);
                //com.Parameters.AddWithValue("@currentpassword", cls.ConfirmPassword);

                //i = (int)com.ExecuteScalar();

                if (dsmenupass != null && dsmenupass.Tables[0].Rows.Count > 0)
                {
                    int result = Convert.ToInt32(dsmenupass.Tables[0].Rows[0]["STATUS"]);
                    if (result > 0)
                    {
                        MySqlParameter[] spmLogin1 = { new MySqlParameter("P_USER_RECNO",  Convert.ToInt32(Session["User_recno"]))
                                            };
                        DataSet ds = sql.getDataSet("PROC_GET_EMAIL_ID", spmLogin1, "");

                        GenerateOTP _GenerateOTP = new GenerateOTP();
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string DisplayName = ds.Tables[0].Rows[0]["DISPLAY_NAME"].ToString().Trim();
                            string LoginName = ds.Tables[0].Rows[0]["LOGIN_NAME"].ToString().Trim();
                            string EmailID = ds.Tables[0].Rows[0]["EMAILID"].ToString().Trim();
                            string Password = ds.Tables[0].Rows[0]["PASSWORD"].ToString().Trim();
                            string DecrptPass = (Password);

                            // string Password1 = DecryptStringAES(Password);
                            // GenerateMailFormat(DisplayName, LoginName, EmailID, oCommonDAL.DecodeString(Password), Action_type);
                            _GenerateOTP.GenerateMailFormatForChangePassword(DisplayName, EmailID, LoginName, (Password));
                            ViewBag.SaveResult = 1;

                            TempData["SaveResultOTP"] = 1;
                            TempData["SaveUpdateMessageOTP"] = "User Password Changed Successfully and an email has been sent to the user with all Important details.";
                            return (RedirectToAction("Index", "Login", new { UserValid = "" }));
                        }
                        return View();
                    }
                    else if (result == -5)
                    {
                        TempData["SaveResultOTP"] = 1;
                        TempData["SaveUpdateMessageOTP"] = "Password should be different from last 3 passwords";
                        // return View();
                        return (RedirectToAction("Index", "LoginChangePassword", new { }));
                    }
                    else
                    {
                        TempData["SaveResultOTP"] = 1;
                        TempData["SaveUpdateMessageOTP"] = "Old Password is Not Correct";
                        return (RedirectToAction("Index", "LoginChangePassword", new { }));
                    }
                }
                else
                {
                    return View();
                }
                #endregion Renu 03 Feb 2020 For Mysql End
            }
            else
            {
                TempData["SaveResultOTP"] = 1;
                TempData["SaveUpdateMessageOTP"] = "Invalid Captcha....Please Try Again";
                return (RedirectToAction("Index", "LoginChangePassword", new { }));
            }
            //End
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

        [EncryptedActionParameter]
        public ActionResult CancelBtn()
        {
            //if(Convert.ToInt32(TempData["CancelMsg"]) == 1)
            //{
            return (RedirectToAction("Index", "VigilanceApplicationForm"));
            //}
            //else if (Convert.ToInt32(TempData["CancelMsg"]) == 2)
            //{
            //    return (RedirectToAction("Index", "Home"));
            //}
            //else
            //{
            //    return (RedirectToAction("Index", "Login"));
            //}
        }
    }
}