using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Controllers
{
    public class CareerPortalOfferLetterGenerateController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.FillExam = Master.FillExamNew();
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Index_Post()
        {
            CMSModel cmsModel = new CMSModel();
            TryUpdateModel(cmsModel);
            string fileLoc = Path.Combine(Server.MapPath("~/CareerPortalOfferLetterFormat/"), cmsModel.ExamName + ".html");
            
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