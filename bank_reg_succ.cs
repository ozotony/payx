namespace XPay
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class bank_reg_succ : Page
    {
        protected Button btnSignIn;
        protected HtmlForm form1;
        public string m = "";
        public string x = "";

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("./login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((base.Request.QueryString["x"] != null) && (base.Request.QueryString["x"] != ""))
            {
                this.x = base.Request.QueryString["x"].ToString().ToUpper();
            }
            else
            {
                base.Response.Redirect("bank_reg.aspx");
            }
            if ((base.Request.QueryString["m"] != null) && (base.Request.QueryString["m"] != ""))
            {
                this.m = base.Request.QueryString["m"].ToString().ToUpper();
            }
            else
            {
                base.Response.Redirect("bank_reg.aspx");
            }
        }
    }
}

