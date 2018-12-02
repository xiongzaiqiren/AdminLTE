

using Chloe.Infrastructure;
using MySql.Data.MySqlClient;
/**
* 命名空间: DAL
*
* 功 能： N/A
* 类 名： BaseService
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/1 22:45:50 熊仔其人 xxh 4.0.30319.42000 初版
*
* Copyright (c) 2018 熊仔其人 Corporation. All rights reserved.
*┌─────────────────────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．   │
*│　版权所有：熊仔其人 　　　　　　　　　　　　　　　　　　　　　　　 │
*└─────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAL
{
    public abstract class BaseService
    {
        //public MySqlContext Db;
        public MySqlConnectionFactory Db;
        static string connString = System.Configuration.ConfigurationManager.ConnectionStrings["mysqlConnectionStr"].ConnectionString;

        protected BaseService()
        {
            Db = Db ?? new MySqlConnectionFactory(connString);
        }
    }

    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        string _connString = null;
        public MySqlConnectionFactory(string connString)
        {
            this._connString = connString;
        }
        public IDbConnection CreateConnection()
        {
            IDbConnection conn = new MySqlConnection(this._connString);
            /*如果有必要需要包装一下驱动的 MySqlConnection*/
            //conn = new Chloe.MySql.ChloeMySqlConnection(conn); 
            return conn;

        }
    }

}
