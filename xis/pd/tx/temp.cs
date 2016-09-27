namespace XPay.xis.pd.tx
{
    using System;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class temp : Page
    {
        protected string adminID = "0";
        protected HtmlForm form1;
        protected Hasher hash_value = new Hasher();
        protected HtmlHead Head1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
            string inputString = "D7B9123C827745841013470000http://xpayng.com/xis/pd/xreturn/index.aspxE092D3166B4E787C6B4B9EDFE8E7E7659D47321DDF4D2644B61B709D0A0A9B9098FB7F3342813FEFCD2F0198F380C6F28D56C3E42CFDE20F8CD472EF5202E312";
            string str2 = this.hash_value.GetGetSHA512String(inputString);
        }
    }
}

