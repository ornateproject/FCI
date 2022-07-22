using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using System.IO;

namespace CWC_CMS.Common
{
    public class CustomExceptionHandlerFilter : FilterAttribute, IExceptionFilter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        [Obsolete]
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                string Message = "Date :" + DateTime.Now.ToString() + ", Controller: " + controllerName + ", Action:" + actionName +
                                 "Error Message : " + exceptionMessage
                                + Environment.NewLine + "Stack Trace : " + stackTrace;

                //saving the data in a text file called Sachin.txt 
                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Logs/Sachin.txt"), Message);
                //

                //Exception Login through Nlog : Tragets (Database,Log File,Email)
                logger.Error(Message + Environment.NewLine, filterContext.Exception);
                //
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
        }
    }
}