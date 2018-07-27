using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ChatApplication.Models.ChatModel
{
    public class ChatContextInitializer : DropCreateDatabaseAlways<ChatContext>
    {
        //for dropping and creating empty datatbase everytime after launching the app
        protected override void Seed(ChatContext context)
        {

        }
    }
}