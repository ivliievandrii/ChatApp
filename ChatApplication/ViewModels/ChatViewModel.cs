using ChatApplication.Models.ChatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.ViewModels
{
    public class ChatViewModel
    {
        public List<ChatUser> Users { get; set; }

        public List<ChatMessage> Messages { get; set; }
    }
}