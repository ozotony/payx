namespace XPay.Classes
{
    using System;
    using System.Configuration;

    public class Helpers
    {
        public string ConnectXhome()
        {
            return ConfigurationManager.ConnectionStrings["homeConnectionString"].ConnectionString;
        }

        public string ConnectXcld()
        {
            return ConfigurationManager.ConnectionStrings["cldConnectionString"].ConnectionString;
        }

        public string ConnectXMerc()
        {
            return ConfigurationManager.ConnectionStrings["mercConnectionString"].ConnectionString;
        }

        public string ConnectXpay()
        {
            return ConfigurationManager.ConnectionStrings["xpayConnectionString"].ConnectionString;
        }

        public string ConvertApos2Tab(string x)
        {
            string str = x;
            if (((x != null) || (x != "")) && x.Contains("'"))
            {
                str = x.Replace("'", "|");
            }
            return str;
        }

        public string ConvertTab2Apos(string x)
        {
            string str = x;
            if (((x != null) || (x != "")) && x.Contains("|"))
            {
                str = x.Replace("|", "'");
            }
            return str;
        }
    }
}

