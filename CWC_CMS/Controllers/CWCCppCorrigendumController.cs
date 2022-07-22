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
    public class CWCCppCorrigendumController : Controller
    {
        public ActionResult Index()
        {
            List<CWCCppCorrigendumModel> CWCCppCorrigendumModelList = new List<CWCCppCorrigendumModel>();
            CWCCppCorrigendumModel CWCCppCorrigendumModelobj = new CWCCppCorrigendumModel();
            CWCCppCorrigendumModelList = CWCCppCorrigendumModelobj.GetCWCTenderListForIndex();

            return View(CWCCppCorrigendumModelList);
        }



        public ActionResult GenerateXML()
        {
            CWCCppCorrigendumModel CWCCppCorrigendumModelobj = new CWCCppCorrigendumModel();
            CWCCppCorrigendumModelobj.ConnectionXML();

            return View();
        }



        [EncryptedActionParameter]
        public ActionResult Create(object TenderID, object CorrigendumID)
        {

            int TenderIDParam = Convert.ToInt32(TenderID);
            int CorrigendumIDParam = Convert.ToInt32(CorrigendumID);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ViewBag.FillTenderType = Master.FillTenderType();
            ViewBag.FillCorrigendumType = Master.FillCorrigendumType();
            ViewBag.FillCorrigendumReason = Master.FillCorrigendumReason();
            ViewBag.FillFormOfContract = Master.FillFormOfContract();
            ViewBag.FillNoOfCover = Master.FillNoOfCover();
            ViewBag.FillTenderCategory = Master.FillTenderCategory();
            ViewBag.FillProductCategory = Master.FillProductCategory();
            ViewBag.FillContractType = Master.FillContractType();
            ViewBag.FillTendererClass = Master.FillTendererClass();
            ViewBag.FillTenderCurrency = Master.FillTenderCurrency();
            ViewBag.FillBidValidityDays = Master.FillBidValidityDays();
            ViewBag.FillOfflineInstruments = Master.FillOfflineInstruments();
            ViewBag.SystemMinDateTimeMinValue = System.DateTime.MinValue;
            ViewBag.FillState = Master.FillStateForTender();
            if (CorrigendumIDParam == 0)
            {
                CWCCppCorrigendumModel CWCCppCorrigendumModelobj = new CWCCppCorrigendumModel(TenderIDParam, 0);
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "Yes";
                ViewBag.IsNewForm = "Yes";
                return View();
            }
            else
            {
                CWCCppCorrigendumModel CWCCppCorrigendumModelobj = new CWCCppCorrigendumModel(TenderIDParam, CorrigendumIDParam);
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "No";
                ViewBag.IsNewForm = "No";
                return View(CWCCppCorrigendumModelobj);
            }

        }



        [EncryptedActionParameter]
        public ActionResult Delete(object CorrigendumID)
        {

            int CorrigendumIDParam = Convert.ToInt32(CorrigendumID);

            CWCCppCorrigendumModel CWCCppCorrigendumModelobj = new CWCCppCorrigendumModel();
            CWCCppCorrigendumModelobj.Delete(CorrigendumIDParam);
            return (RedirectToAction("Index", new { @result = "DeleteSuccess" }));
        }



        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            CWCCppCorrigendumModel CWCCppCorrigendumModelobj = new CWCCppCorrigendumModel();
            TryUpdateModel(CWCCppCorrigendumModelobj);

            if (CWCCppCorrigendumModelobj.FeePaymentMode == "Not Applicable")
            {
                CWCCppCorrigendumModelobj.IsExemptionAllowed = "NA";
                CWCCppCorrigendumModelobj.IsEMDFeeFixedOrPercentage = "NA";
                CWCCppCorrigendumModelobj.IsEmdExceptionAllowed = "NA";
            }

            if (TempData.Peek("IsNewForm").ToString() == "Yes")
            {
                CWCCppCorrigendumModelobj.SaveUpdate("INSERT", Convert.ToInt32(TempData.Peek("TenderID")));
                return (RedirectToAction("Index", new { @result = "Success" }));
            }
            else
            {
                CWCCppCorrigendumModelobj.SaveUpdate("UPDATE", Convert.ToInt32(TempData.Peek("TenderID")));
                return (RedirectToAction("Index", new { @result = "UpdateSuccess" }));
            }


        }


        public JsonResult FillCoverTypeByCoverNo(int CoverNo)
        {
            return Json(new SelectList(Master.FillCoverType(CoverNo), "Value", "Text"), JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetCoverNameByCoverType(int CoverTypeID)
        {
            return Json(Master.FillCoverNameByCoverTypeID(CoverTypeID), JsonRequestBehavior.AllowGet);

        }


        [EncryptedActionParameterAttribute]
        public ActionResult pdf_View(string filePath)
        {
            string path = filePath;
            string Extension = path.Substring((path.LastIndexOf('.') + 1));


            if (Extension == "jpg" || Extension == "png")
            {
                return File(path, "image/" + Extension);
            }
            return File(path, "application/pdf");

        }

        [HttpPost]
        public JsonResult CheckFile()
        {
            bool IsImg = false;
            string fullpath = string.Empty;
            string extension = string.Empty;
            HttpPostedFileBase files = Request.Files["file"];
            if (files != null)
            {
                if (files.ContentLength > 0)
                {
                    extension = System.IO.Path.GetExtension(files.FileName);
                    string filename = files.FileName;
                    // check the file is openable or not
                    if (!System.IO.Directory.Exists(Server.MapPath("~/Temp")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Temp"));
                    }
                    fullpath = Path.Combine(Server.MapPath("~/Temp/"), filename);
                    files.SaveAs(fullpath);

                    try
                    {
                        if (extension.ToLower() == ".pdf")
                        {
                            try
                            {
                                iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(fullpath);

                                oPdfReader.Close();
                                IsImg = true;
                                FileInfo doc = new FileInfo(fullpath);
                                if (doc.Exists)
                                {
                                    doc.Delete();
                                }
                            }
                            catch (iTextSharp.text.exceptions.InvalidPdfException)
                            {
                                IsImg = false;
                            }

                        }
                        else
                        {
                            System.Drawing.Image newImage = System.Drawing.Image.FromFile(fullpath);
                            IsImg = true;
                            if (System.IO.File.Exists(fullpath))
                            {
                                try
                                {
                                    System.IO.File.Delete(fullpath);
                                }
                                catch (Exception exs)
                                {
                                }
                            }
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        IsImg = false;

                        if (System.IO.File.Exists(fullpath))
                        {
                            try
                            {
                                System.IO.File.Delete(fullpath);
                            }
                            catch (Exception exs)
                            {
                            }
                        }
                        // Image.FromFile will throw this if file is invalid.  
                    }

                }
            }

            return Json(IsImg, JsonRequestBehavior.AllowGet);
        }
    }
}