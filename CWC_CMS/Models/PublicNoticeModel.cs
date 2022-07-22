
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace CWC_CMS.Models
{
    public class PublicNoticeModel
    {

        public int? publicNotiecsId { get; set; }

        [Required(ErrorMessage = "Description is required Field")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is Requried")]
        public DateTime NoticeDate { get; set; }

        public HttpPostedFileBase EnglishDocument { get; set; }

        public string EnglishDocumentPath { get; set; }

        public HttpPostedFileBase HindiDocument { get; set; }

        public string HindiDocumentPath { get; set; }

        public List<PublicNoticeModel> PublicNoticeList { get; set; }

        public int SaveUpdatePublicNotices()
        {
            int result = 0;
            string createdby = "";
            Byte[] bytes = null;
            string contenttype = "";
            string filename = "";
            string extension = "";
            string EnglishfilePath = "";
            Byte[] bytes1 = null;
            string contenttype1 = "";
            string filename1 = "";
            string extension1 = "";
            string HindifilePath = "";
            try
            {
                DataSet ds = new DataSet();

                for (int i = 0; i < PublicNoticeList.Count; i++)
                {

                    //For English Document
                    if (PublicNoticeList[i].EnglishDocumentPath == null && PublicNoticeList[i].EnglishDocument != null && PublicNoticeList[i].EnglishDocument.ContentLength > 0)
                    {
                        // check the file is openable or not
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp")))    // for mvc 
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp"));
                        }

                        //string path = HttpContext.Current.Server.MapPath("~\\Temp\\");   //for asp
                        string path = HttpContext.Current.Server.MapPath("~/Temp/");
                        bool FolderExists = Directory.Exists(path);
                        if (!FolderExists)
                        {
                            Directory.CreateDirectory(path);
                        }
                        // PublicNoticeList[i].EnglishDocument.SaveAs(path + PublicNoticeList[i].EnglishDocument.FileName);   //for asp
                        string sourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), PublicNoticeList[i].EnglishDocument.FileName);
                        PublicNoticeList[i].EnglishDocument.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), PublicNoticeList[i].EnglishDocument.FileName));
                        EnglishfilePath = path + PublicNoticeList[i].EnglishDocument.FileName;
                        filename = Path.GetFileName(PublicNoticeList[i].EnglishDocument.FileName);
                        extension = Path.GetExtension(filename);
                        contenttype = PublicNoticeList[i].EnglishDocument.ContentType;

                        //FileStream fs = new FileStream(EnglishfilePath, FileMode.Open, FileAccess.Read);
                        //BinaryReader br = new BinaryReader(fs);
                        //bytes = br.ReadBytes((Int32)fs.Length);
                        //FileInfo doc = new FileInfo(EnglishfilePath);
                        //try { br.Close(); fs.Close(); if (doc.Exists) { doc.Delete(); } }
                        //catch (Exception) { throw; }

                        if (PublicNoticeList[i].EnglishDocument != null)
                        {
                            string targetPath = @"~/Docs/PublicNoticesDoc/";
                            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(targetPath)))
                            {
                                System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(targetPath));
                            }
                            // string destniationpath = Path.Combine(@"~/Docs/PublicNoticesDoc/", PublicNoticeList[i].EnglishDocument.FileName);   //for asp
                            //PublicNoticeList[i].EnglishDocument.SaveAs(fileName);    // for asp
                            //PublicNoticeList[i].EnglishDocument.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Docs/PublicNoticesDoc/"), PublicNoticeList[i].EnglishDocument.FileName));
                            string destiniationpath = Path.Combine(HttpContext.Current.Server.MapPath(targetPath), PublicNoticeList[i].EnglishDocument.FileName); // for mvc
                            FileInfo fileToDownload = new FileInfo(destiniationpath);
                            if (fileToDownload.Exists)
                            { fileToDownload.Delete(); }
                            else
                            {

                            }
                            System.IO.File.Move(sourcePath, destiniationpath);
                            EnglishfilePath = destiniationpath;
                            //FileInfo fileToDownload = new FileInfo(destiniationpath);
                            //if (fileToDownload.Exists)
                            //{ }
                            //else
                            //{

                            //}
                        }


                    }
                    else
                    {
                        EnglishfilePath = PublicNoticeList[i].EnglishDocumentPath;
                        if (EnglishfilePath == null)
                            EnglishfilePath = "";
                    }


                    // For Hindi Document
                    if (PublicNoticeList[i].HindiDocumentPath == null && PublicNoticeList[i].HindiDocument != null && PublicNoticeList[i].HindiDocument.ContentLength > 0)
                    {
                        // check the file is openable or not
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp")))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp"));
                        }
                        //string path = HttpContext.Current.Server.MapPath("~\\Temp\\");   //for asp
                        string path = HttpContext.Current.Server.MapPath("~/Temp/");
                        bool FolderExists = Directory.Exists(path);
                        if (!FolderExists)
                        {
                            Directory.CreateDirectory(path);
                        }
                        // PublicNoticeList[i].HindiDocument.SaveAs(path + PublicNoticeList[i].HindiDocument.FileName);   for asp
                        string sourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), PublicNoticeList[i].HindiDocument.FileName);
                        PublicNoticeList[i].HindiDocument.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), PublicNoticeList[i].HindiDocument.FileName));

                        HindifilePath = path + PublicNoticeList[i].HindiDocument.FileName;
                        filename1 = Path.GetFileName(PublicNoticeList[i].HindiDocument.FileName);
                        extension1 = Path.GetExtension(filename1);
                        contenttype1 = PublicNoticeList[i].HindiDocument.ContentType;
                        //FileStream fs = new FileStream(HindifilePath, FileMode.Open, FileAccess.Read);
                        //BinaryReader br = new BinaryReader(fs);
                        //bytes1 = br.ReadBytes((Int32)fs.Length);
                        //FileInfo doc = new FileInfo(HindifilePath);
                        //try { br.Close(); fs.Close(); if (doc.Exists) { doc.Delete(); } }
                        //catch (Exception) { throw; }

                        if (PublicNoticeList[i].HindiDocument != null)
                        {
                            string targetPath = @"~/Docs/PublicNoticesDoc/";
                            if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(targetPath)))
                            {
                                System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(targetPath));
                            }
                            // string destiniationpath = Path.Combine(@"~/Docs/PublicNoticesDoc/", PublicNoticeList[i].HindiDocument.FileName);   //for asp
                            //PublicNoticeList[i].HindiDocument.SaveAs(fileName);    // for asp
                            //PublicNoticeList[i].HindiDocument.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Docs/PublicNoticesDoc/"), PublicNoticeList[i].HindiDocument.FileName));    // for asp
                            string destiniationpath = Path.Combine(HttpContext.Current.Server.MapPath(targetPath), PublicNoticeList[i].HindiDocument.FileName); // for mvc
                            FileInfo fileToDownload = new FileInfo(destiniationpath);
                            if (fileToDownload.Exists)
                            { fileToDownload.Delete(); }
                            else
                            {

                            }
                            System.IO.File.Move(sourcePath, destiniationpath);
                            HindifilePath = destiniationpath;
                            //FileInfo fileToDownload = new FileInfo(destiniationpath);
                            //if (fileToDownload.Exists)
                            //{ }
                            //else
                            //{

                            //}

                        }
                    }
                    else
                    {
                        HindifilePath = PublicNoticeList[i].HindiDocumentPath;
                    }
                    int mainid = 0;
                    if (PublicNoticeList[i].publicNotiecsId == null)
                        mainid = 0;
                    else
                        mainid = Convert.ToInt32(PublicNoticeList[i].publicNotiecsId);
                    SqlHelper sql = new SqlHelper();
                    MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_PUBLIC_NOTICE_ID", (PublicNoticeList[i].publicNotiecsId == null ? 0 : PublicNoticeList[i].publicNotiecsId)),
                                            new MySqlParameter("P_NOTICE_DATE", PublicNoticeList[i].NoticeDate),
                                            new MySqlParameter("P_DESCRIPTION", PublicNoticeList[i].Description),

                                            new MySqlParameter("P_ENGLISH_DOCFILEPATH", EnglishfilePath== null ? "" : EnglishfilePath),
                                            new MySqlParameter("P_ENGLISH_CONTENT_TYPE", contenttype),
                                            new MySqlParameter("P_ENGLISH_EXTENSION", extension),
                                            new MySqlParameter("P_ENGLISH_FILE_NAME", filename),
                                            new MySqlParameter("P_HINDI_DOCFILEPATH", HindifilePath== null ? "" : HindifilePath),
                                            new MySqlParameter("P_HINDI_CONTENT_TYPE", contenttype1),
                                            new MySqlParameter("P_HINDI_EXTENSION", extension1),
                                            new MySqlParameter("P_HINDI_FILE_NAME", filename1),
                                            new MySqlParameter("P_CREATED_BY", createdby)

                                        };
                    result = sql.execStoredProcudure("PROC_SAVE_UPDATE_PUBLIC_NOTICES", spmLogin);
                }

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public PublicNoticeModel()
        {

        }
        public PublicNoticeModel(int id)
        {

            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_GET_PUBLIC_NOTICES_DETAILS", spmLogin, "");


            DataTable ResultTable = new DataTable();
            if (ds != null)
                ResultTable = ds.Tables[0];
            else
                ResultTable = null;

            //Hashtable ht = new Hashtable();
            //SqlHelper osqlHelper = new SqlHelper();
            //ht.Add("@TenderID", TenderID);
            //DataSet ds = new DataSet();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_TENDER_DETAILS_INDEX_AND_GET_BY_ID", ht);
            #endregion

            if (ds.Tables[0] != null)
            {
                PublicNoticeList = new List<PublicNoticeModel>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable ResultTable1 = new DataTable();
                    ResultTable1 = ds.Tables[0];
                    for (int i = 0; i < ResultTable1.Rows.Count; i++)
                    {
                        PublicNoticeList.Add(new PublicNoticeModel
                        {
                            publicNotiecsId = Convert.ToInt32(ResultTable1.Rows[i]["PUBLIC_NOTICE_ID"].ToString()),
                            NoticeDate = ResultTable1.Rows[i]["NOTICE_DATE"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable1.Rows[i]["NOTICE_DATE"]),
                            Description = ResultTable1.Rows[i]["DESCRIPTION"].ToString(),
                            EnglishDocumentPath = ResultTable1.Rows[i]["ENGLISH_DOCFILEPATH"].ToString(),
                            HindiDocumentPath = ResultTable1.Rows[i]["HINDI_DOCFILEPATH"].ToString()
                        });

                    }
                }
                else
                {
                    PublicNoticeList = null;
                }
            }
        }

        public List<PublicNoticeModel> GetPublicNoticeDetails()
        {
            DataTable ResultTable = new DataTable();
            List<PublicNoticeModel> PublicNoticeModelList = new List<PublicNoticeModel>();
            #region Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_GET_PUBLIC_NOTICES_DETAILS", spmLogin, "");
            #endregion


            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ResultTable = ds.Tables[0];
                        if (ResultTable != null)
                        {
                            if (ResultTable.Rows.Count > 0)
                            {
                                for (int i = 0; i < ResultTable.Rows.Count; i++)
                                {
                                    PublicNoticeModelList.Add(new PublicNoticeModel
                                    {
                                        publicNotiecsId = Convert.ToInt32(ResultTable.Rows[i]["PUBLIC_NOTICE_ID"].ToString()),
                                        NoticeDate = ResultTable.Rows[i]["NOTICE_DATE"] == DBNull.Value ? System.DateTime.MinValue : (Convert.ToDateTime(ResultTable.Rows[i]["NOTICE_DATE"])),
                                        Description = ResultTable.Rows[i]["DESCRIPTION"].ToString(),
                                        EnglishDocumentPath = ResultTable.Rows[i]["ENGLISH_DOCFILEPATH"].ToString(),
                                        HindiDocumentPath = ResultTable.Rows[i]["HINDI_DOCFILEPATH"].ToString()
                                    });
                                }
                                return PublicNoticeModelList;
                            }
                        }
                    }
                }

            }

            return null;
        }

        public int Delete(int publicNoticeIdParam)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_PUBLIC_NOTICEID", publicNoticeIdParam)
                                        };
            int result = sql.execStoredProcudure("PROC_DELETE_PUBLIC_NOTICE", spmLogin);
            #endregion	

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@TenderID", TenderIDParam);
            //int result = osqlHelper.ExecuteQuery("PROC_DELETE_TENDER_DETAILS", ht);
            return result;
        }

    }
}