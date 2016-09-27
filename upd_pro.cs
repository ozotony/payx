namespace XPay
{
    using System;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class upd_pro : Page
    {
        protected string addressID = "0";
        protected string adminID = "0";
        protected Button btnAddMember;
        protected Button btnBack;
        protected HtmlForm form1;
        protected string name = "";
        private XPay.Classes.Registration reg = new XPay.Classes.Registration();
        private Retriever ret = new Retriever();
        private Validator val = new Validator();
        protected TextBox xemail;
        protected TextBox xname;
        protected TextBox xpass;
        private XObjs.Pwallet xpwallet = new XObjs.Pwallet();
        protected TextBox xtelephone;

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            if (((this.xemail.Text == "") || (this.xpass.Text == "")) || (this.xtelephone.Text == ""))
            {
                if (this.xemail.Text == "")
                {
                    this.xemail.BorderColor = Color.Red;
                }
                else
                {
                    this.xemail.BorderColor = Color.Green;
                }
                if (this.xpass.Text == "")
                {
                    this.xpass.BorderColor = Color.Red;
                }
                else
                {
                    this.xpass.BorderColor = Color.Green;
                }
                if (this.xtelephone.Text == "")
                {
                    this.xtelephone.BorderColor = Color.Red;
                }
                else
                {
                    this.xtelephone.BorderColor = Color.Green;
                }
                this.btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE TO FILL IN ALL THE ENTRIES MARKED WITH A RED STAR!!'); </script>");
            }
            else if (((this.xemail.Text != "") && (this.xpass.Text != "")) && (this.xtelephone.Text != ""))
            {
                int num = 0;
                if (this.btnAddMember.Text == "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT")
                {
                    this.btnAddMember.Text = "UPDATE";
                }
                else
                {
                    num += this.val.IsValidMobile(this.xtelephone.Text);
                    num += this.val.IsValidEmail(this.xemail.Text);
                    if (num > 0)
                    {
                        this.btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                        base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE THAT THE E-MAIL ADDRESS AND MOBILE NUMBER ARE IN THE CORRECT FORMAT!!'); </script>");
                    }
                    else
                    {
                        this.reg.updatePwalletProfile(this.adminID, this.xemail.Text, this.xtelephone.Text, this.xpass.Text, this.addressID);
                        this.xtelephone.BorderColor = Color.White;
                        this.xemail.BorderColor = Color.White;
                        this.xpass.BorderColor = Color.White;
                        Email email = new Email();
                        string msg = ("Dear " + this.name + ",<br/>") + "Your profile has been updated on the CLD Platform!<br/>Please notify our ADMIN if this was not intended.<br/>Regards";
                        string subject = "CLD PROFILE UPDATE";
                        string from = "admin@cldng.com";
                        string text = this.xemail.Text;
                        email.sendMail("CLD PROFILE UPDATE", text, from, subject, msg, "");
                        base.Response.Redirect("profile_update_succ.aspx");
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (this.ViewState["PreviousPage"] != null)
            {
                base.Response.Redirect(this.ViewState["PreviousPage"].ToString());
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
                base.Response.Redirect("./login.aspx");
            }
            this.xpwallet = this.ret.getPwalletByID(this.adminID);
            if (this.xpwallet.xmembertype == "rc")
            {
                this.name = this.ret.getMemberByID(this.xpwallet.xmemberID).xname;
                this.addressID = this.ret.getMemberByID(this.xpwallet.xmemberID).addressID;
            }
            else if (this.xpwallet.xmembertype == "rb")
            {
                this.name = this.ret.getBankerByID(this.xpwallet.xmemberID).xname;
                this.addressID = this.ret.getBankerByID(this.xpwallet.xmemberID).addressID;
            }
            else if (this.xpwallet.xmembertype == "ra")
            {
                this.name = this.ret.getAgentByID(this.xpwallet.xmemberID).xname;
                this.addressID = this.ret.getAgentByID(this.xpwallet.xmemberID).addressID;
            }
            else if (this.xpwallet.xmembertype == "rp")
            {
                this.name = this.ret.getPartnerByID(this.xpwallet.xmemberID).cname;
                this.addressID = this.ret.getPartnerByID(this.xpwallet.xmemberID).addressID;
            }
            this.xname.Text = this.name.ToUpper();
            if (!base.IsPostBack)
            {
                this.ViewState["PreviousPage"] = base.Request.UrlReferrer;
                this.xemail.Text = this.xpwallet.xemail;
                this.xtelephone.Text = this.xpwallet.xmobile;
                this.xpass.Text = this.xpwallet.xpass;
            }
        }
    }
}

