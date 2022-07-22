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
    public class HomePageController : Controller
    {
        //
        // GET: /HomePage/
        public ActionResult Index()
        {
            string PageAddressParam = "~/CWC/188.95.36.104_8080/cwc/index.html";
            //string TestParam = "~/CWC/188.95.36.104_8080/cwc/circulars_prmb.html";
            string q = MyExtensions.Encrypt("PageAddress=" + PageAddressParam);
           
            //string q1 = MyExtensions.Encrypt("PageAddress=" + TestParam);
            return (RedirectToAction("Create", "VigilanceApplicationForm"));
        }
	}
}