namespace XPay.InterSwitch.PayDirect.Classes
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Hasher
    {
        public string GetGetSHA512String(string inputString)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in GetSHA512(inputString))
            {
                builder.Append(num.ToString("X2"));
            }
            return builder.ToString();
        }

        public static byte[] GetSHA512(string inputString)
        {
            return SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}

