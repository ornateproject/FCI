using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Controllers
{
    public class HomeController : Controller
    {



        public ActionResult Index()
        {
            Uri myurl = Request.UrlReferrer;   //Added by Renu 15 Jan 2020
            if (myurl != null)
            { 
                if (Session["userdetails"] == null)
                {
                    return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
                }
                SessionFixation();
                if (TempData["ValidationMsg"] != null)
                {
                    ViewBag.ValidationMsg = "T";
                }

                List<HomeModel> homeModelList = new List<HomeModel>();
                HomeModel homeModel = new HomeModel();
                homeModelList = homeModel.GetListOfMenusToEdit();
                return View(homeModelList);
            }
            else
            {
                TempData["SaveResultOTP"] = 1;
                TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                return (RedirectToAction("Index", "Login", new { UserValid = "" }));
            }
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

        public ActionResult CareerIndex()
        {
            if (Session["userdetails"] == null)
            {
                return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
            }
            if (Session["userdetailsCareer"] != null)
            {
                string userdetails = null;
                string[] details = null;

                userdetails = Session["userdetailsCareer"].ToString();
                details = userdetails.Split('(', '@', '#', '$', ')');
                // _omChargesMasterModel.ActionPerformerEmpid = Convert.ToInt32(details[0]);

            }
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}