using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace chat_rooms.Models
{
    public partial class User
    {
        string user_type;

        public string User_type
        {
            get
            {
                return user_type;
            }

            set
            {
                user_type = value;
            }
        }
    }
}