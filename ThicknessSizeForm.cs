using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class ThicknessSizeForm : Form
    {
        public ThicknessSizeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MainForm.Thickness = int.Parse(textBox1.Text);
            //Close();
            int d;
            if (int.TryParse(textBox1.Text, out d))
            {
                MainForm.Thickness = d;
                Close();
            }
            else
            {
                //invalid
                MessageBox.Show("Please enter a valid number");
                Close();
            }
        }
    }
}
