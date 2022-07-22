using CWC_CMS.Models;
using SIDCUL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
namespace CWC_CMS.Controllers
{
    [CustomExceptionHandlerFilter]
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }
        public CaptchaCodeAlphaNumeric ShowCaptchaImage()
        {
            return new CaptchaCodeAlphaNumeric();
        }

        [HttpPost]
        public ActionResult Create()
        {
                ChangePasswordModel _ChangePasswordModel = new ChangePasswordModel();
                TryUpdateModel(_ChangePasswordModel);
                TempData["LoginName"] = _ChangePasswordModel.LoginName;
                if (_ChangePasswordModel.ChangePasswordMethod == "SecurityQuestion")
                {
                    return RedirectToAction("ChangePasswordUsingSecurityQuestion");
                }
                else
                {
                    return RedirectToAction("ChangePasswordUsingOTP");
                }
        }

        public ActionResult ChangePasswordUsingOTP()
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
            string Email = "";
            string Name = "";
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@LoginName", TempData.Peek("LoginName").ToString());
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_EMAIL_AND_PHONE_FOR_VIGILANCE_BY_LOGIN_NAME", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    }
                }

            }

            
                _GenerateOTP.GenerateMailFormatForOTP(Name, Email, TempData.Peek("OTP").ToString());
            

            return View(_GenerateOTP);
        }

        public ActionResult ChangePasswordUsingSecurityQuestion()
        {
            return View();
        }
    }
}