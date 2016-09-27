namespace XPay.ExcelClasses
{
    using System;
    using System.IO;
    using System.Text;

    public class ExcelWriter
    {
        private ushort[] clBegin = new ushort[] { 0x809, 8, 0, 0x10, 0, 0 };
        private ushort[] clEnd;
        private Stream stream;
        private BinaryWriter writer;

        public ExcelWriter(Stream stream)
        {
            ushort[] numArray = new ushort[2];
            numArray[0] = 10;
            this.clEnd = numArray;
            this.stream = stream;
            this.writer = new BinaryWriter(stream);
        }

        public void BeginWrite()
        {
            this.WriteUshortArray(this.clBegin);
        }

        public void EndWrite()
        {
            this.WriteUshortArray(this.clEnd);
            this.writer.Flush();
        }

        public void WriteCell(int row, int col)
        {
            ushort[] numArray = new ushort[] { 0x201, 6, 0, 0, 0x17 };
            numArray[2] = (ushort) row;
            numArray[3] = (ushort) col;
            this.WriteUshortArray(numArray);
        }

        public void WriteCell(int row, int col, double value)
        {
            ushort[] numArray2 = new ushort[5];
            numArray2[0] = 0x203;
            numArray2[1] = 14;
            ushort[] numArray = numArray2;
            numArray[2] = (ushort) row;
            numArray[3] = (ushort) col;
            this.WriteUshortArray(numArray);
            this.writer.Write(value);
        }

        public void WriteCell(int row, int col, int value)
        {
            ushort[] numArray2 = new ushort[5];
            numArray2[0] = 0x27e;
            numArray2[1] = 10;
            ushort[] numArray = numArray2;
            numArray[2] = (ushort) row;
            numArray[3] = (ushort) col;
            this.WriteUshortArray(numArray);
            int num = (value << 2) | 2;
            this.writer.Write(num);
        }

        public void WriteCell(int row, int col, string value)
        {
            ushort[] numArray2 = new ushort[6];
            numArray2[0] = 0x204;
            ushort[] numArray = numArray2;
            int length = value.Length;
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            numArray[1] = (ushort) (8 + length);
            numArray[2] = (ushort) row;
            numArray[3] = (ushort) col;
            numArray[5] = (ushort) length;
            this.WriteUshortArray(numArray);
            this.writer.Write(bytes);
        }

        public void WriteCell(int row, int col, DateTime value, int formatIndex)
        {
            DateTime time = new DateTime(0x76b, 12, 0x1f);
            TimeSpan span = (TimeSpan) (value - time);
            ushort num = (ushort) (span.Days + 1);
            ushort[] numArray2 = new ushort[4];
            numArray2[0] = 2;
            numArray2[1] = 9;
            ushort[] numArray = numArray2;
            numArray[2] = (ushort) row;
            numArray[3] = (ushort) col;
            this.WriteUshortArray(numArray);
            this.writer.Write((byte) 0);
            byte num2 = (byte) (formatIndex & 0x3f);
            this.writer.Write(num2);
            this.writer.Write((byte) 0);
            this.writer.Write(num);
        }

        public void WriteFormat(string value)
        {
            ushort[] numArray2 = new ushort[2];
            numArray2[0] = 30;
            ushort[] numArray = numArray2;
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            int length = bytes.Length;
            numArray[1] = (ushort) (1 + length);
            this.WriteUshortArray(numArray);
            this.writer.Write((byte) length);
            this.writer.Write(bytes);
        }

        private void WriteUshortArray(ushort[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                this.writer.Write(value[i]);
            }
        }
    }
}

