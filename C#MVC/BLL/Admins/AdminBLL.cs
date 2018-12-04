

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

        /// <summary>
        /// 根据用户名/Email/MobilePhone查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 更新登录信息
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="LastLoginIP"></param>
        /// <returns></returns>
        public int UpdateLogin(int AdminID, string LastLoginIP)
        {
            if(AdminID < 1)
            {
                return 0;
            }

            return DataRepository.adminManageDAL.UpdateLogin(AdminID, DateTime.Now, LastLoginIP);
        }

        #region 角色操作
        /// <summary>
        /// 插入角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ApiDataModel<int> InsertAdminRole(AdminRole entity)
        {
            ApiDataModel<int> result = new ApiDataModel<int>();
            if (string.IsNullOrWhiteSpace(entity.AdminRoleName))
            {
                result.Status = StatusCode.ParamError.GetHashCode();
                result.Message = "请输入角色名称";
                return result;
            }
            if(entity.AdminRoleStatus<0)
            {
                result.Status = StatusCode.ParamError.GetHashCode();
                result.Message = "请选择角色状态";
                return result;
            }
            var model = DataRepository.adminManageDAL.InsertAdminRole(entity);
            if(model!=null)
            {
                result.Status = StatusCode.Success.GetHashCode();
                result.Message = "操作成功";
               
            }
            else
            {
                result.Status = StatusCode.Fail.GetHashCode();
                result.Message = "操作失败";
            }
            return result;
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ApiDataModel<int> UpdateAdminRole(AdminRole entity)
        {
            ApiDataModel<int> result = new ApiDataModel<int>();
            if(entity.AdminRoleID<=0)
            {
                result.Status = StatusCode.ParamError.GetHashCode();
                result.Message = "角色ID参数无效";
                return result;
            }
            if (string.IsNullOrWhiteSpace(entity.AdminRoleName))
            {
                result.Status = StatusCode.ParamError.GetHashCode();
                result.Message = "请输入角色名称";
                return result;
            }
            if (entity.AdminRoleStatus < 0)
            {
                result.Status = StatusCode.ParamError.GetHashCode();
                result.Message = "请选择角色状态";
                return result;
            }
            var model = DataRepository.adminManageDAL.UpdateAdminRole(entity);
            if (model>0)
            {
                result.Status = StatusCode.Success.GetHashCode();
                result.Message = "操作成功";

            }
            else
            {
                result.Status = StatusCode.Fail.GetHashCode();
                result.Message = "操作失败";
            }
            return result;
        }
        /// <summary>
        /// 根据id查询角色实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ApiDataModel<AdminRole> GetAdminRoleByID(int Id)
        {
            ApiDataModel<AdminRole> result = new ApiDataModel<AdminRole>();
            if(Id<=0)
            {
                result.Status = StatusCode.InvalidParameter.GetHashCode();
                result.Message = "参数无效";
                return result;
            }
            var model = DataRepository.adminManageDAL.GetAdminRoleByID(Id);
            if(model==null)
            {
                result.Status = StatusCode.NotExisted.GetHashCode();
                result.Message = "数据不存在";
            }
            else
            {
                result.Status = StatusCode.Success.GetHashCode();
                result.Message = "操作成功";
                result.Data = model;
            }
            return result;

        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiDataModel<int> DeleteAdminRole(int id)
        {
            ApiDataModel<int> result = new ApiDataModel<int>();
            if (id <= 0)
            {
                result.Status = StatusCode.InvalidParameter.GetHashCode();
                result.Message = "参数无效";
                return result;
            }
            var r= DataRepository.adminManageDAL.DeleteAdminRole(id);
            if(r>0)
            {
                result.Status = StatusCode.Success.GetHashCode();
                result.Message = "操作成功";
                result.Data = r;
            }
            else
            {
                result.Status = StatusCode.Fail.GetHashCode();
                result.Message = "操作失败";
                result.Data = r;
            }
            return result;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseResultModel<AdminRole> GetAdminRoleList(AdminRoleQuery model)
        {
            ResponseResultModel<AdminRole> result = new ResponseResultModel<AdminRole>();
           
            
            var list= DataRepository.adminManageDAL.GetAdminRoleList(model);
            result.data = new BootstrapTableData<AdminRole>(model.Count,list);
            result.status = ResponseStatus.Success;
            return result;
        }
        #endregion

    }
}
