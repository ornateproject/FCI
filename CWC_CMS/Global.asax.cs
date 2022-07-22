using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CWC_CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
        }
        protected void Application_BeginRequest()
        {
            //Context.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            //Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Context.Response.Cache.SetNoStore();


        }
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.AddHeader("Server", "DENY");
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string _encryptedString = string.Empty;
            string _sessionIPAddress = string.Empty;
            //string _sessionBrowserInfo = string.Empty;
            if (HttpContext.Current.Session != null)
            {
                _encryptedString = Convert.ToString(Session["encryptedSession"]);
                byte[] _encodedAsBytes = System.Convert.FromBase64String(_encryptedString);
                string _decryptedString = System.Text.ASCIIEncoding.ASCII.GetString(_encodedAsBytes);

                char[] _separator = new char[] { '^' };
                if (_decryptedString != string.Empty && _decryptedString != "" && _decryptedString != null)
                {
                    string[] _splitStrings = _decryptedString.Split(_separator);
                    if (_splitStrings.Count() > 0)
                    {
                        //string UserId = _splitStrings[0];
                        //string Ticks = _splitStrings[1];
                        //string dummyGuid = _splitStrings[3];

                        if (_splitStrings[2].Count() > 0)
                        {
                            string[] _userBrowserInfo = _splitStrings[2].Split('~');
                            if (_userBrowserInfo.Count() > 0)
                            {
                                //_sessionBrowserInfo = _userBrowserInfo[0];
                                _sessionIPAddress = _userBrowserInfo[1];
                            }
                        }
                    }
                }

                string _currentUseripAddress;
                if (string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                {
                    _currentUseripAddress = Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    _currentUseripAddress =
                    Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                }

                System.Net.IPAddress result;
                if (!System.Net.IPAddress.TryParse(_currentUseripAddress, out result))
                {
                    result = System.Net.IPAddress.None;
                }

                if (_sessionIPAddress != "" && _sessionIPAddress != string.Empty)
                {
                    //Same way we can validate browser info also...
                    //string _currentBrowserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent;
                    if (_sessionIPAddress != _currentUseripAddress)
                    {
                        Session.RemoveAll();
                        Session.Clear();
                        Session.Abandon();
                        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddSeconds(-30);
                        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                       // return (RedirectToAction("Index", "Login", new { UserValid = "invalid" }));
                    }
                    else
                    {
                        //Valid User
                    }
                }
            }
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            try
            {
                //if (Request.IsSecureConnection == true)
                //{
                //    Response.Cookies["ASP.NET_SessionId"].Secure = true;
                //}
            }

            catch (Exception)
            {
            }
        }

    }
}
