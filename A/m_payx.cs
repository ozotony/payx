namespace XPay.A
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class m_payx : Page
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
        protected SqlDataSource flDs2;
        protected SqlDataSource flPt;
        protected SqlDataSource flTm;
        protected HtmlForm form1;
        protected string fullname = "";
        protected GridView gvAg;
        protected GridView gvDs;
        protected GridView gvPt;
        protected GridView gvTm;
        protected GridView gvAg2;
        protected GridView gvAg3;
        protected GridView gvAg4;
        protected GridView gvAg5;

        protected GridView gvAg6;

        protected GridView gvAg7;
        protected GridView gvAg8;
        protected GridView gvAg9;

        protected Panel ppp;
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


        static object locker = new object();
        static string Generate15UniqueDigits()
        {
            lock (locker)
            {
                Thread.Sleep(100);
                return DateTime.Now.ToString("yyyyMMddHHmmssf");
            }
        }
        protected void AddFeeList()
        {
            if (Session["c_app"] != null)
            {
                c_app = (XObjs.Applicant) Session["c_app"];
            }
            int num = reg.addApplicant(c_app);
            if ((num > 0) && ((Session["AgentType"] != null) && (Session["AgentType"].ToString() != "")))
            {
                agentType = Session["AgentType"].ToString();
                if (agentType == "agent")
                {
                    agentType = "Agent";
                    Session["AgentType"] = agentType;
                }
                if (agentType == "Agent")
                {
                    if (Session["c_reg"] != null)
                    {
                        c_reg = (XObjs.Registration) Session["c_reg"];
                    }
                   
                    fullname = c_reg.Firstname + " " + c_reg.Surname;
                    Session["fullname"] = fullname;
                    email = c_reg.Email;
                    Session["email"] = email;
                    mobile = c_reg.PhoneNumber;
                    Session["mobile"] = mobile;
                }
                else
                {
                    if (Session["c_sub"] != null)
                    {
                        c_sub = (XObjs.Subagent) Session["c_sub"];
                    }
                    fullname = c_sub.Firstname + " " + c_sub.Surname;
                    Session["fullname"] = fullname;
                    email = c_sub.Email;
                    Session["email"] = email;
                    mobile = c_sub.Telephone;
                    Session["mobile"] = mobile;
                }
                scard = ret.getRandomScard();
                string vtransid = Generate15UniqueDigits(); ;
              //  lt_twall = ret.getTwalletByMemberID(adminID, scard.xnum, Session["AgentType"].ToString());
                lt_twall = ret.getTwalletByMemberID(adminID, vtransid, Session["AgentType"].ToString());
                if (lt_twall.Count == 0)
                { 
                   
                   // transID = scard.xnum.ToUpper();
                    transID = vtransid;
                    int num2 = 0;
                    int num3 = 0;
                   // twall.ref_no = "X" + adminID + "-" + DateTime.Now.ToString("yyyy") + "-" + scard.xnum;

                    twall.ref_no = "X" + adminID + "-" + DateTime.Now.ToString("yyyy") + "-" + transID;
                    ref_no = twall.ref_no;
                 //   twall.transID = scard.xnum;
                    twall.transID = transID;
                    twall.xbankerID = "0";
                    twall.xgt = "xpay";
                    twall.xmemberID = adminID;
                    twall.xmembertype = Session["AgentType"].ToString();
                    twall.xpay_status = "2";
                    twall.applicantID = num.ToString();
                    twall.xreg_date = xreg_date;
                    twall.xsync = "0";
                    twall.xvisible = "1";
                    num2 = reg.addTwallet(twall);
                    if (num2 > 0)
                    {
                        if (Session["SCart"] != null)
                        {
                            lt_cart = (List<XObjs.Shopping_card>) Session["SCart"];
                        }
                        if (Session["Sl_init"] != null)
                        {
                            sl_init_amt = (SortedList<string, string>) Session["Sl_init"];
                        }
                        if (Session["Sl_tech"] != null)
                        {
                            sl_tech_amt = (SortedList<string, string>) Session["Sl_tech"];
                        }
                        int num4 = 0;
                        foreach (XObjs.Shopping_card _card in lt_cart)
                        {
                            f_dets.twalletID = num2.ToString();
                            f_dets.fee_listID = _card.xid;
                            f_dets.xlogstaff = adminID;
                            f_dets.xqty = _card.qty;
                            f_dets.xused = "0";
                            f_dets.tot_amt = _card.total_amt.ToString();
                            xtotal_amt += Convert.ToInt32(_card.total_amt);
                            f_dets.init_amt = sl_init_amt[_card.xid];
                            f_dets.tech_amt = sl_tech_amt[_card.xid];
                            f_dets.xreg_date = xreg_date;
                            f_dets.xsync = "0";
                            f_dets.xvisible = "1";
                            num3 = reg.addFee_details(f_dets);
                            num4++;
                            for (int i = 0; i < Convert.ToInt32(_card.qty); i++)
                            {
                                int num6 = 0;
                                c_hwall.transID = transID;// scard.xnum;
                                c_hwall.used_status = "Not Used";
                                c_hwall.product_title = "";
                                c_hwall.xreg_date = xreg_date;
                                c_hwall.used_date = "";
                                c_hwall.fee_detailsID = num3.ToString();
                                num6 = reg.addHwallet(c_hwall);
                            }
                        }
                        if (num4 == lt_cart.Count<XObjs.Shopping_card>())
                        {
                           // base.Response.Redirect("./m_invoicex.aspx?mx=" + adminID + "&tx=" + scard.xnum);

                            base.Response.Redirect("./m_invoicex.aspx?mx=" + adminID + "&tx=" + transID);
                        }
                    }
                }
                else
                {
                    AddFeeList();
                }
            }
        }

        protected void btnApplicant_Click(object sender, EventArgs e)
        {
            show_inv = 0;
        }

        protected void btnChangeItems_Click(object sender, EventArgs e)
        {
            show_inv = 1;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Session["SCart"] != null)
            {
                lt_cart = (List<XObjs.Shopping_card>) Session["SCart"];
                if (lt_cart.Count > 0)
                {
                    show_inv = 2;
                }
                else
                {
                    show_inv = 1;
                }
            }
            else
            {
                show_inv = 1;
            }
        }

        protected void BtnDashboard_Click(object sender, EventArgs e)
        {
            base.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["agent_home"] );
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            AddFeeList();
        }

        protected void btnProceedToPurchase_Click(object sender, EventArgs e)
        {
            Session["c_app"] = null;
            c_app.address = txt_app_addy.Text;
            c_app.xemail = txt_app_email.Text;
            c_app.xmobile = txt_app_no.Text;
            c_app.xname = txt_app_name.Text;
            Session["c_app"] = c_app;
            if ((((txt_app_addy.Text != "") && (txt_app_email.Text != "")) && (txt_app_no.Text != "")) && (txt_app_name.Text != ""))
            {
                show_inv = 1;
            }
            else
            {
                show_inv = 0;
            }
        }

        protected double calcTotalAmt(int qty, int amt)
        {
            return (double) (qty * amt);
        }

        protected void displayTotals()
        {
            if (Session["SCart"] != null)
            {
                lt_cart = (List<XObjs.Shopping_card>) Session["SCart"];
            }
            foreach (XObjs.Shopping_card _card in lt_cart)
            {
                xtot_amt += _card.total_amt;
                xtot_items += Convert.ToInt32(_card.qty);
            }
            lblAgTotAmt.Text = string.Format("{0:n}", xtot_amt);
            lblTmTotAmt.Text = string.Format("{0:n}", xtot_amt);
            lblPtTotAmt.Text = string.Format("{0:n}", xtot_amt);
            lblDsTotAmt.Text = string.Format("{0:n}", xtot_amt);
            lblAgTotQty.Text = xtot_items.ToString();
            lblTmTotQty.Text = xtot_items.ToString();
            lblPtTotQty.Text = xtot_items.ToString();
            lblDsTotQty.Text = xtot_items.ToString();
            lblAgTotItems.Text = st_items.Count.ToString();
            lblTmTotItems.Text = st_items.Count.ToString();
            lblPtTotItems.Text = st_items.Count.ToString();
            lblDsTotItems.Text = st_items.Count.ToString();
        }

        protected void fillAmtList()
        {
            lt_fl = ret.getAllFee_list();
            foreach (XObjs.Fee_list _list in lt_fl)
            {
                sl_init_amt.Add(_list.xid, _list.init_amt);
                Session["Sl_init"] = sl_init_amt;
                sl_tech_amt.Add(_list.xid, _list.tech_amt);
                Session["Sl_tech"] = sl_tech_amt;

                
            }
        }

        protected void gvAg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>) Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>) Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvAg.Rows[rowIndex].Cells[0].Text;
                string str3 = gvAg.Rows[rowIndex].Cells[2].Text;
                string str4 = gvAg.Rows[rowIndex].Cells[3].Text;
                string str5 = gvAg.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvAg.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox) gvAg.Rows[rowIndex].Cells[6].FindControl("txtAg");
                ImageButton button = (ImageButton) gvAg.Rows[rowIndex].Cells[7].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }


        protected void gvAg_RowCommand2(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>)Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvAg2.Rows[rowIndex].Cells[0].Text;
                string str3 = gvAg2.Rows[rowIndex].Cells[2].Text;
                string str4 = gvAg2.Rows[rowIndex].Cells[3].Text;
                string str5 = gvAg2.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvAg2.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox)gvAg2.Rows[rowIndex].Cells[6].FindControl("txtAg");
                ImageButton button = (ImageButton)gvAg2.Rows[rowIndex].Cells[7].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvAg_RowCommand3(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>)Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvAg3.Rows[rowIndex].Cells[0].Text;
                string str3 = gvAg3.Rows[rowIndex].Cells[2].Text;
                string str4 = gvAg3.Rows[rowIndex].Cells[3].Text;
                string str5 = gvAg3.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvAg3.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox)gvAg3.Rows[rowIndex].Cells[6].FindControl("txtAg");
                ImageButton button = (ImageButton)gvAg3.Rows[rowIndex].Cells[7].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvAg_RowCommand5(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>)Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvAg5.Rows[rowIndex].Cells[0].Text;
                string str3 = gvAg5.Rows[rowIndex].Cells[2].Text;
                string str4 = gvAg5.Rows[rowIndex].Cells[3].Text;
                string str5 = gvAg5.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvAg5.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox)gvAg5.Rows[rowIndex].Cells[6].FindControl("txtAg");
                ImageButton button = (ImageButton)gvAg5.Rows[rowIndex].Cells[7].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvAg_RowCommand6(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>)Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvAg6.Rows[rowIndex].Cells[0].Text;
                string str3 = gvAg6.Rows[rowIndex].Cells[2].Text;
                string str4 = gvAg6.Rows[rowIndex].Cells[3].Text;
                string str5 = gvAg6.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvAg6.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox)gvAg6.Rows[rowIndex].Cells[6].FindControl("txtAg");
                ImageButton button = (ImageButton)gvAg6.Rows[rowIndex].Cells[7].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvAg_RowCommand7(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>)Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvAg7.Rows[rowIndex].Cells[0].Text;
                string str3 = gvAg7.Rows[rowIndex].Cells[2].Text;
                string str4 = gvAg7.Rows[rowIndex].Cells[3].Text;
                string str5 = gvAg7.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvAg7.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox)gvAg7.Rows[rowIndex].Cells[6].FindControl("txtAg");
                ImageButton button = (ImageButton)gvAg7.Rows[rowIndex].Cells[7].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvAg_RowCommand9(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>)Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvAg9.Rows[rowIndex].Cells[0].Text;
                string str3 = gvAg9.Rows[rowIndex].Cells[2].Text;
                string str4 = gvAg9.Rows[rowIndex].Cells[3].Text;
                string str5 = gvAg9.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvAg9.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox)gvAg9.Rows[rowIndex].Cells[6].FindControl("txtAg");
                ImageButton button = (ImageButton)gvAg9.Rows[rowIndex].Cells[7].FindControl("lbAddAg");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvDs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DsStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>) Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>) Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvDs.Rows[rowIndex].Cells[0].Text;
                string str3 = gvDs.Rows[rowIndex].Cells[2].Text;
                string str4 = gvDs.Rows[rowIndex].Cells[3].Text;
                string str5 = gvDs.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvDs.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox) gvDs.Rows[rowIndex].Cells[6].FindControl("txtDs");
                ImageButton button = (ImageButton) gvDs.Rows[rowIndex].Cells[7].FindControl("lbAddDs");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvPt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PtStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>) Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>) Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvPt.Rows[rowIndex].Cells[0].Text;
                string str3 = gvPt.Rows[rowIndex].Cells[2].Text;
                string str4 = gvPt.Rows[rowIndex].Cells[3].Text;
                string str5 = gvPt.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvPt.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox) gvPt.Rows[rowIndex].Cells[6].FindControl("txtPt");
                ImageButton button = (ImageButton) gvPt.Rows[rowIndex].Cells[7].FindControl("lbAddPt");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void gvTm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TmStatusClick")
            {
                XObjs.Shopping_card item = new XObjs.Shopping_card();
                if (Session["SCart"] != null)
                {
                    lt_cart = (List<XObjs.Shopping_card>) Session["SCart"];
                }
                if (Session["SItems"] != null)
                {
                    st_items = (SortedList<string, XObjs.Shopping_card>) Session["SItems"];
                }
                GridViewRow namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                int rowIndex = namingContainer.RowIndex;
                string key = e.CommandArgument.ToString();
                string text = gvTm.Rows[rowIndex].Cells[0].Text;
                string str3 = gvTm.Rows[rowIndex].Cells[2].Text;
                string str4 = gvTm.Rows[rowIndex].Cells[3].Text;
                string str5 = gvTm.Rows[rowIndex].Cells[4].Text;
                int amt = Convert.ToInt32(gvTm.Rows[rowIndex].Cells[5].Text);
                TextBox box = (TextBox) gvTm.Rows[rowIndex].Cells[6].FindControl("txtTm");
                ImageButton button = (ImageButton) gvTm.Rows[rowIndex].Cells[7].FindControl("lbAddTm");
                if (((box.Text != "") && (box.Text != "0")) && (val.IsInt32(box.Text) == 0))
                {
                    if (button.ImageUrl != "../images/remove.png")
                    {
                        double num3 = calcTotalAmt(Convert.ToInt32(box.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = box.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                        box.ReadOnly = true;
                        button.ImageUrl = "../images/remove.png";
                    }
                    else
                    {
                        if (lt_cart.Contains(st_items[key]))
                        {
                            lt_cart.Remove(st_items[key]);
                        }
                        box.Text = "0";
                        box.ReadOnly = false;
                        button.ImageUrl = "../images/add_btn.png";
                    }
                }
                displayTotals();
                show_inv = 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["Sl_init"] = null;
                Session["Sl_tech"] = null;
                Session["SCart"] = null;
                Session["SItems"] = null;
                Session["c_app"] = null;
                Session["onlineid"] = null;
                Session["onlineid2"] = null;
                Session["onlineid3"] = null;
                Session["onlineid4"] = null;
                Session["onlineid5"] = null;
                Session["onlineid6"] = null;
                Session["onlineid7"] = null;
                Session["onlineid8"] = null;
                Session["onlineid9"] = null;
                if (base.Request.Form["xname"] != null)
                {
                 txt_app_name.Text=   base.Request.Form["xname"].ToString();
                }

                 if (base.Request.Form["address"] != null)
                {
                 txt_app_addy.Text=   base.Request.Form["address"].ToString();

                 }

                 if (base.Request.Form["email"] != null)
                 {
                     txt_app_email.Text = base.Request.Form["email"].ToString();

                 }

                 if (base.Request.Form["PhoneNumber"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;
                    
                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;

                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = true;

                     gvAg3.Visible = false;

                     ppp.Visible = false;

                     Session["c_app"] = null;
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                      XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg2.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                           string key = row.Cells[1].Text;
                       //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                string text = row.Cells[0].Text;
                string str3 = row.Cells[2].Text;
                string str4 = row.Cells[3].Text;
                string str5 = row.Cells[4].Text;
                int amt = Convert.ToInt32(row.Cells[5].Text);


                          double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = chkBox.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }
                         

                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();


                     //if (Session["SCart"] != null)
                     //{
                     //    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                     //    if (lt_cart.Count > 0)
                     //    {
                     //        show_inv = 2;
                     //    }
                     //    else
                     //    {
                     //        show_inv = 1;
                     //    }
                     //}
                     //else
                     //{
                     //    show_inv = 1;
                     //}

                 }


                 if (base.Request.Form["PhoneNumber2"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber2"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;
                    

                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible =true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;

                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg3.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();


                     //if (Session["SCart"] != null)
                     //{
                     //    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                     //    if (lt_cart.Count > 0)
                     //    {
                     //        show_inv = 2;
                     //    }
                     //    else
                     //    {
                     //        show_inv = 1;
                     //    }
                     //}
                     //else
                     //{
                     //    show_inv = 1;
                     //}

                 }


                 if (base.Request.Form["PhoneNumber3"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber3"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid2"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;

                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg3.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();


                     //if (Session["SCart"] != null)
                     //{
                     //    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                     //    if (lt_cart.Count > 0)
                     //    {
                     //        show_inv = 2;
                     //    }
                     //    else
                     //    {
                     //        show_inv = 1;
                     //    }
                     //}
                     //else
                     //{
                     //    show_inv = 1;
                     //}

                 }

                 if (base.Request.Form["PhoneNumber4"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber4"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = false;
                     gvAg4.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid3"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;
                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                    
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg4.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();


                     //if (Session["SCart"] != null)
                     //{
                     //    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                     //    if (lt_cart.Count > 0)
                     //    {
                     //        show_inv = 2;
                     //    }
                     //    else
                     //    {
                     //        show_inv = 1;
                     //    }
                     //}
                     //else
                     //{
                     //    show_inv = 1;
                     //}

                 }

                 if (base.Request.Form["PhoneNumber5"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber5"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = false;
                     gvAg4.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid4"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;
                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg4.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();


                     //if (Session["SCart"] != null)
                     //{
                     //    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                     //    if (lt_cart.Count > 0)
                     //    {
                     //        show_inv = 2;
                     //    }
                     //    else
                     //    {
                     //        show_inv = 1;
                     //    }
                     //}
                     //else
                     //{
                     //    show_inv = 1;
                     //}

                 }

                 if (base.Request.Form["PhoneNumber6"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber6"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = false;
                     gvAg4.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid5"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;
                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg4.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();


                     //if (Session["SCart"] != null)
                     //{
                     //    lt_cart = (List<XObjs.Shopping_card>)Session["SCart"];
                     //    if (lt_cart.Count > 0)
                     //    {
                     //        show_inv = 2;
                     //    }
                     //    else
                     //    {
                     //        show_inv = 1;
                     //    }
                     //}
                     //else
                     //{
                     //    show_inv = 1;
                     //}

                 }

                 if (base.Request.Form["PhoneNumber7"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber7"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = false;
                     gvAg4.Visible = false;
                     gvAg5.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid6"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;
                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg5.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();



                 }

                 if (base.Request.Form["PhoneNumber8"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber8"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = false;
                     gvAg4.Visible = false;
                     gvAg5.Visible = false;
                     gvAg6.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid7"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;
                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg6.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();



                 }
                 if (base.Request.Form["PhoneNumber9"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber9"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = false;
                     gvAg4.Visible = false;
                     gvAg5.Visible = false;
                     gvAg6.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid8"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;
                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg6.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();



                 }

                 if (base.Request.Form["PhoneNumber10"] != null)
                 {
                     txt_app_no.Text = base.Request.Form["PhoneNumber10"].ToString();
                     adminID = base.Request.Form["pwalletID"].ToString();
                     Session["pwalletID"] = adminID;

                     c_reg = ret.getRegistrationByID(adminID);
                     Session["c_reg"] = c_reg;

                     gvTm.Visible = false;


                     gvPt.Visible = false;

                     gvDs.Visible = false;

                     gvAg.Visible = false;

                     gvAg2.Visible = false;
                     gvAg3.Visible = false;
                     gvAg4.Visible = false;
                     gvAg5.Visible = false;
                     gvAg6.Visible = true;

                     ppp.Visible = false;

                     if (base.Request.Form["onlineid"] != null)
                     {
                         Session["onlineid9"] = base.Request.Form["onlineid"].ToString();
                     }

                     Session["c_app"] = null;
                     txt_app_addy.Text = base.Request.Form["address2"].ToString();
                     txt_app_email.Text = base.Request.Form["email2"].ToString();
                     txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                     txt_app_name.Text = base.Request.Form["xname2"].ToString();
                     c_app.address = txt_app_addy.Text;
                     c_app.xemail = txt_app_email.Text;
                     c_app.xmobile = txt_app_no.Text;
                     c_app.xname = txt_app_name.Text;
                     Session["c_app"] = c_app;
                     XObjs.Shopping_card item = new XObjs.Shopping_card();
                     foreach (GridViewRow row in gvAg7.Rows)
                     {
                         TextBox chkBox = row.FindControl("txtAg") as TextBox;

                         chkBox.Text = "1";


                         string key = row.Cells[1].Text;
                         //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                         string text = row.Cells[0].Text;
                         string str3 = row.Cells[2].Text;
                         string str4 = row.Cells[3].Text;
                         string str5 = row.Cells[4].Text;
                         int amt = Convert.ToInt32(row.Cells[5].Text);


                         double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                         item.xid = text;
                         item.amt = amt;
                         item.qty = chkBox.Text;
                         item.total_amt = num3;
                         item.item_code = key;
                         item.item_desc = str3;
                         item.init_amt = str4;
                         item.tech_amt = str5;
                         if (!lt_cart.Contains(item))
                         {
                             lt_cart.Add(item);
                             Session["SItems"] = st_items;
                         }
                         if (!st_items.ContainsKey(key))
                         {
                             st_items.Add(key, item);
                             Session["SCart"] = lt_cart;
                         }


                     }
                     Session["AgentType"] = "Agent";
                     fillAmtList();
                     AddFeeList();



                 }


                if (base.Request.Form["PhoneNumber11"] != null)
                {
                    txt_app_no.Text = base.Request.Form["PhoneNumber11"].ToString();
                    adminID = base.Request.Form["pwalletID"].ToString();
                    Session["pwalletID"] = adminID;

                    c_reg = ret.getRegistrationByID(adminID);
                    Session["c_reg"] = c_reg;

                    gvTm.Visible = false;


                    gvPt.Visible = false;

                    gvDs.Visible = false;

                    gvAg.Visible = false;

                    gvAg2.Visible = false;
                    gvAg3.Visible = false;
                    gvAg4.Visible = false;
                    gvAg5.Visible = false;
                    gvAg6.Visible = false;
                    gvAg7.Visible = false;
                    gvAg8.Visible = true;
                    ppp.Visible = false;

                    if (base.Request.Form["onlineid"] != null)
                    {
                        Session["onlineid10"] = base.Request.Form["onlineid"].ToString();
                    }

                    Session["c_app"] = null;
                    txt_app_addy.Text = base.Request.Form["address2"].ToString();
                    txt_app_email.Text = base.Request.Form["email2"].ToString();
                    txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                    txt_app_name.Text = base.Request.Form["xname2"].ToString();
                    c_app.address = txt_app_addy.Text;
                    c_app.xemail = txt_app_email.Text;
                    c_app.xmobile = txt_app_no.Text;
                    c_app.xname = txt_app_name.Text;
                    Session["c_app"] = c_app;
                    XObjs.Shopping_card item = new XObjs.Shopping_card();
                    foreach (GridViewRow row in gvAg8.Rows)
                    {
                        TextBox chkBox = row.FindControl("txtAg") as TextBox;

                        chkBox.Text = "1";


                        string key = row.Cells[1].Text;
                        //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                        string text = row.Cells[0].Text;
                        string str3 = row.Cells[2].Text;
                        string str4 = row.Cells[3].Text;
                        string str5 = row.Cells[4].Text;
                        int amt = Convert.ToInt32(row.Cells[5].Text);


                        double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = chkBox.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }


                    }
                    Session["AgentType"] = "Agent";
                    fillAmtList();
                    AddFeeList();



                }


                if (base.Request.Form["PhoneNumber12"] != null)
                {
                    txt_app_no.Text = base.Request.Form["PhoneNumber12"].ToString();
                    adminID = base.Request.Form["pwalletID"].ToString();
                    Session["pwalletID"] = adminID;

                    c_reg = ret.getRegistrationByID(adminID);
                    Session["c_reg"] = c_reg;

                    gvTm.Visible = false;


                    gvPt.Visible = false;

                    gvDs.Visible = false;

                    gvAg.Visible = false;

                    gvAg2.Visible = false;
                    gvAg3.Visible = false;
                    gvAg4.Visible = false;
                    gvAg5.Visible = false;
                    gvAg6.Visible = false;
                    gvAg9.Visible = true;

                    ppp.Visible = false;

                    if (base.Request.Form["onlineid"] != null)
                    {
                        Session["onlineid12"] = base.Request.Form["onlineid"].ToString();
                    }

                    Session["c_app"] = null;
                    txt_app_addy.Text = base.Request.Form["address2"].ToString();
                    txt_app_email.Text = base.Request.Form["email2"].ToString();
                    txt_app_no.Text = base.Request.Form["PhoneNumber77"].ToString();
                    txt_app_name.Text = base.Request.Form["xname2"].ToString();
                    c_app.address = txt_app_addy.Text;
                    c_app.xemail = txt_app_email.Text;
                    c_app.xmobile = txt_app_no.Text;
                    c_app.xname = txt_app_name.Text;
                    Session["c_app"] = c_app;
                    XObjs.Shopping_card item = new XObjs.Shopping_card();
                    foreach (GridViewRow row in gvAg9.Rows)
                    {
                        TextBox chkBox = row.FindControl("txtAg") as TextBox;

                        chkBox.Text = "1";


                        string key = row.Cells[1].Text;
                        //  string key = gvAg2.DataKeys[row.RowIndex].Value.ToString();


                        string text = row.Cells[0].Text;
                        string str3 = row.Cells[2].Text;
                        string str4 = row.Cells[3].Text;
                        string str5 = row.Cells[4].Text;
                        int amt = Convert.ToInt32(row.Cells[5].Text);


                        double num3 = calcTotalAmt(Convert.ToInt32(chkBox.Text), amt);
                        item.xid = text;
                        item.amt = amt;
                        item.qty = chkBox.Text;
                        item.total_amt = num3;
                        item.item_code = key;
                        item.item_desc = str3;
                        item.init_amt = str4;
                        item.tech_amt = str5;
                        if (!lt_cart.Contains(item))
                        {
                            lt_cart.Add(item);
                            Session["SItems"] = st_items;
                        }
                        if (!st_items.ContainsKey(key))
                        {
                            st_items.Add(key, item);
                            Session["SCart"] = lt_cart;
                        }


                    }
                    Session["AgentType"] = "Agent";
                    fillAmtList();
                    AddFeeList();



                }


                fillAmtList();
                if ((((base.Request.Form["agentType"] != null) && (base.Request.Form["agentType"].ToString() != "")) && (base.Request.Form["pwalletID"] != null)) && (base.Request.Form["pwalletID"].ToString() != ""))
                {
                    
                    adminID = base.Request.Form["pwalletID"].ToString();
                    Session["pwalletID"] = adminID;
                    agentType = base.Request.Form["agentType"].ToString();
                    Session["agentType"] = agentType;
                    if (agentType == "Agent")
                    {
                        c_reg = ret.getRegistrationByID(adminID);
                        Session["c_reg"] = c_reg;
                    }
                    else
                    {
                        c_sub = ret.getSubAgentByID(adminID);
                        Session["c_sub"] = c_sub;
                        c_sub_reg = ret.getRegistrationBySubagentRegistrationID(c_sub.RegistrationID);
                        Session["c_sub_reg"] = c_sub_reg;
                    }
                }
                else
                {
                    base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
                }
            }
            else if ((Session["pwalletID"] != null) && (Session["pwalletID"].ToString() != ""))
            {
                adminID = Session["pwalletID"].ToString();
            }
            else
            {
                base.Response.Redirect(ConfigurationManager.AppSettings["agent_home"]);
            }
        }

        protected void sendAlert()
        {
            if ((Session["AgentType"] != null) && (Session["AgentType"].ToString() != ""))
            {
                agentType = Session["AgentType"].ToString();
                if (agentType == "Agent")
                {
                    if (Session["c_reg"] != null)
                    {
                        c_reg = (XObjs.Registration) Session["c_reg"];
                    }
                    fullname = c_reg.Firstname + " " + c_reg.Surname;
                    Session["fullname"] = fullname;
                    email = c_reg.Email;
                    Session["email"] = email;
                    mobile = c_reg.PhoneNumber.ToUpper();
                    Session["mobile"] = mobile;
                }
                else
                {
                    if (Session["c_sub"] != null)
                    {
                        c_sub = (XObjs.Subagent) Session["c_sub"];
                    }
                    fullname = c_sub.Firstname + " " + c_sub.Surname;
                    Session["fullname"] = fullname;
                    email = c_sub.Email;
                    Session["email"] = email;
                    mobile = c_sub.Telephone.ToUpper();
                    Session["mobile"] = mobile;
                }
                Email to_email = new Email();
                Messenger messenger = new Messenger();
                string str = string.Format("{0:n}", xtotal_amt);
                string msg = ((("Dear " + fullname + ",<br/>") + "Your transaction has been added successfully!Please see details below:<br/>") + " Transaction ID: " + transID + "<br/>") + "Amount: " + str + ",<br/>Please go to the nearest bank to make payment or complete the payment online.<br/>Regards";
                string s = ((("Dear " + fullname + ",") + "Your transaction has been added successfully!Please see details below:\r\n") + "Transaction ID: " + transID + "\r\n") + "Amount: " + str + ",\r\nPlease go to the nearest bank to make payment  or complete the payment online.\r\nRegards";
                string subject = "XPAY ALERT";
                string from = "admin@xpay.com";
                string to = email;
                s = base.Server.UrlEncode(s);
                if (mobile.StartsWith("0"))
                {
                    mobile = "234" + mobile.Remove(0, 1);
                }
                to_email.sendMail("XPAY ALERT", to, from, subject, msg, "");
               // string str8 = messenger.send_sms(s, "XPAY ALERT", mobile);
            }
        }
    }
}

