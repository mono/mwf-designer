namespace mwf_designer
{
	partial class NewFileDialog
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
			this.templatesListbox = new System.Windows.Forms.ListBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.namespaceTextbox = new System.Windows.Forms.TextBox();
			this.classTextbox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.filenameTextbox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.doneButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// templatesListbox
			// 
			this.templatesListbox.FormattingEnabled = true;
			this.templatesListbox.Location = new System.Drawing.Point(12, 12);
			this.templatesListbox.Name = "templatesListbox";
			this.templatesListbox.Size = new System.Drawing.Size(96, 121);
			this.templatesListbox.TabIndex = 0;
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(371, 74);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(36, 20);
			this.browseButton.TabIndex = 1;
			this.browseButton.Text = "...";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// namespaceTextbox
			// 
			this.namespaceTextbox.Location = new System.Drawing.Point(187, 23);
			this.namespaceTextbox.Name = "namespaceTextbox";
			this.namespaceTextbox.Size = new System.Drawing.Size(220, 20);
			this.namespaceTextbox.TabIndex = 2;
			// 
			// classTextbox
			// 
			this.classTextbox.Location = new System.Drawing.Point(187, 49);
			this.classTextbox.Name = "classTextbox";
			this.classTextbox.Size = new System.Drawing.Size(220, 20);
			this.classTextbox.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(114, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Class:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(114, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Namespace:";
			// 
			// filenameTextbox
			// 
			this.filenameTextbox.Location = new System.Drawing.Point(187, 74);
			this.filenameTextbox.Name = "filenameTextbox";
			this.filenameTextbox.Size = new System.Drawing.Size(178, 20);
			this.filenameTextbox.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(114, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "File:";
			// 
			// doneButton
			// 
			this.doneButton.Location = new System.Drawing.Point(332, 110);
			this.doneButton.Name = "doneButton";
			this.doneButton.Size = new System.Drawing.Size(75, 23);
			this.doneButton.TabIndex = 8;
			this.doneButton.Text = "Done";
			this.doneButton.UseVisualStyleBackColor = true;
			this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(251, 110);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// NewFileDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 139);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.doneButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.filenameTextbox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.classTextbox);
			this.Controls.Add(this.namespaceTextbox);
			this.Controls.Add(this.browseButton);
			this.Controls.Add(this.templatesListbox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewFileDialog";
			this.Text = "New File";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox templatesListbox;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.TextBox namespaceTextbox;
		private System.Windows.Forms.TextBox classTextbox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox filenameTextbox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button doneButton;
		private System.Windows.Forms.Button cancelButton;
	}
}