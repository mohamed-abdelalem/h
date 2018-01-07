using Chat_Room.manage;
using chat_rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chat_rooms.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult logout()
        //{
        //    return View("/login/index");
        //}
        [HttpPost]
        public ActionResult login(User user)
        {
            User_Manager manage = new User_Manager();
            List<User> list_user = new List<Models.User>();
            list_user = manage.select().ToList();
            foreach(var item in list_user)
            {
                if(item.User_Name==user.User_Name&&item.Password==user.Password)
                {

                    HttpCookie local1 = new HttpCookie("user_server");
                    local1.Values.Add("user_email", item.User_Name);
                    local1.Values.Add("user_password", item.Password);
                    local1.Values.Add("ID", item.ID.ToString());
                    Response.Cookies.Add(local1);

                    return Redirect("/message/index/");

                }


            }
            return Redirect("/login/index/");
        }


    }
}