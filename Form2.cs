using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PainCsharp
{
    public partial class Form2 : Form
    {
        private Form1 object1 = new Form1();
        private Graphics g;
        private int PixelNumber;
        private int numberOfImage;
        private List<byte> file1 = new List<byte>();
        public Form2(int pixelnumber, int NumberOfImage, Form1 obj1)
        {
            object1 = obj1;
            numberOfImage = NumberOfImage;
            PixelNumber = pixelnumber;
            InitializeComponent();
            PaintGrafic();
        }

        public void PaintGrafic()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                List<byte> file1 = new List<byte>();
                file1 = File.ReadAllBytes(openFileDialog1.FileName).ToList();
                 //Application.DoEvents();
                PaintXY();
            }
        }
        public void Analiz(int pixel)
        {
            Pen RedPen = new Pen(Color.Red, 1);
            Pen GreenPen = new Pen(Color.Green, 0.01f);
            Pen BluePen = new Pen(Color.Blue, 0.01f);
            int w = 320, h = 1200;             
                int start=0;//номер байта, с которого начнём отсчёт пикселя
                int end=0;//номер байта, на котором закончится пиксель
                if(pixel==0)//для первого пикселя
                {
                    start = 0;
                    end = numberOfImage * 3;
                }
                else if(pixel !=h*w-1) //для последнего пикселя
                {
                    //start=
                }
                int R = 5;
                g.DrawLine(RedPen, new Point(numberOfImage, R), new Point(numberOfImage, R+ 5));
           
        }
        public void PaintXY()
        {
            pictureBox1.Height = 255 * 21 - 230;    //почему то только так прорисовывает нормально.
            pictureBox1.Width = numberOfImage * 21;
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(0, pictureBox1.Height); // смещение начала координат (в пикселях)
            g.RotateTransform(270);
            g.ScaleTransform(4f, 4f);
            Pen gridPen1 = new Pen(Color.Red, 0.01f);
            Pen gridPen = new Pen(Color.Black, 0.01f); //перо для отрисовки координатной сетки
            Pen penCO = new Pen(Color.Orange, 2f);  //перо для отрисовки осей
            g.DrawLine(penCO, new Point(0, 0), new Point(0, numberOfImage * 15));   //ось ОХ!!
            g.DrawLine(penCO, new Point(0, 0), new Point(255 * 5, 0));  // ось ОУ!!
            int x = 0;
            int y = 0;
            //g.DrawEllipse(penCO, x, y, 2, 2);
            while (x < (255 * 5))
            {
                x += 5;
                g.DrawLine(gridPen, new Point(x, 0), new Point(x, numberOfImage * 5+15));    // || x
            }
            while (y <= 255 * 5)
            {
                y += 5;
                g.DrawLine(gridPen, new Point(0, y), new Point(255 * 5, y));  // || y
            }
            Analiz(PixelNumber);
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            PaintXY();
        }
    }
}
