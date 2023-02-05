using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class DocumentForm : Form
    {
        private int x, y;
        public Bitmap bitmap;
        private Bitmap bitmapTemp;
        public bool WasOpened = false;
        public bool wasChanged = false;
        public string FilePath = "";
        public Tools prev_tool = MainForm.Tool;
        public SaveFileDialog saveDlg = new SaveFileDialog();
        public static Color backColor = Color.White;
        //PictureBox org;
        Image img;
        //public SaveFileDialog dlg = new SaveFileDialog();
        public DocumentForm()
        {
            InitializeComponent();
            bitmap = new Bitmap(MainForm.Width, MainForm.Height);

            pictureBox1.Image = bitmap;
            pictureBox1.Width = MainForm.Width;
            pictureBox1.Height = MainForm.Height;
            backColor = pictureBox1.BackColor;
            img = pictureBox1.Image;
        }
        public DocumentForm(Bitmap bmp, string path)
        {
            InitializeComponent();
            this.bitmap = bmp;
            WasOpened = true;
            FilePath = path;
            pictureBox1.Image = bitmap;
            pictureBox1.Width = MainForm.Width;
            pictureBox1.Height = MainForm.Height;
            backColor = pictureBox1.BackColor;
            img = pictureBox1.Image;
        }
        private void DocumentForm_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
        }
        
        private void DocumentForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                var pen = new Pen(MainForm.Color, MainForm.Thickness);
                switch (MainForm.Tool)
                {
                    case Tools.Pen:
                        bitmapTemp = (Bitmap)bitmap.Clone();
                        var g = Graphics.FromImage(bitmap);
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.DrawLine(pen, x, y, e.X, e.Y);
                        x = e.X;
                        y = e.Y;
                        pictureBox1.Image = bitmapTemp;
                        break;
                    case Tools.Eraser:
                        bitmapTemp = (Bitmap)bitmap.Clone();
                        g = Graphics.FromImage(bitmap);
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        pen.Color = backColor;
                        g.DrawLine(pen, x, y, e.X, e.Y);
                        x = e.X;
                        y = e.Y;
                        pictureBox1.Image = bitmapTemp;
                        break;
                    case Tools.Line:
                        bitmapTemp = (Bitmap)bitmap.Clone();
                        g = Graphics.FromImage(bitmapTemp);
                        g.SmoothingMode = SmoothingMode.HighQuality;

                        g.DrawLine(pen, x, y, e.X, e.Y);

                        pictureBox1.Image = bitmapTemp;
                        //x = e.X; y = e.Y;
                        break;
                    case Tools.Circle:
                        bitmapTemp = (Bitmap)bitmap.Clone();
                        g = Graphics.FromImage(bitmapTemp);
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.DrawEllipse(pen, new Rectangle(x, y, e.X - x, e.Y - y));
                        pictureBox1.Image = bitmapTemp;
                        //img = pictureBox1.Image;
                        break;
                    case Tools.Star:
                        bitmapTemp = (Bitmap)bitmap.Clone();
                        g = Graphics.FromImage(bitmapTemp);
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        PointF[] Star1 = Calculate5StarPoints(new PointF(e.X, e.Y), e.X - x, (e.X - x) / 2.5f);
                        g.DrawPolygon(pen, Star1);
                        pictureBox1.Image = bitmapTemp;
                        //img = pictureBox1.Image;
                        break;
                }

                wasChanged = true;
                pictureBox1.Invalidate();
                img = pictureBox1.Image;
            }
        }

        private void DocumentForm_Load(object sender, EventArgs e)
        {

        }

        
        private void DocumentForm_MouseUp(object sender, MouseEventArgs e)
        {
            switch (MainForm.Tool)
            {
                case Tools.Line:
                    bitmap = bitmapTemp;
                    break;
                case Tools.Circle:
                    bitmap = bitmapTemp;
                    break;
                case Tools.Star:
                    bitmap = bitmapTemp;
                    break;
                case Tools.Scroll:
                    MainForm.Tool = MainForm.Prev_Tool;
                    bitmap = (Bitmap)pictureBox1.Image;
                    break;
            }
        }
        //public void SaveAs(string path)
        //{
        //    bitmap.Save(path);
        //    WasOpened = true;
        //    FilePath = path;
        //}
        public void SaveAs(string path)
        {
            if (WasOpened)
            {
                Bitmap bmp;
               
                bmp = new Bitmap(bitmap);

                using (bmp)
                {
                    
                    bmp.Save(path);
                }
            }
            else
            {
                bitmap.Save(path);
                WasOpened = true;
                FilePath = path;
            }
           
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (wasChanged)
            {
                e.Cancel = true;
                var Closing = new ClosingForm(this);
                Closing.Show();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Height += 50;
            pictureBox1.Width += 50;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Height -= 50;
            pictureBox1.Width -= 50;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }
        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
               
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return bm;
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value != 0)
            {
                switch (MainForm.Tool)
                {
                    case Tools.Pen:
                        MainForm.Prev_Tool = Tools.Pen;
                        break;
                    case Tools.Line:
                        MainForm.Prev_Tool = Tools.Line;
                        break;
                    case Tools.Circle:
                        MainForm.Prev_Tool = Tools.Circle;
                        break;
                    case Tools.Star:
                        MainForm.Prev_Tool = Tools.Star;
                        break;
                }
                MainForm.Tool = Tools.Scroll;
                bitmapTemp = (Bitmap)bitmap.Clone();
                pictureBox1.Image = ZoomPicture(bitmapTemp, new Size(trackBar1.Value, trackBar1.Value));
                
            }
        }
        private PointF[] Calculate5StarPoints(PointF Orig, float outerradius, float innerradius)
        {
            // Define some variables to avoid as much calculations as possible
            // conversions to radians
            double Ang36 = Math.PI / 5.0;   // 36Â° x PI/180
            double Ang72 = 2.0 * Ang36;     // 72Â° x PI/180
            // some sine and cosine values we need
            float Sin36 = (float)Math.Sin(Ang36);
            float Sin72 = (float)Math.Sin(Ang72);
            float Cos36 = (float)Math.Cos(Ang36);
            float Cos72 = (float)Math.Cos(Ang72);
            // Fill array with 10 origin points
            PointF[] pnts = { Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig };
            pnts[0].Y -= outerradius;  // top off the star, or on a clock this is 12:00 or 0:00 hours
            pnts[1].X += innerradius * Sin36; pnts[1].Y -= innerradius * Cos36; // 0:06 hours
            pnts[2].X += outerradius * Sin72; pnts[2].Y -= outerradius * Cos72; // 0:12 hours
            pnts[3].X += innerradius * Sin72; pnts[3].Y += innerradius * Cos72; // 0:18
            pnts[4].X += outerradius * Sin36; pnts[4].Y += outerradius * Cos36; // 0:24 
            // Phew! Glad I got that trig working.
            pnts[5].Y += innerradius;
            // I use the symmetry of the star figure here
            pnts[6].X += pnts[6].X - pnts[4].X; pnts[6].Y = pnts[4].Y;  // mirror point
            pnts[7].X += pnts[7].X - pnts[3].X; pnts[7].Y = pnts[3].Y;  // mirror point
            pnts[8].X += pnts[8].X - pnts[2].X; pnts[8].Y = pnts[2].Y;  // mirror point
            pnts[9].X += pnts[9].X - pnts[1].X; pnts[9].Y = pnts[1].Y;  // mirror point
            return pnts;
        }
    }
}
