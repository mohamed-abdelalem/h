using Chat_Room.manage;
using chat_rooms.manage;
using chat_rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Chat_Room.Controllers
{
    public class messageController : Controller
    {
        int messages_number;
        // GET: message
        public ActionResult Index()
        {

            if (Request.Cookies["user_server"] != null)
            {

                HttpCookie local = System.Web.HttpContext.Current.Request.Cookies["user_server"];


                string name = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
                name = name.Substring(11);
                name = name.Split('&')[0];
                string password = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
                string[] substrings = password.Split('&');
                password = substrings[1];
                password = password.Substring(14);
                string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
                user_ID = user_ID.Split('&')[2];
                user_ID = user_ID.Substring(3);
                int ID = Convert.ToInt16(user_ID);
                return View();

            }
            else
            {
                return Redirect("/login/index/");
            }
        }

        public ActionResult get_all_users()
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);


            User_Manager manage = new User_Manager();
            List<User> users = manage.select().ToList();
            return Json(
    users.Select(x => new
    {
        ID = x.ID,
        User_Name = x.User_Name,
        Password = x.Password

        // Assigment of child fields
    }).Where(x => x.ID != ID), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult send_message(message messge)
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);
            messge.ID_Sender = ID;
            Message_Manager manage = new Message_Manager();
            manage.add(messge);
            return Json(new
            {
                ID = messge.ID,
                ID_Sender = messge.ID_Sender,
                ID____Receiver = messge.ID____Receiver,
                Text = messge.Text,
                seen = messge.seen,
                Date = messge.Date.ToLocalTime().ToString()
                // Assigment of child fields
            });

        }



        public ActionResult get_all_message(int reciver)
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);
            int user_id = ID;
            Message_Manager manage = new Message_Manager();
            List<message> message = manage.select().ToList();
            Session.Add("messages_number", 15);

            return Json(
        message.Select(x => new
        {
            ID = x.ID,
            ID_Sender = x.ID_Sender,
            ID____Receiver = x.ID____Receiver,
            Text = x.Text,
            Date = x.Date.ToLocalTime().ToString()
            // Assigment of child fields
        }).Where(x => (x.ID____Receiver == reciver && user_id == x.ID_Sender)
        || (x.ID_Sender == reciver && x.ID____Receiver == user_id)


    ).OrderBy(x => x.ID).Reverse().Take(15).Reverse(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult previous_message(int reciver)
        {
            messages_number = Convert.ToInt16(Session["messages_number"]);
            messages_number = messages_number + 5;
            Session.Add("messages_number", messages_number);

            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);


            int user_id = ID;
            Message_Manager manage = new Message_Manager();
            List<message> message = manage.select().ToList();
            return Json(
    message.Select(x => new
    {
        ID = x.ID,
        ID_Sender = x.ID_Sender,
        ID____Receiver = x.ID____Receiver,
        Text = x.Text,
        Date = x.Date.ToLocalTime().ToString()
        // Assigment of child fields
    }).Where(x => (x.ID____Receiver == reciver && user_id == x.ID_Sender)
    || (x.ID_Sender == reciver && x.ID____Receiver == user_id)


    ).OrderBy(x => x.ID).Reverse().Take(messages_number).Reverse(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult get_new_messages(int reciver)
        {

            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);



            int user_id = ID;
            Message_Manager manage = new Message_Manager();
            List<message> message = manage.select().ToList();
            return Json(
    message.Select(x => new
    {
        ID = x.ID,
        ID_Sender = x.ID_Sender,
        ID____Receiver = x.ID____Receiver,
        Text = x.Text,
        seen = x.seen,
        Date = x.Date.ToLocalTime().ToString()
    }).Where(x => (x.ID_Sender == reciver && x.ID____Receiver == user_id) && (x.seen == false)), JsonRequestBehavior.AllowGet);
        }


        public void update_messages(int reciver)
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);





            int user_id = ID;
            Message_Manager manage = new Message_Manager();
            List<message> message = manage.select().Where(x =>//(x.ID_Sender==user_id&&x.ID____Receiver==reciver&&x.seen==false)||
            (x.ID_Sender == reciver && x.ID____Receiver == user_id && x.seen == false)



            ).ToList();
            foreach (var item in message)
            {
                item.seen = true;

                manage.update(item);
            }

        }



        public ActionResult logout()
        {
            Response.Cookies["user_server"].Expires = DateTime.Now.AddDays(-1);
            return Redirect("/login/Index");
        }






        public ActionResult search_2(string name)

        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);
            friend_Relations_manage relation = new friend_Relations_manage();
            List<friend_Relations> friends = relation.select().ToList();

            friends = friends.Where(x => x.User_ID == ID).ToList();

            User_Manager manage = new User_Manager();
            List<User> users = new List<chat_rooms.Models.User>();

            foreach (var item in friends)
            {
                foreach (var m in manage.select().ToList())
                {
                    if (m.ID == item.friend_ID && item.Relation_Type == 1)
                    {

                        users.Add(m);
                    }
                }

            }
            users = users.Where(x => x.User_Name.Contains(name)).ToList();


            List<User> all_friends = new List<chat_rooms.Models.User>();

            foreach (var user in users)
            {
                foreach (var item in relation.select().ToList())
                {
                    if (user.ID == item.User_ID && item.friend_ID == ID && item.Relation_Type == 1)
                    {
                        all_friends.Add(user);

                    }
                }
            }
            List<User> others = manage.select().ToList();

            foreach (var item in others)
            {
                foreach (var n in all_friends)
                {
                    if (item.ID == n.ID)
                    {
                        item.ID = 0;
                    }
                }
            }

            others = others.Where(x => x.ID != 0 && x.User_Name.Contains(name)).ToList();

            foreach (var item in relation.select().ToList())

            {

                foreach (var friend in others)
                {

                    if (item.User_ID == ID && item.friend_ID == friend.ID)
                    {

                        friend.User_type = "sent friend requist";
                    }
                    else if (item.User_ID == friend.ID && item.friend_ID == ID)
                    {
                        friend.User_type = "asked your accept";



                    }

                }
            }









            return Json(
    others.Select(x => new
    {
        ID = x.ID,
        User_Name = x.User_Name,
        Password = x.Password,
        User_type = x.User_type

        // Assigment of child fields
    }).Where(x => x.ID != ID), JsonRequestBehavior.AllowGet);
        }


        public ActionResult search(string name)
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);
            friend_Relations_manage relation = new friend_Relations_manage();
            List<friend_Relations> friends = relation.select().ToList();

            friends = friends.Where(x => x.User_ID == ID).ToList();

            User_Manager manage = new User_Manager();
            List<User> users = new List<chat_rooms.Models.User>();

            foreach (var item in friends)
            {
                foreach (var m in manage.select().ToList())
                {
                    if (m.ID == item.friend_ID && item.Relation_Type == 1)
                    {

                        users.Add(m);
                    }
                }

            }
            users = users.Where(x => x.User_Name.Contains(name)).ToList();


            List<User> all_friends = new List<chat_rooms.Models.User>();

            foreach (var user in users)
            {
                foreach (var item in relation.select().ToList())
                {
                    if (user.ID == item.User_ID && item.friend_ID == ID && item.Relation_Type == 1)
                    {
                        all_friends.Add(user);

                    }
                }
            }
            return Json(
    all_friends.Select(x => new
    {
        ID = x.ID,
        User_Name = x.User_Name,
        Password = x.Password

        // Assigment of child fields
    }).Where(x => x.ID != ID), JsonRequestBehavior.AllowGet);
        }










        public ActionResult get_all_friend()
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);
            friend_Relations_manage relation = new friend_Relations_manage();
            List<friend_Relations> friends = relation.select().ToList();

            friends = friends.Where(x => x.User_ID == ID).ToList();

            User_Manager manage = new User_Manager();
            List<User> users = new List<chat_rooms.Models.User>();

            foreach (var item in friends)
            {
                foreach (var m in manage.select().ToList())
                {
                    if (m.ID == item.friend_ID && item.Relation_Type == 1)
                    {

                        users.Add(m);
                    }
                }

            }
            List<User> all_friends = new List<chat_rooms.Models.User>();

            foreach (var user in users)
            {
                foreach (var item in relation.select().ToList())
                {
                    if (user.ID == item.User_ID && item.friend_ID == ID && item.Relation_Type == 1)
                    {
                        all_friends.Add(user);

                    }
                }
            }



            return Json(
    all_friends.Select(x => new
    {
        ID = x.ID,
        User_Name = x.User_Name,
        Password = x.Password

        // Assigment of child fields
    }).Where(x => x.ID != ID), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ADD_friend(int ID)
        {
            User_Manager manage = new User_Manager();
            User user = manage.Get_Item(ID);

            return Json(new
            {
                ID = user.ID,
                User_Name = user.User_Name,
                Password = user.Password


            }, JsonRequestBehavior.AllowGet);



        }
        [HttpPost]

        public void send_friend_requist(int ID)
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int u_ID = Convert.ToInt16(user_ID);



            friend_Relations_manage manage = new friend_Relations_manage();
            friend_Relations relation = new friend_Relations();
            relation.User_ID = u_ID;
            relation.Relation_Type = 1;
            relation.friend_ID = ID;
            manage.add(relation);


        }

        public ActionResult show_requist()
        {

            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int ID = Convert.ToInt16(user_ID);
            friend_Relations_manage relation = new friend_Relations_manage();
            List<friend_Relations> friends = relation.select().ToList();

            friends = friends.Where(x => x.User_ID == ID).ToList();

            User_Manager manage = new User_Manager();
            List<User> users = new List<chat_rooms.Models.User>();

            foreach (var item in friends)
            {
                foreach (var m in manage.select().ToList())
                {
                    if (m.ID == item.friend_ID && item.Relation_Type == 1)
                    {

                        users.Add(m);
                    }
                }

            }


            List<User> all_friends = new List<chat_rooms.Models.User>();

            foreach (var user in users)
            {
                foreach (var item in relation.select().ToList())
                {
                    if (user.ID == item.User_ID && item.friend_ID == ID && item.Relation_Type == 1)
                    {
                        all_friends.Add(user);

                    }
                }
            }
            List<User> others = manage.select().ToList();

            foreach (var item in others)
            {
                foreach (var n in all_friends)
                {
                    if (item.ID == n.ID)
                    {
                        item.ID = 0;
                    }
                }
            }
            List<User> reqisted_friend = new List<chat_rooms.Models.User>();


            foreach (var item in relation.select().ToList())

            {

                foreach (var friend in others)
                {

                    if (item.User_ID == friend.ID && item.friend_ID == ID)
                    {
                        reqisted_friend.Add(friend);


                    }

                }



            }


            return Json(
reqisted_friend.Select(x => new
{
    ID = x.ID,
    User_Name = x.User_Name,
    Password = x.Password,
    User_type = x.User_type

    // Assigment of child fields
}).Where(x => x.ID != ID), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public void ADD_to_your_friend(int ID)
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int u_ID = Convert.ToInt16(user_ID);



            friend_Relations_manage manage = new friend_Relations_manage();
            friend_Relations relation = new friend_Relations();
            relation.User_ID = u_ID;
            relation.Relation_Type = 1;
            relation.friend_ID = ID;
            manage.add(relation);


        }



        public ActionResult get_username()
        {
            string user_ID = System.Web.HttpContext.Current.Request.Cookies["user_server"].Value;
            user_ID = user_ID.Split('&')[2];
            user_ID = user_ID.Substring(3);
            int id = Convert.ToInt16(user_ID);
            User_Manager manage = new User_Manager();
            User user = manage.Get_Item(id);
            return Json(new { ID = id, User_Name=user.User_Name }, JsonRequestBehavior.AllowGet);

        }


    }
}
