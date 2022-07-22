using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
using System.Security.Cryptography;
namespace CWC_CMS.Controllers
{
    [CustomExceptionHandlerFilter]
    public class CMSNewPageController : Controller
    {
        //
        // GET: /CMSNewPage/
        public ActionResult Index()
        {
            if (Session["userdetails"] == null)
            {
                return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
            }

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        // [ValidateAntiForgeryToken]
        public ActionResult Index_Post()
        {
            bool check = false;
            CMSModel cmsModel = new CMSModel();
            TryUpdateModel(cmsModel);

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


                string fileLoc = Path.Combine(Server.MapPath("~/NewPages/"), cmsModel.PageName + ".html");

                string PageName = cmsModel.PageName;

                FileStream fs = null;
                if (!System.IO.File.Exists(fileLoc))
                {
                    using (fs = System.IO.File.Create(fileLoc))
                    {

                    }
                }

                //string PageHTMLContent = cmsModel.FinalSubmitHTML.ToString();

                if (System.IO.File.Exists(fileLoc))
                {
                    using (StreamWriter sw = new StreamWriter(fileLoc))
                    {
                        sw.Write(PageHTMLContent);
                    }
                }

                string BackupName = "NA";
                int result = cmsModel.SaveNewPageHTML(Path.Combine(Server.MapPath("~/NewPages/"), PageName + ".html"), BackupName);
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