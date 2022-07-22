using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Controllers
{
    public class CWCCppTenderController : Controller
    {
        //
        // GET: /CWCCppTender/
        public ActionResult Index()
        {
            List<CWCCppTenderModel> CWCCppTenderModelList = new List<CWCCppTenderModel>();
            CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel();
            CWCCppTenderModelList = CWCCppTenderModelobj.GetCWCTenderListForIndex();

            return View(CWCCppTenderModelList);
        }


        public ActionResult ViewTenderList()
        {
            List<CWCCppTenderModel> CWCCppTenderModelList = new List<CWCCppTenderModel>();
            CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel();
            CWCCppTenderModelList = CWCCppTenderModelobj.GetCWCTenderListForIndex();

            return View(CWCCppTenderModelList);
        }

        [EncryptedActionParameter]
        public ActionResult ViewTenderData(object TenderID)
        {

            int TenderIDParam = Convert.ToInt32(TenderID);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ViewBag.FillTenderType = Master.FillTenderType();
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
            if (TenderIDParam == 0)
            {
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "Yes";
                ViewBag.IsNewForm = "Yes";
                return View();
            }
            else
            {
                CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel(TenderIDParam);
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "No";
                ViewBag.IsNewForm = "No";
                return View(CWCCppTenderModelobj);
            }

        }

        public ActionResult GenerateXML()
        {
            CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel();
            CWCCppTenderModelobj.ConnectionXML();

            return View();
        }



        [EncryptedActionParameter]
        public ActionResult Create(object TenderID)
        {

            int TenderIDParam = Convert.ToInt32(TenderID);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ViewBag.FillTenderType = Master.FillTenderType();
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
            if (TenderIDParam == 0)
            {
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "Yes";
                ViewBag.IsNewForm = "Yes";
                return View();
            }
            else
            {
                CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel(TenderIDParam);
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "No";
                ViewBag.IsNewForm = "No";
                return View(CWCCppTenderModelobj);
            }

        }



        [EncryptedActionParameter]
        public ActionResult Delete(object TenderID)
        {

            int TenderIDParam = Convert.ToInt32(TenderID);

            CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel();
            CWCCppTenderModelobj.Delete(TenderIDParam);
            return (RedirectToAction("Index", new { @result = "DeleteSuccess" }));
        }



        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel();
            TryUpdateModel(CWCCppTenderModelobj);

            if (CWCCppTenderModelobj.FeePaymentMode == "Not Applicable")
            {
                CWCCppTenderModelobj.IsExemptionAllowed = "NA";
                CWCCppTenderModelobj.IsEMDFeeFixedOrPercentage = "NA";
                CWCCppTenderModelobj.IsEmdExceptionAllowed = "NA";
                CWCCppTenderModelobj.ConnectionXML();
            }

            if (TempData.Peek("IsNewForm").ToString() == "Yes")
            {
                CWCCppTenderModelobj.SaveUpdate("INSERT", Convert.ToInt32(TempData.Peek("TenderID")));
                CWCCppTenderModelobj.ConnectionXML();
                return (RedirectToAction("Index", new { @result = "Success" }));
            }
            else
            {
                CWCCppTenderModelobj.SaveUpdate("UPDATE", Convert.ToInt32(TempData.Peek("TenderID")));
                CWCCppTenderModelobj.ConnectionXML();
                return (RedirectToAction("Index", new { @result = "UpdateSuccess" }));
            }
        }


        [EncryptedActionParameter]
        public ActionResult PrintTenderData(object TenderID)
        {
            int TenderIDParam = Convert.ToInt32(TenderID);
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ViewBag.FillTenderType = Master.FillTenderType();
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
            if (TenderIDParam == 0)
            {
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "Yes";
                ViewBag.IsNewForm = "Yes";
                return View();
            }
            else
            {
                CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel(TenderIDParam);
                TempData["TenderID"] = TenderIDParam;
                TempData["IsNewForm"] = "No";
                ViewBag.IsNewForm = "No";
                return View(CWCCppTenderModelobj);
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
            int index1 = filePath.IndexOf(@"\\Document");

            string Extension = path.Substring((path.LastIndexOf('.') + 1));

            #region Renu  2 Feb 2020 To change document path
            //To Replace string start
            if (path != string.Empty)
            {
                string backslash = @"\";
                string Item;
                string ItemList;
                int DelimIndex = 0;
                ItemList = path;

                DelimIndex = ItemList.IndexOf(backslash);

                string FinalString = "";
                int i = 0;
                while (DelimIndex > 0)
                {


                    Item = ItemList.Substring(0, DelimIndex);


                    if (i > 2)
                    {
                        if (i == 2)
                        {
                            FinalString = FinalString + Item;
                        }
                        else
                        {
                            FinalString = FinalString + '/' + Item;
                        }
                    }

                    i = i + 1;
                    int INDELI = (DelimIndex + 1);
                    int len = (ItemList.Length);
                    int increlent = ((ItemList.Length) - DelimIndex);
                    ItemList = ItemList.Substring(INDELI, increlent - 1);
                    DelimIndex = ItemList.IndexOf(backslash, 0);

                }
                FinalString = "http://49.50.87.108:8080" + FinalString + "/" + ItemList;
                string[] authorsList = FinalString.Split(new string[] { "/Document" }, StringSplitOptions.None);

                string original_path = (authorsList[1]).ToString();
                // path = FinalString.ToString();

                path = original_path;
            }

            //end
            #endregion Renu  2 Feb 2020 To change document path
            if (Extension == "jpg" || Extension == "png")
            {
                return File(path, "image/" + Extension);
            }
            path = "~/Document" + path;
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