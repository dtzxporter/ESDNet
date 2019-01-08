using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace ESDNet
{
    public class ESDConnection
    {
        private IESDPlugin PluginInstance = null;
        private WebSocket SocketInstance = null;
        private bool KeepRunning = true;
        private int Port = 0;
        private string UUID = string.Empty;
        private string RegisterEvent = string.Empty;
        private string Info = string.Empty;

        public ESDConnection(int Port, string UUID, string RegisterEvent, string Info, IESDPlugin Plugin)
        {
            Plugin.SetConnection(this);

            this.Port = Port;
            this.UUID = UUID;
            this.RegisterEvent = RegisterEvent;
            this.Info = Info;

            PluginInstance = Plugin;
        }

        public void Run()
        {
            using (SocketInstance = new WebSocket("ws://localhost:" + Port.ToString()))
            {
                SocketInstance.OnOpen += Ws_OnOpen;
                SocketInstance.OnMessage += Ws_OnMessage;
                SocketInstance.OnError += Ws_OnError;
                SocketInstance.OnClose += Ws_OnClose;

                SocketInstance.Connect();

                while (KeepRunning)
                    System.Threading.Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Sets the display title for this context
        /// </summary>
        /// <param name="Title">The title to change to</param>
        /// <param name="Context">The context to change</param>
        /// <param name="Target">Which targets to apply the changes to</param>
        public void SetTitle(string Title, string Context, Messages.ESDSDKTarget Target)
        {
            var Event = new Messages.MEvent()
            {
                Event = "setTitle",
                Context = Context,
                Payload = new Messages.MPayload()
                {
                    Target = Target,
                    Title = Title
                }
            };

            SocketInstance.Send(Newtonsoft.Json.JsonConvert.SerializeObject(Event));
        }

        /// <summary>
        /// Sets the display image for this context
        /// </summary>
        /// <param name="ImagePath">The path of the image file, as a PNG</param>
        /// <param name="Context">The context to change</param>
        /// <param name="Target">Which targets to apply the changes to</param>
        public void SetImage(string ImagePath, string Context, Messages.ESDSDKTarget Target)
        {
            var Event = new Messages.MEvent()
            {
                Event = "setImage",
                Context = Context,
                Payload = new Messages.MPayload()
                {
                    Target = Target,
                    Image = "data:image/png;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(ImagePath))
                }
            };

            SocketInstance.Send(Newtonsoft.Json.JsonConvert.SerializeObject(Event));
        }

        /// <summary>
        /// Shows the ALERT box on the given context
        /// </summary>
        /// <param name="Context">The context to change</param>
        public void ShowAlertForContext(string Context)
        {
            var Event = new Messages.MEvent()
            {
                Event = "showAlert",
                Context = Context
            };

            SocketInstance.Send(Newtonsoft.Json.JsonConvert.SerializeObject(Event));
        }

        /// <summary>
        /// Shows the OK checkbox on the given context
        /// </summary>
        /// <param name="Context">The context to change</param>
        public void ShowOKForContext(string Context)
        {
            var Event = new Messages.MEvent()
            {
                Event = "showOk",
                Context = Context
            };

            SocketInstance.Send(Newtonsoft.Json.JsonConvert.SerializeObject(Event));
        }

        /// <summary>
        /// Changes the state for the given context
        /// </summary>
        /// <param name="State">The state value</param>
        /// <param name="Context">The context to change</param>
        public void SetState(int State, string Context)
        {
            var Event = new Messages.MEvent()
            {
                Event = "setState",
                Context = Context,
                Payload = new Messages.MPayload()
                {
                    State = State
                }
            };

            SocketInstance.Send(Newtonsoft.Json.JsonConvert.SerializeObject(Event));
        }

        #region Events
        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            KeepRunning = false;
        }

        private void Ws_OnError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show("ERROR: " + e.Message);
            KeepRunning = false;
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsText)
            {
                try
                {
                    var Msg = Newtonsoft.Json.JsonConvert.DeserializeObject<Messages.MOnMessage>(e.Data);

                    switch (Msg.Event)
                    {
                        case "keyDown":
                            PluginInstance.KeyDownForAction(Msg.Action, Msg.Context, Msg.Payload, Msg.Device);
                            break;
                        case "keyUp":
                            PluginInstance.KeyUpForAction(Msg.Action, Msg.Context, Msg.Payload, Msg.Device);
                            break;
                        case "willAppear":
                            PluginInstance.WillAppearForAction(Msg.Action, Msg.Context, Msg.Payload, Msg.Device);
                            break;
                        case "willDisappear":
                            PluginInstance.WillDisappearForAction(Msg.Action, Msg.Context, Msg.Payload, Msg.Device);
                            break;
                        case "deviceDidConnect":
                            PluginInstance.DeviceDidConnect(Msg.Device, Msg.DeviceInfo.ToString());
                            break;
                        case "deviceDidDisconnect":
                            PluginInstance.DeviceDidDisconnect(Msg.Device);
                            break;
                    }
                }
                catch
                {
                    // Nothing...
                }
            }
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
            ((WebSocket)sender).Send(Newtonsoft.Json.JsonConvert.SerializeObject(new Messages.MOnOpen()
            {
                UUID = this.UUID,
                RegisterEvent = this.RegisterEvent
            }));
        }
        #endregion
    }
}
