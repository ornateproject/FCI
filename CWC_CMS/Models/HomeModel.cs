using SIDCUL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Models
{
    public class HomeModel
    {
        public int SrNo { get; set; }
        public string ParentMenuName { get; set; }
        public string MenuName { get; set; }
        public string PageAddress { get; set; }


        public List<HomeModel> GetListOfMenusToEdit()
        {
            List<HomeModel> homeModelList = new List<HomeModel>();
            // Renu 03 Feb 2020 For Mysql

            SqlHelper sql = new SqlHelper();
            DataSet ds = new DataSet();
            MySqlParameter[] spmLogin = {
                                                };
            ds = sql.getDataSet("PROC_GET_LIST_OF_MENUS_TO_EDIT", spmLogin, "");
            DataTable dt = new DataTable();

            //SqlHelper osqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //ds = osqlHelper.ExecuteProcudere("PROC_GET_LIST_OF_MENUS_TO_EDIT", ht);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        homeModelList.Add(new HomeModel
                        {
                            SrNo = i + 1,
                            ParentMenuName = dt.Rows[i]["ParentMenuName"].ToString(),
                            MenuName = dt.Rows[i]["MenuName"].ToString(),
                            PageAddress = dt.Rows[i]["PageAddress"].ToString()
                        });
                    }
                    return homeModelList;
                }
            }

            return null;
        }
    }
}