namespace XPay.X
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public partial class profile : Page
    {
        public string adminID = "0";
        protected HtmlForm form1;

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
        }
    }
}

