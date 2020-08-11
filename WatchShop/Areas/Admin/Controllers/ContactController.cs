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
    public class ContactController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var list = db.Contacts.Where(m => m.status != 0).ToList();
            return View(list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.list = db.Contacts.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            
            var list = db.Contacts.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = contact.id;

                contact.created_at = DateTime.Now;
                contact.updated_by = int.Parse(Session["User_Id"].ToString());
                db.Contacts.Add(contact);
                db.SaveChanges();

             
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            var list = db.Contacts.ToList();
            ViewBag.list = list;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            var list = db.Contacts.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                int id = contact.id;

                contact.created_at = DateTime.Now;
                contact.updated_by = int.Parse(Session["User_Id"].ToString());
               
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();

             
                return RedirectToAction("Index");
                
            }
            return View(contact);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if(contact == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            contact.status = (contact.status == 1) ? 2 : 1;
            contact.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Detrash (int id)
        {
            Contact contact = db.Contacts.Find(id);

            if (contact == null)
            {
                return RedirectToAction("Index");
            }
            
           
         
            if (contact.status==1)
            {
                Thongbao.set_flash("Chưa xem thông tin liên hệ này!", "danger");
                return RedirectToAction("Index");
            }
            contact.status = 0;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công!", "seccess");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Contacts.Where(m => m.status == 0).ToList();
            return View(list);
        }

        public ActionResult Retrash(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            contact.status = 2;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("khôi phục thành công!", "success");
            return RedirectToAction("Trash","Category");
        }

    }
}
