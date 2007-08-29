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