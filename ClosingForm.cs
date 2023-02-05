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
    public partial class ClosingForm : Form
    {
        private DocumentForm currentForm;
        public ClosingForm(DocumentForm d)
        {
            currentForm = d;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap currentBitmap = currentForm.bitmap;
            if (currentForm != null && !(currentForm.WasOpened))
            {
                var dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.Filter = " Файлы JPEG (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    currentBitmap.Save(dlg.FileName);
                    currentForm.WasOpened = true;
                    currentForm.FilePath = dlg.FileName;
                    currentForm.wasChanged = false;
                    currentForm.Text = dlg.FileName.Split('\\').Last();
                    currentForm.saveDlg = dlg;
                }
            }
            else if (currentForm != null && currentForm.WasOpened)
            {
                currentForm.bitmap.Save(currentForm.saveDlg.FileName);
                currentForm.WasOpened = true;
                currentForm.wasChanged = false;
            }
            currentForm.Close();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentForm.wasChanged = false;
            currentForm.Close();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
