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
        public ActionResult list3()
        {
            return View();
        }

        static readonly Random random = new Random();

        [ActionName("getlist2")]
        public ActionResult GetList2(Models.RequestQueryTest Params)
        {
            var btd = new Models.BootstrapTableData();
            btd.total = 100;

            var list = new List<Models.DataItemModel>();
            int num = 0;
            for(var i = (Params.pageNumber - 1) * Params.pageSize; i < btd.total && num < Params.pageSize; i++)
            {
                list.Add(new Models.DataItemModel()
                {
                    id = i,
                    name = string.Format("{0}-{1}", i, random.Next(100, 10000)),
                    price = random.Next(10, 50) + (decimal)random.NextDouble(),
                });
                num++;
            }
            btd.rows = list;

            return Json(btd, JsonRequestBehavior.AllowGet);
        }

        [ActionName("getlist3")]
        public ActionResult GetList3(Models.RequestQueryTest Params)
        {
            var result = new Models.ResponseResultModel();
            result.status = Models.ResponseStatus.Success;
            result.message = "ok";
            result.data.total = 100;

            var list = new List<Models.DataItemModel>();
            int num = 0;
            for(var i = (Params.pageNumber - 1) * Params.pageSize; i < result.data.total && num < Params.pageSize; i++)
            {
                list.Add(new Models.DataItemModel()
                {
                    id = i,
                    name = string.Format("{0}-{1}", i, random.Next(100, 10000)),
                    price = random.Next(10, 50) + (decimal)random.NextDouble(),
                });
                num++;
            }
            result.data.rows = list;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult zTree()
        {
            return View();
        }

    }
}