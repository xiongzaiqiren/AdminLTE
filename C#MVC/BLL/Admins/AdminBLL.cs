

using ClassLib4Net.Api;
using Common;
using DAL;
using Model;
using Model.Enum;
/**
* 命名空间: BLL.Admins
*
* 功 能： N/A
* 类 名： AdminBLL
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/2 13:01:46 熊仔其人 xxh 4.0.30319.42000 初版
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

namespace BLL.Admins
{
    public sealed class AdminBLL
    {
        #region Instance
        private static readonly AdminBLL instance = new AdminBLL();
        private AdminBLL() { }
        internal static AdminBLL Instance { get { return instance; } }
        #endregion

        #region Base Methods
        public Admin Insert(Admin entity)
        {
            if(null == entity || string.IsNullOrWhiteSpace(entity.UserName))
                return null;
            entity.CreateTime = DateTime.Now;
            entity.LoginCount = 0;
            return DataRepository.adminManageDAL.Insert(entity);
        }
        public Admin Select(int AdminID)
        {
            if(AdminID < 1)
                return null;
            return DataRepository.adminManageDAL.Select(AdminID);
        }
        public int Delete(int AdminID)
        {
            if(AdminID < 1)
                return 0;
            return DataRepository.adminManageDAL.Delete(AdminID);
        }
        public int Delete(Admin entity)
        {
            if(null == entity || entity.AdminID < 1)
                return 0;
            return DataRepository.adminManageDAL.Delete(entity);
        }

        /// <summary>
        /// 更新所有映射的字段（慎用）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(Admin entity)
        {
            if(null == entity || entity.AdminID < 1)
                return 0;
            return DataRepository.adminManageDAL.Update(entity);
        }
        public int Update(int AdminID, string UserName)
        {
            if(AdminID < 1 || string.IsNullOrWhiteSpace(UserName))
                return 0;
            return DataRepository.adminManageDAL.Update(AdminID, UserName);
        }
        public int Update(int AdminID, DateTime LastLoginTime, string LastLoginIP)
        {
            if(AdminID < 1)
                return 0;
            return DataRepository.adminManageDAL.Update(AdminID, LastLoginTime, LastLoginIP);
        }

        #endregion

        public ApiDataModel<Model.ResultModel.AdminLogin> Login(string name, string pwd)
        {
            var result = new ApiDataModel<Model.ResultModel.AdminLogin>(new Model.ResultModel.AdminLogin());

            if(string.IsNullOrWhiteSpace(name))
            {
                result.Status = StatusCode.UserNameEmpty.GetHashCode();
                result.Message = "请输入登录名";
                return result;
            }

            var entity = DataRepository.adminManageDAL.Select(name);
            if(null == entity || entity.AdminID < 1)
            {
                result.Status = StatusCode.NameOrPasswordError.GetHashCode();
                result.Message = "用户名或密码错误";
                return result;
            }
            else if(entity.IsDelete)
            {
                result.Status = StatusCode.UserDiscard.GetHashCode();
                result.Message = "用户被废弃";
                return result;
            }
            else if(CryptoHelper.GeneratePassword(pwd) != entity.Pwd)
            {
                result.Status = StatusCode.NameOrPasswordError.GetHashCode();
                result.Message = "密码错误";
                return result;
            }

            if(AdminStatus.Enable == entity.Status)
            {
                result.Status = StatusCode.Success.GetHashCode();
                result.Message = "登录成功";

                result.Data.AdminID = entity.AdminID;
                result.Data.UserName = entity.UserName;
                result.Data.NickName = entity.NickName;
                result.Data.JobNumber = entity.JobNumber;
                return result;
            }
            else
            {
                result.Status = StatusCode.UserDisable.GetHashCode();
                result.Message = "用户被禁用";
                return result;
            }


        }

    }
}
