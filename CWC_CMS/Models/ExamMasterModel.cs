using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CWC_CMS.Models
{
    public class ExamMasterModel
    {
        public int? ExamID { get; set; }
        public string ExamName { get; set; }
        public DateTime? CreatedOn { get; set; }


        public ExamMasterModel()
        {

        }

        public ExamMasterModel(int ExamIDParameter)
        {
            try
            {
                SqlHelper oSqlHelper = new SqlHelper();
                DataTable ResultTable = new DataTable();
                Hashtable ht = new Hashtable();
                ht.Add("@ExamID", ExamIDParameter);
                ResultTable = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_EXAM_MASTER_INDEX_AND_GET_BY_ID", ht);
                if (ResultTable != null)
                {
                    CreatedOn = (ResultTable.Rows[0]["CreatedOn"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[0]["CreatedOn"]));
                    ExamName = ResultTable.Rows[0]["ExamName"].ToString();
                    ExamID = (ResultTable.Rows[0]["ExamID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[0]["ExamID"]));
                }

            }
            catch (Exception)
            {

                throw;
            }


        }


        public List<ExamMasterModel> GetDataForIndex()
        {
            SqlHelper oSqlHelper = new SqlHelper();
            DataTable ResultTable = new DataTable();
            List<ExamMasterModel> _ExamMasterModel = new List<ExamMasterModel>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@ExamID", 0);
                ResultTable = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_EXAM_MASTER_INDEX_AND_GET_BY_ID", ht);

                if (ResultTable != null)
                {


                    for (int i = 0; i < ResultTable.Rows.Count; i++)
                    {
                        _ExamMasterModel.Add(new ExamMasterModel
                        {
                            CreatedOn = (ResultTable.Rows[i]["CreatedOn"] == DBNull.Value ? System.DateTime.MinValue : Convert.ToDateTime(ResultTable.Rows[i]["CreatedOn"])),
                            ExamID = (ResultTable.Rows[i]["ExamID"] == DBNull.Value ? 0 : Convert.ToInt32(ResultTable.Rows[i]["ExamID"])),
                            ExamName = ResultTable.Rows[i]["ExamName"].ToString()

                        });
                    }




                }
                //CommonDAL.GenerateMailFormat(Function_Name, Module_Name, Error_Type, Error_Desc, Url, Line_No, Error_Refno);
                return _ExamMasterModel;
            }
            catch (Exception ex)
            {
                return _ExamMasterModel;
            }
        }


        public int SaveUpdate(string Case)
        {
            int returnbyproc = 0;

            try
            {
                Hashtable ht = new Hashtable();
                SqlHelper oSqlHelper = new SqlHelper();
                ht.Add("@CreatedOn", System.DateTime.Now.Date);
                ht.Add("@ExamName", ExamName);
                if (ExamID != 0)
                {
                    ht.Add("@ExamID", ExamID);
                }
                else
                {
                    ht.Add("@ExamID", 0);
                }
                ht.Add("@RecordID_out",0);
                ht.Add("@IpAddress", CommonDAL.GetIPAddress());
                ht.Add("@case", Case);


                returnbyproc = oSqlHelper.ExecuteQueryWithOutParam("PROC_INSERT_UPDATE_DELETE_EXAM_MASTER", ht);
                return returnbyproc;
            }
            catch (Exception e)
            {
                return -1;
            }
            finally
            {


            }

        }




        public int Delete(int ExamIDParameter)
        {
            int returnbyproc = 0;

            try
            {
                Hashtable ht = new Hashtable();
                SqlHelper oSqlHelper = new SqlHelper();
                ht.Add("@CreatedOn", "2002-01-02");
                ht.Add("@ExamName", "");
                ht.Add("@ExamID", ExamIDParameter);
                ht.Add("@IpAddress", CommonDAL.GetIPAddress());
                ht.Add("@case", "Delete");
                ht.Add("@RecordID_out", 0);


                returnbyproc = oSqlHelper.ExecuteQuery("PROC_INSERT_UPDATE_DELETE_EXAM_MASTER", ht);
                return returnbyproc;
            }
            catch (Exception e)
            {
                return -1;
            }
            finally
            {


            }

        }
    }
}