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
    public partial class Form6 : Form
    {
        static DateTime currentDateTime = DateTime.Now;
        static String dateStr = currentDateTime.ToString("yyyy-MM-dd HH_mm_ss") + " _NOTE" + ".txt";
        static string directory = @"C:\\demo\\";
        static string path = Path.Combine(directory, dateStr);
        TextWriter txtNote = new StreamWriter(path);
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNote.WriteLine(textBox1.Text);
            txtNote.Close();
            this.Close();
        }
    }
}
