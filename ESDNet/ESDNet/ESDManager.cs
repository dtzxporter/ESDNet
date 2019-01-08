using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESDNet
{
    public class ESDManager
    {
        /// <summary>
        /// Must be called from the Program.Main(string[] args); to initialize the library.
        /// </summary>
        /// <param name="args">The arguments passed to the plugin at runtime.</param>
        public static void Initialize(string[] args, IESDPlugin Plugin)
        {
            int Port = 0;
            string PluginUUID = string.Empty;
            string RegisterEvent = string.Empty;
            string PluginInfo = string.Empty;

            if (args.Length < 4)
            {
                MessageBox.Show("ERROR: Missing args");
                return;
            }

            // Parse command line arguments for the port, uuid, event, and info params
            for (int arg = 0; arg < 4; arg++)
            {
                string Param = args[2 * arg];
                string Value = args[2 * arg + 1];

                if (Param == "-port")
                {
                    Port = Convert.ToInt32(Value);
                }
                else if (Param == "-pluginUUID")
                {
                    PluginUUID = Value;
                }
                else if (Param == "-registerEvent")
                {
                    RegisterEvent = Value;
                }
                else if (Param == "-info")
                {
                    PluginInfo = Value;
                }
            }

            if (Port == 0)
            {
                MessageBox.Show("ERROR: Port number is invalid!");
                throw new Exception("ERROR: Port number is invalid!");
            }

            if (PluginUUID == string.Empty)
            {
                MessageBox.Show("ERROR: PluginUUID is invalid!");
                throw new Exception("ERROR: PluginUUID is invalid!");
            }

            if (RegisterEvent == string.Empty)
            {
                MessageBox.Show("ERROR: RegisterEvent is invalid!");
                throw new Exception("ERROR: RegisterEvent is invalid!");
            }

            if (PluginInfo == string.Empty)
            {
                MessageBox.Show("ERROR: PluginInfo is invalid!");
                throw new Exception("ERROR: PluginInfo is invalid!");
            }

            // Execute the plugin instance
            new ESDConnection(Port, PluginUUID, RegisterEvent, PluginInfo, Plugin).Run();
        }
    }
}
