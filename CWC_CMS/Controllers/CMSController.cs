using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
namespace CWC_CMS.Controllers
{
    [CustomExceptionHandlerFilter]
    public class CMSController : Controller
    {
        //
        // GET: /CMS/

        [EncryptedActionParameterAttribute]
        public ActionResult Index(object PageAddress)
        {
            string PageAddressParam = PageAddress.ToString();
            string path = Server.MapPath(PageAddressParam);
            string content = System.IO.File.ReadAllText(path);
            CMSModel cmsModel = new CMSModel();
            cmsModel.PageAddress = PageAddressParam;
            cmsModel.InitialPageHTML = content;
            return View(cmsModel);
        }


        


        [HttpPost]
        [ActionName("Index")]

        public ActionResult Index_Post()
        {

            CMSModel cmsModel = new CMSModel();
            TryUpdateModel(cmsModel);
            string fileLoc = Server.MapPath(cmsModel.PageAddress);

            if (System.IO.File.Exists(fileLoc))
            {
                System.IO.File.Delete(fileLoc);
            }

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
            //string content = System.IO.File.ReadAllText(path);
            //return View((object)content);
            return RedirectToAction("Index", "Home", new { @result = "Success" });
        }



       
	}
}