using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class queryParams
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string searchText { get; set; }
        public string sortName { get; set; }
        public string sortOrder { get; set; }
    }

    public class BootstrapTableData
    {
        public long total { get; set; }
        public IList rows { get; set; }

        public BootstrapTableData() { total = 0; rows = default(List<object>); }
        public BootstrapTableData(long total, IList rows) { total = this.total; rows = this.rows; }
    }

    public class DataItemModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
    }

}