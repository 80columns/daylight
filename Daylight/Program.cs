using Daylight.context;
using Daylight.window;
using System;
using System.IO;
using System.Windows.Forms;

namespace Daylight {
    static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // set the current working directory to the application's path
            Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DefaultHiddenApplicationContext(new DaylightWindow()));
        }
    }
}