using System.Collections;
using System.Collections.Generic;

namespace Model
{
    public class RequestQuery
    {
        /// <summary>
        /// 分页数
        /// </summary>
        private int _pageSize;
        public int pageSize
        {
            get { if (_pageSize <= 0) return 10; return _pageSize; }
            set { _pageSize = value; }
        }
        private int _pageNumber;
        /// <summary>
        /// 页码
        /// </summary>
        public int pageNumber
        {
            get { if (_pageNumber <= 0) return 1; return _pageNumber; }
            set { _pageNumber = value; }
        }
        /// <summary>
        /// 过滤条数
        /// </summary>
        public int RowStart
        {
            get
            {
                return (pageNumber - 1) * pageSize;
            }
        }
        public int Count { get; set; }
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
