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
using System.Collections;

namespace PainCsharp
{
    public partial class Form1 : Form
    {
        private ColumgAndString CAS = new ColumgAndString();
        public Form1()
        {
            InitializeComponent();
        }
        private List<Image> files = new List<Image>();
        private int countImages;        //количество выбранных изображений
        public int GetcountImages()
        {
            return countImages;
        }
        public void SetcountImages(int i)
        {
            countImages = i;
        }



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
        private void FromTxtToTB(object sender, EventArgs e)      //сработает для STRINGEZE       
        {
            List<byte> file1 = new List<byte>();
            int w = 320, h = 1200;
            Bitmap im = new Bitmap(w, h);
            Color color;
            int n = 0;
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                file1 = File.ReadAllBytes(openFileDialog3.FileName).ToList();
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

        private void Pixel_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

       

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text != "")
            {
                List<Image> files2 = new List<Image>();
                Bitmap bitmap;
                int Width = 0;  //ширина изображения
                int Height = 0; //высота изображения
                progressBarConvertToTxt.Value = 0;
                                //выбираем много изображений
                if (OpenManyImageForFirstStep.ShowDialog() == DialogResult.OK)
                {
                    this.OpenManyImageForFirstStep.Filter = "Images (*.BMP;*)|*.BMP";
                }
                else
                    return;
                countImages = OpenManyImageForFirstStep.FileNames.Length;
                int j = 0;
                //закидываем в новые битмапы все изображения по порядку
                foreach (string i in OpenManyImageForFirstStep.FileNames)
                {
                    files2.Add(Image.FromFile(OpenManyImageForFirstStep.FileNames[j]));
                    bitmap = new Bitmap(files2[0]);                              //добавили изображение в битмап
                    /* обрабатываем изображения */
                    //if(j > 0)
                    //{
                    //    if (Width != bitmap.Width || Height != bitmap.Height)
                    //    {
                    //        MessageBox.Show("У изображений разные размеры. Обработка прервана");
                    //        return;
                    //    }
                    //}
                    switch (int.Parse(comboBox3.Text))
                    {
                        case 1:
                            CAS.ColumnEze(bitmap, j);
                            break;
                        case 2:
                            CAS.ColumnZmey(bitmap, j);
                            break;
                        case 3:
                            CAS.StringEze(bitmap, j);
                            break;
                        case 4:
                            CAS.StringZmey(bitmap, j);
                            break;
                        default:
                            MessageBox.Show("Что-то не так с выбранным числом в комбобоксе №3");
                            break;
                    }
                    if (j == 0)
                    {
                        Width = 100/* bitmap.Width*/;
                        Height = 100/* bitmap.Height*/;
                        //progressBarConvertToTxt.Maximum = files2[0].W;
                        progressBarConvertToTxt.Visible = true;
                        progressConvertToTxt.Visible = true;
                        progressBarConvertToTxt.Maximum = countImages;
                    }
                    progressBarConvertToTxt.Value++;
                    bitmap.Dispose();
                    files2.Clear();
                    progressConvertToTxt.Text = "Изображений обработанно " + (j+1).ToString();
                    j++;
                }
                MessageBox.Show("Обработка изображений закончена");
                progressBarConvertToTxt.Visible = false;
                progressConvertToTxt.Visible = false;
            }
            else
                MessageBox.Show("Не определён способ формирования матрицы");
        }
        /* функция для расчёта одного элемента ковариационной матрицыэ
         * byte[][] nums - 2 вектора, элементы которых участвуют в расчёте
         * double[] means - средние значения всех векторов
         * int jter - столбец, в котором расположен расчитываемый элемент
         * int iter - строка, в которой расположен расчитываемый элемент
         * h - количество пикселей в изображении
         */
        private double GetCovarMatrix(byte[][] nums, double[] means, int jter, int iter, int h)
        {
            /*
              double cov = 0;
            for (int j = 0; j < countImages; j++)   
            {
                cov += ((nums[0][j] - means[iter]) * (nums[1][j] - means[jter]));
            }
            cov = (cov/(h-1));    
             */
            double cov = 0;
            for (int j = 0; j < countImages; j++)//h; j++)   
            {
                cov += ((nums[0][j] - means[iter]) * (nums[1][j] - means[jter]));
            }
            cov = (cov/(h-1));                                                     /* На что делить - кол-во изоражений, или кол-во пикселей в изображении???*/
            return cov;
        }


        public byte[,] transp(int[,] mas)
        {
            byte[,] temp = new byte[mas.GetLength(1), mas.GetLength(0)];

            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                {
                    temp[i, j] = (byte)mas[j, i];
                }
            }
            return temp;
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

        private void BuildMatrix()
        {
            double eps;
            if (Eps.Text == "")
            {
                MessageBox.Show("Введите погрешность для вычисления собственных значений и векторов матрицы");
                return;
            }
            else
            {
                try
                {
                    eps = Convert.ToDouble(Eps.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Введённое число имеет не верный формат");
                    return;
                }
            }
            Rotation MatrixForRot = new Rotation(10000, eps);     // Для расчёта собственных значений и векторов
            int h = CAS.AllPictures.Length; // количество пикселей в изображении (кол-во стобцов)
            byte[][] CompareVectors = new byte[2][]; // матрица для векторов, которые будем сравнивать и из которых будем составлять матрицу ковариаций
            for (int i = 0; i < 2; i++)
            {
                CompareVectors[i] = new byte[h];
            }

            double[] meanMas = new double[10000 /*countImages*/];            //массив для среднего значения в каждом изображении == вектор средних
            double[][] MatrixK = new double[10000][];       //ковариационная матрица
            for (int i = 0; i < 10000; i++)
                MatrixK[i] = new double[10000];

            int[,] a = new int[countImages, h];     //транспонируем матрицу, чтобы алгоритмы не менять, ибо я путаюсь постоянно
            byte[,] temp = new byte[a.GetLength(1), a.GetLength(0)];
            /* получение среднего значения для каждого пиксела*/
            using (FileStream fsSource = new FileStream("AllColumnEze.txt", FileMode.Open, FileAccess.Read))
            {
                byte[] bufer = new byte[fsSource.Length];           //массив, куда передастся файл
                fsSource.Read(bufer, 0, (int)fsSource.Length);      // передаём весь файл в массив


                for (int k = 0; k < countImages * h;) // транспонируем
                {
                    for (int i = 0; i < countImages; i++)
                        for (int j = 0; j < h; j++, k++)
                        {
                            a[i, j] = bufer[k];
                        }
                }
                temp = transp(a);
                for (int i = 0; i < temp.GetLength(0); i++)
                {
                    double mean = 0;
                    for (int j = 0; j < temp.GetLength(1); j++)
                        mean += temp[i, j];
                    meanMas[i] = mean / temp.GetLength(0);
                }
                fsSource.Close();
            }

            /* получение среднего значения для каждой строки, т.е. каждого изображения */
            //using (FileStream fsSource = new FileStream("AllColumnEze.txt", FileMode.Open, FileAccess.Read))
            //{
            //    byte[] bufer = new byte[fsSource.Length];           //массив, куда передастся файл
            //    fsSource.Read(bufer, 0, (int)fsSource.Length);      // передаём весь файл в массив
            //    for (int i = 0; i < countImages; i++)               // идёт по изображениям
            //    {
            //        double mean = 0;
            //        for (int k = i * h; k < h * (i + 1); k++)       // идёт по байтам в изображении
            //        {
            //            mean += bufer[k];
            //        }
            //        meanMas[i] = (mean / h);
            //    }
            //    fsSource.Close();
            //}
            //for (int i = 0; i < 10000; i++)   //столбец
            //{


            //using (FileStream fsSource = new FileStream("AllColumnEze.txt", FileMode.Open, FileAccess.Read))
            //{
            //    byte[] bufer = new byte[fsSource.Length];           //массив, куда передастся файл
            //    fsSource.Read(bufer, 0, (int)fsSource.Length);      // передаём весь файл в массив
            /* Чтобы взять 2 столбца-вектора из матрицы, для cov */
            for (int i = 0; i < 10000; i++)   //столбец в матрице ковариаций
            {
                for (int j = 0; j < 10000; j++) // строка в матрице ковариаций
                {
                    for (int k = 0; k < countImages; k++)     //копирование нужных элементов для расчёта
                    {
                        CompareVectors[0][k] = temp[i, k];
                        if (i == j)
                            CompareVectors[1][k] = temp[i, k];
                        else
                        {
                            CompareVectors[1][k] = temp[j, k];
                        }
                    }
                    double cov = GetCovarMatrix(CompareVectors, meanMas, j, i, h);
                    MatrixK[i][j] = cov;
                }

                /* Чтобы взять 2 вектора из матрицы, для cov */




                //for (int j = 0; j < countImages; j++)       //столбец
                //{
                //    if (i == j)
                //    {
                //        Array.Copy(bufer, h * i, CompareVectors[0], 0, h);
                //        Array.Copy(CompareVectors[0], 0, CompareVectors[1], 0, h);
                //    }
                //    else
                //    {
                //        Array.Copy(bufer, h * i, CompareVectors[0], 0, h);
                //        Array.Copy(bufer, h * j, CompareVectors[1], 0, h);
                //    }
                //    double cov = GetCovarMatrix(CompareVectors, meanMas, j, i, h);
                //    MatrixK[i][j] = cov;
                //}
                // fsSource.Close();
            }
            //    fsSource.Close();
            //}

            /* нахождение собственных чисел и векторов */
            using (StreamWriter file = new StreamWriter("Result.txt", true, System.Text.Encoding.Default))
            {
                //file.WriteLine("Матрица ковариаций:");
                //for (int i = 0; i < 10000; i++)
                //{
                //    for (int j = 0; j < 10000; j++)
                //        file.Write(MatrixK[i][j].ToString("0.000") + " ");
                //    file.WriteLine();
                //}
                double[][] MatrixResult = MatrixForRot.RotationMethod(MatrixK);
                file.WriteLine("Собственные значения:");
                for (int i = 0; i < 10000; i++)
                {
                    file.Write(MatrixResult[i][i].ToString("0.000") + " ");
                    file.WriteLine();
                }
                file.WriteLine("Собственные вектора:");
                for (int j = 0; j < 10000; j++)
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        file.Write(MatrixForRot.MatrixForOwnVectors[i][j].ToString("0.000") + " ");
                    }
                    file.WriteLine();
                }
            }
            MessageBox.Show("Матрица ковариаций посчитана и записана");

        }


        private void Matrix_Click(object sender, EventArgs e)
        {
            if (countImages < 1)
                return;
            else
                BuildMatrix();
           
        }

        /* TODO:
         * после нахождения собственных векторов и собственных числел сделай:
         * скалярное произведеие собственных векторов и исходного изобреажения
         * формула точная пока не найдена
         * в пятницу в 3 - 4 позвонить
         * кодовые числа = 
         * ряд k - кодовых чисел = кол0во собственных векторов
         * ряд первый пиксль изображения*нпервая координата k собственного вектора
         * сумма даст то число
         * для 1 строки будет набор с(k)
         * восстановление:
         */


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

