using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarManager
{
    enum FarMode
    {
        Explorer,
        FileReader
    }

    class FAR
    {
        Stack<Layer> layerHistory = new Stack<Layer>();
        Layer activeLayer;
        FarMode mode = FarMode.Explorer;

        public FAR(string path)
        {
            this.activeLayer = new Layer(path, 0);
        }

        public void Draw()
        {
            switch (mode)
            {
                case FarMode.Explorer:

                    DrawExplorer();

                    break;
                case FarMode.FileReader:

                    DrawFileReader();

                    break;
                default:
                    break;
            }

            DrawStatusBar();
        }

        private void DrawStatusBar()
        {
            //Console.SetCursorPosition(4, 28);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mode);
        }

        private void DrawFileReader()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(activeLayer.GetSelectedItemInfo(), FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);

                Console.WriteLine(sr.ReadToEnd());

            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open file!");

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private void DrawExplorer()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine();


            for (int i = 0; i < activeLayer.items.Count; ++i)
            {
                if (i == activeLayer.index)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                if (activeLayer.items[i] is DirectoryInfo)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }

                if (activeLayer.items[i].Name.Length <= 20)
                {
                    Console.Write(activeLayer.items[i].Name);
                }
                if (activeLayer.items[i].Name.Length > 20)
                {
                    Console.Write(activeLayer.items[i].Name.Remove(17, activeLayer.items[i].Name.Length - 17) + "...");
                }


                for (int j = 1; j <= 20 - activeLayer.items[i].Name.Length; ++j)
                {
                    Console.Write(' ');
                }

                Console.Write('|');
                Console.WriteLine("{1} - {0} - {2}", activeLayer.items[i].LastAccessTime, activeLayer.items[i].Attributes, activeLayer.items[i].LastWriteTime);
            }
        }

        public void Process(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    activeLayer.Process(-1);
                    break;
                case ConsoleKey.DownArrow:
                    activeLayer.Process(1);
                    break;
                case ConsoleKey.Enter:
                    try
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Clear();
                        if (activeLayer.items[activeLayer.index] is DirectoryInfo)
                        {
                            mode = FarMode.Explorer;
                            layerHistory.Push(activeLayer);
                            activeLayer = new Layer(activeLayer.GetSelectedItemInfo(), 0);
                        }
                        else if (activeLayer.items[activeLayer.index] is FileInfo)
                        {
                            mode = FarMode.FileReader;


                        }
                    }
                    catch (Exception e)
                    {
                        activeLayer = layerHistory.Pop();
                    }
                    break;
                case ConsoleKey.Backspace:
                    if (mode == FarMode.Explorer)
                    {
                        activeLayer = layerHistory.Pop();
                    }
                    else if (mode == FarMode.FileReader)
                    {
                        mode = FarMode.Explorer;
                    }

                    break;
                default:
                    break;
            }
        }

    }
}