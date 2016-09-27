namespace XPay.xis.pd.tx
{
    using System;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class payment_options : Page
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
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
            if (base.IsPostBack)
            {
                if (rblOptions.SelectedValue == "isw")
                {
                    if (Session["xispf"] != null)
                    {
                        xispf = (XObjs.InterSwitchPostFields) Session["xispf"];
                        if ((Session["AgentType"] != null) && (Session["AgentType"].ToString() != ""))
                        {
                            agentType = Session["AgentType"].ToString();
                            if (agentType == "Agent")
                            {
                                c_reg = (XObjs.Registration) Session["c_reg"];
                                xispf.cust_id = c_reg.Sys_ID;
                                xispf.cust_id_desc = "Portal Agent";
                            }
                            else
                            {
                                c_sub = (XObjs.Subagent) Session["c_sub"];
                                xispf.cust_id = c_sub.Sys_ID;
                                xispf.cust_id_desc = "Portal Sub-Agent";
                            }
                        }
                        addIsw_succ = reg.addInterSwitchRecords(xispf);
                        if (addIsw_succ > 0)
                        {
                            update_twallxgt_succ = reg.updateTwalletXgt(xispf.txn_ref, "xpay_isw", adminID);
                            if (update_twallxgt_succ > 0)
                            {
                                base.Response.Redirect("./payment_details.aspx");
                            }
                        }
                    }
                    else
                    {
                        base.Response.Redirect("../../../A/m_pay.aspx");
                    }
                }
                else if ((rblOptions.SelectedValue == "bank") && (Session["xispf"] != null))
                {
                    xispf = (XObjs.InterSwitchPostFields) Session["xispf"];
                    update_twallxgt_succ = reg.updateTwalletXgtBanker(xispf.txn_ref, "xpay_bk", adminID);
                    if (update_twallxgt_succ > 0)
                    {
                        base.Response.Redirect("../../../A/m_invoice_bank.aspx?tx=" + xispf.txn_ref);
                    }
                }
            }
        }
    }
}

