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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mwf_designer
{
	internal partial class NewFileDialog : Form
	{
		private string _templateName;
		private string _fileName;
		private string _class;
		private string _namespace;

		public NewFileDialog()
		{
			InitializeComponent();
		}

		public NewFileDialog(string[] templateNames) : this ()
		{
			templatesListbox.Items.AddRange (templateNames);
			if (templatesListbox.Items.Count >= 1)
				templatesListbox.SelectedIndex = 0;
		}

		public string Class {
			get { return _class; }
		}

		public string File {
			get { return _fileName; }
		}


		public string Namespace {
			get { return _namespace; }
		}

		public string Template {
			get { return _templateName; }
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
			dialog.Filter = "C# Source Code (*.cs)|*.cs| VB.NET Source Code (*.vb)|*.vb|Other|*.*";
			if (dialog.ShowDialog() == DialogResult.OK)
				filenameTextbox.Text = dialog.FileName;
		}

		private void doneButton_Click(object sender, EventArgs e)
		{
			_templateName = (string)templatesListbox.SelectedItem;
			_namespace = namespaceTextbox.Text;
			_class = classTextbox.Text;
			_fileName = filenameTextbox.Text;
			if (_templateName == null || _namespace == null || _class == null || _fileName == null) {
				MessageBox.Show ("Please select a template from the list, specify the class and namespace names and " +
					"then choose file name and location.");
			} else {
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}
	}
}