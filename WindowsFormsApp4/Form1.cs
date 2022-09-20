using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private double A1, A2, A3;
        private double v1, v2, v3;
        private double f1, f2, f3;

      

        private double f_d;
        private int N;
        private PointF[] func_points;
        // myGraphic Exemp = new myGraphic();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Update();
            int wX1, hX1;
            Graphics graphics1 = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.DarkRed, 2f);
            //graphics1.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);//центр
            //graphics1.ScaleTransform(1, -1); //перевернуть
            wX1 = pictureBox1.Width;
            hX1 = pictureBox1.Height;

            //координатные оси:
            Pen GreenPen;
            GreenPen = new Pen(Color.Black, 2f);
            //Ось X
            Point KX1, KX2;
            KX1 = new Point(30, (hX1 - 10) / 2);
            KX2 = new Point(wX1 - 10, (hX1 - 10) / 2);
            graphics1.DrawLine(GreenPen, KX1, KX2);
            //Ось Y
            Point KY1, KY2;
            KY1 = new Point(30, 10);
            KY2 = new Point(30, hX1 - 10);
            graphics1.DrawLine(GreenPen, KY1, KY2);
            //сетка
            int padding = 10;
            int left_keys_padding = 20;//отступ для подписей по y
            //int bottom_keys_padding = 10;

            int actual_width = wX1 - 2 * padding - left_keys_padding;
            int actual_height = hX1 - 2 * padding;

            int actual_top = padding;
            int actual_bottom = actual_top + actual_height;
            int actual_left = padding + left_keys_padding;
            int actual_right = actual_left + actual_width;
            Pen GridPen;
            GridPen = new Pen(Color.Gray, 1f);
            int grid_size = 10;
            PointF K1, K2, K3, K4;
            for (double i = 0.5; i < grid_size; i += 1.0)
            {

                K1 = new PointF((float)(actual_left + i * actual_width / grid_size), actual_top);
                K2 = new PointF((float)(actual_left + i * actual_width / grid_size), actual_bottom);
                graphics1.DrawLine(GridPen, K1, K2);
                K3 = new PointF(actual_left, (float)(actual_top + i * actual_height / grid_size));
                K4 = new PointF(actual_right, (float)(actual_top + i * actual_height / grid_size));
                graphics1.DrawLine(GridPen, K3, K4);
            }


            SumSin(wX1, hX1);
            graphics1.DrawLines(pen, func_points);

        }
        public void SumSin(int wX1, int hX1)
        {
            int padding = 10;
            int left_keys_padding = 20;//отступ для подписей по y
            //int bottom_keys_padding = 10;

            int actual_width = wX1 - 2 * padding - left_keys_padding;
            int actual_height = hX1 - 2 * padding;

            int actual_top = padding;
            int actual_bottom = actual_top + actual_height;
            int actual_left = padding + left_keys_padding;
            int actual_right = actual_left + actual_width;

            DefaultParams();
            if (for_A1.Text != "" || for_A2.Text != "" || for_A3.Text != "")
            {
                A1 = Convert.ToDouble(for_A1.Text);
                A2 = Convert.ToDouble(for_A2.Text);
                A3 = Convert.ToDouble(for_A3.Text);
            }
            else { MessageBox.Show("параметры A по умолчанию", "Внимание!"); }
            if (for_v1.Text != "" || for_v2.Text != "" || for_v3.Text != "")
            {
                v1 = Convert.ToDouble(for_v1.Text);
                v2 = Convert.ToDouble(for_v2.Text);
                v3 = Convert.ToDouble(for_v3.Text);
            }
            else { MessageBox.Show("параметры v по умолчанию", "Внимание!"); }
            if (for_f1.Text != "" || for_f2.Text != "" || for_f3.Text != "")
            {
                f1 = Convert.ToDouble(for_f1.Text);
                f2 = Convert.ToDouble(for_f2.Text);
                f3 = Convert.ToDouble(for_f3.Text);
            }
            else { MessageBox.Show("параметры f по умолчанию", "Внимание!"); }
            if (for_f_d.Text != "" || for_N.Text != "")
            {
                f_d = Convert.ToDouble(for_f_d.Text);
                N = Convert.ToInt32(for_N.Text);
            }
            else { MessageBox.Show("параметры f_d,N по умолчанию", "Внимание!"); }
            func_points = new PointF[N];
            float maxY = 0;
            for (int i = 0; i < N; i++)
            {
                func_points[i] = new PointF(i, (float)(A1 * Math.Sin(2 * Math.PI * v1 * (float)i / f_d + f1) + A2 * Math.Sin(2 * Math.PI * v2 * (float)i / f_d + f2) + A3 * Math.Sin(2 * Math.PI * v3 * (float)i / f_d + f3)));
                if (maxY < Math.Abs(func_points[i].Y)) maxY = Math.Abs(func_points[i].Y);//макс значение Y
            }
            PointF actual_tb = new PointF(actual_top, actual_bottom);//для y
            PointF actual_rl = new PointF(actual_right, actual_left);//для x
            PointF from_toX = new PointF(0, (float)(N / f_d));
            PointF from_toY = new PointF(-maxY * (float)1.1, maxY * (float)1.1);
            convert_range_graph(ref func_points, actual_rl, actual_tb, from_toX, from_toY);//ref-чтобы можно было менять массив

        }
        public void DefaultParams()//параметры по умолчанию
        {
            A1 = 1;
            v1 = 1;
            f1 = 0;
            A2 = 1;
            v2 = 1;
            f2 = 0;
            A3 = 1;
            v3 = 1;
            f3 = 0;
            f_d = 5;
            N = 100;
        }
        public void convert_range_graph(ref PointF[] data, PointF actual_rl, PointF actual_tb, PointF from_toX, PointF from_toY)
        {
            //actual-размер:X-top/right Y-right,left
            //from_to: X-мин, Y-макс

            float kx = (actual_rl.X - actual_rl.Y) / (from_toX.Y - from_toX.X);
            float ky = (actual_tb.X - actual_tb.Y) / (from_toY.Y - from_toY.X);
            //foreach (PointF& item in data)
            for (int i = 0; i < data.Length; i++)
            {
                data[i].X = (data[i].X - from_toX.X) * kx + actual_rl.Y;
                data[i].Y = (data[i].Y - from_toY.X) * ky + actual_tb.Y;
            }
        }
        public void convert_range_point(PointF data, PointF actual_rl, PointF actual_tb, PointF from_toX, PointF from_toY)
        {

            float kx = (actual_rl.X - actual_rl.Y) / (from_toX.Y - from_toX.X);
            float ky = (actual_tb.X - actual_tb.Y) / (from_toY.Y - from_toY.X);
            data.X = (data.X - from_toX.X) * kx + actual_rl.Y;
            data.Y = (data.Y - from_toY.X) * ky + actual_tb.Y;

        }
    }
}
