namespace XPay
{
    using System;
    using System.Web;
    using System.Web.SessionState;

    public partial class a_lo : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Session["pwalletID"] = null;
            context.Response.Redirect("./a_login.aspx");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

