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
    public class UserController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var list = db.Users.Where(m => m.status != 0).ToList();
            return View(list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Category/Create
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

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            var list = db.Users.ToList();
            ViewBag.list = list;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            var list = db.Users.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = user.id;
                user.img = Mystring.ToSlug(user.fullname);
               
                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;
                user.updated_by = int.Parse(Session["User_Id"].ToString());
                user.created_by = int.Parse(Session["User_Id"].ToString());
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");


            }
            return View(user);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            user.status = (user.status == 1) ? 2 : 1;
            user.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Detrash(int id)
        {
            User user = db.Users.Find(id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

           
            if (user.status == 1)
            {
                Thongbao.set_flash("Tài khoản đang được kích hoạt", "danger");
                return RedirectToAction("Index");
            }
          
            user.status = 0;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công!", "seccess");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Users.Where(m => m.status == 0).ToList();
            return View(list);
        }

        public ActionResult Retrash(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            user.status = 2;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("khôi phục thành công!", "success");
            return RedirectToAction("Trash", "User");
        }

    }
}
