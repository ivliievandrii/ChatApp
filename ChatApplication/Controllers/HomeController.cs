﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ChatApplication.Models.ChatModel;
using ChatApplication.ViewModels;

namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        // db context
        private ChatContext db = new ChatContext();
        // viewmodel to pass users/messages arrays as one object to views
        private ChatViewModel viewModel;

        public ActionResult Index(string userLogin, string userEmail, bool? logon, bool? logoff, string message)
        {
            try
            {
                // determines request made using ajax/jquery or not
                if (!Request.IsAjaxRequest())
                {
                    viewModel = new ChatViewModel()
                    {
                        Messages = db.Messages.ToList(),
                        Users = db.Users.ToList()
                    };
                    return View(viewModel);
                }
                // determines if user is about to quit
                else if (logoff != null && (bool)logoff)
                {
                    ChatUser user = db.Users.FirstOrDefault(u => (u.Login == userLogin) && (u.Email == userEmail));
                    user.Online = false;
                    db.Messages.Add(new ChatMessage()
                    {
                        Text = user.Login + " has left chat",
                        User = user,
                        PostTime = DateTime.Now
                    });
                    db.Entry<ChatUser>(user).State = EntityState.Modified;
                    db.SaveChanges();
                    viewModel = new ChatViewModel()
                    {
                        Messages = db.Messages.ToList(),
                        Users = db.Users.ToList()
                    };
                    return PartialView("ChatBox", viewModel);
                }
                // determines if user is about to enter chat
                else if (logon != null && (bool)logon)
                {
                    var users = db.Users.ToList();
                    int usersOnline = users.Where(u => u.Online == true).ToList().Count;
                    if (usersOnline >= 10)
                    {
                        throw new Exception("Chat is full");
                    }
                    else if (users.FirstOrDefault(u => (u.Login == userLogin) && (u.Email == userEmail)) != null)
                    {
                        throw new Exception("User with such login and email already exists. Choose another login/email");
                    }
                    else if (users.FirstOrDefault(u => u.Login == userLogin) != null)
                    {
                        throw new Exception("User with such login already exists. Choose another login");
                    }
                    // checking for user with the same email
                    else if (users.FirstOrDefault(u => u.Email == userEmail) != null)
                    {
                        throw new Exception("User with such email already exists. Choose another email");
                    }
                    // adding user to chat
                    else
                    {
                        ChatUser newUser = new ChatUser()
                        {
                            Login = userLogin,
                            Email = userEmail,
                            LoginTime = DateTime.Now,
                            Online = true
                        };
                        db.Messages.Add(new ChatMessage()
                        {
                            Text = userLogin + " entered the chat",
                            User = newUser,
                            PostTime = DateTime.Now
                        });
                        db.Users.Add(newUser);
                        db.SaveChanges();
                    }
                    viewModel = new ChatViewModel()
                    {
                        Messages = db.Messages.ToList(),
                        Users = db.Users.ToList()
                    };
                    return PartialView("ChatBox", viewModel);
                }
                // adding new message to chat
                else
                {
                    if (!string.IsNullOrEmpty(message))
                    {
                        var currentUser = db.Users.FirstOrDefault
                        (u => (u.Login == userLogin) && (u.Email == userEmail));
                        db.Messages.Add(new ChatMessage()
                        {
                            User = currentUser,
                            Text = message,
                            PostTime = DateTime.Now
                        });
                        db.SaveChanges();
                    }
                    viewModel = new ChatViewModel()
                    {
                        Messages = db.Messages.ToList(),
                        Users = db.Users.ToList()
                    };
                    return PartialView("History", viewModel);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }
    }
}