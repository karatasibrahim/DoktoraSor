using DoktoraSor.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoktoraSor.WebApp.Filters
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.UserDoctor==null || CurrentSession.UserPatient==null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}