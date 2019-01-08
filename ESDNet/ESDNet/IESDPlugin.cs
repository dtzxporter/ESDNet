using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESDNet
{
    public interface IESDPlugin
    {
        void SetConnection(ESDConnection Connection);

        void KeyDownForAction(string Action, string Context, Messages.MPayload Payload, string DeviceID);
        void KeyUpForAction(string Action, string Context, Messages.MPayload Payload, string DeviceID);

        void WillAppearForAction(string Action, string Context, Messages.MPayload Payload, string DeviceID);
        void WillDisappearForAction(string Action, string Context, Messages.MPayload Payload, string DeviceID);

        void DeviceDidConnect(string DeviceID, string DeviceInfo);
        void DeviceDidDisconnect(string DeviceID);
    }
}
