using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Block3_v11
{
    public partial class Form1 : Form
    {
        Bitmap bmp = new Bitmap(400, 400);
        Graphics g;
        int r = 400 / 2 - 70;
        int l = 10 * 5;
        int angle = 0;
        int center = 400 / 2;
        int R;
        int accuracy = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            R = (int)Math.Sqrt((Math.Pow(l / 2, 2) + Math.Pow(r, 2)));
            Render();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            l = (int)numericUpDown1.Value * 5; 
            Render(); 
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            r = 400 / 4 + ((int)numericUpDown2.Value * 5);
            Render();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            angle = trackBar1.Value; 
            Render();
        }
        private int[] LineCoord(int angleIn, int radius, int center)
        {
            int[] coord = new int[2]; 
            angleIn %= (360 * accuracy);
            angleIn *= 1;

            if (angleIn >= 0 && angleIn <= (180 * accuracy))
            {
                coord[0] = center + (int)(radius * Math.Sin(Math.PI * angleIn / (180 * accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * accuracy)));
            }
            else
            {
                coord[0] = center - (int)(radius * -Math.Sin(Math.PI * angleIn / (180 * accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * accuracy)));
            }
            return coord;
        }
        double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }



        private void Render()
        {
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            g.Clear(Color.White);

            R = (int)Math.Sqrt((Math.Pow((l / 2), 2) + Math.Pow(r, 2))); 
            int theta = (int)RadianToDegree(Math.Atan((double)(l / 2) / r));

            int x0 = LineCoord(angle + theta, R, center)[0];
            int y0 = LineCoord(angle + theta, R, center)[1];
            int x1 = LineCoord(angle + 360 - theta, R, center)[0];
            int y1 = LineCoord(angle + 360 - theta, R, center)[1];

            if (checkBox1.Checked == true) 
            {
                g.DrawEllipse(new Pen(Color.FromArgb(0, 0, 150), 1f), center - r, center - r, r * 2, r * 2);
            }

            if (checkBox2.Checked == true)
            {
                g.DrawEllipse(new Pen(Color.FromArgb(0, 150, 0), 2f), center - R, center - R, R * 2, R * 2);
            }

            if (checkBox3.Checked == true) 
            {
                g.DrawLine(new Pen(Color.FromArgb(150, 0, 0), 3f), new Point(center, center), new Point(LineCoord(angle, r, center)[0], LineCoord(angle, r, center)[1]));
            }

            g.DrawLine(new Pen(Color.FromArgb(0, 0, 230), 4f), new Point(x0, y0), new Point(x1, y1));

            if (checkBox4.Checked == true)
            {
                g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), 1f), new Point(center, center), new Point(x0, y0));  
                g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), 1f), new Point(center, center), new Point(x1, y1)); 
            }

            g.Dispose();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Render();
        }
    }
}
