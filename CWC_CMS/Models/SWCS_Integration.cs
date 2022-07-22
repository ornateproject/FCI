using SIDCUL.Areas.Services.Models;
using SIDCUL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using CWC_CMS.Models;

namespace SIDCUL.Areas.Services.Models
{
    public class SWCS_Integration
    {
        public static SWCS swcs1 { get; set; }

        # region Integration
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
                //return System.BitConverter.ToString(hashBytes); 
            }
        }

        public static HttpWebResponse GetResponse(string url)
        {


            HttpWebRequest webRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);

            //  webRequest.Proxy = new System.Net.WebProxy("your proxy server", true); // comment out if you're not going thru a proxy
            webRequest.AllowAutoRedirect = true;
            webRequest.Timeout = 1000 * 30;
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            webRequest.PreAuthenticate = true;
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            return (HttpWebResponse)webRequest.GetResponse();
        }
        public static string GetHMAC(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha1 = new HMACSHA1(keyByte))
            {
                byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        public static string GetMd5(string Msg)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, Msg);
                if (VerifyMd5Hash(md5Hash, Msg, hash))
                {
                    return hash;
                }
                else
                {
                    return "";
                }

            }

        }
        public static string GetMACSHA1(string Key, string Msg)
        {
            string hmac1 = "";
            string hmac2 = "";
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(Key);

            HMACMD5 hmacmd5 = new HMACMD5(keyByte);
            HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);

            byte[] messageBytes = encoding.GetBytes(Msg);
            byte[] hashmessage = hmacmd5.ComputeHash(messageBytes);
            hmac1 = ByteToString(hashmessage);

            byte[] hashmessage1 = hmacsha1.ComputeHash(messageBytes);

            hmac2 = ByteToString(hashmessage1);
            return hmac2;



        }
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("x2"); // hex format
            }
            return (sbinary);
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetResponseAsString(string url, int timeout, Dictionary<string, string> postParameters)
        {

            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                string postData = "";
                foreach (string key in postParameters.Keys)
                {
                    postData += HttpUtility.UrlEncode(key) + "="
                          + HttpUtility.UrlEncode(postParameters[key]) + "&";
                }
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                // return Response 
                return responseFromServer;
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    return text;
                }

            }
        }

        #endregion

     
        public static string Save_Document(string Documentcode, string iuid, string fileName, int applicantRecno, int serviceID)
        {
            string filepath = string.Empty;
            try
            {
                if (fileName != null)
                {
                    string[] str = fileName.Split('.');
                    int length = str.Length;
                    string NewFileName = string.Empty;

                    // check the file is openable or not
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp"));
                    }
                    string url = ConfigurationManager.AppSettings["SWCSDocument"].ToString() + iuid + "/" + fileName;
                    // string url = "http://investuttarakhand.co.in/themes/backend/mydoc/" + iuid + "/" + fileName;

                    WebClient client = new WebClient();
                    filepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName);
                    client.DownloadFile(url, filepath);                // @"E:\\DownloadPdf.pdf");

                    iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(HttpContext.Current.Server.MapPath("~/Temp/") + fileName);
                    oPdfReader.Close();

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Temp/") + fileName) == true)
                    {
                        string docsaveLocationPath = string.Empty;
                        docsaveLocationPath = @"~/Document/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMMM") + "/Service_" + serviceID + "/ApplicantRecno_" + applicantRecno + "/";
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(docsaveLocationPath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(docsaveLocationPath));
                        }
                        string serverPath = HttpContext.Current.Server.MapPath(docsaveLocationPath);
                        string[] array1 = Directory.GetFiles(serverPath, Documentcode + "*.pdf");
                        int documentCount = array1.Length;

                        var sourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName);

                        if (documentCount > 0)
                        {
                            NewFileName = Documentcode + "_V1." + documentCount + '.' + str[str.Length - 1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;

                        }
                        else
                        {
                            NewFileName = Documentcode + "_V1.0" + '.' + str[str.Length - 1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;

                        }
                    }

                }
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Temp/") + fileName) == true)
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Temp/") + fileName);
                }
                return null;
            }
            catch (Exception ex)
            {
                //string Error_refno = string.Empty;

                //ErrorModel.Function_Name = "Time Extension Application";
                //ErrorModel.Module_Name = "Save Document to Local";
                //ErrorModel.Error_Type = "Application";
                //ErrorModel.Error_Desc = ex.Message;
                //ErrorModel.Line_No = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                //ErrorModel.Url =HttpContext.Current.Request.Url.AbsoluteUri;
                //ErrorModel.IP_Address = CommonDAL.GetIPAddress();
                //ErrorModel.Login_Name = "Client";
                //Error_refno = ErrorModel.SaveError();
                return null;
            }
            return filepath;
        }

        public static int Insert_Update(int Application_Recno, int Serviceid, int DocumentRecno, string Upload_Status, string Approved_FailStatus, string filePath, string FileName, string DocumentCode)
        {
            int returnbyproc = 0;
            try
            {
                MySave _mysave = new MySave("PROC_INSERT_UPDATE_APPLICANT_DOCUMENT_new");
                _mysave.AddParameter("@ServiceID", Serviceid);
                _mysave.AddParameter("@ServiceExtension_Recno", Serviceid);
                _mysave.AddParameter("@Application_Recno", Application_Recno);
                _mysave.AddParameter("@DocumentRecno", DocumentRecno);
                _mysave.AddParameter("@Upload_Status", Upload_Status);
                _mysave.AddParameter("@Approved_FailStatus", Approved_FailStatus);
                _mysave.AddParameter("@FilePath", filePath);
                _mysave.AddParameter("@FileName", FileName);
                _mysave.AddParameter("@DocumentCode", DocumentCode);
                returnbyproc = _mysave.ExecuteSave();
            }
            catch (Exception e)
            {

            }
            finally
            {


            }
            return returnbyproc;
        }

        public static string Upload_Single_pdf_File(HttpPostedFileBase file, int Application_no, string FolderName)
        {
            string filepath = string.Empty;
            string TempFileName = string.Empty;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    // if you want to save in folder use this
                    var fileName = Path.GetFileName(file.FileName);
                    TempFileName = fileName;
                    int MaxContentLength = 1024 * 1024 * 25; //25 MB , string[] AllowedExtension = new string[] { ".jpg", ".gif", ".png", ".pdf", ".docx", ".dotx", ".potx", ".doc",".exe" };
                    string[] AllowedExtension = new string[] { ".pdf" };

                    // doc details
                    string[] str = fileName.Split('.');
                    string NewFileName = string.Empty;

                    // check the file is openable or not
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp"));
                    }

                    file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName));
                    iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(HttpContext.Current.Server.MapPath("~/Temp/") + fileName);
                    oPdfReader.Close();

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Temp/") + fileName) == true)
                    {
                        string docsaveLocationPath = string.Empty;
                        docsaveLocationPath = @"~/Document/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMMM") + "/" + FolderName + "/Applicant_Recno" + Application_no + "/";
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(docsaveLocationPath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(docsaveLocationPath));
                        }
                        string serverPath = HttpContext.Current.Server.MapPath(docsaveLocationPath);
                        string[] array1 = Directory.GetFiles(serverPath, FolderName + "_V1*.pdf");
                        int documentCount = array1.Length;

                        var sourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName);

                        //StreamReader reader = null;
                        //WebClient client = new WebClient();
                        //string urlAddress = "http://investuttarakhand.co.in/themes/backend/uploads/Service_Integration_Steps_API_Ver2.0.pdf";
                        //client.DownloadFile(urlAddress, @"E:\\DownloadPdf.pdf");//Path.Combine(Server.MapPath(docsaveLocationPath), NewFileName);


                        if (documentCount > 0)
                        {
                            NewFileName = FolderName + "_V1." + documentCount + '.' + str[1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;


                        }
                        else
                        {
                            NewFileName = FolderName + "_V1.0" + '.' + str[1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;
                        }
                    }

                }
                return filepath;
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Temp/") + TempFileName) == true)
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Temp/") + TempFileName);
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public static string Upload_Single_ExcelFile(HttpPostedFileBase file, string DateTimeCurrent, string FolderName, int Count)
        {
            string filepath = string.Empty;
            string TempFileName = string.Empty;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    // if you want to save in folder use this
                    var fileName = "Excel"+System.DateTime.Now.ToString();
                    TempFileName = fileName;
                    int MaxContentLength = 1024 * 1024 * 25; //25 MB , string[] AllowedExtension = new string[] { ".jpg", ".gif", ".png", ".pdf", ".docx", ".dotx", ".potx", ".doc",".exe" };
                    string[] AllowedExtension = new string[] { ".xls, xlxs" };

                    // doc details
                    string[] str = fileName.Split('.');
                    string NewFileName = string.Empty;

                    // check the file is openable or not
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp"));
                    }

                    file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName));
                    //iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(HttpContext.Current.Server.MapPath("~/Temp/") + fileName);
                    //oPdfReader.Close();

                    if (System.IO.File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/") , fileName)) == true)
                    {
                        string docsaveLocationPath = string.Empty;
                        docsaveLocationPath = @"~/Document/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMMM") + "/" + FolderName + "/Time" + DateTimeCurrent + "/";
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(docsaveLocationPath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(docsaveLocationPath));
                        }
                        string serverPath = HttpContext.Current.Server.MapPath(docsaveLocationPath);
                        string[] array1 = Directory.GetFiles(serverPath, FolderName + Count + "_V1*.pdf");
                        int documentCount = array1.Length;

                        var sourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName);

                        //StreamReader reader = null;
                        //WebClient client = new WebClient();
                        //string urlAddress = "http://investuttarakhand.co.in/themes/backend/uploads/Service_Integration_Steps_API_Ver2.0.pdf";
                        //client.DownloadFile(urlAddress, @"E:\\DownloadPdf.pdf");//Path.Combine(Server.MapPath(docsaveLocationPath), NewFileName);


                        if (documentCount > 0)
                        {
                            NewFileName = FolderName + Count + "_V1." + documentCount + '.' + str[1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;


                        }
                        else
                        {
                            NewFileName = FolderName + Count + "_V1.0" + '.' + str[1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;
                        }
                    }

                }
                return filepath;
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Temp/") + TempFileName) == true)
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Temp/") + TempFileName);
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }


        public static string Upload_Single_pdf_File_new(HttpPostedFileBase file, int Application_no, string FolderName, int Count)
        {
            string filepath = string.Empty;
            string TempFileName = string.Empty;
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    // if you want to save in folder use this
                    var fileName = Path.GetFileName(file.FileName);
                    TempFileName = fileName;
                    int MaxContentLength = 1024 * 1024 * 25; //25 MB , string[] AllowedExtension = new string[] { ".jpg", ".gif", ".png", ".pdf", ".docx", ".dotx", ".potx", ".doc",".exe" };
                    string[] AllowedExtension = new string[] { ".pdf" };

                    // doc details
                    string[] str = fileName.Split('.');
                    string NewFileName = string.Empty;

                    // check the file is openable or not
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp"));
                    }

                    file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName));
                    //iTextSharp.text.pdf.PdfReader oPdfReader = new iTextSharp.text.pdf.PdfReader(HttpContext.Current.Server.MapPath("~/Temp/") + fileName);
                    //oPdfReader.Close();

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Temp/") + fileName) == true)
                    {
                        string docsaveLocationPath = string.Empty;
                        docsaveLocationPath = @"~/Document/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMMM") + "/" + FolderName + "/Applicant_Recno" + Application_no + "/";
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(docsaveLocationPath)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(docsaveLocationPath));
                        }
                        string serverPath = HttpContext.Current.Server.MapPath(docsaveLocationPath);
                        string[] array1 = Directory.GetFiles(serverPath, FolderName + Count + "_V1*.pdf");
                        int documentCount = array1.Length;

                        var sourcePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Temp/"), fileName);

                        //StreamReader reader = null;
                        //WebClient client = new WebClient();
                        //string urlAddress = "http://investuttarakhand.co.in/themes/backend/uploads/Service_Integration_Steps_API_Ver2.0.pdf";
                        //client.DownloadFile(urlAddress, @"E:\\DownloadPdf.pdf");//Path.Combine(Server.MapPath(docsaveLocationPath), NewFileName);


                        if (documentCount > 0)
                        {
                            NewFileName = FolderName + Count + "_V1." + documentCount + '.' + str[1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;


                        }
                        else
                        {
                            NewFileName = FolderName + Count + "_V1.0" + '.' + str[1].ToString();
                            var destinationPath = Path.Combine(HttpContext.Current.Server.MapPath(docsaveLocationPath), NewFileName);
                            System.IO.File.Move(sourcePath, destinationPath);
                            filepath = destinationPath;
                        }
                    }

                }
                return filepath;
            }
            catch (iTextSharp.text.exceptions.InvalidPdfException)
            {
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Temp/") + TempFileName) == true)
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Temp/") + TempFileName);
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static int SWCS_FIRST_SUBMIT(int ApplicationPrimaryKey, int CafID, int ServiceID, string document, string UnitName)
        {


            string[] StatusData = null;
            string strApplRefNo = GenrateApplication_SubmitAndUpdate_RefranceKey(ApplicationPrimaryKey, ServiceID);
            HttpContext.Current.Session["ApplicationRefno"] = strApplRefNo;
            try
            {
                string SWCSuser_id = Convert.ToString(swcs1.RESPONSE.user_id);
                string data = GetSWCSFirstTimePostDataByApplicationRecnoAndServiceID(ApplicationPrimaryKey, ServiceID);

                if (data != "NA")
                {
                    StatusData = data.Split('$');

                    ///--------------- Code for Creation of JSON---------///
                    if (!String.IsNullOrEmpty(Convert.ToString(SWCSuser_id)))
                    {
                        string urls = ConfigurationManager.AppSettings["SWCSPOSTURL"].ToString();

                        HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(urls);

                        request.Method = "POST";

                        string sckey = "!@#$%^&*()_++_)(*&&^%%%%";
                        string application_recno = strApplRefNo;

                        string app_comments = "Application Form Submitted Suceesfully";
                        string Location_Name = Convert.ToString(StatusData[0]);
                        string District_Id = Convert.ToString(StatusData[1]);
                        string Service_Id = Convert.ToString(ServiceID);
                        string iuid = Convert.ToString(swcs1.RESPONSE.iuid);
                        string unit_name = Convert.ToString(UnitName);
                        string Caf_Id = Convert.ToString(CafID);


                        string tt1 = SWCSuser_id + application_recno;
                        string md5 = GetMd5(tt1);
                        string Getmac = GetMACSHA1(sckey, md5);
                        string sp_tag = "SIIDCUL_SWCS_$#@";
                        string app_id = application_recno;
                        string app_name = "SLMS";
                        string app_status = "P";  // application partial save
                        string user_id = SWCSuser_id;
                        //  string Application_Refno = "{ApplicationRefno:SIIDCUL_}";
                        string user_agent = HttpContext.Current.Request.UserAgent;
                        string remote_ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        string documents = document;
                        //------
                        string Print_Url = "https://www.siidculsmartcity.com/Process/ViewApplicationFormDetails.aspx?application_recno=" + CommonDAL.EncodeString(application_recno + "/" + Service_Id);
                        string Revert_Back_Url_Inco = "http://118.185.3.27:8080/Services/AdvertisementInformation?application_recno=" + CommonDAL.EncodeString(application_recno + "/" + Service_Id); ;

                        if (ServiceID == 3 || ServiceID == 51 || ServiceID == 52 || ServiceID == 48 || ServiceID == 50 || ServiceID==16  || ServiceID == 55 || ServiceID == 204)
                        {
                            Revert_Back_Url_Inco = Get_RevertBackURL(ServiceID, ApplicationPrimaryKey);
                            app_status = "I";
                            //  Revert_Back_Url_Inco = "http://localhost:1554/services/LandApplicationForm/RevertApplication?application_recno=" + CommonDAL.EncodeString(ServicePrimaryKey.ToString());
                        }

                        if (ServiceID == 281)
                        {
                            Revert_Back_Url_Inco = Get_RevertBackURL(ServiceID, ApplicationPrimaryKey);
                            app_status = "P";
                            //  Revert_Back_Url_Inco = "http://localhost:1554/services/LandApplicationForm/RevertApplication?application_recno=" + CommonDAL.EncodeString(ServicePrimaryKey.ToString());
                        }
                        string role_id = "4";
                        string role_name = "Client";
                        string role_user_info = string.Empty;
                        string next_role_id = "4";
                        //------------------------------------------------------//
                        string json = "sp_tag=" + sp_tag + "&" +
                                      "app_id=" + app_id + "&" +
                                      "app_name=" + app_name + "&" +
                                      "service_id=" + Service_Id + "&" +
                                      "iuid=" + iuid + "&" +
                                      "user_id=" + user_id + "&" +
                                      "caf_id=" + Caf_Id + "&" +
                                      "unit_name=" + unit_name + "&" +
                                      "app_distt=" + District_Id + "&" +
                                      "app_location=" + Location_Name + "&" +
                                      "app_status=" + app_status + "&" +
                                      "app_comments=" + app_comments + "&" +
                                      "api_hash=" + Getmac + "&" +
                                      "user_agent=" + user_agent + "&" +
                                      "remote_ip=" + remote_ip + "&" +
                                      "reverted_call_back_url=" + Revert_Back_Url_Inco + "&" +
                                      "print_app_call_back_url=" + Print_Url + "&" +

                                      "role_id=" + role_id + "&" +
                                      "role_name=" + role_name + "&" +
                                      "role_user_info=" + role_user_info + "&" +
                                      "next_role_id=" + next_role_id + "&" +


                                      "param_1=" + string.Empty + "&" +
                                      "param_2=" + string.Empty + "&" +
                                      "param_3=" + string.Empty + "&" +
                                      "param_4=" + string.Empty + "&" +
                                      "param_5=" + string.Empty + "&" +
                                      "documents=" + documents;

                        request.Timeout = 1000 * 30;
                        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                        request.PreAuthenticate = true;
                        request.Credentials = CredentialCache.DefaultCredentials;
                       // logger.Error("JSON String:{0}", json);
                        byte[] byteArray = Encoding.UTF8.GetBytes(json);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = byteArray.Length;
                        //request.PreAuthenticate = true;
                        Stream dataStream = request.GetRequestStream();
                        // Write the data to the request stream.
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        // Close the Stream object.
                        dataStream.Close();

                        WebResponse response = request.GetResponse();

                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {

                            var result = streamReader.ReadToEnd();
                           // logger.Error("Single Window Response:{0}", result);
                            JObject jsonDes = JObject.Parse(result);
                            string STATUS = (string)jsonDes["STATUS"];
                            if (STATUS == "200")
                            {
                                Save_SWCS_Fields(ApplicationPrimaryKey, Caf_Id, user_id, iuid, UnitName, ServiceID, strApplRefNo);
                                if (ServiceID == 281)

                                { SendEmailOfApplicationMovingStatus_FromLevelToLevel(ApplicationPrimaryKey, 281); }
                                return 1;
                            }
                            else
                            {
                                Delete_SWCS_Documents_InCaseOfFailure(ApplicationPrimaryKey, ServiceID);
                                Delete_SWCS_ApplicationFrom_DocumentMappingTable_InCaseOfFailure(ApplicationPrimaryKey, ServiceID);
                                return 0;
                            }
                        }

                    }
                }
                else
                {
                    Delete_SWCS_Documents_InCaseOfFailure(ApplicationPrimaryKey, ServiceID);
                    Delete_SWCS_ApplicationFrom_DocumentMappingTable_InCaseOfFailure(ApplicationPrimaryKey, ServiceID);
                    return 0;
                }

                return 0;
            }
            catch (WebException webex)
            {

                var pageContent = new StreamReader(webex.Response.GetResponseStream())
                                     .ReadToEnd();
                JObject jsonDes = JObject.Parse(pageContent);
                string STATUS = (string)jsonDes["STATUS"];
                if (STATUS == "200")
                {
                    return 1;

                }
                else
                {


                    Delete_SWCS_Documents_InCaseOfFailure(ApplicationPrimaryKey, ServiceID);
                    Delete_SWCS_ApplicationFrom_DocumentMappingTable_InCaseOfFailure(ApplicationPrimaryKey, ServiceID);

                    return 0; // fail update status on single windows   delete application from database;
                }
            }



        }


        public static int Save_SWCS_Fields(int ServicePrimaryKey, string Caf_Id, string SWCS_UserId, string IUID, string SWCS_UserName, int ServiceID, string ApplicationRefno)
        {

            int returnParam = 0;
            SqlHelper osqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ServicePrimaryKey", ServicePrimaryKey);
            ht.Add("@Caf_Id", Caf_Id);
            ht.Add("@SWCS_UserId", SWCS_UserId);
            ht.Add("@IUID", IUID);
            ht.Add("@SWCS_UserName", SWCS_UserName);
            ht.Add("@ServiceID", ServiceID);
            ht.Add("@ApplicationRefno", ApplicationRefno);

            returnParam = osqlHelper.ExecuteQuery("PROC_INSERT_SWCS_FIELDS", ht);

            return returnParam;

        }
        public static string GetSWCSFirstTimePostDataByApplicationRecnoAndServiceID(int PrimaryKeyRecno, int ServiceID)
        {
            String Data = "";
            SqlHelper oSqlHelper = new SqlHelper();
            string Inserted_Application_ID = string.Empty;
            Hashtable ht = new Hashtable();

            //  MySave _mysave = new MySave("Create_Land_Application");
            ht.Add("@ServiceID", ServiceID);
            ht.Add("@ServicePrimaryKey", PrimaryKeyRecno);

            DataTable dt = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_GetSWCSFirstTimePostDataByApplicationRecnoAndServiceID", ht);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Data = dt.Rows[0]["Ind_area_name"].ToString() + "$" +
                           dt.Rows[0]["DistrictName"].ToString();

                    return Data;
                }
                return "NA";
            }
            return "NA";
        }


        public static int UpdateStatusSWCS(int ServicePrimaryKey, int ServiceID, string document)
        {
            string[] StatusData = null;
            string[] StatusData1 = null;
            string data = GetSWCSPostDataByApplicationRecnoAndServiceID(ServicePrimaryKey, ServiceID);
            string data1 = GetSWCSFirstTimePostDataByApplicationRecnoAndServiceID(ServicePrimaryKey, ServiceID);


            if (data != "NA" && data1 != "NA")
            {
                StatusData = data.Split('$');
                StatusData1 = data.Split('$');

                string strApplRefNo = GenrateApplication_SubmitAndUpdate_RefranceKey(ServicePrimaryKey, ServiceID);// ServicePrimaryKey.ToString();
                HttpContext.Current.Session["ApplicationRefno"] = strApplRefNo;
                if (ServiceID == 48 || ServiceID == 50 || ServiceID == 49)
                {
                    if(HttpContext.Current.Session["ApplicationRefno"] != null)
                    {
                        strApplRefNo = HttpContext.Current.Session["ApplicationRefno"].ToString();
                    }
                    
                }
                try
                {
                    string SWCSuser_id = Convert.ToString(StatusData[0]);

                    ///--------------- Code for Creation of JSON---------///
                    if (!String.IsNullOrEmpty(Convert.ToString(SWCSuser_id)))
                    {
                        string urls = string.Empty;

                        urls = ConfigurationManager.AppSettings["SWCSUPDATEURL"].ToString();
                        HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(urls);

                        request.Method = "POST";

                        string sckey = "!@#$%^&*()_++_)(*&&^%%%%";
                        string application_recno = strApplRefNo;// Convert.ToString(ServicePrimaryKey);

                        string app_comments = StatusData[7];
                        string Location_Name = Convert.ToString(StatusData[5]);
                        string District_Id = Convert.ToString(StatusData1[1]);  // district name
                        string Service_Id = Convert.ToString(ServiceID);
                        string iuid = StatusData[2];
                        string unit_name = StatusData[6];
                        string Caf_Id = StatusData[3];


                        string tt1 = SWCSuser_id + application_recno;
                        string md5 = SWCS_Integration.CreateMD5(tt1);
                        string Getmac = SWCS_Integration.GetMACSHA1(sckey, md5);
                        string sp_tag = "SIIDCUL_SWCS_$#@";
                        string app_id = application_recno;
                        string app_name = "SLMS";
                        string app_status = "P";
                        string user_id = SWCSuser_id;
                        //  string Application_Refno = "{ApplicationRefno:SIIDCUL_}";
                        string user_agent = HttpContext.Current.Request.UserAgent;
                        string remote_ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        string documents = "";
                        //------
                        string Print_Url = "https://www.siidculsmartcity.com/Process/ViewApplicationFormDetails.aspx?application_recno=" + CommonDAL.EncodeString(application_recno + "/" + Service_Id);
                        string Revert_Back_Url_Inco = "https://www.siidculsmartcity.com/User/ApplicationForm.aspx?application_recno=" + CommonDAL.EncodeString(application_recno + "/" + Service_Id);

                        string download_certificate_call_back_url = string.Empty;  // required when application approved or rejected                       

                        string role_id = StatusData[4];
                        string role_name = StatusData[10];
                        string next_role_id = StatusData[9];
                        string role_user_info = string.Empty;

                        int StatusRecno = Convert.ToInt32(StatusData[8]);
                        if ((ServiceID == 3 || ServiceID == 51 || ServiceID == 52 || ServiceID == 53 || ServiceID == 48 || ServiceID == 50 || ServiceID == 49 || ServiceID == 16 || ServiceID == 281 || ServiceID == 55 || ServiceID == 204) && StatusRecno == 4)
                        {
                            Revert_Back_Url_Inco = Get_RevertBackURL(ServiceID, ServicePrimaryKey);

                        }
                        //if ((ServiceID == 16) && StatusRecno == 4)
                        //{
                        //    Revert_Back_Url_Inco = Get_RevertBackURL(ServiceID, ServicePrimaryKey);

                        //}

                        if (StatusRecno == 1)
                        {
                            app_status = "A";
                            download_certificate_call_back_url = "https://www.siidculsmartcity.com/User/ApplicationForm.aspx?application_recno=" + CommonDAL.EncodeString(application_recno + "/" + Service_Id);
                            if (StatusRecno == 1 && ServiceID == 16)
                            { app_status = "PD"; }
                        }
                        else if (StatusRecno == 2)
                        {
                            app_status = "R";
                            download_certificate_call_back_url = "https://www.siidculsmartcity.com/User/ApplicationForm.aspx?application_recno=" + CommonDAL.EncodeString(application_recno + "/" + Service_Id);
                        }
                        else if (StatusRecno == 4)
                        {
                            app_status = "RBI";
                        }
                        else if (StatusRecno == 8 && (ServiceID == 3 || ServiceID == 51 || ServiceID == 52 || ServiceID == 53 || ServiceID == 48 || ServiceID == 50 || ServiceID == 49 || ServiceID==16 ||  ServiceID == 281 || ServiceID == 55 || ServiceID == 204))
                        {
                            app_status = "P";
                        }
                      
                        else
                        {
                            app_status = "F";
                        }
                        
                        //------------------------------------------------------//
                        string json = "sp_tag=" + sp_tag + "&" +
                                      "app_id=" + app_id + "&" +
                                      "app_name=" + app_name + "&" +
                                      "service_id=" + Service_Id + "&" +
                                      "iuid=" + iuid + "&" +
                                      "user_id=" + user_id + "&" +
                                      "caf_id=" + Caf_Id + "&" +
                                      "unit_name=" + unit_name + "&" +
                                      "app_distt=" + District_Id + "&" +
                                      "app_location=" + Location_Name + "&" +
                                      "app_status=" + app_status + "&" +
                                      "app_comments=" + app_comments + "&" +
                                      "api_hash=" + Getmac + "&" +
                                      "user_agent=" + user_agent + "&" +
                                      "remote_ip=" + remote_ip + "&" +
                                      "reverted_call_back_url=" + Revert_Back_Url_Inco + "&" +
                                      "print_app_call_back_url=" + Print_Url + "&" +
                                      "download_certificate_call_back_url=" + download_certificate_call_back_url + "&" +

                                      "role_id=" + role_id + "&" +
                                      "role_name=" + role_name + "&" +
                                      "role_user_info=" + role_user_info + "&" +
                                      "next_role_id=" + next_role_id + "&" +


                                      "param_1=" + ServiceID.ToString() + "&" +
                                      "param_2=" + string.Empty + "&" +
                                      "param_3=" + string.Empty + "&" +
                                      "param_4=" + string.Empty + "&" +
                                      "param_5=" + string.Empty + "&" +
                                      "documents=" + documents;

                        request.Timeout = 1000 * 30;
                        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
                        request.PreAuthenticate = true;
                        request.Credentials = CredentialCache.DefaultCredentials;

                        byte[] byteArray = Encoding.UTF8.GetBytes(json);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = byteArray.Length;
                        //request.PreAuthenticate = true;
                        Stream dataStream = request.GetRequestStream();
                        // Write the data to the request stream.
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        // Close the Stream object.
                        dataStream.Close();

                        WebResponse response = request.GetResponse();
                        using (var streamReader = new StreamReader(response.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                            JObject jsonDes = JObject.Parse(result);
                            string STATUS = (string)jsonDes["STATUS"];
                            if (STATUS == "200")
                            {
                                int res = SendEmailOfApplicationMovingStatus_FromLevelToLevel(ServicePrimaryKey, ServiceID);
                                return 1;

                            }
                            else
                            {

                                Delete_LastSaved_Remark_Status(Convert.ToInt32(StatusData[13]), Convert.ToInt32(StatusData[12]), ServicePrimaryKey, ServiceID);

                                return 0;
                            }
                        }

                    }
                    return 0;
                }
                catch (WebException webex)
                {

                    var pageContent = new StreamReader(webex.Response.GetResponseStream())
                                         .ReadToEnd();
                    JObject jsonDes = JObject.Parse(pageContent);
                    string STATUS = (string)jsonDes["STATUS"];
                    if (STATUS == "200")
                    {
                        int res = SendEmailOfApplicationMovingStatus_FromLevelToLevel(ServicePrimaryKey, ServiceID);
                        return 1;

                    }
                    else
                    {

                        ErrorModel.SERVICE_RECNO = ServiceID;
                        ErrorModel.APPLICATION_RECNO = ServicePrimaryKey;
                        ErrorModel.Error_Desc_api = jsonDes.ToString();
                        ErrorModel.SaveAPIError();


                        string Error_refno = string.Empty;
                        ErrorModel.Function_Name = "Time Extension Application";
                        ErrorModel.Module_Name = "UpdateStatusSWCS";
                        ErrorModel.Error_Type = "API Application";
                        ErrorModel.Error_Desc = webex.Message;
                        ErrorModel.Line_No = webex.StackTrace.Substring(webex.StackTrace.Length - 7, 7);
                        ErrorModel.Url = HttpContext.Current.Request.Url.AbsoluteUri;
                        ErrorModel.IP_Address = CommonDAL.GetIPAddress();
                        ErrorModel.Login_Name = "Client";
                        Error_refno = ErrorModel.SaveError();

                        Delete_LastSaved_Remark_Status(Convert.ToInt32(StatusData[13]), Convert.ToInt32(StatusData[12]), ServicePrimaryKey, ServiceID);
                        return 0;
                    }
                }
            }
            else
            {
                return 0;
            }

        }

        public static string Get_RevertBackURL(int ServiceID, int ApplicationPrimaryKey)
        {
            string RevertURL = string.Empty;
            if (ServiceID == 3)
            {

                RevertURL = "http://118.185.3.27:8080/services/LandApplicationForm/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            if (ServiceID == 51)
            {

                RevertURL = "http://118.185.3.27:8080/services/Sublease/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 52)
            {

                RevertURL = "http://118.185.3.27:8080/services/RestorationOfCancelledPlot/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 53)
            {
                RevertURL = "http://118.185.3.27:8080/services/TimeExtension/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 48)
            {
                RevertURL = "http://118.185.3.27:8080/services/ApplyForProvisionalTransfer/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 49)
            {
                RevertURL = "http://118.185.3.27:8080/MortgageApplicationForm/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 50)
            {
                RevertURL = "http://118.185.3.27:8080/services/ReconstitutionApplicationForm/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 16)
            {
                RevertURL = "http://118.185.3.27:8080/services/WaterConnectionService/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 281)
            {
                RevertURL = "http://118.185.3.27:8080/services/SurrenderOfPlotService/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 55)
            {
                RevertURL = "http://118.185.3.27:8080/services/ChangeCompanyNameService/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            else if (ServiceID == 204)
            {
                RevertURL = "http://118.185.3.27:8080/services/ChangeInProductService/RevertApplication?application_recno=" + CommonDAL.EncodeString(ApplicationPrimaryKey.ToString());
            }
            return RevertURL;
        }

        public static string GetSWCSPostDataByApplicationRecnoAndServiceID(int PrimaryKeyRecno, int ServiceID)
        {
            String Data = "";
            SqlHelper oSqlHelper = new SqlHelper();
            string Inserted_Application_ID = string.Empty;
            Hashtable ht = new Hashtable();

            //  MySave _mysave = new MySave("Create_Land_Application");
            ht.Add("@ServiceID", ServiceID);
            ht.Add("@PrimaryKeyRecno", PrimaryKeyRecno);

            DataTable dt = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_GetSWCSPostDataByApplicationRecnoAndServiceID", ht);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Data = dt.Rows[0]["SWCS_UserId"].ToString() + "$" +
                           dt.Rows[0]["SWCS_User_Name"].ToString() + "$" +
                           dt.Rows[0]["IUID"].ToString() + "$" +
                           dt.Rows[0]["CAF_ID"].ToString() + "$" +
                           dt.Rows[0]["PendingAt_Role_Recno"].ToString() + "$" +
                           dt.Rows[0]["Ind_area_name"].ToString() + "$" +
                           dt.Rows[0]["Company_Name"].ToString() + "$" +
                           dt.Rows[1]["Remarks"].ToString() + "$" +
                           dt.Rows[1]["Status_Recno"].ToString() + "$" +
                           dt.Rows[1]["PendingAt_Role_Recno"].ToString() + "$" +
                           dt.Rows[0]["ROLE_NAME"].ToString() + "$" +
                           dt.Rows[1]["ROLE_NAME"].ToString() + "$" +
                           dt.Rows[0]["Remarks_RecNo"].ToString() + "$" +
                           dt.Rows[1]["Remarks_RecNo"].ToString() + "$" +

                          dt.Rows[0]["Email"].ToString() + "$" +
                          dt.Rows[1]["Email"].ToString() + "$" +

                          dt.Rows[1]["Status_Name"].ToString() + "$" +
                          dt.Rows[1]["Status_Name"].ToString() + "$" +

                          dt.Rows[1]["ServiceName"].ToString() + "$" +
                          dt.Rows[0]["ClientEmail"].ToString();
                    return Data;
                }
                return "NA";
            }

            return "NA";
        }

        public static void Delete_LastSaved_Remark_Status(int DeleteRemarkRecno, int UpdateRemarkRecno, int ServicePrimaryKey, int ServiceID)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            string Inserted_Application_ID = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("@DeleteRemarkRecno", DeleteRemarkRecno);
            ht.Add("@UpdateRemarkRecno", UpdateRemarkRecno);
            ht.Add("@ServicePrimaryKey", UpdateRemarkRecno);
            ht.Add("@ServiceID", UpdateRemarkRecno);
            oSqlHelper.ExecuteQuery("PROC_Delete_LastSaved_Remark_Status", ht);
        }

        public static void Delete_SWCS_Documents_InCaseOfFailure(int ServicePrimaryKey, int ServiceRecno)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            string Inserted_Application_ID = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("@ServicePrimaryKey", ServicePrimaryKey);
            ht.Add("@ServiceRecno", ServiceRecno);

            DataTable dt = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_GetSWCSDocumentsList", ht);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FilePath = dt.Rows[i]["FilePath"].ToString();
                    if (FilePath.Contains(".pdf"))
                    {
                        if (System.IO.File.Exists(FilePath) == true)
                        {
                            System.IO.File.Delete(FilePath);
                        }
                    }
                }

            }

        }

        public static void Delete_SWCS_ApplicationFrom_DocumentMappingTable_InCaseOfFailure(int ServicePrimaryKey, int ServiceRecno)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            string Inserted_Application_ID = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("@ServicePrimaryKey", ServicePrimaryKey);
            ht.Add("@ServiceRecno", ServiceRecno);
            oSqlHelper.ExecuteProcudere("PROC_Delete_ApplicationFrom_DocumentMappingTable", ht);

        }

        public static string GenrateApplication_SubmitAndUpdate_RefranceKey(int ApplicationPrimaryKey, int ServiceID)
        {
            string key = string.Empty;
            int Ind_Area_Code = Get_Industrial_Estate_Code(ApplicationPrimaryKey, ServiceID);
            if (ServiceID == 3) // for land service (L)
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 51) // subletting
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 53)  // timeextension
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 52) // restoration of cancel plot
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 49) // mortagage
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 50) // reconstitution 
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 48) // Provisional transfer 
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 204) // product change
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 55) // company change
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 16) // water
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }
            else if (ServiceID == 281) // surrender of plot
            {
                key = Ind_Area_Code.ToString() + ServiceID.ToString() + string.Format("{0:000}", ServiceID) + DateTime.Now.Year + ApplicationPrimaryKey;
            }

            return key;
        }

        public static int Get_Industrial_Estate_Code(int ServicePrimaryK, int Service_ID)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            int Ind_area_code = 0;
            Hashtable ht = new Hashtable();

            //  MySave _mysave = new MySave("Create_Land_Application");
            ht.Add("@ServiceID", Service_ID);
            ht.Add("@ServicePrimaryKey", ServicePrimaryK);

            DataTable dt = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_Get_IndustrialEstate_Code", ht);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Ind_area_code = Convert.ToInt32(dt.Rows[0]["Ind_area_recno"]);
                    return Ind_area_code;
                }
                return Ind_area_code;
            }
            return Ind_area_code;
        }

        // delete check point PDF file and file Path

        public static int Delete_SavedPDF_FileAnd_UpdateDatabasePath(int ServiceID, int ServicePrimaryKey, string PathArray)
        {
            int result = 0;
            SqlHelper oSqlHelper = new SqlHelper();
            string Inserted_Application_ID = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("@ServiceID", ServiceID);
            ht.Add("@ServicePrimaryKey", ServicePrimaryKey);
            DataTable dt = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_GetUnsavedData_FailerOfSWCSIntegration_ByServicePrimaryKeyAndServiceID", ht);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strFilePath = dt.Rows[0][i].ToString();
                        string FieldName = dt.Columns[i].ColumnName.ToString();
                        if (strFilePath.Contains(".pdf"))
                        {
                            if (System.IO.File.Exists(strFilePath) == true && PathArray.Contains(strFilePath))
                            {
                                System.IO.File.Delete(strFilePath);

                                string version = string.Empty;
                                version = strFilePath.Substring(strFilePath.Length - 5, 1);
                                int n = Convert.ToInt32(version);
                                if (n > 0)
                                {
                                    string OldPath = string.Empty;
                                    OldPath = strFilePath.Replace("V1." + version, "V1." + --n);
                                    UpdateExistingFile_PathInDatabase(ServiceID, ServicePrimaryKey, FieldName, OldPath);
                                }
                                else
                                {
                                    // set path is null 
                                    strFilePath = null;
                                    UpdateExistingFile_PathInDatabase(ServiceID, ServicePrimaryKey, FieldName, strFilePath);
                                }

                            }
                        }
                    }
                }
            }

            return result;
        }

        public static void UpdateExistingFile_PathInDatabase(int ServiceID, int ServicePrimaryKey, string FieldName, string NewPath)
        {
            SqlHelper oSqlHelper = new SqlHelper();
            Hashtable ht = new Hashtable();
            ht.Add("@ServicePrimaryKey", ServicePrimaryKey);
            ht.Add("@ServiceID", ServiceID);
            ht.Add("@FieldName", FieldName);
            ht.Add("@NewPath", NewPath);
            oSqlHelper.ExecuteProcudere("PROC_DeleteUnsavedPDFFilePath_FailerOfSWCSIntegration", ht);
        }

        public static bool GetSWCS_PaymentStatus(int PrimaryKeyRecno, int ServiceID)
        {
            String Data = "";
            SqlHelper oSqlHelper = new SqlHelper();
            string Inserted_Application_ID = string.Empty;
            Hashtable ht = new Hashtable();

            //  MySave _mysave = new MySave("Create_Land_Application");
            ht.Add("@ServiceID", ServiceID);
            ht.Add("@ServicePrimaryKey", PrimaryKeyRecno);

            DataTable dt = oSqlHelper.ExecuteProcudereReturnDataTable("PROC_GetSWCS_PaymentStatus", ht);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string payment = dt.Rows[0]["Payment_Status"].ToString();
                    if (payment.Trim().ToLower() == "T")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            return false;
        }

        public static SWCS GetFieldForRegisteredPropertyPage(String Token)
        {
            SWCS swcs = new SWCS();
            string docString = string.Empty;
            string docFailString = string.Empty;
            if (Token != "" && Token != string.Empty && Token != null)
            {
                string url = ConfigurationManager.AppSettings["SWCSURL"].ToString() + Token;
                //string url = "http://investuttarakhand.co.in/sso/apiv1/gettokeninfo/token/" + Token;
                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpWebRequest.AllowAutoRedirect = true;
                    httpWebRequest.PreAuthenticate = true;
                    httpWebRequest.Timeout = 150000;
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.Accept = "application/json";
                    WebResponse webResponse = null;
                    webResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var pageContent = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                    if (pageContent != "")
                    {
                        JObject jsonDes = JObject.Parse(pageContent);
                        string STATUS = (string)jsonDes["STATUS"];
                        if (STATUS == "200")
                        {
                            swcs = JsonConvert.DeserializeObject<SWCS>(pageContent);
                            swcs1 = swcs;

                        }
                        else
                        {

                        }



                    }
                    else
                    {

                    }
                }
                catch (WebException wex)
                {

                }
            }
            return swcs;
        }



        public static string ViewDocument(string iuid, string fileName)
        {
            string filepath = string.Empty;
            try
            {
                if (fileName != null)
                {
                    string[] str = fileName.Split('.');
                    int length = str.Length;
                    string NewFileName = string.Empty;

                    // check the file is openable or not
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Temp")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Temp"));
                    }
                    string url = ConfigurationManager.AppSettings["SWCSDocument"].ToString() + iuid + "/" + fileName;
                    // string url = "http://investuttarakhand.co.in/themes/backend/mydoc/" + iuid + "/" + fileName;
                    return url;


                }
            }
            catch (Exception ex)
            {
                string Error_refno = string.Empty;

                ErrorModel.Function_Name = "View Document From Single Window";
                ErrorModel.Module_Name = "SWCS_Integration";
                ErrorModel.Error_Type = "Application";
                ErrorModel.Error_Desc = ex.Message;
                ErrorModel.Line_No = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                ErrorModel.Url = HttpContext.Current.Request.Url.AbsoluteUri;
                ErrorModel.IP_Address = CommonDAL.GetIPAddress();
                ErrorModel.Login_Name = "Client";
                Error_refno = ErrorModel.SaveError();
                return null;
            }
            return filepath;
        }



        // Approval process mail massege

        public static int SendEmailOfApplicationMovingStatus_FromLevelToLevel(int ServicePrimaryKey, int ServiceID)
        {
            string[] StatusData = null;
            string data = GetSWCSPostDataByApplicationRecnoAndServiceID(ServicePrimaryKey, ServiceID);
            string application_Refno = GenrateApplication_SubmitAndUpdate_RefranceKey(ServicePrimaryKey, ServiceID);



            if (data != "NA")
            {
                StatusData = data.Split('$');

                string Applicant_Name = StatusData[6].ToString();
                string ApplicationStatus = StatusData[17].ToString();
                string ServiceName = StatusData[18].ToString();
                string ProcessAtRoleName = StatusData[11].ToString();

                string ClientEmail = StatusData[19].ToString();
                string DepartMentEmail = StatusData[15].ToString();

                int StatusRecno = Convert.ToInt32(StatusData[8]);

                if (StatusRecno == 1 || StatusRecno == 2)  // approved and Rejected case Client mail, and RM mail
                {


                    StringBuilder strMessage = new StringBuilder();
                    string str = "Dear (" + Applicant_Name + ")," + "<br/><br/>";
                    strMessage.Append(str);
                    str = "Status of your application with Ref.No.  :  " + application_Refno + " for " + ServiceName + " is " + ApplicationStatus + " on " + DateTime.Now.ToString("dd-MMM-yyyy") + "<br/><br/>";
                    strMessage.Append(str);

                    str = "<br/> <br/><br/>";
                    strMessage.Append(str);

                    str = "Thanks : <br/><br/>";
                    strMessage.Append(str);

                    str = "<br/><br/>";
                    strMessage.Append(str);

                    str = StatusData[5];// Industrial Area Name;
                    strMessage.Append(str);

                    str = "SIIDCUL Smart City Alter";
                    strMessage.Append(str);

                    int intMailSent = 0;

                    intMailSent = CommonDAL.SendMail(str, strMessage.ToString(), ClientEmail);  // 15 for client email

                    StringBuilder strMessageDepartment = new StringBuilder();
                    string strD = "Dear (" + ProcessAtRoleName + ")," + "<br/><br/>";
                    strMessageDepartment.Append(strD);                   
                    if(StatusRecno == 2)
                    {
                        strD = "Application with Ref.No.  :  " + application_Refno + " for " + ServiceName + " has been " + ApplicationStatus + " on " + DateTime.Now.ToString("dd-MMM-yyyy") + "." + "<br/><br/>";
                    }
                    else
                    {
                        strD = "Application with Ref.No.  :  " + application_Refno + " for " + ServiceName + " has been " + ApplicationStatus + " on " + DateTime.Now.ToString("dd-MMM-yyyy") + ". Please issue letter to Applicant" + "<br/><br/>";
                    }
                   // strD = "Application with Ref.No.  :  " + application_Refno + " for " + ServiceName + " has been " + ApplicationStatus + " on " + DateTime.Now.ToString("dd-MMM-yyyy") + ". Please issue letter to Applicant" + "<br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = "<br/> <br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = "Thanks :<br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = "<br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = StatusData[5];// Industrial Area Name;
                    strMessageDepartment.Append(strD);

                    strD = "SIIDCUL ";
                    strMessageDepartment.Append(strD);

                    intMailSent = CommonDAL.SendMail(strD, strMessageDepartment.ToString(), DepartMentEmail);  // 1 for department Level email

                    return 1;
                }
                else if (StatusRecno == 4)  // Reverted mail, and RM mail
                {
                    int intMailSent = 0;

                    StringBuilder strMessage = new StringBuilder();
                    string str = "Dear (" + Applicant_Name + ")," + "<br/><br/>";
                    strMessage.Append(str);
                    str = "Status of your application with Ref.No.  :  " + application_Refno + " for " + ServiceName + " is " + ApplicationStatus + " on " + DateTime.Now.ToString("dd-MMM-yyyy") + "<br/><br/>";
                    strMessage.Append(str);

                    str = "<br/>";
                    strMessage.Append(str);

                    str = "Remarks :" + StatusData[7];  // remarks
                    strMessage.Append(str);
                    str = "<br/>";
                    strMessage.Append(str);
                    str = "Note : To resubmit the application please login to www.investuttarakhand.com. Click on reverted status to resubmit the application.";
                    strMessage.Append(str);

                    str = "Thanks";
                    strMessage.Append(str);

                    str = "<br/><br/>";
                    strMessage.Append(str);

                    str = StatusData[5];// Industrial Area Name;
                    strMessage.Append(str);

                    //str = "SIIDCUL Smart City Alter";
                    //strMessage.Append(str);
                    intMailSent = CommonDAL.SendMail(str, strMessage.ToString(), ClientEmail);

                    return 1;
                }
                else
                {

                    StringBuilder strMessage = new StringBuilder();
                    string str = "Dear (" + Applicant_Name + ")," + "<br/><br/>";
                    strMessage.Append(str);
                    str = "Status of your application with Ref.No.  :  " + application_Refno + " for " + ServiceName + " is " + ApplicationStatus + " TO " + ProcessAtRoleName + " on " + DateTime.Now.ToString("dd-MMM-yyyy") + "<br/><br/>";
                    strMessage.Append(str);

                    str = "<br/> <br/><br/>";
                    strMessage.Append(str);

                    str = "Thanks :<br/><br/>";
                    strMessage.Append(str);

                    str = "<br/><br/>";
                    strMessage.Append(str);

                    str = StatusData[5];// Industrial Area Name;
                    strMessage.Append(str);

                    str = "SIIDCUL ";
                    strMessage.Append(str);

                    int intMailSent = 0;

                    intMailSent = CommonDAL.SendMail(str, strMessage.ToString(), ClientEmail);  // 15 for client email

                    StringBuilder strMessageDepartment = new StringBuilder();
                    string strD = "Dear (" + ProcessAtRoleName + ")," + "<br/><br/>";
                    strMessageDepartment.Append(strD);
                    strD = "You have received application with Ref.No.  :  " + application_Refno + " for " + ServiceName + " on " + DateTime.Now.ToString("dd-MMM-yyyy") + ". Please login to process" + "<br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = "<br/> <br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = "Thanks :<br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = "<br/><br/>";
                    strMessageDepartment.Append(strD);

                    strD = StatusData[5];// Industrial Area Name;
                    strMessageDepartment.Append(strD);
                    strD = "SIIDCUL ";
                    strMessageDepartment.Append(strD);
                    intMailSent = CommonDAL.SendMail(strD, strMessageDepartment.ToString(), DepartMentEmail);  // 1 for department Level email
                    return 1;
                }
            }
            else
            {
                return 0;
            }

        }
        // end of approval

     

    }

}