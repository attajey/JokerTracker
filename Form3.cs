using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace PokerTracker
{
    public partial class sessionView : Form
    {
        string game = "";
        string smallBlind = "";
        string bigBlind = "";
        string buyIn = "";
        string time = "";
        string cashOut = "";
        string description = "";

        public sessionView(String fileName)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            string directory = @"C:\\demo\\";
            string path = Path.Combine(directory, fileName.Substring(3));
            TextReader txt = new StreamReader(path);  
            
            game = txt.ReadLine();
            smallBlind = txt.ReadLine();
            bigBlind = txt.ReadLine();
            buyIn = txt.ReadLine();
            time = txt.ReadLine();
            time = txt.ReadLine();
            cashOut = txt.ReadLine();
            description = txt.ReadLine();

            smallBlind = Int32.Parse(smallBlind).ToString();
            bigBlind = Int32.Parse(bigBlind).ToString();
            buyIn = Int32.Parse(buyIn).ToString();
            cashOut = Int32.Parse(cashOut).ToString();
            int total = Int32.Parse(cashOut) - Int32.Parse(buyIn);
            double hours = Int32.Parse(time) / 60;
            double moneyPerHour = total / hours;
            double bbPerHour = moneyPerHour / Int32.Parse(bigBlind);

            this.textBox1.Text = game;
            this.textBox5.Text = Int32.Parse(smallBlind).ToString("#,##0");
            this.textBox6.Text = Int32.Parse(bigBlind).ToString("#,##0");
            this.textBox7.Text = Int32.Parse(buyIn).ToString("#,##0");
            this.textBox9.Text = Int32.Parse(cashOut).ToString("#,##0");
            this.textBox13.Text = total.ToString("#,##0");

            if (total > 0)
            {
                this.textBox13.BackColor = Color.Green;
                this.textBox13.ForeColor = Color.Black;
            }
            else if (total < 0)
            {
                this.textBox13.BackColor = Color.Red;
                this.textBox13.ForeColor = Color.Black;
            }
            if (moneyPerHour > 0)
            {
                this.textBox10.BackColor = Color.Green;
                this.textBox10.ForeColor = Color.Black;
            }
            else if (moneyPerHour < 0)
            {
                this.textBox10.BackColor = Color.Red;
                this.textBox10.ForeColor = Color.Black;

            }
            if (bbPerHour > 0)
            {
                this.textBox11.BackColor = Color.Green;
                this.textBox11.ForeColor = Color.Black;
            }
            else if (bbPerHour < 0)
            {
                this.textBox11.BackColor = Color.Red;
                this.textBox11.ForeColor = Color.Black;

            }


            if (Int32.Parse(time) == 0)//Bonus Game
            {
                this.textBox10.Text = "0";
                this.textBox11.Text = "0";
            }
            else
            {
                this.textBox10.Text = moneyPerHour.ToString("#,##0");
                this.textBox11.Text = bbPerHour.ToString("#,##0");
            }

            this.textBox14.Text = description;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
