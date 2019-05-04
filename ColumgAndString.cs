using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace PainCsharp
{
    class ColumgAndString
    {
        public ProgressBar bar;
        private List<Byte> fileForColorByte = new List<Byte>();
        private bool Mono = true;
        private static List<string> SS = new List<string>();
        private Thread SumBitmaps = new Thread(new ParameterizedThreadStart(SummaOfBitmaps));

        public static void SummaOfBitmaps (Object FileName)
        {
            string fileName = (string)FileName;
            SS.Add(fileName);
            //File.ReadAllBytes(fileName);

        }
        public void ColumnEze(List<Bitmap> files2)
        {
            Color color;
            for (int i = 0; i < files2.Count; i++)
            {
                if (Mono == true)
                {
                    for (int y = 0; y < files2[1].Width; y++)
                    {
                        if (Mono == true)
                        {
                            for (int x = 0; x < files2[1].Height; x++)
                            {
                                color = files2[i].GetPixel(y, x);
                                //    Int32 bright32;
                                Int32 R = color.R; Int32 G = color.G; Int32 B = color.B;
                                if (R == G && G == B)   //для монохромный изображений
                                {
                                    Byte bright8;
                                    Byte R8 = color.R;
                                    bright8 = R8;
                                    fileForColorByte.Add(bright8);
                                }
                                else
                                {
                                    Mono = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    string name = i.ToString() + " ColumnEze.txt";
                    File.WriteAllBytes(name, fileForColorByte.ToArray());
                    SumBitmaps.Start(name);
                    fileForColorByte.Clear();
                }
                else
                {
                    fileForColorByte.Clear();
                    MessageBox.Show("Изображение не монохромно, целью были моего создания была работа с монохромными изображениями");
                    break;
                }
            }
            MessageBox.Show("DONE");
            Mono = true;
        }
        public void ColumnZmey(List<Bitmap> files2)
        {
            Color color;
            Byte bright8 = 0;
            for (int i = 0; i < files2.Count; i++)
            {
                if (Mono == true)               //проверка на монохромность
                {
                    for (int y = 0; y < files2[1].Width; y++)
                    {
                        if (Mono == false)  //если нашли что-то не монохромное
                            break;
                        for (int x = 0; x < files2[1].Height; x++)
                        {
                            if (Mono == false)
                                break;
                            color = files2[i].GetPixel(y, x);
                            //  Int32 bright32;
                            Int32 R = color.R; Int32 G = color.G; Int32 B = color.B;
                            if (R == G && G == B)   //для монохромный изображений
                            {
                                Byte R8 = color.R;
                                bright8 = R8;
                                fileForColorByte.Add(bright8);
                            }
                            else
                            {
                                Mono = false;           //тут может выясниться, что изображение не монохромное, поэтому выше есть проверки
                                break;
                            }
                            if (x == files2[1].Height - 1)
                            {
                                y++;
                                int xx = x;
                                for (; xx >= 0; xx--)
                                {
                                    color = files2[i].GetPixel(y, xx);
                                    R = color.R; G = color.G; B = color.B;
                                    if (R == G && G == B)   //для монохромный изображений
                                    {
                                        Byte R8 = color.R;
                                        bright8 = R8;
                                        fileForColorByte.Add(bright8);
                                    }
                                    else
                                    {
                                        Mono = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    string name = i.ToString() + " ColumnZmey.txt";
                    File.WriteAllBytes(name, fileForColorByte.ToArray());
                    fileForColorByte.Clear();
                    bright8 = 0;
                }
                else
                {
                    fileForColorByte.Clear();
                    MessageBox.Show("Изображение не монохромно, целью были моего создания была работа с монохромными изображениями");
                    break;
                }
            }
            MessageBox.Show("DONE");
            Mono = true;
        }
        public void StringEze(List<Bitmap> files2)
        {
            Color color;
            Byte bright8 = 0;
            for (int i = 0; i < files2.Count; i++)
            {
                if (Mono == true)
                {
                    for (int y = 0; y < files2[1].Height; y++)
                    {
                        if (Mono == false)
                            break;
                        for (int x = 0; x < files2[1].Width; x++)
                        {

                            color = files2[i].GetPixel(x, y);
                            //  Int32 bright;
                            Int32 R = color.R;
                            Int32 G = color.G;
                            Int32 B = color.B;
                            if (R == G && G == B)   //для монохромный изображений
                            {
                                Byte R8 = color.R;
                                bright8 = R8;
                                fileForColorByte.Add(bright8);
                            }
                            else
                            {
                                Mono = false;
                                break;
                            }
                        }
                    }
                    string name = i.ToString() + " StringEze.txt";
                    File.WriteAllBytes(name, fileForColorByte.ToArray());
                    fileForColorByte.Clear();
                }
                else
                {
                    fileForColorByte.Clear();
                    MessageBox.Show("Изображение не монохромно, целью были моего создания была работа с монохромными изображениями");
                    break;
                }
            }
            MessageBox.Show("DONE");
            Mono = true;
        }
        public void StringZmey(List<Bitmap> files2)
        {
            Color color;
            Byte bright8 = 0;
            for (int i = 0; i < files2.Count; i++)
            {
                if (Mono == true)
                {
                    for (int y = 0; y < files2[1].Height; y++)
                    {
                        if (Mono == false)
                            break;
                        for (int x = 0; x < files2[1].Width; x++)
                        {
                            color = files2[i].GetPixel(x, y);
                            // Int32 bright;
                            Int32 R = color.R;
                            Int32 G = color.G;
                            Int32 B = color.B;
                            /* for non-mono image
                            bright = (R << 16) + (G << 8) + B; 
                            fileForColor.Add(bright.ToString()); 
                            */
                            if (R == G && G == B)   //для монохромный изображений
                            {
                                Byte R8 = color.R;
                                bright8 = R8;
                                fileForColorByte.Add(bright8);

                                if (x == files2[1].Width - 1)
                                {
                                    y++;
                                    int xx = x;
                                    for (; xx >= 0; xx--)
                                    {
                                        color = files2[i].GetPixel(xx, y);

                                        R = color.R; G = color.G; B = color.B;

                                        if (R == G && G == B)   //для монохромный изображений
                                        {
                                            R8 = color.R;
                                            bright8 = R8;
                                            fileForColorByte.Add(bright8);
                                        }
                                        else
                                        {
                                            Mono = false;
                                            break;
                                        }
                                        /* for non-mono image
                                        bright = (R << 16) + (G << 8) + B;
                                        fileForColor.Add(bright.ToString());
                                        */
                                    }
                                }
                            }
                            else
                            {
                                Mono = false;
                                break;
                            }
                        }
                    }

                    string name = i.ToString() + " StringZmey.txt";
                    File.WriteAllBytes(name, fileForColorByte.ToArray());
                    fileForColorByte.Clear();
                    /* for non-mono image
                  File.WriteAllLines(name, fileForColor);
                  fileForColor.Clear();
                  */
                }
                else
                {
                    fileForColorByte.Clear();
                    MessageBox.Show("Изображение не монохромно, целью были моего создания была работа с монохромными изображениями");
                    break;
                }
            }
            MessageBox.Show("DONE");
            Mono = true;
        }
    }
}
