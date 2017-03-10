using System;
using System.Collections.Generic;
using System.Linq;
using Wolop.Graphics.Base;

namespace Wolop.Graphics.Base
{
    public class FrameBuffer
    {
        public FrameBuffer(int left, int top, int width, int height)
        {
            Instance = this;

            Left = left;
            Top = top;
            Width = width;
            Height = height;
            Clear();
        }

        public static FrameBuffer Instance { get; private set; }

        public void Clear()
        {
            Console.Clear();
            chixels = new Chixel[Width, Height];
        }

        public void Draw()
        {
            List<Chixel> chixellist = new List<Chixel>();
            for (int y = 0; y < Height; y++)
            {
                if (y < Console.WindowHeight)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (x < Console.WindowWidth)
                        {
                           chixellist.Add(chixels[x,y]);
                        }
                    }
                }
            }

            if (!chixellist.Any(x => x.Dirty)) return;
            ConsoleColor currColor = chixellist.First().ForegroundColor;
            List<Chixel> tempChixels = new List<Chixel>();
            foreach (Chixel chixel in chixellist)
            {
                if (currColor != chixel.ForegroundColor)
                {
                    PrintChixels(tempChixels, currColor);
                    tempChixels.Clear();
                    currColor = chixel.ForegroundColor;
                }
                tempChixels.Add(chixel);
            }
            PrintChixels(tempChixels, currColor);
            Console.SetCursorPosition(0, 0);
        }

        private void PrintChixels(List<Chixel> tempChixels, ConsoleColor fgColor)
        {
            Console.ForegroundColor = fgColor;
            string output = "";
            tempChixels.ForEach(x =>
            {
                output += x.Glyph;
                x.Dirty = false;
            });
            Console.Write(output);
        }


        public void DrawFrame()
        {
            var forceDirty = false;

            if ((lastWindowWidth != Console.WindowWidth) || lastWindowHeight != Console.WindowHeight)
            {
                if (Instance == this)
                {
                    Console.Clear();
                }
                forceDirty = true;

                lastWindowWidth = Console.WindowWidth;
                lastWindowHeight = Console.WindowHeight;
            }

            for (var y = 0; y < Height; y++)
            {
                if (y >= Console.WindowHeight) continue;
                for (var x = 0; x < Width; x++)
                {
                    if (x >= Console.WindowWidth) continue;
                    Chixel ch = chixels[x, y];
                    if ((ch != null) && (ch.Dirty || forceDirty))
                    {
                        Console.SetCursorPosition(x + Left, y + Top);
                        Console.ForegroundColor = ch.ForegroundColor;
                        Console.BackgroundColor = ch.BackgroundColor;
                        Console.Write(ch.Glyph);
                        ch.Dirty = false;
                    }
                }
            }
        }

        public Chixel GetChixel(int x, int y)
        {
            x -= Left;
            y -= Top;
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                //throw new Exception();
                return null;
            }

            return chixels[x, y];
        }

        public void SetChixel(int x, int y, Chixel chixel)
        {
            SetChixel(x, y, chixel.Glyph, chixel.ForegroundColor, chixel.BackgroundColor);
        }

        public void SetChixel(int x, int y, Char c, ConsoleColor fg_color = ConsoleColor.White, ConsoleColor bg_color = ConsoleColor.Black)
        {
            x -= Left;
            y -= Top;
            // check that the chixel is actually changed
            // if so, update values and set dirty

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                //throw new Exception();
                return;
            }

            Chixel ch = chixels[x, y];
            if (ch != null && ch.Glyph == c && ch.ForegroundColor == fg_color && ch.BackgroundColor == bg_color)
            {
                return;
            }


            chixels[x, y] = new Chixel(c, fg_color, bg_color);
        }

        public void Write(int x, int y, string s, ConsoleColor fg_color = ConsoleColor.White, ConsoleColor bg_color = ConsoleColor.Black)
        {
            // TODO: Detect ANSI escapes and output as a single write.

            int initX = x;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                {
                    x = initX;
                    y++;
                    continue;
                }

                //if (s[i] == '\x1b')
                // {
                //    int ansiEnd = s.IndexOf('m', i);
                //}

                SetChixel(x, y, s[i], fg_color, bg_color);
                x++;
            }
        }

        private int Left;
        private int Top;
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        private Chixel[,] chixels;

        private int lastWindowWidth = -1;
        private int lastWindowHeight = -1;

    }
}

