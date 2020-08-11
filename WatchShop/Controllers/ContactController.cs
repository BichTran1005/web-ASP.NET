using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchShop.Models;
namespace WatchShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        TheWatchShopDbContext db = new TheWatchShopDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Contact contact)
        {
           if(ModelState.IsValid)
            {
                contact.created_at = DateTime.Now;
                contact.updated_at = DateTime.Now;
                contact.updated_by = 1;
                contact.status = 1;
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}