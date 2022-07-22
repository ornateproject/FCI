using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;


namespace CWC_CMS.Models
{
    public class SqlHelper
    {
        string ConnectionString = string.Empty;
        static SqlConnection con;

        public SqlHelper()
        {

            //    if (ConfigurationManager.ConnectionStrings["dbcs"] != null)
            //    {
            //        string connStr = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            //    }

            //    ConnectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            //    con = new SqlConnection(ConnectionString);
            //}

            //public void SetConnection()
            //{
            //    if (ConnectionString == string.Empty)
            //    {
            //        ConnectionString = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            //    }
            //    con = new SqlConnection(ConnectionString);
            //}
        }


        public int ExecuteQueryWithParam(string procName, Hashtable parms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter sqlparam = new SqlParameter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (parms.Count > 0)
            {
                foreach (DictionaryEntry de in parms)
                {
                    if (de.Key.ToString().Contains("_out"))
                    {
                        sqlparam = new SqlParameter(de.Key.ToString(), de.Value);
                        sqlparam.DbType = DbType.Int32;
                        sqlparam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(sqlparam);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                    }
                }
            }
            if (con == null)
            {
                //SetConnection();
            }
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
                con.Open();
            int result = cmd.ExecuteNonQuery();
            if (sqlparam != null)
                result = Convert.ToInt32(sqlparam.SqlValue.ToString());
            return result;

        }

        public DataSet ExecuteProcudere(string procName, Hashtable parms)
        {

            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandText = procName;
                cmd.CommandType = CommandType.StoredProcedure;
                if (con == null)
                {
                    //SetConnection();
                }
                cmd.Connection = con;

                if (parms.Count > 0)
                {
                    foreach (DictionaryEntry de in parms)
                    {
                        cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                    }
                }
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                string Error_refno = string.Empty;

                String FUNCTION_NAME = "ExecuteProcudere";
                String MODULE_NAME = procName;
                String ERROR_TYPE = "DATABASE";
                String ERROR_DESC = ex.Message;
                string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                Error_refno = CommonDAL.InsertException(FUNCTION_NAME, MODULE_NAME, ERROR_TYPE, ERROR_DESC, url, lineNumber);
                return null;
            }

        }

        public DataTable ExecuteProcudereReturnDataTable(string procName, Hashtable parms)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            cmd.CommandText = procName;
            cmd.CommandType = CommandType.StoredProcedure;
            if (con == null)
            {
               // SetConnection();
            }
            cmd.Connection = con;
            if (parms.Count > 0)
            {
                foreach (DictionaryEntry de in parms)
                {
                    cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                }
            }
            da.SelectCommand = cmd;
            da.Fill(ds, "TableData");
            dt = ds.Tables["TableData"];
            return dt;
        }


        public int ExecuteQuery(string procName, Hashtable parms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (parms.Count > 0)
            {
                foreach (DictionaryEntry de in parms)
                {
                    cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                }
            }
            if (con == null)
            {
               // SetConnection();
            }
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
                con.Open();
            int result = cmd.ExecuteNonQuery();
            return result;
        }
        public int ExecuteQueryWithOutParam(string procName, Hashtable parms)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter sqlparam = new SqlParameter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procName;
                if (parms.Count > 0)
                {
                    foreach (DictionaryEntry de in parms)
                    {
                        if (de.Key.ToString().Contains("_out"))
                        {
                            sqlparam = new SqlParameter(de.Key.ToString(), de.Value);
                            sqlparam.DbType = DbType.Int32;
                            sqlparam.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(sqlparam);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                        }
                    }
                }
                if (con == null)
                {
                    //SetConnection();
                }
                cmd.Connection = con;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int result = cmd.ExecuteNonQuery();
                if (sqlparam != null)
                    result = Convert.ToInt32(sqlparam.SqlValue.ToString());
                return result;
            }
            catch (Exception ex)
            {

                //string Error_refno = string.Empty;

                //String FUNCTION_NAME = "ExecuteQueryWithOutParam";
                //String MODULE_NAME = procName;
                //String ERROR_TYPE = "DATABASE";
                //String ERROR_DESC = ex.Message;
                //string lineNumber = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                //string url = HttpContext.Current.Request.Url.AbsoluteUri;
                //Error_refno = CommonDAL.InsertException(FUNCTION_NAME, MODULE_NAME, ERROR_TYPE, ERROR_DESC, url, lineNumber);

                return 0;
            }
        }



        public String ExecuteQueryWithOutParamINString(string procName, Hashtable parms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameter sqlparam = new SqlParameter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            if (parms.Count > 0)
            {
                foreach (DictionaryEntry de in parms)
                {
                    if (de.Key.ToString().Contains("_out"))
                    {
                        sqlparam = new SqlParameter(de.Key.ToString(), de.Value);
                        sqlparam.DbType = DbType.String;
                        sqlparam.Size = 400;
                        sqlparam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(sqlparam);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                    }
                }
            }
            if (con == null)
            {
                //SetConnection();
            }
            cmd.Connection = con;
            if (con.State == ConnectionState.Closed)
                con.Open();
            String result = cmd.ExecuteNonQuery().ToString();
            if (sqlparam != null)
                result = sqlparam.SqlValue.ToString();
            return result;
        }

        #region Renu added 31 Jan 2020 MySql Connections


        private MySqlConnection nisConn;
        private MySqlCommand nisDataCommand;
        private MySqlDataAdapter nisDataAdapter = new MySqlDataAdapter();
        private MySqlTransaction nisTrans;

        public void OpenConn()
        {
            if (nisConn == null)
            {
                //nisConn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
                nisConn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString);

                if (nisConn.State == ConnectionState.Closed)
                {
                    nisConn.Open();
                    nisDataCommand = new MySqlCommand();
                    nisDataCommand.Connection = nisConn;
                }
            }
            else
            {
                if (nisConn.State == ConnectionState.Closed)
                    nisConn.Open();
                nisDataCommand = new MySqlCommand();
                nisDataCommand.Connection = nisConn;
            }
        }

        public void OpenConn1()
        {
            nisConn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            if (nisConn == null)
            {

                //nisConn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
                nisConn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString);

                if (nisConn.State == ConnectionState.Closed)
                {
                    nisConn.Open();
                    nisDataCommand = new MySqlCommand();
                    nisDataCommand.Connection = nisConn;
                }
            }
            else
            {
                if (nisConn.State == ConnectionState.Closed)
                    nisConn.Open();
                nisDataCommand = new MySqlCommand();
                nisDataCommand.Connection = nisConn;
            }
        }
        public void CloseConn()
        {
            if (nisConn != null)
            {
                if (nisConn.State == ConnectionState.Open)
                    nisConn.Close();
            }

        }
        public void disposeConn()
        {
            if (nisConn != null)
            {
                if (nisConn.State == ConnectionState.Closed)
                    nisConn.Dispose();
                nisConn = null;
            }
        }
        public void OpenTransaction()
        {
            if (nisTrans != null)
            {
                nisTrans = null;
            }

            nisTrans = nisDataCommand.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            nisDataCommand.Transaction = nisTrans;

        }
        public void CommintTransaction()
        {
            if (nisTrans != null)
            {
                nisTrans.Commit();
            }
        }

        public void RollbackTransaction()
        {
            if (nisTrans != null)
            {
                if (nisTrans.Connection != null)
                {
                    nisTrans.Rollback();
                }
            }
        }

        public int execStoredProcudure(string strProcName, MySqlParameter[] arProcParams)
        {
            try
            {
                int returnValue;

                MySqlParameter param = new MySqlParameter();
                OpenConn();
                nisDataCommand.CommandType = CommandType.StoredProcedure;
                nisDataCommand.CommandText = strProcName;

                nisDataCommand.Parameters.Clear();
                foreach (MySqlParameter LoopVar_param in arProcParams)
                {
                    param = LoopVar_param;
                    nisDataCommand.Parameters.Add(param);
                }
                returnValue = Convert.ToInt32(nisDataCommand.ExecuteScalar());
                CloseConn();
                disposeConn();

                return returnValue;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public String execStoredProcudureInString(string strProcName, MySqlParameter[] arProcParams)
        {
            String returnValue;

            MySqlParameter param = new MySqlParameter();
            OpenConn();
            nisDataCommand.CommandType = CommandType.StoredProcedure;
            nisDataCommand.CommandText = strProcName;

            nisDataCommand.Parameters.Clear();
            foreach (MySqlParameter LoopVar_param in arProcParams)
            {
                param = LoopVar_param;
                nisDataCommand.Parameters.Add(param);
            }
            returnValue = (nisDataCommand.ExecuteScalar()).ToString();
            CloseConn();
            disposeConn();

            return returnValue;
        }


        public decimal execStoredProcudure1(string strProcName, MySqlParameter[] arProcParams)
        {
            decimal returnValue;

            MySqlParameter param = new MySqlParameter();
            OpenConn();
            nisDataCommand.CommandType = CommandType.StoredProcedure;
            nisDataCommand.CommandText = strProcName;

            nisDataCommand.Parameters.Clear();
            foreach (MySqlParameter LoopVar_param in arProcParams)
            {
                param = LoopVar_param;
                nisDataCommand.Parameters.Add(param);
            }
            returnValue = Convert.ToDecimal(nisDataCommand.ExecuteScalar());
            CloseConn();
            disposeConn();

            return returnValue;
        }


        public int execStoredProcudure(string strProcName, MySqlParameter[] arProcParams, string str)
        {
            int returnValue;
            OpenConn();
            MySqlParameter param = new MySqlParameter();
            nisDataCommand.CommandType = CommandType.StoredProcedure;
            nisDataCommand.CommandText = strProcName;
            nisDataCommand.Parameters.Clear();
            foreach (MySqlParameter LoopVar_param in arProcParams)
            {
                param = LoopVar_param;
                nisDataCommand.Parameters.Add(param);
            }
            returnValue = Convert.ToInt32(nisDataCommand.ExecuteScalar());
            CloseConn();
            disposeConn();
            return returnValue;
        }

        public int execSQL(string strSql)
        {
            nisDataCommand.CommandType = CommandType.Text;
            nisDataCommand.CommandText = strSql;
            int intRows = nisDataCommand.ExecuteNonQuery();

            return intRows;
        }

        public string execScaler(string strSql)
        {
            string val = "";
            this.OpenConn();
            nisDataCommand.CommandType = CommandType.Text;
            nisDataCommand.CommandText = strSql;
            val = Convert.ToString(nisDataCommand.ExecuteScalar());
            return val;
            this.CloseConn();
        }
        public string execScaler(string strSql, string str, MySqlParameter[] arProcParams)
        {
            string val = "";
            if (str == "Query")
            {
                this.OpenConn();
                nisDataCommand.CommandType = CommandType.Text;
                nisDataCommand.CommandText = strSql;
                val = Convert.ToString(nisDataCommand.ExecuteScalar());
                this.CloseConn();
            }
            else if (str == "Procedure")
            {
                MySqlParameter param = new MySqlParameter();
                this.OpenConn();
                foreach (MySqlParameter LoopVar_param in arProcParams)
                {
                    param = LoopVar_param;
                    nisDataCommand.Parameters.Add(param);
                }
                nisDataCommand.CommandType = CommandType.StoredProcedure;
                nisDataCommand.CommandText = strSql;
                val = Convert.ToString(nisDataCommand.ExecuteScalar());

                this.CloseConn();
            }
            return val;

        }
        public DataTable getDataTable(string Storeproc, String Command)
        {
            this.OpenConn();
            MySqlDataAdapter da = new MySqlDataAdapter(Storeproc, nisConn);
            if (Command == "Procedure")
            {
                nisDataCommand.CommandType = CommandType.StoredProcedure;
            }
            else if (Command == "Query")
            {
                nisDataCommand.CommandType = CommandType.Text;
            }
            nisDataCommand.CommandText = Storeproc;
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.CloseConn();
            this.disposeConn();
            return dt;
        }


        public DataTable getDataTable(string StoreProc, MySqlParameter[] arProcParams, String tblname)
        {
            DataSet ds = new DataSet();
            MySqlParameter param = new MySqlParameter();
            OpenConn();
            MySqlDataAdapter da = new MySqlDataAdapter(StoreProc, nisConn);
            nisDataCommand.CommandType = CommandType.StoredProcedure;
            nisDataCommand.CommandText = StoreProc;
            nisDataCommand.Parameters.Clear();
            nisDataAdapter.SelectCommand = nisDataCommand;

            foreach (MySqlParameter LoopVar_param in arProcParams)
            {
                param = LoopVar_param;
                nisDataCommand.Parameters.Add(param);
            }
            if (tblname == null || tblname == "")
            {
                nisDataAdapter.Fill(ds);
            }
            else
            {
                nisDataAdapter.Fill(ds, tblname);
            }
            DataTable dt = new DataTable();
            nisDataAdapter.Fill(dt);
            this.CloseConn();
            this.disposeConn();
            return dt;
        }


        public DataSet getDataSet(string StoreProc, MySqlParameter[] arProcParams, String tblname)
        {
            DataSet ds1 = new DataSet();
            //DataSet ds = new DataSet();
            try
            {

                MySqlParameter param = new MySqlParameter();
                OpenConn1();
                MySqlDataAdapter da = new MySqlDataAdapter(StoreProc, nisConn);
                nisDataCommand.CommandType = CommandType.StoredProcedure;
                nisDataCommand.CommandText = StoreProc;
                nisDataCommand.Parameters.Clear();
                nisDataAdapter.SelectCommand = nisDataCommand;

                foreach (MySqlParameter LoopVar_param in arProcParams)
                {
                    param = LoopVar_param;
                    nisDataCommand.Parameters.Add(param);
                }
                //if (tblname == null || tblname == "")
                //{
                //    nisDataAdapter.Fill(ds);
                //}
                //else
                //{
                //    nisDataAdapter.Fill(ds, tblname);
                //}

                nisDataAdapter.Fill(ds1);
                this.CloseConn();
                this.disposeConn();

            }
            catch (Exception ex)
            {
                ds1 = null;
            }
            return ds1;
        }




        public DataSet GetDataSet(string strSql)
        {
            this.OpenConn();
            MySqlDataAdapter da = new MySqlDataAdapter(strSql, nisConn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            this.CloseConn();
            this.disposeConn();
            return ds;
        }

        public DataSet GetDataSet(string strSql, string tblname)
        {
            this.OpenConn();
            MySqlDataAdapter da = new MySqlDataAdapter(strSql, nisConn);
            DataSet ds = new DataSet();
            da.Fill(ds, tblname);
            this.CloseConn();
            this.disposeConn();
            return ds;
        }

        public DataSet GetDataSet(string strSPName, string tblname, MySqlParameter[] arProcParams)
        {
            this.OpenConn();
            DataSet ds = new DataSet();
            MySqlParameter param = new MySqlParameter();
            nisDataCommand.CommandType = CommandType.StoredProcedure;
            nisDataCommand.CommandText = strSPName;
            nisDataCommand.Parameters.Clear();
            nisDataAdapter.SelectCommand = nisDataCommand;

            foreach (MySqlParameter LoopVar_param in arProcParams)
            {
                param = LoopVar_param;
                nisDataCommand.Parameters.Add(param);
            }
            if (tblname == null)
            {
                nisDataAdapter.Fill(ds);
            }
            else
            {
                nisDataAdapter.Fill(ds, tblname);
            }
            this.CloseConn();
            this.disposeConn();
            return ds;
        }

        private MySqlDataReader getDataReader(string strSql)
        {
            this.OpenConn();
            MySqlDataReader dReader;
            nisDataCommand.CommandType = CommandType.Text;
            nisDataCommand.CommandText = strSql;
            dReader = nisDataCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dReader;

        }
        public int getSingleIntRecord(String Query, String FieldName)
        {
            int i = 0;

            MySqlDataReader dr;
            OpenConn();
            nisDataCommand = new MySqlCommand(Query, nisConn);
            dr = nisDataCommand.ExecuteReader(CommandBehavior.SingleResult);
            while (dr.Read())
            {
                if (dr[0].ToString() == "")
                {
                    i = 0;
                }
                else
                {
                    i = Convert.ToInt32(dr[0].ToString());

                }
            }
            CloseConn();
            return i;
        }
        //public Boolean CheckforGrid(GridView grvGrid)
        //{
        //    Boolean Msg = false;
        //    for (int i = 0; i <= grvGrid.Rows.Count - 1; i++)
        //    {
        //        CheckBox chk = (CheckBox)grvGrid.Rows[i].FindControl("chkMain");
        //        if (chk.Checked == true)
        //        {
        //            Msg = true;
        //            goto last;
        //        }

        //    }
        //    last:;
        //    return Msg;
        //}
        //private string RemoveSpecialChar(string str)
        //{
        //    Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //    return r.Replace(str, String.Empty);
        //}
        #endregion
    }
}