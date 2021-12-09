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
        string[] tempAr, choice = new string[2], rps = { "rock", "paper", "scissors"}, results = new string[10];
        bool bot = true,t = true;
        int count = 0;//just count through the ten
        Label lbl;
        ListBox list;
        Button btn;
        
        public Form1()
        {
            if (t)
            {
                resultsRead();
                t = !t;
            }
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
            lbl.Name = "outcome";
            this.Controls.Add(lbl);

            lbl = new Label();
            lbl.Size = new Size(140, 25);
            lbl.Location = new Point(20, 25);
            lbl.Name = "choice";
            lbl.Text = "Selected: "+Capitalize(choice[0]);
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

            btn = new Button();
            btn.Size = new Size(100,50);
            btn.Location = new Point(180, 50);
            btn.Click += Btn_Click;
            btn.Text = "Begin Battle";
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = this.BackColor;
            btn.Name = "1p";
            this.Controls.Add(btn);

            list = new ListBox();
            list.Size = new Size(120,210);
            list.Location = new Point(170, 110);
            this.Controls.Add(list);

            MainMenu menu = new MainMenu();
            MenuItem mf = new MenuItem("Settings");
            mf.MenuItems.Add("2 Player Mode", new EventHandler(botEn)).Shortcut = Shortcut.CtrlS;
            mf.MenuItems.Add("Complex Background", new EventHandler(menuFile_Select2)).Shortcut = Shortcut.CtrlD;
            mf.MenuItems.Add("Rules", new EventHandler(menuFile_Select4)).Shortcut = Shortcut.CtrlA;
            mf.MenuItems.Add("Exit", new EventHandler(menuFile_Select));
            menu.MenuItems.Add(mf);
            this.Menu = menu;
        }

        private void botEn(object sender, EventArgs e)
        {
            bot = !bot;
        }

        private void menuFile_Select4(object sender, EventArgs e)
        {
            MessageBox.Show("Paper beats Rock, Rock beats Scissors, Scissors beat Paper", "Rules of the game");
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
            if (bot==false)
            {
                hiSh2player(sender,e);
             }
           changeLabel("choice","Selected: " + Capitalize(choice[0]), bot);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (bot==true)
            {
                choice[1] = rps[rng.Next(0,3)];
            }
            if (win()==0)
            {
                changeLabel("outcome", "Opponent Win", bot);
            }
            else if (win()==1)
            {
                changeLabel("outcome", "Host Win", bot);
            }
            else if (win()==2)
            {
                changeLabel("outcome", "Stalemate", bot);
            }
            else
            {
                changeLabel("outcome", "E440R", bot);
            }
            foreach (Control c in this.Controls)
            {
                if (c is PictureBox)
                {
                    pic = c as PictureBox;
                    pic.Image = Image.FromFile("../../Resources/"+ pic.Name.Substring(0, pic.Name.Length - 1) + rng.Next(1, 4).ToString() + ".jpg");
                }
            }
            resultsWrite();
            listUpdate();
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
        private void hiSh2player(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is PictureBox)
                {
                    pic = c as PictureBox;
                    if (pic.Name.Substring(pic.Name.Length - 1) == "1" && bot == false)
                    {
                        pic.Name = pic.Name.Replace('1', '2');
                    }
                    else if (pic.Name.Substring(pic.Name.Length - 1) == "1" && bot == true)
                    {
                        pic.Name = pic.Name.Replace('2', '1');
                    }
                    //if (pic.Name.Substring(pic.Name.Length - 1)== "2" && bot==false)
                    //{
                    //    pic.Show();
                    //}
                    //else if (pic.Name.Substring(pic.Name.Length - 1) == "2" && bot == true)
                    //{
                    //    pic.Hide();
                    //}
                }
            }
        }
        private void resultsWrite()
        {
            resultArraysort();
            using (StreamWriter file = new StreamWriter(@"../../Resources/results.txt"))
            {
                foreach (var item in results)
                {
                    file.Write(item+'-');
                }
            }
        }
        private string rightLabel(string name)
        {
            foreach (Control c in this.Controls)
            {
                if (c is Label)
                {
                    if ((c as Label).Name==name)
                    {
                        return (c as Label).Text;
                    }
                }
            }
            return "Error";
        }
        private void resultsRead()
        {
            using (StreamReader file = new StreamReader(@"../../Resources/results.txt"))
            {
                tempAr= file.ReadLine().Split('-');
                for (int i = 0; i < results.Length; i++)
                {
                    results[i] = tempAr[i];
                }
                resultArraysort();
            }
        }
        private void resultArraysort()
        {
            //Array.Reverse(results);
            //Array.Copy(results, 1, results, 0, results.Length - 1);
            //results[results.Length - 1] = rightLabel("outcome");
            //for (int i = 0; i < results.Length; i++)
            //{
            //    for (int j = 0; j < results.Length; j++)
            //    {
            //        if (i == 0) 
            //        {

            //        }
            //        else
            //        {
            //            results[i] = results[i + 1];
            //        }
            //    }
            //}
        }
        private void listUpdate()
        {
            list.Items.Clear();
            tempAr = results;
            for (int i = 0; i < results.Length; i++)
            {
                list.Items.Add(results[i]);
            }
        }
        private void changeLabel(string name,string str, bool bo)
        {
            //foreach (Control c in this.Controls)
            //{
            //    if (c is Label)
            //    {
            //        Label l = c as Label;
            //        if (l.Name == name) 
            //        {
            //            if (bo == false)
            //            {
            //                l.Text = "P" + pic.Name.Substring(pic.Name.Length - 1) + " " + str;
            //            }
            //            else
            //            {
            //                l.Text = str;
            //            }
            //        }

            //    }
            //}
        }
        public string Capitalize(string word)
        {
            return word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
        }
    }
}
