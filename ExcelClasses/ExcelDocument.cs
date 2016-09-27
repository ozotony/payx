namespace XPay.ExcelClasses
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    public class ExcelDocument
    {
        private Dictionary<long, CellInfo> cells;
        private ushort[] clBegin = new ushort[] { 0x209, 8, 0, 0x10, 0, 0 };
        private ushort[] clEnd;
        private List<ColumnInfo> columns;
        private Font defaultFont;
        private List<FontInfo> fonts;
        private List<string> formats;
        private List<FormatInfo> fx;
        private List<int> rows;

        public ExcelDocument()
        {
            ushort[] numArray = new ushort[2];
            numArray[0] = 10;
            this.clEnd = numArray;
            this.defaultFont = new Font("Arial", 10f);
            this.CodePage = CultureInfo.CurrentCulture.TextInfo.ANSICodePage;
            this.rows = new List<int>();
            this.columns = new List<ColumnInfo>();
            this.cells = new Dictionary<long, CellInfo>();
            this.fonts = new List<FontInfo>();
            this.fonts.Add(new FontInfo(this.DefaultFont, ExcelColor.Automatic));
            this.fonts.Add(new FontInfo(this.DefaultFont, ExcelColor.Automatic));
            this.fonts.Add(new FontInfo(this.DefaultFont, ExcelColor.Automatic));
            this.fonts.Add(new FontInfo(this.DefaultFont, ExcelColor.Automatic));
            this.fx = new List<FormatInfo>();
            this.formats = new List<string>();
            this.formats.Add("General");
            this.formats.Add("0");
            this.formats.Add("0.00");
            this.formats.Add("#,##0");
            this.formats.Add("#,##0.00");
            this.formats.Add("($#,##0_);($#,##0)");
            this.formats.Add("($#,##0_);[Red]($#,##0)");
            this.formats.Add("($#,##0.00_);($#,##0.00)");
            this.formats.Add("($#,##0.00_);[Red]($#,##0.00)");
            this.formats.Add("0%");
            this.formats.Add("0.00%");
            this.formats.Add("0.00E+00");
            this.formats.Add("# ?/?");
            this.formats.Add("# ??/??");
            this.formats.Add("m/d/yy");
            this.formats.Add("d-mmm-yy");
            this.formats.Add("d-mmm");
            this.formats.Add("mmm-yy");
            this.formats.Add("h:mm AM/PM");
            this.formats.Add("h:mm:ss AM/PM");
            this.formats.Add("h:mm");
            this.formats.Add("h:mm:ss");
            this.formats.Add("m/d/yy h:mm");
            this.formats.Add("(#,##0_);(#,##0)");
            this.formats.Add("(#,##0_);[Red](#,##0)");
            this.formats.Add("(#,##0.00_);(#,##0.00)");
            this.formats.Add("(#,##0.00_);[Red](#,##0.00)");
            this.formats.Add("_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)");
            this.formats.Add("_($* #,##0_);_($* (#,##0);_($* \"-\"_);_(@_)");
            this.formats.Add("_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)");
            this.formats.Add("_($* #,##0.00_);_($* (#,##0.00);_($* \"-\"??_);_(@_)");
            this.formats.Add("mm:ss");
            this.formats.Add("[h]:mm:ss");
            this.formats.Add("mm:ss.0");
            this.formats.Add("##0.0E+0");
            this.formats.Add("@");
        }

        private void BuildInternalTables()
        {
            foreach (KeyValuePair<long, CellInfo> pair in this.cells)
            {
                FormatInfo item = new FormatInfo(pair.Value);
                if (pair.Value.Document.FX.IndexOf(item) == -1)
                {
                    pair.Value.Document.FX.Add(item);
                }
                pair.Value.FXIndex = (byte) (pair.Value.Document.FX.IndexOf(item) + 0x15);
            }
        }

        public XPay.ExcelClasses.Cell Cell(int row, int column)
        {
            return new XPay.ExcelClasses.Cell(row, column, this);
        }

        public void ColumnWidth(int column, int width)
        {
            ColumnInfo item = new ColumnInfo {
                Index = column,
                Width = width
            };
            int index = this.columns.IndexOf(item);
            if (index == -1)
            {
                this.columns.Add(item);
                index = this.columns.IndexOf(item);
            }
            else
            {
                this.columns[index].Width = width;
            }
        }

        internal CellInfo GetCellInfo(int row, int column)
        {
            CellInfo info;
            long key = HashCode(row, column);
            if (!this.cells.TryGetValue(key, out info))
            {
                if (this.rows.IndexOf(row) == -1)
                {
                    this.rows.Add(row);
                }
                info = new CellInfo(this) {
                    Row = row,
                    Column = column
                };
                this.cells.Add(key, info);
            }
            return info;
        }

        private static long HashCode(int row, int column)
        {
            return ((row << 0x20) + column);
        }

        private static bool IsNumber(object value)
        {
            return ((value is sbyte) || ((value is byte) || ((value is short) || ((value is ushort) || ((value is int) || ((value is uint) || ((value is long) || ((value is ulong) || ((value is float) || ((value is double) || (value is decimal)))))))))));
        }

        public void Save(Stream stream)
        {
            int num;
            if (this.CodePage == 0)
            {
                this.CodePage = CultureInfo.CurrentCulture.TextInfo.ANSICodePage;
            }
            this.BuildInternalTables();
            BinaryWriter writer = new BinaryWriter(stream);
            WriteUshortArray(writer, this.clBegin);
            this.WriteAuthorRecord(writer);
            this.WriteCodepageRecord(writer);
            this.WriteFontTable(writer);
            this.WriteHeaderRecord(writer);
            this.WriteFooterRecord(writer);
            this.WriteFormatTable(writer);
            this.WriteWindowProtectRecord(writer);
            this.WriteXFTable(writer);
            this.WriteStyleTable(writer);
            for (num = 0; num < this.columns.Count; num++)
            {
                this.WriteColumnInfoRecord(writer, this.columns[num]);
            }
            this.rows.Sort();
            for (num = 0; num < this.rows.Count; num++)
            {
                foreach (KeyValuePair<long, CellInfo> pair in this.cells)
                {
                    if (pair.Value.Row == this.rows[num])
                    {
                        this.WriteCellValue(writer, pair.Value);
                    }
                }
            }
            WriteUshortArray(writer, this.clEnd);
            writer.Flush();
        }

        private void WriteAuthorRecord(BinaryWriter writer)
        {
            string str;
            ushort[] numArray = new ushort[] { 0x5c, 0x20 };
            if (string.IsNullOrEmpty(this.UserName))
            {
                str = string.Empty.PadRight(0x1f);
            }
            else
            {
                str = this.UserName.Substring(0, (this.UserName.Length > 0x1f) ? 0x1f : this.UserName.Length).PadRight(0x1f);
            }
            WriteUshortArray(writer, numArray);
            writer.Write(str);
        }

        private static void WriteByteArray(BinaryWriter writer, byte[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                writer.Write(value[i]);
            }
        }

        public void WriteCell(int row, int column, object value)
        {
            this.Cell(row, column).Value = value;
        }

        private void WriteCellValue(BinaryWriter writer, CellInfo cell)
        {
            if (cell.Value == null)
            {
                this.WriteEmptyCell(writer, cell);
            }
            else if (cell.Value is string)
            {
                this.WriteStringCell(writer, cell);
            }
            else if (IsNumber(cell.Value))
            {
                this.WriteNumberCell(writer, cell);
            }
            else if (cell.Value is DateTime)
            {
                this.WriteDateCell(writer, cell);
            }
            else
            {
                this.WriteStringCell(writer, cell);
            }
        }

        private void WriteCodepageRecord(BinaryWriter writer)
        {
            ushort[] numArray2 = new ushort[3];
            numArray2[0] = 0x42;
            numArray2[1] = 2;
            ushort[] numArray = numArray2;
            numArray[2] = (ushort) this.CodePage;
            WriteUshortArray(writer, numArray);
        }

        private void WriteColumnInfoRecord(BinaryWriter writer, ColumnInfo info)
        {
            ushort[] numArray2 = new ushort[] { 0x7d, 12, 0, 0, 0, 15, 0, 0 };
            numArray2[2] = (ushort) info.Index;
            numArray2[3] = (ushort) info.Index;
            numArray2[4] = (ushort) ((info.Width * 0x100) / 7);
            ushort[] numArray = numArray2;
            WriteUshortArray(writer, numArray);
        }

        private void WriteDateCell(BinaryWriter writer, CellInfo cell)
        {
            if (cell.Value is DateTime)
            {
                DateTime time = (DateTime) cell.Value;
                DateTime time2 = new DateTime(0x76b, 12, 0x1f);
                TimeSpan span = (TimeSpan) (time - time2);
                double num = span.Days + 1;
                if (num >= 60.0)
                {
                    num++;
                }
                ushort[] numArray = new ushort[] { 0x203, 14, (ushort) cell.Row, (ushort) cell.Column, cell.FXIndex };
                WriteUshortArray(writer, numArray);
                writer.Write(num);
            }
        }

        private void WriteEmptyCell(BinaryWriter writer, CellInfo cell)
        {
            ushort[] numArray = new ushort[] { 0x201, 6, 0, 0, 15 };
            numArray[2] = (ushort) cell.Row;
            numArray[3] = (ushort) cell.Column;
            WriteUshortArray(writer, numArray);
        }

        private void WriteFontRecord(BinaryWriter writer, Font font, ExcelColor color)
        {
            ushort[] numArray2 = new ushort[5];
            numArray2[0] = 0x231;
            numArray2[4] = color.Index;
            ushort[] numArray = numArray2;
            byte[] bytes = Encoding.ASCII.GetBytes(font.FontFamily.Name);
            int length = bytes.Length;
            numArray[1] = (ushort) (7 + length);
            numArray[2] = (ushort) (font.SizeInPoints * 20f);
            int num2 = 0;
            if (font.Bold)
            {
                num2 |= 1;
            }
            if (font.Italic)
            {
                num2 |= 2;
            }
            if (font.Underline)
            {
                num2 |= 4;
            }
            if (font.Strikeout)
            {
                num2 |= 8;
            }
            numArray[3] = (ushort) num2;
            WriteUshortArray(writer, numArray);
            writer.Write((byte) length);
            writer.Write(bytes);
        }

        private void WriteFontTable(BinaryWriter writer)
        {
            foreach (FontInfo info in this.fonts)
            {
                this.WriteFontRecord(writer, info.Font, info.Color);
            }
        }

        private void WriteFooterRecord(BinaryWriter writer)
        {
            ushort[] numArray2 = new ushort[2];
            numArray2[0] = 0x15;
            ushort[] numArray = numArray2;
            WriteUshortArray(writer, numArray);
        }

        private void WriteFormat(BinaryWriter writer, string value)
        {
            ushort[] numArray2 = new ushort[2];
            numArray2[0] = 30;
            ushort[] numArray = numArray2;
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            int length = bytes.Length;
            numArray[1] = (ushort) (1 + length);
            WriteUshortArray(writer, numArray);
            writer.Write((byte) length);
            writer.Write(bytes);
        }

        private void WriteFormatTable(BinaryWriter writer)
        {
            for (int i = 0; i < this.formats.Count; i++)
            {
                this.WriteFormat(writer, this.formats[i]);
            }
        }

        private void WriteFXRecord(BinaryWriter writer, FormatInfo info)
        {
            ushort[] numArray = new ushort[] { 0x243, 12 };
            WriteUshortArray(writer, numArray);
            byte[] buffer = new byte[4];
            buffer[0] = (byte) info.FontIndex;
            buffer[1] = (byte) info.FormatIndex;
            buffer[2] = 1;
            byte num = 0;
            if (info.FontIndex > 0)
            {
                num = (byte) (num | 2);
            }
            if (info.HorizontalAlignment != Alignment.General)
            {
                num = (byte) (num | 4);
            }
            if (info.BackColor.Index != ExcelColor.Automatic.Index)
            {
                num = (byte) (num | 0x10);
            }
            num = (byte) (num << 2);
            buffer[3] = num;
            WriteByteArray(writer, buffer);
            ushort horizontalAlignment = (ushort) info.HorizontalAlignment;
            ushort num3 = 1;
            if (info.BackColor.Index != ExcelColor.Automatic.Index)
            {
                num3 = (ushort) ((num3 & -1985) | ((info.BackColor.Index & 0x1f) << 6));
                num3 = (ushort) ((num3 & -63489) | ((ExcelColor.WindowText.Index & 0x1f) << 11));
            }
            else
            {
                num3 = 0xce00;
            }
            ushort[] numArray3 = new ushort[4];
            numArray3[0] = horizontalAlignment;
            numArray3[1] = num3;
            ushort[] numArray2 = numArray3;
            WriteUshortArray(writer, numArray2);
        }

        private void WriteHeaderRecord(BinaryWriter writer)
        {
            ushort[] numArray2 = new ushort[2];
            numArray2[0] = 20;
            ushort[] numArray = numArray2;
            WriteUshortArray(writer, numArray);
        }

        private void WriteNumberCell(BinaryWriter writer, CellInfo cell)
        {
            double num = Convert.ToDouble(cell.Value);
            ushort[] numArray = new ushort[] { 0x203, 14, (ushort) cell.Row, (ushort) cell.Column, cell.FXIndex };
            WriteUshortArray(writer, numArray);
            writer.Write(num);
        }

        private void WriteStringCell(BinaryWriter writer, CellInfo cell)
        {
            string str;
            if (cell.Value is string)
            {
                str = (string) cell.Value;
            }
            else
            {
                str = cell.Value.ToString();
            }
            if (str.Length > 0xff)
            {
                str = str.Substring(0, 0xff);
            }
            ushort[] numArray2 = new ushort[6];
            numArray2[0] = 0x204;
            ushort[] numArray = numArray2;
            byte[] bytes = Encoding.GetEncoding(this.CodePage).GetBytes(str);
            int length = bytes.Length;
            numArray[1] = (ushort) (8 + length);
            numArray[2] = (ushort) cell.Row;
            numArray[3] = (ushort) cell.Column;
            numArray[4] = cell.FXIndex;
            numArray[5] = (ushort) length;
            WriteUshortArray(writer, numArray);
            writer.Write(bytes);
        }

        private void WriteStyleTable(BinaryWriter writer)
        {
            byte[][] bufferArray2 = new byte[6][];
            bufferArray2[0] = new byte[] { 0x10, 0x80, 3, 0xff };
            bufferArray2[1] = new byte[] { 0x11, 0, 9, 0x43, 0x6f, 0x6d, 0x6d, 0x61, 0x20, 0x5b, 0x30, 0x5d };
            bufferArray2[2] = new byte[] { 0x12, 0x80, 4, 0xff };
            bufferArray2[3] = new byte[] { 0x13, 0, 12, 0x43, 0x75, 0x72, 0x72, 0x65, 110, 0x63, 0x79, 0x20, 0x5b, 0x30, 0x5d };
            byte[] buffer = new byte[4];
            buffer[1] = 0x80;
            buffer[3] = 0xff;
            bufferArray2[4] = buffer;
            bufferArray2[5] = new byte[] { 20, 0x80, 5, 0xff };
            byte[][] bufferArray = bufferArray2;
            ushort[] numArray = new ushort[2];
            numArray[0] = 0x293;
            for (int i = 0; i < 6; i++)
            {
                numArray[1] = (ushort) bufferArray[i].Length;
                WriteUshortArray(writer, numArray);
                WriteByteArray(writer, bufferArray[i]);
            }
        }

        private static void WriteUshortArray(BinaryWriter writer, ushort[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                writer.Write(value[i]);
            }
        }

        private void WriteWindowProtectRecord(BinaryWriter writer)
        {
            ushort[] numArray2 = new ushort[3];
            numArray2[0] = 0x19;
            numArray2[1] = 2;
            ushort[] numArray = numArray2;
            WriteUshortArray(writer, numArray);
        }

        private void WriteXFTable(BinaryWriter writer)
        {
            ushort[][] numArray = new ushort[][] { 
                new ushort[] { 0x243, 12, 0, 0x3f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 1, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 1, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 2, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 2, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 0xf7f5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0, 1, 0, 0xce00, 0, 0 }, 
                new ushort[] { 0x243, 12, 0x2101, 0xfbf5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0x1f01, 0xfbf5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0x2001, 0xfbf5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0x1e01, 0xfbf5, 0xfff0, 0xce00, 0, 0 }, new ushort[] { 0x243, 12, 0x901, 0xfbf5, 0xfff0, 0xce00, 0, 0 }
             };
            for (int i = 0; i < 0x15; i++)
            {
                WriteUshortArray(writer, numArray[i]);
            }
            foreach (FormatInfo info in this.fx)
            {
                this.WriteFXRecord(writer, info);
            }
        }

        public int CodePage { get; set; }

        public Font DefaultFont
        {
            get
            {
                return this.defaultFont;
            }
        }

        internal List<FontInfo> Fonts
        {
            get
            {
                return this.fonts;
            }
        }

        internal List<string> Formats
        {
            get
            {
                return this.formats;
            }
        }

        internal List<FormatInfo> FX
        {
            get
            {
                return this.fx;
            }
        }

        public XPay.ExcelClasses.Cell this[int row, int column]
        {
            get
            {
                return this.Cell(row, column);
            }
        }

        public string UserName { get; set; }
    }
}

