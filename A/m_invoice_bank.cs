namespace XPay.A
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class m_invoice_bank : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        public double amt = 0.0;
        protected Button BtnDashboard;
        protected string c_addy = "";
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        public string coy_name = "";
        public string cust_id = "";
        protected string email = "";
        protected HtmlForm form1;
        protected string fullname = "";
        protected Hasher hash_value = new Hasher();
        protected string inputString = "";
        public double isw_conv_fee = 0.0;
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string membertype = "";
        protected string mobile = "";
        protected string pay_item_name = "Payment for multiple online CLD services";
        public string refno = "";
        protected Retriever ret = new Retriever();
        public tm t = new tm();
        public double total_amt = 0.0;
        protected string transD = "0";
        public XObjs.InterSwitchPostFields xispf = new XObjs.InterSwitchPostFields();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void BtnDashboard_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("http://ipo.cldng.com/A/profile.aspx");
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
                base.Response.Redirect("http://ipo.cldng.com/A/profile.aspx");
            }
            if (base.Request.QueryString["tx"] == null)
            {
                base.Response.Redirect("http://ipo.cldng.com/A/profile.aspx");
            }
            if (base.Request.QueryString["tx"] != null)
            {
                this.transD = base.Request.QueryString["tx"].ToString();
                this.Session["tx"] = this.transD;
            }
            this.lt_twall = this.ret.getTwalletByMemberID(this.adminID, this.transD, this.Session["AgentType"].ToString());
            if (this.lt_twall.Count > 0)
            {
                this.Session["lt_twall"] = this.lt_twall;
                this.lt_fdets = this.ret.getFee_detailsByTwalletID(this.lt_twall[0].xid);
                if (this.lt_fdets.Count > 0)
                {
                    this.Session["lt_fdets"] = this.lt_fdets;
                }
                if ((this.Session["AgentType"] != null) && (this.Session["AgentType"].ToString() != ""))
                {
                    this.agentType = this.Session["AgentType"].ToString();
                    if (this.agentType == "Agent")
                    {
                        if (this.Session["c_reg"] != null)
                        {
                            this.c_reg = (XObjs.Registration) this.Session["c_reg"];
                            this.fullname = this.c_reg.Firstname + " " + this.c_reg.Surname;
                            this.coy_name = this.c_reg.CompanyName;
                            this.cust_id = this.c_reg.Sys_ID;
                            this.email = this.c_reg.Email;
                            this.mobile = this.c_reg.PhoneNumber;
                            this.Session["fullname"] = this.fullname;
                            this.Session["email"] = this.email;
                            this.Session["mobile"] = this.mobile;
                            this.Session["c_addy"] = this.c_reg.CompanyAddress;
                            this.Session["cust_id"] = this.cust_id;
                        }
                    }
                    else
                    {
                        XObjs.Registration registration = new XObjs.Registration();
                        if (this.Session["c_sub"] != null)
                        {
                            this.c_sub = (XObjs.Subagent) this.Session["c_sub"];
                            this.fullname = this.c_sub.Firstname + " " + this.c_sub.Surname;
                            this.Session["fullname"] = this.fullname;
                            this.email = this.c_sub.Email;
                            this.Session["email"] = this.email;
                            this.mobile = this.c_sub.Telephone;
                            this.Session["mobile"] = this.mobile;
                        }
                        if (this.Session["c_sub_reg"] != null)
                        {
                            registration = (XObjs.Registration) this.Session["c_sub_reg"];
                            this.coy_name = registration.CompanyName;
                            this.cust_id = registration.Sys_ID + "_" + this.c_sub.AssignID;
                            this.Session["fullname"] = this.fullname;
                            this.Session["email"] = this.email;
                            this.Session["mobile"] = this.mobile;
                            this.Session["c_addy"] = registration.CompanyAddress;
                            this.Session["cust_id"] = this.cust_id;
                        }
                    }
                }
                else
                {
                    base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
                }
            }
            else
            {
                base.Response.Redirect("http://ipo.cldng.com/A/profile.aspx");
            }
        }
    }
}

