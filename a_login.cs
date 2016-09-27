namespace XPay
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class a_login : Page
    {
        public string adminID = "0";
        public string agentState = "0";
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        public string ccode = "";
        public string code_text = "";
        protected Button ConfirmDetails;
        public string email_text = "";
        public string enable_Captcha = "0";
        public string enable_Confirm = "0";
        public string enable_Save = "1";
        public int file_len = 0x400;
        public string file_string = "Xavier";
        protected HtmlForm form1;
        protected Hasher hash = new Hasher();
        public string new_hash = "";
        public string newp = "0";
        public string newState = "0";
        public static string OriginalIP = "";
        public string password_text = "";
        protected RadioButtonList rblAgentType;
        public static string remote_host = "";
        public static string remote_user = "";
        public static string RemoteIP = "";
        public Retriever ret = new Retriever();
        protected Button Save;
        public static string server_name = "";
        public static string server_url = "";
        public string serverpath = "";
        public string x_code = "";
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
            }
            else
            {
                this.newState = "1";
                this.xcode.Text = "";
            }
            if (this.rblAgentType.SelectedValue != "")
            {
                this.agentState = "0";
            }
            else
            {
                this.agentState = "1";
            }
            if ((this.rblAgentType.SelectedValue != "") && (str == this.xcode.Text.ToUpper()))
            {
                this.xpassword.Focus();
                this.enable_Save = "0";
                this.enable_Confirm = "1";
                this.enable_Captcha = "1";
                this.newp = "1";
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
            this.serverpath = base.Server.MapPath("~/");
            string clientIPAddress = login.GetClientIPAddress(this.Context.Request);
            if (!base.IsPostBack)
            {
                this.Session["AgentType"] = null;
            }
            else if (this.rblAgentType.SelectedValue != "")
            {
                this.Session["AgentType"] = this.rblAgentType.SelectedValue;
                this.enable_Captcha = "1";
                this.xcode.Focus();
            }
            else if ((this.Session["AgentType"] != null) && (this.Session["AgentType"].ToString() != ""))
            {
                this.enable_Captcha = "1";
                if (this.Session["AgentType"].ToString() == "Agent")
                {
                    this.rblAgentType.SelectedIndex = 0;
                }
                else
                {
                    this.rblAgentType.SelectedIndex = 1;
                }
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            this.ccode = ConfigurationManager.AppSettings["ccode"];
            this.x_code = ConfigurationManager.AppSettings["xcode"];
            if (this.xpassword.Text != "")
            {
                this.password_text = this.xpassword.Text;
                this.new_hash = this.hash.GetGetSHA512String(this.ccode + this.password_text + this.x_code);
            }
            string str = "";
            if ((this.Session["AgentType"] != null) && (this.Session["AgentType"].ToString() != ""))
            {
                str = this.Session["AgentType"].ToString();
            }
            if (str == "Agent")
            {
                this.adminID = this.ret.getAgentLogDetails(this.xemail.Text, this.new_hash);
                this.c_reg = this.ret.getRegistrationByID(this.adminID);
                this.Session["c_reg"] = this.c_reg;
            }
            else
            {
                this.adminID = this.ret.getSubAgentLogDetails(this.xemail.Text, this.new_hash);
                this.c_sub = this.ret.getSubAgentByID(this.adminID);
                this.Session["c_sub"] = this.c_sub;
                this.c_sub_reg = this.ret.getRegistrationBySubagentRegistrationID(this.c_sub.RegistrationID);
                this.Session["c_sub_reg"] = this.c_sub_reg;
            }
            if (this.adminID != "0")
            {
                if (this.ret.addAdminLog(this.adminID, RemoteIP, remote_host, remote_user, server_name, server_url) != "")
                {
                    this.Session["pwalletID"] = this.adminID;
                    base.Response.Redirect("./A/profile.aspx");
                }
                else
                {
                    base.Response.Redirect("./login.aspx");
                }
            }
        }

        protected void xpassword_Unload(object sender, EventArgs e)
        {
            this.password_text = this.xpassword.Text;
        }
    }
}

