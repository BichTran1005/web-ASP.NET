using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchShop.Models;

namespace WatchShop.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var list = db.Posts.Where(m => m.status != 0).ToList();
            return View(list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.list = db.Posts.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {

            var list = db.Posts.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = post.id;
                string slug = Mystring.ToSlug(post.title);
                post.slug = slug;
                post.img = slug;
                post.created_at = DateTime.Now;
                post.updated_at = DateTime.Now;
                post.updated_by = int.Parse(Session["User_Id"].ToString());
                post.created_by = int.Parse(Session["User_Id"].ToString());
                db.Posts.Add(post);
                db.SaveChanges();

                Link link = new Link();
                link.slug = slug;
                link.tableId = post.id;
                link.types = "post";
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            var list = db.Posts.ToList();
            ViewBag.list = list;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            var list = db.Posts.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = post.id;
                string slug = Mystring.ToSlug(post.title);
                post.slug = slug;
                post.img = slug;
                post.created_at = DateTime.Now;
                post.updated_at = DateTime.Now;
                post.updated_by = int.Parse(Session["User_Id"].ToString());
                post.created_by = int.Parse(Session["User_Id"].ToString());
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                Link link = db.Links.Where(m => m.tableId == id && m.types == "post").First();
                link.slug = slug;
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(post);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            post.status = (post.status == 1) ? 2 : 1;
            post.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Detrash(int id)
        {
            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return RedirectToAction("Index");
            }
            
           
            if (post.status == 1)
            {
                Thongbao.set_flash("Bài viết đang được xuất bản!", "danger");
                return RedirectToAction("Index");
            }
            post.status = 0;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công!", "seccess");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Posts.Where(m => m.status == 0).ToList();
            return View(list);
        }

        public ActionResult Retrash(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            post.status = 2;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("khôi phục thành công!", "success");
            return RedirectToAction("Trash", "Category");
        }

    }
}
