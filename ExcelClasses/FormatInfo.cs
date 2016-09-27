namespace XPay.ExcelClasses
{
    using System;
    using System.Drawing;

    internal class FormatInfo
    {
        private CellInfo cell;
        private int fontIndex;
        private int formatIndex;

        public FormatInfo(CellInfo cell)
        {
            this.cell = cell;
            if (string.IsNullOrEmpty(cell.Format))
            {
                this.formatIndex = 0;
            }
            else
            {
                this.formatIndex = cell.Document.Formats.IndexOf(cell.Format);
            }
            FontInfo item = new FontInfo(cell.Font, cell.ForeColor);
            this.fontIndex = cell.Document.Fonts.IndexOf(item);
            if (this.fontIndex == -1)
            {
                cell.Document.Fonts.Add(item);
                this.fontIndex = cell.Document.Fonts.IndexOf(item);
            }
            if (this.fontIndex > 3)
            {
                this.fontIndex++;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is FormatInfo)
            {
                FormatInfo info = (FormatInfo) obj;
                return ((((this.fontIndex == info.fontIndex) && (this.formatIndex == info.formatIndex)) && ((this.ForeColor.Index == info.ForeColor.Index) && (this.BackColor.Index == info.BackColor.Index))) && (this.HorizontalAlignment == info.HorizontalAlignment));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ExcelColor BackColor
        {
            get
            {
                return this.cell.BackColor;
            }
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this.cell.Font;
            }
        }

        public int FontIndex
        {
            get
            {
                return this.fontIndex;
            }
        }

        public ExcelColor ForeColor
        {
            get
            {
                return this.cell.ForeColor;
            }
        }

        public string Format
        {
            get
            {
                return this.cell.Format;
            }
        }

        public int FormatIndex
        {
            get
            {
                return this.formatIndex;
            }
        }

        public Alignment HorizontalAlignment
        {
            get
            {
                return this.cell.Alignment;
            }
        }
    }
}

