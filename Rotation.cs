using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PainCsharp
{
    class Rotation
    {
        public Rotation(int N, double Eps)
        {
            n = N;
            eps = Eps;
        }
        private static int n;
        private double eps;
        public double[][] MatrixForOwnVectors { get; set; } = new double[n][];
        public double[][] Matrix { get; set; } = new double[n][];
            
        public double[][] Multiplication(double[][] a, double[][] b)//, double[,] c)
        {
            //if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[][] r = new double[n][]; // результирющая матрица
            for (int i = 0; i < n; i++)
            {
                r[i] = new double[n];
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        r[i][j] += a[i][k] * b[k][j];
                    }
                }
            }
            return r;
        }

        /* Метод Якоби
         * Входящие значения:
         * Matrix - матрица, для которой будут искаться собственные значения и вектора
         * n - размерность матрицы
         */
        public double[][] RotationMethod(double[][] Matrix)
        {
            int iteration = -1;
            double max = 0; //текущий максимум
            int MaxI = 0, MaxJ = 0; //координаты максимума
            do
            {
                max = 0;
                /* ищём наддиаганальный элемент максимальный по модулю */
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (j <= i)     //наткнулись на диаганальный/поддиаганальный элемент 
                            continue;
                        if (max < Math.Abs(Matrix[i][j]))
                        {
                            max = Matrix[i][j];
                            MaxI = i;
                            MaxJ = j;
                        }
                    }
                }   //закончили поиск максимального элемента по модулю

                iteration++;
                /* поиск угла поворота */
                double angle = 0; //угoл поворота
                double forAtan; //Тут сохраним аргумент арктангенса
                double Cos, Sin; //Для значений sin & cos угла
                forAtan = (2 * max) / (Matrix[MaxI][MaxI] - Matrix[MaxJ][MaxJ]);
                angle = 0.5 * Math.Atan(forAtan);
                Cos = Math.Cos(angle);
                Sin = Math.Sin(angle);
                /* Строим матрицу вращения */
                double[][] RotMat = new double[n][];
                for (int i = 0; i < n; i++)
                    RotMat[i] = new double[n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == MaxI && j == MaxI)
                            RotMat[i][j] = Cos;
                        else if (i == MaxI && j == MaxJ)
                            RotMat[i][j] = -1 * Sin;
                        else if (i == MaxJ && j == MaxI)
                            RotMat[i][j] = Sin;
                        else if (i == MaxJ && j == MaxJ)
                            RotMat[i][j] = Cos;
                        else if (i == j)
                            RotMat[i][j] = 1;
                        else
                            RotMat[i][j] = 0;
                    }
                }
                /* Матрица вращения построена */
                /* Перемножаем матрицы вращений,
                 * т.к. они нужны для нахождения собственных значений
                 */
                if (iteration == 0)
                {
                    MatrixForOwnVectors = RotMat;
                }
                else
                {
                    MatrixForOwnVectors = Multiplication(MatrixForOwnVectors, RotMat);
                }

                /* Транспонирование матрицы*/
                double tmp;
                double[][] tmpMat = (double[][])RotMat.Clone();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        tmp = tmpMat[i][j];
                        tmpMat[i][j] = tmpMat[j][i];
                        tmpMat[j][i] = tmp;
                    }
                }
                /* Заканчиваем транспонирование матрицы*/
                /* Вращаем */
                double[][] SemiIT;
                SemiIT = Multiplication(tmpMat, Matrix);//, RotMat);
                Matrix = Multiplication(SemiIT, RotMat);
                /* Готово. Далее вновь сравниваем */
                //RotationMethod(Matrix, n);
            }
            while (max > eps);

            return Matrix;
        }
    }

}
