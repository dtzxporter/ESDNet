using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESDNet.Messages
{
    internal class MOnMessage
    {
        [JsonProperty(PropertyName = "event")]
        public string Event { get; set; }
        [JsonProperty(PropertyName = "context")]
        public string Context { get; set; }
        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }
        [JsonProperty(PropertyName = "device")]
        public string Device { get; set; }
        [JsonProperty(PropertyName = "payload")]
        public MPayload Payload { get; set; }
        [JsonProperty(PropertyName = "deviceInfo")]
        public object DeviceInfo { get; set; }
    }
}
