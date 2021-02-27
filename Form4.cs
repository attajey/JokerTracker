using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerTracker
{
    public partial class About : Form
    {
        public About()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            string nl = "\r\n";
            this.richTextBox1.Text = "This is a Poker Tracker Program named after two individuals, Foko & Jimi. " +
                nl + nl + "The purpose of this program is to help poker players keep track of their games and make desicions based on these informations. " +
                nl + nl + "All the data bases are saved as a Text file in your C directory. " + 
                nl + nl + "As this is a Beta version of this program, we'll be more glad to hear about the bugs you'll find or any recommendations that you have ( Go to Contact Us Menu ) ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void About_Load(object sender, EventArgs e)
        {

        }
    }
   
}
