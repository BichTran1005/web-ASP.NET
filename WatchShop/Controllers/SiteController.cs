using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchShop.Models;
namespace WatchShop.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        TheWatchShopDbContext db = new TheWatchShopDbContext(); 
        public ActionResult Index(string slug="")
        {
            if(slug=="")
            {
                return this.Home();
            }
            else
            {
                var row_link = db.Links.Where(m => m.slug == slug);
                if (row_link.Count()!=0)
                {
                    var link = row_link.First();
                    string type = link.types.Trim();
                    switch(type)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug);
                            }
                           
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                         
                        default: return this.Erorr404(slug);
                    }
                }
                else
                {
                    int count_product = db.Products.Where(m => m.slug == slug && m.status == 1).Count();
                    if(count_product!=0)
                    {
                        return this.ProductDetai(slug);
                    }
                    else
                    {
                        int count_post = db.Posts.Where(m => m.slug == slug && m.status == 1).Count();
                        if (count_post != 0)
                        {
                            return this.PostDetail(slug);
                        }
                        else
                        {
                           
                        }
                    }

                    return null;
                }
            }
            
        }
        public ActionResult Home()
        {
            
            var list1 = db.Categories.Where(m => m.parentid == 0).ToList();
            return View("Home",list1);
        }
        public ActionResult Product()
        {
            return View("Product");
        }

        public ActionResult ProductHome(int catid)
        {
            
            //var item = db.Categories.Where(m => m.slug == slug).First();
            //ViewBag.Title = item.name;
            //List<int> listcatid = db.Categories.Where(m => m.parentid == item.id).Select(m => m.id).ToList();
            //listcatid.Add(item.id);
            //var list = db.Products.Where(m => m.status == 1 && listcatid.Contains(m.catid)).OrderByDescending(m => m.created_at).ToList();
            var list = db.Products.Where(m => m.status == 1 && m.catid == catid).ToList();
            return View("_ProductHome", list);
        }

        public ActionResult ProductCategory(string slug)
        {
            var item = db.Categories.Where(m => m.slug == slug).First();
            ViewBag.Title = item.name;
            List<int> listcatid = db.Categories.Where(m => m.parentid == item.id).Select(m => m.id).ToList();
            listcatid.Add(item.id);
            var list = db.Products.Where(m => m.status == 1 && listcatid.Contains(m.catid)).OrderByDescending(m => m.created_at).ToList();
            return View("ProductCategory", list);
        }

        public ActionResult ProductDetai(string slug)
        {
            var item = db.Products.Where(m => m.slug == slug && m.status == 1).First();
            return View("ProductDetai",item);
        }

        public ActionResult PostTopic(string slug)
        {
            return View("PostTopic");
        }

        public ActionResult PostPage(string slug)
        {
            return View("PostPage");
        }
        public ActionResult PostDetail(string slug)
        {
            var item = db.Products.Where(m => m.slug == slug && m.status == 1).First();
            return View("PostDetail", item);
        }
        public ActionResult Erorr404(string slug)
        {
            return View("Erorr404");
        }


        public ActionResult Login()
        {
            if (Session["User_Admin"].ToString() != "")
            {
                Response.Redirect("~/View/Site/Home");
            }
            return View();
        }
        [HttpPost]


        public ActionResult Create()
        {
            ViewBag.list = db.Users.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {

            var list = db.Users.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = user.id;
                user.created_at = DateTime.Now;
                user.updated_by = int.Parse(Session["User_Id"].ToString());
                user.created_by = int.Parse(Session["User_Id"].ToString());
                db.Users.Add(user);
                db.SaveChanges();
                user.password = Mystring.ToMD5(user.password);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public void Logout()
        {
            if (!Session["User_Admin"].Equals(""))
            {
                Session["User_Admin"] = "";
                Session["User_Id"] = "";
                Response.Redirect("~/Home");
            }
            else
            {
                Response.Redirect("");
            }

        }
    }
}

//var list = db.Posts.ToList();

//            foreach (var item in list)
//            {
//                Link link = new Link();
//link.slug = item.slug;
//                link.tableId = item.id;
//                link.types = "topic";
//                db.Links.Add(link);
//                db.SaveChanges();
//            }