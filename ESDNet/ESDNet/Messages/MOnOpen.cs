using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESDNet.Messages
{
    internal class MOnOpen
    {
        [JsonProperty(PropertyName = "uuid")]
        public string UUID { get; set; }
        [JsonProperty(PropertyName = "event")]
        public string RegisterEvent { get; set; }
    }
}
