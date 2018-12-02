

using Chloe.Annotations;
/**
* 命名空间: Model
*
* 功 能： N/A
* 类 名： AdminRoleRelation
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/1 22:09:16 熊仔其人 xxh 4.0.30319.42000 初版
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
using System.Threading.Tasks;

namespace Model
{
    [Table("t_adminrolerelation")]
    public class AdminRoleRelation : TableAttribute
    {
        [Column("AdminRoleRelationID", IsPrimaryKey = true)]
        [AutoIncrement]
        public int AdminRoleRelationID { get; set; }
        public int AdminID { get; set; }
        public int AdminRoleID { get; set; }
    }
}
