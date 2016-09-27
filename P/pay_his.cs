namespace XPay.P
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;
    using XPay.InterSwitch.PayDirect.Classes;

    public partial class pay_his : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        public double amt = 0.0;
        protected AppStatus c_as = new AppStatus();
        protected XObjs.AgentInfo c_ai = new XObjs.AgentInfo();
        protected Button btnBack;
        protected Button btnExportExcel;
        protected Button btnSearch;
        protected Button btnSearchTransaction;
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
        protected DropDownList ddl_cat;
        protected DropDownList ddl_mode;
        protected DropDownList ddl_status;
        public string docpath = "";
        public ExcelFuncs ef = new ExcelFuncs();
        protected string email = "";
        protected HtmlForm form1;
        protected string from_dt = "0000-01-01";
        protected TextBox fromDate;
        protected string fullname = "";
        protected int grand_tot_amt = 0;
        protected int grand_tot_cnt = 0;
        protected GridView gvTm;
        protected HtmlHead Head1;
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
        protected TextBox toDate;
        protected int tot_amtx = 0;
        protected string transID = "";
        protected XObjs.Twallet twall = new XObjs.Twallet();
        protected Transactions tx = new Transactions();
        protected TextBox txt_trans;
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
                this.doShowLatest();
            }
            if (this.Session["grand_tot_cnt"] == null)
            {
                this.Session["grand_tot_cnt"] = "0";
            }
            if (this.Session["new_grand_tot_amt"] == null)
            {
                this.Session["new_grand_tot_amt"] = "0";
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

        protected void doShowLatest()
        {
            if ((Session["merchant_type"] != null) && (Session["merchant_type"].ToString() != ""))
            {
                merchant_type = Session["merchant_type"].ToString();
            }
            Session["new_grand_tot_amt"] = "0";
            lt_ri = ret.getPaymentReportItemISW(ddl_cat.SelectedValue, ddl_status.SelectedValue, to_dt.Trim(), to_dt.Trim(), merchant_type);
            Session["grand_tot_cnt"] = lt_ri.Count;
            if (lt_ri.Count > 0)
            {
                toDate.Text = to_dt.Trim(); fromDate.Text = to_dt.Trim();
                tm_cnt = lt_ri.Count;
                Session["tm_cnt"] = tm_cnt;
                foreach (XObjs.ReportItem item in lt_ri)
                {
                    item.isw_amt = Math.Round(Convert.ToDouble(ret.getISWtransactionByTransactionID(item.transID).isw_conv_fee), 2).ToString();
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
                    if (Session["XPayMemberType"] != null)
                    {
                        if (Session["XPayMemberType"].ToString() == "merchant")
                        {
                            gvTm.Columns[6].Visible = false;
                            gvTm.Columns[7].Visible = false;
                            item.total_amt = item.init_amt;
                            grand_tot_amt += Convert.ToInt32(item.total_amt);
                            item.office_status = status;
                            item.data_status = data_status;
                            item.payment_mode = ddl_mode.SelectedItem.Text;
                            item.payment_status = ddl_status.SelectedItem.Text;
                            item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                        }
                        else
                        {
                            gvTm.Columns[6].Visible = true;
                            gvTm.Columns[7].Visible = true;
                            item.total_amt = Convert.ToString((int)(Convert.ToInt32(item.init_amt) + Convert.ToInt32(item.tech_amt)));
                            grand_tot_amt += Convert.ToInt32(item.total_amt);
                            item.office_status = status;
                            item.data_status = data_status;
                            item.payment_mode = ddl_mode.SelectedItem.Text;
                            item.payment_status = ddl_status.SelectedItem.Text;
                            item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                            item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                            item.total_amt = string.Format("{0:n}", Convert.ToInt32(item.total_amt));
                        }
                    }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if ((Session["merchant_type"] != null) && (Session["merchant_type"].ToString() != ""))
            {
                merchant_type = Session["merchant_type"].ToString();
            }
            Session["new_grand_tot_amt"] = "0";
            if (fromDate.Text == "")
            {
                fromDate.Text = from_dt;
            }
            if (toDate.Text == "")
            {
                toDate.Text = to_dt;
            }
            lt_ri = ret.getPaymentReportItemISW(ddl_cat.SelectedValue, ddl_status.SelectedValue, fromDate.Text.Trim(), toDate.Text.Trim(), merchant_type);


            Session["grand_tot_cnt"] = lt_ri.Count;
            if (lt_ri.Count > 0)
            {
                tm_cnt = lt_ri.Count;
                Session["tm_cnt"] = tm_cnt;
                foreach (XObjs.ReportItem item in lt_ri)
                {
                    item.isw_amt = Convert.ToString(Math.Round(Convert.ToDecimal(item.isw_amt), 2));
                    item.full_amt = Convert.ToString(Convert.ToDouble(item.full_amt));
                    item.full_amt = string.Format("{0:n}", Convert.ToDouble(item.full_amt));
                }
                Session["lt_ri"] = lt_ri;
                gvTm.DataSource = lt_ri;
                gvTm.DataBind();
                new_grand_tot_amt = ret.getPaymentReportItemGenISWSum(ddl_status.SelectedValue, fromDate.Text.Trim(), toDate.Text.Trim(), merchant_type);
                Session["new_grand_tot_amt"] = new_grand_tot_amt;

                String new_grand_tot_amt2 = ret.getPaymentReportItemGenISWSum2(ddl_status.SelectedValue, fromDate.Text.Trim(), toDate.Text.Trim(), merchant_type);
                Session["new_grand_tot_amt2"] = new_grand_tot_amt2;
                show_details_grid = 0;
                show_details_grid_wingman = 0;
                show_inv = 0;
            }

            else
            {
                show_inv = 1;
            }

            //if (lt_ri.Count > 0)
            //{

            //    tm_cnt = lt_ri.Count;
            //    Session["tm_cnt"] = tm_cnt;
            //    foreach (XObjs.ReportItem item in lt_ri)
            //    {
            //        item.isw_amt = Math.Round(Convert.ToDouble(ret.getISWtransactionByTransactionID(item.transID).isw_conv_fee), 2).ToString();
            //        if (item.item_code.Contains("T"))
            //        {
            //            if (item.item_code == "T002")
            //            {
            //                lt_pw = t.getStageByClientIDAcc(item.newtransID);
            //                if (lt_pw.Count > 0)
            //                {
            //                    SortedList<string, string> x = c_as.showTmStatus(lt_pw[0].status, lt_pw[0].data_status);
            //                    status = x["status"];
            //                    data_status = x["data_status"];
            //                }
            //            }
            //            else
            //            {
            //                lt_pw = t.getTmGenStageByClientIDAcc(item.newtransID);
            //                if (lt_pw.Count > 0)
            //                {
            //                    SortedList<string, string> x = c_as.showTmStatus(lt_pw[0].status, lt_pw[0].data_status);
            //                    status = x["status"];
            //                    data_status = x["data_status"];
            //                }
            //            }
            //        }
            //        else if (item.item_code.Contains("P"))
            //        {
            //            if ((item.item_code == "P001") || (item.item_code == "P002"))
            //            {
            //                lt_pw = t.getPtStageByClientIDAcc(item.newtransID);
            //                if (lt_pw.Count > 0)
            //                {
            //                    SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
            //                    status = x["status"];
            //                    data_status = x["data_status"];
            //                }
            //            }
            //            else if (item.item_code == "P003")
            //            {
            //                lt_pw = t.getPtRenStageByClientIDAcc(item.newtransID);
            //                if (lt_pw.Count > 0)
            //                {
            //                    SortedList<string, string> x = c_as.showPtStatus(lt_pw[0].status, lt_pw[0].data_status);
            //                    status = x["status"];
            //                    data_status = x["data_status"];
            //                }
            //            }
            //        }
                   
            //        if (Session["XPayMemberType"] != null)
            //        {
                       
            //            if (Session["XPayMemberType"].ToString() == "merchant")
            //            {
            //                gvTm.Columns[6].Visible = false;
            //                gvTm.Columns[7].Visible = false;
            //                item.total_amt = item.init_amt;
            //                grand_tot_amt += Convert.ToInt32(item.total_amt);
            //                item.office_status = status;
            //                item.data_status = data_status;
            //                item.payment_mode = ddl_mode.SelectedItem.Text;
            //                item.payment_status = ddl_status.SelectedItem.Text;
            //                item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
            //            }
            //            else
            //            {
            //                gvTm.Columns[6].Visible = true;
            //                gvTm.Columns[7].Visible = true;
            //                item.total_amt = Convert.ToString((int)(Convert.ToInt32(item.init_amt) + Convert.ToInt32(item.tech_amt)));
            //                grand_tot_amt += Convert.ToInt32(item.total_amt);
            //                item.office_status = status;
            //                item.data_status = data_status;
            //                item.payment_mode = ddl_mode.SelectedItem.Text;
            //                item.payment_status = ddl_status.SelectedItem.Text;
            //                item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
            //                item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
            //                item.total_amt = string.Format("{0:n}", Convert.ToInt32(item.total_amt));
            //            }
            //        }
            //    }
            //    Session["lt_ri"] = lt_ri;
            //    gvTm.DataSource = lt_ri;
            //    gvTm.DataBind();
            //    new_grand_tot_amt = string.Format("{0:n}", grand_tot_amt);
            //    Session["new_grand_tot_amt"] = new_grand_tot_amt;
            //    show_details_grid = 0;
            //    show_details_grid_wingman = 0;
            //    show_inv = 0;
            //}
            //else
            //{
            //    show_inv = 1;
            //}
        }

        protected void btnSearchTransaction_Click(object sender, EventArgs e)
        {
          
            if ((Session["merchant_type"] != null) && (Session["merchant_type"].ToString() != ""))
            {
                merchant_type = Session["merchant_type"].ToString();
            }
            Session["new_grand_tot_amt"] = "0";
            if (fromDate.Text == "")
            {
                fromDate.Text = from_dt;
            }
            if (toDate.Text == "")
            {
                toDate.Text = to_dt;
            }
            string transID = ""; string[] arr_tnx = null;
            if (txt_trans.Text.Contains("-"))
            {
                arr_tnx=txt_trans.Text.Trim().Split('-');
                transID = arr_tnx[0];
            }
            if(arr_tnx.Length==3)
            { 
            lt_ri = ret.getPaymentReportItemByTransID(arr_tnx[0],arr_tnx[1],arr_tnx[2], merchant_type);
            Session["grand_tot_cnt"] = lt_ri.Count;
            if (lt_ri.Count > 0)
            {
                lt_ri[0].isw_amt = Math.Round(Convert.ToDouble(ret.getISWtransactionByTransactionID(transID).isw_conv_fee), 2).ToString();
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

                    if (Session["XPayMemberType"] != null)
                    {
                        if (Session["XPayMemberType"].ToString() == "merchant")
                        {
                            gvTm.Columns[6].Visible = false;
                            gvTm.Columns[7].Visible = false;
                            item.total_amt = item.init_amt;
                            grand_tot_amt += Convert.ToInt32(item.total_amt);
                            item.office_status = status;
                            item.data_status = data_status;
                            item.payment_mode = ddl_mode.SelectedItem.Text;
                            item.payment_status = ddl_status.SelectedItem.Text;
                            item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                        }
                        else
                        {
                            gvTm.Columns[6].Visible = true;
                            gvTm.Columns[7].Visible = true;
                            item.total_amt = Convert.ToString((int)(Convert.ToInt32(item.init_amt) + Convert.ToInt32(item.tech_amt)));
                            grand_tot_amt += Convert.ToInt32(item.total_amt);
                            item.office_status = status;
                            item.data_status = data_status;
                            item.payment_mode = ddl_mode.SelectedItem.Text;
                            item.payment_status = ddl_status.SelectedItem.Text;
                            item.init_amt = string.Format("{0:n}", Convert.ToInt32(item.init_amt));
                            item.tech_amt = string.Format("{0:n}", Convert.ToInt32(item.tech_amt));
                            item.total_amt = string.Format("{0:n}", Convert.ToInt32(item.total_amt));
                        }
                    }
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
            else
            {
                //The transaction ID format is wrong
            }
        }

        protected void gvTm_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
       
            //
            gvTm.PageIndex = e.NewPageIndex;
            if (Session["lt_ri"] != null)
            {
                lt_ri.Clear();
                lt_ri = (List<XObjs.ReportItem>)Session["lt_ri"];
            }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            if (Session["tm_cnt"] != null)
            {
                tm_cnt = Convert.ToInt32(Session["tm_cnt"]);
            }
            show_inv = 0;
            show_details_grid = 0;
            show_details_grid_wingman = 0;
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmDetailsClick")
            {
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string transID = e.CommandArgument.ToString();
                Session["transID"] = transID;
                twall = ret.getTwalletByTransID(transID);
               
                if (twall.xid != null)
                {
                    isw_fields = ret.getISWtransactionByTransactionID(transID);
                    isw_fields.TransactionDate = isw_fields.TransactionDate.Substring(0, 11).Trim();
                    string xgt = twall.xgt;
                    lt_fdets = ret.getFee_detailsByTwalletID(twall.xid);
                    c_app = ret.getApplicantByID(twall.applicantID);
                    Session["c_app"] = c_app;
                    Session["AgentType"] = twall.xmembertype;
                    Session["transID"] = transID;
                    Session["memberID"] = adminID;
                    Session["transDate"] = twall.xreg_date;
                    if (twall.xmembertype == "Agent")
                    {
                        c_reg = ret.getRegistrationByID(twall.xmemberID);
                        fullname = c_reg.Firstname + " " + c_reg.Surname;
                        coy_name = c_reg.CompanyName;
                        cust_id = c_reg.Sys_ID;
                        email = c_reg.Email;
                        mobile = c_reg.PhoneNumber;
                        Session["coy_name"] = coy_name;
                        Session["fullname"] = fullname;
                        Session["email"] = email;
                        Session["mobile"] = mobile;
                        Session["c_addy"] = c_reg.CompanyAddress;
                    }
                    else
                    {
                        c_sub = ret.getSubAgentByID(twall.xmemberID);
                        fullname = c_sub.Firstname + " " + c_sub.Surname;
                        email = c_sub.Email;
                        mobile = c_sub.Telephone;
                        if (c_sub.xid != null)
                        {
                            c_sub_reg = ret.getRegistrationByID(c_sub.RegistrationID);
                            coy_name = c_sub_reg.CompanyName;
                            cust_id = c_sub_reg.Sys_ID + "_" + c_sub.AssignID;
                        }
                    }
                    c_ai.code = cust_id;
                    c_ai.xname = fullname;
                    c_ai.xemail = email;
                    c_ai.xmobile = mobile;

                    Session["c_ai"] = c_ai;

                    if (xgt == "xpay_bk")
                    {
                        Session["payment_type"] = "Bank";
                        c_banker = ret.getBankerByID(ret.getPwalletByID(twall.xbankerID).xmemberID);
                        Session["bank_xname"] = c_banker.xname;
                        Session["bank_bankname"] = c_banker.bankname;
                        Session["bank_xposition"] = c_banker.xposition;
                        Session["bank_street"] = ret.getAddressByID(c_banker.addressID).street;
                        Session["bank_telephone"] = ret.getAddressByID(c_banker.addressID).telephone1;
                        Session["bank_email"] = ret.getAddressByID(c_banker.addressID).email1;
                    }
                    else if (xgt == "xpay_isw")
                    {
                        Session["payment_type"] = "Online (Inter Switch)";
                    }
                    else
                    {
                        Session["payment_type"] = "Online";
                    }
                    show_inv = 1;
                    if (Session["tm_cnt"] != null)
                    {
                        tm_cnt = Convert.ToInt32(Session["tm_cnt"]);
                    }
                    show_details_grid = 1;
                }
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if ((Session["tm_cnt"] != null) && (Session["tm_cnt"].ToString() != ""))
            {
                tm_cnt = Convert.ToInt32(Session["tm_cnt"]);
            }
            if ((Session["lt_ri"] != null) && (Session["lt_ri"].ToString() != ""))
            {
                lt_ri = (List<XObjs.ReportItem>)Session["lt_ri"];
            }
            gvTm.DataSource = lt_ri;
            gvTm.DataBind();
            show_inv = 0;
            if (Session["XPayMemberType"] != null)
            {
                if (Session["XPayMemberType"].ToString() == "merchant")
                {
                    docpath = docpath + "Merchant-" + adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";
                }
                else if (Session["XPayMemberType"].ToString() == "wingman")
                {
                    docpath = docpath + "Partner-" + adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";
                }
                else
                {
                    docpath = docpath + "Admin-" + adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";
                }
            }
            //if (Session["c_app"] != null) { c_app = (XObjs.Applicant)Session["c_app"]; }
            //if (Session["c_ai"] != null) { c_ai = (XObjs.AgentInfo)Session["c_ai"]; }

            ef.CreateReportExcel(this, lt_ri, docpath, ddl_mode.SelectedItem.Text + " Payment Report");
        }
    }
}

