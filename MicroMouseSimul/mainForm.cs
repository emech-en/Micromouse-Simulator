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
        private IAlgorithm algorithm = new DeadEnd();

        private MouseWorld world;

        public mainForm()
        {
            world = new MouseWorld();
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
        }

        private void picMazeHolder_Paint(object sender, PaintEventArgs e)
        {
            var cells = world.GetMazeCells();
            var g = e.Graphics;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    drawCell(cells[i, j], i, j, g);
                    drawCellData(algorithm.GetCellData(i, j), i, j, g);
                }
            }
            drawRobot(world._robot, g);
        }

        private void drawCellData(string p, int i, int j, Graphics g)
        {
            var brush = new SolidBrush(Color.FromArgb(128, Color.Black));
            g.DrawString(p, new System.Drawing.Font(FontFamily.GenericSerif, 10), brush, new RectangleF(j * 32 + 5, (15 - i) * 32 + 5, 24, 24));
        }

        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "Map File (*.map)|*.map|Map File (*.maz)|*.maz";
            var ofResult = ofDialog.ShowDialog(this);
            if (ofResult == System.Windows.Forms.DialogResult.OK)
            {
                if (ofDialog.FileName.IndexOf(".maz") == -1)
                {
                    try
                    {
                        var _maze2 = ObjectSaver.Load(ofDialog.FileName) as MouseWorld;
                        if (_maze2 != null)
                        {
                            world = _maze2;
                            world.ValidateCells();
                        }
                        else
                            MessageBox.Show("error");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error" + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        int[,] iWalls = new int[16, 16];
                        var _mazeWalls = System.IO.File.ReadAllLines(ofDialog.FileName);
                        for (int i = 0; i < 16; i++)
                        {
                            var wallCols = _mazeWalls[15 - i].Split(' ');
                            for (int j = 0; j < 16; j++)
                            {
                                iWalls[i, j] = int.Parse(wallCols[j]);
                            }
                        }
                        world = new MouseWorld(iWalls);
                        world.ValidateCells();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error" + ex.Message);
                    }
                }
                picMazeHolder.Invalidate();
            }
        }

        private void drawCell(Cell cellValue, int i, int j, Graphics g)
        {
            var brush = new SolidBrush(Color.FromArgb(128, Color.Black));
            if (cellValue.NorthWall)
                g.FillRectangle(brush, j * 32, (15 - i) * 32, 34, 2);
            if (cellValue.EastWall)
                g.FillRectangle(brush, (j + 1) * 32, (15 - i) * 32, 2, 34);
            if (cellValue.SouthWall)
                g.FillRectangle(brush, (j) * 32, (15 - i + 1) * 32, 34, 2);
            if (cellValue.WestWall)
                g.FillRectangle(brush, j * 32, (15 - i) * 32, 2, 34);
        }

        private void drawRobot(Robot r, Graphics g)
        {
            var brush = new SolidBrush(Color.FromArgb(128, Color.Red));
            switch (r.Direction)
            {
                case enumDirection.North:
                    g.FillPolygon(brush, new Point[] { new Point(r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5 + 20), new Point(r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5 + 20), new Point(r.XLocation * 32 + 2 + 5 + 10, (15 - r.YLocation) * 32 + 2 + 5) });
                    break;
                case enumDirection.South:
                    g.FillPolygon(brush, new Point[] { new Point(r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5), new Point(r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5), new Point(r.XLocation * 32 + 2 + 5 + 10, (15 - r.YLocation) * 32 + 2 + 5 + 20) });
                    break;
                case enumDirection.East:
                    g.FillPolygon(brush, new Point[] { new Point(r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5 + 10), new Point(r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5), new Point(r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5 + 20) });
                    break;
                case enumDirection.West:
                    g.FillPolygon(brush, new Point[] { new Point(r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5 + 10), new Point(r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5), new Point(r.XLocation * 32 + 2 + 5 + 20, (15 - r.YLocation) * 32 + 2 + 5 + 20) });
                    break;
                default:
                    g.FillRectangle(brush, r.XLocation * 32 + 2 + 5, (15 - r.YLocation) * 32 + 2 + 5, 20, 20);
                    break;
            }
        }

        private void btnEditMap_Click(object sender, EventArgs e)
        {
            mapDesigner frmDesigner = new mapDesigner(ref world);
            frmDesigner.ShowDialog(this);
            picMazeHolder.Invalidate();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            algorithmRunner.WorkerSupportsCancellation = false;
            algorithmRunner.RunWorkerAsync();
        }

        private void algorithmRunner_DoWork(object sender, DoWorkEventArgs e)
        {
            algorithm = new DeadEnd();
            do
            {
                if (algorithmRunner.WorkerSupportsCancellation)
                    return;

                world._robot.Go();
                picMazeHolder.Invalidate();
                System.Threading.Thread.CurrentThread.Join(150);
                if (algorithmRunner.WorkerSupportsCancellation)
                    return;
                
                var action = algorithm.Think(world._robot, world.getCurrentCell());
                picMazeHolder.Invalidate();
                System.Threading.Thread.CurrentThread.Join(150);
                if (algorithmRunner.WorkerSupportsCancellation)
                    return;
                
                switch (action)
                {
                    case enumRobotAction.TurnLeft:
                        world._robot.TrunLeft();
                        break;
                    case enumRobotAction.TurnRight:
                        world._robot.TrunRight();
                        break;
                    case enumRobotAction.TurnBack:
                        world._robot.TrunBack();
                        break;
                    case enumRobotAction.GoStraight:
                        break;
                    default:
                        break;
                }
                picMazeHolder.Invalidate();
                System.Threading.Thread.CurrentThread.Join(150);
            } while (world.NotFinished());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            algorithmRunner.WorkerSupportsCancellation = true;
        }

    }
}
