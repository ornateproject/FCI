using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.SessionState;
using System.Net.Mail;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CWC_CMS.Models
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class MySave
    {
        SqlCommand cmd;
        SqlConnection con;
        DataSet ds;
        SqlDataAdapter adap;
        DataTable dt;
        public MySave(string procedure_name)
        {
            cmd = new SqlCommand();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString);
            cmd.CommandText = procedure_name;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
        }

        public void AddOutputParameter(string param_name)
        {
            
                SqlParameter parMSG = cmd.CreateParameter();
                parMSG.ParameterName = param_name;
                parMSG.Direction = ParameterDirection.Output;
                parMSG.DbType = DbType.Int32;
                cmd.Parameters.Add(parMSG);
            
        }
        public void AddParameter(string param_name, string param_value)
        {
            if (param_value == "" || param_value == null)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }
        public SqlParameter GetParameter(string param_name, SqlDbType param_value)
        {
            if (param_value == 0M)
            {
                cmd.Parameters.Add(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add(param_name, param_value);
            }

            return cmd.Parameters[param_name];
        }
        public void AddParameter(string param_name, int? param_value)
        {
            if (param_value == null)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }
        public void AddParameter(string param_name, byte[] param_value)
        {
            if (param_value == null)
            {
                param_value = new byte[10];
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }
        public void AddParameter(string param_name, decimal? param_value)
        {
            if (param_value == null)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }
        public void AddParameter(string param_name, bool param_value)
        {
            if (param_value == false)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }



        public void AddParameter(string param_name, DateTime param_value)
        {
            DateTime dd = new DateTime(0001, 01, 01);
            if (param_value.CompareTo(dd) == 0)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }

        public void AddParameter(string param_name, DateTime? param_value)
        {
            DateTime dd = new DateTime(0001, 01, 01);
            if (param_value == null)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }
        public void AddParameter(string param_name, DataTable param_value)
        {
            if (param_value == null)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }
        public void AddParameter(string param_name, List<int> param_value)
        {
            if (param_value == null)
            {
                cmd.Parameters.AddWithValue(param_name, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(param_name, param_value);
            }
        }
        public int ExecuteSave()
        {
            int returnbyproc = 0;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.Connection = con;
            returnbyproc = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            con.Dispose();
            cmd.Dispose();
            return returnbyproc;

        }
        public void UserMsgBox(string sMsg)
        {
            Page page = HttpContext.Current.Handler as Page;
            if (string.IsNullOrEmpty(sMsg))
            {
                sMsg = "Error";
            }
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + sMsg + "');", true);
        }

        public DataSet GetDataByProcedure()
        {
            try
            {
                
                adap = new SqlDataAdapter();
                ds = new DataSet();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                adap.SelectCommand = cmd;
                adap.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Dispose();
                cmd.Dispose();
                adap.Dispose();
                ds.Dispose();
            }
        }
    }
}