namespace XPay.P
{
    using System;
    using System.Configuration;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class xmail : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected Button btnSubmit;
        protected string check_trans_page = "";
        protected string coy_name = "";
        protected string cust_id = "";
        protected string email = "";
        protected HtmlForm form1;
        protected string fullname = "";
        protected HtmlHead Head1;
        protected string log_date = "";
        protected string mobile = "";
        protected RadioButtonList rbl_mail_cat;
        protected string ref_no = "";
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected string transID = "";
        protected TextBox txt_msg;
        private XObjs.Pwallet xpwallet = new XObjs.Pwallet();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public int xsucc = 0;

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            this.xsucc = 0;
            if (this.txt_msg.Text != "")
            {
                this.Session["msg"] = this.txt_msg.Text;
                this.sendMsg();
            }
            else
            {
                this.xsucc = 2;
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
            if ((this.Session["Pwallet"] != null) && (this.Session["Pwallet"].ToString() != ""))
            {
                this.xpwallet = (XObjs.Pwallet) this.Session["Pwallet"];
                this.Session["fullname"] = this.ret.getPartnerByID(this.xpwallet.xmemberID).cname;
                this.Session["email"] = this.xpwallet.xemail;
                this.Session["mobile"] = this.xpwallet.xmobile;
            }
        }

        protected void sendMsg()
        {
            if (this.Session["fullname"] != null)
            {
                this.fullname = this.Session["fullname"].ToString();
            }
            if (this.Session["email"] != null)
            {
                this.email = this.Session["email"].ToString();
            }
            Email email = new Email();
            Messenger messenger = new Messenger();
            if (this.Session["msg"] != null)
            {
                string subject = this.rbl_mail_cat.SelectedValue + " From: " + this.fullname;
                string from = this.email;
                string to = ConfigurationManager.AppSettings["contact_email"];
                if (email.sendMail(this.rbl_mail_cat.SelectedValue, to, from, subject, this.Session["msg"].ToString(), "") != "bad")
                {
                    this.txt_msg.Text = "";
                    this.xsucc = 1;
                }
            }
        }
    }
}

