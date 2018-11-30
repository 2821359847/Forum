using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class Posting
    {
        public int ID { set; get; }
        public string Title { set; get; }
        public string Body { set; get; }
        public string Poster { set; get; }
        public DateTime Posttime { set; get; }

        public Posting() { }

        public Posting(int id, string title, string body, string poster, DateTime posttime)
        {
            this.ID = id;
            this.Title = title;
            this.Body = body;
            this.Poster = poster;
            this.Posttime = posttime;
        }
    }
}
