namespace XPay.X
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class m_items : Page
    {
        protected TextBox a_init_amt;
        protected TextBox a_item;
        protected TextBox a_item_code;
        protected TextBox a_qt_code;
        protected TextBox a_tech_amt;
        protected DropDownList a_xcategory;
        protected string adminID = "0";
        protected Button btnAddItem;
        protected Button btnBack;
        protected Button btnEditBack;
        protected Button btnEditItem;
        protected Button btnExportExcel;
        protected LinkButton btnNewRecLink;
        protected ImageButton btnNewrecord;
        protected Button btnSearch;
        protected XObjs.Fee_list c_x = new XObjs.Fee_list();
        protected DropDownList ddl_merchants;
        public string docpath = "";
        protected TextBox e_init_amt;
        protected TextBox e_item;
        protected TextBox e_item_code;
        protected TextBox e_qt_code;
        protected TextBox e_tech_amt;
        protected DropDownList e_xcategory;
        protected HiddenField e_xid;
        public ExcelFuncs ef = new ExcelFuncs();
        protected HtmlForm form1;
        protected GridView gvX;
        protected HtmlHead Head1;
        public List<XObjs.MerchantCatList> lt_cdl = new List<XObjs.MerchantCatList>();
        public List<XObjs.MerchantDropList> lt_mdl = new List<XObjs.MerchantDropList>();
        public List<XObjs.Fee_list> lt_x = new List<XObjs.Fee_list>();
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected int show_add = 0;
        protected int show_edit = 0;
        protected int show_inv = 0;
        protected int x_cnt = 0;
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            this.c_x.item_code = this.a_item_code.Text;
            this.c_x.item = this.a_item.Text;
            this.c_x.xdesc = this.a_item.Text;
            this.c_x.qt_code = this.a_qt_code.Text;
            this.c_x.init_amt = this.a_init_amt.Text;
            this.c_x.tech_amt = this.a_tech_amt.Text;
            this.c_x.xcategory = this.a_xcategory.SelectedValue;
            this.c_x.xlogstaff = this.adminID;
            this.c_x.xreg_date = this.xreg_date;
            this.c_x.xvisible = "1";
            this.c_x.xsync = this.ddl_merchants.SelectedValue;
            int num = this.reg.addFee_list(this.c_x);
            StringBuilder builder = new StringBuilder();
            if (num > 0)
            {
                builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Success\" class=\"style2\" src=\"../images/check.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD ADDED SUCCESSFULLY!!</strong></td></tr>");
            }
            else
            {
                builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Error\" class=\"style2\" src=\"../images/delete.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD NOT ADDED SUCCESSFULLY!!</strong></td></tr>");
            }
            this.Session["x_msg"] = builder.ToString();
            base.Response.Redirect("./m_items.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            this.Session["x_msg"] = "";
            this.show_inv = 1;
            this.show_edit = 0;
            this.show_add = 0;
            if (this.Session["x_cnt"] != null)
            {
                this.x_cnt = Convert.ToInt32(this.Session["x_cnt"]);
            }
        }

        protected void btnEditBack_Click(object sender, EventArgs e)
        {
            this.Session["x_msg"] = "";
            this.show_inv = 1;
            this.show_edit = 0;
            this.show_add = 0;
            if (this.Session["x_cnt"] != null)
            {
                this.x_cnt = Convert.ToInt32(this.Session["x_cnt"]);
            }
        }

        protected void btnEditItem_Click(object sender, EventArgs e)
        {
            if ((this.Session["e_xid"] != null) && (this.Session["e_xid"].ToString() != ""))
            {
                this.c_x.xid = this.Session["e_xid"].ToString();
                this.c_x.item_code = this.e_item_code.Text;
                this.c_x.item = this.e_item.Text;
                this.c_x.xdesc = this.e_item.Text;
                this.c_x.qt_code = this.e_qt_code.Text;
                this.c_x.init_amt = this.e_init_amt.Text;
                this.c_x.tech_amt = this.e_tech_amt.Text;
                this.c_x.xcategory = this.e_xcategory.SelectedValue;
                this.c_x.xlogstaff = this.adminID;
                this.c_x.xreg_date = this.xreg_date;
                this.c_x.xvisible = "1";
                this.c_x.xsync = this.ddl_merchants.SelectedValue;
                int num = this.reg.updateFee_list(this.c_x);
                StringBuilder builder = new StringBuilder();
                if (num > 0)
                {
                    builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Success\" class=\"style2\" src=\"../images/check.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD EDITED SUCCESSFULLY!!</strong></td></tr>");
                }
                else
                {
                    builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Error\" class=\"style2\" src=\"../images/delete.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD NOT EDITED SUCCESSFULLY!!</strong></td></tr>");
                }
                this.Session["x_msg"] = builder.ToString();
                base.Response.Redirect("./m_items.aspx");
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (this.Session["x_cnt"] != null)
            {
                this.x_cnt = Convert.ToInt32(this.Session["x_cnt"]);
            }
            this.show_inv = 1;
            this.show_edit = 0;
            this.show_add = 0;
            this.Session["x_msg"] = "";
            if (this.Session["x_cnt"] != null)
            {
                this.x_cnt = Convert.ToInt32(this.Session["x_cnt"]);
            }
            this.show_inv = 1;
            this.show_edit = 0;
            this.show_add = 0;
            this.Session["x_msg"] = "";
            if ((this.Session["x_cnt"] != null) && (this.Session["x_cnt"].ToString() != ""))
            {
                this.x_cnt = Convert.ToInt32(this.Session["x_cnt"]);
            }
            if ((this.Session["lt_x"] != null) && (this.Session["lt_x"].ToString() != ""))
            {
                this.lt_x = (List<XObjs.Fee_list>) this.Session["lt_x"];
            }
            this.gvX.DataSource = this.lt_x;
            this.gvX.DataBind();
            this.show_inv = 0;
            string docpath = this.docpath;
            this.docpath = docpath + "Fee_Items-" + this.adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";
            this.ef.CreateItemsExcel(this, this.lt_x, this.docpath, this.ddl_merchants.SelectedValue.ToUpper() + " Fee Items Details");
        }

        protected void btnNewRecLink_Click(object sender, EventArgs e)
        {
            this.Session["x_msg"] = "";
            this.show_inv = 0;
            this.show_edit = 0;
            this.show_add = 1;
        }

        protected void btnNewrecord_Click(object sender, ImageClickEventArgs e)
        {
            this.Session["x_msg"] = "";
            this.show_inv = 0;
            this.show_edit = 0;
            this.show_add = 1;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Session["x_msg"] = "";
            this.lt_x = this.ret.getFee_listByMerchant(this.ddl_merchants.SelectedValue);
            this.Session["grand_tot_cnt"] = this.lt_x.Count;
            this.lt_cdl = this.ret.getMerchantCatList(this.ddl_merchants.SelectedValue);
            foreach (XObjs.MerchantCatList list in this.lt_cdl)
            {
                ListItem item = new ListItem {
                    Value = list.cat_name,
                    Text = list.cat_name
                };
                if (item.Value == "ag")
                {
                    item.Text = "Accreditations";
                }
                if (item.Value == "ds")
                {
                    item.Text = "Designs";
                }
                if (item.Value == "pt")
                {
                    item.Text = "Patents";
                }
                if (item.Value == "tm")
                {
                    item.Text = "Trademarks";
                }
                this.a_xcategory.Items.Add(item);
                this.e_xcategory.Items.Add(item);
            }
            if (this.lt_x.Count > 0)
            {
                this.x_cnt = this.lt_x.Count;
                this.Session["x_cnt"] = this.x_cnt;
                foreach (XObjs.Fee_list _list in this.lt_x)
                {
                    if (this.ddl_merchants.SelectedValue == "cld")
                    {
                        if (_list.xcategory == "ag")
                        {
                            _list.xcategory = "Accreditations";
                        }
                        if (_list.xcategory == "ds")
                        {
                            _list.xcategory = "Designs";
                        }
                        if (_list.xcategory == "pt")
                        {
                            _list.xcategory = "Patents";
                        }
                        if (_list.xcategory == "tm")
                        {
                            _list.xcategory = "Trademarks";
                        }
                    }
                }
                this.Session["lt_x"] = this.lt_x;
                this.gvX.DataSource = this.lt_x;
                this.gvX.DataBind();
                this.show_inv = 1;
                this.show_edit = 0;
                this.show_add = 0;
            }
        }

        protected void gvX_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvX.PageIndex = e.NewPageIndex;
            if (this.Session["lt_x"] != null)
            {
                this.lt_x.Clear();
                this.lt_x = (List<XObjs.Fee_list>) this.Session["lt_x"];
            }
            this.gvX.DataSource = this.lt_x;
            this.gvX.DataBind();
            if (this.Session["x_cnt"] != null)
            {
                this.x_cnt = Convert.ToInt32(this.Session["x_cnt"]);
            }
            this.show_inv = 1;
            this.show_edit = 0;
            this.show_add = 0;
        }

        protected void gvX_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow namingContainer;
            int rowIndex;
            this.Session["x_msg"] = "";
            this.Session["e_xid"] = "";
            if (e.CommandName == "TmEditClick")
            {
                namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                rowIndex = namingContainer.RowIndex;
                this.e_xid.Value = e.CommandArgument.ToString();
                this.Session["e_xid"] = this.e_xid.Value;
                string text = "";
                this.e_item_code.Text = this.gvX.Rows[rowIndex].Cells[1].Text;
                this.e_item.Text = this.gvX.Rows[rowIndex].Cells[2].Text;
                this.e_qt_code.Text = this.gvX.Rows[rowIndex].Cells[3].Text;
                this.e_init_amt.Text = this.gvX.Rows[rowIndex].Cells[4].Text;
                this.e_tech_amt.Text = this.gvX.Rows[rowIndex].Cells[5].Text;
                text = this.gvX.Rows[rowIndex].Cells[6].Text;
                foreach (ListItem item in this.e_xcategory.Items)
                {
                    if (item.Text == text)
                    {
                        item.Selected = true;
                    }
                }
                this.show_inv = 0;
                this.show_edit = 1;
                this.show_add = 0;
            }
            if (e.CommandName == "TmDeleteClick")
            {
                namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                rowIndex = namingContainer.RowIndex;
                int num2 = this.reg.deleteFee_list(e.CommandArgument.ToString());
                StringBuilder builder = new StringBuilder();
                if (num2 > 0)
                {
                    builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Success\" class=\"style2\" src=\"../images/check.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD DELETED SUCCESSFULLY!!</strong></td></tr>");
                }
                else
                {
                    builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Error\" class=\"style2\" src=\"../images/delete.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD NOT DELETED SUCCESSFULLY!!</strong></td></tr>");
                }
                this.Session["x_msg"] = builder.ToString();
                base.Response.Redirect("./m_items.aspx");
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
                this.lt_mdl = this.ret.getMerchantDropList();
                foreach (XObjs.MerchantDropList list in this.lt_mdl)
                {
                    ListItem item = new ListItem {
                        Value = list.code,
                        Text = list.cname
                    };
                    this.ddl_merchants.Items.Add(item);
                }
            }
        }
    }
}

