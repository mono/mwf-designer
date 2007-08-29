namespace mwf_designer
{
    partial class Toolbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
		this._filterTextBox = new System.Windows.Forms.TextBox();
		this._toolbox = new System.Windows.Forms.ListBox();
		this.SuspendLayout();
		// 
		// _filterTextBox
		// 
		this._filterTextBox.Dock = System.Windows.Forms.DockStyle.Top;
		this._filterTextBox.Location = new System.Drawing.Point(0, 0);
		this._filterTextBox.Name = "_filterTextBox";
		this._filterTextBox.Size = new System.Drawing.Size(209, 20);
		this._filterTextBox.TabIndex = 0;
		// 
		// _toolbox
		// 
		this._toolbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this._toolbox.Dock = System.Windows.Forms.DockStyle.Fill;
		this._toolbox.FormattingEnabled = true;
		this._toolbox.Location = new System.Drawing.Point(0, 20);
		this._toolbox.Name = "_toolbox";
		this._toolbox.Size = new System.Drawing.Size(209, 340);
		this._toolbox.TabIndex = 1;
		// 
		// Toolbox
		// 
		this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this._toolbox);
		this.Controls.Add(this._filterTextBox);
		this.Name = "Toolbox";
		this.Size = new System.Drawing.Size(209, 360);
		this.ResumeLayout(false);
		this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _filterTextBox;
        private System.Windows.Forms.ListBox _toolbox;
    }
}
