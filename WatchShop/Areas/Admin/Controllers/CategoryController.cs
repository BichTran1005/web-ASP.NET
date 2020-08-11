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
    public class CategoryController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var list = db.Categories.Where(m => m.status != 0).ToList();
            return View(list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.list = db.Categories.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            
            var list = db.Categories.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = category.id;
                string slug= Mystring.ToSlug(category.name);
                category.slug = slug;
                category.created_at = DateTime.Now;
                category.updated_by = int.Parse(Session["User_Id"].ToString());
                category.created_by = int.Parse(Session["User_Id"].ToString());
                db.Categories.Add(category);
                db.SaveChanges();

                Link link = new Link();
                link.slug = slug;
                link.tableId = category.id;
                link.types = "category";
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            var list = db.Categories.ToList();
            ViewBag.list = list;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            var list = db.Categories.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = category.id;
                string slug = Mystring.ToSlug(category.name);
                category.slug = slug;
                category.created_at = DateTime.Now;
                category.updated_by = int.Parse(Session["User_Id"].ToString());
                category.created_by = int.Parse(Session["User_Id"].ToString());
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();

                Link link = db.Links.Where(m => m.tableId == id && m.types == "category").First();
                link.slug = slug;
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Trash");
        }

        public ActionResult Status(int id)
        {
            Category category = db.Categories.Find(id);
            if(category==null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            category.status = (category.status == 1) ? 2 : 1;
            category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Detrash (int id)
        {
            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return RedirectToAction("Index");
            }
            
            int count_child = db.Categories.Where(m => m.parentid == id).Count();
            if(count_child!=0)
            {
                Thongbao.set_flash("Loại sản phẩm có chứa cấp con!", "danger");
                return RedirectToAction("Index");
            }
            int count_product = db.Products.Where(m => m.catid == id).Count();
            if (count_product != 0)
            {
                Thongbao.set_flash("Loại sản phẩm có chứa sản phẩm!", "danger");
                return RedirectToAction("Index");
            }
            category.status = 0;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công!", "seccess");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Categories.Where(m => m.status == 0).ToList();
            return View(list);
        }

        public ActionResult Retrash(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            category.status = 2;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("khôi phục thành công!", "success");
            return RedirectToAction("Trash","Category");
        }

    }
}
