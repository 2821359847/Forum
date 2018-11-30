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
    public class HomeController : Controller
    {
        PostingDBContext context;
        PostingData postingData;

        public HomeController()
        {
            context = new PostingDBContext();
            postingData = new PostingData(context);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var postinglist = new PostingList
            {
                Postings = postingData.GetAll()
            };
            return View(postinglist);
        }

        [AllowAnonymous]
        public ViewResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            Posting posting = new Posting();
            return View(posting);
        }

        [HttpPost]
        public IActionResult Create(string title, string body)
        {
            Posting posting = new Posting();
            posting.Title = title;
            posting.Body = body;
            posting.Poster = User.Identity.Name;
            posting.Posttime = DateTime.Now;
            postingData.Add(posting);
            return RedirectToRoute(new { controller = "Home", action = "Index"});
        }
    }

    public class PostingData
    {
        private PostingDBContext _context { get; set; }

        public PostingData(PostingDBContext context)
        {
            _context = context;
        }

        public void Add(Posting posting)
        {
            _context.Add(posting);
            _context.SaveChanges();
        }

        public Posting Get(int id)
        {
            return _context.Postings.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Posting> GetAll()
        {
            return _context.Postings.ToList<Posting>();
        }
    }

    public class PostingList
    {
        public IEnumerable<Posting> Postings { set; get; }
    }
}