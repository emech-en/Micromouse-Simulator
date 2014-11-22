using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MicroMouseSimul.MicroMouse;

namespace MicroMouseSimul
{
    public partial class mapDesigner : Form
    {
        private const int iCellWidth = 20;
        private const int iWallWidth = 6;
        private int iCwWidth
        {
            get
            {
                return iCellWidth + iWallWidth;
            }
        }


        private Label[,] lblLeftWalls = new Label[16, 17];
        private Label[,] lblTopWalls = new Label[17, 16];
        private Dictionary<Label, bool> dicSelectedWalls = new Dictionary<Label, bool>();

        private MouseWorld world;

        public mapDesigner(ref MouseWorld pWorld)
        {
            InitializeComponent();

            this.world = pWorld;

            Cell[,] cells = pWorld.GetMazeCells();

            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (i < 16)
                    {
                        this.lblLeftWalls[i, j] = new Label();

                        if (j < 16)
                            this.dicSelectedWalls.Add(lblLeftWalls[i, j], cells[i, j].WestWall);
                        else
                            this.dicSelectedWalls.Add(lblLeftWalls[i, j], cells[i, j - 1].EastWall);

                        this.lblLeftWalls[i, j].Location = new Point(j * iCwWidth, (15 - i) * iCwWidth + iWallWidth);
                        this.lblLeftWalls[i, j].Name = "leftWall" + i + "," + j;
                        this.lblLeftWalls[i, j].Size = new System.Drawing.Size(iWallWidth, iCellWidth);
                        this.lblLeftWalls[i, j].BackColor = dicSelectedWalls[lblLeftWalls[i, j]] ? System.Drawing.Color.Black : System.Drawing.Color.Pink;
                        this.lblLeftWalls[i, j].Cursor = Cursors.Hand;
                        this.lblLeftWalls[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.wall_Click);
                        this.lblLeftWalls[i, j].MouseEnter += new System.EventHandler(this.wall_MouseEnter);
                        this.lblLeftWalls[i, j].MouseLeave += new System.EventHandler(this.wall_MouseLeave);
                        this.pnlMap.Controls.Add(lblLeftWalls[i, j]);
                    }
                    if (j < 16)
                    {
                        this.lblTopWalls[i, j] = new Label();

                        if (i < 16)
                            this.dicSelectedWalls.Add(lblTopWalls[i, j], cells[i, j].SouthWall);
                        else
                            this.dicSelectedWalls.Add(lblTopWalls[i, j], cells[i - 1, j].NorthWall);

                        this.lblTopWalls[i, j].Location = new Point(j * iCwWidth + iWallWidth, (16 - i) * iCwWidth);
                        this.lblTopWalls[i, j].Name = "topWall" + i + "," + j;
                        this.lblTopWalls[i, j].Size = new System.Drawing.Size(iCellWidth, iWallWidth);
                        this.lblTopWalls[i, j].BackColor = dicSelectedWalls[lblTopWalls[i, j]] ? System.Drawing.Color.Black : System.Drawing.Color.Pink;
                        this.lblTopWalls[i, j].Cursor = Cursors.Hand;
                        this.lblTopWalls[i, j].MouseDown += new System.Windows.Forms.MouseEventHandler(this.wall_Click);
                        this.lblTopWalls[i, j].MouseEnter += new System.EventHandler(this.wall_MouseEnter);
                        this.lblTopWalls[i, j].MouseLeave += new System.EventHandler(this.wall_MouseLeave);
                        this.pnlMap.Controls.Add(lblTopWalls[i, j]);
                    }
                }
            }
        }

        private void wall_Click(object sender, EventArgs e)
        {
            var wall = sender as Label;
            if (!dicSelectedWalls[wall])
            {
                wall.BackColor = System.Drawing.Color.Black;
                dicSelectedWalls[wall] = true;
            }
            else
            {
                wall.BackColor = System.Drawing.Color.Pink;
                dicSelectedWalls[wall] = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog fdSave = new SaveFileDialog();
            fdSave.FileName = "Untiteled Map";
            fdSave.AddExtension = true;
            fdSave.DefaultExt = "map";
            fdSave.Filter = "Map File (*.map)|*.map";
            var result = fdSave.ShowDialog();


            if (result == System.Windows.Forms.DialogResult.OK)
            {
                world.ValidateCells();

                int[,] cells = new int[16, 16];
                for (int i = 0; i < 17; i++)
                {
                    for (int j = 0; j < 17; j++)
                    {
                        if (j < 16)
                        {
                            if (i < 16 && dicSelectedWalls[lblTopWalls[i, j]])
                                cells[i, j] |= 4;
                            if (i > 0 && dicSelectedWalls[lblTopWalls[i, j]])
                                cells[i - 1, j] |= 1;
                        }

                        if (i < 16)
                        {
                            if (j < 16 && dicSelectedWalls[lblLeftWalls[i, j]])
                            {
                                cells[i, j] |= 8;
                            }
                            if (j > 0 && dicSelectedWalls[lblLeftWalls[i, j]])
                            {
                                cells[i, j - 1] |= 2;
                            }
                        }
                    }
                }
                try
                {
                    world.SetMaze(cells);
                    ObjectSaver.Save(fdSave.FileName, world);
                    //MessageBox.Show(this, "Your map has been saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show(this, "Something goes wrong. We couldnt save your map.\nPlease try again.", "Oops", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void wall_MouseEnter(object sender, EventArgs e)
        {
            var wall = sender as Label;
            if (!dicSelectedWalls[wall])
            {
                wall.BackColor = System.Drawing.Color.Red;
            }
        }

        private void wall_MouseLeave(object sender, EventArgs e)
        {
            var wall = sender as Label;
            if (!dicSelectedWalls[wall])
            {
                wall.BackColor = System.Drawing.Color.Pink;
            }
        }


    }
}
