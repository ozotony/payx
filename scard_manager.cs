namespace XPay
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using XPay.Classes;

    public partial class scard_manager : Page
    {
        protected string adminID = "0";
        protected HtmlForm form1;
        public List<XObjs.Scard> lt_scards = new List<XObjs.Scard>();
        public XObjs.Scard sc = new XObjs.Scard();
        public ScardManager scm = new ScardManager();
        public string xlogstaff = "1";
        public string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public string xsync = "0";
        public string xvalid = "1";
        public string xvisible = "1";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["pwalletID"] != null)
            {
                if (this.Session["pwalletID"].ToString() != "")
                {
                    this.adminID = this.Session["pwalletID"].ToString();
                }
                else
                {
                    base.Response.Redirect("./login.aspx");
                }
            }
            else
            {
                base.Response.Redirect("./login.aspx");
            }
            this.lt_scards = this.scm.GenerateGuidNum(12, 0x30d40);
            if (this.lt_scards.Count > 0)
            {
                this.scm.addScards(this.lt_scards);
            }
        }
    }
}

