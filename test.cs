namespace XPay
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using XPay.Classes;

    public partial class test : Page
    {
        protected HtmlForm form1;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sss2 = "test";
            string str2 = new Email().sendMail("Templar", "ozotony@yahoo.com", "paymentsupport@einaosolutions.com", "Testing", "Testing New mail", "");
        }
    }
}

