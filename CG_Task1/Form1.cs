using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Task1
{
	public partial class Form1 : Form
	{

		public Form1()
		{
			InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        }

        int max = -1;
        private List<byte> rgb_orig;
        private List<byte> rgb_grey1;
        private List<byte> rgb_grey2;
        private List<byte> rgb_dif;

        private void newImage(ref Bitmap bmp, ref List<byte> lst, double r, double g, double b)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color bitmapColor = bmp.GetPixel(x, y);
                    int colorGray = (int)(bitmapColor.R * r + bitmapColor.G * g + bitmapColor.B * b);
                    bmp.SetPixel(x, y, Color.FromArgb(colorGray, colorGray, colorGray));
                    lst.Add((byte)colorGray);
                }
        }

        private void difImages(ref Bitmap bmp1, ref Bitmap bmp2)
        {
            var img = pictureBox1.Image.Clone();
            Bitmap bmp = img as Bitmap;

            for (int x = 0; x < bmp1.Width; x++)
                for (int y = 0; y < bmp1.Height; y++)
                {
                    Color bitmapColor1 = bmp1.GetPixel(x, y);
                    Color bitmapColor2 = bmp2.GetPixel(x, y);

                    int clr = Math.Abs(bitmapColor1.R - bitmapColor2.R);
                    max = (clr > max) ? clr : max;

                    bmp.SetPixel(x, y, Color.FromArgb(clr, clr, clr));
                }

            for (int x = 0; x < bmp1.Width; x++)
                for (int y = 0; y < bmp1.Height; y++)
                {
                    Color bitmapColor = bmp.GetPixel(x, y);
                    Color clr = Color.FromArgb(bitmapColor.R * (200 / max), bitmapColor.G * (200 / max), bitmapColor.B * (200 / max));
                    bmp.SetPixel(x, y, clr);
                    rgb_dif.Add(clr.R);
                }
            pictureBox4.Image = bmp;
        }


		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            Bitmap bmp_orig = pictureBox1.Image as Bitmap;
           
            var img = pictureBox1.Image.Clone();
            var img1 = pictureBox1.Image.Clone();

            rgb_orig = new List<byte>();
            rgb_grey1 = new List<byte>();
            rgb_grey2 = new List<byte>();
            rgb_dif = new List<byte>();

            vals_original_im(ref bmp_orig);

            label1.Show();
            label2.Show();
            label3.Show();
            label4.Show();

            Bitmap bmp1 = img as Bitmap;
            Bitmap bmp2 = img1 as Bitmap;

            newImage(ref bmp1, ref rgb_grey1, 0.33, 0.33, 0.33);
			pictureBox2.Image = bmp1;
            newImage(ref bmp2, ref rgb_grey2, 0.299, 0.587, 0.114);
            pictureBox3.Image = bmp2;

            difImages(ref bmp1, ref bmp2);
        }

        private void vals_original_im(ref Bitmap bmp)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color bitmapColor = bmp.GetPixel(x, y);
                    rgb_orig.Add(bitmapColor.R);
                    rgb_orig.Add(bitmapColor.G);
                    rgb_orig.Add(bitmapColor.B);
                }

        }

		private void button1_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
		}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            chart1.Show();
            PictureBox pic = sender as PictureBox;
            chart1.Series["vals"].Points.Clear();
            chart1.Series["vals"].LegendText = "pixels";
            List<byte> rgb;
            switch (pic.Name)
            {
                default:
                case "pictureBox1":
                    rgb = rgb_orig;
                    break;
                case "pictureBox2":
                    rgb = rgb_grey1;
                    break;
                case "pictureBox3":
                    rgb = rgb_grey2;
                    break;
                case "pictureBox4":
                    rgb = rgb_dif;
                    break;
            }

            int beg = 0;
            int step = 20;
            while (beg <= 255)
            {
                chart1.Series["vals"].Points.AddXY(String.Format("{0} - {1}", beg, beg + step), rgb.Count(x => { return x >= beg && x <= beg + step; }));
                beg += step;
            }
        }
    }

}
