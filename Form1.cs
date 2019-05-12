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
using System.Runtime.InteropServices;

namespace PainCsharp
{
    public partial class Form1 : Form
    {

        //  public static extern int Vvod(int parm, int parm2);
        private ColumgAndString CAS = new ColumgAndString();// (progressBarConvertToTxt, progressConvertToTxt);
        private int Height { get; set; }
        private int Width { get; set; }
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
        private List<Image> files2 = new List<Image>(); // для выбора кучи изображений
        private List<Bitmap> BitImage = new List<Bitmap>(); // битмапы с изображениями из files2

        

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
            Height = b1.Height; //Это высота картинки, и наша матрица по вертикали будет состоять из точно такого же числа элементов.
            Width = b1.Width; //Это ширина картинки, т.е. число элементов матрицы по горизонтали
            progressBarConvertToTxt.Maximum = Height+2;
            //Тут мы объявляем саму матрицу в виде двумерного массива,
            Color[,] colorMatrix = new Color[Width, Height];

            //Цикл будет выполняться от 0 и до тех пор, пока y меньше height (высоты матрицы и картинки)
            //На каждой итерации увеличиваем значение y на единицу.
            for (int y = 0; y < Height; y++)
            {
                //В начале каждой итерации мы обнуляем переменные для формирования строк для файлов
                FileLine1 = string.Empty;
                FileLine2 = string.Empty;
                //А теперь сканируем горизонтальные строки матрицы:
                for (int x = 0; x < Width; x++)
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

        /* выбать первое изображение*/
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

        /* выбрать второе изображение, теперь этой кнопки нет */
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

        
        public int NameToNumber(string s, int NumOfImage)
        {
            s = s.Trim('.', 'b', 'i', 'n');
            NumOfImage = int.Parse(s);
            return NumOfImage;
        }
        /* Выводит изображение из txt файла
         * в выбранный текстбокс
         * откуда здесь брать разрешение изображения пока не знаю, только если в файле хранить
         */
        private void button4_Click(object sender, EventArgs e)      //сработает для STRINGEZE       
        {
            List<byte> file1 = new List<byte>();
            int w = 320, h = 1200;
            Bitmap im = new Bitmap(w, h);
            Color color;
            int n = 0;
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                file1 = File.ReadAllBytes(openFileDialog3.FileName).ToList();
                if (file1.Count() <= 1) return;
                if (NumberOfImage > 1)
                {
                    string s = openFileDialog3.SafeFileName;
                    NumberOfImage = NameToNumber(s, numberOfImage);
                }
                //x,y - координаты для пикселей в файле
                for (int y = 0; y < h; y++)
                {//xx - координаты для изображения, которое строим
                    for (int xx = 0; xx < w; xx++,n++)
                    {
                        byte r = file1[n];
                        byte g = r;
                        byte b = r;
                        color = Color.FromArgb(r, g, b);
                        im.SetPixel(xx, y, color);
                    }
                }
                if (int.Parse(comboBox2.Text) == 1)
                    pictureBox1.Image = im;
                if (int.Parse(comboBox2.Text) == 2)
                    pictureBox2.Image = im;
            }
        }

        /* Сохраняет изображение из выбранного текстбокса в txt*/
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
                numberOfImage = 50;
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
            CAS.label = progressConvertToTxt;
            progressConvertToTxt.Visible = false;
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
            /* Объединим полученные файлы в 1, из которого после сделаем матрицу */

        }
        /* функция для расчёта корреляционной матрицы
         * Входные параметры:
         * double[][] nums - исходная матрица
         * double[] means - среднии значения столбцов исходной матрицы
         * int n - размерность исходной матрицы
         * Возвращаемое значение:
         * double [,] corr - корреляционная матрица
         */

        private static double[][] GetCovarMatrix(double[][] nums, double[] means, long n)
        {
            /* TODO:
             * перепиши алгоритм, ибо там где выделяется дохуя памяти - vs ругается на это
             * подумай, как переделать действия в циклах так, не потребовалось выделение
             * ресурсов для связанной с нулями хери
             */
            double[][] corr = new double[n][];
            for (int i = 0; i < n; i++)
                corr[i] = new double[n];    //дохуя памяти

            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < n; k++)
                        sum += ((nums[k][i] - means[i]) * (nums[k][j] - means[j]));
                    corr[i][j] = corr[j][i] = sum / (n - 1);
                }
            return corr;
        }

        /*Строит ковариационную матрицу
         * Массив AllPictures - получен из ColumnAndString, в нём все изображения
         * записаны построчно.
         * Поскольку кол-во столбцов в несколько тысяч раз превышает
         * кол-во строк, а в итоге должна быть квадратая матрица,
         * было принято решение объявить массив из массивов nums.
         * Он будет содержать в себе AllPictures, а оставшиеся пустые
         * места заполнит 0.
         * */

        private void Matrix_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("Парм, который ты добавил: {0}", parm3.ToString());
            /* так как пока не пробовал это на нормальных данных, задаю всё "топорно"*/
            
            //double[,] nums = new double[n, n]          //исходная матрица 
            //{   { 3, 1, 2},
            //    { 4, 6, 5},
            //    { 7, 9, 8}
            //};
          //  double[,] correlation = new double[n, n];   //Корреляционная матрица
            
            long n = CAS.AllPictures.GetLongLength(1); //количество элементов в строке
            Rotation MatrixForRot = new Rotation(progressBarConvertToTxt, progressConvertToTxt, n);     // Для расчёта собственных значений и векторов
            List<double[]> list = new List<double[]>();
            long h = CAS.AllPictures.GetLongLength(0); //в столбце
            double[] copy = new double[n];  //"промежуточный массив" для нормального объявления матрицы
            double[][] nums = new double[n][]; //исходная матрица с изображениями
            //for(int i=0;i<n;i++)
            //{
            //    nums[i] = new double[n];
            //}
            for (int i = 0; i < h; i++)
            {
                nums[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    copy[j] = CAS.AllPictures[i, j];
                    if (j == n - 1)
                    {
                        copy.CopyTo(nums[i], 0);
                    }
                }
                //if (i == h - 1)
                //    for (long k = 0,l = h; k < n; k++,l++)
                //    {
                //        nums[l] = new double[n];
                //    }
            }
            double mean = 0;                           //среднее значение
            double[] meanMass = new double[n];         //массив для средних значений == вектор средних
            double[] dispmatr = new double[n];         //массив для дисперсий
            double[][] MatrixK = new double[n][];       //ковариационная матрица
            for (int i = 0; i < h; i++)
            {
                MatrixK[i] = new double[n];
            }
            /* получение среднего значения для каждого столбца */
            for (int j = 0; j < n; j++) 
            {
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    if (nums[i] == null)
                        break;
                    else
                    {
                        sum += nums[i][j];
                        mean = sum / n;
                    }
                }
                meanMass[j] = mean;
            }

            /*РАСЧЁТ ДИСПЕРСИИ*/

            for (int j = 0; j < n; j++)
            {
                double semiDispersion = 0,
                       dispersion = 0; //дисперсия для 
                for (int i = 0; i < n; i++)
                {
                    if (nums[i] != null)
                        semiDispersion += Math.Pow((nums[i][j] - meanMass[j]), 2); // тут следующая ошибка
                    else
                    {
                        // semiDispersion += Math.Pow(meanMass[j], 2);
                        semiDispersion += (Math.Pow(meanMass[j], 2)) * (n - i);
                        break;
                    }
                }
                dispersion = semiDispersion / (n-1);
                dispmatr[j] = dispersion;
            }
            // вывод дисперсии в файл:
            StreamWriter file = new StreamWriter("Results.txt");
            file.WriteLine("Дисперсии:");
            for (int i = 0; i < n; i++)
            {
                file.WriteLine(dispmatr[i]);
            }

            /* Похоже на КОВАРИАЦИОННУЮ МАТРИЦУ, А НЕ КОРРЕЛЯЦИОННУЮ */
            file.WriteLine("Ковариационная матрица:");
            // Получение ковариационной матрицы
            MatrixK = GetCovarMatrix(nums, meanMass, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    file.Write(MatrixK[i][j] + " ");
                }
                file.WriteLine();
            }
            /*
            // получение ковариационной матрицы из корреляционной
            file.WriteLine("Ковариационная матрица:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatrixK[i,j] = correlation[i,j] * Math.Sqrt(correlation[i,i] * correlation[j,j]);
                    file.Write(MatrixK[i, j] + " ");
                    //MessageBox.Show(MatrixK[i, j] + " a[" + i.ToString() + "][" + j.ToString() + "] ");
                }
                file.WriteLine();
            }*/
            //   CalcOwnValue(MatrixK, dispmatr, n);

            double[][] MatrixResult = MatrixForRot.RotationMethod(MatrixK, n);
            file.WriteLine("Собственные значения:");
            for (int i = 0; i < n; i++)
            {
                file.Write(MatrixResult[i][i].ToString("0.000") + " ");
                file.WriteLine();
            }
            file.WriteLine("Собственные вектора:");
            for (int j = 0; j < n; j++) 
            {
                for (int i = 0; i < n; i++)
                    file.Write(MatrixForRot.MatrixForOwnVectors[i][j].ToString("0.000") + " ");
                file.WriteLine();
            }
            file.Close();
        }


        //TODO: сделай матрицу КОВАРИАЦИЙ треугольной, найди собственные вектора и собственные числа
        /* поиск собственных значений
         * Входные параметры:
         * double[,] MatrixK - ковариационная матрица
         * double[] dispmatr -  дисперсии (диаганальные элементы в MatrixK)
         * int n - размерность ковариационной матрицы
         */

        /* Метод вращения Якоби */
        /*   public void CalcOwnValue(double[,] MatrixK, double[] dispmatr, int n)
            {
                unsafe
                {
                    double a = (double)n;
                    int loopNumber = 50; // количество проходов
                    double[,] OwnVectors = new double[n, n];
                    double[] OwnElements = new double[n];
                    double[] b = new double[n + n];
                    double* z = (b + a);
                    for (int i = 0; i < n; i++)
                    {
                        z[i] = 0;
                        b[i] = OwnElements[i] = MatrixK[i, i];
                        for (int j = 0; j < n; j++)
                        {
                            if (i == j)
                                OwnVectors[i, j] = 1;
                            else
                                OwnVectors[i, j] = 0;
                        }
                    }
                    for (int i = 0; i < loopNumber; i++)
                    {
                        double sm = 0;
                        for (int p = 0; p < n - 1; p++)
                        {
                            for (int q = p + 1; q < n; q++)
                            {
                                sm += Math.Abs(MatrixK[p, q]);
                            }
                        }
                        if (sm == 0) break;
                        double tresh = i < 3 ? 0.2 * sm / (n * n) : 0;
                        for (int p = 0; p < n - 1; p++)
                        {
                            for (int q = p + 1; q < n; q++)
                            {
                                double g = 1e12 * Math.Abs(MatrixK[p, q]);
                                if (i >= 3 && Math.Abs(OwnElements[p]) > g && Math.Abs(OwnElements[q]) > g) MatrixK[p, q] = 0;
                                else
                                if (Math.Abs(MatrixK[p, q]) > tresh)
                                {
                                    double theta = 0.5 * (OwnElements[q] - OwnElements[p]) / MatrixK[p, q];
                                    double t = 1 / (Math.Abs(theta) + Math.Sqrt(1 + theta * theta));
                                    if (theta < 0) t = -t;
                                    double c = 1 / Math.Sqrt(1 + t * t);
                                    double s = t * c;
                                    double tau = s / (1 + c);
                                    double h = t * MatrixK[p, q];
                                    z[p] -= h;
                                    z[q] += h;
                                    OwnElements[p] -= h;
                                    OwnElements[q] += h;
                                    MatrixK[p, q] = 0;
                                    for (int j = 0; j < p; j++)
                                    {
                                        double g1 = MatrixK[j, p];
                                        double h1 = MatrixK[j, q];
                                        MatrixK[j, p] = g1 - s * (h1 + g1 * tau);
                                        MatrixK[j, q] = h1 + s * (g1 - h1 * tau);
                                    }
                                    for (int j = p + 1; j < q; j++)
                                    {
                                        double g1 = MatrixK[p, j];
                                        double h1 = MatrixK[j, q];
                                        MatrixK[p, j] = g1 - s * (h1 + g1 * tau);
                                        MatrixK[j, q] = h1 + s * (g1 - h1 * tau);
                                    }
                                    for (int j = q + 1; j < n; j++)
                                    {
                                        double g1 = MatrixK[p, j];
                                        double h1 = MatrixK[q, j];
                                        MatrixK[p, j] = g1 - s * (h1 + g1 * tau);
                                        MatrixK[q, j] = h1 + s * (g1 - h1 * tau);
                                    }
                                    for (int j = 0; j < n; j++)
                                    {
                                        double g1 = OwnVectors[j, p];
                                        double h1 = OwnVectors[j, q];
                                        OwnVectors[j, p] = g1 - s * (h1 + g1 * tau);
                                        OwnVectors[j, q] = h1 + s * (g1 - h1 * tau);
                                    }
                                }
                            }
                        }
                        for (int p = 0; p < n; p++)
                        {
                            OwnElements[p] = (b[p] += z[p]);
                            z[p] = 0;
                        }
                    }
                }
            }*/

    }
}

