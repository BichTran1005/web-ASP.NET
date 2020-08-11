using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatchShop
{
    public static class Thongbao
    {
        public static bool has_flash()
        {
            if(System.Web.HttpContext.Current.Session["Thong_Bao"].Equals(""))
            {
                return false;
            }
            return true;
        }

        public static void set_flash(string msg, string msg_type)
        {
            ThongbaoModel tb = new ThongbaoModel();
            tb.msg = msg;
            tb.msg_type = msg_type;
            System.Web.HttpContext.Current.Session["Thong_Bao"] = tb;

        }

        public static ThongbaoModel get_flash()
        {
            ThongbaoModel thongbao = (ThongbaoModel)System.Web.HttpContext.Current.Session["Thong_Bao"];
            System.Web.HttpContext.Current.Session["Thong_Bao"] = "";
            return thongbao;
        }
    }
}