using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using CWC_CMS.Models;

namespace CWC_CMS.Controllers
{
    public class RTIApplicationFormController : Controller
    {
        //
        // GET: /VigilanceApplicationForm/
        public ActionResult Index()
        {
            if (TempData["ApplicationRefno"] != null)
            {
                ViewBag.IsSuccessMsg = 1;
                ViewBag.ApplicationRefno = TempData["ApplicationRefno"].ToString();
            }
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index_Post()
        {

            RTIApplicationFormModel _VigilanceApplicationFormModelobj = new RTIApplicationFormModel();
            TryUpdateModel(_VigilanceApplicationFormModelobj);
            if(this.IsCaptchaValid("")){
                string ApplicationRefno =  _VigilanceApplicationFormModelobj.SaveUpdateDelete("INSERT");

                TempData["ApplicationRefno"] = ApplicationRefno;
                return (RedirectToAction("Index", new { @result = "Success" }));
            }
            return (RedirectToAction("Index", new { @result = "Failed" }));
        }
	}
}