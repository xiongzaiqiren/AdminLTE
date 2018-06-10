using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    //public class HomeVM
    //{
    //}

    public class RequestQueryTest : RequestQuery
    {
        public string name { get; set; }
    }

    public class DataItemModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
    }

}