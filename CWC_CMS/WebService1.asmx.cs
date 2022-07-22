using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using CWC_CMS.Models;

namespace CWC_CMS
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]      
        public DataSet GetUserComplaintDetails()
        {
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                          
                                        };
             DataSet ds1 = sql.getDataSet("PROC_GET_COMPLAINT_DETAILS_FOR_COMPLAINT_MANAGEMENT", spmLogin, "");
             return ds1;      
        }


        [WebMethod]
        public DataSet GEtComplaintAgainstEmployeeDetails()
        {
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            DataSet ds = sql.getDataSet("PROC_VIGILANCE_DETAILS_OF_ACCUSSED_FOR_COMPLAINT_MANAGEMENT", spmLogin, "");
            return ds;         
        }
    }
}
