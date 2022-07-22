using CWC_CMS.Models;
using SIDCUL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
namespace CWC_CMS.Controllers
{
    [CustomExceptionHandlerFilter]
    public class CWCViewPageController : Controller
    {
        //
        // GET: /CWCViewPage/
        [EncryptedActionParameterAttribute]
        public ActionResult Index(object PageAddress)
        {
            string PageAddressParam = PageAddress.ToString();
            string DesktopPath = Request.Url.Authority;
            ViewBag.DesktopPath = DesktopPath;
            if (PageAddressParam == "~/CWC/188.95.36.104_8080/cwc/index.html")
            {
                ViewBag.IsHomePage = true;
            }
            else
            {
                ViewBag.IsHomePage = false;
            }
            string path = Server.MapPath(PageAddressParam);
            string content = System.IO.File.ReadAllText(path);
            CMSModel cmsModel = new CMSModel();
            cmsModel.PageAddress = PageAddressParam;
            cmsModel.InitialPageHTML = content;
            return View(cmsModel);
        }


        public ActionResult RedirectToStaticPages(string PageAddressParam)
        {
            string q = MyExtensions.Encrypt("PageAddress=" + PageAddressParam);
            return RedirectToAction("Index", new { q });
        }

        [EncryptedActionParameterAttribute]
        public ActionResult IndexCareer()
        {
            string PageAddressParam = "~/CWC/CWCJOBS/index.html";
            string DesktopPath = Request.Url.Authority;
            ViewBag.DesktopPath = DesktopPath;
            ViewBag.IsHomePage = false;
            
            string path = Server.MapPath(PageAddressParam);
            string content = System.IO.File.ReadAllText(path);
            CMSModel cmsModel = new CMSModel();
            cmsModel.PageAddress = PageAddressParam;
            cmsModel.InitialPageHTML = content;
            return View(cmsModel);
        }


        public ActionResult Encryption()
        {
            string PageAddressParam = "~/CWC/188.95.36.104_8080/cwc/index.html";
            string q = MyExtensions.Encrypt("PageAddress=" + PageAddressParam);
            
            return View();
        }
	}
}