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
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

#if WITH_MONO_DESIGN
using Mono.Design;
using DocumentDesigner = Mono.Design.DocumentDesigner;
#endif

namespace mwf_designer
{
	public partial class MainView : Form
	{
		Workspace _workspace;
		private readonly string MODIFIED_MARKER = " *";

		public MainView ()
		{
			InitializeComponent ();
			toolbox.ToolPicked += OnToolbox_ToolPicked;
			LoadWorkspace ();
		}

		private void openToolStripMenuItem_Click (object sender, EventArgs e)
		{			
			OpenFileDialog dialog = new OpenFileDialog ();
			dialog.CheckFileExists = true;
			dialog.Multiselect = false;
			dialog.Filter = "C# Source Code (*.cs)|*.cs|VB.NET Source Code (*.vb)|*.vb";
			if (dialog.ShowDialog () == DialogResult.OK) {
				if (surfaceTabs.TabPages.ContainsKey (dialog.FileName)) {// tab page for file already existing
					surfaceTabs.SelectedTab = surfaceTabs.TabPages[dialog.FileName];
				} else {
					if (CodeProvider.IsValid (dialog.FileName))
						LoadDocument (dialog.FileName, _workspace);
					else
						MessageBox.Show ("No corresponding .Designer file found for " + dialog.FileName);
				}
			}
		}

		private void OnToolbox_ToolPicked (object sender, EventArgs args)
		{
			if (_workspace != null && _workspace.ActiveDocument != null) {
				IDesignerHost host = _workspace.ActiveDocument.DesignSurface.GetService (typeof (IDesignerHost)) as IDesignerHost;
				IToolboxService tb = _workspace.ActiveDocument.DesignSurface.GetService (typeof (IToolboxService)) as IToolboxService;
				if (host != null && tb != null)
					((IToolboxUser)(DocumentDesigner)host.GetDesigner (host.RootComponent)).ToolPicked (tb.GetSelectedToolboxItem ());
			}
		}

		private void LoadWorkspace ()
		{
			_workspace = new Workspace ();
			surfaceTabs.SelectedIndexChanged += delegate {
				UpdateWorkspaceActiveDocument ();
			};
			_workspace.ActiveDocumentChanged += OnActiveDocumentChanged;
			_workspace.References.ReferencesChanged += delegate {
				PopulateToolbox (toolbox, _workspace.References);
			};
			_workspace.Services.AddService (typeof (IToolboxService), (IToolboxService) toolbox);
			AddErrorsTab ();
			_workspace.Load ();
		}

		private IComponent GetPrimarySelection (Document document)
		{
			if (document == null)
				throw new ArgumentNullException ("document");

			ISelectionService service = document.DesignSurface.GetService (typeof (ISelectionService)) as ISelectionService;
			if (service != null)
				return (IComponent) service.PrimarySelection;
			else
				return null;
		}

		private void AddErrorsTab ()
		{
			ErrorListTabPage errors = new ErrorListTabPage ();
			surfaceTabs.TabPages.Add (errors);
			_workspace.Services.AddService (typeof (IUIService), (IUIService) errors);
		}

		// Currently populates with all MWF Controls that have a public ctor with no params
		//
		private void PopulateToolbox (ToolBoxList toolbox, References references)
		{
			toolbox.SuspendLayout ();
			toolbox.Clear ();
			foreach (ToolboxItem item in _workspace.GetToolboxItems ()) {
				string category = (string) item.Properties["Category"];
				if (category != null)
					toolbox.AddToolboxItem (item, category);
				else
					toolbox.AddToolboxItem (item);
			}
			toolbox.ResumeLayout ();
		}

		private void LoadDocument (string file, Workspace workspace)
		{
			TabPage tab = new TabPage (Path.GetFileNameWithoutExtension (file));
			tab.Name = file; // the key

			// loads and associates the tab page with the document
			Document doc = workspace.LoadDocument (file, tab); 
			if (doc.LoadSuccessful) {
				doc.Modified += OnDocumentModified;
				workspace.ActiveDocument = doc;
				tab.Controls.Add ((Control)doc.DesignSurface.View);
				surfaceTabs.TabPages.Add (tab);
				surfaceTabs.SelectedTab = surfaceTabs.TabPages[file];
			} else {
				workspace.CloseDocument (doc);
				tab.Dispose ();
			}
		}

		private void OnDocumentModified (object sender, EventArgs args)
		{
			if (!surfaceTabs.SelectedTab.Text.EndsWith (MODIFIED_MARKER))
				surfaceTabs.SelectedTab.Text += MODIFIED_MARKER;
		}

		private void OnActiveDocumentChanged (object sender, ActiveDocumentChangedEventArgs args)
		{
			object primarySelection = args.NewDocument != null ? GetPrimarySelection (args.NewDocument) : null;
			propertyGrid.ActiveComponents = primarySelection != null ? new object[] { primarySelection } : new object[0];
		}

		private void CloseDocument (Document doc)
		{
			doc.Modified -= OnDocumentModified;
			_workspace.CloseDocument (doc);
			surfaceTabs.TabPages.Remove (surfaceTabs.SelectedTab);
		}

		private void newToolStripMenuItem_Click (object sender, EventArgs e)
		{
			NewFileDialog dialog = new NewFileDialog (TemplateManager.AvailableTemplates);
			if (dialog.ShowDialog () == DialogResult.OK) {
				TemplateManager.WriteCode (dialog.Template, dialog.File, CodeProvider.GetCodeBehindFileName (dialog.File), 
										   dialog.Namespace, dialog.Class);
				this.LoadDocument (dialog.File, _workspace);
			}
		}

		private void UpdateWorkspaceActiveDocument ()
		{
			if (!(surfaceTabs.SelectedTab is ErrorListTabPage))
				_workspace.ActiveDocument = _workspace.GetDocument (surfaceTabs.SelectedTab);
			else
				_workspace.ActiveDocument = null;
		}


		private void exitToolStripMenuItem_Click (object sender, EventArgs e)
		{
			this.Close ();
		}

		private void saveToolStripMenuItem_Click (object sender, EventArgs e)
		{
			if (_workspace.ActiveDocument != null && _workspace.ActiveDocument.IsModified) {
				_workspace.ActiveDocument.Save ();
				surfaceTabs.SelectedTab.Text = Path.GetFileNameWithoutExtension (_workspace.ActiveDocument.FileName);
			}
		}

		private void closeToolStripMenuItem_Click (object sender, EventArgs e)
		{
			if (!(surfaceTabs.SelectedTab is ErrorListTabPage))
				if (_workspace.ActiveDocument != null)
					CloseDocument (_workspace.ActiveDocument);
		}

		private void referencesToolStripMenuItem_Click (object sender, EventArgs e)
		{
			new ReferencesDialog (_workspace.References).ShowDialog ();
		}
	}
}