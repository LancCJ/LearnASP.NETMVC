using LearnASP.NETMVC.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnASP.NETMVC.Filters
{
    public class EmployeeExceptionFilter: HandleErrorAttribute
    {

        public override void OnException(ExceptionContext filterContext)
        {

            FileLogger logger = new FileLogger();
            logger.LogException(filterContext.Exception);

            base.OnException(filterContext);

            //当返回自定义响应时，做的第一件事情就是通知MVC 引擎，手动处理异常，因此不需要执行默认的操作，不会显示默认的错误页面
            //filterContext.ExceptionHandled = true;
            //filterContext.Result = new ContentResult()
            //{
            //    Content = "Sorry for the Error"
            //};
        }

    }
}