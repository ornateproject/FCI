using CWC_CMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Controllers
{
    [Serializable()]
    public class CareerPortalLoginController : Controller
    {
        // GET: CareerPortalLogin
        public ActionResult Index()
        {
            CareerPortalLoginModel _CareerPortalLoginModel = new CareerPortalLoginModel();

            if (TempData["SaveResultOTP"] != null && TempData["SaveUpdateMessageOTP"] != null)
            {
                ViewBag.SaveResult = Convert.ToInt32(TempData["SaveResultOTP"]);
                ViewBag.SaveUpdateMessage = TempData["SaveUpdateMessageOTP"].ToString();
            }
            if (Request["UserValid"] != null)
            {

                ViewBag.Invalid = Request["UserValid"].ToString();

                if (TempData["UserLogin"] != null)
                {
                    ViewBag.abc = "yes";


                    string empid = TempData["employeeid"].ToString();
                    string UserLogin = TempData["UserLogin"].ToString();

                    ViewBag.empid = empid;
                    ViewBag.UserLogin = UserLogin;
                }

            }
            Session.Abandon();
            Session.Clear();
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create(CareerPortalLoginModel _login)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("RegistrationNo", _login.RegistrationNo == null ? "NA" : _login.RegistrationNo),
                                            new MySqlParameter("RollNo", _login.RollNo == null ? "NA" : _login.RollNo),
                                            new MySqlParameter("DOB", _login.DOB == null ? System.DateTime.MinValue : _login.DOB)
                                        };
            int ExamID = sql.execStoredProcudure("PROC_UNLOCK_ACCOUNT_UPDATE", spmLogin);
            #endregion	

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@RegistrationNo", _login.RegistrationNo == null ? "NA" : _login.RegistrationNo);
            //ht.Add("@RollNo", _login.RollNo == null ? "NA" : _login.RollNo);
            //ht.Add("@DOB", _login.DOB == null ? System.DateTime.MinValue : _login.DOB);
            //ht.Add("@IfUserValidThenExamID_out", 0);
            //int ExamID = osqlHelper.ExecuteQueryWithOutParam("PROC_CHECK_IF_USER_IS_VALID", ht);


            if (ExamID > 0)
            {

                if (_login.RollNo == null)
                {
                    _login.RollNo = Master.GET_ROLL_NO_BY_REGISTRATION_NO_AND_EXAM_ID(_login.RegistrationNo, ExamID);
                }
                Session["userdetailsCareer"] = _login.RegistrationNo + "(@#$)" + _login.RollNo + "(@#$)" + _login.DOB + "(@#$)" + ExamID;
                return (RedirectToAction("GenerateOfferLetter", "ExcelUpload"));
            }
            else
            {

                TempData["SaveResultOTP"] = 1;
                TempData["SaveUpdateMessageOTP"] = "Invalid User....Please Try Again";
                //invalid = "Invalid User....Please Try Again";

            }




            return (RedirectToAction("Index", "Login", new { }));
        }
    }
}