using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
using System.Security.Cryptography;

namespace CWC_CMS.Controllers
{
    public class CMSMajorChangeController : Controller
    {
        //
        // GET: /CMSMajorChange/
        [EncryptedActionParameterAttribute]
        [CustomExceptionHandlerFilter]
        public ActionResult Index(object PageAddress, object MenuName)
        {
            if (Session["userdetails"] != null)  //Added by Renu 15 Jan 2020
            {
                Uri myurl = Request.UrlReferrer;   //Added by Renu 15 Jan 2020
                if (myurl != null)
                {
                    if (MenuName.ToString() == "Customize Tenders")
                    {
                        return RedirectToAction("Index", "CWCCppTender");
                    }
                    else if (MenuName.ToString() == "Customize Public Notices")
                    {
                        return RedirectToAction("Create", "PublicNotice", new { id = 1 });
                    }
                    else
                    {
                        string PageAddressParam = PageAddress.ToString();
                        string path = Server.MapPath(PageAddressParam);
                        string content = System.IO.File.ReadAllText(path);
                        string DesktopPath = Request.Url.Authority;
                        ViewBag.DesktopPath = DesktopPath;
                        CMSModel cmsModel = new CMSModel();
                        cmsModel.PageAddress = PageAddressParam;
                        cmsModel.InitialPageHTML = content;
                        return View(cmsModel);
                    }
                }
                else
                {
                    //return RedirectToAction("Index", "HomePage");
                    return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
                }
            }
            else
            {
                // return RedirectToAction("Index", "HomePage");
                return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
            }
            SessionFixation();
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

        [CustomExceptionHandlerFilter]
        [EncryptedActionParameterAttribute]
        public ActionResult CareerPortalIndex(object PageAddress)
        {
            string PageAddressParam = PageAddress.ToString();
            string path = Server.MapPath(PageAddressParam);
            string content = System.IO.File.ReadAllText(path);
            string DesktopPath = Request.Url.Authority;
            ViewBag.DesktopPath = DesktopPath;
            CMSModel cmsModel = new CMSModel();
            cmsModel.PageAddress = PageAddressParam;
            cmsModel.InitialPageHTML = content;
            return View(cmsModel);
        }



        [HttpPost]
        [ActionName("Index")]
        //  [ValidateAntiForgeryToken]
        [CustomExceptionHandlerFilter]
        public ActionResult Index_Post()
        {
            bool check = false;

            CMSModel cmsModel = new CMSModel();
            TryUpdateModel(cmsModel);
            string fileLoc = Server.MapPath(cmsModel.PageAddress);
            string PageHTMLContent = cmsModel.FinalSubmitHTML.ToString();
            PageHTMLContent = PageHTMLContent.Replace("<", "");
            if (PageHTMLContent.Contains("<"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains(">"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("script"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("<script>"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("</script>"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("alert"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("onerror"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("iframe"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("video"))
            {
                check = true;
            }
            else if (PageHTMLContent.Contains("audio"))
            {
                check = true;
            } 

            PageHTMLContent = PageHTMLContent.Replace(">", "");
            PageHTMLContent = PageHTMLContent.Replace("script", "");
            PageHTMLContent = PageHTMLContent.Replace("<script>", "");
            PageHTMLContent = PageHTMLContent.Replace("</script>", "");
            PageHTMLContent = PageHTMLContent.Replace("alert", "");
            PageHTMLContent = PageHTMLContent.Replace("onerror", "");
            PageHTMLContent = PageHTMLContent.Replace("iframe", "");
            PageHTMLContent = PageHTMLContent.Replace("video", "");
            PageHTMLContent = PageHTMLContent.Replace("audio", "");
            if (!check)
            {
                if (TempData["ContentSaltKey"] != null)
                {
                    string SaltKey = TempData.Peek("ContentSaltKey").ToString();
                    PageHTMLContent = CommonDAL.DecryptWithSaltKey(PageHTMLContent, SaltKey);
                    if (PageHTMLContent.ToLower() == "error")
                    {
                        TempData["ValidationMsg"] = "failed";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["ValidationMsg"] = "failed";
                    return RedirectToAction("Index", "Home");
                }

                string MenuName = Master.GET_MENU_NAME_BY_PAGE_ADDRESS(cmsModel.PageAddress);

                if (System.IO.File.Exists(fileLoc))
                {
                    System.IO.File.Move(Server.MapPath(cmsModel.PageAddress), Path.Combine(Server.MapPath("~/PagesBackupForRestorationPurposes/"), MenuName + System.DateTime.Now.ToString("yyyyMMddHHmmssfff")));
                }

                FileStream fs = null;
                if (!System.IO.File.Exists(fileLoc))
                {
                    using (fs = System.IO.File.Create(fileLoc))
                    {

                    }
                }

                if (System.IO.File.Exists(fileLoc))
                {
                    using (StreamWriter sw = new StreamWriter(fileLoc))
                    {
                        sw.Write(PageHTMLContent);
                    }
                }

                string BackupName = cmsModel.PageAddress + System.DateTime.Now + "Backup";
                int result = cmsModel.SaveHTML(Path.Combine(Server.MapPath("~/PagesBackupForRestorationPurposes/"), MenuName + System.DateTime.Now.ToString("yyyyMMddHHmmssfff")), BackupName);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Home", new { @result = "Success" });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { @result = "Failed" });
                }
            }
            else
            {
                TempData["ValidationMsg"] = "failed";
                return RedirectToAction("Index", "Home");
            }

        }

        [AllowAnonymous]
        [CustomExceptionHandlerFilter]
        public JsonResult SaveImageAndReturnItsPath(string Parameter)
        {


            WebClient client = new WebClient();
            string filepath = Path.Combine(Server.MapPath("~/images/"), "img" + System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            client.DownloadFile(Parameter, filepath);
            return Json(filepath, JsonRequestBehavior.AllowGet);

        }

        #region Encription
        [HttpPost]
        public JsonResult Encrypt()
        {
            string html = string.Empty;
            string value = string.Empty;
            if (Request.Form["html"] != null)
            {
                value = HttpUtility.UrlDecode(Request.Form["html"].ToString());
                string EncryptedPass = string.Empty;
                string SaltKey = CreateSalt(8);
                if (!string.IsNullOrEmpty(value))
                {
                    TempData["ContentSaltKey"] = SaltKey;
                    EncryptedPass = CommonDAL.EncryptWithSalt(value, SaltKey);
                }
                return Json(EncryptedPass, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.DenyGet);
            }
        }

        private string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        #endregion
    }
}