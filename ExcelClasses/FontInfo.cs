namespace XPay.ExcelClasses
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class FontInfo
    {
        public FontInfo(System.Drawing.Font font, ExcelColor color)
        {
            this.Font = font;
            this.Color = color;
        }

        public override bool Equals(object obj)
        {
            if (obj is FontInfo)
            {
                FontInfo info = (FontInfo) obj;
                return (this.Font.Equals(info.Font) && this.Color.Equals(info.Color));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (this.Font.GetHashCode() ^ this.Color.GetHashCode());
        }

        public ExcelColor Color { get; set; }

        public System.Drawing.Font Font { get; set; }
    }
}

