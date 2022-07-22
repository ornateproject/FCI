using System;
using CWC_CMS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;


namespace CWC_CMS.Controllers
{
    public class PublicNoticeController : Controller
    {
        // GET: PublicNotice
        public ActionResult Index()
        {
            List<PublicNoticeModel> PublicNoticeModelList = new List<PublicNoticeModel>();
            PublicNoticeModel PublicNoticeModelobj = new PublicNoticeModel();
            PublicNoticeModelList = PublicNoticeModelobj.GetPublicNoticeDetails();

            return View(PublicNoticeModelList);

        }


        [EncryptedActionParameter]
        public ActionResult Create(object PublicNoticeID)
        {
            if (PublicNoticeID.ToString() == "System.Object")
            {
                PublicNoticeID = 1;
            }
            int IDParam = Convert.ToInt32(PublicNoticeID);

            if (IDParam == 0)
            {
                TempData["IDParam"] = IDParam;
                TempData["IsNewForm"] = "Yes";
                ViewBag.IsNewForm = "Yes";
                return View();
            }
            else
            {
                List<PublicNoticeModel> PublicNoticeModelList = new List<PublicNoticeModel>();
                PublicNoticeModel CWCCppTenderModelobj = new PublicNoticeModel(0);
                // CWCCppTenderModel CWCCppTenderModelobj = new CWCCppTenderModel(TenderIDParam);
                TempData["IDParam"] = IDParam;
                TempData["IsNewForm"] = "No";
                ViewBag.IsNewForm = "No";
                return View(CWCCppTenderModelobj);
            }

        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create()
        {
            PublicNoticeModel PublicNoticeModelObj = new PublicNoticeModel();
            TryUpdateModel(PublicNoticeModelObj);

            if (TempData.Peek("IsNewForm").ToString() == "Yes")
            {
                PublicNoticeModelObj.SaveUpdatePublicNotices();
                return (RedirectToAction("Index", new { }));
            }
            else
            {
                PublicNoticeModelObj.SaveUpdatePublicNotices();
                return (RedirectToAction("Create", "PublicNotice", new { id = 0 }));
            }


        }

        [EncryptedActionParameter]
        public ActionResult Document_View(string Filepath, int noticeid, int filecheck)
        {
            //try
            //{
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                          new MySqlParameter("P_PUBLIC_NOTICE_ID", noticeid),
                                          new MySqlParameter("P_FILE_CHECK", filecheck)
                                        };
            ds = sql.getDataSet("PROC_GET_DOCUMENT_INFO_FOR_PUBLIC_NOTICES", spmLogin, "");

            string path = "";
            if (filecheck == 0)
                path = ds.Tables[0].Rows[0]["ENGLISH_DOCFILEPATH"].ToString();
            else
                path = ds.Tables[0].Rows[0]["HINDI_DOCFILEPATH"].ToString();

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
                string[] authorsList = FinalString.Split(new string[] { "/Docs" }, StringSplitOptions.None);

                string original_path = (authorsList[1]).ToString();
                // path = FinalString.ToString();

                path = original_path;
            }

            //end
            #endregion Renu  2 Feb 2020 To change document path
            if (Extension == "jpg" || Extension == "png")
            {

            }
            path = "~/Docs" + path;
            return File(path, "application/pdf");
            //if (ds != null)
            //{
            //    if (ds.Tables.Count > 0)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            if (ds.Tables[0].Rows[0]["FILE_DATA"] != null)
            //            {
            //                string DocName = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
            //                Byte[] bytes = (Byte[])ds.Tables[0].Rows[0]["FILE_DATA"];
            //                Response.Buffer = true;
            //                Response.Charset = "";
            //                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //                Response.ContentType = ds.Tables[0].Rows[0]["CONTENT_TYPE"].ToString();
            //                Response.AddHeader("content-disposition", "attachment;filename=" + DocName + ds.Tables[0].Rows[0]["EXTENSION"].ToString());
            //                Response.BinaryWrite(bytes);
            //                Response.Flush();

            //                HttpContext.ApplicationInstance.CompleteRequest();
            //                Response.Close();

            //            }
            //            else
            //            {
            //                //CommonBLL.DisplayPopUpMessage(this, "No Attachments Found..", "");
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //CommonDAL.DisplayPopUpMessage(this, "No Attachments Found..", "");
            //        //msg
            //    }
            //}
            //else
            //{
            //    //CommonDAL.DisplayPopUpMessage(this, "No Attachments Found..", "");
            //    //msg
            //}

            //}
            //catch (Exception ex)
            //{

            //}

        }

        [EncryptedActionParameter]
        public ActionResult Delete(object publicNoticeId)
        {

            int publicNoticeIdParam = Convert.ToInt32(publicNoticeId);

            PublicNoticeModel PublicNoticeModelobj = new PublicNoticeModel();
            PublicNoticeModelobj.Delete(publicNoticeIdParam);
            return (RedirectToAction("Create", "PublicNotice", new { id = 0 }));
        }
    }
}