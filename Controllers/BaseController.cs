using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using CMS.Models;

namespace CMS.Controllers {

    public class BaseController : Controller {

        protected Db db {
            get;
            private set;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            db = new Db();
            base.OnActionExecuting(filterContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext) {
            base.OnResultExecuted(filterContext);
            db.Dispose();
        }

        private void alert(string type, string icon, string title, string message) {
            TempData["alertType"] = type;
            TempData["alertIcon"] = icon;
            TempData["alertTitle"] = title;
            TempData["alertMessage"] = message;
        }

        public void alertSuccess(string title, string message) {
            alert("success", "fa-check", title, message);
        }

        public void alertInfo(string title, string message) {
            alert("info", "fa-info-circle", title, message);
        }

        public void alertWarning(string title, string message) {
            alert("warning", "fa-exclamation-triangle", title, message);
        }

        public void alertDanger(string title, string message) {
            alert("danger", "fa-times", title, message);
        }

    }

}