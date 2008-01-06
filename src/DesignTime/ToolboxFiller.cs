//
// Authors:	 
//	  Ivan N. Zlatev (contact i-nZ.net)
//
// (C) 2008 Ivan N. Zlatev

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
using System.ComponentModel.Design;
using System.Reflection;
using System.Drawing.Design;

#if WITH_MONO_DESIGN
using Mono.Design;
#endif

namespace mwf_designer
{
	internal class ToolboxFiller
	{
		private ToolBoxList _toolbox;
		private References _references;

		public ToolboxFiller (References references, ToolBoxList toolbox)
		{
			if (toolbox == null)
				throw new ArgumentNullException ("toolbox");
			if (references == null)
				throw new ArgumentNullException ("references");

			_references = references;
			_references.ReferenceAdded += OnReferenceAdded;
			_references.ReferenceRemoved += OnReferenceRemoved;
			_toolbox = toolbox;
			LoadToolboxItems (toolbox, references);
		}

		private void LoadToolboxItems (ToolBoxList toolbox, References references)
		{
			toolbox.SuspendLayout ();
			foreach (Type type in containers) {
				ToolboxItem tool = new ToolboxItem (type);
				toolbox.AddToolboxItem (tool, "Containers");
			}
			foreach (Type type in commonControls) {
				ToolboxItem tool = new ToolboxItem (type);
				toolbox.AddToolboxItem (tool, "Common Controls");
			}
			foreach (Type type in menusToolbars) {
				ToolboxItem tool = new ToolboxItem (type);
				toolbox.AddToolboxItem (tool, "Menus and Toolbars");
			}
			foreach (Type type in commonComponents) {
				ToolboxItem tool = new ToolboxItem (type);
				toolbox.AddToolboxItem (tool, "Common Components");
			}
			foreach (Assembly assembly in references.Assemblies)
				LoadAssemblyToolboxItems (toolbox, assembly);
			toolbox.ResumeLayout ();
		}

		private void LoadAssemblyToolboxItems (ToolBoxList toolbox, Assembly assembly)
		{
			_toolbox.SuspendLayout ();
			foreach (Type type in assembly.GetTypes()) {
				if (IsValidToolType (type) && HasEmptyPublicCtor (type)) {
					ToolboxItem tool = new ToolboxItem (type);
					// If the assembly is not in the GAC assume
					// an assembly with custom controls
					//
					if (assembly.GlobalAssemblyCache)
						toolbox.AddToolboxItem (tool, "All");
					else
						toolbox.AddToolboxItem (tool, "Custom Components");
				}
			}
			_toolbox.ResumeLayout ();
		}

		private void OnReferenceAdded (object sender, ReferenceAddedEventArgs args)
		{
			LoadAssemblyToolboxItems (_toolbox, args.Assembly);
		}

		private void OnReferenceRemoved (object sender, ReferenceRemovedEventArgs args)
		{
			_toolbox.Clear ();
			LoadToolboxItems (_toolbox, _references);
		}

		private bool HasEmptyPublicCtor (Type type)
		{
			bool result = false;
			ConstructorInfo[] ctors = type.GetConstructors ();
			if (ctors.Length > 0) {
				foreach (ConstructorInfo ctor in ctors)
					if (ctor.GetParameters().Length == 0)
						result = true;
			}
			return result;
		}

		private bool IsValidToolType (Type type)
		{
			ToolboxItemAttribute toolboxAttribute = TypeDescriptor.GetAttributes (type)[typeof (ToolboxItemAttribute)] as ToolboxItemAttribute;
			if (toolboxAttribute.ToolboxItemTypeName != ToolboxItemAttribute.None.ToolboxItemTypeName &&
				!type.IsAbstract && !type.IsInterface &&
				((type.Attributes & TypeAttributes.Public) == TypeAttributes.Public) && 
				((type.Attributes & TypeAttributes.NestedFamily) != TypeAttributes.NestedFamily) && 
				((type.Attributes & TypeAttributes.NestedFamORAssem) != TypeAttributes.NestedFamORAssem) && 
				typeof (System.Windows.Forms.Control).IsAssignableFrom (type))
				return true;
			return false;
		}


		private static readonly Type[] containers = new Type[] {
			typeof (System.Windows.Forms.FlowLayoutPanel),
			typeof (System.Windows.Forms.GroupBox),
			typeof (System.Windows.Forms.Panel),
			typeof (System.Windows.Forms.SplitContainer),
			typeof (System.Windows.Forms.TabControl),
			typeof (System.Windows.Forms.TableLayoutPanel),
		};

		private static readonly Type[] menusToolbars = new Type[] {
			typeof (System.Windows.Forms.ContextMenuStrip),
			typeof (System.Windows.Forms.MenuStrip),
			typeof (System.Windows.Forms.StatusStrip),
			typeof (System.Windows.Forms.ToolStrip),
			typeof (System.Windows.Forms.ToolStripContainer),
		};

		private static readonly Type[] commonControls = new Type[] {
			typeof (System.Windows.Forms.Button),
			typeof (System.Windows.Forms.CheckBox),
			typeof (System.Windows.Forms.CheckedListBox),
			typeof (System.Windows.Forms.ComboBox),
			typeof (System.Windows.Forms.DateTimePicker),
			typeof (System.Windows.Forms.Label),
			typeof (System.Windows.Forms.LinkLabel),
			typeof (System.Windows.Forms.ListBox),
			typeof (System.Windows.Forms.ListView),
			typeof (System.Windows.Forms.MaskedTextBox),
			typeof (System.Windows.Forms.MonthCalendar),
			typeof (System.Windows.Forms.NotifyIcon),
			typeof (System.Windows.Forms.NumericUpDown),
			typeof (System.Windows.Forms.PictureBox),
			typeof (System.Windows.Forms.ProgressBar),
			typeof (System.Windows.Forms.RadioButton),
			typeof (System.Windows.Forms.RichTextBox),
			typeof (System.Windows.Forms.TextBox),
			typeof (System.Windows.Forms.ToolTip),
			typeof (System.Windows.Forms.TreeView),
			typeof (System.Windows.Forms.WebBrowser),
		};

		private static readonly Type[] commonComponents = new Type[] {
			typeof (System.ComponentModel.BackgroundWorker),
			typeof (System.IO.FileSystemWatcher),
			typeof (System.Diagnostics.Process),
			typeof (System.Windows.Forms.ImageList),
			typeof (System.Windows.Forms.HelpProvider),
			typeof (System.IO.Ports.SerialPort),
			typeof (System.Windows.Forms.Timer),
		};
	}
}
