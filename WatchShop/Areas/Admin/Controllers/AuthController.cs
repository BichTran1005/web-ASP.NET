using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchShop;
using WatchShop.Models;
namespace WatchShop.Areas.Admin.Controllers
{
    
    public class AuthController : Controller
    {
        TheWatchShopDbContext db = new TheWatchShopDbContext();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            if(Session["User_Admin"].ToString()!="")
            {
                Response.Redirect("~/Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection field)
        {
           
            string username = field["Username"];
            string password = Mystring.ToMD5(field["Password"]);
            int count_username = db.Users.Where(m => (m.username == username || m.email==username) && m.status == 1 && m.access != 0).Count();
            if(count_username==0)
            {
                ViewBag.Error = "<span class='text-danger' >Tài khoản không tồn tại!</span>";
            }
            else
            {
                var count_account = db.Users.Where(m => (m.username == username || m.email == username) && m.status == 1 && m.access != 0 && m.password == password);
                if(count_account.Count() == 0)
                {
                    ViewBag.Error = "<span class='text-danger' >Mật khẩu không chính xác!</span>";
                }
                else
                {
                    var user = count_account.First();
                    Session["User_Admin"] =user.username;
                    Session["User_Id"] = user.id;
                    Response.Redirect("~/Admin");
                }
                return null;
            }
            
            return View("Login");
        }

        public void Logout()
        {
            if (!Session["User_Admin"].Equals(""))
            {
                Session["User_Admin"] = "";
                Session["User_Id"] = "";
                Response.Redirect("~/Admin");
            }
            else
            {
                Response.Redirect("~/Admin/login");
            }

        }
    }
}