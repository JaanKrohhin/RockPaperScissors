using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockPaperScissors
{
    public partial class Form1 : Form
    {
        Random rng = new Random();
        PictureBox pic;
        int[] choice;
        string[] rps = { "rock","paper","scissors"};
        public Form1()
        {
            this.Size = new Size(840,500);
            this.Text = "Rock Papers Scissors";
            this.BackColor = Color.LightCyan;
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial", 12);
            this.Icon = Properties.Resources.rps;

            Label lbl = new Label();
            lbl.Size = new Size(100, 25);
            lbl.Location = new Point(30, 30);
            lbl.Text = "1 point";
            this.Controls.Add(lbl);

            pic = new PictureBox();
            pic.Image = Properties.Resources.rock;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(25, 70);
            pic.Click += Pic_Click;
            pic.Name = "rock";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.paper;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(25, 190);
            pic.Click += Pic_Click;
            pic.Name = "paper";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.scissors;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(25, 300);
            pic.Click += Pic_Click;
            pic.Name = "scissors";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.rock;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(560, 70);
            pic.Click += Pic_Click;
            pic.Name = "rock2";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.paper;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(560, 190);
            pic.Click += Pic_Click;
            pic.Name = "paper2";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.scissors;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(560, 300);
            pic.Click += Pic_Click;
            pic.Name = "scissors2";
            this.Controls.Add(pic);


            Button btn = new Button();
            btn.Size = new Size(100,50);
            btn.Location = new Point(320, 25);
            btn.Click += Btn_Click;
            btn.Text = "Begin Battle";
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.LightBlue;
            this.Controls.Add(btn);


            MainMenu menu = new MainMenu();
            MenuItem mf = new MenuItem("File");
            mf.MenuItems.Add("Exit", new EventHandler(menuFile_Select));
            //mf.MenuItems.Add("Image", new EventHandler(menuFile_Select2));
            //mf.MenuItems.Add("Windows", new EventHandler(menuFile_Select3));
            mf.MenuItems.Add(mf);
            this.Menu = menu;

        }

        private void Pic_Click(object sender, EventArgs e)
        {
            switch (pic.Name)
            {
                case "rock":
                    choice[0] = 0;
                    break;
                case "paper":
                    choice[0] = 1;
                    break;
                case "scissors":
                    choice[0] = 2;
                    break;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            choice[1]= rng.Next(0, 2);

        }
        private void menuFile_Select(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
