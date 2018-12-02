using Chloe;
using Chloe.MySql;
using Model;
/**
* 命名空间: DAL.Admins
*
* 功 能： N/A
* 类 名： AdminDAL
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/1 23:00:50 熊仔其人 xxh 4.0.30319.42000 初版
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

namespace DAL.Admins
{
    public sealed class AdminDAL : BaseService
    {
        #region Instance 

        private static readonly AdminDAL instance = new AdminDAL();
        private AdminDAL() { }
        internal static AdminDAL Instance
        {
            get { return instance; }
        }
        #endregion

        #region Base Methods
        public Admin Insert(Admin entity)
        {
            Admin result = null;
            using(MySqlContext context = new MySqlContext(Db))
            {
                try
                {
                    context.Session.BeginTransaction();
                    result = context.Insert(entity);
                    context.Session.CommitTransaction();
                    return result;
                }
                catch(Exception ex)
                {
                    if(context.Session.IsInTransaction)
                        context.Session.RollbackTransaction();
                    throw ex;
                }
            }
        }
        public Admin Select(int AdminID)
        {
            using(MySqlContext context = new MySqlContext(Db))
            {
                IQuery<Admin> q = context.Query<Admin>();
                return q.Where(a => a.AdminID == AdminID).FirstOrDefault();
            }
        }
        public int Delete(int AdminID)
        {
            using(MySqlContext context = new MySqlContext(Db))
            {
                return context.Delete<Admin>(a => a.AdminID == AdminID);
            }
        }
        public int Delete(Admin entity)
        {
            using(MySqlContext context = new MySqlContext(Db))
            {
                return context.Delete(entity);
            }
        }

        /// <summary>
        /// 更新所有映射的字段（慎用）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(Admin entity)
        {
            using(MySqlContext context = new MySqlContext(Db))
            {
                /* 更新所有映射的字段 */
                return context.Update(entity);
            }
        }
        public int Update(int AdminID, string UserName)
        {
            using(MySqlContext context = new MySqlContext(Db))
            {
                IQuery<Admin> q = context.Query<Admin>();
                var entity = q.Where(a => a.AdminID == AdminID).FirstOrDefault();
                if(null == entity)
                    return 0;

                /* 在修改实体属性前让上下文跟踪实体 */
                context.TrackEntity(entity);

                /* 然后再修改实体属性 */
                entity.UserName = UserName;

                /* 然后调用 Update 方法，这时只会更新被修改过的属性 */
                return context.Update(entity);
            }
        }
        public int Update(int AdminID, DateTime LastLoginTime, string LastLoginIP)
        {
            int result;
            using(MySqlContext context = new MySqlContext(Db))
            {
                /* tips：必须在 lambda 里写 new User() */
                result = context.Update<Admin>(a => a.AdminID == AdminID, a => new Admin()
                {
                    LastLoginTime = LastLoginTime,
                    LastLoginIP = LastLoginIP,
                    LoginCount = a.LoginCount + 1
                });
            }
            return result;
        }

        #endregion

        public Admin Select(string name)
        {
            using(MySqlContext context = new MySqlContext(Db))
            {
                IQuery<Admin> q = context.Query<Admin>();
                return q.Where(a => a.UserName == name || a.Email == name || a.MobilePhone == name).FirstOrDefault();
            }
        }

    }
}
