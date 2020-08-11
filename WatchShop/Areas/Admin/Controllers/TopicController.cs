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
    public class TopicController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        // GET: Admin/Topic
        public ActionResult Index()
        {
            return View(db.Topics.ToList());
        }

        // GET: Admin/Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Admin/Topic/Create
        public ActionResult Create()
        {
            ViewBag.list = db.Topics.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Topic topic)
        {
            var list = db.Topics.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                
                string slug = Mystring.ToSlug(topic.name);
                topic.slug = slug;
                topic.created_at = DateTime.Now;
                topic.updated_at = DateTime.Now;
                topic.updated_by = int.Parse(Session["User_Id"].ToString());
                topic.created_by = int.Parse(Session["User_Id"].ToString());
                db.Topics.Add(topic);
                db.SaveChanges();

                Link link = new Link();
                link.slug = slug;
                link.tableId = topic.id;
                link.types = "topic";
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(topic);
        }

        // GET: Admin/Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,slug,parentid,orders,metakey,metadesc,created_at,created_by,updated_at,updated_by,status")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        // GET: Admin/Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Admin/Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Status(int id)
        {
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            topic.status = (topic.status == 1) ? 2 : 1;
            topic.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Detrash(int id)
        {
            Topic topic = db.Topics.Find(id);

            if (topic == null)
            {
                return RedirectToAction("Index");
            }

            int count_child = db.Topics.Where(m => m.parentid == id).Count();
            if (count_child != 0)
            {
                Thongbao.set_flash("Loại sản phẩm có chứa cấp con!", "danger");
                return RedirectToAction("Index");
            }
            int count_product = db.Posts.Where(m => m.topid == id).Count();
            if (count_product != 0)
            {
                Thongbao.set_flash("Loại sản phẩm có chứa sản phẩm!", "danger");
                return RedirectToAction("Index");
            }
            topic.status = 0;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công!", "seccess");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Topics.Where(m => m.status == 0).ToList();
            return View(list);
        }

        public ActionResult Retrash(int id)
        {
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            topic.status = 2;
            //category.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("khôi phục thành công!", "success");
            return RedirectToAction("Trash", "Topic");
        }


    }
}
