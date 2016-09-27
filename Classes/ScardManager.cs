namespace XPay.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class ScardManager
    {
        public string addScards(List<XObjs.Scard> lt_scards)
        {
            int num = 0;
            foreach (XObjs.Scard scard in lt_scards)
            {
                SqlConnection connection = new SqlConnection(this.Connect());
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO scard (xnum,xvalid,xlogstaff,xreg_date,xvisible,xsync) VALUES (@xnum,@xvalid,@xlogstaff,@xreg_date,@xvisible,@xsync) SELECT SCOPE_IDENTITY()";
                connection.Open();
                command.Parameters.Add("@xnum", SqlDbType.NVarChar);
                command.Parameters.Add("@xvalid", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@xlogstaff", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@xreg_date", SqlDbType.NVarChar, 50);
                command.Parameters.Add("@xvisible", SqlDbType.NVarChar, 10);
                command.Parameters.Add("@xsync", SqlDbType.VarChar, 10);
                command.Parameters["@xnum"].Value = scard.xnum;
                command.Parameters["@xvalid"].Value = scard.xvalid;
                command.Parameters["@xlogstaff"].Value = scard.xlogstaff;
                command.Parameters["@xreg_date"].Value = scard.xreg_date;
                command.Parameters["@xvisible"].Value = scard.xvisible;
                command.Parameters["@xsync"].Value = scard.xsync;
                num += Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return num.ToString();
        }

        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["cldConnectionString"].ConnectionString;
        }

        public List<XObjs.Scard> GenerateGuidNum(int amt, int cnt)
        {
            List<XObjs.Scard> list = new List<XObjs.Scard>();
            string str = "1";
            string str2 = "0";
            string str3 = DateTime.Now.ToString("yyyy-MM-dd");
            string str4 = "1";
            string str5 = "1";
            for (int i = 0; i < cnt; i++)
            {
                XObjs.Scard item = new XObjs.Scard {
                    xnum = Guid.NewGuid().ToString("n").Substring(0, amt).ToUpper(),
                    xlogstaff = str4,
                    xreg_date = str3,
                    xsync = str2,
                    xvisible = str,
                    xvalid = str5
                };
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}

