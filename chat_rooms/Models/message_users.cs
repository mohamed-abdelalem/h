using chat_rooms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chat_rooms.Models
{
    public class message_users
    {
        List<User> Users;
        List<message> message;

        public List<User> Users1
        {
            get
            {
                return Users;
            }

            set
            {
                Users = value;
            }
        }

        public List<message> Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
    }
}