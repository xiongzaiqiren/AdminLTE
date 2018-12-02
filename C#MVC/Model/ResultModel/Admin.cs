/**
* 命名空间: Model.ResultModel
*
* 功 能： N/A
* 类 名： Admin
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/2 18:44:02 熊仔其人 xxh 4.0.30319.42000 初版
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

namespace Model.ResultModel
{
    class Admin
    {
    }

    public class AdminLogin
    {
        public int AdminID { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string JobNumber { get; set; }
    }

}
