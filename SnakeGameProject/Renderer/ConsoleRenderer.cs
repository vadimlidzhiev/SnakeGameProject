﻿namespace SnakeGameProject.Renderer
{
    public class ConsoleRenderer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private const int MaxColors = 8;
        private readonly ConsoleColor[] _colors;
        private readonly char[,] _pixels;
        private readonly byte[,] _pixelColors;
        private readonly int _maxWidth;
        private readonly int _maxHeight;

        public ConsoleColor BgColor { get; set; }

        public char this[int w, int h]
        {
            get { return _pixels[w, h]; }
            set { _pixels[w, h] = value; }
        }

        public ConsoleRenderer(ConsoleColor[] colors)
        {
            if (colors.Length > MaxColors)
            {
                var tmp = new ConsoleColor[MaxColors];
                Array.Copy(colors, tmp, tmp.Length);
                colors = tmp;
            }

            _colors = colors;

            _maxWidth = Console.LargestWindowWidth;
            _maxHeight = Console.LargestWindowHeight;
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
            _pixels = new char[Width, Height];
            _pixelColors = new byte[Width, Height];
        }

        public void SetPixel(int w, int h, char val, byte colorIdx)
        {
            _pixels[w, h] = val;
            _pixelColors[w, h] = colorIdx;
        }


        public void Render()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = BgColor;

            Console.SetCursorPosition(0, 0);

            var currentColor = BgColor;
            Console.ForegroundColor = currentColor;

            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    char ch = _pixels[w, h];
                    byte clrIdx = _pixelColors[w, h];

                    if (ch == 0)
                    {
                        ch = ' ';
                        clrIdx = 0;
                    }

                    var clr = _colors[clrIdx];

                    if (clr != currentColor)
                    {
                        Console.ForegroundColor = clr;
                        currentColor = clr;
                    }

                    Console.Write(ch);
                }

                if (h < Height - 1) Console.Write('\n');
            }

            Console.ResetColor();
        }

        public void DrawString(string text, int atWidth, int atHeight, ConsoleColor color)
        {
            var colorIdx = Array.IndexOf(_colors, color);
            if (colorIdx < 0)
                return;

            for (int i = 0; i < text.Length; i++)
            {
                _pixels[atWidth + i, atHeight] = text[i];
                _pixelColors[atWidth + i, atHeight] = (byte)colorIdx;
            }
        }

        public void Clear()
        {
            for (int w = 0; w < Width; w++)
                for (int h = 0; h < Height; h++)
                {
                    _pixelColors[w, h] = 0;
                    _pixels[w, h] = (char)0;
                }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ConsoleRenderer casted)
                return false;

            if (_maxWidth != casted._maxWidth || _maxHeight != casted._maxHeight ||
                Width != casted.Width || Height != casted.Height ||
                _colors.Length != casted._colors.Length)
            {
                return false;
            }


            for (int i = 0; i < _colors.Length; i++)
            {
                if (_colors[i] != casted._colors[i])
                    return false;
            }

            for (int w = 0; w < Width; w++)
                for (var h = 0; h < Height; h++)
                {
                    if (_pixels[w, h] != casted._pixels[w, h] ||
                                    _pixelColors[w, h] != casted._pixelColors[w, h])
                    {
                        return false;
                    }
                }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = HashCode.Combine(_maxWidth, _maxHeight, Width, Height);

            for (int i = 0; i < _colors.Length; i++)
            {
                hash = HashCode.Combine(hash, _colors[i]);
            }

            for (int w = 0; w < Width; w++)
                for (var h = 0; h < Height; h++)
                {
                    hash = HashCode.Combine(hash, _pixelColors[w, h], _pixels[w, h]);
                }

            return hash;
        }
    }
}