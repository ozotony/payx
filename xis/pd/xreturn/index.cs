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
    using System.IO;
    using System.Net.Mail;
    using PayX.Classes;

    public partial class index : Page
    {
        protected string adminID = "0";
        public string amt = "0";
        protected string apprAmt = "0";
        protected Button btnProceed;
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.Twallet c_twall = new XObjs.Twallet();
        protected string cardNum = "0";
        protected string check_trans_page = "";
        public string cust_id = "";
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
        protected string payRef = "";
        public string[] pending_code_list;
        public string product_id = "";
        public HiddenField xname;
        public HiddenField xname2;
        public HiddenField vamount;
        public HiddenField vtransactionid;
        public HiddenField vtype;
        public MarkInfo kkx = new MarkInfo();


        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        public string resp = "";
        public string response = "";
        protected Retriever ret = new Retriever();
        protected string retRef = "";
        private int succ = 0;
        public string total_amt = "0";
        protected Transactions tx = new Transactions();
        protected string txnref = "";
        public XWriters x = new XWriters();
        protected string xpay_status = "0";
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        public StringBuilder xstring;
        string vitem_code = "";
        String vid = "";
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            kkx = null; 


            if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
            if (Session["fullname"] != null)
            {
                fullname = Session["fullname"].ToString();
            }
            if (Session["email"] != null)
            {
                email = Session["email"].ToString();
            }
            if (Session["mobile"] != null)
            {
                mobile = Session["mobile"].ToString();
            }
            if (Session["cust_id"] != null)
            {
                cust_id = Session["cust_id"].ToString();
            }
            xstring = new StringBuilder();
            product_id = ConfigurationManager.AppSettings["pd_product_id"];
            mackey = ConfigurationManager.AppSettings["pd_mackey"];
            check_trans_page = ConfigurationManager.AppSettings["pd_get_trans_json_page"];
            if ((Request.Form["txnRef"] != null) && (Request.Form["txnRef"] != ""))
            {
                txnref = Request.Form["txnRef"].ToString();
                Session["transID"] = txnref;
            }
            if ((Request.Form["payRef"] != null) && (Request.Form["payRef"] != ""))
            {
                payRef = Request.Form["payRef"].ToString();
            }
            if ((Request.Form["retRef"] != null) && (Request.Form["retRef"] != ""))
            {
                retRef = Request.Form["retRef"].ToString();
            }
            if ((Request.Form["cardNum"] != null) && (Request.Form["cardNum"] != ""))
            {
                cardNum = Request.Form["cardNum"].ToString();
            }
            if ((Request.Form["apprAmt"] != null) && (Request.Form["apprAmt"] != ""))
            {
                apprAmt = Request.Form["apprAmt"].ToString();
            }
            if ((Request.Form["resp"] != null) && (Request.Form["resp"] != ""))
            {
                resp = Request.Form["resp"].ToString();
            }
            if ((Request.Form["desc"] != null) && (Request.Form["desc"] != ""))
            {
                desc = Request.Form["desc"].ToString();
            }
            if (!IsPostBack)
            {
                Session["vitem_code"] = null;
                c_twall = ret.getTwalletByTransIDAdminID(txnref, adminID);
                c_app = ret.getApplicantByID(c_twall.applicantID);
                isw_fields = ret.getISWtransactionByTransactionID(txnref.Trim());
                if (c_twall.xid != null)
                {
                    Session["c_twall"] = c_twall;
                    lt_fdets = ret.getFee_detailsByTwalletID(c_twall.xid);
                    if (lt_fdets.Count > 0)
                    {
                        Session["lt_fdets"] = lt_fdets;
                    }
                    lt_hwall = ret.getHwalletByTransID(txnref);
                    int num = 1;
                    int num2 = 0;
                    XObjs.Registration  c_reg2 = (XObjs.Registration)Session["c_reg"];
                 vid = c_reg2.xid;
                    foreach (XObjs.Hwallet hwallet in lt_hwall)
                    {
                        XObjs.PaymentReciept item = new XObjs.PaymentReciept();
                        XObjs.Fee_list _list = new XObjs.Fee_list();
                        XObjs.Fee_details _details = new XObjs.Fee_details();
                        _details = ret.getFee_detailsByID(hwallet.fee_detailsID);
                        _list = ret.getFee_listByID(_details.fee_listID);
                        item.sn = num.ToString();
                        item.item_code = _list.item_code;

                        if (item.item_code == "AA1")
                        {

                            Session["vitem_code"] = "AA1";
                        }
                        item.item_desc = _list.xdesc;
                        item.init_amt = string.Format("{0:n}", Convert.ToInt32(_details.init_amt));
                        item.tech_amt = string.Format("{0:n}", Convert.ToInt32(_details.tech_amt));
                        item.qty = string.Format("{0:n}", 1);
                        int num3 = Convert.ToInt32(_details.init_amt) + Convert.ToInt32(_details.tech_amt);
                        item.tot_amount = string.Format("{0:n}", num3);
                        item.transID = hwallet.transID + "-" + hwallet.fee_detailsID + "-" + hwallet.xid;
                        num2 += num3;
                        Session["amt"] = num2;
                        total_amt = string.Format("{0:n}", num2 + Math.Round(Convert.ToDouble(isw_fields.isw_conv_fee), 2));
                        Session["total_amt"] = total_amt;
                        lt_pr.Add(item);
                        num++;
                    }
                }
                xstring.AppendLine("Transaction reference= " + txnref + " Payment reference= " + payRef + " Switching Bank Reference number= " + retRef + " card No= " + cardNum + " apprAmt= " + apprAmt);
                inputString = product_id.Trim() + txnref.Trim() + mackey.Trim();
                string headerValue = hash_value.GetGetSHA512String(inputString);
                
              

                isr = tx.myRedirect(check_trans_page.Trim() + "?productid=" + product_id.Trim() + "&transactionreference=" + txnref.Trim() + "&amount=" + isw_fields.amount.Trim(), "Hash", headerValue.Trim());
               
                if (((isr.ResponseCode != "") && (isr.ResponseCode != null)) && (isr.ResponseCode == "00"))
                {
                    err_desc = eh.getErrorDesc(isr.ResponseCode);
                    if (((err_desc != "") && (err_desc != null)) && (err_desc != "NA"))
                    {
                        isr.ResponseDescription = err_desc;
                    }
                    xstring.AppendLine("Sent Amount: " + isw_fields.amount + "\r\n Product ID: " + product_id + "\r\n Hash: " + headerValue + "\r\n Amount: " + isr.Amount + "\r\n CardNumber: " + isr.CardNumber + "\r\n MerchantReference: " + isr.MerchantReference + "\r\n PaymentReference: " + isr.PaymentReference + "\r\n RetrievalReferenceNumber: " + isr.RetrievalReferenceNumber + "\r\n LeadBankCbnCode: " + isr.LeadBankCbnCode + "\r\n TransactionDate: " + isr.TransactionDate + "\r\n ResponseCode: " + isr.ResponseCode + "\r\n ResponseDescription: " + isr.ResponseDescription + "\r\n Json Page: " + check_trans_page + "\r\n Form Response: " + resp + "\r\n Form Description: " + desc);
                    if (txnref != "")
                    {
                        if (Directory.Exists(Server.MapPath("~/") + "InterLogs/"))
                        {
                        docpath = Server.MapPath("~/") + "InterLogs/" + txnref + ".txt";                       
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                        } 
                    }
                        
                    else
                    {
                        if (Directory.Exists(Server.MapPath("~/") + "InterLogs/"))
                        {
                            docpath = Server.MapPath("~/") + "InterLogs/xxx.txt";
                            succ = x.WriteToFile(xstring.ToString(), docpath);
                        }
                    }
                    succ = reg.updateInterSwitchRecords(txnref, payRef, retRef, isr.ResponseCode, isr.TransactionDate, isr.MerchantReference, isr.ResponseDescription,isr.PaymentReference);

                  

                    if (isr.ResponseCode == "00" && (isr.PaymentReference!=null ||isr.PaymentReference!="") )
                    {
                        xpay_status = "1";
                    }
                    else
                    {
                        xpay_status = "3";
                    }
                    reg.updateTwalletPaymentStatus(txnref.Trim(), xpay_status.Trim());
                    if (succ != 0)
                    {
                        if (Session["vitem_code"] != null)
                        {

                            vitem_code = Convert.ToString(Session["vitem_code"]);
                            Retriever kp = new Retriever();
                           // int vmax = kp.getMaxSysId();
                           // vmax=vmax+1;
                           // String vsys_id = "CLD/RA/0" + vmax;

                            Registration dd = new Registration();
                           // dd.updateRegistrationSysID(vid, vsys_id);
                            dd.updateRegistrationSysID2(vid, "Paid");
                           

                       XObjs.Registration ds=kp.getRegistrationByID(vid);
                       sendemail(ds);

                           // Session["cust_id"] = vsys_id;
                            if (Session["cust_id"] != null) {  
                            cust_id = Session["cust_id"].ToString();

                            }
                           

                        }

                        if (Session["onlineid"] != null)
                        {
                            Registration dd = new Registration();
                           
                            string dpp = Convert.ToString(Session["onlineid"]);
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(dpp);
                            dd.updateTransID2(dpp.Trim(), txnref.Trim());

                            dd.updateHwallet2(txnref.Trim(), "Used");


                        }

                        if (Session["onlineid2"] != null)
                        {
                            Registration dd = new Registration();
                            string dpp = Convert.ToString(Session["onlineid2"]);
                            dd.updateTransID3(dpp, txnref.Trim());
                            dd.updateHwallet2(txnref.Trim(), "Used");


                        }


                        if (Session["onlineid3"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid3"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "Name";
                           
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());
                            dd.updateHwallet2(txnref.Trim(), "Used");
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);

                        }

                        if (Session["onlineid4"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid4"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "Address";

                            dd.updateHwallet2(txnref.Trim(), "Used");

                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());


                        }

                        if (Session["onlineid5"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid5"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "Agent";
                            dd.updateHwallet2(txnref.Trim(), "Used");
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());


                        }

                        if (Session["onlineid6"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid6"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "Rectification";
                            dd.updateHwallet2(txnref.Trim(), "Used");
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());


                        }


                        if (Session["onlineid7"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid7"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "Assignment";
                            dd.updateHwallet2(txnref.Trim(), "Used");
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());


                        }

                        if (Session["onlineid8"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid8"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "Assignment2";
                            dd.updateHwallet2(txnref.Trim(), "Used");
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());


                        }

                        if (Session["onlineid9"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid9"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "Renewal";
                            dd.updateHwallet2(txnref.Trim(), "Used");
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());


                        }


                        if (Session["onlineid10"] != null)
                        {
                            Registration dd = new Registration();
                            xname.Value = "Success";
                            xname2.Value = Convert.ToString(Session["onlineid10"]);
                            vamount.Value = isr.Amount;
                            vtransactionid.Value = isr.MerchantReference;
                            vtype.Value = "TradeMarkAmendment";
                            dd.updateHwallet2(txnref.Trim(), "Used");
                            Retriever dp = new Retriever();
                            kkx = dp.getMarkInfo(xname2.Value);
                           
                            //Registration dd = new Registration();
                            //string dpp = Convert.ToString(Session["onlineid2"]);
                            //dd.updateTransID3(dpp, txnref.Trim());


                        }




                        sendAlertHtml();
                    }
                }
                else if (((isr.ResponseCode != "") && (isr.ResponseCode != null)) && (isr.ResponseCode != "00"))
                {
                    err_desc = eh.getErrorDesc(isr.ResponseCode);
                    if (((err_desc != "") && (err_desc != null)) && (err_desc != "NA"))
                    {
                        isr.ResponseDescription = err_desc;
                    }
                    xstring.AppendLine("Sent Amount: " + isw_fields.amount + "\r\n Product ID: " + product_id + "\r\n Hash: " + headerValue + "\r\n Amount: " + isr.Amount + "\r\n CardNumber: " + isr.CardNumber + "\r\n MerchantReference: " + isr.MerchantReference + "\r\n PaymentReference: " + isr.PaymentReference + "\r\n RetrievalReferenceNumber: " + isr.RetrievalReferenceNumber + "\r\n LeadBankCbnCode: " + isr.LeadBankCbnCode + "\r\n TransactionDate: " + isr.TransactionDate + "\r\n ResponseCode: " + isr.ResponseCode + "\r\n ResponseDescription: " + isr.ResponseDescription + "\r\n Json Page: " + check_trans_page + "\r\n Form Response: " + resp + "\r\n Form Description: " + desc);
                    if (txnref != "")
                    {
                        docpath = Server.MapPath("~/") + "InterLogs/" + txnref + ".txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    else
                    {
                        docpath = Server.MapPath("~/") + "InterLogs/xxx.txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    succ = reg.updateInterSwitchRecords(txnref, payRef, retRef, isr.ResponseCode, isr.TransactionDate, isr.MerchantReference, isr.ResponseDescription,isr.PaymentReference);
                    if (isr.ResponseCode == "00" && (isr.PaymentReference != null || isr.PaymentReference != ""))
                    {
                        xpay_status = "1";
                    }
                    else
                    {
                        xpay_status = "3";
                    }
                    reg.updateTwalletPaymentStatus(txnref.Trim(), xpay_status.Trim());
                    if (succ != 0)
                    {
                        sendUnsuccAlertHtml();
                    }
                }
                else if ((isr.ResponseCode == "") || (isr.ResponseCode == null))
                {
                    string str2 = "None";
                    string str3 = "None";
                    xstring.AppendLine("Sent Amount: " + isw_fields.amount + "\r\n Product ID: " + product_id + "\r\n Hash: " + headerValue + "\r\n Amount: None\r\n CardNumber: None\r\n MerchantReference: None\r\n PaymentReference: None\r\n RetrievalReferenceNumber: None\r\n LeadBankCbnCode: None\r\n TransactionDate: None\r\n ResponseCode: " + str2 + "\r\n ResponseDescription: " + str3 + "\r\n Json Page: " + check_trans_page + "\r\n Form Response: " + resp + "\r\n Form Description: " + desc);
                    if (txnref != "")
                    {
                        docpath = Server.MapPath("~/") + "InterLogs/" + txnref + ".txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    else
                    {
                        docpath = Server.MapPath("~/") + "InterLogs/xxx.txt";
                        succ = x.WriteToFile(xstring.ToString(), docpath);
                    }
                    xpay_status = "3";
                    reg.updateTwalletPaymentStatus(txnref, xpay_status);
                    if (desc == "")
                    {
                        isr.ResponseDescription = "Transaction Pending";
                    }
                    else
                    {
                        isr.ResponseDescription = desc;
                    }
                    if (resp == "")
                    {
                        isr.ResponseCode = "XXXX";
                    }
                    else
                    {
                        isr.ResponseCode = resp;
                    }
                    sendUnsuccAlertHtml();
                }
            }
        }

        public void sendemail(XObjs.Registration px2)
        {
            try
            {
                int port = 0x24b;


                MailMessage mail = new MailMessage();
                mail.From =
           new MailAddress("noreply@iponigeria.com", "noreply@iponigeria.com");
                // new MailAddress("tradeservices@fsdhgroup.com");
                mail.Priority = MailPriority.High;

                mail.To.Add(
    new MailAddress(px2.Email));

                //    new MailAddress("ozotony@yahoo.com"));



                //mail.CC.Add(new MailAddress("Anthony.Ozoagu@firstcitygroup.com"));

                mail.Subject = "Agent Accreditation Request Approved";

                mail.IsBodyHtml = true;
                String ss2 = "Dear " + px2.CompanyName + ",<br/> <br/>" + " You have successfully made payment for:   .<br/>";

                //  ss2 = ss2 + "To gain access to your account, you would need to click here <a href=\"http://88.150.164.30/IpoTest2/#/Register/" + vid + " \">click</a>   to validate your account and also make payment. " + "<br/><br/><br/>";
              
                ss2 = ss2 + " <table style=\"border:1px solid black;border-collapse:collapse; \"   >  <tr> <td style=\"border:1px solid black;\" > AGENT CODE </td> <td style=\"border:1px solid black;\" >" + px2.Sys_ID + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > FIRSTNAME </td> <td style=\"border:1px solid black;\" >" + px2.Firstname + "</td> </tr>";
                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > SURNAME </td> <td style=\"border:1px solid black;\" >" + px2.Surname + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > EMAIL </td> <td style=\"border:1px solid black;\" >" + px2.Email + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > DATE OF BIRTH </td> <td style=\"border:1px solid black;\" >" + px2.DateOfBrith + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > COMPANY NAME </td> <td style=\"border:1px solid black;\" >" + px2.CompanyName + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > COMPANY ADDRESS </td> <td style=\"border:1px solid black;\" >" + px2.CompanyAddress + "</td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" > CERTIFICATE </td> <td style=\"border:1px solid black;\" ><a href=\"http://ipo.cldng.com/" + px2.Certificate + " \">Download</a> </td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" >  LETTER OF INTRODUCTION </td> <td style=\"border:1px solid black;\" ><a href=\"http://ipo.cldng.com/" + px2.Introduction + " \">Download</a> </td> </tr>";

                ss2 = ss2 + "<tr> <td style=\"border:1px solid black;\" >  PASSPORT </td> <td style=\"border:1px solid black;\" ><a href=\"http://ipo.cldng.com/" + px2.logo + " \">Download</a> </td> </tr>";





                ss2 = ss2 + "</table> <br/><br/>";

                ss2 = ss2 + "Your application will be reviewed by the accreditation panel to ensure you have met the minimum standard requirements as required by the registry. <br/>";

                ss2 = ss2 + "You will be notified on the status of your application in due course. <br/> <br/>";


             

                ss2 = ss2 + "Please do not reply this mail <br/> <br/>";

                ss2 = ss2 + "Live 24/7 Support: (+234) 09038979681 <br/>";

                ss2 = ss2 + "info@iponigeria.com or go online to use our live feedback form <br/><br/> ";

                ss2 = ss2 + "<b>Disclaimer:</b>This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorized use, disclosure or copying is strictly prohibited and may be unlawful. Iponigeria.com disclaims any liability for any action taken in reliance on the content of this e-mail. The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of iponigeria.com. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.";










                //ss2 = ss2 + "Please keep your password safe and do not share your log in details with anyone. You may change your password at your convenience. In the event that you cannot remember your password, kindly follow the instructions provided for password recovery."  + "<br/>";
                //ss2 = ss2 + "Please do not reply this mail" +  "<br/><br/>";
                //ss2 = ss2 + "Email: info@iponigeria.com or go online to use our live feedback form .<br/><br/>";

                String ss = "<html> <head> </head> <body>" + ss2 + "</body> </html>";

                //  mail.Body = ss;

                mail.Body = ss;

                SmtpClient client = new SmtpClient("88.150.164.30");
                //  SmtpClient client = new SmtpClient("192.168.0.12");

                client.Port = port;

                //    client.Credentials = new System.Net.NetworkCredential("paymentsupport@einaosolutions.com", "Zues.4102.Hector");

                client.Credentials = new System.Net.NetworkCredential("noreply@iponigeria.com", "Einao2015@@$");



                //   new System.Net.NetworkCredential("ebusiness@firstcitygroup.com", "welcome@123");
                //   new System.Net.NetworkCredential(q60.smtp_user, q60.smtp_password);







                client.Send(mail);

            }
            catch (Exception ee)
            {


            }



        }
        
        protected void sendAlertHtml()
        {
            if (Session["fullname"] != null)
            {
                fullname = Session["fullname"].ToString();
            }
            if (Session["email"] != null)
            {
                email = Session["email"].ToString();
            }
            if (Session["mobile"] != null)
            {
                mobile = Session["mobile"].ToString();
            }
            if (Session["total_amt"] != null)
            {
                total_amt = Session["total_amt"].ToString();
            }
            Email c_mail = new Email();
            Messenger messenger = new Messenger();
            string str = "Dear " + fullname + ",<br/>";
            string str2 = "Dear " + fullname + ",\r\n";
            if (Session["Refno"] != null)
            {
                str = ((((str + "Your payment transaction has been successfully completed!<br/>") + "Reason: " + isr.ResponseDescription + "<br />") + "Transaction Reference: " + Session["Refno"].ToString().ToUpper() + "<br/>") + "Payment Reference :" + payRef + "<br/>") + "Please view more details below!!<br/><br/>Regards<br/><br/><br/><br/><hr>";
                string str6 = str2;
                str2 = str6 + "Your payment transaction has been successfully completed\r\nReason: " + isr.ResponseDescription + "\r\nTransaction Reference: " + Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                html_msg = html_msg + str;
                html_msg = html_msg + "<html><head id='Head1' runat='server'><title>PAYMENT RECIEPT</title>";
                html_msg = html_msg + "  <link href=\"http://payx.com.ng/css/style.css\" rel=\"stylesheet\" type=\"text/css\" /> ";
                html_msg = html_msg + "<style type='text/css'>";
                html_msg = html_msg + ".item_alt {background-color:#E3EAEB; color:#000000;text-align:left;font-weight:normal;font-size:14px;} .tiger-stripe{ text-align:left;font-weight:normal;font-size:14px;}";
                html_msg = html_msg + ".tbbg{padding:0;margin:0 auto;width:100%;height:20px;background-color:#006699;text-align:center;color:#fff;font-weight:bold;border-color:#006699;}";
                html_msg = html_msg + "</style></head><body><form id='form1' runat='server'><div>";
                html_msg = html_msg + " <table class=\"form\"  style=\"font-size:16px;text-align:center;font-weight:normal;width:100%;border:1px solid #000000;\">";
                html_msg = html_msg + " <tr align=\"center\" style=\"text-align:center;\">";
                html_msg = html_msg + "<td  colspan=\"4\" style=\"text-align:center;\" align=\"center\"> <img alt=\"Coat of Arms\"  src=\"http://payx.com.ng/images/LOGOCLD.jpg\" width=\"458px\" height=\"76px\" /></td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + " <tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">PAYMENT RECIEPT FOR TRANSACTION :&nbsp;" + c_twall.ref_no + " </td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"><strong>TRANSACTION ID:</strong> " + txnref + "</td>";
                html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"> <strong>DATE:</strong>  " + xreg_date + "</td>";
                html_msg = html_msg + " </tr>";
                if (kkx != null)
                {
                    html_msg = html_msg + "<tr>";
                    html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"><strong>PRODUCT TITLE:</strong> " + kkx.product_title + "</td>";
                    html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"> <strong>FILE NUMBER:</strong>  " + kkx.reg_number + "</td>";
                    html_msg = html_msg + " </tr>";

                    html_msg = html_msg + "<tr>";
                    html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"><strong>CLASS:</strong> " + kkx.nice_class + "</td>";
                    html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"> </td>";
                    html_msg = html_msg + " </tr>";

                }
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"text-align:center;font-weight:bold;\">";
                html_msg = html_msg + "<td colspan=\"4\">PAYMENT REFERENCE:&nbsp;\"" + payRef + "\"</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:7%;\">NAME: </td>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:43%;\">" + c_app.xname + "</td>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:7%;\">NAME: </td>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:43%;\" >" + fullname + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr style=\"background-color:#E3EAEB;\">";
                html_msg = html_msg + "<td align=\"left\" >ADDRESS: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + c_app.address + "</td>";
                html_msg = html_msg + "<td align=\"left\" >CODE: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + cust_id + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"left\" >E-MAIL: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + c_app.xemail + "</td>";
                html_msg = html_msg + "<td align=\"left\" >E-MAIL: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + email + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr style=\"background-color:#E3EAEB;\">";
                html_msg = html_msg + "<td align=\"left\" >MOBILE: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + c_app.xmobile + "</td>";
                html_msg = html_msg + "<td align=\"left\" >MOBILE: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + mobile + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"center\" colspan=\"4\"  style=\"background-color:#666; color:#ffffff;font-weight:bold;\"><strong>--- PAYMENT DETAILS ---</strong></td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"left\" colspan=\"4\" style=\"font-size:12px;\">";
                html_msg = html_msg + "<table style=\"width:100%;\" id=\"mitems\" class=\"tiger-stripe\" >";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff;\">";
                html_msg = html_msg + "<td><strong>S/N</strong></td>";
                html_msg = html_msg + "<td><strong>TRANSACTION ID</strong></td>";
                html_msg = html_msg + "<td><strong>ITEM CODE</strong></td>";
                html_msg = html_msg + "<td><strong>ITEM DESCRIPTION</strong></td>";
                html_msg = html_msg + "<td><strong>QTY</strong></td>";
                html_msg = html_msg + "<td style=\"text-align:center;\">APPLICATION FEE(NGN)</strong></td>";
                html_msg = html_msg + "<td style=\"text-align:center;\"><strong>TECH. FEE(NGN)</strong></td>";
                html_msg = html_msg + "<td style=\"text-align:center;\"><strong>TOTAL (NGN)</strong></td>";
                html_msg = html_msg + "</tr>";
                foreach (XObjs.PaymentReciept reciept in lt_pr)
                {
                    html_msg = html_msg + "<tr>";
                    html_msg = html_msg + "<td>" + reciept.sn + "</td>";
                    html_msg = html_msg + "<td>" + reciept.transID + "</td>";
                    html_msg = html_msg + "<td>" + reciept.item_code + "</td>";
                    html_msg = html_msg + "<td>" + reciept.item_desc + "</td>";
                    html_msg = html_msg + "<td>" + reciept.qty + "</td>";
                    html_msg = html_msg + "<td style=\"text-align:right;\">" + reciept.init_amt + "</td>";
                    html_msg = html_msg + "<td style=\"text-align:right;\">" + reciept.tech_amt + "</td>";
                    html_msg = html_msg + "<td style=\"text-align:right;\">" + reciept.tot_amount + "</td>";
                    html_msg = html_msg + " </tr>";
                }
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<td colspan=\"7\" style=\"text-align:right;font-weight:bold;\">";
                html_msg = html_msg + "PayX Convenience Fee:&nbsp;</td>";
                html_msg = string.Concat(new object[] { html_msg, "<td align=\"right\">&nbsp;", Math.Round(Convert.ToDouble(isw_fields.isw_conv_fee), 2), "</td>" });
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "</table>";
                html_msg = html_msg + "</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td colspan=\"4\" >&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"font-size:13px;text-decoration:underline; color:#1C5E55; font-weight:bold; text-align:right;\" align=\"right\" >";
                html_msg = html_msg + "<td colspan=\"4\" >TOTAL AMOUNT:&nbsp;NGN&nbsp;" + total_amt + "</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\"></td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"center\" colspan=\"4\">";
                html_msg = html_msg + "POWERED BY<br/>";
                html_msg = html_msg + "  <img alt=\"Pay X\" src=\"http://payx.com.ng/images/payxlogo.jpg\"   width=\"90px\" height=\"40px\"/>";
                html_msg = html_msg + "<img alt=\"interswitch\"  src=\"http://payx.com.ng/images/isw_logo_small.gif\" /><br /> ";
                html_msg = html_msg + "Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />";
                html_msg = html_msg + "<a href=\"http://www.einaosolutions.com\">www.einaosolutions.com</a><br />";
                html_msg = html_msg + "Support E-mail(s): <a href=\"mailto:paymentsupport@einaosolutions.com\">paymentsupport@einaosolutions.com</a><br />";
                html_msg = html_msg + "Customer Contact Support Line(s): +2349038979681  ";
                html_msg = html_msg + "</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + " </table>";
                html_msg = html_msg + "</table></div></form></body></html>";
            }
            string subject = "PAYX ALERT";
            string from = "paymentsupport@einaosolutions.com";
            string to = email;
            c_mail.sendMail("PAYX ALERT", to, from, subject, html_msg, "");
            c_mail.sendMail("PAYX ALERT", c_app.xemail, from, subject, html_msg, "");
        }

        protected void sendUnsuccAlertHtml()
        {
            if (Session["fullname"] != null)
            {
                fullname = Session["fullname"].ToString();
            }
            if (Session["email"] != null)
            {
                email = Session["email"].ToString();
            }
            if (Session["mobile"] != null)
            {
                mobile = Session["mobile"].ToString();
            }
            if (Session["total_amt"] != null)
            {
                total_amt = Session["total_amt"].ToString();
            }
            Email c_mail = new Email();
            Messenger messenger = new Messenger();
            string str = "Dear " + fullname + ",<br/>";
            string str2 = "Dear " + fullname + ",\r\n";
            if (Session["Refno"] != null)
            {
                str = ((((str + "Your payment transaction was not successfully completed!<br/>") + "Reason: " + isr.ResponseDescription + "<br />") + "Transaction Reference: " + Session["Refno"].ToString().ToUpper() + "<br/>") + "Payment Reference :" + payRef + "<br/>") + "Please view more details below!!<br/><br/>Regards<br/><br/><br/><br/><hr>";
                string str6 = str2;
                str2 = str6 + "Your payment transaction was not successfully completed\r\nReason: " + isr.ResponseDescription + "\r\nTransaction Reference: " + Session["Refno"].ToString().ToUpper() + " \r\nPayment Reference :" + payRef + "\r\nPlease check your 'Payment Status' or 'History Log' to view more details\r\nRegards";
                html_msg = html_msg + str;
                html_msg = html_msg + "<html><head id='Head1' runat='server'><title>PAYMENT INVOICE SLIP</title>";
                html_msg = html_msg + "  <link href=\"http://payx.com.ng/css/style.css\" rel=\"stylesheet\" type=\"text/css\" /> ";
                html_msg = html_msg + "<style type='text/css'>";
                html_msg = html_msg + ".item_alt {background-color:#E3EAEB; color:#000000;text-align:left;font-weight:normal;font-size:14px;} .tiger-stripe{ text-align:left;font-weight:normal;font-size:14px;}";
                html_msg = html_msg + ".tbbg{padding:0;margin:0 auto;width:100%;height:20px;background-color:#006699;text-align:center;color:#fff;font-weight:bold;border-color:#006699;}";
                html_msg = html_msg + "</style></head><body><form id='form1' runat='server'><div>";
                html_msg = html_msg + " <table class=\"form left-align\"  style=\"font-size:16px;text-align:center;font-weight:normal;width:100%;border:1px solid #000000;\">";
                html_msg = html_msg + " <tr align=\"center\" style=\"text-align:center;\">";
                html_msg = html_msg + "<td  colspan=\"4\" style=\"text-align:center;\" align=\"center\"> <img alt=\"Coat of Arms\"  src=\"http://payx.com.ng/images/LOGOCLD.jpg\" width=\"458px\" height=\"76px\" /></td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + " <tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">PAYMENT INVOICE SLIP FOR TRANSACTION :&nbsp;" + c_twall.ref_no + " </td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"><strong>TRANSACTION ID:</strong> " + txnref + "</td>";
                html_msg = html_msg + "<td colspan=\"2\" align=\"center\" style=\"width:50%;\"> <strong>DATE:</strong>  " + xreg_date + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"text-align:center;font-weight:bold;\">";
                html_msg = html_msg + "<td colspan=\"4\">PAYMENT REFERENCE:&nbsp;\"" + payRef + "\"</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\">&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:7%;\">NAME: </td>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:43%;\">" + c_app.xname + "</td>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:7%;\">NAME: </td>";
                html_msg = html_msg + "<td align=\"left\" style=\"width:43%;\" >" + fullname + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr style=\"background-color:#E3EAEB;\">";
                html_msg = html_msg + "<td align=\"left\" >ADDRESS: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + c_app.address + "</td>";
                html_msg = html_msg + "<td align=\"left\" >CODE: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + cust_id + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"left\" >E-MAIL: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + c_app.xemail + "</td>";
                html_msg = html_msg + "<td align=\"left\" >E-MAIL: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + email + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr style=\"background-color:#E3EAEB;\">";
                html_msg = html_msg + "<td align=\"left\" >MOBILE: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + c_app.xmobile + "</td>";
                html_msg = html_msg + "<td align=\"left\" >MOBILE: </td>";
                html_msg = html_msg + "<td align=\"left\" >" + mobile + "</td>";
                html_msg = html_msg + " </tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"center\" colspan=\"4\"  style=\"background-color:#666; color:#ffffff;font-weight:bold;\"><strong>--- PAYMENT DETAILS ---</strong></td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"left\" colspan=\"4\" style=\"font-size:12px;\">";
                html_msg = html_msg + "<table style=\"width:100%;\" id=\"mitems\" class=\"tiger-stripe\" >";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff;\">";
                html_msg = html_msg + "<td><strong>S/N</strong></td>";
                html_msg = html_msg + "<td><strong>TRANSACTION ID</strong></td>";
                html_msg = html_msg + "<td><strong>ITEM CODE</strong></td>";
                html_msg = html_msg + "<td><strong>ITEM DESCRIPTION</strong></td>";
                html_msg = html_msg + "<td><strong>QTY</strong></td>";
                html_msg = html_msg + "<td style=\"text-align:center;\">APPLICATION FEE(NGN)</strong></td>";
                html_msg = html_msg + "<td style=\"text-align:center;\"><strong>TECH. FEE(NGN)</strong></td>";
                html_msg = html_msg + "<td style=\"text-align:center;\"><strong>TOTAL (NGN)</strong></td>";
                html_msg = html_msg + "</tr>";
                foreach (XObjs.PaymentReciept reciept in lt_pr)
                {
                    html_msg = html_msg + "<tr>";
                    html_msg = html_msg + "<td>" + reciept.sn + "</td>";
                    html_msg = html_msg + "<td>" + reciept.transID + "</td>";
                    html_msg = html_msg + "<td>" + reciept.item_code + "</td>";
                    html_msg = html_msg + "<td>" + reciept.item_desc + "</td>";
                    html_msg = html_msg + "<td>" + reciept.qty + "</td>";
                    html_msg = html_msg + "<td style=\"text-align:right;\">" + reciept.init_amt + "</td>";
                    html_msg = html_msg + "<td style=\"text-align:right;\">" + reciept.tech_amt + "</td>";
                    html_msg = html_msg + "<td style=\"text-align:right;\">" + reciept.tot_amount + "</td>";
                    html_msg = html_msg + " </tr>";
                }
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<td colspan=\"7\" style=\"text-align:right;font-weight:bold;\">";
                html_msg = html_msg + "PayX Convenience Fee:&nbsp;</td>";
                html_msg = string.Concat(new object[] { html_msg, "<td align=\"right\">&nbsp;", Math.Round(Convert.ToDouble(isw_fields.isw_conv_fee), 2), "</td>" });
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "</table>";
                html_msg = html_msg + "</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td colspan=\"4\" >&nbsp;</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"font-size:13px;text-decoration:underline; color:#1C5E55; font-weight:bold; text-align:right;\" align=\"right\" >";
                html_msg = html_msg + "<td colspan=\"4\" >TOTAL AMOUNT:&nbsp;NGN&nbsp;" + total_amt + "</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr style=\"background-color:#1C5E55; color:#ffffff; text-align:center;\">";
                html_msg = html_msg + "<td colspan=\"4\"></td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + "<tr>";
                html_msg = html_msg + "<td align=\"center\" colspan=\"4\">";
                html_msg = html_msg + "POWERED BY<br/>";
                html_msg = html_msg + "  <img alt=\"Pay X\" src=\"http://payx.com.ng/images/payxlogo.jpg\"   width=\"90px\" height=\"40px\"/>";
                html_msg = html_msg + "<img alt=\"interswitch\"  src=\"http://payx.com.ng/images/isw_logo_small.gif\" /><br /> ";
                html_msg = html_msg + "Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />";
                html_msg = html_msg + "<a href=\"http://www.einaosolutions.com\">www.einaosolutions.com</a><br />";
                html_msg = html_msg + "Support E-mail(s): <a href=\"mailto:paymentsupport@einaosolutions.com\">paymentsupport@einaosolutions.com</a><br />";
                html_msg = html_msg + "Customer Contact Support Line(s): +2349038979681  ";
                html_msg = html_msg + "</td>";
                html_msg = html_msg + "</tr>";
                html_msg = html_msg + " </table>";
                html_msg = html_msg + "</table></div></form></body></html>";
            }
            string subject = "PAYX ALERT";
            string from = "paymentsupport@einaosolutions.com";
            string to = email;
            c_mail.sendMail("PAYX ALERT", to, from, subject, html_msg, "");
            c_mail.sendMail("PAYX ALERT", c_app.xemail, from, subject, html_msg, "");
        }
    }
}

