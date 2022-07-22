using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CWC_CMS.Common;
namespace CWC_CMS.Controllers
{
    [CustomExceptionHandlerFilter]
    public class CWCCuricullumController : Controller
    {
        //
        // GET: /CWCCuricullum/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult HRMSLandingPage()
        {
            Session["EmployeeOnly"] = "IsEmployee";
            return RedirectToAction("Index", "EmployeeOnlyCirculars");
        }


        

        public ActionResult ViewDocument(string EncryptedString)
        {
            ViewBag.EncryptedString = EncryptedString;
            return View();
        }


        public ActionResult CurriculumPartialViewLoad(string CurriculumID)
        {

            if (Session["EmployeeOnly"] != null)
            {
                ViewBag.IsEmployee = "Yes";
                return PartialView("~/Views/CWCCuricullum/_EmployeeOnlyCurriculum.cshtml");
            }
            else
            {
                if (CurriculumID == "GeneralCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_GeneralCurriculum.cshtml");
                }
                else if (CurriculumID == "FinanceCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_FinanceCurriculum.cshtml");
                }
                else if (CurriculumID == "CommercialCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_CommercialCurriculum.cshtml");
                }
                else if (CurriculumID == "EngineeringCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_EngineeringCurriculum.cshtml");
                }
                else if (CurriculumID == "InspectionCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_InspectionCurriculum.cshtml");
                }
                else if (CurriculumID == "MISCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_MISCurriculum.cshtml");
                }
                else if (CurriculumID == "PurchaseCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_PurchaseCurriculum.cshtml");
                }
                else if (CurriculumID == "TechnicalCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_TechnicalCurriculum.cshtml");
                }
                else if (CurriculumID == "TradeCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_TradeCurriculum.cshtml");
                }
                else if (CurriculumID == "VigilanceCurriculum")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_VigilanceCurriculum.cshtml");
                }
                else if (CurriculumID == "FCI")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_FCICurriculum.cshtml");
                }
                else if (CurriculumID == "GeneralWarehousing")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_GeneralWarehousingCurriculum.cshtml");
                }
                else if (CurriculumID == "DedicatedWarehousing")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_DedicatedWarehousingCurriculum.cshtml");
                }
                else if (CurriculumID == "CustomBonded")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_CustomBondedCurriculum.cshtml");
                }
                else if (CurriculumID == "Transport")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_TransportCurriculum.cshtml");
                }
                else if (CurriculumID == "Rebates")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_RebatesCurriculum.cshtml");
                }
                else if (CurriculumID == "Miscellaneous")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_MiscellaneousCurriculum.cshtml");
                }
                else if (CurriculumID == "MiscellaneousCategory")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_MiscellaneousCategoryCurriculum.cshtml");
                }
                else if (CurriculumID == "ISODocs")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_ISODocsCurriculum.cshtml");
                }
                else if (CurriculumID == "UpgradationList")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_UpgradationList.cshtml");
                }
                else if (CurriculumID == "SeniorityList")
                {
                    ViewBag.IsEmployee = "Yes";
                    return PartialView("~/Views/CWCCuricullum/_SeniorityList.cshtml");
                }
            }
            
            return null;
        }
	}
}