using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PainCsharp
{
    class ColumgAndString
    {
       public ProgressBar bar;
       private List<string> fileForColor = new List<string>();
        private List<string> fileForColor1 = new List<string>();
        // по столбцам. с 0 строки до n, с 1 до n и тд
        public void ColumnEze(List<Bitmap> files2)
        {
            Color color;
            for (int i = 0;i < files2.Count;i++)//проходим по всем изображениям
            {
                for (int x = 0;x < files2[1].Height; x++ )   // Идём по столбцам ХЗ
                {
                    for(int y = 0; y < files2[1].Width; y++) //идём по строкам ХЗ
                    {
                        color = files2[i].GetPixel(y, x);
                        uint bright;
                        uint R = color.R;
                        uint G = color.G;
                        uint B = color.B;
                        bright = (R << 16) + (G << 8) + B;  //закидываем все цвета в 1 число. первые 8 бит(c 0 по 7) - B, далее G и R 
                        fileForColor.Add(bright.ToString());   // кладём это в лист
                        fileForColor1.Add(R.ToString());
                        fileForColor1.Add(G.ToString());
                        fileForColor1.Add(B.ToString());
                    }
                }
                string name = i.ToString() + ".txt";
                File.WriteAllLines(name, fileForColor);
                File.WriteAllLines(name + "333", fileForColor1);
            }


            MessageBox.Show("DONE");
        }
        public void ColumnZmey(List<Bitmap> files2)
        {

        }
        public void StringEze(List<Bitmap> files2)
        {

        }
        public void StringZmey(List<Bitmap> files2)
        {

        }
    }
}
