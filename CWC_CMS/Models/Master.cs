using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Models
{
    public static class Master
    {
        public static string GET_MENU_NAME_BY_PAGE_ADDRESS(string PageAddress)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("P_PageAddress", PageAddress)
                                        };
            ds = sql.getDataSet("PROC_GET_MENU_NAME_BY_PAGE_ADDRESS", spmLogin, "");
            #endregion
 
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            return "";
        }



        public static string GET_ROLL_NO_BY_REGISTRATION_NO_AND_EXAM_ID(string RegtNo, int ExamID)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@RegtNo", RegtNo);
            ht.Add("@ExamID", ExamID);
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_ROLL_NO_BY_REGISTRATION_NO_AND_EXAM_ID", ht);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            return "";
        }

        public static int CheckIfExamNameIsAllowed(string ExamName)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ExamName", ExamName);
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_CHECK_IF_EXAM_NAME_IS_ALLOWED", ht);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
            }
            return -1;
        }



        public static SelectList FillSecurityQuestion()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = { };
            ds = sql.getDataSet("PROC_GET_SECURITY_QUESTION_FOR_VIGILANCE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_SECURITY_QUESTION_FOR_VIGILANCE", ht);
            if (ds != null)
            {
                SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "SecurityQuestionID", "SecurityQuestion");
                return sl;
            }
            return null;
        }


        public static SelectList FillTenderType()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_TENDER_TYPE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_TENDER_TYPE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "TenderTypeID", "TenderType");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillCorrigendumType()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_CORRIGENDUM_TYPE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_CORRIGENDUM_TYPE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "CorrigendumTypeID", "CorrigendumType");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillCorrigendumReason()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_CORRIGENDUM_REASON", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_CORRIGENDUM_REASON", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "CorrigendumReasonID", "CorrigendumReason");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillFormOfContract()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_FORM_OF_CONTRACT", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_FORM_OF_CONTRACT", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "ContractFormID", "ContractForm");
                        return sl;
                    }
                }

            }
            return null;
        }



        public static SelectList FillExamNew()
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_EXAM_NEW", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "ExamName", "ExamName");
                        return sl;
                    }
                }

            }
            return null;
        }




        public static SelectList FillExam()
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_EXAM", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "ExamID", "ExamName");
                        return sl;
                    }
                }

            }
            return null;
        }



        public static SelectList FillSalutation()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = { };
            ds = sql.getDataSet("PROC_FILL_SALUTATION_FOR_VIGILANCE_FORM", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_SALUTATION_FOR_VIGILANCE_FORM", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "Salutation", "Salutation");
                        return sl;
                    }
                }

            }
            return null;
        }

        public static SelectList FillNoOfCover()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_NO_OF_COVER", spmLogin, "");
            #endregion
            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_NO_OF_COVER", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "CoverNoID", "CoverNo");
                        return sl;
                    }
                }

            }
            return null;
        }



        public static SelectList FillCoverType(int CoverNo)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("CoverNo", CoverNo)
                                        };
            ds = sql.getDataSet("PROC_FILL_COVER_TYPE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@CoverNo", CoverNo);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_COVER_TYPE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "CoverTypeID", "CoverType");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillCityByStateID(int StateID)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("StateID", StateID)
                                        };
            ds = sql.getDataSet("PROC_FILL_CITY_BY_STATE_ID", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@StateID", StateID);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_CITY_BY_STATE_ID", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "CityID", "CityName");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillPincodeByCityID(int CityID)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("CityID", CityID)
                                        };
            ds = sql.getDataSet("PROC_FILL_PINCODE_BY_CITY_ID", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@CityID", CityID);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_PINCODE_BY_CITY_ID", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "PincodeID", "Pincode");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static string FillCoverNameByCoverTypeID(int CoverTypeID)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("p_CoverTypeID", CoverTypeID)
                                        };
            ds = sql.getDataSet("PROC_FILL_COVER_NAME_BY_COVER_TYPE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@CoverTypeID", CoverTypeID);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_COVER_NAME_BY_COVER_TYPE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                }

            }
            return null;
        }


        public static int GetCurrentIdentityOfTenderTable()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            int result = sql.execStoredProcudure("PROC_GET_CURRENT_IDENTITY_OF_TENDER_TABLE", spmLogin);
            #endregion
            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_CURRENT_IDENTITY_OF_TENDER_TABLE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

            }
            return 0;
        }


        public static int GetCurrentIdentityOfComplaintTable()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_GET_CURRENT_IDENTITY_OF_COMPLAINT_TABLE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_CURRENT_IDENTITY_OF_COMPLAINT_TABLE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

            }
            return 0;
        }


        public static int GetCurrentIdentityOfCorrigendumTable()
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_CURRENT_IDENTITY_OF_CORRIGENDUM_TABLE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

            }
            return 0;
        }


        public static int InsertRollNoRegNoDateOfBirthForLoginFromExcel(int ExamID)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ExamID", ExamID);
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_INSERT_ROLL_NO_REG_NO_DATE_OF_BIRTH_FOR_LOGIN_FROM_EXCEL", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

            }
            return 0;
        }



        public static int GetVigilanceIDByVigilanceComplaintID(int VigilanceComplaintID)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = { new MySqlParameter("VigilanceComplaintID", VigilanceComplaintID) };
            ds = sql.getDataSet("PROC_GET_VIGILANCE_ID_BY_VIGILANCE_COMPLAINT_ID", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@VigilanceComplaintID", VigilanceComplaintID);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_VIGILANCE_ID_BY_VIGILANCE_COMPLAINT_ID", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

            }
            return 0;
        }



        public static string GetExamNameByExamID(int ExamID)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ExamID", ExamID);
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_EXAM_NAME_BY_EXAM_ID", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                }

            }
            return "";
        }

        public static string GetMobileNOByLoginName(string LoginName)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("LoginName", LoginName)
                                        };
            ds = sql.getDataSet("PROC_GET_MOBILE_NO_BY_LOGIN_NAME", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@LoginName", LoginName);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_EMAIL_ID_BY_LOGIN_NAME", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                }

            }
            return "";
        }



        public static string GetEmailIDByLoginName(string LoginName)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("LoginName", LoginName)
                                        };
            ds = sql.getDataSet("PROC_GET_EMAIL_ID_BY_LOGIN_NAME", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@LoginName", LoginName);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_EMAIL_ID_BY_LOGIN_NAME", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return ds.Tables[0].Rows[0][0].ToString();
                    }
                }

            }
            return "";
        }


        public static int GetCurrentIdentityOfVigilanceComplaintTable()
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_CURRENT_IDENTITY_OF_VIGILANCE_COMPLAINT_TABLE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

            }
            return 0;
        }


        public static int GetVigilanceIDByLoginName(string LoginName)
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("LoginName", LoginName)
                                        };
            ds = sql.getDataSet("PROC_GET_VIGILANCE_ID_BY_LOGIN_NAME", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@LoginName", LoginName);
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_VIGILANCE_ID_BY_LOGIN_NAME", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    }
                }

            }
            return 0;
        }



        public static SelectList FillProductCategory()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_PRODUCT_CATEGORY", spmLogin, "");
            #endregion
            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_PRODUCT_CATEGORY", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "ProductCategoryID", "ProductCategory");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillContractType()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_CONTRACT_TYPE", spmLogin, "");
            #endregion
            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_CONTRACT_TYPE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "ContractTypeID", "ContractType");
                        return sl;
                    }
                }

            }
            return null;
        }



        public static SelectList FillTendererClass()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_TENDERER_CLASS", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_TENDERER_CLASS", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "TendererClassID", "TendererClass");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillTenderCurrency()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_TENDER_CURRENCY", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_TENDER_CURRENCY", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "TenderCurrencyID", "TenderCurrency");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillBidValidityDays()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_BID_VALIDITY_DAYS", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_BID_VALIDITY_DAYS", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "BidValidityDaysID", "BidValidityDays");
                        return sl;
                    }
                }

            }
            return null;
        }



        public static SelectList FillOfflineInstruments()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_OFFLINE_INSTRUMENTS", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_OFFLINE_INSTRUMENTS", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "OfflineInstrumentID", "OfflineInstrument");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillTenderCategory()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_TENDER_CATEGORY", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_TENDER_CATEGORY", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "CategoryID", "Category");
                        return sl;
                    }
                }

            }
            return null;
        }






        public static SelectList FillState()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = { };
            ds = sql.getDataSet("PROC_FILL_STATE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_STATE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "StateID", "StateName");
                        return sl;
                    }
                }

            }
            return null;
        }


        public static SelectList FillSubComplaint()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = { };
            ds = sql.getDataSet("PROC_GET_SUB_COMPLAINT_DETAILS", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_GET_SUB_COMPLAINT_DETAILS", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "SubComplaintId", "SubComplaintName");
                        return sl;
                    }
                }

            }
            return null;

        }


        public static SelectList FillStateForTender()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {

                                        };
            ds = sql.getDataSet("PROC_FILL_STATE_FOR_TENDER", spmLogin, "");
            #endregion


            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_STATE_FOR_TENDER", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "StateID", "StateName");
                        return sl;
                    }
                }

            }
            return null;
        }

        public static SelectList FillCity()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = { };
            ds = sql.getDataSet("PROC_FILL_CITY", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_CITY", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "CityID", "CityName");
                        return sl;
                    }
                }

            }
            return null;
        }

        public static SelectList FillPincode()
        {
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = { };
            ds = sql.getDataSet("PROC_FILL_PINCODE", spmLogin, "");
            #endregion

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //DataSet ds = oSqlHelper.ExecuteProcudere("PROC_FILL_PINCODE", ht);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        SelectList sl = new SelectList(ds.Tables[0].AsDataView(), "PincodeID", "Pincode");
                        return sl;
                    }
                }

            }
            return null;
        }
    }
}