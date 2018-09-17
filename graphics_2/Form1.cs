using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphics_2
{

    enum rgb_color { Red, Green, Blue }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        Bitmap bmp;

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image = Bitmap.FromFile(openFileDialog1.FileName);
            bmp = pictureBox1.Image as Bitmap;
        }


        private void take_one_color(Bitmap bmp, rgb_color chosen_color)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = bmp.Clone() as Image;
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        var color = bmp.GetPixel(x, y);

                        if (chosen_color == rgb_color.Red)
                            (pictureBox1.Image as Bitmap).SetPixel(x, y, Color.FromArgb(color.A, color.R, 0, 0));
                        else if (chosen_color == rgb_color.Green)
                            (pictureBox1.Image as Bitmap).SetPixel(x, y, Color.FromArgb(color.A, 0, color.G, 0));
                        else if (chosen_color == rgb_color.Blue)
                            (pictureBox1.Image as Bitmap).SetPixel(x, y, Color.FromArgb(color.A, 0, 0, color.B));
                    }
                }
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            take_one_color(bmp, rgb_color.Red);
            pictureBox1.Refresh();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            take_one_color(bmp, rgb_color.Green);
            pictureBox1.Refresh();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            take_one_color(bmp, rgb_color.Blue);
            pictureBox1.Refresh();
        }


        Bitmap[] res = new Bitmap[3];

        private void barchart()
        {
            if (pictureBox1.Image != null)
            {
                for (int k = 0; k < 3; k++)
                {
                    res[k] = new Bitmap(768, 600);
                }

                int width = 768, height = 600;

                int[] R = new int[256];
                int[] G = new int[256];
                int[] B = new int[256];
                int i, j;
                Color color;
                for (i = 0; i < bmp.Width; ++i)
                    for (j = 0; j < bmp.Height; ++j)
                    {
                        color = bmp.GetPixel(i, j);
                        ++R[color.R];
                        ++G[color.G];
                        ++B[color.B];
                    }
                int max = 0;
                for (i = 0; i < 256; ++i)
                {
                    if (R[i] > max)
                        max = R[i];
                    if (G[i] > max)
                        max = G[i];
                    if (B[i] > max)
                        max = B[i];
                }
                double point = (double)max / height;
                for (i = 0; i < width - 3; ++i)
                {
                    for (j = height - 1; j > height - R[i / 3] / point; j--)
                    {
                        res[0].SetPixel(i, j, Color.Red);
                    }
                    i++;
                    for (j = height - 1; j > height - G[i / 3] / point; j--)
                    {
                        res[1].SetPixel(i, j, Color.Green);
                    }
                    i++;
                    for (j = height - 1; j > height - B[i / 3] / point; j--)
                    {
                        res[2].SetPixel(i, j, Color.Blue);
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            barchart();
            pictureBox2.Image = res[0];
            pictureBox3.Image = res[1];
            pictureBox4.Image = res[2];
        }

        
    }
}
