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
using System.Globalization;

namespace PokerTracker
{
    public partial class Form2 : Form
    {
        int cashOut = 0;
        bool flag; //0=sb 1=bb 2=bi
        string comboBoxItem = "";

        Stopwatch stopwatch = new Stopwatch();
        //Making a new file for the new session
        static DateTime currentDateTime = DateTime.Now;
        static String dateStr = currentDateTime.ToString("yyyy-MM-dd HH_mm_ss") + ".txt";
        static string directory = @"C:\\demo\\";
        static string path = Path.Combine(directory, dateStr);
        TextWriter txt = new StreamWriter(path);
        //File opened for writing  
        public Form2()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.button2.Visible = false;//Unpause Button      
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] games = { "Hold'em", "Omaha", "Omaha-5", "Bonus" };

            if (comboBox1.SelectedIndex == 0)//holdem
            {
                flag = true;
                comboBoxItem = games[0];
            }
            else if (comboBox1.SelectedIndex == 1)//omaha
            {
                flag = true;
                comboBoxItem = games[1];
            }
            else if (comboBox1.SelectedIndex == 2)//omaha-5
            {
                flag = true;
                comboBoxItem = games[2];
            }
            else if (comboBox1.SelectedIndex == 3)//bonus
            {
                flag = false;             
                comboBoxItem = games[3];
                this.textBox5.Dispose();
                this.textBox6.Dispose();
                this.textBox7.Dispose();
                this.timeBox.Dispose();
                this.startButton.Dispose();
                this.finishButton.Dispose();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            timeBox.Text = "Counting...";
        }

        private void finishButton_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds,
                stopwatch.Elapsed.Milliseconds / 10);
            timeBox.Text = elapsedTime;
            stopwatch.Restart();
            MessageBox.Show("Session Save Succesfully. Enter Your Cash Out!");
        }
        private void button1_Click(object sender, EventArgs e)//Pause Button
        {
            stopwatch.Stop();
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds,
                stopwatch.Elapsed.Milliseconds / 10);
            timeBox.Text = elapsedTime;
            button1.Visible = false;
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)//Unpause Button
        {
            stopwatch.Start();
            button2.Visible = false;
            button1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)//Done Button
        {
            int i;
            if (!int.TryParse(textBox1.Text, out i))
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            else
            {
                if (flag == false)//Bonus Game
                {
                    txt.WriteLine(comboBoxItem);//game
                    txt.WriteLine(0);//small blind
                    txt.WriteLine(0);//big blind
                    txt.WriteLine(0);//buy in
                    txt.WriteLine(0);//time
                    txt.WriteLine(0);//time in mins
                    txt.WriteLine(0);//cash out                    
                }
                else
                {
                    int smallBlind = Int32.Parse(this.textBox5.Text);
                    int bigBlind = Int32.Parse(this.textBox6.Text);
                    int buyIn = Int32.Parse(this.textBox7.Text);
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        stopwatch.Elapsed.Hours, stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds,
                        stopwatch.Elapsed.Milliseconds / 10);

                    txt.WriteLine(comboBoxItem);//game               
                    txt.WriteLine(smallBlind);//small blind
                    txt.WriteLine(bigBlind);//big blind 
                    txt.WriteLine(buyIn);//buy in    
                    txt.WriteLine(elapsedTime);//time
                    txt.WriteLine(stopwatch.Elapsed.Minutes);//time in mins                    
                }
                cashOut = Int32.Parse(this.textBox1.Text); //We have cash-out in both Bonus & other games
                txt.WriteLine(cashOut);//cash out
                if (textBox3.Text.Equals(""))
                {
                    txt.WriteLine("No Notes");//description
                }
                else
                {
                    txt.WriteLine(textBox3.Text);//description
                }
                txt.Close();
                this.Close();
            }         
        }      
    }
}
