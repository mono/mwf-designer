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