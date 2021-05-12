using DoktoraSor.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoktoraSor.WebApp.Filters
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.UserDoctor!=null && CurrentSession.UserDoctor.IsAdmin==false)
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
           
        }
    }
}