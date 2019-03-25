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
    public partial class Form1 : Form
    {
        private ColumgAndString CAS = new ColumgAndString();
        private int numberOfImage = 0;
        private int numberOfImage2 = 0;
        public int NumberOfImage
        {
            get
            { return numberOfImage; }
            set
            { numberOfImage = value; }
        }
        public int NumberOfImage2
        {
            get
            { return numberOfImage2; }
            set
            { numberOfImage2 = value; }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private List<Image> files = new List<Image>();
        private List<Image> files2 = new List<Image>();
        private List<Bitmap> BitImage = new List<Bitmap>();

        

        public Image Histogramma(Image PictureOne)
        {
            Bitmap barChart = null;
            Bitmap PictureOne1 = new Bitmap(PictureOne);
            barChart = new Bitmap(PictureOne.Width, PictureOne.Height);
            int[] R = new int[256];
            int[] G = new int[256];
            int[] B = new int[256];
            int i, j;
            Color color;
            for (i = 0; i < PictureOne.Width; ++i)
                for (j = 0; j < PictureOne.Height; ++j)
                {
                    color = PictureOne1.GetPixel(i, j);
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
            double point = (double)max / PictureOne.Height;
            for (i = 0; i < PictureOne.Width - 3; ++i)
            {
                for (j = PictureOne.Height - 1;
                    j > PictureOne.Height - R[i / 4] / point;
                    --j)
                {
                    barChart.SetPixel(i, j, Color.Red);
                }
                ++i;
                for (j = PictureOne.Height - 1; j > PictureOne.Height - G[i / 4] / point; --j)
                {
                    barChart.SetPixel(i, j, Color.Green);
                }
                ++i;
                for (j = PictureOne.Height - 1; j > PictureOne.Height - B[i / 4] / point; --j)
                {
                    barChart.SetPixel(i, j, Color.Blue);
                }
            }
            return barChart;
        }

        public void FromImageToTxt(Image image)
        {
            progressConvertToTxt.Visible = true;
            progressBarConvertToTxt.Visible = true;
            //Объявляем переменную для формирования строки первой матрицы в текстовом представлении (значения по оси Х картинки)
            string FileLine1 = string.Empty;
            //Объявляем список строк, в который будем построчно добавлять матрицу в текстовом виде
            List<string> file1 = new List<string>();

            //Объявляем переменную для формирования строки матрицы, в которую будем писать значения цветов в формате ARGB
            string FileLine2 = string.Empty;
            //Список строк для формирования второго файла
            List<string> file2 = new List<string>();

            Bitmap b1 = new Bitmap(image);

            //Объявляем переменные для значений высоты и ширины матрицы (картинки)...
            //...и тут же задаем значения этих переменных взяв их из высоты и ширины картинки в пикселях
            int height = b1.Height; //Это высота картинки, и наша матрица по вертикали будет состоять из точно такого же числа элементов.
            int width = b1.Width; //Это ширина картинки, т.е. число элементов матрицы по горизонтали
            progressBarConvertToTxt.Maximum = height+2;
            //Тут мы объявляем саму матрицу в виде двумерного массива,
            Color[,] colorMatrix = new Color[width, height];

            //Цикл будет выполняться от 0 и до тех пор, пока y меньше height (высоты матрицы и картинки)
            //На каждой итерации увеличиваем значение y на единицу.
            for (int y = 0; y < height; y++)
            {
                //В начале каждой итерации мы обнуляем переменные для формирования строк для файлов
                FileLine1 = string.Empty;
                FileLine2 = string.Empty;
                //А теперь сканируем горизонтальные строки матрицы:
                for (int x = 0; x < width; x++)
                {
                    //В матрицу добавляем цвет точки с координатами x,y из картинки b1.            
                    colorMatrix[x, y] = b1.GetPixel(x, y);
                    //А теперь преобразуем цвет точки (x,y) в:
                    //1. Текстовое представление:
                    FileLine1 += colorMatrix[x, y].ToString() + " ";
                    //2. Значение цвета в целочисленном формате:
                    FileLine2 += colorMatrix[x, y].ToArgb().ToString() + " ";
                    //Тут в обоих случаях в конечном итоге строки преобразуются к типу string для удобства их сохранения в текстовом файле
                }
                //А теперь в списки добавляем каждую из полученных строк:
                //Строка картинки в текстовом виде:
                file1.Add(FileLine1);
                //Строка картинки в виде значения цветов пикселей
                file2.Add(FileLine2);
                progressBarConvertToTxt.Value++;
            }
            //Записываем полученные результаты в текстовые файлы:
            File.WriteAllLines("text.txt", file1);
            progressBarConvertToTxt.Value++;
            File.WriteAllLines("ARGB.txt", file2);
            progressBarConvertToTxt.Value++;
            MessageBox.Show("Изображение сохранено");
            progressBarConvertToTxt.Visible = false;
            progressConvertToTxt.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                sr.Close();
                label1.Text = label1.Text + pictureBox1.Image.Width.ToString() + "x" + pictureBox1.Image.Height.ToString();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch(OutOfMemoryException mem)
            {
                MessageBox.Show("Что-то с памятью моей стало \r\n" + mem.ToString());
            }
            catch(ArgumentException arg)
            {
                MessageBox.Show("Что-то не то пошло на вход \r\n" + arg.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new
                  StreamReader(openFileDialog2.FileName);
                sr.Close();
                label2.Text = label2.Text + pictureBox2.Image.Width.ToString() + "x" + pictureBox2.Image.Height.ToString();
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                pictureBox2.Image = Image.FromFile(openFileDialog2.FileName);
            }
            catch (OutOfMemoryException mem)
            {
                MessageBox.Show("Что-то с памятью моей стало \r\n" + mem.ToString());
            }
            catch (ArgumentException arg)
            {
                MessageBox.Show("Что-то не то пошло на вход \r\n" + arg.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap PictureOne = new Bitmap(pictureBox1.Image);
                Bitmap barChart = null;
                barChart = new Bitmap(Histogramma(PictureOne));
                pictureBox3.Image = barChart;
            }
            if (pictureBox2.Image != null)
            {
                Bitmap PictureTwo = new Bitmap(pictureBox2.Image);
                Bitmap barChart2 = null;
                barChart2 = new Bitmap(Histogramma(PictureTwo));
                pictureBox4.Image = barChart2;
            }
        }
        public int NameToNumber(string s, int NumOfImage)
        {
            s = s.Trim('.', 'b', 'i', 'n');
            NumOfImage = int.Parse(s);
            return NumOfImage;
        }
        private void button4_Click(object sender, EventArgs e)      //сработает для COLUMNEZE       
        {

            List<byte> file1 = new List<byte>();
            int w = 320, h = 1200;
            Bitmap im = new Bitmap(w, h);
            Color color;
            int n = 0;
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                file1 = File.ReadAllBytes(openFileDialog3.FileName).ToList();
                if (file1.Count() < 1) return;
                if (NumberOfImage > 1)
                {
                    string s = openFileDialog3.SafeFileName;
                    NumberOfImage = NameToNumber(s, numberOfImage);
                }
                //x,y - координаты для пикселей в файле
                for (int y = 0; y < h; y++)
                {//xx - координаты для изображения, которое строим
                    for (int xx = 0; xx < 320; xx++,n++)
                    {
                        byte r = file1[n];
                        byte g = r;
                        byte b = r;
                        color = Color.FromArgb(r, g, b);
                        im.SetPixel(xx, y, color);
                        if (xx == 0 && y > 1190)
                        {

                        }
                    }
                }
                if (int.Parse(comboBox2.Text) == 1)
                    pictureBox1.Image = im;
                if (int.Parse(comboBox2.Text) == 2)
                    pictureBox2.Image = im;
            }
        }


        private void ImToTxt_Click(object sender, EventArgs e)
        {
            try
            {
                int number = int.Parse(comboBox1.Text);
                switch (number)
                {
                    case 1:
                        FromImageToTxt(pictureBox1.Image);
                        break;
                    case 2:
                        FromImageToTxt(pictureBox2.Image);
                        break;
                    default:
                        MessageBox.Show("Что-то не так с номером изображения, которое вы хотите преобразовать в txt формат");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Хэй, что-то тут не так с номером или преобразованием\n" + ex.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog4.ShowDialog() == DialogResult.OK)
            {
                this.openFileDialog4.Filter = "Images (*.BMP;*)|*.BMP";
            }
            int j = 0;
            //закидываем в новые битмапы все изображения по порядку
            foreach(string i in openFileDialog4.FileNames)
            {
                files.Add(Image.FromFile(openFileDialog4.FileNames[j]));
                BitImage.Add(new Bitmap(files[j]));
                j++;
            }
            NumberOfImage = files.Count;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(NumberOfImage > 1)
            {
                Color color;
                List<Byte> file_proba = new List<Byte>();
                int i = 1;
                //проверяем, что размеры у всех фото одинаковы
                foreach (Bitmap im in BitImage)
                {
                   // MessageBox.Show(BitImage[i].Width.ToString() + " " + BitImage[i - 1].Width.ToString() + " vs " + BitImage[i].Height.ToString() + " " + BitImage[i - 1].Height.ToString());
                    if (BitImage[i].Width != BitImage[i - 1].Width || BitImage[i].Height != BitImage[i - 1].Height)
                    {
                        MessageBox.Show("Карамба!!! У изображений разные размеры!");
                        return;
                    }
                    if(i< NumberOfImage-1)
                    i++;
                }
                i = 0;
                int Width = BitImage[1].Width; //ширина
                int Height = BitImage[1].Height; //высота
                progressBarConvertToTxt.Visible = true;
                progressBarConvertToTxt.Maximum = Width* Height*BitImage.Count();
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        for (int number = 0; number < NumberOfImage; number++) 
                        {
                            color = BitImage[number].GetPixel(x, y);
                            file_proba.Add(color.R);
                            file_proba.Add(color.G);
                            file_proba.Add(color.B);
                            i++;
                        } 
                        progressBarConvertToTxt.Value = i;
                        progressConvertToTxt.Text = "Пикселей обработано: "+i.ToString();
                       // Application.DoEvents();
                    }
                }
                progressConvertToTxt.Visible = false;
                progressBarConvertToTxt.Visible = false;
                string name = numberOfImage.ToString() + ".bin";
                File.WriteAllBytes(name, file_proba.ToArray());
                MessageBox.Show("Готово");

            } //END sizeMainLists > 0
            else
            {
                MessageBox.Show("Тысяча чертей, мне нужно больше изображений!");
                return;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
          //  if (numberOfImage < 1)
          //      numberOfImage = 50;
            try
            {
                Form2 f = new Form2(int.Parse(Pixel_number.Text), numberOfImage,this);
                f.Show();
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Не ввёл номер пикселя");
            }
            catch (FormatException)
            {
                MessageBox.Show("Что-то ты ввёл не то");
            }
        }


        private void Pixel_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

       

        private void button9_Click(object sender, EventArgs e)
        {
            //выюираем много изображений
            if (OpenManyImageForFirstStep.ShowDialog() == DialogResult.OK)
            {
                this.OpenManyImageForFirstStep.Filter = "Images (*.BMP;*)|*.BMP";
            }
            int j = 0;
            //закидываем в новые битмапы все изображения по порядку
            foreach (string i in OpenManyImageForFirstStep.FileNames)
            {
                files2.Add(Image.FromFile(OpenManyImageForFirstStep.FileNames[j]));
                BitImage.Add(new Bitmap(files2[j]));
                j++;
            }
            NumberOfImage2 = files2.Count;

            // закнончили закидывать и выбирать

            if (NumberOfImage2 > 1)
            {
             //   Color color;
             //   List<Byte> file_proba = new List<Byte>();
                int i = BitImage.Count-1;
                //проверяем, что размеры у всех фото одинаковы
                foreach (Bitmap im in BitImage)
                {
                    // MessageBox.Show(BitImage[i].Width.ToString() + " " + BitImage[i - 1].Width.ToString() + " vs " + BitImage[i].Height.ToString() + " " + BitImage[i - 1].Height.ToString());
                    if (BitImage[i].Width != BitImage[i - 1].Width || BitImage[i].Height != BitImage[i - 1].Height)
                    {
                        MessageBox.Show("Карамба!!! У изображений разные размеры!");
                        return;
                    }
                        i--;
                    if (i == 0)
                        break;
                }
                i = 0;
                //int Width = BitImage[1].Width; //ширина
                //int Height = BitImage[1].Height; //высота
                //progressBarConvertToTxt.Visible = true;
                //progressBarConvertToTxt.Maximum = Width * Height * BitImage.Count();
                //for (int number = 0; number < NumberOfImage2; number++)
                //{
                //    for (int y = 0; y < Height; y++)
                //    {
                //        for (int x = 0; x < Width; x++)
                //        {
                //                color = BitImage[number].GetPixel(x, y);
                //                file_proba.Add(color.R);
                //                file_proba.Add(color.G);
                //                file_proba.Add(color.B);
                //               // i++;
                //        }
                //    }
                //    i++;
                //    progressBarConvertToTxt.Value = i;
                //    progressConvertToTxt.Text = "Изображений обработано: " + i.ToString();
                //    string name = numberOfImage2.ToString()+" im" + ".txt";
                //    File.WriteAllBytes(name, file_proba.ToArray());
                //    Application.DoEvents();
                //}
                //progressConvertToTxt.Visible = false;
                //progressBarConvertToTxt.Visible = false;
                ////string name = numberOfImage.ToString() + ".txt";
                ////File.WriteAllBytes(name, file_proba.ToArray());
                
                //MessageBox.Show("Готово");

            } //END sizeMainLists > 0
            else
            {
                MessageBox.Show("Количество изображений нужно увеличить");
            }
            CAS.bar = progressBarConvertToTxt;

            switch (int.Parse(comboBox3.Text))
            {
                case 1:
                    CAS.ColumnEze(BitImage);
                    BitImage.Clear();
                    break;
                case 2:
                    CAS.ColumnZmey(BitImage);
                    BitImage.Clear();
                    break;
                case 3:
                    CAS.StringEze(BitImage);
                    BitImage.Clear();
                    break;
                case 4:
                    CAS.StringZmey(BitImage);
                    BitImage.Clear();
                    break;
                default:
                    MessageBox.Show("Что-то не так с выбранным числом в комбобоксе №3");
                    break;
            }
        }

        private static double[,] CalcCorr(double[,] vals, double[] means, int m, int n)
        {
            double[,] corr = new double[m, m];

            for (int i = 0; i < m; i++)
                for (int j = i; j < m; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < n; k++)
                        sum += ((vals[k, i] - means[i]) * (vals[k, j] - means[j]));

                    corr[i, j] = corr[j, i] = sum / (n - 1);
                 //   MessageBox.Show(corr[i, j].ToString());
                }

            return corr;
        }


        private void Matrix_Click(object sender, EventArgs e)
        {
            const int n = 3;
            double sr_z = 0;
            double[] srednznach = new double[n];
            double[] dispmatr = new double[n];
            double[,] nums = new double[n, n] //исходная матрица 
            {   { 5, 4, 2 },
                { 0, 1, 6 },
                { 4, 9, 3 }
            };
            double[,] MatrixK = new double[n, n];

            for (int j = 0; j < n; j++) //среднее значение для каждого столбца
            {
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    sum += nums[i, j]; //сумма эл-тов
                    sr_z = sum / n; // (сумма/числострок)
                    srednznach[j] = sr_z;
                }
                //MessageBox.Show(srednznach[j].ToString());
            }
            //дисперсия
            for (int j = 0; j < n; j++)
            {
                double nedodispersia = 0, dispersia = 0;
                for (int i = 0; i < n; i++)
                {
                    nedodispersia += System.Math.Pow((nums[i, j] - srednznach[j]), 2);
                }
                dispersia = nedodispersia / (n-1);
                //dispersia = Math.Round(dispersia);
                dispmatr[j] = dispersia;
                MessageBox.Show(dispmatr[j] + " \t");
            }

            //захуярили корреляционную матрицу
            double[,] korrel = new double[n, n];
            korrel = CalcCorr(nums, srednznach, n, n);

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    MessageBox.Show(korrel[i, j].ToString()+"  lll   ");
                }
            }

            // из коРРЕЛЯЦИОННОй захуярили коВАРИАЦИООНУЮ
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatrixK[i,j] = korrel[i,j] * Math.Sqrt(korrel[i,i] * korrel[j,j]);
                    MessageBox.Show(MatrixK[i, j].ToString() + "  000   ");
                }
            }
          

        }
    }
}

//TODO: сделай матрицу КОВАРИАЦИЙ треугольной, найди собственные вектора и собственные числа