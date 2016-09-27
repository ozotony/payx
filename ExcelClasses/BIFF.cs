namespace XPay.ExcelClasses
{
    using System;

    internal sealed class BIFF
    {
        public const ushort BOFRecord = 0x209;
        public const ushort CodepageRecord = 0x42;
        public const ushort ColumnInfoRecord = 0x7d;
        public const ushort DefaultColor = 0x7fff;
        public const ushort EOFRecord = 10;
        public const ushort ExtendedRecord = 0x243;
        public const ushort FontRecord = 0x231;
        public const ushort FooterRecord = 0x15;
        public const ushort FormatRecord = 30;
        public const ushort HeaderRecord = 20;
        public const ushort LabelRecord = 0x204;
        public const ushort NumberRecord = 0x203;
        public const ushort StyleRecord = 0x293;
        public const ushort WindowProtectRecord = 0x19;
        public const ushort XFRecord = 0x243;
    }
}

