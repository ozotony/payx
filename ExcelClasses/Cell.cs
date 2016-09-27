namespace XPay.ExcelClasses
{
    using System;
    using System.Drawing;

    public class Cell
    {
        private CellInfo cellInfo;
        private ExcelDocument document;

        internal Cell(int row, int column, ExcelDocument document)
        {
            this.document = document;
            this.cellInfo = document.GetCellInfo(row, column);
        }

        public XPay.ExcelClasses.Alignment Alignment
        {
            get
            {
                return this.cellInfo.Alignment;
            }
            set
            {
                this.cellInfo.Alignment = value;
            }
        }

        public ExcelColor BackColor
        {
            get
            {
                return this.cellInfo.BackColor;
            }
            set
            {
                this.cellInfo.BackColor = value;
            }
        }

        public int Column
        {
            get
            {
                return this.cellInfo.Column;
            }
        }

        internal ExcelDocument Document
        {
            get
            {
                return this.document;
            }
        }

        public System.Drawing.Font Font
        {
            get
            {
                return this.cellInfo.Font;
            }
            set
            {
                this.cellInfo.Font = value;
            }
        }

        public ExcelColor ForeColor
        {
            get
            {
                return this.cellInfo.ForeColor;
            }
            set
            {
                this.cellInfo.ForeColor = value;
            }
        }

        public string Format
        {
            get
            {
                return this.cellInfo.Format;
            }
            set
            {
                this.cellInfo.Format = value;
                if (!this.document.Formats.Contains(value))
                {
                    this.document.Formats.Add(value);
                }
            }
        }

        public int Row
        {
            get
            {
                return this.cellInfo.Row;
            }
        }

        public object Value
        {
            get
            {
                return this.cellInfo.Value;
            }
            set
            {
                this.cellInfo.Value = value;
            }
        }
    }
}

