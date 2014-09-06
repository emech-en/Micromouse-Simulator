namespace MicroMouseSimul
{
    partial class mainForm
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
            this.picMazeHolder = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
            this.pnlPicMazeHolder = new System.Windows.Forms.Panel();
			this.pnlMazeSolvingStatusHolder = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lblAlgorithm = new System.Windows.Forms.Label();
			this.lblTurnCount = new System.Windows.Forms.Label ();
			this.lblMoveCount = new System.Windows.Forms.Label ();
			this.lblCellVisitedCount = new System.Windows.Forms.Label ();
            this.btnEditMap = new System.Windows.Forms.Button();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.algorithmRunner = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.picMazeHolder)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMazeHolder
            // 
            this.picMazeHolder.BackColor = System.Drawing.Color.White;
            this.picMazeHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMazeHolder.Location = new System.Drawing.Point(0, 0);
            this.picMazeHolder.Name = "picMazeHolder";
            this.picMazeHolder.Size = new System.Drawing.Size(631, 519);
            this.picMazeHolder.TabIndex = 0;
            this.picMazeHolder.TabStop = false;
            this.picMazeHolder.Paint += new System.Windows.Forms.PaintEventHandler(this.picMazeHolder_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.lblAlgorithm);
            this.panel1.Controls.Add(this.btnEditMap);
            this.panel1.Controls.Add(this.btnLoadMap);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 519);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(631, 47);
            this.panel1.TabIndex = 1;

			// 
			// pnlMazeSolvingStatusHolder
			// 
			this.pnlMazeSolvingStatusHolder.Controls.Add(this.lblTurnCount);
			this.pnlMazeSolvingStatusHolder.Controls.Add (this.lblMoveCount);
			this.pnlMazeSolvingStatusHolder.Controls.Add (this.lblCellVisitedCount);
			this.pnlMazeSolvingStatusHolder.Cursor = System.Windows.Forms.Cursors.Default;
			this.pnlMazeSolvingStatusHolder.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlMazeSolvingStatusHolder.Location = new System.Drawing.Point(0, 0);
			this.pnlMazeSolvingStatusHolder.Name = "pnlMazeSolvingStatusHolder";
			this.pnlMazeSolvingStatusHolder.Padding = new System.Windows.Forms.Padding(0);
			this.pnlMazeSolvingStatusHolder.Size = new System.Drawing.Size(130, 519);
			this.pnlMazeSolvingStatusHolder.TabIndex = 2;
			// 
			// pnlPicMazeHolder
			// 
			this.pnlPicMazeHolder.Controls.Add(this.picMazeHolder);
			this.pnlPicMazeHolder.Controls.Add(this.pnlMazeSolvingStatusHolder);
			this.pnlPicMazeHolder.Cursor = System.Windows.Forms.Cursors.Default;
			this.pnlPicMazeHolder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlPicMazeHolder.Location = new System.Drawing.Point(0, 0);
			this.pnlPicMazeHolder.Name = "pnlPicMazeHolder";
			this.pnlPicMazeHolder.Padding = new System.Windows.Forms.Padding(10);
			this.pnlPicMazeHolder.Size = new System.Drawing.Size(631, 519);
			this.pnlPicMazeHolder.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStop.Location = new System.Drawing.Point(483, 10);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(55, 27);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.ItemHeight = 18;
            this.comboBox1.Location = new System.Drawing.Point(241, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 3;
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAlgorithm.Location = new System.Drawing.Point(160, 10);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(81, 27);
            this.lblAlgorithm.TabIndex = 2;
            this.lblAlgorithm.Text = "Algorithm :";
            this.lblAlgorithm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblTurnCount
			// 
			this.lblTurnCount.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTurnCount.Location = new System.Drawing.Point(160, 10);
			this.lblTurnCount.Name = "lblTurnCount";
			this.lblTurnCount.Size = new System.Drawing.Size(81, 27);
			this.lblTurnCount.TabIndex = 2;
			this.lblTurnCount.Text = "Turns : ";
			this.lblTurnCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMoveCount
			// 
			this.lblMoveCount.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblMoveCount.Location = new System.Drawing.Point(160, 10);
			this.lblMoveCount.Name = "lblMoveCount";
			this.lblMoveCount.Size = new System.Drawing.Size(81, 27);
			this.lblMoveCount.TabIndex = 2;
			this.lblMoveCount.Text = "Moves : ";
			this.lblMoveCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCellVisitedCount
			// 
			this.lblCellVisitedCount.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblCellVisitedCount.Location = new System.Drawing.Point(160, 10);
			this.lblCellVisitedCount.Name = "lblCellVisitedCount";
			this.lblCellVisitedCount.Size = new System.Drawing.Size(81, 27);
			this.lblCellVisitedCount.TabIndex = 2;
			this.lblCellVisitedCount.Text = "Unique Cells : ";
			this.lblCellVisitedCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEditMap
            // 
            this.btnEditMap.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEditMap.Location = new System.Drawing.Point(85, 10);
            this.btnEditMap.Name = "btnEditMap";
            this.btnEditMap.Size = new System.Drawing.Size(75, 27);
            this.btnEditMap.TabIndex = 4;
            this.btnEditMap.Text = "Edit Map";
            this.btnEditMap.UseVisualStyleBackColor = true;
            this.btnEditMap.Click += new System.EventHandler(this.btnEditMap_Click);
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLoadMap.Location = new System.Drawing.Point(10, 10);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(75, 27);
            this.btnLoadMap.TabIndex = 1;
            this.btnLoadMap.Text = "Load Map";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // btnRun
            // 
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRun.Location = new System.Drawing.Point(538, 10);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(83, 27);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // algorithmRunner
            // 
            this.algorithmRunner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.algorithmRunner_DoWork);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 566);
			this.Controls.Add (this.pnlPicMazeHolder);
            this.Controls.Add(this.panel1);
            this.Name = "mainForm";
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMazeHolder)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picMazeHolder;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.Button btnRun;
        public System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.Panel pnlPicMazeHolder;
		public System.Windows.Forms.Panel pnlMazeSolvingStatusHolder;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblAlgorithm;
        private System.Windows.Forms.Button btnEditMap;
        private System.ComponentModel.BackgroundWorker algorithmRunner;
        private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Label lblTurnCount;
		private System.Windows.Forms.Label lblMoveCount;
		private System.Windows.Forms.Label lblCellVisitedCount;
    }
}