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
using UndoEngine = Mono.Design.UndoEngine;
using MenuCommandService = Mono.Design.MenuCommandService;
#endif

namespace mwf_designer
{
	public partial class MainView : Form
	{
		private Workspace _workspace;
		private ToolboxFiller _toolboxFiller;
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
			_workspace.Services.AddService (typeof (IToolboxService), (IToolboxService) toolbox);
			_toolboxFiller = new ToolboxFiller (_workspace.References, toolbox);
			AddErrorsTab ();
			_workspace.Load ();
		}

		private void AddErrorsTab ()
		{
			ErrorListTabPage errors = new ErrorListTabPage ();
			surfaceTabs.TabPages.Add (errors);
			_workspace.Services.AddService (typeof (IUIService), (IUIService) errors);
		}

		private void LoadDocument (string file, Workspace workspace)
		{
			TabPage tab = new TabPage (Path.GetFileNameWithoutExtension (file));
			tab.Name = file; // the key

			// loads and associates the tab page with the document
			Document doc = workspace.CreateDocument (file, tab);
			doc.Services.AddService (typeof (IMenuCommandService), new MenuCommandService (doc.Services));
			doc.Load ();
			doc.Services.AddService (typeof (UndoEngine), new UndoRedoEngine (doc.Services));
			if (doc.LoadSuccessful) {
				doc.Modified += OnDocumentModified;
				workspace.ActiveDocument = doc;
				((Control)doc.DesignSurface.View).Dock = DockStyle.Fill;
				tab.Controls.Add ((Control)doc.DesignSurface.View);
				surfaceTabs.SuspendLayout ();
				surfaceTabs.TabPages.Add (tab);
				surfaceTabs.ResumeLayout ();
				surfaceTabs.SelectedTab = surfaceTabs.TabPages[file];
			} else {
				MessageBox.Show ("Unable to load!");
				tab.Dispose ();
				workspace.CloseDocument (doc);
			}
		}

		private void OnDocumentModified (object sender, EventArgs args)
		{
			if (!surfaceTabs.SelectedTab.Text.EndsWith (MODIFIED_MARKER))
				surfaceTabs.SelectedTab.Text += MODIFIED_MARKER;
		}

		private void OnActiveDocumentChanged (object sender, ActiveDocumentChangedEventArgs args)
		{
			if (args.NewDocument != null)
				propertyGrid.Update (args.NewDocument.Services);
		}

		private void CloseDocument (Document doc)
		{
			doc.Modified -= OnDocumentModified;
			surfaceTabs.TabPages.Remove (surfaceTabs.SelectedTab);
			_workspace.CloseDocument (doc);
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

		private void OnReferences_Clicked (object sender, EventArgs e)
		{
			new ReferencesDialog (_workspace.References).ShowDialog ();
		}
		
		private void OnUndo_Clicked (object sender, EventArgs args)
		{
			if (_workspace.ActiveDocument != null) {
				UndoRedoEngine undoEngine = _workspace.ActiveDocument.DesignSurface.GetService (typeof (UndoEngine)) as UndoRedoEngine;
				if (undoEngine != null)
					undoEngine.Undo ();
			}
		}

		private void OnRedo_Clicked (object sender, EventArgs args)
		{
			if (_workspace.ActiveDocument != null) {
				UndoRedoEngine undoEngine = _workspace.ActiveDocument.DesignSurface.GetService (typeof (UndoEngine)) as UndoRedoEngine;
				if (undoEngine != null)
					undoEngine.Redo ();
			}
		}

		private void OnCut_Clicked (object sender, EventArgs args)
		{
			if (_workspace.ActiveDocument != null) {
				IMenuCommandService menuCommands = _workspace.ActiveDocument.DesignSurface.GetService (typeof (IMenuCommandService)) as IMenuCommandService;
				if (menuCommands != null)
					menuCommands.FindCommand (StandardCommands.Cut).Invoke ();
			}
		}

		private void OnCopy_Clicked (object sender, EventArgs args)
		{
			if (_workspace.ActiveDocument != null) {
				IMenuCommandService menuCommands = _workspace.ActiveDocument.DesignSurface.GetService (typeof (IMenuCommandService)) as IMenuCommandService;
				if (menuCommands != null)
					menuCommands.FindCommand (StandardCommands.Copy).Invoke ();
			}
		}

		private void OnPaste_Clicked (object sender, EventArgs args)
		{
			if (_workspace.ActiveDocument != null) {
				IMenuCommandService menuCommands = _workspace.ActiveDocument.DesignSurface.GetService (typeof (IMenuCommandService)) as IMenuCommandService;
				if (menuCommands != null)
					menuCommands.FindCommand (StandardCommands.Paste).Invoke ();
			}
		}

		private void OnDelete_Clicked (object sender, EventArgs args)
		{
			if (_workspace.ActiveDocument != null) {
				IMenuCommandService menuCommands = _workspace.ActiveDocument.DesignSurface.GetService (typeof (IMenuCommandService)) as IMenuCommandService;
				if (menuCommands != null)
					menuCommands.FindCommand (StandardCommands.Delete).Invoke ();
			}
		}
	}
}