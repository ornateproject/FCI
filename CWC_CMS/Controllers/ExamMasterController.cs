using CWC_CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Controllers
{
    public class ExamMasterController : Controller
    {

        int location = 0, role = 0, UserRecno;
        string UserLoginName = "";
        public ActionResult Index()
        {
            ExamMasterModel ExamMasterModel = new ExamMasterModel();
            List<ExamMasterModel> _ExamMasterModel = new List<ExamMasterModel>();

            _ExamMasterModel = ExamMasterModel.GetDataForIndex();


            return View(_ExamMasterModel);

        }

        [EncryptedActionParameterAttribute]
        public ActionResult Create(object ExamID)
        {
            int ExamIDLocal = Convert.ToInt32(ExamID);

            
            if (ExamIDLocal != 0)
            {

                ExamMasterModel _ExamMasterModel = new ExamMasterModel(ExamIDLocal);
                TempData["IsNewForm"] = "No";
                return View(_ExamMasterModel);

            }
            else
            {
                ExamMasterModel _ExamMasterModel = new ExamMasterModel();
                TempData["IsNewForm"] = "Yes";
                return View();
            }

        }


        [EncryptedActionParameterAttribute]
        public ActionResult ViewExams(object ExamID)
        {
            int ExamIDLocal = Convert.ToInt32(ExamID);


            if (ExamIDLocal != 0)
            {

                ExamMasterModel _ExamMasterModel = new ExamMasterModel(ExamIDLocal);
                TempData["IsNewForm"] = "No";
                return View(_ExamMasterModel);

            }
            else
            {
                ExamMasterModel _ExamMasterModel = new ExamMasterModel();
                TempData["IsNewForm"] = "Yes";
                return View();
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_post()
        {
            ExamMasterModel _ExamMasterModel = new ExamMasterModel();

            try
            {
                TryUpdateModel(_ExamMasterModel);
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();

                if (ModelState.IsValid)
                {


                    if (TempData.Peek("IsNewForm").ToString() == "No")
                    {

                        _ExamMasterModel.SaveUpdate("UPDATE");
                        return (RedirectToAction("Index", new { @result = "UpdateSuccess" }));
                    }

                    else
                    {

                        _ExamMasterModel.ExamID = 0;
                        _ExamMasterModel.SaveUpdate("INSERT");
                        return (RedirectToAction("Index", new { @result = "Success" }));
                    }
                }
                // TODO: Add insert logic here
                else
                {
                    
                    return View();
                }

            }
            catch
            {
                return (RedirectToAction("Index", new { @result = "Failed" }));
            }

        }



        [EncryptedActionParameterAttribute]
        public ActionResult Delete(object ExamID)
        {
            
            int ExamIDLocal = Convert.ToInt32(ExamID);
            ExamMasterModel _ExamMasterModel = new ExamMasterModel();
            _ExamMasterModel.Delete(ExamIDLocal);
            return (RedirectToAction("Index", new { @result = "DeleteSuccess" }));
        }
	}
}