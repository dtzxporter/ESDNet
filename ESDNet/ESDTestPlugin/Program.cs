using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESDTestPlugin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Tell the library our command line arguments, and pass an instance of our plugin.
            ESDNet.ESDManager.Initialize(args, new TestPlugin());
        }
    }
}
