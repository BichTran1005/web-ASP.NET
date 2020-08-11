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
    public class SliderController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var list = db.Sliders.Where(m => m.status != 0).ToList();
            return View(list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.list = db.Sliders.Where(m => m.status != 0).ToList();
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {
            var list = db.Sliders.ToList();
            ViewBag.list = list;
            if (ModelState.IsValid)
            {
                slider.created_at = DateTime.Now;
                slider.updated_at = DateTime.Now;
                slider.url = "slider";
                slider.img = Mystring.ToSlug(slider.name);
                slider.updated_by = int.Parse(Session["User_Id"].ToString());
                slider.created_by = int.Parse(Session["User_Id"].ToString());
                db.Sliders.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,slug,parentid,orders,metakey,metadesc,created_at,created_by,updated_by,status")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Sliders.Find(id);
            db.Sliders.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        

        public ActionResult Status(int id)
        {
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            slider.status = (slider.status == 1) ? 2 : 1;
            slider.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi trạng thái thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Detrash(int id)
        {
            Slider slider = db.Sliders.Find(id);

            slider.status = 0;
            slider.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công!", "success");
            return RedirectToAction("Index");
        }

        public ActionResult Trash()
        {
            var list = db.Sliders.Where(m => m.status == 0).ToList();
            return View(list);
        }

        public ActionResult Retrash(int id)
        {
            Slider slider = db.Sliders.Find(id);
            if (slider == null)
            {
                Thongbao.set_flash("Loại sản phẩm không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            slider.status = 2;
            slider.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("khôi phục thành công!", "success");
            return RedirectToAction("Trash", "Slider");
        }

    }
}
