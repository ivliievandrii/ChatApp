using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.Models.ChatModel
{
    public class ChatMessage
    {
        public int Id { get; set; }

        public ChatUser User { get; set; }

        public DateTime PostTime { get; set; }

        public string Text { get; set; }
    }
}