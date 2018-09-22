using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Http403(Exception exception)
        {
            Response.StatusCode = 403;
            Response.ContentType = "text/html";
            return View(exception);
        }

        public ActionResult Http404(Exception exception)
        {
            Response.StatusCode = 404;
            Response.ContentType = "text/html";
            return View(exception);
        }

        public ActionResult Http500(Exception exception)
        {
            Response.StatusCode = 500;
            Response.ContentType = "text/html";
            return View(exception);
        }

    }
}