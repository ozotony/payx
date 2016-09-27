using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XPay.Classes;
using System.Configuration;
using System.Text;

namespace PayX
{
      
    public partial class isw_test : System.Web.UI.Page
    {
        public string mackey = "";
        public string inputString = "";
        public string product_id = "";
        protected string check_trans_page = "";
        protected string txnref = "201503051052547";
        protected string amount = "2783249";
        protected XPay.InterSwitch.PayDirect.Classes.Transactions tx = new XPay.InterSwitch.PayDirect.Classes.Transactions();
        protected XObjs.InterSwitchResponse isr = new XObjs.InterSwitchResponse();
        protected XObjs.InterSwitchResponse isr2 = new XObjs.InterSwitchResponse();
        protected List<XObjs.InterSwitchPostFields> isw_fields = new List<XObjs.InterSwitchPostFields>();
        protected XPay.InterSwitch.PayDirect.Classes.Hasher hash_value = new XPay.InterSwitch.PayDirect.Classes.Hasher();
        protected XPay.InterSwitch.PayDirect.Classes.ErrorHandler eh = new XPay.InterSwitch.PayDirect.Classes.ErrorHandler();

        protected Retriever ret = new Retriever();
        protected Registration reg = new Registration();
        protected int up_cnt = 0; protected int up_tw_cnt = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
         //   product_id = ConfigurationManager.AppSettings["pd_product_id"];

            product_id = "4387";
          //  mackey = ConfigurationManager.AppSettings["pd_mackey"];
            mackey = "98182F4D8A980E79D1C8B199442BB38D15992401BDC56ED09EBA1EA9B9BE5A1D0EDF7935802D0B3E87E2618C87D3C4FD6BEC8D589F08ED9A7E30795DC6262A41";
           // check_trans_page = ConfigurationManager.AppSettings["pd_get_trans_json_page"];
            string vtrans_id = "201503051052547";
            check_trans_page = "https://webpay.interswitchng.com/paydirect/api/v1/gettransaction.json";

            isw_fields = ret.getISWtransactionBadRecords(vtrans_id);
            if(isw_fields.Count>0)
            {
                foreach(XObjs.InterSwitchPostFields x in isw_fields)
                { 
                inputString = product_id + x.txn_ref + mackey;
              
                string headerValue = hash_value.GetGetSHA512String(inputString);
                isr = tx.myRedirect(check_trans_page + "?productid=" + product_id.Trim() + "&transactionreference=" + x.txn_ref.Trim() + "&amount=" + x.vamount, "Hash", headerValue.Trim());
                string ss = "";
                //    if (isr.MerchantReference!=null)
                //{               
                //up_cnt += reg.updateInterSwitchRecords(x.txn_ref.Trim(), x.pay_ref, x.ret_ref, isr.ResponseCode, isr.TransactionDate, isr.MerchantReference, isr.ResponseDescription);
                //if (isr.ResponseCode == "00")
                //{
                //    up_tw_cnt += reg.updateTwalletPaymentStatus(x.txn_ref.Trim(), "1");
                //}
                //else
                //{
                //    up_tw_cnt += reg.updateTwalletPaymentStatus(x.txn_ref.Trim(), "3");
                //}
                //}
                
             }
            
            }

            
        }

    }
}