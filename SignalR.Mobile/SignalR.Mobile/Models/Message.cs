using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR.Backend
{
    public class Message
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        [JsonIgnore]
        public string MetaData => $"{Author} sent at {Timestamp.ToString("hh:mm dd.MM.yyyy")}";
    }
}
