using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockPaperScissors
{
    public partial class Form1 : Form
    {
        
        Random rng = new Random();
        PictureBox pic;
        string[] choice = new string[2];
        string[] rps = { "rock", "paper", "scissors"};
        string[] results = new string[11];
        bool bot = true;
        Label lbl;
        ListBox list;
        public Form1()
        {
            resultsRead();
            choice[0] = "rock";
            this.Size = new Size(500,600);
            this.Text = "Rock Papers Scissors";
            this.BackColor = Color.LightCyan;
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial", 12);
            this.Icon = Properties.Resources.battle;

            lbl = new Label();
            lbl.Size = new Size(100, 25);
            lbl.Location = new Point(180, 25);
            lbl.TextAlign = (ContentAlignment)HorizontalAlignment.Center;
            this.Controls.Add(lbl);
            
            pic = new PictureBox();
            pic.Image = Properties.Resources.rock1;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(25, 70);
            pic.Click += Pic_Click;
            pic.Name = "rock1";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.paper1;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(25, 190);
            pic.Click += Pic_Click;
            pic.Name = "paper1";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.scissors1;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(25, 300);
            pic.Click += Pic_Click;
            pic.Name = "scissors1";
            this.Controls.Add(pic);

            pic = new PictureBox();
            pic.Image = Properties.Resources.rock1;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(350, 70);
            pic.Click += Pic_Click;
            pic.Name = "rock2";
            this.Controls.Add(pic);
            pic.Hide();

            pic = new PictureBox();
            pic.Image = Properties.Resources.paper1;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(350, 190);
            pic.Click += Pic_Click;
            pic.Name = "paper2";
            this.Controls.Add(pic);
            pic.Hide();

            pic = new PictureBox();
            pic.Image = Properties.Resources.scissors1;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Size = new Size(100, 100);
            pic.Location = new Point(350, 300);
            pic.Click += Pic_Click;
            pic.Name = "scissors2";
            this.Controls.Add(pic);
            pic.Hide();

            Button btn = new Button();
            btn.Size = new Size(100,50);
            btn.Location = new Point(180, 50);
            btn.Click += Btn_Click;
            btn.Text = "Begin Battle";
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = this.BackColor;
            this.Controls.Add(btn);

            list = new ListBox();
            list.Size = new Size(100,200);
            list.Location = new Point(180, 110);
            this.Controls.Add(list);
            MainMenu menu = new MainMenu();
            MenuItem mf = new MenuItem("Settings");
            mf.MenuItems.Add("2 Player Mode", new EventHandler(menuFile_Select3)).Shortcut = Shortcut.CtrlS;
            mf.MenuItems.Add("Dark Theme", new EventHandler(menuFile_Select2)).Shortcut = Shortcut.CtrlD;
            mf.MenuItems.Add("Rules", new EventHandler(menuFile_Select4)).Shortcut = Shortcut.CtrlA;
            mf.MenuItems.Add("Exit", new EventHandler(menuFile_Select));
            menu.MenuItems.Add(mf);
            this.Menu = menu;
        }

        private void menuFile_Select4(object sender, EventArgs e)
        {
            MessageBox.Show("Paper beats Rock, Rock beats Scissors, Scissors beat Paper", "Rules of the game");
        }

        private void menuFile_Select3(object sender, EventArgs e)
        {
            hiSh2player();
        }

        private void menuFile_Select2(object sender, EventArgs e)
        {
            MenuItem m = sender as MenuItem;
            m.Checked = !m.Checked;
            if (m.Checked==true)
            {
                this.BackgroundImage = Properties.Resources.back;
            }
            else
            {
                this.BackgroundImage = null;
                this.BackColor = Color.LightCyan;
            }
        }

        private void Pic_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            switch (p.Name.Substring(p.Name.Length - 1))
            {
                case "1":
                    switch (p.Name.Substring(0, p.Name.Length - 1))
                    {
                        case "rock":
                            choice[0] = p.Name.Substring(0, p.Name.Length - 1);
                            break;
                        case "paper":
                            choice[0] = p.Name.Substring(0, p.Name.Length - 1);
                            break;
                        case "scissors":
                            choice[0] = p.Name.Substring(0, p.Name.Length - 1);
                            break;
                    }
                    break;
                case "2":
                    switch (p.Name.Substring(0, p.Name.Length - 1))
                    {
                        case "rock":
                            choice[1] = p.Name.Substring(0, p.Name.Length - 1);
                            break;
                        case "paper":
                            choice[1] = p.Name.Substring(0, p.Name.Length - 1);
                            break;
                        case "scissors":
                            choice[1] = p.Name.Substring(0, p.Name.Length - 1);
                            break;
                    }
                    break;

            }
            switch (p.Name.Substring(0, p.Name.Length - 1))
            {
                case "rock":
                    choice[0] = p.Name.Substring(0, p.Name.Length - 1);
                    break;
                case "paper":
                    choice[0] = p.Name.Substring(0, p.Name.Length - 1);
                    break;
                case "scissors":
                    choice[0] = p.Name.Substring(0, p.Name.Length - 1);
                    break;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (bot==true)
            {
                choice[1] = rps[rng.Next(0,3)];
            }
            if (win()==0)
            {
                lbl.Text = "Opponent win";
            }
            else if (win()==1)
            {
                lbl.Text = "Host win";
            }
            else if (win()==2)
            {
                lbl.Text = "Stalemate";
            }
            else
            {
                lbl.Text = "What? ERR0R??";
            }
            foreach (Control c in this.Controls)
            {
                Console.WriteLine(c);
                if (c is PictureBox)
                {
                    pic = c as PictureBox;
                    pic.Image = Image.FromFile("../../Resources/"+ pic.Name.Substring(0, pic.Name.Length - 1) + rng.Next(1, 4).ToString() + ".jpg");
                }
            }
            resultsWrite();
        }
        private void menuFile_Select(object sender, EventArgs e)
        {
            this.Close();
        }
        private int win()
        {
            if (choice[0] == choice[1])
            {
                return 2;
            }
            else if (choice[0] == "rock")
            {
                if (choice[1] == "paper")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (choice[0] == "paper")
            {
                if (choice[1] == "scissors")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (choice[0] == "scissors")
            {
                if (choice[1] == "rock")
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            return 4;

        }
        private void hiSh2player()
        {
            bot = !bot;
            foreach (Control c in this.Controls)
            {
                Console.WriteLine(c);
                if (c is PictureBox)
                {
                    pic = c as PictureBox;
                    if (pic.Name.Substring(pic.Name.Length - 1)== "2" && bot==false)
                    {
                        pic.Show();
                    }
                    else if (pic.Name.Substring(pic.Name.Length - 1) == "2" && bot == true)
                    {
                        pic.Hide();
                    }
                }
            }
        }
        private void resultsWrite()
        {
            using (StreamWriter file = new StreamWriter(@"../../Resources/results.txt"))
            {
                foreach (var item in results)
                {
                    file.WriteLine(item);
                }
            }
        }
        private void resultsRead()
        {
            using (StreamReader file = new StreamReader(@"../../Resources/results.txt"))
            {
                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = file.ReadLine();
                }
            }
        }
        private void resultArray()
        {

        }
    }
}
