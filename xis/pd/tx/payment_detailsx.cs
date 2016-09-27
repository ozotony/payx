namespace XPay.xis.pd.tx
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class payment_detailsx : Page
    {
        public string addy = "";
        protected string adminID = "0";
        public string agt_code = "";
        public string amt = "0";
        protected Button BtnDashboard;
        protected Button btnPay;
        public int btnProceedShow = 1;
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        public string coy_name = "";
        protected HtmlForm form1;
        protected HtmlHead Head1;
        public string isw_conv_fee = "0";
        public string item_code = "";
        public string item_desc = "";
        public string name = "";
        public string refno = "";
        public string total_amt = "0";
        public XObjs.InterSwitchPostFields xispf = new XObjs.InterSwitchPostFields();
        protected Transactions xtrans = new Transactions();

        protected void BtnDashboard_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("http://ipo.cldng.com/A/profile.aspx");
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("./formx.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["c_app"] != null)
            {
                this.c_app = (XObjs.Applicant) this.Session["c_app"];
            }
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
            if (this.Session["amt"] != null)
            {
                this.amt = this.Session["amt"].ToString();
            }
            if (this.Session["isw_conv_fee"] != null)
            {
                this.isw_conv_fee = this.Session["isw_conv_fee"].ToString();
            }
            if (this.Session["total_amt"] != null)
            {
                this.total_amt = this.Session["total_amt"].ToString();
            }
            if (this.Session["name"] != null)
            {
                this.name = this.Session["name"].ToString();
            }
            if (this.Session["coy_name"] != null)
            {
                this.coy_name = this.Session["coy_name"].ToString();
            }
            if (this.Session["Refno"] != null)
            {
                this.refno = this.Session["Refno"].ToString();
            }
            if (this.Session["Address"] != null)
            {
                this.addy = this.Session["Address"].ToString();
            }
            if (this.Session["item_code"] != null)
            {
                this.item_code = this.Session["item_code"].ToString();
            }
            if (this.Session["item_desc"] != null)
            {
                this.item_desc = this.Session["item_desc"].ToString();
            }
            if (this.Session["cust_id"] != null)
            {
                this.agt_code = this.Session["cust_id"].ToString();
            }
            this.amt = string.Format("{0:n}", this.amt);
            this.isw_conv_fee = string.Format("{0:n}", this.isw_conv_fee);
            if (this.addy.Contains<char>(','))
            {
                this.addy = this.addy.Replace(",", ", ");
            }
        }
    }
}

