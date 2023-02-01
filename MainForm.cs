using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class MainForm : Form
    {
        public static Color Color { get; set; }
        public static Tools Tool { get; set; }
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Thickness { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Color = Color.Black;
            Tool = Tools.Pen;
            Width = 600;
            Height = 400;
            Thickness = 1;
        }

        private void MaimForm_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new DocumentForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            var frm = new CanvasSizeForm();
            frm.MdiParent = this;
            frm.Show();
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }

        private void рисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }
        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new CanvasSizeForm();
            frm.MdiParent = this;
            frm.Show();
        }
        private void черныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Black;
        }
        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Red;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Blue;
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Green;
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                Color = cd.Color;
        }

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void penStripButton_Click(object sender, EventArgs e)
        {
            Tool = Tools.Pen;
        }

        private void circleStripButton_Click(object sender, EventArgs e)
        {
            Tool = Tools.Circle;
        }

        private void starStripButton_Click(object sender, EventArgs e)
        {
            Tool= Tools.Star;
        }

        private void backgroundStripButton_Click(object sender, EventArgs e)
        {
            //if (DocumentForm.pictureBox1 != null)
            //{
            //    DocumentForm.pictureBox1.BackColor = Color;
            //}
        }

        private void eraserStripButton_Click(object sender, EventArgs e)
        {
            //if (DocumentForm.pictureBox1 != null)
            //{
                Tool = Tools.Pen;
                Color = DocumentForm.backColor;
            //}
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg|Все файлы ()*.*|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var documentForm = new DocumentForm(new Bitmap(dlg.FileName), dlg.FileName);
                documentForm.MdiParent = this;
                documentForm.Show();
            }
            

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = ActiveMdiChild as DocumentForm;

            if (d != null && !(d.WasOpened))
            {
                var dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpg)|*.jpg";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    d.SaveAs(dlg.FileName);
                }
            }
            else if (d != null && d.WasOpened)
            {
                d.SaveAs(d.FilePath);
            }
            //DocumentForm currentform = (DocumentForm)ActiveMdiChild;

            //if (!currentform.WasOpened)
            //{
            //    currentform.WasOpened = true;
            //    SaveFileDialog dlg = new SaveFileDialog();
            //    dlg.AddExtension = true;
            //    dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpg)|*.jpg";
            //    ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg };

            //    if (dlg.ShowDialog() == DialogResult.OK)
            //    {
            //        currentform.bitmap.Save(dlg.FileName, ff[dlg.FilterIndex - 1]);
            //    }
            //    currentform.dlg = dlg;
            //}
            //else
            //{
            //    currentform.bitmap.Save(currentform.dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
            //}
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = ActiveMdiChild as DocumentForm;

            if (d != null)
            {
                var dlg = new SaveFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    d.SaveAs(dlg.FileName);
                }

            }
        }

        private void thicknessStripButton_Click(object sender, EventArgs e)
        {
            var frm = new ThicknessSizeForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var d = ActiveMdiChild as DocumentForm;

            //if (d != null)
            //{
            //    var dlg = new SaveFileDialog();
            //    if (dlg.ShowDialog() == DialogResult.OK)
            //    {
            //        d.SaveAs(dlg.FileName);
            //    }

            //}
        }

        private void lineStripButton_Click(object sender, EventArgs e)
        {
            Tool = Tools.Line;
        }
    }
}
