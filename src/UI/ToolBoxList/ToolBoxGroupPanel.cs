//
// Authors:	 
//	  Jonathan Pobst (monkey@jpobst.com>)
//
// (C) 2007 Jonathan Pobst

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
using System.Drawing;
using System.Windows.Forms;

namespace mwf_designer 
{
	public class ToolBoxGroupPanel : UserControl 
	{

#region Fields
		private ToolBoxGroupHeader GroupHeader1;
		private Panel ItemPanel;
#endregion

#region Public Constructor
		public ToolBoxGroupPanel ()
		{
			ItemPanel = new Panel ();
			GroupHeader1 = new ToolBoxGroupHeader ();
			SuspendLayout ();

			//
			// ItemPanel
			//
			ItemPanel.Dock = DockStyle.Fill;
			ItemPanel.Location = new Point (1, 21);
			ItemPanel.Name = "ItemPanel";
			ItemPanel.Size = new Size (198, 0);
			ItemPanel.TabIndex = 1;
			//
			// GroupHeader1
			//
			GroupHeader1.Dock = DockStyle.Top;
			GroupHeader1.Expanded = true;
			GroupHeader1.Location = new Point (1, 1);
			GroupHeader1.Margin = new Padding (0);
			GroupHeader1.Name = "GroupHeader1";
			GroupHeader1.Padding = new Padding (1, 1, 1, 0);
			GroupHeader1.Size = new Size (198, 20);
			GroupHeader1.TabIndex = 0;

			Controls.Add (this.ItemPanel);
			Controls.Add (this.GroupHeader1);
			Name = "ToolBoxGroupPanel";
			Padding = new System.Windows.Forms.Padding (1, 1, 1, 0);
			Size = new System.Drawing.Size (200, 20);
			ResumeLayout (false);

			GroupHeader1.ExpandedChanged += new EventHandler (GroupHeader1_ExpandedChanged);
			ItemPanel.ControlAdded += new ControlEventHandler (ItemPanel_ControlAdded);
			Layout += new LayoutEventHandler (ToolBoxGroupPanel_Layout);
		}
#endregion

#region Public Properties
		public bool Expanded {
			get { return GroupHeader1.Expanded;}
			set {
				if (GroupHeader1.Expanded != value) {
					GroupHeader1.Expanded = value;
					PerformLayout ();
				}
			}
		}

		public ControlCollection Items {
			get { return ItemPanel.Controls;}
		}

		public override string Text {
			get { return GroupHeader1.Text;}
			set { GroupHeader1.Text = value;}
		}
#endregion

#region Private Methods
		private void ToolBoxGroupPanel_Layout (object sender, LayoutEventArgs e)
		{
			// Figure out how tall we need to be
			if (GroupHeader1.Expanded) {
				int y = 0;

				foreach (Control c in Items)
					if (c.Enabled)
						y += c.Height;

				if (y == 0)
					Visible = false;
				else
					Visible	= true;

				Height = y + GroupHeader1.Height + 1;
			} else {
				Height = GroupHeader1.Bottom;
			}
		}

		private void ItemPanel_ControlAdded (object sender, ControlEventArgs e)
		{
			// Sort incoming items
			ToolBoxListItem new_tbi = (ToolBoxListItem)e.Control;

			foreach (Control c in ItemPanel.Controls) {
				ToolBoxListItem tbi = c as ToolBoxListItem;

				if (tbi == null)
					continue;

				if (string.Compare (new_tbi.Text, tbi.Text) > 0) {
					ItemPanel.Controls.SetChildIndex (new_tbi, ItemPanel.Controls.GetChildIndex (tbi));
					break;
				}
			}

			e.Control.Dock = DockStyle.Top;
			PerformLayout ();
		}

		private void GroupHeader1_ExpandedChanged (object sender, EventArgs e)
		{
			PerformLayout ();
		}
#endregion
	}
}
