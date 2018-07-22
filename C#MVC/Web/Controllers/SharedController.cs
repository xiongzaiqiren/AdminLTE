using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public PartialViewResult MainHeader(int uid)
        {
            var model = new { uid = uid };
            return PartialView(model);
        }

        public PartialViewResult LeftSidebar(int uid)
        {
            var model = new { uid = uid };
            return PartialView(model);
        }

    }
}