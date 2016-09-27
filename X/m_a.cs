namespace XPay.X
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class m_a : Page
    {
        protected TextBox a_cname;
        protected TextBox a_xemail;
        protected TextBox a_xmobile;
        protected TextBox a_xname;
        protected TextBox a_xpass;
        protected string adminID = "0";
        protected Button btnAddItem;
        protected Button btnBack;
        protected Button btnEditBack;
        protected Button btnEditItem;
        protected Button btnExportExcel;
        protected LinkButton btnNewRecLink;
        protected ImageButton btnNewrecord;
        private XObjs.Pwallet c_x = new XObjs.Pwallet();
        public string docpath = "";
        protected TextBox e_cname;
        protected HiddenField e_mem_id;
        protected TextBox e_xemail;
        protected HiddenField e_xid;
        protected TextBox e_xmobile;
        protected TextBox e_xname;
        protected TextBox e_xpass;
        public ExcelFuncs ef = new ExcelFuncs();
        protected HtmlForm form1;
        protected GridView gvX;
        protected HtmlHead Head1;
        public List<XObjs.Merchant> lt_m = new List<XObjs.Merchant>();
        public List<XObjs.Pwallet> lt_x = new List<XObjs.Pwallet>();
        protected XPay.Classes.Registration reg = new XPay.Classes.Registration();
        protected Retriever ret = new Retriever();
        protected int show_add = 0;
        protected int show_edit = 0;
        protected int show_inv = 0;
        protected int x_cnt = 0;
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            XObjs.XMember x = new XObjs.XMember {
                xname = this.a_xname.Text,
                cname = this.a_cname.Text,
                xreg_date = this.xreg_date,
                xvisible = "1",
                xsync = "0",
                xpassword = this.a_xpass.Text,
                nationality = "160",
                addressID = "0",
                sys_ID = ""
            };
            int num = this.reg.addXpayAdmin(x);
            this.c_x.xemail = this.a_xemail.Text;
            this.c_x.xmobile = this.a_xmobile.Text;
            this.c_x.xmemberID = num.ToString();
            this.c_x.xmembertype = "rx";
            this.c_x.xpass = this.a_xpass.Text;
            this.c_x.reg_date = this.xreg_date;
            num = this.reg.addPwallet(this.c_x);
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
            base.Response.Redirect("./m_a.aspx");
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
            if ((((this.Session["e_xid"] != null) && (this.Session["e_xid"].ToString() != "")) && (this.Session["e_mem_id"] != null)) && (this.Session["e_mem_id"].ToString() != ""))
            {
                this.c_x.xid = this.Session["e_xid"].ToString();
                this.c_x.xmemberID = this.Session["e_mem_id"].ToString();
                this.c_x.xpass = this.e_xpass.Text;
                this.c_x.xmobile = this.e_xmobile.Text;
                this.c_x.xemail = this.e_xemail.Text;
                XObjs.XMember member = new XObjs.XMember {
                    xname = this.e_xname.Text,
                    cname = this.e_cname.Text
                };
                int num = this.reg.updateAdmin(this.c_x.xid, this.c_x.xmemberID, this.c_x.xemail, this.c_x.xmobile, this.c_x.xpass, member.xname, member.cname);
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
                base.Response.Redirect("./m_a.aspx");
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
            if ((this.Session["lt_m"] != null) && (this.Session["lt_m"].ToString() != ""))
            {
                this.lt_m = (List<XObjs.Merchant>) this.Session["lt_m"];
            }
            this.gvX.DataSource = this.lt_m;
            this.gvX.DataBind();
            this.show_inv = 0;
            string docpath = this.docpath;
            this.docpath = docpath + "Admin-" + this.adminID + "_" + DateTime.Now.ToLongTimeString().Replace(" ", "_").Replace(":", "_") + ".xls";
            this.ef.CreateMemberExcel(this, this.lt_m, this.docpath, "Admin Details");
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

        protected void gvX_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvX.PageIndex = e.NewPageIndex;
            if (this.Session["lt_m"] != null)
            {
                this.lt_m.Clear();
                this.lt_m = (List<XObjs.Merchant>) this.Session["lt_m"];
            }
            this.gvX.DataSource = this.lt_m;
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
            this.Session["e_mem_id"] = "";
            if (e.CommandName == "TmEditClick")
            {
                namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                rowIndex = namingContainer.RowIndex;
                this.e_xid.Value = e.CommandArgument.ToString();
                this.Session["e_xid"] = this.e_xid.Value;
                this.e_mem_id.Value = this.gvX.Rows[rowIndex].Cells[8].Text;
                this.Session["e_mem_id"] = this.e_mem_id.Value;
                this.e_xname.Text = this.gvX.Rows[rowIndex].Cells[2].Text;
                this.e_cname.Text = this.gvX.Rows[rowIndex].Cells[3].Text;
                this.e_xemail.Text = this.gvX.Rows[rowIndex].Cells[4].Text;
                this.e_xmobile.Text = this.gvX.Rows[rowIndex].Cells[5].Text;
                this.e_xpass.Text = this.gvX.Rows[rowIndex].Cells[9].Text;
                this.show_inv = 0;
                this.show_edit = 1;
                this.show_add = 0;
            }
            if (e.CommandName == "TmDeleteClick")
            {
                namingContainer = (GridViewRow) ((ImageButton) e.CommandSource).NamingContainer;
                rowIndex = namingContainer.RowIndex;
                int num2 = this.reg.updateDeleteXAdmin(e.CommandArgument.ToString());
                StringBuilder builder = new StringBuilder();
                if (num2 > 0)
                {
                    builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Success\" class=\"style2\" src=\"../images/check.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD ARCHIVED SUCCESSFULLY!!</strong></td></tr>");
                }
                else
                {
                    builder.Append("<tr><td style=\"text-align:center;\" colspan=\"2\"><img alt=\"Error\" class=\"style2\" src=\"../images/delete.png\"  width=\"48px\" height=\"48px\" /><br /><strong>RECORD NOT ARCHIVED SUCCESSFULLY!!</strong></td></tr>");
                }
                this.Session["x_msg"] = builder.ToString();
                base.Response.Redirect("./m_a.aspx");
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
                this.lt_x = this.ret.getPwalletByMemberType("rx");
                this.Session["grand_tot_cnt"] = this.lt_x.Count;
                if (this.lt_x.Count > 0)
                {
                    foreach (XObjs.Pwallet pwallet in this.lt_x)
                    {
                        XObjs.Merchant item = new XObjs.Merchant {
                            xid = pwallet.xid,
                            xmembertype = pwallet.xmembertype,
                            xmemberID = pwallet.xmemberID,
                            xemail = pwallet.xemail,
                            xmobile = pwallet.xmobile,
                            xpass = pwallet.xpass,
                            reg_date = pwallet.reg_date
                        };
                        XObjs.XMember member = new XObjs.XMember();
                        member = this.ret.getAdminByID(item.xmemberID);
                        item.xname = member.xname;
                        item.cname = member.cname;
                        item.xpassword = pwallet.xpass;
                        item.nationality = member.nationality;
                        item.addressID = member.addressID;
                        item.sys_ID = member.sys_ID;
                        item.xvisible = member.xvisible;
                        item.xsync = member.xsync;
                        item.xreg_date = member.xreg_date;
                        if (item.xvisible == "1")
                        {
                            this.lt_m.Add(item);
                        }
                        this.x_cnt = this.lt_m.Count;
                        this.Session["x_cnt"] = this.x_cnt;
                    }
                    this.Session["lt_m"] = this.lt_m;
                    this.gvX.DataSource = this.lt_m;
                    this.gvX.DataBind();
                    this.gvX.Columns[8].Visible = false;
                    this.gvX.Columns[9].Visible = false;
                    this.show_inv = 1;
                    this.show_edit = 0;
                    this.show_add = 0;
                }
            }
        }
    }
}

