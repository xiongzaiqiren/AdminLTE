

using Chloe.Annotations;
using Model.Enum;
/**
* 命名空间: Model
*
* 功 能： N/A
* 类 名： Admin
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/1 21:51:22 熊仔其人 xxh 4.0.30319.42000 初版
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
    [Table("t_admin")]
    public class Admin
    {
        [Column("AdminID", IsPrimaryKey = true)]
        [AutoIncrement]
        public int AdminID { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string JobNumber { get; set; }
        public AdminStatus Status { get; set; }
        public int CreateAdminID { get; set; }
        public DateTime CreateTime { get; set; }
        public int UpdateAdminID { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string LastLoginIP { get; set; }
        public int LoginCount { get; set; }

        public bool IsDelete { get; set; }
        public int DeleteAdminID { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
