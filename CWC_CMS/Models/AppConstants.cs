using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;
namespace CWC_CMS.Models
{
    public class AppConstants
    {

        public const string succesLoginStatus = "S";
        public const string failLoginStatus = "F";
        //public const string defaultPswd = "information@123";
        //public const string fromMail = "sachin.bhatt@vedang.net";
        //public const string fromMailPwd = "information@123";
        //public const string mailHost = "smtp.bizmail.yahoo.com";

        //Added by sachin for Online Payment
        public const string MerchantCode = "T5043";
        public const string RequestTransaction = "T";
        public const string RequestOfflineVerification = "O";
        public const string RequestRealTimeVerification = "S";
        public const string RequestRefund = "R";
        public const string Blank = "NA";
        public const string CurrencyCode = "INR";
        public const string Path = "C:\\PropertyFile\\Merchant.Property";
        //
        //public const string defaultPswd = "123456";
        //public const string fromMail = "vedangtestmail@gmail.com";
        //public const string fromMailPwd = "Sc@km@0007";
        //public const string mailHost = "smtp.gmail.com";

        public const string defaultPswd = "123456";
        public const string fromMail = "cwcgrievanceredressalportal@gmail.com";//"pyadav@ornatets.com"; // "projectnoreply9@gmail.com";//"cwcgrievanceredressalportal@gmail.com";
        public const string fromMailPwd = "Cwc@2020";//"Pankaj@123";// "projectnoreply9@8";//"Cwc@2020";
        public const string mailHost = "smtp.gmail.com";//"mail.ornatets.com";//"smtp.gmail.com";

        public const int mailPort = 587;
        public const string AppRefNo = "SIIDCUL_";
        public const string AppWaterRefNo = "WATER_";
        public const string AppTransferRefNo = "Transfer_";
        public const string AppMortgageRefNo = "Mortgage_";
        public const string AppSublettingRefNo = "Subletting_";
        public const string AppCompanyRefNo = "ChangeCompanyName_";
        public const string AppReconstitutionRefNo = "Reconstitution_";
        public const string RejectReason = "rejected";
        public const string AppGSTRefNo = "SIIDCUL_";
        public const string TransferfirstSatus = "Under Process at Regional Office";
        public const string ApprovalReason = "Approved";
        public const string ApprovalReasonRO = "Approved By Regional Office";
        public const string AppProductChangeRefNo = "ProductChange_";

        public const string AllotedReason = "allotted";
        public const string Clarification = "Clarification Short By User";
        public const string UnderProcessHO = "Under Process at Head Office";
        public const string confirmDelMessage = "Are you sure you want to Delete Information?";

        public static string pswdMail = "F";
        public static string pswdChange = "F";

        //Nadim for village upload
        public static int pageSize = 5;
        public static string hindiFontName = "shusha";
        public static string localFontName = "TL-TTHemalatha";


        public const string OneTimePayment = "one time";
        public const string HalfYearlyPayment = "half yearly";
        public const string AnnuallPayment = "yearly";

        //SOCB  mansi 25-feb-2013 for themes
        public const string DefaultTheme = "BlueMoon";
        //EOCB  mansi 25-feb-2013 for themes
        //SOCB  shubham bahuguna 13-nov-2013 for bind registration no. without using Industrial area dropdown on page
        public const int Default_Ind_Area = 12;
        //EOCB  shubham bahuguna 13-nov-2013 
        #region Enumerations
        #endregion
        #region SMSGetway
        public const string UID = "5349494443554c";
        public const string PIN = "098e0e924c45ed4c2108c43a24505016";
        public const string SENDER = "SIDCUL";
        public const string ROUTE = "5";
        public const string DOMAIN = "www.smsalertbox.com";
        #endregion

        // Service Rec no and processing fees constant
        public const int LandServiceID = 3;
        public const int TimeExtensionServiceID = 53;
        public const int RestorationServiceID = 52;
        public const int SublettingServiceID = 51;
        public const int SublettingExtensionServiceID = 5111;

        public const int NOCForIndustrialLeaseServiceID = 50001;
        public const int NOCForResidentialSubleaseServiceID = 50002;
        public const int NOCForResidentialLeaseServiceID = 50003;

        public static int FlatTransferServiceID = 400;
        public static int ResidentialTransferServiceID = 401;
        public static int ApplyForProvisionalTransferServiceID = 402;
        public static int FinalTransferServiceID = 403;
        public static int ResidentialMortgageServiceID = 404;

        public static int ServiceIDForOtherFeesPayment = 405;
        public static int TransferEligibilityCheckServiceID = 2;
        public static string RMCheckPointDropdownDisplay1 = "Yes";
        public static string RMCheckPointDropdownValue1 = "Yes";
        public static string RMCheckPointDropdownDisplay2 = "No";
        public static string RMCheckPointDropdownValue2 = "No";

        public static int ReconstitutionServiceID = 50;
        public static int MortgageServiceID = 49;

        public const decimal SublettingProcessingFee = 10000;
        public const int WaterProcessingFee = 2000;


        public const int SurrenderOfPlotServiceID = 281;
        public const int WaterConnectionServiceID = 16;
        public const int ChangeInProductServiceID = 204;
        public const int ChangeCompanyNameServiceID = 55;
        public const int CancellationOfPlotServiceID = 00;

        #region Error
        public void InsertException(String FunctionName, String MODULE_NAME, String ERROR_TYPE, String ERROR_DESC, String url, string Line_No)
        {
            Hashtable ht = new Hashtable();

            ht.Add("@P_FUNCTION_NAME", FunctionName);
            ht.Add("@P_MODULE_NAME", MODULE_NAME);
            ht.Add("@P_ERROR_TYPE", ERROR_TYPE);
            ht.Add("@P_ERROR_DESC", ERROR_DESC);
            ht.Add("@P_ERROR_Page", url);
            ht.Add("@Line_No", Line_No);

            GenerateMailFormat(FunctionName, MODULE_NAME, ERROR_TYPE, ERROR_DESC, url, Line_No);

        }


        protected void GenerateMailFormat(String FunctionName, String ModuleName, String ErrorType, String ErrorDesc, String url, string Line_no)
        {
            string body = this.PopulateBody(FunctionName, ModuleName, ErrorType, ErrorDesc, url, Line_no);

            //Pass Parameters to SendMail function
            this.sendMail("nikhil.sharma@vedang.net", Convert.ToString("#Bug Details - SIIDCUL"), body);

        }

        private string PopulateBody(String FunctionName, string ModuleName, String ErrorName, String ErrorDesc, string url, string Error_Line_No)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("ErrorPage.aspx")))
            {
                body = reader.ReadToEnd();
            }
            #region LocalIP
            string LocalIP = string.Empty;
            try
            {
                String hostName = System.Net.Dns.GetHostName();
                IPHostEntry localIpAddresses = Dns.GetHostEntry(hostName);
                LocalIP = "<em>Local IP Address is:  " + localIpAddresses.AddressList[0].ToString() + "</em><br />";
            }
            catch (Exception ex)
            {
                LocalIP = ex.Message;
            }
            #endregion
            #region ExternalIP
            string ExternalIP = string.Empty;
            try
            {
                // External IP Address (get your external IP locally)
                UTF8Encoding utf8 = new UTF8Encoding();
                WebClient webClient = new WebClient();
                String externalIps = utf8.GetString(webClient.DownloadData("http://whatismyip.com/automation/n09230945.asp"));
                ExternalIP = "<h2>Your External IP Address is: " + externalIps + "</h2><br />";
            }
            catch (Exception ex)
            {
                ExternalIP = ex.Message;

            }
            #endregion
            #region RemoteIP
            string RemoteIP = string.Empty;
            try
            {
                // Remote IP Address (useful for getting user's IP from public site; run locally it just returns localhost)
                string remoteIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(remoteIpAddress))
                {
                    remoteIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                RemoteIP = "<em>Your Remote IP Address is:  " + remoteIpAddress + "</em><br />";
            }
            catch (Exception ex)
            {
                RemoteIP = ex.Message;
            }
            #endregion
            string Username = string.Empty;
            if (HttpContext.Current.Session["LoginName"] != null)
            {
                Username = HttpContext.Current.Session["LoginName"].ToString();
            }
            else
            {
                Username = "Session expired of User";
            }

            body = body.Replace("{FunctionName}", FunctionName);
            body = body.Replace("{ModuleName}", ModuleName);
            body = body.Replace("{ErrorName}", ErrorName);
            body = body.Replace("{ErrorDesc}", ErrorDesc);
            body = body.Replace("{url}", url);
            body = body.Replace("{LineNo}", Error_Line_No);
            body = body.Replace("{date}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") +
                                    @"<br/>  Login_name: " + Username + "<br/>" +
                                    LocalIP + " <br/> " + ExternalIP + " <br/> " + RemoteIP);
            return body;
        }

        //public const string DefaultTheme = "Butterfly";
        private void sendMail(string EmailID, string Subject, string Body)
        {
            MailMessage msg = new MailMessage();
            try
            {
                msg.From = new MailAddress(AppConstants.fromMail);
                if (EmailID != "" || EmailID != string.Empty)
                {
                    string[] multi = EmailID.Split(',');
                    foreach (string EM in multi)
                    {
                        msg.To.Add(EM);
                    }
                    msg.Body = Body;
                    msg.IsBodyHtml = true;
                    msg.Subject = Subject;
                    SmtpClient smt = new SmtpClient(AppConstants.mailHost);
                    smt.Port = AppConstants.mailPort;
                    smt.Credentials = new NetworkCredential(AppConstants.fromMail, AppConstants.fromMailPwd);
                    smt.EnableSsl = true;
                    smt.Send(msg);
                }

            }
            catch (Exception Ex)
            {

            }
            finally
            {

                msg.Dispose();
            }
        }
        #endregion
        //public const string AllotedReason = "allotted";
        //public const string Clarification = "Clarification Short By User";
        //public const string UnderProcessHO = "Under Process at Head Office";
        //public const string confirmDelMessage = "Are you sure you want to Delete Information?";

        //public static string pswdMail = "F";
        //public static string pswdChange = "F";
    }
}