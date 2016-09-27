namespace XPay.xis.pd.tx
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class re_payment_details : Page
    {
        public string addy = "";
        protected string adminID = "0";
        protected string agentType = "";
        public string amt = "0";
        protected Button btnPay;
        public int btnProceedShow = 1;
        protected string c_addy = "";
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        public string coy_name = "";
        protected HtmlForm form1;
        protected HtmlHead Head1;
        public string isw_conv_fee = "0";
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        public string name = "";
        public string refno = "";
        protected Retriever ret = new Retriever();
        public string total_amt = "0";
        protected List<XObjs.Twallet> twall = new List<XObjs.Twallet>();
        public XObjs.InterSwitchPostFields xispf = new XObjs.InterSwitchPostFields();
        protected Transactions xtrans = new Transactions();

        protected void btnPay_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("./form.aspx");
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
            if (this.Session["xispf"] != null)
            {
                this.xispf = (XObjs.InterSwitchPostFields) this.Session["xispf"];
            }
            if ((this.Session["AgentType"] != null) && (this.Session["AgentType"].ToString() != ""))
            {
                this.agentType = this.Session["AgentType"].ToString();
                if (this.agentType == "Agent")
                {
                    if (this.Session["c_reg"] != null)
                    {
                        this.c_reg = (XObjs.Registration) this.Session["c_reg"];
                        this.Session["name"] = this.c_reg.Firstname + " " + this.c_reg.Surname;
                        this.Session["coy_name"] = this.c_reg.CompanyName;
                        this.Session["Address"] = this.c_reg.CompanyAddress;
                    }
                }
                else
                {
                    XObjs.Registration registration = new XObjs.Registration();
                    if (this.Session["c_sub"] != null)
                    {
                        this.c_sub = (XObjs.Subagent) this.Session["c_sub"];
                        this.Session["name"] = this.c_sub.Firstname + " " + this.c_sub.Surname;
                    }
                    if (this.Session["c_sub_reg"] != null)
                    {
                        registration = (XObjs.Registration) this.Session["c_sub_reg"];
                        this.Session["coy_name"] = registration.CompanyName;
                        this.Session["Address"] = registration.CompanyAddress;
                    }
                }
            }
            this.twall = this.ret.getTwalletByMemberID(this.adminID, this.xispf.txn_ref, this.agentType);
            this.lt_fdets = this.ret.getFee_detailsByTwalletID(this.twall[0].xid);
            int num = 0;
            int num2 = 0;
            if (this.lt_fdets.Count > 0)
            {
                foreach (XObjs.Fee_details _details in this.lt_fdets)
                {
                    num += (Convert.ToInt32(_details.init_amt) * Convert.ToInt32(_details.xqty)) * 100;
                    num2 += (Convert.ToInt32(_details.tech_amt) * Convert.ToInt32(_details.xqty)) * 100;
                }
            }
            this.refno = this.xispf.txn_ref;
            this.isw_conv_fee = Math.Round(Convert.ToDecimal(this.xispf.isw_conv_fee), 2).ToString();
            this.total_amt = Convert.ToString(this.xispf.amount);
            this.amt = Convert.ToString((decimal) ((Convert.ToDecimal(this.total_amt) / 100M) - Convert.ToDecimal(this.isw_conv_fee)));
            this.name = this.Session["name"].ToString();
            this.coy_name = this.Session["coy_name"].ToString();
            this.addy = this.Session["Address"].ToString();
            this.amt = string.Format("{0:n}", this.amt);
            this.isw_conv_fee = string.Format("{0:n}", this.isw_conv_fee);
            this.Session["amt"] = this.amt;
            this.Session["isw_conv_fee"] = this.isw_conv_fee;
            this.Session["total_amt"] = this.total_amt;
            this.Session["Refno"] = this.refno;
            this.Session["einao_split_amt"] = num2.ToString();
            this.Session["cld_split_amt"] = num.ToString();
            this.Session["hashString"] = this.xispf.hash;
            if (this.addy.Contains<char>(','))
            {
                this.addy = this.addy.Replace(",", ", ");
            }
        }
    }
}

