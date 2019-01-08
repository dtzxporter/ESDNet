using ESDNet;
using ESDNet.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESDTestPlugin
{
    class TestPlugin : IESDPlugin
    {
        // A local reference to the current connection
        private ESDConnection _Connection = null;
        private System.Timers.Timer _UpdateTimer = null;
        private PerformanceCounter _CPUReader = null;

        // Keep the list of visible contexts in sync
        private object _SyncRoot = new object();
        private HashSet<string> _VisibleContexts = null;

        public TestPlugin()
        {
            _CPUReader = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _VisibleContexts = new HashSet<string>();

            _UpdateTimer = new System.Timers.Timer(1000);
            _UpdateTimer.Elapsed += _UpdateTimer_Elapsed;
            _UpdateTimer.Start();
        }

        private void _UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Update the CPU usage...
            var Title = (int)_CPUReader.NextValue() + "%";

            foreach (var Context in _VisibleContexts)
            {
                _Connection.SetTitle(Title, Context, ESDSDKTarget.ESDSDKTarget_HardwareAndSoftware);
            }
        }

        public void SetConnection(ESDConnection Connection)
        {
            // Keep a copy of the ESD connection.
            _Connection = Connection;
        }

        public void KeyDownForAction(string Action, string Context, MPayload Payload, string DeviceID)
        {
        }

        public void KeyUpForAction(string Action, string Context, MPayload Payload, string DeviceID)
        {
        }

        public void WillAppearForAction(string Action, string Context, MPayload Payload, string DeviceID)
        {
            lock(_SyncRoot)
            {
                _VisibleContexts.Add(Context);
            }
        }

        public void WillDisappearForAction(string Action, string Context, MPayload Payload, string DeviceID)
        {
            lock(_SyncRoot)
            {
                _VisibleContexts.Remove(Context);
            }
        }

        public void DeviceDidConnect(string DeviceID, string DeviceInfo)
        {
        }

        public void DeviceDidDisconnect(string DeviceID)
        {
        }
    }
}
