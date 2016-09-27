namespace XPay.Classes
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public class Email
    {
      //  private string hostname = "mail.einaosolutions.com";

        private string hostname = "88.150.164.30";
        
    
        private MailMessage mail = new MailMessage();
        private string passwd = "Zues.4102.Hector";
        private int port = 0x24b;
        private string username = "paymentsupport@einaosolutions.com";

        public string sendMail(string userdisplayname, string to, string from, string subject, string msg, string path)
        {
            string str = "";
            SmtpClient client = new SmtpClient {
                Credentials = new NetworkCredential(this.username, this.passwd),
                //Port = this.port,
                Host = this.hostname,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 0x4e20
            };
            this.mail = new MailMessage();
            string[] strArray = to.Split(new char[] { ',' });
            try
            {
                this.mail.From = new MailAddress(from, userdisplayname, Encoding.UTF8);
                for (byte i = 0; i < strArray.Length; i = (byte) (i + 1))
                {
                    this.mail.To.Add(strArray[i]);
                }
                this.mail.Priority = MailPriority.High;
                this.mail.Subject = subject;
                this.mail.Body = msg;
                if (path != "")
                {
                    LinkedResource item = new LinkedResource(path) {
                        ContentId = "Logo"
                    };
                    AlternateView view = AlternateView.CreateAlternateViewFromString("<html><body><table border=2><tr width=100%><td><img src=cid:Logo alt=companyname /></td><td>FROM BLUEFROST</td></tr></table><hr/></body></html>" + msg, null, "text/html");
                    view.LinkedResources.Add(item);
                    this.mail.AlternateViews.Add(view);
                    this.mail.IsBodyHtml = true;
                    this.mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    this.mail.ReplyTo = new MailAddress(to);
                    client.Send(this.mail);
                    return str;
                }
                if (path == "")
                {
                    this.mail.IsBodyHtml = true;
                    this.mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    this.mail.ReplyTo = new MailAddress(to);
                    client.Send(this.mail);
                    str = "good";
                }
            }
            catch (Exception exception)
            {
                if (exception.ToString() == "The operation has timed out")
                {
                    client.Send(this.mail);
                    str = "bad";
                }
            }
            return str;
        }
    }
}

