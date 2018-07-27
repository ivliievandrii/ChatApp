using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.Models.ChatModel
{
    public class ChatUser
    {
        public int ChatUserId { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public DateTime LoginTime { get; set; }

        public bool Online { get; set; }
    }
}