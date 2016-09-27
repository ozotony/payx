namespace XPay.X
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class m_struc : Page
    {
        protected DropDownList a_p_type;
        protected DropDownList a_r_type;
        protected DropDownList a_xpartner;
        protected TextBox a_xratio;
        protected string adminID = "13";
        protected Button btnAddItem;
        protected Button btnBack;
        protected Button btnEditBack;
        protected Button btnEditItem;
        protected Button btnExportExcel;
        protected LinkButton btnNewRecLink;
        protected ImageButton btnNewrecord;
        protected Button btnSearch;
        protected XObjs.XMember c_member = new XObjs.XMember();
        protected XObjs.PRatio c_x = new XObjs.PRatio();
        protected DropDownList ddl_merchants;
        public string docpath = "";
        protected DropDownList e_p_type;
        protected DropDownList e_r_type;
        protected HiddenField e_xid;
        protected DropDownList e_xpartner;
        protected TextBox e_xratio;
        public ExcelFuncs ef = new ExcelFuncs();
        protected HtmlForm form1;
        protected GridView gvX;
        protected HtmlHead Head1;
        public List<XObjs.MerchantDropList> lt_mdl = new List<XObjs.MerchantDropList>();
        public List<XObjs.Pwallet> lt_p = new List<XObjs.Pwallet>();
        public List<XObjs.PRatio> lt_x = new List<XObjs.PRatio>();
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected int show_add = 0;
        protected int show_edit = 0;
        protected int show_inv = 0;
        protected int x_cnt = 0;
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            this.c_x.xpartnerID = this.a_xpartner.Text;
            this.c_x.p_type = this.a_p_type.SelectedValue;
            if (this.a_r_type.SelectedValue == "fee_list")
            {
                this.c_x.xratio = "0";
            }
            else
            {
                this.c_x.xratio = this.a_xratio.Text;
            }
            this.c_x.r_type = this.a_r_type.SelectedValue;
            this.c_x.xreg_date = this.xreg_date;
            this.c_x.xvisible = "1";
            this.c_x.xsync = this.ddl_merchants.SelectedValue;
            int num = this.reg.addPRatio(this.c_x);
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
            base.Response.Redirect("./m_struc.aspx");
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
                this.c_x.xpartnerID = this.e_xpartner.Text;
                this.c_x.p_type = this.e_p_type.SelectedValue;
                if (this.e_r_type.SelectedValue == "fee_list")
                {
                    this.c_x.xratio = "0";
                }
                else
                {
                    this.c_x.xratio = this.e_xratio.Text;
                }
                this.c_x.xratio = this.e_xratio.Text;
                this.c_x.r_type = this.e_r_type.SelectedValue;
                this.c_x.xreg_date = this.xreg_date;
                this.c_x.xvisible = "1";
                this.c_x.xsync = this.ddl_merchants.SelectedValue;
                int num = this.reg.updatePRatio(this.c_x);
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
                base.Response.Redirect("./m_struc.aspx");
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
            if ((this.Session["x_cnt"] != null) && (this.Session["x_cnt"].ToString() != ""))
            {
                this.x_cnt = Convert.ToInt32(this.Session["x_cnt"]);
            }
            if ((this.Session["lt_x"] != null) && (this.Session["lt_x"].ToString() != ""))
            {
                this.lt_x = (List<XObjs.PRatio>) this.Session["lt_x"];
            }
            this.gvX.DataSource = this.lt_x;
            this.gvX.DataBind();
            this.show_inv = 0;
            string docpath = this.docpath;
            this.docpath = docpath + "Partner-" + this.adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";
            this.ef.CreateStructureExcel(this, this.lt_x, this.docpath, "Partner Details");
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
            this.lt_x = this.ret.getPratioByMerchant(this.ddl_merchants.SelectedValue);
            this.Session["grand_tot_cnt"] = this.lt_x.Count;
            if (this.lt_x.Count > 0)
            {
                this.x_cnt = this.lt_x.Count;
                this.Session["x_cnt"] = this.x_cnt;
                foreach (XObjs.PRatio ratio in this.lt_x)
                {
                    XObjs.Pwallet pwallet = this.ret.getPwalletByID(ratio.xpartnerID);
                    XObjs.XPartner partner = this.ret.getPartnerByID(pwallet.xmemberID);
                    ratio.xpartnerID = partner.xname;
                    ratio.xvisible = partner.cname;
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
                this.lt_x = (List<XObjs.PRatio>) this.Session["lt_x"];
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
                this.e_xratio.Text = this.gvX.Rows[rowIndex].Cells[3].Text;
                foreach (ListItem item in this.e_xpartner.Items)
                {
                    if (item.Value == this.gvX.Rows[rowIndex].Cells[1].Text)
                    {
                        item.Selected = true;
                    }
                }
                foreach (ListItem item in this.e_p_type.Items)
                {
                    if (item.Value == this.gvX.Rows[rowIndex].Cells[2].Text)
                    {
                        item.Selected = true;
                    }
                }
                foreach (ListItem item in this.e_r_type.Items)
                {
                    if (item.Value == this.gvX.Rows[rowIndex].Cells[4].Text)
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
                int num2 = this.reg.deletePRatio(e.CommandArgument.ToString());
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
                base.Response.Redirect("./m_struc.aspx");
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
                this.lt_p = this.ret.getPwalletByMemberType("rp");
                foreach (XObjs.Pwallet pwallet in this.lt_p)
                {
                    ListItem item = new ListItem();
                    ListItem item2 = new ListItem();
                    item.Value = pwallet.xid;
                    item.Text = this.ret.getPartnerByID(pwallet.xmemberID).xname;
                    this.a_xpartner.Items.Add(item);
                    item2.Value = pwallet.xid;
                    item2.Text = this.ret.getPartnerByID(pwallet.xmemberID).xname;
                    this.e_xpartner.Items.Add(item2);
                }
                this.lt_mdl = this.ret.getMerchantDropList();
                foreach (XObjs.MerchantDropList list in this.lt_mdl)
                {
                    ListItem item3 = new ListItem {
                        Value = list.code,
                        Text = list.cname
                    };
                    this.ddl_merchants.Items.Add(item3);
                }
            }
        }
    }
}

