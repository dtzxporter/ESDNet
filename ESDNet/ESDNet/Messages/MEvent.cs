using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESDNet.Messages
{
    internal class MEvent
    {
        [JsonProperty(PropertyName = "event")]
        public string Event { get; set; }
        [JsonProperty(PropertyName = "context")]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "payload")]
        public object Payload { get; set; }
    }
}
