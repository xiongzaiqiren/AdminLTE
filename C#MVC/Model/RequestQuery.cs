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
}
