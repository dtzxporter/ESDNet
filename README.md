# ESDNet
A framework for writing Elgato Stream Deck plugins with the .NET framework.

## Usage:
- Create a new c# WinForms project.
- Add a reference to ESDNet.dll.
- Delete the form and clear out Program.Main() to be:
``` csharp
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
```
- Replace `TestPlugin` with the name of the class that you created to implement the IESDPlugin interface.
- Follow the standard Elgato plugin tutorial for building resources and directories.
  - You must ensure that you copy `ESDNet.dll`, `Newtonsoft.Json.dll`, and `websocket-sharp.dll` to the plugin folder as well.
- See `ESDTestPlugin` for an example in `C#` that emulates the Elgato CPU test plugin.