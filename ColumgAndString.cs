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
        private List<Byte> fileForColorByte = new List<Byte>();
        private bool Mono = true;
        
        // private static List<string> SS = new List<string>();
        public byte[] AllPictures { get; set; }
        /* Объединяет все изображения в 1 массив AllPictures и записывает
         * все изображения в отдельный файл, в зависимости от типа 
         * их обработки
         * list - побитовое представление картинки
         * name - имя файла, в котором будут все изображения
         * iter - номер присланного сообщения (играет роль того, в какую строку будет 
         * писаться текущее изображение)
         */
        public void SummaOfBitmaps(List<Byte> list, string name)
        {
            AllPictures = new byte[list.Count];
            int i = 0;
            foreach (var Byte in list)
            {
                AllPictures[i] = Byte;
                ++i;
            }
            Stream fs = new FileStream(name, FileMode.Append, FileAccess.Write);
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(list.ToArray(), 0, list.Count());
            }
            fs.Close();
        }

        public void ColumnEze(Bitmap bitmap, int C)
        {
            Color color;        //для яркости пиксела
            for (int y = 0; y < 100; y++)//bitmap.Width; y++)
            {
                if (Mono == true)
                {
                    for (int x = 0; x < 100; x++)//bitmap.Height; x++)
                    {
                        color = bitmap.GetPixel(y, x);
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
                            MessageBox.Show("Изображение не монохромно");
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Изображение не монохромно");
                    break;
                }
            }

            string name = C.ToString() + " ColumnEze.bin";
            File.WriteAllBytes(name, fileForColorByte.ToArray());
            SummaOfBitmaps(fileForColorByte, "AllColumnEze.txt");
            fileForColorByte.Clear();            
            Mono = true;
        }

        public void ColumnZmey(Bitmap bitmap, int C)
        {
            Color color;
            for (int y = 0; y < bitmap.Width; y++)
            {
                if (Mono == false)  //если нашли что-то не монохромное
                    break;
                for (int x = 0; x < 100/* bitmap.Height*/; x++)
                {
                    if (Mono == false)
                        break;
                    color = bitmap.GetPixel(y, x);
                    //  Int32 bright32;
                    Int32 R = color.R; Int32 G = color.G; Int32 B = color.B;
                    if (R == G && G == B)   //для монохромный изображений
                    {
                        Byte R8 = color.R;
                        fileForColorByte.Add(R8);
                    }
                    else
                    {
                        Mono = false;           //тут может выясниться, что изображение не монохромное, поэтому выше есть проверки
                        MessageBox.Show("Изображение не монохромно");
                        fileForColorByte.Clear();
                        break;
                    }
                    if (x == 100/* bitmap.Height*/ - 1)
                    {
                        y++;
                        int xx = x;
                        for (; xx >= 0; xx--)
                        {
                            color = bitmap.GetPixel(y, xx);
                            R = color.R; G = color.G; B = color.B;
                            if (R == G && G == B)   //для монохромный изображений
                            {
                                Byte R8 = color.R;
                                fileForColorByte.Add(R8);
                            }
                            else
                            {
                                Mono = false;
                                MessageBox.Show("Изображение не монохромно");
                                fileForColorByte.Clear();
                                break;
                            }
                        }
                    }
                }
            }
            string name = C.ToString() + " ColumnZmey.txt";
            File.WriteAllBytes(name, fileForColorByte.ToArray());
           SummaOfBitmaps(fileForColorByte, "AllColumnZmey.txt");
            fileForColorByte.Clear();
        }

        public void StringEze(Bitmap bitmap, int C)
        {
            Color color;
            for (int y = 0; y < 100/*  bitmap.Height*/; y++)
            {
                if (Mono == false)
                    break;
                for (int x = 0; x < 100/*  bitmap.Width*/; x++)
                {

                    color = bitmap.GetPixel(x, y);
                    //  Int32 bright;
                    Int32 R = color.R;
                    Int32 G = color.G;
                    Int32 B = color.B;
                    if (R == G && G == B)   //для монохромный изображений
                    {
                        Byte R8 = color.R;
                        fileForColorByte.Add(R8);
                    }
                    else
                    {
                        Mono = false;
                        MessageBox.Show("Изображение не монохромно");
                        fileForColorByte.Clear();
                        break;
                    }
                }
            }
            string name = C.ToString() + " StringEze.txt";
            File.WriteAllBytes(name, fileForColorByte.ToArray());
        //    SummaOfBitmaps(fileForColorByte, "AllStringEze.txt", C);
            fileForColorByte.Clear();
            Mono = true;
        }
        public void StringZmey(Bitmap bitmap, int C)
        {
            Color color;
            for (int y = 0; y < 100/* bitmap.Height*/; y++)
            {
                if (Mono == false)
                    break;
                for (int x = 0; x < 100/*  bitmap.Width*/; x++)
                {
                    color = bitmap.GetPixel(x, y);
                    // Int32 bright;
                    Int16 R = color.R;
                    Int16 G = color.G;
                    Int16 B = color.B;
                    /* for non-mono image
                    bright = (R << 16) + (G << 8) + B; 
                    fileForColor.Add(bright.ToString()); 
                    */
                    if (R == G && G == B)   //для монохромный изображений
                    {
                        Byte R8 = color.R;
                        fileForColorByte.Add(R8);

                        if (x == 100/* bitmap.Width */- 1)
                        {
                            y++;
                            int xx = x;
                            for (; xx >= 0; xx--)
                            {
                                color = bitmap.GetPixel(xx, y);

                                R = color.R; G = color.G; B = color.B;

                                if (R == G && G == B)   //для монохромный изображений
                                {
                                    R8 = color.R;
                                    fileForColorByte.Add(R8);
                                }
                                else
                                {
                                    Mono = false;
                                    MessageBox.Show("Изображение не монохромно");
                                    fileForColorByte.Clear();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Mono = false;
                        MessageBox.Show("Изображение не монохромно");
                        fileForColorByte.Clear();
                        break;
                    }
                }
            }

            string name = C.ToString() + " StringZmey.txt";
            File.WriteAllBytes(name, fileForColorByte.ToArray());
            SummaOfBitmaps(fileForColorByte, "AllStringZmey.txt");
            fileForColorByte.Clear();
            Mono = true;
        }
    }
}
