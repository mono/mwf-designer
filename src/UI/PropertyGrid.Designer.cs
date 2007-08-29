using System;
using System.Windows.Forms;

namespace mwf_designer
{
    partial class PropertyGrid
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
            this._componentsCombo = new System.Windows.Forms.ComboBox();
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // _componentsCombo
            // 
            this._componentsCombo.Dock = System.Windows.Forms.DockStyle.Top;
            this._componentsCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._componentsCombo.FormattingEnabled = true;
            this._componentsCombo.Location = new System.Drawing.Point(0, 0);
            this._componentsCombo.Name = "_componentsCombo";
            this._componentsCombo.Size = new System.Drawing.Size(256, 21);
            this._componentsCombo.TabIndex = 1;
			this._componentsCombo.SelectedIndexChanged += new EventHandler (OnComponentsCombo_SelectedIndexChanged);
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyGrid.Location = new System.Drawing.Point(0, 21);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(256, 390);
            this._propertyGrid.TabIndex = 2;
            // 
            // PropertyGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._propertyGrid);
            this.Controls.Add(this._componentsCombo);
            this.Name = "PropertyGrid";
            this.Size = new System.Drawing.Size(256, 411);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox _componentsCombo;
        private System.Windows.Forms.PropertyGrid _propertyGrid;

    }
}
