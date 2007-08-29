namespace mwf_designer
{
	partial class ReferencesDialog
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
			this.referencesList = new System.Windows.Forms.ListBox();
			this.remove = new System.Windows.Forms.Button();
			this.done = new System.Windows.Forms.Button();
			this.add = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// referencesList
			// 
			this.referencesList.FormattingEnabled = true;
			this.referencesList.Location = new System.Drawing.Point(12, 12);
			this.referencesList.Name = "referencesList";
			this.referencesList.Size = new System.Drawing.Size(219, 264);
			this.referencesList.TabIndex = 0;
			// 
			// remove
			// 
			this.remove.Location = new System.Drawing.Point(237, 41);
			this.remove.Name = "remove";
			this.remove.Size = new System.Drawing.Size(75, 23);
			this.remove.TabIndex = 2;
			this.remove.Text = "Remove";
			this.remove.UseVisualStyleBackColor = true;
			this.remove.Click += new System.EventHandler(this.remove_Click);
			// 
			// done
			// 
			this.done.Location = new System.Drawing.Point(237, 253);
			this.done.Name = "done";
			this.done.Size = new System.Drawing.Size(75, 23);
			this.done.TabIndex = 3;
			this.done.Text = "Done";
			this.done.UseVisualStyleBackColor = true;
			this.done.Click += new System.EventHandler(this.done_Click);
			// 
			// add
			// 
			this.add.Location = new System.Drawing.Point(237, 12);
			this.add.Name = "add";
			this.add.Size = new System.Drawing.Size(75, 23);
			this.add.TabIndex = 4;
			this.add.Text = "Add";
			this.add.UseVisualStyleBackColor = true;
			this.add.Click += new System.EventHandler(this.add_Click);
			// 
			// ReferencesDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(317, 287);
			this.Controls.Add(this.add);
			this.Controls.Add(this.done);
			this.Controls.Add(this.remove);
			this.Controls.Add(this.referencesList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReferencesDialog";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Referenced Assemblies";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox referencesList;
		private System.Windows.Forms.Button remove;
		private System.Windows.Forms.Button done;
		private System.Windows.Forms.Button add;
	}
}