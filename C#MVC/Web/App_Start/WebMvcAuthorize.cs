using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web
{
    /// <summary>
    /// 自定义AuthorizeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class WebMvcAuthorize : AuthorizeAttribute
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RedirectResult redirectResult { get; set; }
        /// <summary>
        /// 允许访问的角色
        /// </summary>
        public new LoginRole[] Roles { get; set; }

        private AccessAccount currentLoginAccount;

        /// <summary>
        /// 在过程请求授权时调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //if (filterContext.RequestContext.HttpContext.Session["LoginAccount"] != null) { }
            currentLoginAccount = AccessService.Account;
            if(null != currentLoginAccount && currentLoginAccount.ID > 0)
            {
                //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //string actionName = filterContext.ActionDescriptor.ActionName;
            }
            else
            {
                filterContext.Result = new RedirectResult(string.Format("{0}{1}{2}", ClassLib4Net.ConfigHelper.GetAppSetting("DomainName"), "/Account/Login?rtn=", HttpUtility.UrlEncode(filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri)));
                return;
            }

            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 重写时，提供一个入口点用于进行自定义授权检查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            //if (!httpContext.User.Identity.IsAuthenticated)
            //{
            //    return false;
            //}
            if(Roles == null)
            {
                return true;
            }
            if(Roles.Length == 0)
            {
                return true;
            }
            //if (Roles.Any(httpContext.User.IsInRole))
            //{
            //    return true;
            //}

            if(currentLoginAccount == null || currentLoginAccount.ID < 1)
            {
                return false;
            }
            else if(Roles.Any(role => role.Equals(currentLoginAccount.Role)))
            {
                return true;
            }

            return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// 在缓存模块请求授权时调用
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return HttpValidationStatus.IgnoreThisRequest;
        }

        /// <summary>
        /// 处理未能授权的 HTTP 请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult(string.Format("{0}{1}{2}", ClassLib4Net.ConfigHelper.GetAppSetting("DomainName"), "/Account/Unauthorized?rtn=", HttpUtility.UrlEncode(filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri)));
        }

    }

}