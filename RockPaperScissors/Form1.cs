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
        PictureBox pic1,pic2,pic3;
        string[] tempAr, choice = new string[2] {"rock","rock"}, rps = { "rock", "paper", "scissors"}, results = new string[10];
        bool bot = true, turn1 = false;
        int count = 0, winEW;//just count through the ten
        Label lbl, lbl2, dummy;
        ListBox list;
        Button btn;
        
        public Form1()
        {
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

            lbl2 = new Label();
            lbl2.Size = new Size(200, 25);
            lbl2.Location = new Point(20, 25);
            lbl2.Name = "choice";
            lbl2.Text = "Selected: "+Capitalize(choice[0]);
            this.Controls.Add(lbl2);

            pic1 = new PictureBox();
            pic1.Image = Properties.Resources.rock1;
            pic1.SizeMode = PictureBoxSizeMode.StretchImage;
            pic1.Size = new Size(100, 100);
            pic1.Location = new Point(25, 70);
            pic1.Click += Pic_Click;
            pic1.Name = "rock1";
            this.Controls.Add(pic1);

            pic2 = new PictureBox();
            pic2.Image = Properties.Resources.paper1;
            pic2.SizeMode = PictureBoxSizeMode.StretchImage;
            pic2.Size = new Size(100, 100);
            pic2.Location = new Point(25, 190);
            pic2.Click += Pic_Click;
            pic2.Name = "paper1";
            this.Controls.Add(pic2);

            pic3 = new PictureBox();
            pic3.Image = Properties.Resources.scissors1;
            pic3.SizeMode = PictureBoxSizeMode.StretchImage;
            pic3.Size = new Size(100, 100);
            pic3.Location = new Point(25, 300);
            pic3.Click += Pic_Click;
            pic3.Name = "scissors1";
            this.Controls.Add(pic3);

            btn = new Button();
            btn.Size = new Size(100,50);
            btn.Location = new Point(180, 50);
            btn.Click += Btn_Click;
            btn.Text = "Begin Battle";
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = this.BackColor;
            btn.Name = "battle";
            this.Controls.Add(btn);

            list = new ListBox();
            list.Size = new Size(120,210);
            list.Location = new Point(170, 110);
            this.Controls.Add(list);

            MainMenu menu = new MainMenu();
            MenuItem mf = new MenuItem("Settings");
            mf.MenuItems.Add("2 Player Mode", new EventHandler(botEn)).Shortcut = Shortcut.CtrlS;
            mf.MenuItems.Add("Complex Background", new EventHandler(menuFile_Select1)).Shortcut = Shortcut.CtrlD;
            mf.MenuItems.Add("Rules", new EventHandler(menuFile_Select2)).Shortcut = Shortcut.CtrlA;
            mf.MenuItems.Add("Exit", new EventHandler(menuFile_Select3));
            menu.MenuItems.Add(mf);
            this.Menu = menu;
        }

        private void botEn(object sender, EventArgs e)
        {
            bot = !bot;
            if (bot == false)
            {
                btnText(1);
            }
            else
            {
                btnText(2);
            }

        }
        private void menuFile_Select1(object sender, EventArgs e)
        {
            MenuItem m = sender as MenuItem;
            m.Checked = !m.Checked;
            if (m.Checked == true)
            {
                this.BackgroundImage = Properties.Resources.back;
            }
            else
            {
                this.BackgroundImage = null;
                this.BackColor = Color.LightCyan;
            }
        }

        private void menuFile_Select2(object sender, EventArgs e)
        {
            MessageBox.Show("Paper beats Rock, Rock beats Scissors, Scissors beat Paper", "Rules of the game");
        }
        private void menuFile_Select3(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Pic_Click(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            switch (p.Name.Substring(p.Name.Length - 1))
            {
                case "1":
                    choice[0] = p.Name.Substring(0, p.Name.Length - 1);
                    lbl2.Text = "Selected: "+ Capitalize(p.Name.Substring(0, p.Name.Length - 1));
                    break;
                case "2":
                    choice[1] = p.Name.Substring(0, p.Name.Length - 1);
                    lbl2.Text = "P2 Selected: " + Capitalize(p.Name.Substring(0, p.Name.Length - 1));
                    break;
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (pic1.Name.Substring(pic1.Name.Length - 1)=="1" && bot==false)
            {
                btnText(2);
                turn1 = !turn1;
                replaceName('1', '2');
            }
            else
            {
                if (bot==true)
                {
                    choice[1] = rps[rng.Next(0,3)];
                }
                else
                {
                    replaceName('2', '1');
                }
                if (win()==0)
                {
                    lbl.Text = "Opponent Win";
                }
                else if (win()==1)
                {
                    lbl.Text = "Host Win";
                }
                else if (win()==2)
                {
                    lbl.Text = "Stalemate";
                }


                pic1.Image = Image.FromFile("../../Resources/" + pic1.Name.Substring(0, pic1.Name.Length - 1) + rng.Next(1, 4).ToString() + ".jpg");
                pic2.Image = Image.FromFile("../../Resources/" + pic2.Name.Substring(0, pic2.Name.Length - 1) + rng.Next(1, 4).ToString() + ".jpg");
                pic3.Image = Image.FromFile("../../Resources/" + pic3.Name.Substring(0, pic3.Name.Length - 1) + rng.Next(1, 4).ToString() + ".jpg");
            }

            //resultsWrite();
            //listUpdate();
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
        private void replaceName(char i, char k)
        {
            pic1.Name = pic1.Name.Replace(i, k);
            pic2.Name = pic2.Name.Replace(i, k);
            pic3.Name = pic3.Name.Replace(i, k);
        }
        private void btnText(int i)
        {
            if (i==1)
            {
                btn.Text = "Next Battle";
            }
            else
            {
                btn.Text = "Battle";
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
        private Control getFormControl(string str)
        {
            foreach (Control item in this.Controls)
            {
                if (item.Name==str)
                {
                    return item;
                }
            }
            return dummy;
            
        }
        public string Capitalize(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }
    }
}
