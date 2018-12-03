using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class WebMvcActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 在执行操作结果之前由 ASP.NET MVC 框架调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            this.OnResultExecuting(filterContext);
        }

        /// <summary>
        /// 在执行操作结果后由 ASP.NET MVC 框架调用
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            this.OnResultExecuted(filterContext);
        }

    }
}