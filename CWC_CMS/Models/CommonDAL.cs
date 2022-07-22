using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Collections;
using System.Web.Mvc;
using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CWC_CMS.Models
{
    public class CommonDAL
    {
        SqlHelper osqlHelper = new SqlHelper();
        SqlHelper sql = new SqlHelper();
        internal static string SendSMS(string MobileNo, string EnCodedMsg)
        {
            try
            {
                string uid = AppConstants.UID;
                string pin = AppConstants.PIN;
                string sender = AppConstants.SENDER;
                string route = AppConstants.ROUTE;
                string domain = AppConstants.DOMAIN;

                string url = ("http://" + domain + "/api/sms.php?uid=" + uid + "&pin=" + pin + "&sender=" + sender + "&route=" + route + "&mobile=" + MobileNo + "&message=" + EnCodedMsg + "&pushid=1&tempid=2");
                string results = "";
                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                try
                {

                    HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

                    StreamReader sr = new StreamReader(httpres.GetResponseStream());

                    results = sr.ReadToEnd();

                    sr.Close();


                }
                catch
                {
                    return "0";
                }
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string EncodeString(string sData)
        {
            try
            {
                byte[] encData_byte = new byte[sData.Length];

                encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);

                string encodedData = Convert.ToBase64String(encData_byte);

                return encodedData;

            }
            catch (Exception ex)
            {
                throw new Exception("Error in Encryption" + ex.Message);
            }
        }
        /****************************DECRYPTION CODE STARTS HERE*************************************/
        public static string DecodeString(string sData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(sData);

                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

                char[] decoded_char = new char[charCount];

                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

                string result = new String(decoded_char);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Decryption : " + ex.Message);
                //InsertException("DecodeString", "CommonDAL", ex.Message);
            }
        }
        public static void DisplayPopUpMessage(Control control, string message, string url)
        {
            string msg = "";
            msg += "alert('" + message + "');";
            if (url != string.Empty)
            {
                msg += "window.location.href = '" + url + "';";
            }
            ScriptManager.RegisterStartupScript(control, control.GetType(), "Popup", msg, true);

        }

        public static int SendMail(string strMailSubject, string strMessage, string strMailTo)
        {
            int intStatusMessage = 0;
            try
            {
                SmtpClient smtp = new SmtpClient();

                smtp.Host = AppConstants.mailHost;
                smtp.Port = AppConstants.mailPort;
                smtp.Credentials = new NetworkCredential(AppConstants.fromMail, AppConstants.fromMailPwd);

                MailMessage message = new MailMessage();
                //message.To.Add(new MailAddress(strMailTo));
                string[] Multi = strMailTo.Split(',');
                foreach (string MultiEmailID in Multi)
                {
                    message.To.Add(new MailAddress(MultiEmailID));
                }
                message.Subject = strMailSubject;
                message.From = new MailAddress(AppConstants.fromMail);
                message.IsBodyHtml = true;
                message.Body = strMessage;
                smtp.EnableSsl = true;
                smtp.Send(message);
                intStatusMessage = 1;
                return intStatusMessage;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SendMailWithAttachment(string strMailSubject, string strMessage, string strMailTo, string strMailCC, string strBCC, string strAttachPath)
        {
            int intStatusMessage = 0;
            MailMessage message = null;
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Host = AppConstants.mailHost;
                smtp.Port = AppConstants.mailPort;
                smtp.Credentials = new NetworkCredential(AppConstants.fromMail, AppConstants.fromMailPwd);

                message = new MailMessage();
                message.From = new MailAddress(AppConstants.fromMail);

                //if Multiple recipients
                string[] Multi = strMailTo.Split(',');
                foreach (string MultiEmailID in Multi)
                {
                    if (MultiEmailID != "" && MultiEmailID != string.Empty)
                        message.To.Add(new MailAddress(MultiEmailID));
                }
                //if Multiple CC recipients
                string[] MultiCC = strMailCC.Split(',');
                foreach (string EmailIDCC in MultiCC)
                {
                    if (EmailIDCC != "" && EmailIDCC != string.Empty)
                        message.CC.Add(new MailAddress(EmailIDCC));
                }

                //if Multiple BCC recipients
                string[] MultiBCC = strBCC.Split(',');
                foreach (string EmailIDBCC in MultiBCC)
                {
                    if (EmailIDBCC != "" && EmailIDBCC != string.Empty)
                        message.Bcc.Add(new MailAddress(EmailIDBCC));
                }

                //recipients

                message.Subject = strMailSubject;
                message.IsBodyHtml = true;
                message.Body = strMessage;

                if (strAttachPath != null && strAttachPath != string.Empty)
                {
                    Attachment attachment = new System.Net.Mail.Attachment(strAttachPath);
                    message.Attachments.Add(attachment);
                }

                smtp.Send(message);
                intStatusMessage = 1;

                return intStatusMessage;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #region For Error Handling

        public static string InsertException(String FunctionName, String MODULE_NAME, String ERROR_TYPE, String ERROR_DESC, String url, string Line_No)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            string Error_Refno = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@P_FUNCTION_NAME", FunctionName);
                ht.Add("@P_MODULE_NAME", MODULE_NAME);
                ht.Add("@P_ERROR_TYPE", ERROR_TYPE);
                ht.Add("@P_ERROR_DESC", ERROR_DESC);
                ht.Add("@P_ERROR_Page", url);
                ht.Add("@Line_No", Line_No);
                ht.Add("@ERROR_REFNO_out", "");
                Error_Refno = oSqlHelper.ExecuteQueryWithOutParamINString("PROC_INSERT_ERROR_DETAILS", ht);
                GenerateMailFormat(FunctionName, MODULE_NAME, ERROR_TYPE, ERROR_DESC, url, Line_No, Error_Refno);
                return Error_Refno;
            }
            catch (Exception ex)
            {
                return Error_Refno;
            }
        }

        public static void GenerateMailFormat(String FunctionName, String ModuleName, String ErrorType, String ErrorDesc, String url, string Line_no, string Error_Refno)
        {
            try
            {
                string body = PopulateBody(FunctionName, ModuleName, ErrorType, ErrorDesc, url, Line_no, Error_Refno);

                //Pass Parameters to SendMail function
                SendMailWithAttachment("#Bug Details - SMARTCITY", body, "raghubeer.singh@vedang.net", string.Empty, string.Empty, string.Empty);
            }
            catch (Exception ex)
            {

            }

        }

        protected static string PopulateBody(String FunctionName, string ModuleName, String ErrorName, String ErrorDesc, string url, string Error_Line_No, string Error_Refno)
        {
            try
            {
                string body = string.Empty;             
                string path = HttpContext.Current.Server.MapPath(@"~/Views/Shared/Error1.html");
                using (StreamReader reader = new StreamReader(path))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{FunctionName}", FunctionName);
                body = body.Replace("{ModuleName}", ModuleName);
                body = body.Replace("{ErrorName}", ErrorName);
                body = body.Replace("{ErrorDesc}", ErrorDesc);
                body = body.Replace("{url}", url);
                body = body.Replace("{LineNo}", Error_Line_No);
                string IP = GetIPAddress();
                body = body.Replace("{Error_Refno}", Error_Refno + " <br/>Client IP Address: " + IP);
                return body;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        #endregion

        //RB Singh 22-05-2019
        public static string GetRandomPassword(int length)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string password = string.Empty;
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow Repetation of Characters            
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }

        //RB Singh 22-05-2019

        #region  Added by sachin 16-01-2020 for Ency & Decry
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public static string EncryptWithSalt(string plainText,string SaltKeys)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKeys)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DecryptWithSaltKey(string encryptedText,string SaltKeys)
        {
            try
            {


                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKeys)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            catch (Exception)
            {

                return "Error";
            }
        }

        #endregion

        #region For AuditTrail

        public int AuditTrail(string UserName ,string CurrentURL,string IP,int ForAction,string LoginStatusDetails)
        {
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("p_UserName",UserName),
                                             new MySqlParameter("p_CurrentAction",CurrentURL),
                                             new MySqlParameter("p_ForAction",ForAction),
                                             new MySqlParameter("p_Ip",IP),
                                             new MySqlParameter("p_LoginStatusDetails",LoginStatusDetails)
                                        };
            int result = sql.execStoredProcudure("PROC_UPDATE_AUDIT_TRAIL_DETAILS", spmLogin);
            return result;
            //Hashtable ht = new Hashtable();
            //                ht.Add("@UserName", UserName);         
            //                ht.Add("@CurrentAction", CurrentURL);
            //                ht.Add("@ForAction", ForAction);
            //                ht.Add("@Ip", IP);
            //                ht.Add("LoginStatusDetails", LoginStatusDetails);
            //                ht.Add("@MSG_out", "");
            //                int result = osqlHelper.ExecuteQueryWithOutParam("PROC_UPDATE_AUDIT_TRAIL_DETAILS", ht);
            //return result;
        }
        #endregion



    }
}