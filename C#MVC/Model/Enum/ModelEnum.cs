/**
* 命名空间: Model.Enum
*
* 功 能： N/A
* 类 名： ModelEnum
*
* Ver 变更日期 负责人 当前系统用户名 CLR版本 变更内容
* ───────────────────────────────────
* V0.01 2018/12/1 19:15:48 熊仔其人 xxh 4.0.30319.42000 初版
*
* Copyright (c) 2018 熊仔其人 Corporation. All rights reserved.
*┌─────────────────────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．   │
*│　版权所有：熊仔其人 　　　　　　　　　　　　　　　　　　　　　　　 │
*└─────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Model.Enum
{
    class ModelEnum
    {

    }

    #region Base

    #region 语言种类

    /// <summary>
    /// 语言种类
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// 英语
        /// </summary>
        en_US = 0,
        /// <summary>
        /// 中文
        /// </summary>
        zh_CN = 1
    }

    #endregion

    #region 多语言实现方法

    /// <summary>
    /// 定义枚举多语言属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MultiLanguageAttribute : Attribute
    {
        public MultiLanguageAttribute(object key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public object key { get; private set; }
        public string value { get; private set; }
    }

    #region 扩展enum属性

    public static class MultiLanguageExtensions
    {
        /// <summary>
        /// 当前语言值
        /// </summary>
        /// <param name="enumObject"></param>
        /// <returns></returns>
        public static string Language(this System.Enum enumObject)
        {
            return Language(enumObject, CurrentCultureName.Replace('-', '_'));
        }

        /// <summary>
        /// 枚举指定语言显示
        /// </summary>
        /// <param name="enumObject"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static string Language(this System.Enum enumObject, string language)
        {
            string cultureName = language.Replace('-', '_');

            Type t = enumObject.GetType();
            string s = enumObject.ToString();
            MultiLanguageAttribute[] os = null;
            try
            {
                os = (MultiLanguageAttribute[])t.GetField(s).GetCustomAttributes(typeof(MultiLanguageAttribute), false);
            }
            catch { }
            if(os != null)
            {
                foreach(MultiLanguageAttribute o in os)
                {
                    if(o.key.ToString() == cultureName)
                    {
                        s = o.value;
                    }
                }
            }

            return s;
        }

        /// <summary>
        /// 枚举转换为字典，并翻译为当前语言
        /// </summary>
        /// <param name="em</param>
        /// <returns></returns>
        public static IDictionary<int, string> ToIEnumerable(this System.Enum enumObject)
        {
            string cultureName = CurrentCultureName.Replace('-', '_'); //当前系统语言
            IDictionary<int, string> dic = new Dictionary<int, string>();

            Array array = System.Enum.GetValues(enumObject.GetType());//当前枚举的所有项
            Type t = enumObject.GetType();//当前枚举类型
            foreach(int val in array)
            {
                string emName = System.Enum.GetName(enumObject.GetType(), val);
                string emKey = emName;
                MultiLanguageAttribute[] os = null;
                try
                {
                    os = (MultiLanguageAttribute[])t.GetField(emName).GetCustomAttributes(typeof(MultiLanguageAttribute), false);
                }
                catch { }
                if(os != null)
                {
                    foreach(MultiLanguageAttribute la in os)
                    {
                        if(la.key.ToString() == cultureName)
                        {
                            emName = la.value;
                            break;
                        }
                    }
                }
                dic.Add(val, emName);
            }

            return dic;
        }


        ///// <summary>
        ///// 枚举转换为selectlist，并翻译为当前语言
        ///// </summary>
        ///// <param name="enumObject"></param>
        ///// <returns></returns>
        //public static SelectList ToSelectList(this System.Enum enumObject)
        //{
        //    IDictionary<int, string> dic = enumObject.ToIEnumerable();
        //    return new SelectList(dic, "Key", "Value");
        //}
        /// <summary>
        /// 当前用户地区编码
        /// </summary>
        public static string CurrentCultureName
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
        }

    }

    #endregion

    #endregion


    /// <summary>
    /// 状态码
    /// </summary>
    [Serializable]
    [DataContract]
    public enum StatusCode
    {
        #region 2开头
        /// <summary>
        /// 成功
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "成功")]
        Success = 200,
        /// <summary>
        /// 失败
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "失败")]
        Fail = 201,
        /// <summary>
        /// 存在
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "存在")]
        Existed = 202,
        /// <summary>
        /// 不存在
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "不存在")]
        NotExisted = 203,
        /// <summary>
        /// 已经完成
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "已经完成")]
        Completed = 204,
        /// <summary>
        /// 未完成
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "未完成")]
        InComplete = 205,
        #endregion

        #region 4开头
        /// <summary>
        /// 参数错误
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "参数错误")]
        ParamError = 401,
        /// <summary>
        /// 参数无效
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "参数无效")]
        InvalidParameter = 402,
        /// <summary>
        /// 不能正确匹配
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "不能正确匹配")]
        NotMatch = 403,
        /// <summary>
        /// 未授权的请求
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "未授权的请求")]
        Unauthorized = 404,

        /// <summary>
        /// 当前处在锁定时间
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "当前处在锁定时间")]
        TimeLock = 405,

        /// <summary>
        /// 扩展表中没有数据保存
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "扩展表中没有数据保存")]
        NotExitOfExtensionalInfo = 406,
        /// <summary>
        /// 用户名或者密码错误
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "用户名或者密码错误")]
        NameOrPasswordError = 407,
        /// <summary>
        /// 用户废弃
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "用户废弃")]
        UserDiscard = 408,
        /// <summary>
        /// 用户禁用
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "用户禁用")]
        UserDisable = 409,
        /// <summary>
        /// 手机号格式有误
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "手机号格式有误")]
        UsertelError = 410,
        /// <summary>
        /// 验证码已过期
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "验证码已过期")]
        Expire = 411,
        /// <summary>
        /// 值相同
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "值相同")]
        ValueSame = 412,
        /// <summary>
        /// 验证码错误
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "验证码错误")]
        ValidateCode = 413,
        /// <summary>
        /// 余额不足
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "余额不足")]
        BalanceLow = 414,
        /// <summary>
        /// 登录异常
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "登录异常")]
        LoginEx = 415,
        /// <summary>
        /// 用户名不能为空
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "用户名不能为空")]
        UserNameEmpty = 416,
        /// <summary>
        /// 支付密码错误
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "支付密码错误")]
        zf_passError = 417,
        #endregion

        #region 5开头
        /// <summary>
        /// 错误
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "错误")]
        Error = 500,
        /// <summary>
        /// 服务器错误
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "服务器错误")]
        ServerError = 501,
        /// <summary>
        /// Token过期
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "Token过期")]
        TokenExpire = 502,
        #endregion

        /// <summary>
        /// 禁用
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "禁用")]
        Disable = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "启用")]
        Enable = 1
    }
    #endregion

    #region Admin
    /// <summary>
    /// 登录用户类型
    /// </summary>
    [Serializable]
    [DataContract]
    public enum LoginRole
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "超级管理员")]
        superAdmin = 0,
        /// <summary>
        /// 管理员
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "管理员")]
        admin = 1,
        /// <summary>
        /// 商户
        /// </summary>
        //[EnumMember]
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "商户")]
        businesses = 2,
        /// <summary>
        /// 普通用户
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "普通用户")]
        user = 3,
    }
    /// <summary>
    /// 管理员状态
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AdminStatus
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "禁用")]
        Disable = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "启用")]
        Enable = 1
    }
    /// <summary>
    /// 管理员角色状态
    /// </summary>
    [Serializable]
    [DataContract]
    public enum AdminRoleStatus
    {
        /// <summary>
        /// 禁用
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "禁用")]
        Disable = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [EnumMember]
        [MultiLanguage(Language.zh_CN, "启用")]
        Enable = 1
    }
    #endregion

}
