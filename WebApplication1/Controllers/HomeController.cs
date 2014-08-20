using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class MyModel
    {
        public MyModel(string msg)
        {
            this.Message = msg;
        }

        public string Message { get; private set; }
    }

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("~/tada/tada.cshtml", new MyModel("tada"));
        }
    }
}