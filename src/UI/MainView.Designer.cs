namespace mwf_designer
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
		this.mainMenu = new System.Windows.Forms.MenuStrip();
		this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
		this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
		this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
		this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.alignToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
		this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.surfaceTabs = new System.Windows.Forms.TabControl();
		this.toolbox = new mwf_designer.Toolbox();
		this.propertyGrid = new mwf_designer.PropertyGrid();
		this.mainMenu.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		this.SuspendLayout();
		// 
		// mainMenu
		// 
		this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.formatToolStripMenuItem,
            this.helpToolStripMenuItem});
		this.mainMenu.Location = new System.Drawing.Point(0, 0);
		this.mainMenu.Name = "mainMenu";
		this.mainMenu.Size = new System.Drawing.Size(829, 24);
		this.mainMenu.TabIndex = 0;
		this.mainMenu.Text = "menuStrip1";
		// 
		// fileToolStripMenuItem
		// 
		this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
			this.closeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
		this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
		this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
		this.fileToolStripMenuItem.Text = "File";
		// 
		// newToolStripMenuItem
		// 
		this.newToolStripMenuItem.Name = "newToolStripMenuItem";
		this.newToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
		this.newToolStripMenuItem.Text = "New";
		this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
		// 
		// openToolStripMenuItem
		// 
		this.openToolStripMenuItem.Name = "openToolStripMenuItem";
		this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
		this.openToolStripMenuItem.Text = "Open";
		this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
		// 
		// saveToolStripMenuItem
		// 
		this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
		this.saveToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
		this.saveToolStripMenuItem.Text = "Save";
		this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
		// 
		// closeToolStripMenuItem
		// 
		this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
		this.closeToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
		this.closeToolStripMenuItem.Text = "Close";
		this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
		// 
		// toolStripMenuItem1
		// 
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 6);
		// 
		// exitToolStripMenuItem
		// 
		this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
		this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
		this.exitToolStripMenuItem.Text = "Exit";
		this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
		// 
		// editToolStripMenuItem
		// 
		this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripMenuItem2,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.preferencesToolStripMenuItem});
		this.editToolStripMenuItem.Name = "editToolStripMenuItem";
		this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
		this.editToolStripMenuItem.Text = "Edit";
		// 
		// undoToolStripMenuItem
		// 
		this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
		this.undoToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
		this.undoToolStripMenuItem.Text = "Undo";
		// 
		// redoToolStripMenuItem
		// 
		this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
		this.redoToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
		this.redoToolStripMenuItem.Text = "Redo";
		// 
		// toolStripMenuItem2
		// 
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(193, 6);
		// 
		// cutToolStripMenuItem
		// 
		this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
		this.cutToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
		this.cutToolStripMenuItem.Text = "Cut";
		// 
		// copyToolStripMenuItem
		// 
		this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
		this.copyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
		this.copyToolStripMenuItem.Text = "Copy";
		// 
		// pasteToolStripMenuItem
		// 
		this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
		this.pasteToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
		this.pasteToolStripMenuItem.Text = "Paste";
		// 
		// deleteToolStripMenuItem
		// 
		this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		this.deleteToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
		this.deleteToolStripMenuItem.Text = "Delete";
		// 
		// toolStripMenuItem3
		// 
		this.toolStripMenuItem3.Name = "toolStripMenuItem3";
		this.toolStripMenuItem3.Size = new System.Drawing.Size(193, 6);
		// 
		// preferencesToolStripMenuItem
		// 
		this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
		this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
		this.preferencesToolStripMenuItem.Text = "Referenced Assemblies";
		this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.referencesToolStripMenuItem_Click);
		// 
		// formatToolStripMenuItem
		// 
		this.formatToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alignToolStripMenuItem,
            this.toolStripMenuItem5});
		this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
		this.formatToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
		this.formatToolStripMenuItem.Text = "Format";
		// 
		// alignToolStripMenuItem
		// 
		this.alignToolStripMenuItem.Name = "alignToolStripMenuItem";
		this.alignToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
		this.alignToolStripMenuItem.Text = "Align";
		// 
		// toolStripMenuItem5
		// 
		this.toolStripMenuItem5.Name = "toolStripMenuItem5";
		this.toolStripMenuItem5.Size = new System.Drawing.Size(108, 22);
		this.toolStripMenuItem5.Text = ".....";
		// 
		// helpToolStripMenuItem
		// 
		this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
		this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
		this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
		this.helpToolStripMenuItem.Text = "Help";
		// 
		// aboutToolStripMenuItem
		// 
		this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
		this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
		this.aboutToolStripMenuItem.Text = "About";
		// 
		// splitContainer1
		// 
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(0, 24);
		this.splitContainer1.Name = "splitContainer1";
		// 
		// splitContainer1.Panel1
		// 
		this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
		// 
		// splitContainer1.Panel2
		// 
		this.splitContainer1.Panel2.Controls.Add(this.propertyGrid);
		this.splitContainer1.Size = new System.Drawing.Size(829, 469);
		this.splitContainer1.SplitterDistance = 626;
		this.splitContainer1.TabIndex = 1;
		// 
		// splitContainer2
		// 
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.Location = new System.Drawing.Point(0, 0);
		this.splitContainer2.Name = "splitContainer2";
		// 
		// splitContainer2.Panel1
		// 
		this.splitContainer2.Panel1.Controls.Add(this.toolbox);
		// 
		// splitContainer2.Panel2
		// 
		this.splitContainer2.Panel2.Controls.Add(this.surfaceTabs);
		this.splitContainer2.Size = new System.Drawing.Size(626, 469);
		this.splitContainer2.SplitterDistance = 208;
		this.splitContainer2.TabIndex = 0;
		// 
		// surfaceTabs
		// 
		this.surfaceTabs.Dock = System.Windows.Forms.DockStyle.Fill;
		this.surfaceTabs.Location = new System.Drawing.Point(0, 0);
		this.surfaceTabs.Name = "surfaceTabs";
		this.surfaceTabs.SelectedIndex = 0;
		this.surfaceTabs.Size = new System.Drawing.Size(414, 469);
		this.surfaceTabs.TabIndex = 0;
		// 
		// toolbox
		// 
		this.toolbox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.toolbox.Location = new System.Drawing.Point(0, 0);
		this.toolbox.Name = "toolbox";
		this.toolbox.SelectedCategory = null;
		this.toolbox.Size = new System.Drawing.Size(208, 469);
		this.toolbox.TabIndex = 0;
		// 
		// propertyGrid
		// 
		this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
		this.propertyGrid.Location = new System.Drawing.Point(0, 0);
		this.propertyGrid.Name = "propertyGrid";
		this.propertyGrid.Size = new System.Drawing.Size(199, 469);
		this.propertyGrid.TabIndex = 0;
		// 
		// MainView
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(829, 493);
		this.Controls.Add(this.splitContainer1);
		this.Controls.Add(this.mainMenu);
		this.MainMenuStrip = this.mainMenu;
		this.Name = "MainView";
		this.Text = "MWF Designer";
		this.mainMenu.ResumeLayout(false);
		this.mainMenu.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.ResumeLayout(false);
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel2.ResumeLayout(false);
		this.splitContainer2.ResumeLayout(false);
		this.ResumeLayout(false);
		this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alignToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
	    private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private PropertyGrid propertyGrid;
	    private System.Windows.Forms.SplitContainer splitContainer2;
	    private Toolbox toolbox;
	    private System.Windows.Forms.TabControl surfaceTabs;
    }
}

