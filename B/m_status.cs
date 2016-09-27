namespace XPay.B
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public class m_status : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected Button btnBack;
        protected Button btnConfirm;
        protected Button btnValidate;
        protected XObjs.Address c_addy = new XObjs.Address();
        protected XObjs.Pwallet c_pwall = new XObjs.Pwallet();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected XObjs.XAgent c_xagt = new XObjs.XAgent();
        protected XObjs.XBanker c_xbank = new XObjs.XBanker();
        protected XObjs.XMember c_xmem = new XObjs.XMember();
        public string coy_name = "";
        public string cust_id = "";
        protected string email = "";
        protected HtmlForm form1;
        protected string fullname = "";
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string mobile = "";
        protected string paid_status_msg = "";
        protected RadioButtonList rblagentType;
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected int show_inv = 0;
        protected string state_row = "0";
        protected string status_msg = "";
        protected int succ;
        protected double tot_amtx = 0.0;
        protected string transID = "";
        protected int update_twallxgt_succ = 0;
        protected Validator val = new Validator();
        protected TextBox xmob;
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected TextBox xtrans;
        protected string xvisible = "1";

        protected void btnBack_Click(object sender, EventArgs e)
        {
            this.show_inv = 0;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (((this.xtrans.Text == "") || (this.xmob.Text == "")) || (this.val.IsValidMobile(this.xmob.Text) > 0))
            {
                if (this.xtrans.Text == "")
                {
                    this.xtrans.BorderColor = Color.Red;
                }
                else
                {
                    this.xtrans.BorderColor = Color.Green;
                }
                if (this.xmob.Text == "")
                {
                    this.xmob.BorderColor = Color.Red;
                }
                else
                {
                    this.xmob.BorderColor = Color.Green;
                }
                base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE TO FILL IN ALL THE ENTRIES MARKED WITH A RED STAR!!'); </script>");
            }
            else if (((this.xtrans.Text != "") && (this.xmob.Text != "")) && (this.val.IsValidMobile(this.xmob.Text) == 0))
            {
                if ((this.Session["agentType"] != null) && (this.Session["agentType"].ToString() != ""))
                {
                    this.agentType = this.Session["agentType"].ToString();
                }
                if (this.agentType == "Agent")
                {
                    this.c_reg = this.ret.getRegistrationByPhoneNumber(this.xmob.Text);
                    this.Session["c_reg"] = this.c_reg;
                    this.Session["RegID"] = this.c_reg.xid;
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
                else
                {
                    if (this.Session["c_sub"] != null)
                    {
                        this.c_sub = this.ret.getSubAgentByPhoneNumber(this.xmob.Text);
                        this.Session["c_sub"] = this.c_sub;
                        this.Session["RegID"] = this.c_sub.xid;
                        this.fullname = this.c_sub.Firstname + " " + this.c_sub.Surname;
                        this.Session["fullname"] = this.fullname;
                        this.email = this.c_sub.Email;
                        this.Session["email"] = this.email;
                        this.mobile = this.c_sub.Telephone;
                        this.Session["mobile"] = this.mobile;
                    }
                    if (this.Session["c_sub_reg"] != null)
                    {
                        this.c_sub_reg = this.ret.getRegistrationBySubagentRegistrationID(this.c_sub.RegistrationID);
                        this.Session["c_sub_reg"] = this.c_sub_reg;
                        this.coy_name = this.c_sub_reg.CompanyName;
                        this.cust_id = this.c_sub_reg.Sys_ID + "_" + this.c_sub.AssignID;
                        this.Session["fullname"] = this.fullname;
                        this.Session["email"] = this.email;
                        this.Session["mobile"] = this.mobile;
                        this.Session["c_addy"] = this.c_sub_reg.CompanyAddress;
                    }
                }
                string xmemberID = "";
                if ((this.Session["RegID"] != null) && (this.Session["RegID"].ToString() != ""))
                {
                    xmemberID = this.Session["RegID"].ToString();
                }
                if ((xmemberID != null) && (xmemberID != ""))
                {
                    this.lt_twall = this.ret.getValidatedTwalletByMemberID(xmemberID, this.xtrans.Text);
                    if (this.lt_twall.Count > 0)
                    {
                        this.paid_status_msg = this.lt_twall[0].xpay_status;
                        if (this.paid_status_msg == "1")
                        {
                            this.paid_status_msg = "PAID";
                            this.btnValidate.Visible = false;
                        }
                        else
                        {
                            this.paid_status_msg = "NOT PAID";
                            this.btnValidate.Visible = true;
                        }
                        this.lt_fdets = this.ret.getFee_detailsByTwalletID(this.lt_twall[0].xid);
                        this.Session["transID"] = this.xtrans.Text;
                        this.Session["memberID"] = xmemberID;
                        this.show_inv = 1;
                    }
                    else
                    {
                        this.status_msg = "COULD NOT FIND THE TRANSACTION ON THE SYSTEM!!";
                    }
                }
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            if ((this.Session["transID"] != null) && (this.Session["memberID"] != null))
            {
                this.succ = this.reg.updateTwalletBanker(this.Session["transID"].ToString(), this.adminID, this.Session["memberID"].ToString());
                if (this.succ > 0)
                {
                    this.sendAlert();
                    this.show_inv = 0;
                    this.status_msg = "TRANSACTION :" + this.Session["transID"].ToString().ToUpper() + "HAS BEEN SUCCESSFULLY UPDATED!!";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect("../login.aspx");
            }
            if (!base.IsPostBack)
            {
                this.Session["transID"] = null;
                this.Session["memberID"] = null;
                this.Session["agentType"] = null;
            }
            if (this.rblagentType.SelectedValue != "")
            {
                this.Session["agentType"] = this.rblagentType.SelectedValue;
            }
            else if ((this.Session["agentType"] != null) && (this.Session["agentType"].ToString() != ""))
            {
                if (this.Session["agentType"].ToString() == "Agent")
                {
                    this.rblagentType.SelectedIndex = 0;
                }
                else
                {
                    this.rblagentType.SelectedIndex = 1;
                }
            }
        }

        protected void sendAlert()
        {
            this.fullname = this.Session["fullname"].ToString();
            this.email = this.Session["email"].ToString();
            this.mobile = this.Session["mobile"].ToString();
            Email email = new Email();
            Messenger messenger = new Messenger();
            string msg = (("Dear " + this.fullname + ",<br/>") + "Transaction : " + this.Session["transID"].ToString().ToUpper() + " has been successfully validated!<br/>") + "You may now use you items from your profile page.<br/>Regards";
            string s = ("Dear " + this.fullname + ",\r\n") + "Transaction : " + this.Session["transID"].ToString().ToUpper() + " has been successfully validated!\r\nYou may now use you items from your profile page.\r\nRegards";
            string subject = "XPAY ALERT";
            string from = "admin@xpay.com";
            string to = this.email;
            string mobile = this.mobile;
            s = base.Server.UrlEncode(s);
            if (mobile.StartsWith("0"))
            {
                mobile = "234" + mobile.Remove(0, 1);
            }
            email.sendMail("XPAY ALERT", to, from, subject, msg, "");
            string str7 = messenger.send_sms(s, "XPAY ALERT", mobile);
        }
    }
}

