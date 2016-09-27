using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using XPay.Classes;
namespace PayX.Handler
{
    /// <summary>
    /// Summary description for GetTransaction2
    /// </summary>
    public class GetTransaction2 : IHttpHandler
    {
        public string mackey = "";
        public string inputString = "";
        public string product_id = "";
        protected string check_trans_page = "";
        protected string txnref = "201502270850474";
        protected string amount = "2783249";
        protected XPay.InterSwitch.PayDirect.Classes.Transactions tx = new XPay.InterSwitch.PayDirect.Classes.Transactions();
        protected XPay.Classes.XObjs.InterSwitchResponse isr = new XPay.Classes.XObjs.InterSwitchResponse();
        protected XPay.Classes.XObjs.InterSwitchResponse isr2 = new XPay.Classes.XObjs.InterSwitchResponse();
        protected List<XPay.Classes.XObjs.InterSwitchPostFields> isw_fields = new List<XPay.Classes.XObjs.InterSwitchPostFields>();
        protected XPay.InterSwitch.PayDirect.Classes.Hasher hash_value = new XPay.InterSwitch.PayDirect.Classes.Hasher();
        protected XPay.InterSwitch.PayDirect.Classes.ErrorHandler eh = new XPay.InterSwitch.PayDirect.Classes.ErrorHandler();

        protected Retriever ret = new Retriever();
        protected Registration reg = new Registration();
        protected int up_cnt = 0; protected int up_tw_cnt = 0;
        public void ProcessRequest(HttpContext context)
        {
            var pp = context.Request["vid"];
            String dd = "";
            JavaScriptSerializer ser = new JavaScriptSerializer();

            product_id = "4387";
            //  mackey = ConfigurationManager.AppSettings["pd_mackey"];
            mackey = "98182F4D8A980E79D1C8B199442BB38D15992401BDC56ED09EBA1EA9B9BE5A1D0EDF7935802D0B3E87E2618C87D3C4FD6BEC8D589F08ED9A7E30795DC6262A41";
            // check_trans_page = ConfigurationManager.AppSettings["pd_get_trans_json_page"];
            string vtrans_id = pp;
            check_trans_page = "https://webpay.interswitchng.com/paydirect/api/v1/gettransaction.json";

            isw_fields = ret.getISWtransactionBadRecords(vtrans_id);
    






            context.Response.ContentType = "application/json";
            context.Response.Write(ser.Serialize(isw_fields));
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