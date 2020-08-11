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
    public class MenuController : Controller
    {
        private TheWatchShopDbContext db = new TheWatchShopDbContext();

        public ActionResult Index()
        {
            ViewBag.listCat = db.Categories.Where(m => m.status == 1).ToList();
            ViewBag.listTopic = db.Topics.Where(m => m.status == 1).ToList();
            ViewBag.listPage = db.Posts.Where(m => m.status == 1 && m.type == "page").ToList();
            return View(db.Menus.Where(m => m.status != 0).ToList());
        }
        [HttpPost]
        public ActionResult Index(FormCollection dulieu)
        {
            //X? LƯ TOPIC
            if (!string.IsNullOrEmpty(dulieu["THEMTOPIC"]))
            {
                if (!string.IsNullOrEmpty(dulieu["itemTop"]))
                {
                    var itemtop = dulieu["itemTop"];
                    var arrtop = itemtop.Split(',');
                    int dem = 0;
                    foreach (var rtop in arrtop)
                    {
                        int id = int.Parse(rtop);
                        Topic topic = db.Topics.Find(id);
                        Menu menu = new Menu();
                        menu.name = topic.name;
                        menu.link = topic.slug;
                        menu.type = "topic";
                        menu.tableid = id;
                        menu.orders = 1;
                        menu.position = dulieu["position"];
                        menu.parentid = 0;
                        menu.status = 2;
                        menu.created_at = DateTime.Now;
                        menu.created_by = int.Parse(Session["User_Id"].ToString());
                        menu.updated_at = DateTime.Now;
                        menu.updated_by = int.Parse(Session["User_Id"].ToString());
                        db.Menus.Add(menu);
                        db.SaveChanges();
                        dem++;
                    }
                    Thongbao.set_flash("Thêm thành công " + dem + "Menu", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    Thongbao.set_flash("Chua chọn menu", "danger");
                    return RedirectToAction("Index");
                }
            }
            //X? LƯ CATEGORY
            if (!string.IsNullOrEmpty(dulieu["THEMCATEGORY"]))
            {
                if (!string.IsNullOrEmpty(dulieu["itemCat"]))
                {
                    var itemcat = dulieu["itemCat"];
                    var arrcat = itemcat.Split(',');
                    int dem = 0;
                    foreach (var rcat in arrcat)
                    {
                        int id = int.Parse(rcat);
                        Category cat = db.Categories.Find(id);
                        Menu menu = new Menu();
                        menu.name = cat.name;
                        menu.link = cat.slug;
                        menu.type = "category";
                        menu.tableid = id;
                        menu.orders = 1;
                        menu.position = dulieu["position"];
                        menu.parentid = 0;
                        menu.status = 2;
                        menu.created_at = DateTime.Now;
                        menu.created_by = int.Parse(Session["User_Id"].ToString());
                        menu.updated_at = DateTime.Now;
                        menu.updated_by = int.Parse(Session["User_Id"].ToString());
                        db.Menus.Add(menu);
                        db.SaveChanges();
                        dem++;
                    }
                    Thongbao.set_flash("Thêm thành công " + dem + " Menu", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    Thongbao.set_flash("Chua chọn menu", "danger");
                    return RedirectToAction("Index");
                }
            }
            //X? LƯ Page
            if (!string.IsNullOrEmpty(dulieu["THEMPAGE"]))
            {
                if (!string.IsNullOrEmpty(dulieu["itemPage"]))
                {
                    var itempage = dulieu["itemPage"];
                    var arrpage = itempage.Split(',');
                    int dem = 0;
                    foreach (var rcat in arrpage)
                    {
                        int id = int.Parse(rcat);
                        Post mpo = db.Posts.Find(id);
                        Menu menu = new Menu();
                        menu.name = mpo.title;
                        menu.link = mpo.slug;
                        menu.type = "page";
                        menu.tableid = id;
                        menu.orders = 1;
                        menu.position = dulieu["position"];
                        menu.parentid = 0;
                        menu.status = 2;
                        menu.created_at = DateTime.Now;
                        menu.created_by = int.Parse(Session["User_Id"].ToString());
                        menu.updated_at = DateTime.Now;
                        menu.updated_by = int.Parse(Session["User_Id"].ToString());
                        db.Menus.Add(menu);
                        db.SaveChanges();
                        dem++;
                    }
                    Thongbao.set_flash("Thêm thành công " + dem + " Menu", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    Thongbao.set_flash("Chua chọn menu", "danger");
                    return RedirectToAction("Index");
                }
            }
            //X? LƯ CUSTOM
            if (!string.IsNullOrEmpty(dulieu["THEMCUSTOM"]))
            {
                if (!string.IsNullOrEmpty(dulieu["name"]) && !string.IsNullOrEmpty(dulieu["link"]))
                {
                    Menu menu = new Menu();
                    menu.name = dulieu["name"];
                    menu.link = dulieu["link"];
                    menu.type = "custom";
                    menu.orders = 1;
                    menu.position = dulieu["position"];
                    menu.parentid = 0;
                    menu.status = 2;
                    menu.created_at = DateTime.Now;
                    menu.created_by = int.Parse(Session["User_Id"].ToString());
                    menu.updated_at = DateTime.Now;
                    menu.updated_by = int.Parse(Session["User_Id"].ToString());
                    db.Menus.Add(menu);
                    db.SaveChanges();
                    Thongbao.set_flash("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    Thongbao.set_flash("Tên menu và link không được trùng !", "danger");
                    return RedirectToAction("Index");
                }
            }
            ViewBag.listCat = db.Categories.Where(m => m.status == 1).ToList();
            ViewBag.listTopic = db.Topics.Where(m => m.status == 1).ToList();
            ViewBag.listPage = db.Posts.Where(m => m.status == 1 && m.type == "page").ToList();
            return View(db.Menus.Where(m => m.status != 0).ToList());
        }
        public ActionResult Trash()
        {
            return View(db.Menus.Where(m => m.status == 0).ToList());
        }
        // GET: Admin/Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }
        // GET: Admin/Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Link,Type,TableId,Orders,Position,ParentId,Createdat,Createdby,Updatedat,Updatedby,Status")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: Admin/Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu mmenu = db.Menus.Find(id);
            db.Menus.Remove(mmenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Status(int id)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                Thongbao.set_flash("Menu không t?n t?i", "danger");
                return RedirectToAction("Index");
            }
            menu.status = (menu.status == 1) ? 2 : 1;
            menu.updated_at = DateTime.Now;
            menu.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Thay đổi thái thành công", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Retrash(int id)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                Thongbao.set_flash("Menu này không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            menu.status = 2;
            menu.updated_at = DateTime.Now;
            menu.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Khôi phục thành công", "success");
            return RedirectToAction("Trash");
        }
        public ActionResult Deltrash(int id)
        {
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                Thongbao.set_flash("Menu này không tồn tại", "danger");
                return RedirectToAction("Index");
            }
            menu.status = 0;
            menu.updated_at = DateTime.Now;
            menu.updated_by = int.Parse(Session["User_Id"].ToString());
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            Thongbao.set_flash("Xóa vào thùng rác thành công", "success");
            return RedirectToAction("Index");
        }

    }
}
