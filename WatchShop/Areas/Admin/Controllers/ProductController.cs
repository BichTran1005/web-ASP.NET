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
    public class ProductController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var list = db.Products.Where(m => m.status != 0).ToList();
            return View(list);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.list = db.Products.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            var list = db.Products.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = product.id;
                string slug = Mystring.ToSlug(product.name);
                product.slug = slug;
                product.img = slug;
                product.promo = 0;
                product.created_at = DateTime.Now;
                product.updated_at = DateTime.Now;
                product.updated_by = int.Parse(Session["User_Id"].ToString());
                product.created_by = int.Parse(Session["User_Id"].ToString());
                db.Products.Add(product);
                db.SaveChanges();

                //Link link = new Link();
                //link.slug = slug;
                //link.tableId = product.id;
                //link.types = "category";
                //db.Links.Add(link);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            var list = db.Products.ToList();
            ViewBag.list = list;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var list = db.Products.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = product.id;
                string slug = Mystring.ToSlug(product.name);
                product.slug = slug;
                product.created_at = DateTime.Now;
                product.updated_by = int.Parse(Session["User_Id"].ToString());
                product.created_by = int.Parse(Session["User_Id"].ToString());
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                //Link link = db.Links.Where(m => m.tableId == id && m.types == "category").First();
                //link.slug = slug;
                //db.Entry(link).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");


            }
            return View(product);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Status(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            product.status = (product.status == 1) ? 2 : 1;
            product.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Detrash(int id)
        {
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            //int count_child = db.Products.Where(m => m.status == 1).Count();
            if (product.status==1)
            {
                Thongbao.set_flash("Sản phẩm đang được xuất bản!", "danger");
                return RedirectToAction("Index");
            }

            product.status = 0;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công!", "seccess");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Products.Where(m => m.status == 0).ToList();
            return View(list);
        }

        public ActionResult Retrash(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            product.status = 2;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("khôi phục thành công!", "success");
            return RedirectToAction("Trash", "Product");
        }

        public String CategoryName(int? id)
        {
            var category = db.Categories.Where(m=>m.id==id).Select(m=>m.name).First().ToString();
            return category;
            
        }

    }
}
