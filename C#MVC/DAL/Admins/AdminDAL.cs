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

        /// <summary>
        /// 根据用户名/Email/MobilePhone查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Admin Select(string name)
        {
            using(MySqlContext context = new MySqlContext(Db))
            {
                IQuery<Admin> q = context.Query<Admin>();
                return q.Where(a => a.UserName == name || a.Email == name || a.MobilePhone == name).FirstOrDefault();
            }
        }
        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="LastLoginTime"></param>
        /// <param name="LastLoginIP"></param>
        /// <returns></returns>
        public int UpdateLogin(int AdminID, DateTime LastLoginTime, string LastLoginIP)
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

        #region 角色操作
        /// <summary>
        /// 插入角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public AdminRole InsertAdminRole(AdminRole entity)
        {
            AdminRole result = null;
            using (MySqlContext context = new MySqlContext(Db))
            {
                try
                {
                    context.Session.BeginTransaction();
                    result = context.Insert(entity);
                    context.Session.CommitTransaction();
                    return result;
                }
                catch (Exception ex)
                {
                    if (context.Session.IsInTransaction)
                        context.Session.RollbackTransaction();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateAdminRole(AdminRole entity)
        {
            using (MySqlContext context = new MySqlContext(Db))
            {
                IQuery<AdminRole> q = context.Query<AdminRole>();
                var modelRole = q.Where(a => a.AdminRoleID == entity.AdminRoleID).FirstOrDefault();
                if (null == entity)
                    return 0;

                /* 在修改实体属性前让上下文跟踪实体 */
                context.TrackEntity(modelRole);

                /* 然后再修改实体属性 */
                modelRole.AdminRoleName = entity.AdminRoleName;
                modelRole.UpdateTime = DateTime.Now;
                modelRole.AdminRoleStatus = entity.AdminRoleStatus;

                /* 然后调用 Update 方法，这时只会更新被修改过的属性 */
                return context.Update(modelRole);
            }
        }
        /// <summary>
        /// 根据id查询角色实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public AdminRole GetAdminRoleByID(int Id)
        {
            using (MySqlContext context = new MySqlContext(Db))
            {
                IQuery<AdminRole> q = context.Query<AdminRole>();
                var model = q.Where(a => a.AdminRoleID == Id).FirstOrDefault();
                return model;
            }

        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAdminRole(int id)
        {
            using (MySqlContext context = new MySqlContext(Db))
            {
                IQuery<AdminRole> q = context.Query<AdminRole>();
                int ret = context.Delete<AdminRole>(a => a.AdminRoleID == id);
                return ret;
            }
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<AdminRole> GetAdminRoleList(AdminRoleQuery model)
        {
            List<AdminRole> r = new List<AdminRole>();

            using (MySqlContext context = new MySqlContext(Db))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@"select AdminRoleID,AdminRoleName,AdminRoleStatus,CreateTime  from t_adminrole where 1=1 ");
                StringBuilder sqlCount = new StringBuilder();
                sqlCount.Append(@"select count(1) from t_adminrole  where 1=1");
                List<DbParam> _listDbParam = new List<DbParam>();
                if (!string.IsNullOrWhiteSpace(model.StartTime))
                {
                    sql.Append(" and w.CreateTime>= ?starttime ");
                    sqlCount.Append(" and w.CreateTime>= ?starttime ");
                    _listDbParam.Add(new DbParam("?starttime", "" + model.StartTime + ""));
                }
                if (!string.IsNullOrWhiteSpace(model.EndTime))
                {
                    sql.Append(" and w.CreateTime<= ?endtime ");
                    sqlCount.Append(" and w.CreateTime<= ?endtime ");
                    _listDbParam.Add(new DbParam("?endtime", "" + model.EndTime + ""));
                }

                sql.Append(" order by  AdminRoleID desc  limit " + (model.pageNumber - 1) * model.pageSize + ", " + model.pageSize);
                r = context.SqlQuery<AdminRole>(sql.ToString(), _listDbParam.ToArray()).ToList();
                model.Count = context.SqlQuery<int>(sqlCount.ToString(), _listDbParam.ToArray()).FirstOrDefault();
                return r;

            }

        }
        #endregion

    }
}
