using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gumball
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Region rg;
        private GraphicsPath gp;
        public List<PictureBox> pictureboxes = new List<PictureBox>();
        private void Form1_Load(object sender, EventArgs e)
        {
            gp = new GraphicsPath();
            gp.AddEllipse(pictureBox1.DisplayRectangle);
            rg = new Region(gp);
            pictureBox1.Region = rg;
            pictureBox2.Region = rg;
            for (int i = 0; i < 64; i++)
            {
                pictureboxes.Add(new PictureBox());
            }
            int lx = 0;
            int ly = 0;
            foreach (PictureBox picturebox in pictureboxes)
            {
                picturebox.Cursor = Cursors.Hand;
                picturebox.Size = new System.Drawing.Size(35, 35);
                picturebox.BackColor = System.Drawing.Color.Silver;
                picturebox.Location = new Point(195 + 10 * lx + picturebox.Size.Width * lx, 115 + 10 * ly + picturebox.Size.Height * ly);
                gp = new GraphicsPath();
                gp.AddEllipse(picturebox.DisplayRectangle);
                rg = new Region(gp);
                picturebox.Region = rg;
                lx++;
                if (lx > 7)
                {
                    lx = 0;
                    ly++;
                }
                this.Controls.Add(picturebox);
                picturebox.BringToFront();
            }
            AddEvents();
        }
        private void AddEvents()
        {
            foreach (var (picturebox, index) in pictureboxes.WithIndex())
            {
                picturebox.Click += (sender, e) =>
                {
                    ProcessLogic(index);
                };
            }
        }
        private void ProcessLogic(int index)
        {
            if (pictureboxes[index].BackColor == Color.Silver)
            {
                bool player1turn = false;
                bool player2turn = false;
                if (lbp1.Text == ">")
                {
                    pictureboxes[index].BackColor = Color.White;
                    if (index > 7 & index < 56)
                    {
                        if (pictureboxes[index - 8].BackColor == Color.White & pictureboxes[index + 8].BackColor == Color.White)
                        {
                            player1turn = true;
                        }
                    }
                    if (!player1turn)
                    {
                        if ((index > 0 & index < 7) | (index > 8 & index < 15) | (index > 16 & index < 23) | (index > 24 & index < 31) | (index > 32 & index < 39) | (index > 40 & index < 47) | (index > 48 & index < 55) | (index > 56 & index < 63))
                        {
                            if (pictureboxes[index - 1].BackColor == Color.White & pictureboxes[index + 1].BackColor == Color.White)
                            {
                                player1turn = true;
                            }
                        }
                    }
                    if (!player1turn)
                    {
                        lbp1.Text = "";
                        lbp2.Text = ">";
                    }
                }
                else if (lbp2.Text == ">")
                {
                    pictureboxes[index].BackColor = Color.Black;
                    if (index > 7 & index < 56)
                    {
                        if (pictureboxes[index - 8].BackColor == Color.Black & pictureboxes[index + 8].BackColor == Color.Black)
                        {
                            player2turn = true;
                        }
                    }
                    if (!player2turn)
                    {
                        if ((index > 0 & index < 7) | (index > 8 & index < 15) | (index > 16 & index < 23) | (index > 24 & index < 31) | (index > 32 & index < 39) | (index > 40 & index < 47) | (index > 48 & index < 55) | (index > 56 & index < 63))
                        {
                            if (pictureboxes[index - 1].BackColor == Color.Black & pictureboxes[index + 1].BackColor == Color.Black)
                            {
                                player2turn = true;
                            }
                        }
                    }
                    if (!player2turn)
                    {
                        lbp1.Text = ">";
                        lbp2.Text = "";
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lbp1.Text = ">";
            lbp2.Text = "";
            foreach (PictureBox picturebox in pictureboxes)
            {
                picturebox.BackColor = Color.Silver;
            }
        }
    }
    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self) => self.Select((item, index) => (item, index));
    }
}