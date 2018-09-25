using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {   
        private byte[] rgbValues;
        private byte[] disp_rgbValues;
        private byte[] hsvValues;
        private byte[] disp_hsvValues;
        private Bitmap bmp;
        private int avg_h = 0;
        private int avg_s = 0;
        private  int avg_v = 0;

        private int cavg_h = 0;
        private int cavg_s = 0;
        private int cavg_v = 0;

        private bool activate_flag;


        public Form1()
        {
            InitializeComponent();
            activate_flag = false;
        }

        private void load_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private byte[] GetHSV(byte _r, byte _g, byte _b) {
            double r = (double)_r / 255;
            double g = (double)_g / 255;
            double b = (double)_b / 255;

            double mx = Math.Max(r, Math.Max(g, b));
            double mn = Math.Min(r, Math.Min(g, b));
            byte[] res = new byte[3];
            double H= 0;
            if (mx == mn)
                H = 0;
            else if (mx == r && g >= b)
                H = 60 *(g - b)/(mx - mn);
            else if (mx == r && g < b)
                H = 60 * (g - b) / (mx - mn)+360;
            else if (mx == g)
                H = 60 * (b - r) / (mx - mn)+120;
            else if (mx == b)
                H = 60 * (r - g) / (mx - mn)+240;

            double S;
            if (mx == 0)
                S = 0;
            else
                S = 1 - mn / mx;

            double V = mx;

            res[0] = (byte)(H/360*255);
            res[1] = (byte)(S * 255);
            res[2] = (byte)(V * 255);
            return res;
        }


        private void GetDefValues() {
           // var g = Graphics.FromImage(pictureBox1.Image);
            bmp = pictureBox1.Image as Bitmap;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            rgbValues = new byte[bytes];
            hsvValues = new byte[bytes];
            disp_hsvValues = new byte[bytes];
            disp_rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            System.Runtime.InteropServices.Marshal.Copy(ptr, disp_rgbValues, 0, bytes);
            bmp.UnlockBits(bmpData);
            avg_h = 0;
             avg_s = 0;
             avg_v = 0;

            // Set every third value to 255. A 24bpp bitmap will look red.  
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                // rgbValues[counter] = 255;      // blue
                // rgbValues[counter+1] = 255; // green
                // rgbValues[counter+2] = 255;   // red


                byte[] hsvpixel;
                hsvpixel = GetHSV(rgbValues[counter + 2], rgbValues[counter + 1], rgbValues[counter]);
                hsvValues[counter] = hsvpixel[0]; // hue
                hsvValues[counter + 1] = hsvpixel[1]; // sat
                hsvValues[counter + 2] = hsvpixel[2]; // val

                disp_hsvValues[counter] = hsvpixel[0]; // hue
                disp_hsvValues[counter + 1] = hsvpixel[1]; // sat
                disp_hsvValues[counter + 2] = hsvpixel[2]; // val

                avg_h += hsvpixel[0];
                avg_s += hsvpixel[1];
                avg_v += hsvpixel[2];
            }
            // Copy the RGB values back to the bitmap
            //System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            

            

            avg_h /= bytes;
            avg_s /= bytes;
            avg_v /= bytes;

            

            trackBarHUE.Value = avg_h;
            trackBarSat.Value = avg_s;
            trackBarBr.Value = avg_v;

            activate_flag = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            activate_flag = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
            GetDefValues();

          

        }

        private byte[] GetRGB(byte _H, byte _S, byte _V) {
            double H = (double)_H / 255 * 360;
            double S = (double)_S / 255 * 100;
            double V = (double)_V / 255 * 100;
            int Hi = (int)H / 60 % 6;
            double Vmin = (100 - S) * V / 100;
            double a = (V - Vmin) * ((int)H % 60) / 60;
            double Vinc = Vmin + a;
            double Vdec = V - a;

            byte[] res = new byte[3];
            switch (Hi)
            {
                // b g r
                default:
                case 0:
                    res[0] = (byte)(Vmin*255 / 100);
                    res[1] = (byte)(Vinc*255 / 100);
                    res[2] = (byte)(V*255 / 100);
                    break;
                case 1:
                    res[0] = (byte)(Vmin*255 / 100);
                    res[1] = (byte)(V*255 / 100);
                    res[2] = (byte)(Vdec*255 / 100);
                    break;
                case 2:
                    res[0] = (byte)(Vinc*255 / 100);
                    res[1] = (byte)(V*255 / 100);
                    res[2] = (byte)(Vmin*255 / 100);
                    break;
                case 3:
                    res[0] = (byte)(V*255 / 100);
                    res[1] = (byte)(Vdec*255 / 100);
                    res[2] = (byte)(Vmin*255 / 100);
                    break;
                case 4:
                    res[0] = (byte)(V*255 / 100);
                    res[1] = (byte)(Vmin*255 / 100);
                    res[2] = (byte)(Vinc*255 / 100);
                    break;
                case 5:
                    res[0] = (byte)(Vdec*255 / 100);
                    res[1] = (byte)(Vmin*255 / 100);
                    res[2] = (byte)(V*255 / 100);
                    break;
                
            }
            return res;

        }

        private void trackBarHUE_ValueChanged(object sender, EventArgs e)
        {
            if (!activate_flag)
                return;
            double scale_hue = (double) trackBarHUE.Value / avg_h;
            double scale_sat = (double)trackBarSat.Value / avg_s;
            double scale_val = (double)trackBarBr.Value / avg_v;

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            System.Runtime.InteropServices.Marshal.Copy(ptr, disp_rgbValues, 0, bytes);

            

            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                // rgbValues[counter] = 255;      // blue
                // rgbValues[counter+1] = 255; // green
                // rgbValues[counter+2] = 255;   // red
                if ((int)hsvValues[counter] * scale_hue > 255)
                    disp_hsvValues[counter] = (byte)((int)(((int)hsvValues[counter]) * scale_hue) % 255);
                else
                    disp_hsvValues[counter] = (byte)(((int)hsvValues[counter]) * scale_hue);

                if ((int)hsvValues[counter + 1] * scale_sat > 255)
                    disp_hsvValues[counter + 1] = (byte)255;
                else
                    disp_hsvValues[counter + 1] = (byte)(((int)hsvValues[counter + 1]) * scale_sat);

                if ((int)hsvValues[counter + 2] * scale_val > 255)
                    disp_hsvValues[counter + 2] = (byte)255;
                else
                    disp_hsvValues[counter + 2] = (byte)(((int)hsvValues[counter + 2]) * scale_val);

                byte[] rgbpixel = GetRGB(disp_hsvValues[counter], disp_hsvValues[counter + 1], disp_hsvValues[counter + 2]);
                

                for (int i = 0; i< 3;++i)
                    disp_rgbValues[counter+i] = rgbpixel[i];
            }
            System.Runtime.InteropServices.Marshal.Copy(disp_rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            Refresh();

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.Image.Save(saveFileDialog1.FileName);
        }

        private void save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            activate_flag = false;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
           // System.Runtime.InteropServices.Marshal.Copy(ptr, disp_rgbValues, 0, bytes);
            bmp.UnlockBits(bmpData);
            for (int i = 0; i < rgbValues.Length; i++) {
                disp_rgbValues[i] = rgbValues[i];
                disp_hsvValues[i] = hsvValues[i];
            }

            trackBarHUE.Value = avg_h;
            trackBarSat.Value = avg_s;
            trackBarBr.Value = avg_v;

            activate_flag = true;
            Refresh();
        }
    }
}
