using ClassLib4Net;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Web.Models
{
    #region Model
    /// <summary>
    /// 访问账户模型
    /// 熊学浩
    /// </summary>
    [Serializable]
    [DataContract]
    public class AccessAccount
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public string JobNumber { get; set; }

        [DataMember]
        public bool loginRememberMe { get; set; }

        [DataMember]
        public LoginRole Role { get; set; }

        public AccessAccount() { }
        public AccessAccount(long ID, string Name, LoginRole Role) : base()
        {
            this.ID = ID;
            this.Name = Name;
            this.Role = Role;
        }
    }
    #endregion

    /// <summary>
    /// 访问服务
    /// 熊学浩
    /// </summary>
    public class AccessService
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string key = "AccessAccount";
        private static string cookieDomain = ConfigHelper.GetAppSetting("CookieDomain");

        /// <summary>
        /// 全部角色
        /// </summary>
        public static readonly LoginRole[] Roles = new[] { LoginRole.superAdmin, LoginRole.admin };

        public static AccessAccount Account
        {
            get
            {
                try
                {
                    return GetCurrentLoginAccount();
                }
                catch(Exception Ex)
                {
                    log.Error(Ex);
                    return null;
                }
            }
            set
            {
                try
                {
                    SetCurrentLoginAccount(value);
                }
                catch(Exception Ex)
                {
                    log.Error(Ex);
                }
            }
        }

        private static void SetCurrentLoginAccount(AccessAccount Account)
        {
            try
            {
                //log.Debug("SetCurrentLoginAccount > Account=Start" + Account.ID + "\n" + Account.Name + "\n" + Account.role);
                //log.Debug("SetCurrentLoginAccount > Account=" + JsonHelper.Serialize(Account));
                if(null != Account && Account.ID > 0)
                {
                    if(CookieHelper.CookieIsEnable())
                    {
                        CookieHelper.Set(Account, key, Account.loginRememberMe ? 7 * 24 * 60 : 20, Domain: cookieDomain);
                    }
                    //log.Debug("SetCurrentLoginAccount > Cookie完毕");
                    if(null != HttpContext.Current && null != HttpContext.Current.Session && !HttpContext.Current.Session.IsReadOnly)
                    {
                        if(HttpContext.Current.Session[key] != null)
                            HttpContext.Current.Session.Remove(key);
                        HttpContext.Current.Session.Add(key, Account);
                    }
                    //log.Debug("SetCurrentLoginAccount > Session完毕");
                }
            }
            catch(Exception ex)
            {
                log.Error("SetCurrentLoginAccount > Exception=" + ex.Message);
            }
        }
        private static AccessAccount GetCurrentLoginAccount()
        {
            if(null != HttpContext.Current.Session && null != HttpContext.Current.Session[key])
            {
                AccessAccount Account = (AccessAccount)HttpContext.Current.Session[key];
                return Account;
            }
            else if(null != CookieHelper.Get<AccessAccount>(key))
            {
                return CookieHelper.Get<AccessAccount>(key);
            }
            else
                return null;
        }

        public static void RemoveAccount()
        {
            if(HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
                HttpContext.Current.Session.Abandon();
            }

            if(CookieHelper.Get<AccessAccount>(key) != null)
            {
                CookieHelper.Remove(key);
            }
        }

    }
}