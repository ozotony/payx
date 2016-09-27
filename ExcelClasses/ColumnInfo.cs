namespace XPay.ExcelClasses
{
    using System;
    using System.Runtime.CompilerServices;

    internal class ColumnInfo
    {
        public override bool Equals(object obj)
        {
            if (obj is ColumnInfo)
            {
                return (this.Index == ((ColumnInfo) obj).Index);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int Index { get; set; }

        public int Width { get; set; }
    }
}

