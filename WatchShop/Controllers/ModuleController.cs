using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchShop.Models;

namespace WatchShop.Controllers
{
    
    public class ModuleController : Controller
    {
        TheWatchShopDbContext db = new TheWatchShopDbContext();
        // GET: Module
        public ActionResult MainMenu()
        {
            var list = db.Menus.Where(m => m.parentid == 0 && m.status == 1 && m.position == "mainmenu").OrderBy(m => m.orders).ToList();
         
            return View("_MainMenu",list);
            
        }

        public ActionResult SubMainMenu(int parentid)
        {
            ViewBag.menuitem = db.Menus.Find(parentid);

            var list = db.Menus.Where(m => m.parentid == parentid && m.status == 1 && m.position == "mainmenu").OrderBy(m => m.orders).ToList();
            if(list.Count()!=0)
            {
                return View("_SubMainMenu2", list);
            }
            else
            {
                return View("_SubMainMenu1", list);
            }
        }

        public ActionResult SliderShow()
        {
            var list = db.Sliders.Where(m => m.position == "slideshow" && m.status == 1).OrderBy(m => m.orders).ToList();
            return View("_SliderShow", list);
        }

        public ActionResult Category()
        {
            var list = db.Categories.Where(m => m.parentid == 0 && m.status == 1).OrderBy(m => m.orders).ToList();
            return View("_Category", list);
        }

        public ActionResult SubCategory(int parentid)
        {
            ViewBag.categoryitem = db.Categories.Find(parentid);

            var list = db.Categories.Where(m => m.parentid == parentid && m.status == 1).OrderBy(m => m.orders).ToList();
            if (list.Count() != 0)
            {
                return View("_SubCategory2", list);
            }
            else
            {
                return View("_SubCategory1", list);
            }
        }

        public ActionResult Post()
        {
            var list = db.Posts.Where(m => m.type =="post"  && m.status == 1).ToList();
            return View("_Post", list);
        }
    }
}