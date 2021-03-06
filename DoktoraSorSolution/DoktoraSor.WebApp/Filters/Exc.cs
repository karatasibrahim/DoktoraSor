using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoktoraSor.WebApp.Filters
{
    public class Exc : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData["LastError"] = filterContext.Exception;
            filterContext.ExceptionHandled = true; //Hatayı yönet
            filterContext.Result = new RedirectResult("/Home/HasError");

        }
    }
}