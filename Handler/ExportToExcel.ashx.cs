using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using XPay.Classes;

namespace PayX.Handler
{
    /// <summary>
    /// Summary description for ExportToExcel
    /// </summary>
    public class ExportToExcel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Retriever ret = new Retriever();
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = Int32.MaxValue;

            var pp = (context.Request["vid"]);

            var pp2 = (context.Request["vid2"]);


            List<XObjs.ReportItemGenISW> lt_gen_isw = lt_gen_isw = ret.getPaymentReportItemGenISW("1",  pp,pp2, "", "ASC");

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