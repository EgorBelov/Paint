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
            MainForm.Height = int.Parse(textBox3.Text);
            MainForm.Height = int.Parse(textBox2.Text);
            Close();
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
