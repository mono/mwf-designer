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
		AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs args) {
			System.Windows.Forms.MessageBox.Show (args.ExceptionObject.GetType ().Name);
			if (args.ExceptionObject is Exception)
				OnException ((Exception)args.ExceptionObject);
		};

		try {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainView());
		} catch (Exception e) {
			OnException (e);
			System.Windows.Forms.Application.Exit ();
		}
        }

	private static void OnException (Exception e)
	{
		MessageBox.Show ("A fatal error occured. Please file a bug report with the following details (Ctrl-C to copy to clipboard):" + 
				 System.Environment.NewLine + System.Environment.NewLine +
				 e.ToString (), "Fatal Error");
	}
    }
}