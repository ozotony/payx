namespace XPay.xis.pd.tx
{
    using System;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class test_form : Page
    {
        protected string adminID = "0";
        public string amount = "";
        public string cld_split_amt = "0";
        public string coy_name = "";
        public string currency = "";
        public string einao_split_amt = "0";
        protected HtmlForm form1;
        public string hash = "";
        protected Hasher hash_value = new Hasher();
        protected HtmlHead Head1;
        public string inputString = "";
        public string name = "";
        public string pay_item_id = "0";
        public string product_id = "0";
        public string site_redirect_url = "";
        public string txn_ref = "";

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
            this.product_id = ConfigurationManager.AppSettings["pd_product_id"];
            this.currency = ConfigurationManager.AppSettings["pd_currency"];
            this.site_redirect_url = ConfigurationManager.AppSettings["pd_site_redirect_url"];
            this.pay_item_id = ConfigurationManager.AppSettings["pd_pay_item_id"];
            if (this.Session["Refno"] != null)
            {
                this.txn_ref = this.Session["Refno"].ToString();
            }
            if (this.Session["total_amt"] != null)
            {
                this.amount = this.Session["total_amt"].ToString();
            }
            if (this.Session["hashString"] != null)
            {
                this.hash = this.Session["hashString"].ToString();
            }
            if (this.Session["einao_split_amt"] != null)
            {
                this.einao_split_amt = this.Session["einao_split_amt"].ToString();
            }
            if (this.Session["cld_split_amt"] != null)
            {
                this.cld_split_amt = this.Session["cld_split_amt"].ToString();
            }
            if (this.Session["name"] != null)
            {
                this.name = this.Session["name"].ToString();
            }
            if (this.Session["coy_name"] != null)
            {
                this.coy_name = this.Session["coy_name"].ToString();
            }
        }
    }
}

