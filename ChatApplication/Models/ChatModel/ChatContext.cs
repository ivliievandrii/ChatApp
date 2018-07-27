namespace ChatApplication.Models.ChatModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ChatContext : DbContext
    {
        public ChatContext()
            : base("name=ChatContext")
        {
        }

        public DbSet<ChatUser> Users { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }
    }


}