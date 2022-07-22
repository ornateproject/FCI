using CWC_CMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Models
{
    public class CMSModel
    {

        public string ExamName { get; set; }

        [AllowHtml]
        public string InitialPageHTML { get; set; }

        [AllowHtml]
        public string SavedHTML { get; set; }

        public string PageAddress { get; set; }

        [AllowHtml]
        public string FinalSubmitHTML { get; set; }

        public string PageName { get; set; }


        public int SaveHTML(string OldFileNewPath, string BackupName)
        {
            try
            {
                #region Renu 03 Feb 2020 For Mysql Start 
                // Renu 03 Feb 2020 For Mysql Start 
                DataSet ds = new DataSet();
                SqlHelper sql = new SqlHelper();
                MySqlParameter[] spmLogin = {
                                            new MySqlParameter("PageAddress", PageAddress),
                                            new MySqlParameter("FinalSubmitHTML", FinalSubmitHTML),
                                            new MySqlParameter("OldFileNewPath", OldFileNewPath),
                                            new MySqlParameter("BackupName", BackupName)
                                            };
                int result = sql.execStoredProcudure("PROC_SAVE_FINAL_HTML", spmLogin);
                #endregion

                //SqlHelper osqlHelper = new SqlHelper();
                //Hashtable ht = new Hashtable();
                //ht.Add("@PageAddress", PageAddress);
                //ht.Add("@FinalSubmitHTML", FinalSubmitHTML);
                //ht.Add("@OldFileNewPath", OldFileNewPath);
                //ht.Add("@BackupName", BackupName);
                //int result = osqlHelper.ExecuteQuery("PROC_SAVE_FINAL_HTML", ht);
                return result;
            }
            catch (Exception)
            {
                return -1;
            }

        }


        public int SaveNewPageHTML(string OldFileNewPath, string BackupName)
        {
            try
            {
                #region Renu 03 Feb 2020 For Mysql Start 
                // Renu 03 Feb 2020 For Mysql Start 
                DataSet ds = new DataSet();
                SqlHelper sql = new SqlHelper();
                MySqlParameter[] spmLogin = {
                                            new MySqlParameter("PageAddress", "NA"),
                                            new MySqlParameter("FinalSubmitHTML", FinalSubmitHTML),
                                            new MySqlParameter("OldFileNewPath", OldFileNewPath),
                                            new MySqlParameter("BackupName", BackupName)
                                            };
                int result = sql.execStoredProcudure("PROC_SAVE_FINAL_HTML_FOR_NEW_PAGE", spmLogin);
                #endregion

                //SqlHelper osqlHelper = new SqlHelper();
                //Hashtable ht = new Hashtable();
                //ht.Add("@PageAddress", "NA");
                //ht.Add("@FinalSubmitHTML", FinalSubmitHTML);
                //ht.Add("@OldFileNewPath", OldFileNewPath);
                //ht.Add("@BackupName", BackupName);
                //int result = osqlHelper.ExecuteQuery("PROC_SAVE_FINAL_HTML_FOR_NEW_PAGE", ht);
                return result;
            }
            catch (Exception)
            {
                return -1;
            }

        }
    }
}