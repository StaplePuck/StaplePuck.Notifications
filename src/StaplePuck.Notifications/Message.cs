using System;
using System.Collections.Generic;
using System.Text;

namespace StaplePuck.Notifications
{
    public class Message
    {
        public string[] registration_ids { get; set; } = Array.Empty<string>();
        public Notification notification { get; set; } =  new Notification();
        public object data { get; set; } = new object();
    }
}
