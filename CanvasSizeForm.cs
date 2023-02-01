using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Paint
{
    public partial class CanvasSizeForm : Form
    {
 
        public CanvasSizeForm()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int d, b;
            if (int.TryParse(textBox3.Text, out d) && int.TryParse(textBox2.Text, out b))
            {
                MainForm.Height = d;
                MainForm.Width = b;
                Close();
            }
            else
            {
                //invalid
                MessageBox.Show("Please enter a valid number");
                Close();
            }
        }

        private void CanvasSizeForm_Load(object sender, EventArgs e)
        {

        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
