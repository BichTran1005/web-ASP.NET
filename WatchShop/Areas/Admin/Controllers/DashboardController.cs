using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatchShop.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if (Session["User_Admin"].Equals(""))
            {
                Response.Redirect("~/Admin/login");
            }
            return View();
        }
        
    }
}