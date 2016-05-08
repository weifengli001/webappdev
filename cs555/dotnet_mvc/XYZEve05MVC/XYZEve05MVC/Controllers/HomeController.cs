using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XYZEve05MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Logout()
        {
            SessionFacade.Logout();
            return Redirect("Index");
        }
    }
}