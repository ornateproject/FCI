using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Controllers
{
    public class CareerPortalResultGenerateController : Controller
    {
        public ActionResult Index()
        {

            string PageAddressParam = "~/CWC/188.95.36.104_8080/cwc/CWCCareerPortalResultFormat.html";
            string path = Server.MapPath(PageAddressParam);
            string content = System.IO.File.ReadAllText(path);
            string DesktopPath = Request.Url.Authority;
            ViewBag.DesktopPath = DesktopPath;
            CMSModel cmsModel = new CMSModel();
            cmsModel.PageAddress = PageAddressParam;
            cmsModel.InitialPageHTML = content;
            ViewBag.FillExam = Master.FillExamNew();
            return View(cmsModel);
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Index_Post()
        {
            CMSModel cmsModel = new CMSModel();
            TryUpdateModel(cmsModel);
            string fileLoc = Path.Combine(Server.MapPath("~/CareerPortalResultFormat/"), cmsModel.ExamName + ".html");

            if (System.IO.File.Exists(fileLoc))
            {
                System.IO.File.Delete(fileLoc);
            }
            string PageName = cmsModel.ExamName;

            FileStream fs = null;
            if (!System.IO.File.Exists(fileLoc))
            {
                using (fs = System.IO.File.Create(fileLoc))
                {

                }
            }

            string PageHTMLContent = cmsModel.FinalSubmitHTML.ToString();

            if (System.IO.File.Exists(fileLoc))
            {
                using (StreamWriter sw = new StreamWriter(fileLoc))
                {
                    sw.Write(PageHTMLContent);
                }
            }

            string BackupName = "NA";
            int result = cmsModel.SaveNewPageHTML(Path.Combine(Server.MapPath("~/CareerPortalOfferLetterFormat/"), PageName + ".html"), BackupName);
            if (result > 0)
            {
                return RedirectToAction("Index", "Home", new { @result = "Success" });
            }
            else
            {
                return RedirectToAction("Index", "Home", new { @result = "Failed" });
            }
        }
    }
}