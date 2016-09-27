namespace XPay.ExcelClasses
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class CellInfo
    {
        private object value;

        public CellInfo(ExcelDocument document)
        {
            this.BackColor = ExcelColor.Automatic;
            this.ForeColor = ExcelColor.Automatic;
            this.Font = document.DefaultFont;
            this.Document = document;
        }

        public XPay.ExcelClasses.Alignment Alignment { get; set; }

        public ExcelColor BackColor { get; set; }

        public int Column { get; set; }

        public ExcelDocument Document { get; set; }

        public System.Drawing.Font Font { get; set; }

        public ExcelColor ForeColor { get; set; }

        public string Format { get; set; }

        internal byte FXIndex { get; set; }

        public int Row { get; set; }

        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                if (value is DateTime)
                {
                    this.Format = this.Document.Formats[15];
                }
            }
        }
    }
}

