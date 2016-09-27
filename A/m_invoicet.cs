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

    public partial class m_invoicet : Page
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
        protected string item_code = "";
        protected string item_desc = "";
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string membertype = "";
        protected string mobile = "";
        protected string pay_item_name = "Payment for multiple online CLD services";
        protected Button ProceedToPayment;
        public string refno = "";
        protected Retriever ret = new Retriever();
        public tm t = new tm();
        public double total_amt = 0.0;
        protected string transD = "0";
        public XObjs.InterSwitchPostFields xispf = new XObjs.InterSwitchPostFields();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void BtnDashboard_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
        }

        protected double calcTotalAmt(int qty, int amt)
        {
            return (double) (qty * amt);
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
            if (base.Request.QueryString["tx"] == null)
            {
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
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
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
        }

        protected void ProceedToPayment_Click(object sender, EventArgs e)
        {
            if (this.Session["lt_twall"] != null)
            {
                this.lt_twall = (List<XObjs.Twallet>) this.Session["lt_twall"];
            }
            if (this.Session["lt_fdets"] != null)
            {
                this.lt_fdets = (List<XObjs.Fee_details>) this.Session["lt_fdets"];
            }
            if (this.Session["c_addy"] != null)
            {
                this.c_addy = this.Session["c_addy"].ToString();
            }
            if (this.Session["tot_amtx"] != null)
            {
                this.amt = (double) this.Session["tot_amtx"];
            }
            int num = 0;
            int num2 = 0;
            foreach (XObjs.Fee_details _details in this.lt_fdets)
            {
                num += (Convert.ToInt32(_details.init_amt) * Convert.ToInt32(_details.xqty)) * 100;
                num2 += (Convert.ToInt32(_details.tech_amt) * Convert.ToInt32(_details.xqty)) * 100;
            }
            if (this.amt > 0.0)
            {
                this.total_amt = Math.Round((double) (this.amt / 0.985), 2);
                this.isw_conv_fee = this.total_amt - this.amt;
                if (this.isw_conv_fee > 2000.0)
                {
                    this.isw_conv_fee = 2000.0;
                    this.total_amt = this.isw_conv_fee + this.amt;
                }
            }
            this.Session["amt"] = this.amt * 100.0;
            this.Session["isw_conv_fee"] = Math.Round(this.isw_conv_fee, 2);
            this.Session["total_amt"] = Convert.ToInt32((double) (this.total_amt * 100.0)).ToString();
            this.Session["name"] = this.fullname;
            this.Session["coy_name"] = this.coy_name;
            this.Session["Refno"] = this.lt_twall[0].transID;
            this.Session["Address"] = this.c_addy;
            this.Session["einao_split_amt"] = num2.ToString();
            this.Session["cld_split_amt"] = num.ToString();
            this.xispf.amount = Convert.ToInt32((double) (this.total_amt * 100.0)).ToString();
            this.xispf.isw_conv_fee = this.isw_conv_fee.ToString();
            this.xispf.cust_id = this.cust_id;
            this.xispf.cust_id_desc = "";
            this.xispf.cust_name = this.fullname;
            this.xispf.resp_desc = "";
            if (this.lt_fdets.Count == 1)
            {
                XObjs.Fee_list _list = this.ret.getFee_listByID(this.lt_fdets[0].fee_listID);
                this.xispf.pay_item_name = _list.item;
                this.Session["item_code"] = _list.item_code;
                this.item_code = _list.item_code;
                this.Session["item_desc"] = _list.item;
                this.item_desc = _list.item;
            }
            else
            {
                this.xispf.pay_item_name = this.pay_item_name;
            }
            this.xispf.txn_ref = this.lt_twall[0].transID.ToUpper();
            this.xispf.product_id = ConfigurationManager.AppSettings["pd_product_id_test"];
            this.xispf.currency = ConfigurationManager.AppSettings["pd_currency"];
            this.xispf.site_redirect_url = ConfigurationManager.AppSettings["pd_site_redirect_url_test"];
            this.xispf.site_name = ConfigurationManager.AppSettings["pd_site_name"];
            this.xispf.pay_item_id = ConfigurationManager.AppSettings["pd_pay_item_id"];
            this.xispf.mackey = ConfigurationManager.AppSettings["pd_mackey_test"];
            this.xispf.local_date_time = DateTime.Now.ToString("dd-MMM-yy HH:MM:ss");
            this.xispf.TransactionDate = "";
            this.xispf.MerchantReference = "";
            this.xispf.trans_status = "AR";
            this.xispf.xreg_date = this.xreg_date;
            this.xispf.xvisible = "1";
            this.xispf.xsync = "0";
            this.inputString = this.xispf.txn_ref + this.xispf.product_id + this.xispf.pay_item_id + this.xispf.amount + this.xispf.site_redirect_url + this.xispf.mackey;
            this.xispf.hash = this.hash_value.GetGetSHA512String(this.inputString);
            if ((this.xispf.hash != null) && (this.xispf.hash.Length > 5))
            {
                this.Session["hashString"] = this.xispf.hash;
            }
            this.Session["xispf"] = this.xispf;
            if (this.Session["xispf"] != null)
            {
                base.Response.Redirect("../xis/pd/tx/payment_optionst.aspx");
            }
        }
    }
}

