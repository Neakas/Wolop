using System;
using System.Threading;
using Wolop.Graphics.Base;

namespace Wolop
{
    public class Engine
    {
        public Engine()
        {
            FrameBuffer frameBuffer = new FrameBuffer(0, 0, Console.WindowWidth, Console.WindowHeight - 1);
            GraphicProcessor processor = new GraphicProcessor(frameBuffer);
            processor.Run();

            char ch = '#';
            int x = 0;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo ck = Console.ReadKey(true);
                    ch = ck.KeyChar;

                    if (ck.Key == ConsoleKey.B)
                    {
                        x++;
                    }
                }

                int width = Console.WindowWidth;
                int height = Console.WindowHeight;
                ConsoleColor color = ConsoleColor.Black;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (j == x)
                        {
                            frameBuffer.Write(i, j, ch.ToString(), ConsoleColor.Red);
                        }
                        else
                        {
                            frameBuffer.Write(i, j, ch.ToString(), ConsoleColor.DarkGray);
                        }
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}
