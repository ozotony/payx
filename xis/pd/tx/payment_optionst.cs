namespace XPay.xis.pd.tx
{
    using System;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class payment_optionst : Page
    {
        public int addIsw_succ;
        protected string adminID = "0";
        protected string agentType = "";
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected RadioButtonList rblOptions;
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        public int update_twallxgt_succ = 0;
        public XObjs.InterSwitchPostFields xispf = new XObjs.InterSwitchPostFields();

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
            if (base.IsPostBack)
            {
                if (this.rblOptions.SelectedValue == "isw")
                {
                    if (this.Session["xispf"] != null)
                    {
                        this.xispf = (XObjs.InterSwitchPostFields) this.Session["xispf"];
                        if ((this.Session["AgentType"] != null) && (this.Session["AgentType"].ToString() != ""))
                        {
                            this.agentType = this.Session["AgentType"].ToString();
                            if (this.agentType == "Agent")
                            {
                                this.c_reg = (XObjs.Registration) this.Session["c_reg"];
                                this.xispf.cust_id = this.c_reg.Sys_ID;
                                this.xispf.cust_id_desc = "Portal Agent";
                            }
                            else
                            {
                                this.c_sub = (XObjs.Subagent) this.Session["c_sub"];
                                this.xispf.cust_id = this.c_sub.Sys_ID;
                                this.xispf.cust_id_desc = "Portal Sub-Agent";
                            }
                        }
                        this.addIsw_succ = this.reg.addInterSwitchRecords(this.xispf);
                        if (this.addIsw_succ > 0)
                        {
                            this.update_twallxgt_succ = this.reg.updateTwalletXgt(this.xispf.txn_ref, "xpay_isw", this.adminID);
                            if (this.update_twallxgt_succ > 0)
                            {
                                base.Response.Redirect("./payment_detailst.aspx");
                            }
                        }
                    }
                    else
                    {
                        base.Response.Redirect("../../../A/m_payt.aspx");
                    }
                }
                else if ((this.rblOptions.SelectedValue == "bank") && (this.Session["xispf"] != null))
                {
                    this.xispf = (XObjs.InterSwitchPostFields) this.Session["xispf"];
                    this.update_twallxgt_succ = this.reg.updateTwalletXgtBanker(this.xispf.txn_ref, "xpay_bk", this.adminID);
                    if (this.update_twallxgt_succ > 0)
                    {
                        base.Response.Redirect("../../../A/m_invoice_bank.aspx?tx=" + this.xispf.txn_ref);
                    }
                }
            }
        }
    }
}

