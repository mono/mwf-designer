//
// Authors:	 
//	  Ivan N. Zlatev (contact i-nZ.net)
//
// (C) 2007 Ivan N. Zlatev

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace mwf_designer
{
	internal partial class ErrorList : UserControl
	{

		private class Error
		{
			private string _details;
			private string _message;

			public Error (string message, string details)
			{
				if (message == null)
					throw new ArgumentNullException ("message");
				_details = details;
				_message = message;
			}

			public string Details {
				get { return _details; }
				set { _details = value; }
			}

			public string Message {
				get { return _message; }
				set { _message = value; }
			}

			public override string ToString ()
			{
				return _message;
			}
		}

		public ErrorList ()
		{
			InitializeComponent ();
		}

		private void saveButton_Click (object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
			if (dialog.ShowDialog() == DialogResult.OK) 
				WriteErrorsToFile (dialog.FileName);
		}

		private void errorsList_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (_errorsList.SelectedIndex != -1) {
				Error error = (Error) _errorsList.Items[_errorsList.SelectedIndex];
				_detailedText.Text = error.Details;
			}
		}

		public void AddError (string message, string details)
		{
			_errorsList.Items.Add (new Error (message, details));
		}

		private void WriteErrorsToFile (string fileName)
		{
			StringBuilder sb = new StringBuilder ();
			foreach (Error error in _errorsList.Items) {
				sb.AppendLine ("==== " + error.Message + " ====");
				sb.Append (Environment.NewLine);
				sb.Append (error.Details);
				sb.Append (Environment.NewLine);
				sb.Append (Environment.NewLine);
			}

			FileStream stream = File.OpenWrite (fileName);
			byte [] bytes = Encoding.Default.GetBytes (sb.ToString ());
			stream.Write (bytes, 0, bytes.Length);
			stream.Close ();
			stream.Dispose ();
		}

		public void Clear ()
		{
			_errorsList.SuspendLayout ();
			_errorsList.Items.Clear ();
			_errorsList.ResumeLayout ();
			_detailedText.Text = "";
		}
	}
}
