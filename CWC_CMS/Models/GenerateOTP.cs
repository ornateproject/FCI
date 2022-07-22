using CWC_CMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Net.Mail;
using System.Web;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Net;

namespace SIDCUL.Models
{
    public class GenerateOTP
    {

        public string OTP { get; set; }
        public string Generate()
        {
            string characters = "1234567890";
            int length = 6;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }


        public string ActionsPerformedWhenOTPIsValid(int VigilanceID)
        {
            string result = "";
            #region Renu 03 Feb 2020 For Mysql Start 
            // Renu 03 Feb 2020 For Mysql Start 
            DataSet ds = new DataSet();
            SqlHelper sql = new SqlHelper();
            MySqlParameter[] spmLogin = {
                                            new MySqlParameter("p_VigilanceID", VigilanceID)
                                        };
            result = sql.execStoredProcudureInString("PROC_UPDATE_VIGILANCE_DETAILS_WHEN_OTP_IS_VALID", spmLogin);
            #endregion	

            //SqlHelper oSqlHelper = new SqlHelper();
            //Hashtable ht = new Hashtable();
            //ht.Add("@VigilanceID", VigilanceID);
            //ht.Add("@UserNamePassword_out", "NA");
            //result = oSqlHelper.ExecuteQueryWithOutParamINString("PROC_UPDATE_VIGILANCE_DETAILS_WHEN_OTP_IS_VALID", ht);
            return result;
        }

        #region For Mail
        public void GenerateMailFormat(string DisplayName, String EmailID, String LoginName, string Password)
        {

            string link = "Now you can login in FCI Portal";
            string body = this.PopulateBody(DisplayName, EmailID, LoginName, Password, link);
            string Sub = "User login Details ";

            this.sendMail(EmailID, Sub, body, link);

        }

        public void GenerateMailFormatForOTP(string DisplayName, String EmailID, string OTP)
        {

            string body = this.PopulateBodyForOTP(DisplayName, EmailID, OTP);
            string Sub = "FCI Vigilance Department OTP Details ";

            this.sendMail(EmailID, Sub, body, OTP);

        }



        public void GenerateMailForChangeStatus(string ApplicationRefNo, string DisplayName, String EmailID, string Remarks, string status, string SubmitDate)
        {

            string body = this.PopulateBodyForChangeStatus(ApplicationRefNo, DisplayName, EmailID, Remarks, status, SubmitDate);
            string Sub = "Complaint Management System";

            this.sendMailForChangeStatus(EmailID, Sub, body, Remarks);

        }


        private string PopulateBodyForChangeStatus(string ApplicationRefNo, string DisplayName, String EmailID, string Remarks, string status, string SubmitDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Views/ChangeStatus.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{Remarks}", Remarks);
            body = body.Replace("{ApplicationSubmitDate}", SubmitDate);
            body = body.Replace("{status}", status);
            body = body.Replace("{ApplicationRefNo}", ApplicationRefNo);


            return body;
        }

        private void sendMailForChangeStatus(string EmailID, string Subject, string Body, string Remarks)
        {
            //MailMessage msg = new MailMessage();
            try
            {
                //msg.From = new MailAddress(AppConstants.fromMail);
                if (EmailID != "" || EmailID != string.Empty)
                {
                    //msg.To.Add(EmailID);
                    //msg.Body = Body;
                    //msg.IsBodyHtml = true;
                    //msg.Subject = Subject;
                    //SmtpClient smt = new SmtpClient(AppConstants.mailHost);
                    //smt.Port = AppConstants.mailPort;
                    //smt.Credentials = new NetworkCredential(AppConstants.fromMail, AppConstants.fromMailPwd);
                    //smt.EnableSsl = true;
                    //smt.Send(msg);

                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("FCI", AppConstants.fromMail));
                    message.To.Add(MailboxAddress.Parse(EmailID));
                    message.Subject = Subject;
                    //message.Body = emailModel.Message;
                    // message.Subject = emailModel.Subject;
                    var builder = new BodyBuilder();
                    builder.HtmlBody = Body;

                    message.Body = builder.ToMessageBody();

                    //  System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    using (var client = new SmtpClient())
                    {
                        client.Connect(AppConstants.mailHost, 465, SecureSocketOptions.SslOnConnect);

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(AppConstants.fromMail, AppConstants.fromMailPwd);

                        client.Send(message);
                        client.Disconnect(true);
                    }

                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
           
        }




        public void GenerateMailFormatForApplicationRefno(string DisplayName, String EmailID, string ApplicationRefno, string SubmitDate)
        {

            string body = this.PopulateBodyForApplicationRefno(DisplayName, EmailID, ApplicationRefno, SubmitDate);
            string Sub = "FCI Vigilance Complaint Successful Submit Message ";

            this.sendMail(EmailID, Sub, body, ApplicationRefno);

        }
        public void GenerateMailFormatForPersonalDivision(string DisplayName, string ApplicationRefNo, String Organization, string DetailsOfAllegation, DateTime DateOfAllegation, string EmailID, string SubmitDate, string ComplainyMobileNo, string ComplainyEmailId)
        {

            string body = this.PopulateBodyForPersonalDivision(DisplayName, EmailID, ApplicationRefNo, Organization, DetailsOfAllegation, DateOfAllegation, SubmitDate, ComplainyMobileNo, ComplainyEmailId);
            string Sub = "FCI General Complaint Detail";

            this.sendMail(EmailID, Sub, body, ApplicationRefNo);

        }
        public void GenerateMailFormatForEWCDivision(string DisplayName, string ApplicationRefNo, String Organization, string DetailsOfAllegation, DateTime DateOfAllegation, string EmailID, string SubmitDate, string ComplainyMobileNo, string ComplainyEmailId)
        {

            string body = this.PopulateBodyForEWCDivision(DisplayName, EmailID, ApplicationRefNo, Organization, DetailsOfAllegation, DateOfAllegation, SubmitDate, ComplainyMobileNo, ComplainyEmailId);
            string Sub = "Employee Walfare Cell Complaint Detail";

            this.sendMail(EmailID, Sub, body, ApplicationRefNo);

        }


        private string PopulateBodyForEWCDivision(string DisplayName, String EmailID, String ApplicationRefNo, string Organization, string DetailsOfAllegation, DateTime DateOfAllegation, string SubmitDate, string ComplainyMobileNo, string ComplainyEmailId)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Views/EWCpage.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{ApplicationRefNo}", ApplicationRefNo);
            body = body.Replace("{Organization}", Organization);
            body = body.Replace("{DetailsOfAllegation}", DetailsOfAllegation);
            body = body.Replace("{SubmitDate}", SubmitDate);
            body = body.Replace("{EWC}", "Employee Walfare Cell");
            body = body.Replace("{ComplainyMobileNo}", ComplainyMobileNo);
            body = body.Replace("{ComplainyEmailId}", ComplainyEmailId);
            return body;
        }



        private string PopulateBodyForPersonalDivision(string DisplayName, String EmailID, String ApplicationRefNo, string Organization, string DetailsOfAllegation, DateTime DateOfAllegation, string SubmitDate, string ComplainyMobileNo, string ComplainyEmailId)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Views/ComplaintPersonalDivision.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{ApplicationRefNo}", ApplicationRefNo);
            body = body.Replace("{Organization}", Organization);
            body = body.Replace("{DetailsOfAllegation}", DetailsOfAllegation);
            body = body.Replace("{SubmitDate}", SubmitDate);
            body = body.Replace("{G}", "General");
            body = body.Replace("{ComplainyMobileNo}", ComplainyMobileNo);
            body = body.Replace("{ComplainyEmailId}", ComplainyEmailId);
            return body;
        }




        private string PopulateBody(string DisplayName, String EmailID, String LoginName, string Password, string Link)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Views/AddUsers.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{LoginName}", LoginName);
            body = body.Replace("{Password}", Password);
            body = body.Replace("{Link}", Link);

            return body;
        }

        private string PopulateBodyForOTP(string DisplayName, String EmailID, string OTP)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Views/OTPMail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{OTP}", OTP);


            return body;
        }


        private string PopulateBodyForApplicationRefno(string DisplayName, String EmailID, string ApplicationRefno, string SubmitDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Views/ApplicationRefnoMail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{ApplicationRefno}", ApplicationRefno);
            body = body.Replace("{ApplicationSubmitDate}", SubmitDate);


            return body;
        }

        private void sendMail(string EmailID, string Subject, string Body, string link)
        {
           // MailMessage msg = new MailMessage();
            try
            {
              //  msg.From = new MailAddress(AppConstants.fromMail);
                if (EmailID != "" || EmailID != string.Empty)
                {
                    //msg.To.Add(EmailID);
                    //msg.Body = Body;
                    //msg.IsBodyHtml = true;
                    //msg.Subject = Subject;
                    //SmtpClient smt = new SmtpClient(AppConstants.mailHost);
                    //smt.Port = AppConstants.mailPort;
                    //smt.UseDefaultCredentials = false;
                    //smt.Credentials = new NetworkCredential(AppConstants.fromMail, AppConstants.fromMailPwd);
                    //smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                    ////smt.EnableSsl = true;
                    //smt.EnableSsl = false;
                    //smt.Send(msg);


                    MimeMessage message = new MimeMessage();
                    message.From.Add(new MailboxAddress("FCI", AppConstants.fromMail));
                    message.To.Add(MailboxAddress.Parse(EmailID));
                    message.Subject = Subject;
                    //message.Body = emailModel.Message;
                    // message.Subject = emailModel.Subject;
                    var builder = new BodyBuilder();
                    builder.HtmlBody = Body;

                    message.Body = builder.ToMessageBody();

                    //  System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    using (var client = new SmtpClient())
                    {
                        client.Connect(AppConstants.mailHost, 465, SecureSocketOptions.SslOnConnect);

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(AppConstants.fromMail, AppConstants.fromMailPwd);

                        client.Send(message);
                        client.Disconnect(true);
                    }

                }

                //string fromaddr = "cwcgrievanceredressalportal@gmail.com";
                //string toaddr = "tokumarneeraj@gmail.com";//TO ADDRESS HERE
                //string password = "Cwc@2020";

            //MailMessage msg = new MailMessage();
            //msg.Subject = "Username &password";
            //msg.From = new MailAddress(fromaddr);
            //msg.Body = "Message BODY";
            //msg.To.Add(new MailAddress(toaddr));
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = false;
            //smtp.EnableSsl = false;
            //NetworkCredential nc = new NetworkCredential(fromaddr, password);
            //smtp.Credentials = nc;
            //smtp.Send(msg);
        }
            catch (Exception Ex)
            {
                throw Ex;
            }

           
           
        }
        #endregion

        //Renu 17 Jan 2020 for change password mail system
        #region For Change Password Mail   
        public void GenerateMailFormatForChangePassword(string DisplayName, String EmailID, String LoginName, string Password)
        {

            string link = "Now you can login in FCI Portal";
            string body = this.PopulateBodyForChangePassword(DisplayName, EmailID, LoginName, Password, link);
            string Sub = "User login Details ";

            this.sendMail(EmailID, Sub, body, link);

        }

        private string PopulateBodyForChangePassword(string DisplayName, String EmailID, String LoginName, string Password, string Link)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Views/AddUsers.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{DisplayName}", DisplayName);
            body = body.Replace("{LoginName}", LoginName);
            body = body.Replace("{Password}", Password);
            body = body.Replace("{Link}", Link);


            return body;
        }
        #endregion

        #region
        //SMS Integration By Pankaj on 27 Feb 2020
        public void SMSFOROTP(string DisplayName, string MobileNO, string OTP)
        {
            //Your user name
            string user = "CwareC";
            //Your authentication key
            string key = "7df934f306XX";
            //Multiple mobiles numbers separated by comma
            string mobile = "+91" + MobileNO;
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderid = "CWCWMS";
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode("Dear" + " " + DisplayName + " " + "FCI Vigilance Department OTP Details" + " " + "Your OTP:-" + OTP);
            string accounrusageId = "1";

            string sbPstData = "user=" + user + "&key=" + key + "&mobile=" + mobile + "&message=" + message + "&senderid=" + senderid + "&accusage=" + accounrusageId;

            try
            {
                //Call Send SMS API
                string sendSMSUri = "http://mobicomm.dove-sms.com/submitsms.jsp?";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                System.Text.UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPstData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {

                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                //  MessageBox.Show(ex.Message.ToString());
            }

        }

        public void SMSFORComplaint(string DisplayName, string MobileNO, string ApplicationRefNo)
        {
            //Your user name
            string user = "CwareC";
            //Your authentication key
            string key = "7df934f306XX";
            //Multiple mobiles numbers separated by comma
            string mobile = "+91" + MobileNO;
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderid = "CWCWMS";
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode("Dear" + "  " + DisplayName + "," + " " + "Your Application with Reference No" + " " + ApplicationRefNo + " " + "has been successfully submitted to the Vigilance Department of CWC.if complaint found false causing harrassment to employees,legal prosecution may be initiated");
            string accounrusageId = "1";

            string sbPstData = "user=" + user + "&key=" + key + "&mobile=" + mobile + "&message=" + message + "&senderid=" + senderid + "&accusage=" + accounrusageId;

            try
            {
                //Call Send SMS API
                string sendSMSUri = "http://mobicomm.dove-sms.com/submitsms.jsp?";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                System.Text.UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPstData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {

                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                //  MessageBox.Show(ex.Message.ToString());
            }

        }
        #endregion


    }
}