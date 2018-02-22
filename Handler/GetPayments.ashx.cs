using PayX.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using XPay.Classes;

namespace PayX.Handler
{
    /// <summary>
    /// Summary description for GetPayments
    /// </summary>
    public class GetPayments : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            zues ret = new zues();
            var pp = (context.Request["vid"]);

            var pp2 = (context.Request["vid2"]);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = Int32.MaxValue;

     
            List<Payments> lt_gen_isw = lt_gen_isw = ret.getPayment2(pp, pp2);

            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(lt_gen_isw));
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