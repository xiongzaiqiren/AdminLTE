using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    /// <summary>
    /// 自定义异常处理
    /// </summary>
    public class WebMvcHandleError : HandleErrorAttribute
    {
        /// <summary>
		/// 日志记录器
		/// </summary>
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 在发生异常时调用
        /// </summary>
        /// <param name="Context"></param>
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            log.Error("<br/><strong>客户机IP</strong>：" + HttpContext.Current.Request.UserHostAddress + "<br /><strong>错误地址</strong>：【" + HttpContext.Current.Request.HttpMethod + "】" + "【url】=" + HttpContext.Current.Request.Url + "【UrlReferrer】=" + HttpContext.Current.Request.UrlReferrer + "【ExceptionHandled】=" + context.ExceptionHandled, context.Exception);

            if(context.ExceptionHandled)
            {
                HttpException httpException = context.Exception as HttpException;
                if(httpException != null && httpException.GetHttpCode() != 500)//为什么要特别强调500 因为MVC处理HttpException的时候，如果为500 则会自动将其ExceptionHandled设置为true，那么我们就无法捕获异常
                {
                    log.Error("<br/><strong>HttpCode</strong>：" + httpException.GetHttpCode() + "<br/><strong>客户机IP</strong>：" + HttpContext.Current.Request.UserHostAddress + "<br /><strong>错误地址</strong>：【" + HttpContext.Current.Request.HttpMethod + "】" + "【url】=" + HttpContext.Current.Request.Url + "【UrlReferrer】=" + HttpContext.Current.Request.UrlReferrer + "【Platform】=" + HttpContext.Current.Request.Browser.Platform + "【UserAgent】=" + HttpContext.Current.Request.UserAgent, httpException);
                    return;
                }
            }
            Exception exception = context.Exception;
            if(exception != null)
            {
                HttpException httpException = exception as HttpException;
                if(httpException != null)
                {
                    //网络错误
                    context.Controller.ViewBag.UrlRefer = context.HttpContext.Request.UrlReferrer;
                    int DataEroorCode = httpException.GetHttpCode();
                    if(DataEroorCode == 404)
                    {
                        //context.HttpContext.Response.Redirect("~/SysError/404");
                    }
                    else if(DataEroorCode == 500)
                    {
                        //context.HttpContext.Response.Redirect("~/SysError/500");
                    }
                    else
                    {
                        //context.HttpContext.Response.Redirect("~/SysError/" + DataEroorCode);
                    }

                    //写入日志 记录
                    log.Error("<br/><strong>HttpCode</strong>：" + httpException.GetHttpCode() + "<br/><strong>客户机IP</strong>：" + HttpContext.Current.Request.UserHostAddress + "<br /><strong>错误地址</strong>：【" + HttpContext.Current.Request.HttpMethod + "】" + "【url】=" + HttpContext.Current.Request.Url + "【UrlReferrer】=" + HttpContext.Current.Request.UrlReferrer + "【Platform】=" + HttpContext.Current.Request.Browser.Platform + "【UserAgent】=" + HttpContext.Current.Request.UserAgent, httpException);
                    context.ExceptionHandled = true;//设置异常已经处理
                }
                else
                {
                    log.Error("<br/><strong>客户机IP</strong>：" + HttpContext.Current.Request.UserHostAddress + "<br /><strong>错误地址</strong>：【" + HttpContext.Current.Request.HttpMethod + "】" + "【url】=" + HttpContext.Current.Request.Url + "【UrlReferrer】=" + HttpContext.Current.Request.UrlReferrer + "【Platform】=" + HttpContext.Current.Request.Browser.Platform + "【UserAgent】=" + HttpContext.Current.Request.UserAgent, exception);
                    //编程或者系统错误，不处理，留给HandError处理
                }
            }

            //context.RequestContext.HttpContext.Response.Redirect("http://wx.newsasker.com/help/error.html");
        }

    }
}