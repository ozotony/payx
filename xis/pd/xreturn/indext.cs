namespace XPay.xis.pd.xreturn
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class indext : Page
    {
        protected string adminID = "3";
        public string amt = "0";
        protected string apprAmt = "0";
        protected Button btnProceed;
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.Twallet c_twall = new XObjs.Twallet();
        protected string cardNum = "0";
        protected string check_trans_page = "";
        public string desc = "";
        public string docpath = "";
        protected ErrorHandler eh = new ErrorHandler();
        public string email = "";
        public string err_desc = "";
        protected HtmlForm form1;
        public string fullname = "";
        public string hash = "";
        protected Hasher hash_value = new Hasher();
        protected HtmlHead Head1;
        public string html_msg = "";
        public string inputString = "";
        protected XObjs.InterSwitchResponse isr = new XObjs.InterSwitchResponse();
        protected XObjs.InterSwitchPostFields isw_fields = new XObjs.InterSwitchPostFields();
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.Hwallet> lt_hwall = new List<XObjs.Hwallet>();
        protected List<XObjs.PaymentReciept> lt_pr = new List<XObjs.PaymentReciept>();
        public string mackey = "";
        public string mobile = "";
        protected string payRef = "FBN|WEB|Einao|23-04-2014|047800";
        public string[] pending_code_list;
        public string product_id = "";
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        public string resp = "";
        public string response = "";
        protected Retriever ret = new Retriever();
        protected string retRef = "000000133154";
        private int succ = 0;
        public string total_amt = "0";
        protected Transactions tx = new Transactions();
        protected string txnref = "662813FEE85F";
        public XWriters x = new XWriters();
        protected string xpay_status = "0";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public StringBuilder xstring;

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
            if (this.Session["fullname"] != null)
            {
                this.fullname = this.Session["fullname"].ToString();
            }
            if (this.Session["email"] != null)
            {
                this.email = this.Session["email"].ToString();
            }
            if (this.Session["mobile"] != null)
            {
                this.mobile = this.Session["mobile"].ToString();
            }
            this.xstring = new StringBuilder();
            this.product_id = ConfigurationManager.AppSettings["pd_product_id_test"];
            this.mackey = ConfigurationManager.AppSettings["pd_mackey_test"];
            this.check_trans_page = ConfigurationManager.AppSettings["pd_get_trans_json_page_test"];
            if ((base.Request.QueryString["txnRef"] != null) && (base.Request.QueryString["txnRef"] != ""))
            {
                this.txnref = base.Request.QueryString["txnRef"].ToString();
                this.Session["transID"] = this.txnref;
            }
            if ((base.Request.QueryString["payRef"] != null) && (base.Request.QueryString["payRef"] != ""))
            {
                this.payRef = base.Request.QueryString["payRef"].ToString();
            }
            if ((base.Request.QueryString["retRef"] != null) && (base.Request.QueryString["retRef"] != ""))
            {
                this.retRef = base.Request.QueryString["retRef"].ToString();
            }
            if ((base.Request.QueryString["cardNum"] != null) && (base.Request.QueryString["cardNum"] != ""))
            {
                this.cardNum = base.Request.QueryString["cardNum"].ToString();
            }
            if ((base.Request.QueryString["apprAmt"] != null) && (base.Request.QueryString["apprAmt"] != ""))
            {
                this.apprAmt = base.Request.QueryString["apprAmt"].ToString();
            }
            if ((base.Request.QueryString["resp"] != null) && (base.Request.QueryString["resp"] != ""))
            {
                this.resp = base.Request.QueryString["resp"].ToString();
            }
            if ((base.Request.QueryString["desc"] != null) && (base.Request.QueryString["desc"] != ""))
            {
                this.desc = base.Request.QueryString["desc"].ToString();
            }
            if (!base.IsPostBack)
            {
                this.c_twall = this.ret.getTwalletByTransIDAdminID(this.txnref, this.adminID);
                this.c_app = this.ret.getApplicantByID(this.c_twall.applicantID);
                if (this.c_twall.xid != null)
                {
                    this.Session["c_twall"] = this.c_twall;
                    this.lt_fdets = this.ret.getFee_detailsByTwalletID(this.c_twall.xid);
                    if (this.lt_fdets.Count > 0)
                    {
                        this.Session["lt_fdets"] = this.lt_fdets;
                    }
                    this.lt_hwall = this.ret.getHwalletByTransID(this.txnref);
                    int num = 1;
                    int num2 = 0;
                    foreach (XObjs.Hwallet hwallet in this.lt_hwall)
                    {
                        XObjs.PaymentReciept item = new XObjs.PaymentReciept();
                        XObjs.Fee_list _list = new XObjs.Fee_list();
                        XObjs.Fee_details _details = new XObjs.Fee_details();
                        _details = this.ret.getFee_detailsByID(hwallet.fee_detailsID);
                        _list = this.ret.getFee_listByID(_details.fee_listID);
                        item.sn = num.ToString();
                        item.item_code = _list.item_code;
                        item.item_desc = _list.xdesc;
                        item.init_amt = string.Format("{0:n}", Convert.ToInt32(_details.init_amt));
                        item.tech_amt = string.Format("{0:n}", Convert.ToInt32(_details.tech_amt));
                        item.qty = string.Format("{0:n}", Convert.ToInt32(_details.xqty));
                        item.tot_amount = string.Format("{0:n}", Convert.ToInt32(_details.tot_amt));
                        item.transID = hwallet.transID + "-" + hwallet.fee_detailsID + "-" + hwallet.xid;
                        num2 += Convert.ToInt32(_details.tot_amt);
                        this.Session["amt"] = num2;
                        this.total_amt = string.Format("{0:n}", num2);
                        this.Session["total_amt"] = this.total_amt;
                        this.lt_pr.Add(item);
                        num++;
                    }
                }
                this.isw_fields = this.ret.getISWtransactionByTransactionID(this.txnref);
                this.xstring.AppendLine("Transaction reference= " + this.txnref + " Payment reference= " + this.payRef + " Switching Bank Reference number= " + this.retRef + " card No= " + this.cardNum + " apprAmt= " + this.apprAmt);
                this.inputString = this.product_id + this.txnref + this.mackey;
                string headerValue = this.hash_value.GetGetSHA512String(this.inputString);
                this.isr = this.tx.myRedirect(this.check_trans_page + "?productid=" + this.product_id + "&transactionreference=" + this.txnref + "&amount=" + this.isw_fields.amount, "Hash", headerValue);
                if ((this.isr.ResponseCode != "") && (this.isr.ResponseCode != null))
                {
                    this.err_desc = this.eh.getErrorDesc(this.isr.ResponseCode);
                    if (((this.err_desc != "") && (this.err_desc != null)) && (this.err_desc != "NA"))
                    {
                        this.isr.ResponseDescription = this.err_desc;
                    }
                    this.xstring.AppendLine("Sent Amount: " + this.isw_fields.amount + "\r\n Product ID: " + this.product_id + "\r\n Hash: " + headerValue + "\r\n Amount: " + this.isr.Amount + "\r\n CardNumber: " + this.isr.CardNumber + "\r\n MerchantReference: " + this.isr.MerchantReference + "\r\n PaymentReference: " + this.isr.PaymentReference + "\r\n RetrievalReferenceNumber: " + this.isr.RetrievalReferenceNumber + "\r\n LeadBankCbnCode: " + this.isr.LeadBankCbnCode + "\r\n TransactionDate: " + this.isr.TransactionDate + "\r\n ResponseCode: " + this.isr.ResponseCode + "\r\n ResponseDescription: " + this.isr.ResponseDescription + "\r\n Json Page: " + this.check_trans_page + "\r\n Form Response: " + this.resp + "\r\n Form Description: " + this.desc);
                    if (this.txnref != "")
                    {
                        this.docpath = base.Server.MapPath("~/") + "InterLogs/" + this.txnref + ".txt";
                        this.succ = this.x.WriteToFile(this.xstring.ToString(), this.docpath);
                    }
                    else
                    {
                        this.docpath = base.Server.MapPath("~/") + "InterLogs/xxx.txt";
                        this.succ = this.x.WriteToFile(this.xstring.ToString(), this.docpath);
                    }
                    this.succ = this.reg.updateInterSwitchRecords(this.txnref, this.payRef, this.retRef, this.isr.ResponseCode, this.isr.TransactionDate, this.isr.MerchantReference, this.isr.ResponseDescription,isr.PaymentReference);
                    if (this.isr.ResponseCode == "00")
                    {
                        this.xpay_status = "1";
                    }
                    else
                    {
                        this.xpay_status = "3";
                    }
                    if (this.succ != 0)
                    {
                        this.sendAlertHtml();
                    }
                }
                else
                {
                    string str2 = "None";
                    string str3 = "None";
                    this.xstring.AppendLine("Sent Amount: " + this.isw_fields.amount + "\r\n Product ID: " + this.product_id + "\r\n Hash: " + headerValue + "\r\n Amount: None\r\n CardNumber: None\r\n MerchantReference: None\r\n PaymentReference: None\r\n RetrievalReferenceNumber: None\r\n LeadBankCbnCode: None\r\n TransactionDate: None\r\n ResponseCode: " + str2 + "\r\n ResponseDescription: " + str3 + "\r\n Json Page: " + this.check_trans_page + "\r\n Form Response: " + this.resp + "\r\n Form Description: " + this.desc);
                    if (this.txnref != "")
                    {
                        this.docpath = base.Server.MapPath("~/") + "InterLogs/" + this.txnref + ".txt";
                        this.succ = this.x.WriteToFile(this.xstring.ToString(), this.docpath);
                    }
                    else
                    {
                        this.docpath = base.Server.MapPath("~/") + "InterLogs/xxx.txt";
                        this.succ = this.x.WriteToFile(this.xstring.ToString(), this.docpath);
                    }
                    this.xpay_status = "3";
                    if (this.desc == "")
                    {
                        this.isr.ResponseDescription = "Transaction Pending";
                    }
                    else
                    {
                        this.isr.ResponseDescription = this.desc;
                    }
                    if (this.resp == "")
                    {
                        this.isr.ResponseCode = "XXXX";
                    }
                    else
                    {
                        this.isr.ResponseCode = this.resp;
                    }
                    this.sendAlertHtml();
                }
            }
        }

        protected void sendAlert()
        {
            if (this.Session["fullname"] != null)
            {
                this.fullname = this.Session["fullname"].ToString();
            }
            if (this.Session["email"] != null)
            {
                this.email = this.Session["email"].ToString();
            }
            if (this.Session["mobile"] != null)
            {
                this.mobile = this.Session["mobile"].ToString();
            }
            Email email = new Email();
            Messenger messenger = new Messenger();
            string msg = "Dear " + this.fullname + ",<br/>";
            string s = "Dear " + this.fullname + ",\r\n";
            if (this.Session["Refno"] != null)
            {
                string str8;
                if (this.isr.ResponseCode == "00")
                {
                    msg = ((((msg + "Your payment transaction has been successfully completed!<br/>") + "Reason: " + this.isr.ResponseDescription + "<br />") + "Transaction Reference: " + this.Session["Refno"].ToString().ToUpper() + "<br/>") + "Payment Reference :" + this.payRef + "<br/>") + "Please check your \"Payment Status\" or \"History Log\" to view more details!!<br/><br/>Regards";
                    str8 = s;
                    s = str8 + "Your payment transaction has been successfully completed\r\nReason: " + this.isr.ResponseDescription + "\r\nTransaction Reference: " + this.Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + this.payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                }
                else
                {
                    msg = ((((msg + "Your payment transaction was not successfull!<br/>") + "Reason: " + this.isr.ResponseDescription + "<br />") + "Transaction Reference: " + this.Session["Refno"].ToString().ToUpper() + "<br/>") + "Payment Reference :" + this.payRef + "<br/>") + "Please check your \"Payment Status\" or \"History Log\" to view more details!!<br/><br/>Regards";
                    str8 = s;
                    s = str8 + "Your payment transaction was not successfull\r\nTransaction Reference: " + this.Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + this.payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                }
            }
            string subject = "XPAY ALERT";
            string from = "admin@xpay.com";
            string to = this.email;
            string mobile = this.mobile;
            s = base.Server.UrlEncode(s);
            if (mobile.StartsWith("0"))
            {
                mobile = "234" + mobile.Remove(0, 1);
            }
            email.sendMail("XPAY ALERT", to, from, subject, msg, "");
            string str7 = messenger.send_sms(s, "XPAY ALERT", mobile);
        }

        protected void sendAlertHtml()
        {
            if (this.Session["fullname"] != null)
            {
                this.fullname = this.Session["fullname"].ToString();
            }
            if (this.Session["email"] != null)
            {
                this.email = this.Session["email"].ToString();
            }
            if (this.Session["mobile"] != null)
            {
                this.mobile = this.Session["mobile"].ToString();
            }
            if (this.Session["total_amt"] != null)
            {
                this.total_amt = this.Session["total_amt"].ToString();
            }
            Email email = new Email();
            Messenger messenger = new Messenger();
            string str = "Dear " + this.fullname + ",<br/>";
            string str2 = "Dear " + this.fullname + ",\r\n";
            if (this.Session["Refno"] != null)
            {
                str = ((((str + "Your payment transaction has been successfully completed!<br/>") + "Reason: " + this.isr.ResponseDescription + "<br />") + "Transaction Reference: " + this.Session["Refno"].ToString().ToUpper() + "<br/>") + "Payment Reference :" + this.payRef + "<br/>") + "Please view more details below!!<br/><br/>Regards<br/><br/><br/><br/><hr>";
                string str6 = str2;
                str2 = str6 + "Your payment transaction has been successfully completed\r\nReason: " + this.isr.ResponseDescription + "\r\nTransaction Reference: " + this.Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + this.payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                this.html_msg = this.html_msg + str;
                this.html_msg = this.html_msg + "<html><head id='Head1' runat='server'><title>PAYMENT RECIEPT</title>";
                this.html_msg = this.html_msg + "<style type='text/css'>";
                this.html_msg = this.html_msg + ".item_alt {background-color:#E3EAEB; color:#000000;text-align:center;font-weight:bold;font-size:14px; } .tiger-stripe{ text-align:center;font-weight:bold;font-size:14px; }";
                this.html_msg = this.html_msg + ".tbbg{padding:0;margin:0 auto;width:100%;height:20px;background-color:#006699;text-align:center;color:#fff;font-weight:bold;border-color:#006699;}";
                this.html_msg = this.html_msg + ".tbbg{padding:0;margin:0 auto;width:100%;height:20px;background-color:#006699;text-align:center;color:#fff;font-weight:bold;border-color:#006699;}";
                this.html_msg = this.html_msg + "</style></head><body><form id='form1' runat='server'><div>";
                this.html_msg = this.html_msg + "<table  style=\"font-size:16px;width:100%;border:1px solid #000000;\">";
                this.html_msg = this.html_msg + " <tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                this.html_msg = this.html_msg + "<td colspan=\"2\">INVOICE/RECIEPT FOR TRANSACTION :&nbsp;" + this.c_twall.ref_no + " </td>";
                this.html_msg = this.html_msg + " </tr>";
                this.html_msg = this.html_msg + " <tr >";
                this.html_msg = this.html_msg + "<td  colspan=\"2\" style=\"text-align:center;\"> <img alt=\"Coat of Arms\" height=\"84px\" src=\"http://payx.com.ng/images/LOGOCLD.jpg\" width=\"509px\" /></td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\"> </td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\"><strong> DATE:</strong> " + this.xreg_date + "</td>";
                this.html_msg = this.html_msg + "<td align=\"center\"> <strong> INVOICE DATE:</strong>  " + this.c_twall.xreg_date + "</td>";
                this.html_msg = this.html_msg + " </tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\" style=\"background-color:#666; color:#ffffff;font-weight:bold;\">--- APPLICANT INFORMATION ---</td>";
                this.html_msg = this.html_msg + " </tr>";
                this.html_msg = this.html_msg + " <tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\"><strong> NAME:</strong><br />" + this.c_app.xname + "</td>";
                this.html_msg = this.html_msg + " </tr>";
                this.html_msg = this.html_msg + " <tr>";
                this.html_msg = this.html_msg + " <td align=\"center\" colspan=\"2\">&nbsp;</td>";
                this.html_msg = this.html_msg + " </tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + " <td align=\"center\"><strong> E-MAIL ADDRESS:</strong><br />" + this.c_app.xemail + "</td>";
                this.html_msg = this.html_msg + "<td align=\"center\"><strong>MOBILE NUMBER:</strong><br />" + this.c_app.xmobile + " </td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\" style=\"background-color:#666; color:#ffffff;font-weight:bold;\">---AGENT INFORMATION ---</td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\"><strong> NAME:</strong><br />" + this.fullname + "</td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\">&nbsp;</td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\"><strong> E-MAIL ADDRESS:</strong><br />" + this.email + "</td>";
                this.html_msg = this.html_msg + "<td align=\"center\"><strong>MOBILE NUMBER:</strong><br />" + this.mobile + " </td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\"  style=\"background-color:#666; color:#ffffff;font-weight:bold;\"><strong>--- PAYMENT DETAILS ---</strong></td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + " <tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\">&nbsp;</td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\" style=\"font-size:14px;\">";
                this.html_msg = this.html_msg + "<table style=\"width:100%;\" id=\"mitems\" class=\"tiger-stripe\" >";
                this.html_msg = this.html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff;\">";
                this.html_msg = this.html_msg + "<td style=\"width:5%;\"><strong>S/N</strong></td>";
                this.html_msg = this.html_msg + "<td style=\"width:20%;\"><strong>TRANSACTION ID</strong></td>";
                this.html_msg = this.html_msg + "<td style=\"width:10%;\"><strong>ITEM CODE</strong></td>";
                this.html_msg = this.html_msg + "<td style=\"width:50%;\"><strong>ITEM DESCRIPTION</strong></td>";
                this.html_msg = this.html_msg + "<td style=\"width:15%;\"><strong>AMOUNT (<em>NGN</em> )</strong></td>";
                this.html_msg = this.html_msg + "</tr>";
                foreach (XObjs.PaymentReciept reciept in this.lt_pr)
                {
                    this.html_msg = this.html_msg + "<tr>";
                    this.html_msg = this.html_msg + "<td>" + reciept.sn + "</td>";
                    this.html_msg = this.html_msg + "<td>" + reciept.transID + "</td>";
                    this.html_msg = this.html_msg + "<td>" + reciept.item_code + "</td>";
                    this.html_msg = this.html_msg + "<td>" + reciept.item_desc + "</td>";
                    this.html_msg = this.html_msg + "<td>" + reciept.qty + "</td>";
                    this.html_msg = this.html_msg + "<td style=\"text-align:right;\">" + reciept.init_amt + "</td>";
                    this.html_msg = this.html_msg + "<td style=\"text-align:right;\">" + reciept.tech_amt + "</td>";
                    this.html_msg = this.html_msg + "<td style=\"text-align:right;\">" + reciept.tot_amount + "</td>";
                    this.html_msg = this.html_msg + " </tr>";
                }
                this.html_msg = this.html_msg + "</table>";
                this.html_msg = this.html_msg + "</td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                this.html_msg = this.html_msg + "<td colspan=\"2\"></td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr style=\"font-size:16px;text-decoration:underline; color:#1C5E55; font-weight:bolder; text-align:right;\" align=\"right\" >";
                this.html_msg = this.html_msg + "<td colspan=\"2\" ><em>TOTAL AMOUNT:</em>&nbsp;" + this.total_amt + " NGN</td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                this.html_msg = this.html_msg + "<td colspan=\"2\"></td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td  style=\"text-align:left;\">";
                this.html_msg = this.html_msg + "<img alt=\"Einao Solutions\" src=\"http://payx.com.ng/images/einao_logo.png\"   width=\"100px\" height=\"50px\"/></td>";
                this.html_msg = this.html_msg + "<td style=\"text-align:right;\">";
                this.html_msg = this.html_msg + "<img alt=\"Einao Solutions\" src=\"http://payx.com.ng/images/payxlogo.jpg\"   width=\"100px\" height=\"50px\"/></td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + "<tr>";
                this.html_msg = this.html_msg + "<td align=\"center\" colspan=\"2\">";
                this.html_msg = this.html_msg + "Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />";
                this.html_msg = this.html_msg + "<a href=\"http://www.einaosolutions.com\">www.einaosolutions.com</a><br />";
                this.html_msg = this.html_msg + "Support E-mail(s): <a href=\"mailto:paymentsupport@einaosolutions.com\">paymentsupport@einaosolutions.com</a><br />";
                this.html_msg = this.html_msg + "Customer Contact Support Line(s): +2349038979681  ";
                this.html_msg = this.html_msg + "</td>";
                this.html_msg = this.html_msg + "</tr>";
                this.html_msg = this.html_msg + " </table>";
                this.html_msg = this.html_msg + "</table></div></form></body></html>";
            }
            string str5 = this.email;
        }
    }
}

