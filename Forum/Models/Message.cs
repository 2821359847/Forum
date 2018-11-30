using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class Message
    {
        public int MessageId { set; get; }
        public int TopicId { set; get; }
        public int FloorId { set; get; }
        public string Text { set; get; }
        public string Messager { set; get; }
        public DateTime MessageTime { set; get; }

        public Message() { }

        public Message(int messageId, int topicId, int floorId, string text, string messager, DateTime messageTime)
        {
            this.MessageId = messageId;
            this.TopicId = topicId;
            this.FloorId = floorId;
            this.Text = text;
            this.Messager = messager;
            this.MessageTime = messageTime;
        }
    }
}
