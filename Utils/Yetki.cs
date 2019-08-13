using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

public class OturumAttribute : AuthorizeAttribute {

    protected override bool AuthorizeCore(HttpContextBase httpContext) {
        return true;// Util.oturumAcikmi();
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {
        filterContext.Result = new RedirectResult("~/admin");
    }

}
