namespace XPay
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class login : Page
    {
        public string adminID = "0";
        public string code_text = "";
       // public string code_text2 = "";

        protected Button ConfirmDetails;
        public string email_text = "";
        public string enable_Captcha = "1";
        public string enable_Confirm = "0";
        public string enable_Save = "1";
        protected HtmlForm form1;
        public string newp = "0";
        public string newState = "0";
        public static string OriginalIP = "";
        public string password_text = "";
        public static string remote_host = "";
        public static string remote_user = "";
        public static string RemoteIP = "";
        public Retriever ret = new Retriever();
        protected Button Save;
        public static string server_name = "";
        public static string server_url = "";
        protected TextBox xcode;
        protected TextBox xemail;
        protected TextBox xpassword;
        public zues z = new zues();

        protected void ConfirmDetails_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (this.xemail.Text == "")
            {
                this.email_text = "1";
                num++;
            }
            if (this.xcode.Text == "")
            {
                this.code_text = "1";
                num++;
            }
            if (num != 0)
            {
                base.Response.Write("<script language=JavaScript>alert('Please fill in the marked fileds')</script>");
            }
            else
            {
                this.doCaptcha();
            }
        }

        protected void doCaptcha()
        {
            string str = "";
            if (this.Session["Captcha"] != null)
            {
                str = this.Session["Captcha"].ToString();
            }
            if (str == this.xcode.Text.ToUpper())
            {
                this.newState = "0";
                this.enable_Save = "0";
                this.enable_Confirm = "1";
                this.enable_Captcha = "0";
                this.newp = "1";
                this.xpassword.Focus();
            }
            else
            {
                this.newState = "1";
                this.xcode.Text = "";
            }
        }

        public static string GetClientIPAddress(HttpRequest httpRequest)
        {
            OriginalIP = "Proxy IP: " + httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
            RemoteIP = "Remote IP: " + httpRequest.ServerVariables["LOCAL_ADDR"];
            remote_host = "Remote Host: " + httpRequest.ServerVariables["REMOTE_HOST"];
            remote_user = "User Agent: " + httpRequest.UserAgent;
            server_name = "UserHostName: " + httpRequest.UserHostName;
            server_url = "UserHostAddress: " + httpRequest.UserHostAddress;
            if ((OriginalIP != null) && (OriginalIP.Trim().Length > 0))
            {
                return (OriginalIP + "(" + RemoteIP + ")");
            }
            return RemoteIP;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string clientIPAddress = GetClientIPAddress(this.Context.Request);
            if (!base.IsPostBack)
            {
                this.xemail.Focus();
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (this.xpassword.Text != "")
            {
                this.password_text = this.xpassword.Text;
            }
            XObjs.Pwallet pwallet = this.ret.a_xadminz(this.xemail.Text, this.xpassword.Text);
            this.adminID = pwallet.xid;
            if ((this.adminID != null) && (this.adminID != "0"))
            {
                if (!(this.ret.addAdminLog(this.adminID, RemoteIP, remote_host, remote_user, server_name, server_url) != ""))
                {
                    base.Response.Redirect("./login.aspx");
                }
                else
                {
                    this.Session["pwalletID"] = this.adminID;
                    this.Session["Pwallet"] = pwallet;
                    string xmembertype = pwallet.xmembertype;
                    if (xmembertype != null)
                    {
                        if (!(xmembertype == "ra"))
                        {
                            if (xmembertype == "rb")
                            {
                                base.Response.Redirect("./B/m_status.aspx");
                            }
                            else if (xmembertype == "rc")
                            {
                                base.Response.Redirect("./C/profile.aspx");
                            }
                            else if (xmembertype == "rp")
                            {
                                this.Session["merchant_type"] = this.ret.getPartnerByID(pwallet.xmemberID).xsync;
                                base.Response.Redirect("./P/pay_his.aspx");
                            }
                            else if (xmembertype == "rx")
                            {
                                base.Response.Redirect("./X/profile.aspx");
                            }
                        }
                        else
                        {
                            base.Response.Redirect("./A/profile.aspx");
                        }
                    }
                }
            }
        }

        protected void xpassword_Unload(object sender, EventArgs e)
        {
            this.password_text = this.xpassword.Text;
        }
    }
}

