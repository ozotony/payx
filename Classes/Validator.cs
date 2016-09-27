namespace XPay.Classes
{
    using System;

    public class Validator
    {
        public int IsDecimal(string txt)
        {
            int num = 0;
            try
            {
                Convert.ToDecimal(txt);
                return num;
            }
            catch (FormatException)
            {
                num++;
                return num;
            }
        }

        public int IsInt32(string txt)
        {
            int num = 0;
            try
            {
                Convert.ToInt32(txt);
                return num;
            }
            catch (FormatException)
            {
                num++;
                return num;
            }
        }

        public int IsInt64(string txt)
        {
            int num = 0;
            try
            {
                Convert.ToInt64(txt);
                return num;
            }
            catch (FormatException)
            {
                num++;
                return num;
            }
        }

        public int IsPresent(string txt)
        {
            int num = 0;
            if (txt == "")
            {
                num++;
            }
            return num;
        }

        public int IsValidEmail(string txt)
        {
            int num = 0;
            if (txt != "")
            {
                if ((txt.IndexOf("@") == -1) || (txt.IndexOf(".") == -1))
                {
                    num++;
                    return num;
                }
                return num;
            }
            return num;
        }

        public int IsValidMobile(string txt)
        {
            int num = 0;
            if (txt != "")
            {
                if (txt.Length < 11)
                {
                    num++;
                }
                return num;
            }
            num++;
            return num;
        }

        public int IsWithinRange(string txt, decimal min, decimal max)
        {
            int num = 0;
            decimal num2 = Convert.ToDecimal(txt);
            if ((num2 < min) || (num2 > max))
            {
                num++;
                return num;
            }
            return num;
        }
    }
}

