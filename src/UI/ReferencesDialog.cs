using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace mwf_designer
{
	internal partial class ReferencesDialog : Form
	{
		private References _references;

		public ReferencesDialog (References references)
		{
			if (references == null)
				throw new ArgumentNullException ("references");
			_references = references;

			InitializeComponent ();
			PopulateReferencesList ();
		}

		private void PopulateReferencesList ()
		{
			referencesList.Items.AddRange (_references.FileNames);
		}

		private void done_Click (object sender, EventArgs e)
		{
			this.Close ();
		}

		private void add_Click (object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog ();
			dialog.CheckFileExists = true;
			dialog.Multiselect = true;
			dialog.Filter = ".Net Assembly (*.dll)|*.dll";
			if (dialog.ShowDialog () == DialogResult.OK) {
				foreach (string file in dialog.FileNames) {
					if (_references.AddReference (file))
						referencesList.Items.Add (Path.GetFileName (file));
				}
			}
			dialog.Dispose ();
		}

		private void remove_Click (object sender, EventArgs e)
		{
			if (referencesList.SelectedIndex != -1) {
				_references.RemoveReference ((string)referencesList.Items[referencesList.SelectedIndex]);
				referencesList.Items.RemoveAt (referencesList.SelectedIndex);
			}
		}
	}
}