using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EmechTools.Encrypting;
using MicroMouseSimul.Algorithms;
using MicroMouseSimul.MicroMouse;

namespace MicroMouseSimul
{
	public partial class mainForm : Form
	{
		private IAlgorithm algorithm = new DeadEnd ();
		private MouseWorld world;

		public mainForm ()
		{
			world = new MouseWorld ();
			InitializeComponent ();
		}

		public void StoreNames(List<string> input)
		{
			comboBox1.BeginUpdate ();
			if (comboBox1.InvokeRequired)
				comboBox1.Invoke((MethodInvoker)delegate {
					StoreNames(input);
				});
			else
			{
				comboBox1.Items.Clear();
				comboBox1.Items.AddRange(input.ToArray());
			}
			comboBox1.EndUpdate ();
		}
		private void mainForm_Load (object sender, EventArgs e)
		{
            comboBox1.SelectedIndex = 0;
		}

		private void picMazeHolder_Paint (object sender, PaintEventArgs e)
		{
            if (world != null)
            {
                lblMoveCount.Text = "Moves : " + world.count;
                lblTurnCount.Text = "Turns : " + world.turnCount;
                lblCellVisitedCount.Text = "Unique Cells : " + world.cellVisitedCount;
            }
            
			var cells = world.GetMazeCells ();
            var visitedCells = world.GetVisitedCells();
			var g = e.Graphics;
			for (int i = 0; i < 16; i++) {
				for (int j = 0; j < 16; j++) {
					drawCell (cells [i, j],visitedCells[i,j] , i, j, g);
					drawCellData (world.GetCellData (i, j), i, j, g);
				}
			}
			drawRobot (world._robot, g);
		}

		private void drawCellData (string p, int i, int j, Graphics g)
		{
			var brush = new SolidBrush (Color.FromArgb (128, Color.Black));
			g.DrawString (p, new System.Drawing.Font (FontFamily.GenericSerif, 10), brush, new RectangleF (j * 32 + 5, (15 - i) * 32 + 5, 24, 24));
		}

		private void btnLoadMap_Click (object sender, EventArgs e)
		{
			OpenFileDialog ofDialog = new OpenFileDialog ();
			ofDialog.Filter = "Map File (*.map)|*.map|Map File (*.maz)|*.maz";
			var ofResult = ofDialog.ShowDialog (this);
			if (ofResult == System.Windows.Forms.DialogResult.OK) {
				if (ofDialog.FileName.IndexOf (".maz") == -1) {
					try {
						var _maze2 = ObjectSaver.Load (ofDialog.FileName) as MouseWorld;
						if (_maze2 != null) {
							world = _maze2;
							world.ValidateCells ();
						} else
							MessageBox.Show ("error");
					} catch (Exception ex) {
						MessageBox.Show ("error" + ex.Message);
					}
				} else {
					try {
						int[,] iWalls = new int[16, 16];
						var _mazeWalls = System.IO.File.ReadAllLines (ofDialog.FileName);
						for (int i = 0; i < 16; i++) {
							var wallCols = _mazeWalls [15 - i].Split (' ');
							for (int j = 0; j < 16; j++) {
								iWalls [i, j] = int.Parse (wallCols [j]);
							}
						}
						world = new MouseWorld (iWalls);
						world.ValidateCells ();
					} catch (Exception ex) {
						MessageBox.Show ("error" + ex.Message);
					}
				}
				picMazeHolder.Invalidate ();
			}
		}

		private void drawCell (Cell cellValue,bool visited, int i, int j, Graphics g)
		{
            SolidBrush brush ;
            if (visited)
                brush = new SolidBrush(Color.FromArgb(128, Color.Black));
            else
                brush = new SolidBrush(Color.FromArgb(16, Color.Black));

			if (cellValue.NorthWall)
				g.FillRectangle (brush, j * 32, (15 - i) * 32, 34, 2);
			if (cellValue.EastWall)
				g.FillRectangle (brush, (j + 1) * 32, (15 - i) * 32, 2, 34);
			if (cellValue.SouthWall)
				g.FillRectangle (brush, (j) * 32, (15 - i + 1) * 32, 34, 2);
			if (cellValue.WestWall)
				g.FillRectangle (brush, j * 32, (15 - i) * 32, 2, 34);
		}

		private void drawRobot (Robot r, Graphics g)
		{
			var brush = new SolidBrush (Color.FromArgb (128, Color.Red));
			switch (r.Direction) {
			case enumDirection.North:
				g.FillPolygon (brush, new Point[] {
					new Point (r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5 + 20),
					new Point (r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5 + 20),
					new Point (r.XLocation * 32 + 2 + 5 + 10, (15 - r.YLocation) * 32 + 2 + 5)
				});
				break;
			case enumDirection.South:
				g.FillPolygon (brush, new Point[] {
					new Point (r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5),
					new Point (r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5),
					new Point (r.XLocation * 32 + 2 + 5 + 10, (15 - r.YLocation) * 32 + 2 + 5 + 20)
				});
				break;
			case enumDirection.East:
				g.FillPolygon (brush, new Point[] {
					new Point (r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5 + 10),
					new Point (r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5),
					new Point (r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5 + 20)
				});
				break;
			case enumDirection.West:
				g.FillPolygon (brush, new Point[] {
					new Point (r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5 + 10),
					new Point (r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5),
					new Point (r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5 + 20)
				});
				break;
			default:
				g.FillRectangle (brush, r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5, 20, 20);
				break;
			}
		}

		private void btnEditMap_Click (object sender, EventArgs e)
		{
			mapDesigner frmDesigner = new mapDesigner (ref world);
			frmDesigner.ShowDialog (this);
			picMazeHolder.Invalidate ();
		}

		private void btnRun_Click (object sender, EventArgs e)
		{
            btnRun.Enabled = false;
            btnLoadMap.Enabled = false;
            btnEditMap.Enabled = false;
            comboBox1.Enabled = false;
            IAlgorithm selectedAlg;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    selectedAlg = new WallFollower(false);
                    break;
                case 1:
                    selectedAlg = new DeadEnd();
                    break;
                case 2:
                default:
                    selectedAlg = new FloodFill();
                    break;
            }
            btnStop.Enabled = true;

            world.Start(selectedAlg);
			algorithmRunner.WorkerSupportsCancellation = false;
			algorithmRunner.RunWorkerAsync ();
		}

		private void algorithmRunner_DoWork (object sender, DoWorkEventArgs e)
		{
			do {
				picMazeHolder.Invalidate ();
				System.Threading.Thread.CurrentThread.Join (150);

			} while (world.Go_go_go() && !algorithmRunner.WorkerSupportsCancellation);
			picMazeHolder.Invalidate ();
			System.Threading.Thread.CurrentThread.Join (150);
		}

		private void btnStop_Click (object sender, EventArgs e)
		{
			algorithmRunner.WorkerSupportsCancellation = true;
		}

        private void algorithmRunner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRun.Enabled = true;
            btnLoadMap.Enabled = true;
            btnEditMap.Enabled = true;
            comboBox1.Enabled = true;

            btnStop.Enabled = false;
        }

        private void lblAlgorithm_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
	}
}
