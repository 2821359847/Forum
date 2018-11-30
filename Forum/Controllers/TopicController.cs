using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;

namespace Forum.Controllers
{
    [Authorize]
    public class TopicController : Controller
    {
        PostingDBContext context;
        MessageData messageData;

        public TopicController()
        {
            context = new PostingDBContext();
            messageData = new MessageData(context);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Info(int id)
        {
            TopicViewModel topicViewModel = new TopicViewModel();
            topicViewModel.posting = context.Postings.FirstOrDefault(e => e.ID == id);
            topicViewModel.messages = messageData.Get(id);
            return View(topicViewModel);
        }

        [HttpPost]
        public IActionResult Info(int id, string postMessage)
        {
            TopicViewModel topicViewModel = new TopicViewModel();
            topicViewModel.posting = context.Postings.FirstOrDefault(e => e.ID == id);
            topicViewModel.messages = messageData.Get(id);
            if (postMessage != null && postMessage.Replace(" ","") != "")
            {
                Message message = new Message();
                message.TopicId = id;
                message.FloorId = topicViewModel.messages.Count() + 1;
                message.Text = postMessage;
                message.Messager = User.Identity.Name;
                message.MessageTime = DateTime.Now;
                messageData.Add(message);
                topicViewModel.posting = context.Postings.FirstOrDefault(e => e.ID == id);
                topicViewModel.messages = messageData.Get(id);
            }
            return View(topicViewModel);
        }
    }

    public class MessageData
    {
        private PostingDBContext _context { get; set; }

        public MessageData(PostingDBContext context)
        {
            _context = context;
        }

        public void Add(Message message)
        {
            _context.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<Message> Get(int id)
        {
            IEnumerable<Message> messages = from m in _context.Messages
                                               where m.TopicId == id
                                               orderby m.MessageTime descending
                                               select m;
            return messages;
        }
    }

    public class TopicViewModel
    {
        public Posting posting { set; get; }
        public IEnumerable<Message> messages { set; get; }
        public string PostMessage { set; get; }
    }
}