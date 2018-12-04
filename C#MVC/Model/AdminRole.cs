﻿

using Chloe.Annotations;
using Model.Enum;
/**
* 命名空间: Model
*
* 功 能： N/A
* 类 名： AdminRole
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/1 22:05:33 熊仔其人 xxh 4.0.30319.42000 初版
*
* Copyright (c) 2018 熊仔其人 Corporation. All rights reserved.
*┌─────────────────────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．   │
*│　版权所有：熊仔其人 　　　　　　　　　　　　　　　　　　　　　　　 │
*└─────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Table("t_adminrole")]
    public class AdminRole : TableAttribute
    {
        [Column("AdminRoleID", IsPrimaryKey = true)]
        [AutoIncrement]
        public int AdminRoleID { get; set; }
        public string AdminRoleName { get; set; }
        public AdminRoleStatus AdminRoleStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class AdminRoleQuery:RequestQuery
    {
        public string RoleName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
    //public class RequestQuery
    //{
    //    /// <summary>
    //    /// 分页数
    //    /// </summary>
    //    private int _pageSize;
    //    public int pageSize
    //    {
    //        get { if (_pageSize <= 0) return 10; return _pageSize; }
    //        set { _pageSize = value; }
    //    }
    //    private int _pageNumber;
    //    /// <summary>
    //    /// 页码
    //    /// </summary>
    //    public int pageNumber
    //    {
    //        get { if (_pageNumber <= 0) return 1; return _pageNumber; }
    //        set { _pageNumber = value; }
    //    }
    //    /// <summary>
    //    /// 过滤条数
    //    /// </summary>
    //    public int RowStart
    //    {
    //        get
    //        {
    //            return (pageNumber - 1) * pageSize;
    //        }
    //    }

    //    public string searchText { get; set; }
    //    public string sortName { get; set; }
    //    public string sortOrder { get; set; }
    //}
}
