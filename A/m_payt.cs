namespace XPay.A
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class m_payt : Page
    {
        protected string adminID = "0";
        protected string agentType = "";
        protected Button btnApplicant;
        protected Button btnApplicant0;
        protected Button btnChangeItems;
        protected Button btnConfirm;
        protected Button BtnDashboard;
        protected Button BtnDashboard0;
        protected Button BtnDashboard1;
        protected Button btnGo;
        protected Button btnProceedToPurchase;
        protected XObjs.Applicant c_app = new XObjs.Applicant();
        protected XObjs.Hwallet c_hwall = new XObjs.Hwallet();
        protected XObjs.Registration c_reg = new XObjs.Registration();
        protected XObjs.Subagent c_sub = new XObjs.Subagent();
        protected XObjs.Registration c_sub_reg = new XObjs.Registration();
        protected string email = "";
        protected XObjs.Fee_details f_dets = new XObjs.Fee_details();
        protected SqlDataSource flAg;
        protected SqlDataSource flDs;
        protected SqlDataSource flPt;
        protected SqlDataSource flTm;
        protected HtmlForm form1;
        protected string fullname = "";
        protected GridView gvAg;
        protected GridView gvDs;
        protected GridView gvPt;
        protected GridView gvTm;
        protected Label Label1;
        protected Label Label10;
        protected Label Label11;
        protected Label Label13;
        protected Label Label2;
        protected Label Label3;
        protected Label Label4;
        protected Label Label5;
        protected Label Label6;
        protected Label Label7;
        protected Label Label8;
        protected Label Label9;
        protected Label lblAgTotAmt;
        protected Label lblAgTotItems;
        protected Label lblAgTotQty;
        protected Label lblDsTotAmt;
        protected Label lblDsTotItems;
        protected Label lblDsTotQty;
        protected Label lblPtTotAmt;
        protected Label lblPtTotItems;
        protected Label lblPtTotQty;
        protected Label lblTmTotAmt;
        protected Label lblTmTotItems;
        protected Label lblTmTotQty;
        protected List<XObjs.Shopping_card> lt_cart = new List<XObjs.Shopping_card>();
        protected List<XObjs.Fee_list> lt_fl = new List<XObjs.Fee_list>();
        protected List<XObjs.Twallet> lt_twall = new List<XObjs.Twallet>();
        protected string mobile = "";
        protected string ref_no = "";
        private XPay.Classes.Registration reg = new XPay.Classes.Registration();
        private Retriever ret = new Retriever();
        protected XObjs.Scard scard = new XObjs.Scard();
        protected int show_inv = 0;
        protected SortedList<string, string> sl_init_amt = new SortedList<string, string>();
        protected SortedList<string, string> sl_tech_amt = new SortedList<string, string>();
        protected SortedList<string, XObjs.Shopping_card> st_items = new SortedList<string, XObjs.Shopping_card>();
        protected double tot_amtx = 0.0;
        protected string transID = "";
        protected XObjs.Twallet twall = new XObjs.Twallet();
        protected TextBox txt_app_addy;
        protected TextBox txt_app_email;
        protected TextBox txt_app_name;
        protected TextBox txt_app_no;
        private Validator val = new Validator();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected double xtot_amt = 0.0;
        protected double xtot_items = 0.0;
        protected int xtotal_amt = 0;

        protected void AddFeeList()
        {
            if (this.Session["c_app"] != null)
            {
                this.c_app = (XObjs.Applicant) this.Session["c_app"];
            }
            int num = this.reg.addApplicant(this.c_app);
            if ((num > 0) && ((this.Session["AgentType"] != null) && (this.Session["AgentType"].ToString() != "")))
            {
                this.agentType = this.Session["AgentType"].ToString();
                if (this.agentType == "Agent")
                {
                    if (this.Session["c_reg"] != null)
                    {
                        this.c_reg = (XObjs.Registration) this.Session["c_reg"];
                    }
                    this.fullname = this.c_reg.Firstname + " " + this.c_reg.Surname;
                    this.Session["fullname"] = this.fullname;
                    this.email = this.c_reg.Email;
                    this.Session["email"] = this.email;
                    this.mobile = this.c_reg.PhoneNumber;
                    this.Session["mobile"] = this.mobile;
                }
                else
                {
                    if (this.Session["c_sub"] != null)
                    {
                        this.c_sub = (XObjs.Subagent) this.Session["c_sub"];
                    }
                    this.fullname = this.c_sub.Firstname + " " + this.c_sub.Surname;
                    this.Session["fullname"] = this.fullname;
                    this.email = this.c_sub.Email;
                    this.Session["email"] = this.email;
                    this.mobile = this.c_sub.Telephone;
                    this.Session["mobile"] = this.mobile;
                }
                this.scard = this.ret.getRandomScard();
                this.lt_twall = this.ret.getTwalletByMemberID(this.adminID, this.scard.xnum, this.Session["AgentType"].ToString());
                if (this.lt_twall.Count == 0)
                {
                    this.transID = this.scard.xnum.ToUpper();
                    int num2 = 0;
                    int num3 = 0;
                    this.twall.ref_no = "X" + this.adminID + "-" + DateTime.Now.ToString("yyyy") + "-" + this.scard.xnum;
                    this.ref_no = this.twall.ref_no;
                    this.twall.transID = this.scard.xnum;
                    this.twall.xbankerID = "0";
                    this.twall.xgt = "xpay";
                    this.twall.xmemberID = this.adminID;
                    this.twall.xmembertype = this.Session["AgentType"].ToString();
                    this.twall.xpay_status = "2";
                    this.twall.applicantID = num.ToString();
                    this.twall.xreg_date = this.xreg_date;
                    this.twall.xsync = "0";
                    this.twall.xvisible = "1";
                    num2 = this.reg.addTwallet(this.twall);
                    if (num2 > 0)
                    {
                        if (this.Session["SCart"] != null)
                        {
                            this.lt_cart = (List<XObjs.Shopping_card>) this.Session["SCart"];
                        }
                        if (this.Session["Sl_init"] != null)
                        {
                            this.sl_init_amt = (SortedList<string, string>) this.Session["Sl_init"];
                        }
                        if (this.Session["Sl_tech"] != null)
                        {
                            this.sl_tech_amt = (SortedList<string, string>) this.Session["Sl_tech"];
                        }
                        int num4 = 0;
                        foreach (XObjs.Shopping_card _card in this.lt_cart)
                        {
                            this.f_dets.twalletID = num2.ToString();
                            this.f_dets.fee_listID = _card.xid;
                            this.f_dets.xlogstaff = this.adminID;
                            this.f_dets.xqty = _card.qty;
                            this.f_dets.xused = "0";
                            this.f_dets.tot_amt = _card.total_amt.ToString();
                            this.xtotal_amt += Convert.ToInt32(_card.total_amt);
                            this.f_dets.init_amt = this.sl_init_amt[_card.xid];
                            this.f_dets.tech_amt = this.sl_tech_amt[_card.xid];
                            this.f_dets.xreg_date = this.xreg_date;
                            this.f_dets.xsync = "0";
                            this.f_dets.xvisible = "1";
                            num3 = this.reg.addFee_details(this.f_dets);
                            num4++;
                            for (int i = 0; i < Convert.ToInt32(_card.qty); i++)
                            {
                                int num6 = 0;
                                this.c_hwall.transID = this.scard.xnum;
                                this.c_hwall.used_status = "Not Used";
                                this.c_hwall.product_title = "";
                                this.c_hwall.xreg_date = this.xreg_date;
                                this.c_hwall.used_date = "";
                                this.c_hwall.fee_detailsID = num3.ToString();
                                num6 = this.reg.addHwallet(this.c_hwall);
                            }
                        }
                        if (num4 == this.lt_cart.Count<XObjs.Shopping_card>())
                        {
                            base.Response.Redirect("./m_invoicet.aspx?mx=" + this.adminID + "&tx=" + this.scard.xnum);
                        }
                    }
                }
                else
                {
                    this.AddFeeList();
                }
            }
        }

        protected void btnApplicant_Click(object sender, EventArgs e)
        {
            this.show_inv = 0;
        }

        protected void btnChangeItems_Click(object sender, EventArgs e)
        {
            this.show_inv = 1;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (this.Session["SCart"] != null)
            {
                this.lt_cart = (List<XObjs.Shopping_card>) this.Session["SCart"];
            }
            this.show_inv = 2;
        }

        protected void BtnDashboard_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            this.AddFeeList();
        }

        protected void btnProceedToPurchase_Click(object sender, EventArgs e)
        {
            this.Session["c_app"] = null;
            this.c_app.address = this.txt_app_addy.Text;
            this.c_app.xemail = this.txt_app_email.Text;
            this.c_app.xmobile = this.txt_app_no.Text;
            this.c_app.xname = this.txt_app_name.Text;
            this.Session["c_app"] = this.c_app;
            if ((((this.txt_app_addy.Text != "") && (this.txt_app_email.Text != "")) && (this.txt_app_no.Text != "")) && (this.txt_app_name.Text != ""))
            {
                this.show_inv = 1;
            }
            else
            {
                this.show_inv = 0;
            }
        }

        protected double calcTotalAmt(int qty, int amt)
        {
            return (double) (qty * amt);
        }

        protected void displayTotals()
        {
            if (this.Session["SCart"] != null)
            {
                this.lt_cart = (List<XObjs.Shopping_card>) this.Session["SCart"];
            }
            foreach (XObjs.Shopping_card _card in this.lt_cart)
            {
                this.xtot_amt += _card.total_amt;
                this.xtot_items += Convert.ToInt32(_card.qty);
            }
            this.lblAgTotAmt.Text = this.xtot_amt.ToString();
            this.lblTmTotAmt.Text = this.xtot_amt.ToString();
            this.lblPtTotAmt.Text = this.xtot_amt.ToString();
            this.lblDsTotAmt.Text = this.xtot_amt.ToString();
            this.lblAgTotQty.Text = this.xtot_items.ToString();
            this.lblTmTotQty.Text = this.xtot_items.ToString();
            this.lblPtTotQty.Text = this.xtot_items.ToString();
            this.lblDsTotQty.Text = this.xtot_items.ToString();
            this.lblAgTotItems.Text = this.st_items.Count.ToString();
            this.lblTmTotItems.Text = this.st_items.Count.ToString();
            this.lblPtTotItems.Text = this.st_items.Count.ToString();
            this.lblDsTotItems.Text = this.st_items.Count.ToString();
        }

        protected void fillAmtList()
        {
            this.lt_fl = this.ret.getAllFee_list();
            foreach (XObjs.Fee_list _list in this.lt_fl)
            {
                this.sl_init_amt.Add(_list.xid, _list.init_amt);
                this.Session["Sl_init"] = this.sl_init_amt;
                this.sl_tech_amt.Add(_list.xid, _list.tech_amt);
                this.Session["Sl_tech"] = this.sl_tech_amt;
            }
        }

        protected void gvAg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (this.Session["SCart"] != null)
                {
                    this.lt_cart = (List<XObjs.Shopping_card>) this.Session["SCart"];
                }
                if (this.Session["SItems"] != null)
                {
                    this.st_items = (SortedList<string, XObjs.Shopping_card>) this.Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = this.gvAg.Rows[rowIndex].Cells[2].Text;
                string str3 = this.gvAg.Rows[rowIndex].Cells[0].Text;
                int amt = Convert.ToInt32(this.gvAg.Rows[rowIndex].Cells[3].Text);
                TextBox box = (TextBox) this.gvAg.Rows[rowIndex].Cells[4].FindControl("txtAg");
                ImageButton button = (ImageButton) this.gvAg.Rows[rowIndex].Cells[4].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (this.val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../../images/x.gif")
                    {
                        double num3 = this.calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = str3;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = text;
                        if (!this.lt_cart.Contains(item))
                        {
                            this.lt_cart.Add(item);
                            this.Session["SItems"] = this.st_items;
                        }
                        if (!this.st_items.ContainsKey(key))
                        {
                            this.st_items.Add(key, item);
                            this.Session["SCart"] = this.lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../../images/x.gif";
                    }
                    else
                    {
                        if (this.lt_cart.Contains(this.st_items[key]))
                        {
                            this.lt_cart.Remove(this.st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../../images/checkmark.gif";
                    }
                }
                this.displayTotals();
                this.show_inv = 1;
            }
        }

        protected void gvDs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DsStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (this.Session["SCart"] != null)
                {
                    this.lt_cart = (List<XObjs.Shopping_card>) this.Session["SCart"];
                }
                if (this.Session["SItems"] != null)
                {
                    this.st_items = (SortedList<string, XObjs.Shopping_card>) this.Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = this.gvDs.Rows[rowIndex].Cells[2].Text;
                string str3 = this.gvDs.Rows[rowIndex].Cells[0].Text;
                int amt = Convert.ToInt32(this.gvDs.Rows[rowIndex].Cells[3].Text);
                TextBox box = (TextBox) this.gvDs.Rows[rowIndex].Cells[4].FindControl("txtDs");
                ImageButton button = (ImageButton) this.gvDs.Rows[rowIndex].Cells[4].FindControl("lbAddDs");
                if (((box.Text != "") && (box.Text != "0")) && (this.val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../../images/x.gif")
                    {
                        double num3 = this.calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = str3;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = text;
                        if (!this.lt_cart.Contains(item))
                        {
                            this.lt_cart.Add(item);
                            this.Session["SItems"] = this.st_items;
                        }
                        if (!this.st_items.ContainsKey(key))
                        {
                            this.st_items.Add(key, item);
                            this.Session["SCart"] = this.lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../../images/x.gif";
                    }
                    else
                    {
                        if (this.lt_cart.Contains(this.st_items[key]))
                        {
                            this.lt_cart.Remove(this.st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../../images/checkmark.gif";
                    }
                }
                this.displayTotals();
                this.show_inv = 1;
            }
        }

        protected void gvPt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PtStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (this.Session["SCart"] != null)
                {
                    this.lt_cart = (List<XObjs.Shopping_card>) this.Session["SCart"];
                }
                if (this.Session["SItems"] != null)
                {
                    this.st_items = (SortedList<string, XObjs.Shopping_card>) this.Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = this.gvPt.Rows[rowIndex].Cells[2].Text;
                string str3 = this.gvPt.Rows[rowIndex].Cells[0].Text;
                int amt = Convert.ToInt32(this.gvPt.Rows[rowIndex].Cells[3].Text);
                TextBox box = (TextBox) this.gvPt.Rows[rowIndex].Cells[4].FindControl("txtPt");
                ImageButton button = (ImageButton) this.gvPt.Rows[rowIndex].Cells[4].FindControl("lbAddPt");
                if (((box.Text != "") && (box.Text != "0")) && (this.val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../../images/x.gif")
                    {
                        double num3 = this.calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = str3;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = text;
                        if (!this.lt_cart.Contains(item))
                        {
                            this.lt_cart.Add(item);
                            this.Session["SItems"] = this.st_items;
                        }
                        if (!this.st_items.ContainsKey(key))
                        {
                            this.st_items.Add(key, item);
                            this.Session["SCart"] = this.lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../../images/x.gif";
                    }
                    else
                    {
                        if (this.lt_cart.Contains(this.st_items[key]))
                        {
                            this.lt_cart.Remove(this.st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../../images/checkmark.gif";
                    }
                }
                this.displayTotals();
                this.show_inv = 1;
            }
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (this.Session["SCart"] != null)
                {
                    this.lt_cart = (List<XObjs.Shopping_card>) this.Session["SCart"];
                }
                if (this.Session["SItems"] != null)
                {
                    this.st_items = (SortedList<string, XObjs.Shopping_card>) this.Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = this.gvTm.Rows[rowIndex].Cells[2].Text;
                string str3 = this.gvTm.Rows[rowIndex].Cells[0].Text;
                int amt = Convert.ToInt32(this.gvTm.Rows[rowIndex].Cells[3].Text);
                TextBox box = (TextBox) this.gvTm.Rows[rowIndex].Cells[4].FindControl("txtTm");
                ImageButton button = (ImageButton) this.gvTm.Rows[rowIndex].Cells[4].FindControl("lbAddTm");
                if (((box.Text != "") && (box.Text != "0")) && (this.val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../../images/x.gif")
                    {
                        double num3 = this.calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = str3;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = text;
                        if (!this.lt_cart.Contains(item))
                        {
                            this.lt_cart.Add(item);
                            this.Session["SItems"] = this.st_items;
                        }
                        if (!this.st_items.ContainsKey(key))
                        {
                            this.st_items.Add(key, item);
                            this.Session["SCart"] = this.lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../../images/x.gif";
                    }
                    else
                    {
                        if (this.lt_cart.Contains(this.st_items[key]))
                        {
                            this.lt_cart.Remove(this.st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../../images/checkmark.gif";
                    }
                }
                this.displayTotals();
                this.show_inv = 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.Session["Sl_init"] = null;
                this.Session["Sl_tech"] = null;
                this.Session["SCart"] = null;
                this.Session["SItems"] = null;
                this.Session["c_app"] = null;
                this.fillAmtList();
                if ((((base.Request.Form["agentType"] != null) && (base.Request.Form["agentType"].ToString() != "")) && (base.Request.Form["pwalletID"] != null)) && (base.Request.Form["pwalletID"].ToString() != ""))
                {
                    this.adminID = base.Request.Form["pwalletID"].ToString();
                    this.Session["pwalletID"] = base.Request.Form["pwalletID"].ToString();
                    this.agentType = base.Request.Form["agentType"].ToString();
                    this.Session["agentType"] = base.Request.Form["agentType"].ToString();
                    if (this.agentType == "Agent")
                    {
                        this.c_reg = this.ret.getRegistrationByID(base.Request.Form["pwalletID"].ToString());
                        this.Session["c_reg"] = this.c_reg;
                    }
                    else
                    {
                        this.c_sub = this.ret.getSubAgentByID(base.Request.Form["pwalletID"].ToString());
                        this.Session["c_sub"] = this.c_sub;
                        this.c_sub_reg = this.ret.getRegistrationBySubagentRegistrationID(this.c_sub.RegistrationID);
                        this.Session["c_sub_reg"] = this.c_sub_reg;
                    }
                }
                else
                {
                    base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
                }
            }
            else if ((this.Session["pwalletID"] != null) && (this.Session["pwalletID"].ToString() != ""))
            {
                this.adminID = this.Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
        }

        protected void sendAlert()
        {
            if ((this.Session["AgentType"] != null) && (this.Session["AgentType"].ToString() != ""))
            {
                this.agentType = this.Session["AgentType"].ToString();
                if (this.agentType == "Agent")
                {
                    if (this.Session["c_reg"] != null)
                    {
                        this.c_reg = (XObjs.Registration) this.Session["c_reg"];
                    }
                    this.fullname = this.c_reg.Firstname + " " + this.c_reg.Surname;
                    this.Session["fullname"] = this.fullname;
                    this.email = this.c_reg.Email;
                    this.Session["email"] = this.email;
                    this.mobile = this.c_reg.PhoneNumber.ToUpper();
                    this.Session["mobile"] = this.mobile;
                }
                else
                {
                    if (this.Session["c_sub"] != null)
                    {
                        this.c_sub = (XObjs.Subagent) this.Session["c_sub"];
                    }
                    this.fullname = this.c_sub.Firstname + " " + this.c_sub.Surname;
                    this.Session["fullname"] = this.fullname;
                    this.email = this.c_sub.Email;
                    this.Session["email"] = this.email;
                    this.mobile = this.c_sub.Telephone.ToUpper();
                    this.Session["mobile"] = this.mobile;
                }
                Email email = new Email();
                Messenger messenger = new Messenger();
                string str = string.Format("{0:n}", this.xtotal_amt);
                string msg = ((("Dear " + this.fullname + ",<br/>") + "Your transaction has been added successfully!Please see details below:<br/>") + " Transaction ID: " + this.transID + "<br/>") + "Amount: " + str + ",<br/>Please go to the nearest bank to make payment or complete the payment online.<br/>Regards";
                string s = ((("Dear " + this.fullname + ",") + "Your transaction has been added successfully!Please see details below:\r\n") + "Transaction ID: " + this.transID + "\r\n") + "Amount: " + str + ",\r\nPlease go to the nearest bank to make payment  or complete the payment online.\r\nRegards";
                string subject = "PAYX ALERT";
                string from = "paymentsupport@einaosolutions.com";
                string to = this.email;
                string mobile = this.mobile;
                s = base.Server.UrlEncode(s);
                if (mobile.StartsWith("0"))
                {
                    mobile = "234" + mobile.Remove(0, 1);
                }
                email.sendMail("PAYX ALERT", to, from, subject, msg, "");
              //  string str8 = messenger.send_sms(s, "PAYX ALERT", mobile);
            }
        }
    }
}

