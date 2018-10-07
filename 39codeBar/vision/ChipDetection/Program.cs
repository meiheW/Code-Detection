using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChipDetection
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
              
            bool isResetConfiguration = false;
            if (args.Length == 1 && args[0] == "1")
            {
                isResetConfiguration = true;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormChipDetection(isResetConfiguration));
        }
    }
}
