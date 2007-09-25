using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace mwf_designer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			try {
				Application.Run(new MainView());
			} catch (Exception e) {
				MessageBox.Show ("A fatal error occured. Please file a bug report with the following details (Ctrl-C to copy to clipboard):" + 
								 System.Environment.NewLine + System.Environment.NewLine +
								 e.ToString (), "Fatal Error");
				System.Windows.Forms.Application.Exit ();
			}
        }
    }
}