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
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace mwf_designer
{
	internal class ErrorListTabPage : TabPage, IUIService
	{
		private ErrorList _errorList;
		private readonly string ERRORS_TAB_KEY = @"!?\/#__Errors__!?\/#";
		private readonly string ERRORS_TAB_TEXT = "Errors";

		public ErrorListTabPage ()
		{
			InitializeComponent ();
		}
	
		private void InitializeComponent ()
		{
			_errorList = new ErrorList ();
			_errorList.Dock = DockStyle.Fill;

			this.Name = ERRORS_TAB_KEY;
			this.Text = ERRORS_TAB_TEXT;
			this.Controls.Add (_errorList);
		}

#region IUIService

		private IDictionary _styles;

		void IUIService.SetUIDirty ()
		{
			_errorList.Clear ();
		}

		void IUIService.ShowError (Exception exception)
		{
			string details = exception.StackTrace;

			if (exception.Data["Details"] != null)
				details = (string)exception.Data["Details"] + System.Environment.NewLine + System.Environment.NewLine + details;
			_errorList.AddError (exception.Message, details);
		}

		void IUIService.ShowError (string message)
		{
			_errorList.AddError (message, Environment.StackTrace);
		}

		void IUIService.ShowError (Exception exception, string message)
		{
			_errorList.AddError (message, exception.ToString ());
		}

		void IUIService.ShowMessage (string message)
		{
			_errorList.AddError (message, Environment.StackTrace);
		}

		void IUIService.ShowMessage (string message, string caption)
		{
			_errorList.AddError (caption, message);
		}

		DialogResult IUIService.ShowMessage (string message, string caption, MessageBoxButtons buttons)
		{
			throw new NotImplementedException ();
		}

		IDictionary IUIService.Styles {
			get {
				if (_styles == null)
					_styles = new Hashtable ();
				return _styles;
			}
		}

		IWin32Window IUIService.GetDialogOwnerWindow ()
		{
			Control parent = this.Parent;

			while (parent != null) {
				if (parent is Form)
					return (Form)parent;
				parent = parent.Parent;
			}

			return null;
		}

		bool IUIService.CanShowComponentEditor (object component)
		{
			return false; // TODO
			// throw new NotImplementedException ();
		}

		bool IUIService.ShowComponentEditor (object component, IWin32Window parent)
		{
			throw new NotImplementedException ();
		}

		DialogResult IUIService.ShowDialog (Form form)
		{
			throw new NotImplementedException ();
		}

		bool IUIService.ShowToolWindow (Guid toolWindow)
		{
			throw new NotImplementedException ();
		}
#endregion

	}
}
