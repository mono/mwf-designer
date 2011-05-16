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
		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs ueargs) {
				System.Windows.Forms.MessageBox.Show (ueargs.ExceptionObject.GetType ().Name);
				if (ueargs.ExceptionObject is Exception)
					OnException ((Exception) ueargs.ExceptionObject);
			};

			try {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainView(args));
			} catch (Exception e) {
				OnException (e);
				System.Windows.Forms.Application.Exit ();
			}
		}

		private static void OnException (Exception e)
		{
			MessageBox.Show ("A fatal error occurred. Please file a bug report with the following details (Ctrl-C to copy to clipboard):" + System.Environment.NewLine + System.Environment.NewLine + e.ToString (), "Fatal Error");
		}
	}
}
