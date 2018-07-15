using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public abstract class RequestQuery
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
        public BootstrapTableData(long total, IList rows) { this.total = total; this.rows = rows; }
    }
    public class BootstrapTableData<T> : BootstrapTableData
    {
        public new IList<T> rows { get; set; }

        public BootstrapTableData() { total = 0; rows = new List<T>(); }
        public BootstrapTableData(long total, IList<T> rows) { this.total = total; this.rows = rows; }
    }

    public enum ResponseStatus : int
    {
        Success = 200,
        ServerError = 500,
        ParameterError = 503,
        RequestParameterError = 403,
        Jump = 302
    }

    public abstract class ResponseResult
    {
        public ResponseStatus status { get; set; }
        public string message { get; set; }

        public ResponseResult() { status = ResponseStatus.Success; message = "ok"; }
        public ResponseResult(ResponseStatus status, string message) { this.status = status; this.message = message; }
    }

    public class ResponseResultModel : ResponseResult
    {
        public BootstrapTableData data { get; set; }

        public ResponseResultModel() : base() { data = new BootstrapTableData(); }
        public ResponseResultModel(ResponseStatus status, string message) : base(status, message) { data = new BootstrapTableData(); }
    }

    public class ResponseResultModel<T> : ResponseResult
    {
        public BootstrapTableData<T> data { get; set; }

        public ResponseResultModel() : base() { data = new BootstrapTableData<T>(); }
        public ResponseResultModel(ResponseStatus status, string message) : base(status, message) { data = new BootstrapTableData<T>(); }
    }

}