using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace CWC_CMS.Models
{
    public class MySearch
    {
        SqlConnection con ;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter adap;
      
        public MySearch(string sql)
        {

             con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString);
             try
             {
                cmd = new SqlCommand();
                adap = new SqlDataAdapter();
                ds = new DataSet();
                cmd.CommandText = sql;
                cmd.Connection = con;
                cmd.Parameters.Clear();
            }
            catch (Exception e)
            {
            }
            finally
            {

            }
           
        }

        public string AddParameter(string parameter, string value)
        {
            try
            {
                if (value == null || value == "")
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
               

        }
        public string AddParameter(string parameter, int value)
        {
            try
            {
                if (value == null)
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
        }
        public string AddParameter(string parameter, int? value)
        {
            try
            {
                if (value == null)
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
        }
        public string AddParameter(string parameter, decimal value)
        {
            try
            {
                if (value == null || value == 0.0M)
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
        }
        public string AddParameter(string parameter, DataTable value)
        {
            try
            {
                if (value == null)
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
        }
        public string AddParameter(string parameter, DateTime value)
        {
            try
            {
                DateTime dd = new DateTime(0001, 01, 01);
                if (value.CompareTo(dd) == 0)
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
        }
        public string AddParameter(string parameter, DateTime? value)
        {
            try
            {
               
                if (value == null)
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
        }
        public string AddParameter(string parameter, byte[] value)
        {
            try
            {
                if (value == null )
                {
                    cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue(parameter, value);
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
            }
            return "Success";
        }

        public DataSet ExecuteSearch()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
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
            }
           }


        public int ExecuteInsert()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                adap.InsertCommand = cmd;
                adap.InsertCommand.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Dispose();
                cmd.Dispose();
            }
        }
    }
}