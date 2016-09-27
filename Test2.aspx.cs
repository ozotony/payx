using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XPay.Classes;

namespace PayX
{
    public partial class Test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str2 = new Email().sendMail("Templar", "ozotony@yahoo.com", "paymentsupport@einaosolutions.com", "Testing", "Testing New mail", "");
        }
    }
}