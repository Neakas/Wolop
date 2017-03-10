using System;

namespace Wolop.Graphics.Base
{
    public class Chixel
    {
        public Chixel(char glyph, ConsoleColor fg_color = ConsoleColor.White, ConsoleColor bg_color = ConsoleColor.Black)
        {
            Glyph = glyph;
            ForegroundColor = fg_color;
            BackgroundColor = bg_color;
            Dirty = true;
        }

        public Chixel(Chixel other)
        {
            Glyph = other.Glyph;
            ForegroundColor = other.ForegroundColor;
            BackgroundColor = other.BackgroundColor;
            Dirty = true;
        }

        public char Glyph { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public bool Dirty { get; set; }
    }
}

