using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace mwf_designer
{
	internal partial class Toolbox : UserControl, IToolboxService
	{

		public Toolbox ()
		{
			InitializeComponent ();
			this._toolbox.SelectedIndexChanged += OnToolbox_SelectedIndexChanged;
		}

		private void OnToolbox_SelectedIndexChanged (object sender, EventArgs args)
		{
			if (_toolbox.SelectedIndex != -1) {
				if (ToolPicked != null)
					ToolPicked (this, EventArgs.Empty);
			}
		}

		public event EventHandler ToolPicked;

		public void Clear ()
		{
			_toolbox.Items.Clear ();
		}

#region IToolboxService implementation

		public System.Drawing.Design.CategoryNameCollection CategoryNames {
			get { return null; }
		}

		public string SelectedCategory {
			get { return null; }
			set  { }
		}

		public void AddCreator (System.Drawing.Design.ToolboxItemCreatorCallback creator, string format, System.ComponentModel.Design.IDesignerHost host)
		{
		}

		public void AddCreator (System.Drawing.Design.ToolboxItemCreatorCallback creator, string format)
		{
		}

		public void AddLinkedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category, System.ComponentModel.Design.IDesignerHost host)
		{
		}

		public void AddLinkedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, System.ComponentModel.Design.IDesignerHost host)
		{
		}

		public void AddToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category)
		{
			this.AddToolboxItem (toolboxItem);
		}

		public void AddToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
			_toolbox.Items.Add (toolboxItem);
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem (object serializedObject, System.ComponentModel.Design.IDesignerHost host)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItem DeserializeToolboxItem (object serializedObject)
		{
			return null;
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem (System.ComponentModel.Design.IDesignerHost host)
		{
			return this.GetSelectedToolboxItem ();
		}

		public System.Drawing.Design.ToolboxItem GetSelectedToolboxItem ()
		{
			return (ToolboxItem)_toolbox.SelectedItem;
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (string category, System.ComponentModel.Design.IDesignerHost host)
		{
			return this.GetToolboxItems ();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (string category)
		{
			return this.GetToolboxItems ();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems (System.ComponentModel.Design.IDesignerHost host)
		{
			return this.GetToolboxItems ();
		}

		public System.Drawing.Design.ToolboxItemCollection GetToolboxItems ()
		{
			ToolboxItem[] items = new ToolboxItem[_toolbox.Items.Count];
			_toolbox.Items.CopyTo (items, 0);
			return new ToolboxItemCollection (items);
		}

		public bool IsSupported (object serializedObject, System.Collections.ICollection filterAttributes)
		{
			return false;
		}

		public bool IsSupported (object serializedObject, System.ComponentModel.Design.IDesignerHost host)
		{
			return false;
		}

		public bool IsToolboxItem (object serializedObject, System.ComponentModel.Design.IDesignerHost host)
		{
			return false;
		}

		public bool IsToolboxItem (object serializedObject)
		{
			return false;
		}

		public void RemoveCreator (string format, System.ComponentModel.Design.IDesignerHost host)
		{
		}

		public void RemoveCreator (string format)
		{
		}

		public void RemoveToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem, string category)
		{
			this.RemoveToolboxItem (toolboxItem);
		}

		public void RemoveToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
			_toolbox.Items.Remove (toolboxItem);
		}

		public void SelectedToolboxItemUsed ()
		{
			_toolbox.SelectedIndex = -1;
		}

		public object SerializeToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
			return null;
		}

		public bool SetCursor ()
		{
			return false;
		}

		public void SetSelectedToolboxItem (System.Drawing.Design.ToolboxItem toolboxItem)
		{
			_toolbox.SelectedItem = toolboxItem;
		}
#endregion
	}
}
