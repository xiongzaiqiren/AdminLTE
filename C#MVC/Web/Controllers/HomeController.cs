using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult list()
        {
            return View();
        }
        public ActionResult list2()
        {
            return View();
        }

        static readonly Random random = new Random();

        [ActionName("getlist2")]
        public ActionResult GetList2(Models.queryParams Params)
        {
            var btd = new Models.BootstrapTableData();
            btd.total = 100;

            var list = new List<Models.DataItemModel>();
            for(var i = 0; i < btd.total && i < Params.pageSize; i++)
            {
                list.Add(new Models.DataItemModel()
                {
                    id = i,
                    name = string.Format("{0}-{1}", i, random.Next(100, 10000)),
                    price = random.Next(10, 50) + (decimal)random.NextDouble(),
                });
            }
            btd.rows = list;

            return Json(btd, JsonRequestBehavior.AllowGet);
        }


    }
}