using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIDCUL.Models;
using CWC_CMS.Models;
namespace SIDCUL.Models
{
    public static class ErrorModel
    {
        public static string Function_Name { get; set; }
        public static string Module_Name { get; set; }
        public static string Error_Type { get; set; }
        public static string Error_Desc { get; set; }
        public static string Error_Desc_api { get; set; }
        public static string Url { get; set; }
        public static string Line_No { get; set; }
        public static string IP_Address { get; set; }
        public static string Login_Name { get; set; }
        public static int SERVICE_RECNO { get; set; }
        public static int APPLICATION_RECNO { get; set; }

        public static string SaveError()
        {
            SqlHelper oSqlHelper = new SqlHelper();
            string Error_Refno = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@P_FUNCTION_NAME", Function_Name);
                ht.Add("@P_MODULE_NAME", Module_Name);
                ht.Add("@P_ERROR_TYPE", Error_Type);
                ht.Add("@P_ERROR_DESC", Error_Desc);
                ht.Add("@P_ERROR_Page", Url);
                ht.Add("@P_Line_No", Line_No);
                ht.Add("@P_IP_Address", IP_Address);
                ht.Add("@ERROR_REFNO_out", "");
                Error_Refno = oSqlHelper.ExecuteQueryWithOutParamINString("PROC_INSERT_ERROR_DETAILS", ht);
                CommonDAL.GenerateMailFormat(Function_Name, Module_Name, Error_Type, Error_Desc, Url, Line_No, Error_Refno);
                return Error_Refno;
            }
            catch (Exception ex)
            {
                return Error_Refno;
            } 
        }

        public static string SaveAPIError()
        {
            SqlHelper oSqlHelper = new SqlHelper();
            string Error_Refno = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@P_SERVICE_RECNO", SERVICE_RECNO);
                ht.Add("@P_APPLICATION_RECNO", APPLICATION_RECNO);
                ht.Add("@P_ERROR_DESC", Error_Desc_api);
                ht.Add("@ERROR_REFNO_out", "");
                Error_Refno = oSqlHelper.ExecuteQueryWithOutParamINString("PROC_INSERT_API_ERROR_DETAILS", ht);
              //  CommonDAL.GenerateMailFormat(Function_Name, Module_Name, Error_Type, Error_Desc, Url, Line_No, Error_Refno);
                return Error_Refno;
            }
            catch (Exception ex)
            {
                return Error_Refno;
            }
        }

       
    }

         
}