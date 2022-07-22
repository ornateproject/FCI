using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWC_CMS.Controllers
{
    public class EmployeeOnlyCircularsController : Controller
    {
        // GET: EmployeeOnlyCirculars
        public ActionResult Index()
        {
            return View();
        }
    }
}