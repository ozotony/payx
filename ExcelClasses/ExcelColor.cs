namespace XPay.ExcelClasses
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ExcelColor
    {
        private System.Drawing.Color color;
        private ushort index;
        internal ExcelColor(System.Drawing.Color color, ushort index)
        {
            this.color = color;
            this.index = index;
        }

        public ushort Index
        {
            get
            {
                return this.index;
            }
        }
        public System.Drawing.Color Color
        {
            get
            {
                return this.color;
            }
        }
        public static ExcelColor Black
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Black, 0);
            }
        }
        public static ExcelColor White
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.White, 1);
            }
        }
        public static ExcelColor Red
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Red, 2);
            }
        }
        public static ExcelColor Green
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Green, 3);
            }
        }
        public static ExcelColor Blue
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Blue, 4);
            }
        }
        public static ExcelColor Yellow
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Yellow, 5);
            }
        }
        public static ExcelColor Magenta
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Magenta, 6);
            }
        }
        public static ExcelColor Cyan
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Cyan, 7);
            }
        }
        public static ExcelColor DarkRed
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.DarkRed, 0x10);
            }
        }
        public static ExcelColor DarkGreen
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.DarkGreen, 0x11);
            }
        }
        public static ExcelColor DarkBlue
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.DarkBlue, 0x12);
            }
        }
        public static ExcelColor Olive
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Olive, 0x13);
            }
        }
        public static ExcelColor Purple
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Purple, 20);
            }
        }
        public static ExcelColor Teal
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Teal, 0x15);
            }
        }
        public static ExcelColor Silver
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Silver, 0x16);
            }
        }
        public static ExcelColor Gray
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.Gray, 0x17);
            }
        }
        public static ExcelColor WindowText
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.FromKnownColor(KnownColor.WindowText), 0x18);
            }
        }
        public static ExcelColor WindowBackground
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.FromKnownColor(KnownColor.Window), 0x19);
            }
        }
        public static ExcelColor Automatic
        {
            get
            {
                return new ExcelColor(System.Drawing.Color.FromKnownColor(KnownColor.WindowText), 0x7fff);
            }
        }
        public override bool Equals(object obj)
        {
            return ((obj is ExcelColor) && (this.index == ((ExcelColor) obj).index));
        }

        public override int GetHashCode()
        {
            return this.index;
        }
    }
}

