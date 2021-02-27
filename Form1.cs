using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerTracker
{
    public partial class Form1 : Form
    {
        string noteText = "";
        bool flag = true;//For photo slider
        int x = 0, y = 0, x1 = 0, y1 = 0;//For photo slider positioning

        public Form1()
        {
            System.IO.Directory.CreateDirectory("C:\\demo");//Checks if the dir exists or not

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            string game = "";
            string smallBlind = "";
            string bigBlind = "";
            string buyIn = "";
            string time = "";
            string cashOut = "";
            int totalCashOut = 0;
            double totalMoneyPerHour = 0;
            double totalBigBlindPerHour = 0;
            int totalHandsPlayed = 0;
            int totalBalance = 0;
            double totalHoursPlayed = 0;
            //double[] balances = { 1 };
            List<double> balancesList = new List<double>();
            double totalBuyIn = 0;
            InitializeComponent();

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            tabPage1.Name = "sessions";
            tabPage1.Text = "Sessions";
            tabPage1.Font = new Font("Verdana", 12);
            tabPage1.Width = 100;
            tabPage1.Height = 100;      
            
            tabPage2.Name = "balance";
            tabPage2.Text = "Total Balance";
            tabPage2.Font = new Font("Verdana", 12);
            tabPage2.Width = 100;
            tabPage2.Height = 100;

            tabPage3.Name = "notes";
            tabPage3.Text = "Notes";
            tabPage3.Font = new Font("Verdana", 12);
            tabPage3.Width = 100;
            tabPage3.Height = 100;

            DirectoryInfo d = new DirectoryInfo(@"C:\\demo");//Dir info of data
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files

            int fileCounter = 0;//Number of Sessions
            int noteCounter = 0;//Number of Notes
            for (int i = 0; i < Files.Length; i++)//Getting files for Total Balance tab
            {
               // balances = new double[Files.Length];
                string directory = @"C:\\demo\\";
                string path = Path.Combine(directory, Files[i].Name);
                TextReader txt = new StreamReader(path);

                if (new FileInfo(path).Length == 0)//Deletes file if its empty
                {
                    txt.Close();
                    File.Delete(path);
                    this.Close();
                    Application.Restart();
                }
                else if (!Files[i].Name.Substring(20).Equals("_NOTE.txt"))//Checks if file is not note
                {
                    totalHandsPlayed++;
                    game = txt.ReadLine();
                    smallBlind = txt.ReadLine();
                    bigBlind = txt.ReadLine();
                    buyIn = txt.ReadLine();
                    totalBuyIn += Int32.Parse(buyIn);
                    time = txt.ReadLine();
                    time = txt.ReadLine();
                    cashOut = txt.ReadLine();
                    totalCashOut = Int32.Parse(cashOut) - Int32.Parse(buyIn);
                    balancesList.Add(totalCashOut);
                    totalBalance += Int32.Parse(cashOut) - Int32.Parse(buyIn);
                    double hours;

                    if (time.Equals("0"))//checks if its a Bonus game
                    {
                        hours = 0;
                        totalHoursPlayed += hours;
                        totalMoneyPerHour += 0;
                        totalBigBlindPerHour += 0;
                    }
                    else
                    {
                        hours = Int32.Parse(time) / 60;
                        totalHoursPlayed += hours;
                        double moneyPerHour = totalCashOut / hours;
                        double bigBlindPerHour = moneyPerHour / Int32.Parse(bigBlind);
                        totalMoneyPerHour += moneyPerHour;
                        totalBigBlindPerHour += bigBlindPerHour;
                    }
                }
            }
            foreach (FileInfo file in Files)
            {
                if (file.Name.Substring(20).Equals("_NOTE.txt"))//Check if founded file is note
                {
                    noteCounter++;
                    string str1 = "";
                    str1 = str1 + noteCounter.ToString() + ". " + file.Name;
                    listBox2.Items.Add(str1);
                }
                else
                {
                    fileCounter++;
                    string str = "";
                    str = str + fileCounter.ToString() + ". " + file.Name + "_Balance : " + balancesList[fileCounter-1];
                    listBox1.Items.Add(str);
                }
            }

            this.textBox5.Text = totalBalance.ToString("#,##0");
            this.textBox6.Text = (totalMoneyPerHour / totalHandsPlayed).ToString("#,##0");
            this.textBox7.Text = (totalBigBlindPerHour / totalHandsPlayed).ToString("#,##0");
            this.textBox8.Text = totalHandsPlayed.ToString("#,##0");
            this.textBox11.Text = (totalBalance / totalHandsPlayed).ToString("#,##0");
            this.textBox13.Text = totalHoursPlayed.ToString("#,##0");
            this.textBox15.Text = (totalHoursPlayed / totalHandsPlayed).ToString("#,##0");
            this.textBox17.Text = (totalBuyIn / totalHandsPlayed).ToString("#,##0");
        }

        private void button1_Click(object sender, EventArgs e)//New Game Button
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            Application.Restart();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//Session List Selected
        {
            string temp = listBox1.SelectedItem.ToString();
            int i = temp.IndexOf("_Balance : ");
            temp = temp.Substring(0, i);
            var form3 = new sessionView(temp);
            form3.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)//Exit in Menu
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)//Help in Menu
        {
        
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)//About in Menu
        {
            About f4 = new About();
            f4.ShowDialog();
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)//Contact Us in Menu
        {
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)//Photo Slider
        {
            x1 = pictureBox2.Location.X;
            y1 = pictureBox2.Location.Y;
            x = pictureBox1.Location.X;
            y = pictureBox1.Location.Y;

            if (flag)
            {
                pictureBox1.Location = new Point(x + 812, y);
                pictureBox2.Location = new Point(x1 + 600, y1);
                flag = !flag;
            }
            else if (!flag)
            {
                pictureBox1.Location = new Point(x - 812, y);
                pictureBox2.Location = new Point(x1 - 600, y1);
                flag = !flag;
            }
        }

        private void button2_Click(object sender, EventArgs e)//Add New Note Button
        {
            Form6 f6 = new Form6();
            f6.ShowDialog();
            Application.Restart();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)//Notes List Select
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\\demo");//Getting dir info
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            for (int i = 0; i < Files.Length; i++)                  
            {
                if (Files[i].Name.Substring(20).Equals("_NOTE.txt"))//Checks if its a note file
                {
                    if (Files[i].Name.Equals(listBox2.Items[listBox2.SelectedIndex].ToString().Substring(3)))
                    {
                        TextReader noteTxt = new StreamReader(Files[i].FullName);
                        noteText = noteTxt.ReadToEnd();
                        MessageBox.Show(noteText);
                    }               
                }
            }
        }
    }
}
