using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESDNet.Messages
{
    public enum ESDSDKTarget : int
    {
        ESDSDKTarget_HardwareAndSoftware = 0,
        ESDSDKTarget_HardwareOnly = 1,
        ESDSDKTarget_SoftwareOnly = 2
    }

    public class MPayload
    {
        [JsonProperty(PropertyName = "target")]
        public ESDSDKTarget Target { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
        [JsonProperty(PropertyName = "state")]
        public int State { get; set; }
    }
}
