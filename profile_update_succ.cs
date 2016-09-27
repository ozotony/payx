namespace XPay
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class profile_update_succ : Page
    {
        protected string adminID = "0";
        protected Button btnSignIn;
        protected HtmlForm form1;

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("./login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.Session["adminID"] = null;
                base.Response.Redirect("./login.aspx");
            }
            else
            {
                base.Response.Redirect("./login.aspx");
            }
        }
    }
}

