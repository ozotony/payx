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

    public partial class m_invoicex : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected string agt_code = "";
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
            base.Response.Redirect("http://ipo.cldng.com/A/profile.aspx");
        }

        protected double calcTotalAmt(int qty, int amt)
        {
            return (double) (qty * amt);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["c_app"] != null)
            {
                c_app = (XObjs.Applicant) Session["c_app"];
            }
            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
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
                transD = base.Request.QueryString["tx"].ToString();
                Session["tx"] = transD;
            }
            lt_twall = ret.getTwalletByMemberID(adminID, transD, Session["AgentType"].ToString());
            if (lt_twall.Count > 0)
            {
                Session["lt_twall"] = lt_twall;
                lt_fdets = ret.getFee_detailsByTwalletID(lt_twall[0].xid);
                if (lt_fdets.Count > 0)
                {
                    Session["lt_fdets"] = lt_fdets;
                }
                if ((Session["AgentType"] != null) && (Session["AgentType"].ToString() != ""))
                {
                    agentType = Session["AgentType"].ToString();
                    if (agentType == "Agent")
                    {
                        if (Session["c_reg"] != null)
                        {
                            c_reg = (XObjs.Registration) Session["c_reg"];
                            fullname = c_reg.Firstname + " " + c_reg.Surname;
                            coy_name = c_reg.CompanyName;
                            cust_id = c_reg.Sys_ID;
                            email = c_reg.Email;
                            mobile = c_reg.PhoneNumber;
                            Session["fullname"] = fullname;
                            Session["email"] = email;
                            Session["mobile"] = mobile;
                            Session["c_addy"] = c_reg.CompanyAddress;
                            Session["cust_id"] = cust_id;
                        }
                    }
                    else
                    {
                        XObjs.Registration registration = new XObjs.Registration();
                        if (Session["c_sub"] != null)
                        {
                            c_sub = (XObjs.Subagent) Session["c_sub"];
                            fullname = c_sub.Firstname + " " + c_sub.Surname;
                            Session["fullname"] = fullname;
                            email = c_sub.Email;
                            Session["email"] = email;
                            mobile = c_sub.Telephone;
                            Session["mobile"] = mobile;
                        }
                        if (Session["c_sub_reg"] != null)
                        {
                            registration = (XObjs.Registration) Session["c_sub_reg"];
                            coy_name = registration.CompanyName;
                            cust_id = registration.Sys_ID + "_" + c_sub.AssignID;
                            Session["fullname"] = fullname;
                            Session["email"] = email;
                            Session["mobile"] = mobile;
                            Session["c_addy"] = registration.CompanyAddress;
                            Session["cust_id"] = cust_id;
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
            if (Session["lt_twall"] != null)
            {
                lt_twall = (List<XObjs.Twallet>) Session["lt_twall"];
            }
            if (Session["lt_fdets"] != null)
            {
                lt_fdets = (List<XObjs.Fee_details>) Session["lt_fdets"];
            }
            if (Session["c_addy"] != null)
            {
                c_addy = Session["c_addy"].ToString();
            }
            if (Session["tot_amtx"] != null)
            {
                amt = (double) Session["tot_amtx"];
            }
            int num = 0;
            int num2 = 0;
            foreach (XObjs.Fee_details _details in lt_fdets)
            {
                num += (Convert.ToInt32(_details.init_amt) * Convert.ToInt32(_details.xqty)) * 100;
                num2 += (Convert.ToInt32(_details.tech_amt) * Convert.ToInt32(_details.xqty)) * 100;
            }
            if (amt > 0.0)
            {
                total_amt = Math.Round((double) (amt / 0.985), 2);
                isw_conv_fee = total_amt - amt;
                if (isw_conv_fee > 2000.0)
                {
                    isw_conv_fee = 2000.0;
                    total_amt = isw_conv_fee + amt;
                }
            }
            Session["amt"] = amt * 100.0;
            Session["isw_conv_fee"] = Math.Round(isw_conv_fee, 2);
            Session["total_amt"] = Convert.ToInt32((double) (total_amt * 100.0)).ToString();
            Session["name"] = fullname;
            Session["coy_name"] = coy_name;
            Session["Refno"] = lt_twall[0].transID;
            Session["Address"] = c_addy;
            Session["einao_split_amt"] = num2.ToString();
            Session["cld_split_amt"] = num.ToString();
            xispf.amount = Convert.ToInt32((double) (total_amt * 100.0)).ToString();
            xispf.isw_conv_fee = isw_conv_fee.ToString();
            xispf.cust_id = cust_id;
            xispf.cust_id_desc = "";
            xispf.cust_name = fullname;
            xispf.resp_desc = "";
            if (lt_fdets.Count == 1)
            {
                XObjs.Fee_list _list = ret.getFee_listByID(lt_fdets[0].fee_listID);
                xispf.pay_item_name = _list.item;
                Session["item_code"] = _list.item_code;
                item_code = _list.item_code;
                Session["item_desc"] = _list.item;
                item_desc = _list.item;
            }
            else
            {
                xispf.pay_item_name = pay_item_name;
            }
            xispf.txn_ref = lt_twall[0].transID.ToUpper();
            xispf.product_id = ConfigurationManager.AppSettings["pd_product_id"];
            xispf.currency = ConfigurationManager.AppSettings["pd_currency"];
            xispf.site_redirect_url = ConfigurationManager.AppSettings["pd_site_redirect_url"];
            xispf.site_name = ConfigurationManager.AppSettings["pd_site_name"];
            xispf.pay_item_id = ConfigurationManager.AppSettings["pd_pay_item_id"];
            xispf.mackey = ConfigurationManager.AppSettings["pd_mackey"];
            xispf.local_date_time = DateTime.Now.ToString("dd-MMM-yy HH:MM:ss");
            xispf.TransactionDate = "";
            xispf.MerchantReference = "";
            xispf.trans_status = "AR";
            xispf.xreg_date = xreg_date;
            xispf.xvisible = "1";
            xispf.xsync = "0";
            inputString = xispf.txn_ref + xispf.product_id + xispf.pay_item_id + xispf.amount + xispf.site_redirect_url + xispf.mackey;
            xispf.hash = hash_value.GetGetSHA512String(inputString);
            if ((xispf.hash != null) && (xispf.hash.Length > 5))
            {
                Session["hashString"] = xispf.hash;
            }
            Session["xispf"] = xispf;
            if (Session["xispf"] != null)
            {
                base.Response.Redirect("../xis/pd/tx/payment_optionsx.aspx");
            }
        }
    }
}

