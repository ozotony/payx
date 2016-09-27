using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XPay.Classes;
using XPay.InterSwitch.PayDirect.Classes;

namespace PayX.X
{
    public partial class charge_back : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        public double amt = 0.0;
        protected AppStatus c_as = new AppStatus();
        protected XObjs.AgentInfo c_ai = new XObjs.AgentInfo();      
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.XBanker c_banker = new XObjs.XBanker();
        protected XObjs.Fee_details c_fdets = new XObjs.Fee_details();
        protected XObjs.XPartner c_partner = new XObjs.XPartner();
        protected XObjs.PRatio c_pr = new XObjs.PRatio();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string check_trans_page = "";
        protected string coy_name = "";
        protected string cust_id = "";
        public string data_status = "N/A";
        public string docpath = "";
        protected string email = "";
        protected string from_dt = "0000-01-01";
        protected string fullname = "";
        protected int grand_tot_amt = 0;
        protected int grand_tot_cnt = 0;
        protected XObjs.InterSwitchResponse isr = new XObjs.InterSwitchResponse();
        protected XObjs.InterSwitchPostFields isw_fields = new XObjs.InterSwitchPostFields();
        protected List<XObjs.Fee_details> lt_fdets = new List<XObjs.Fee_details>();
        protected List<XObjs.PartnerGrid> lt_pg = new List<XObjs.PartnerGrid>();
        protected List<tm.Stage> lt_pw = new List<tm.Stage>();
        public List<XObjs.ReportItem> lt_ri = new List<XObjs.ReportItem>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string merchant_type = "";
        protected string mobile = "";
        protected string new_grand_tot_amt = "";
        protected string pr = "";
        protected string ref_no = "";
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected string search_msg = "";
        protected int show_banker_grid = 0;
        protected int show_details_grid = 0;
        protected int show_details_grid_wingman = 0;
        protected int show_inv = 0;
        public string status = "N/A";
        protected tm t = new tm();
        protected int tm_cnt = 0;
        protected string to_dt = DateTime.Now.ToString("yyyy-MM-dd");
        protected int tot_amtx = 0;
        protected string transID = "";
        protected XObjs.Twallet twall = new XObjs.Twallet();
        protected Transactions tx = new Transactions();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected int xtotal_amt = 0;

        protected void btnBack_Click(object sender, EventArgs e)
        {
            this.show_inv = 0;
            this.show_details_grid = 0;
            this.show_details_grid_wingman = 0;
            if (this.Session["tm_cnt"] != null)
            {
                this.tm_cnt = Convert.ToInt32(this.Session["tm_cnt"]);
            }
        }
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
            if (!base.IsPostBack)
            {
                this.Session["XPayMemberType"] = null;
                this.Session["grand_tot_cnt"] = null;
                this.Session["new_grand_tot_amt"] = null;
                this.Session["transID"] = null;
                this.Session["payment_type"] = null;
                this.Session["bank_xname"] = null;
                this.Session["bank_bankname"] = null;
                this.Session["bank_xposition"] = null;
                this.Session["bank_street"] = null;
                this.Session["bank_telephone"] = null;
                this.Session["bank_email"] = null;
                this.Session["transDate"] = null;
                this.c_pr = this.ret.getPratioByMemberID(this.adminID);
                if (this.c_pr.xid != null)
                {
                    this.Session["XPayMemberType"] = this.c_pr.p_type;
                    this.Session["merchant_type"] = this.c_pr.xsync;
                }
                else
                {
                    this.Session["XPayMemberType"] = "admin";
                    this.Session["merchant_type"] = "";
                }
            }
           
            if (this.Session["transID"] == null)
            {
                this.Session["transID"] = "0";
            }
            if (this.Session["tm_cnt"] != null)
            {
                this.tm_cnt = Convert.ToInt32(this.Session["tm_cnt"]);
            }
            else
            {
                this.Session["tm_cnt"] = "0";
            }
            if (this.Session["payment_type"] == null)
            {
                this.Session["payment_type"] = "";
            }
            if (this.Session["bank_xname"] == null)
            {
                this.Session["bank_xname"] = "";
            }
            if (this.Session["bank_bankname"] == null)
            {
                this.Session["bank_bankname"] = "";
            }
            if (this.Session["bank_xposition"] == null)
            {
                this.Session["bank_xposition"] = "";
            }
            if (this.Session["bank_street"] == null)
            {
                this.Session["bank_street"] = "";
            }
            if (this.Session["bank_telephone"] == null)
            {
                this.Session["bank_telephone"] = "";
            }
            if (this.Session["bank_email"] == null)
            {
                this.Session["bank_email"] = "";
            }
            if (this.Session["transDate"] == null)
            {
                this.Session["bank_email"] = "";
            }
        }

       
        protected void btnSearchTransaction_Click(object sender, EventArgs e)
        {
            if ((Session["merchant_type"] != null) && (Session["merchant_type"].ToString() != ""))
            {
                merchant_type = Session["merchant_type"].ToString();
            }

            string MerchantReference = "";
            
                MerchantReference = txt_trans.Text.Trim();

                lt_ri = ret.getChargeBackReportItemByMerchantRefD(MerchantReference);

            Session["grand_tot_cnt"] = lt_ri.Count;
            if (lt_ri.Count > 0)
            {
                lt_ri[0].isw_amt = Math.Round(Convert.ToDouble(lt_ri[0].isw_amt), 2).ToString();
                tm_cnt = lt_ri.Count;
                Session["tm_cnt"] = tm_cnt;
                foreach (XObjs.ReportItem item in lt_ri)
                {
                    if (item.item_code.Contains("T"))
                    {
                        lt_pw = t.getStageByClientIDAcc(item.newtransID);
                        if (lt_pw.Count > 0)
                        {
                            SortedList<string, string> x = c_as.showTmStatus(lt_pw[0].status, lt_pw[0].data_status);
                            status = x["status"];
                            data_status = x["data_status"];
                        }
                    }
                    else if (item.item_code.Contains("P"))
                    {
                        lt_pw = t.getPtStageByClientIDAcc(item.newtransID);
                        if (lt_pw.Count > 0)
                        {
                            SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
                            status = x["status"];
                            data_status = x["data_status"];
                        }
                    }
                    //else if (item.item_code.Contains("D"))
                    //{
                    //    lt_pw = t.getDsStageByClientIDAcc(item.newtransID);
                    //    if (lt_pw.Count > 0)
                    //    {
                    //if (lt_pw.Count > 0)
                    //{
                    //    SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
                    //    status = x["status"];
                    //    data_status = x["data_status"];
                    //}
                    //    }
                    //}
                    item.total_amt = Convert.ToString((int)(Convert.ToDouble(item.init_amt) + Convert.ToDouble(item.isw_amt)));
                    grand_tot_amt += Convert.ToInt32(item.total_amt);
                    item.office_status = status +" Office";
                    item.data_status = "";
                    item.payment_status = "";
                    item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                    item.isw_amt = string.Format("{0:n}", Convert.ToDouble(item.isw_amt));
                    item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                    item.total_amt = string.Format("{0:n}", Convert.ToInt32(item.total_amt) + Convert.ToDouble(item.isw_amt));

                    isw_fields = ret.getISWtransactionByTransactionID(item.transID);
                    twall = ret.getTwalletByTransID(item.transID);
                    isw_fields.TransactionDate = isw_fields.TransactionDate.Substring(0, 11).Trim();
                    string xgt = twall.xgt;
                    c_app = ret.getApplicantByID(twall.applicantID);
                    
                }
                Session["lt_ri"] = lt_ri;
                gvTm.DataSource = lt_ri;
                gvTm.DataBind();
                new_grand_tot_amt = string.Format("{0:n}", grand_tot_amt);
                Session["new_grand_tot_amt"] = new_grand_tot_amt;
                show_details_grid = 0;
                show_details_grid_wingman = 0;
                show_inv = 0;
            }
            else
            {
                show_inv = 1;
            }
        }

       
     
    }
}