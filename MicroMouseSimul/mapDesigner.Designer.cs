namespace MicroMouseSimul
{
    partial class mapDesigner
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
            this.pnlMap = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlMap
            // 
            this.pnlMap.Location = new System.Drawing.Point(12, 12);
            this.pnlMap.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(422, 422);
            this.pnlMap.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(359, 437);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Map";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // mapDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 474);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mapDesigner";
            this.ShowInTaskbar = false;
            this.Text = "Map Designer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.Button btnSave;








    }
}

