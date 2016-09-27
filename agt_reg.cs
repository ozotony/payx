namespace XPay
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using XPay.Classes;

    public partial class agt_reg : Page
    {
        private List<string> AllEmails = new List<string>();
        private List<string> AllMobiles = new List<string>();
        protected Button btnAddMember;
        protected TextBox cname;
        protected SqlDataSource ds_DefaultCountry;
        protected SqlDataSource ds_Nationality;
        protected SqlDataSource ds_State;
        protected HtmlForm form1;
        protected DropDownList nationality;
        protected string newState = "0";
        private XPay.Classes.Registration reg = new XPay.Classes.Registration();
        protected DropDownList residence;
        protected Retriever ret = new Retriever();
        protected string state_row = "0";
        private Validator val = new Validator();
        protected TextBox xaddress;
        private XObjs.Address xaddy = new XObjs.Address();
        protected TextBox xcity;
        protected TextBox xcode;
        protected TextBox xemail;
        private XObjs.XAgent xmem = new XObjs.XAgent();
        protected TextBox xname;
        protected TextBox xpass;
        private XObjs.Pwallet xpwallet = new XObjs.Pwallet();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected DropDownList xselectState;
        protected string xsync = "0";
        protected TextBox xtelephone;
        protected string xvisible = "1";

        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            if (((((this.xname.Text == "") || (this.cname.Text == "")) || ((this.xcity.Text == "") || (this.xaddress.Text == ""))) || (((this.xtelephone.Text == "") || (this.xemail.Text == "")) || (this.xpass.Text == ""))) || (this.xcode.Text == ""))
            {
                if (this.xname.Text == "")
                {
                    this.xname.BorderColor = Color.Red;
                }
                else
                {
                    this.xname.BorderColor = Color.Green;
                }
                if (this.cname.Text == "")
                {
                    this.cname.BorderColor = Color.Red;
                }
                else
                {
                    this.cname.BorderColor = Color.Green;
                }
                if (this.xcity.Text == "")
                {
                    this.xcity.BorderColor = Color.Red;
                }
                else
                {
                    this.xcity.BorderColor = Color.Green;
                }
                if (this.xaddress.Text == "")
                {
                    this.xaddress.BorderColor = Color.Red;
                }
                else
                {
                    this.xaddress.BorderColor = Color.Green;
                }
                if (this.xtelephone.Text == "")
                {
                    this.xtelephone.BorderColor = Color.Red;
                }
                else
                {
                    this.xtelephone.BorderColor = Color.Green;
                }
                if (this.xemail.Text == "")
                {
                    this.xemail.BorderColor = Color.Red;
                }
                else
                {
                    this.xemail.BorderColor = Color.Green;
                }
                if (this.xpass.Text == "")
                {
                    this.xpass.BorderColor = Color.Red;
                }
                else
                {
                    this.xpass.BorderColor = Color.Green;
                }
                if (this.xcode.Text == "")
                {
                    this.xcode.BorderColor = Color.Red;
                }
                else
                {
                    this.xcode.BorderColor = Color.Green;
                }
                this.btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE TO FILL IN ALL THE ENTRIES MARKED WITH A RED STAR!!'); </script>");
            }
            else if (((((this.xname.Text != "") && (this.cname.Text != "")) && ((this.xcity.Text != "") && (this.xaddress.Text != ""))) && (((this.xtelephone.Text != "") && (this.xemail.Text != "")) && (this.xpass.Text != ""))) && (this.xcode.Text != ""))
            {
                int num = 0;
                if (this.btnAddMember.Text == "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT")
                {
                    if (!this.doCaptcha())
                    {
                        this.btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                    }
                    else
                    {
                        this.btnAddMember.Text = "REGISTER";
                    }
                }
                else
                {
                    num += this.val.IsValidMobile(this.xtelephone.Text);
                    num += this.val.IsValidEmail(this.xemail.Text);
                    if (num > 0)
                    {
                        this.btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                        base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE THAT THE E-MAIL ADDRESS AND MOBILE NUMBER ARE IN THE CORRECT FORMAT!!'); </script>");
                    }
                    else
                    {
                        bool flag = this.AllMobiles.Contains(this.xtelephone.Text);
                        bool flag2 = this.AllEmails.Contains(this.xemail.Text);
                        if (!flag && !flag2)
                        {
                            int num2 = 0;
                            int num3 = 0;
                            this.xaddy.city = this.xcity.Text;
                            this.xaddy.countryID = this.residence.SelectedValue;
                            this.xaddy.email1 = this.xemail.Text;
                            this.xaddy.lgaID = "0";
                            this.xaddy.log_staff = "0";
                            this.xaddy.reg_date = this.xreg_date;
                            this.xaddy.stateID = this.xselectState.SelectedValue;
                            this.xaddy.street = this.xaddress.Text;
                            this.xaddy.telephone1 = this.xtelephone.Text;
                            this.xaddy.visible = this.xvisible;
                            this.xaddy.zip = "";
                            this.xaddy.xsync = this.xsync;
                            num3 = this.reg.addXpayAddress(this.xaddy);
                            if (num3 > 0)
                            {
                                this.xmem.xname = this.xname.Text;
                                this.xmem.cname = this.cname.Text;
                                this.xmem.xreg_date = this.xreg_date;
                                this.xmem.xvisible = this.xvisible;
                                this.xmem.xsync = this.xsync;
                                this.xmem.xpassword = this.xpass.Text;
                                this.xmem.nationality = this.nationality.SelectedValue;
                                this.xmem.addressID = num3.ToString();
                                this.xmem.sys_ID = "";
                                num2 = this.reg.addXpayAgent(this.xmem);
                                if (num2 > 0)
                                {
                                    this.xpwallet.xemail = this.xemail.Text;
                                    this.xpwallet.xmobile = this.xtelephone.Text;
                                    this.xpwallet.xmemberID = num2.ToString();
                                    this.xpwallet.xmembertype = "ra";
                                    this.xpwallet.xpass = this.xpass.Text;
                                    this.xpwallet.reg_date = this.xreg_date;
                                    this.reg.addPwallet(this.xpwallet);
                                    this.xname.BorderColor = Color.White;
                                    this.cname.BorderColor = Color.White;
                                    this.xcity.BorderColor = Color.White;
                                    this.xaddress.BorderColor = Color.White;
                                    this.xtelephone.BorderColor = Color.White;
                                    this.xemail.BorderColor = Color.White;
                                    this.xpass.BorderColor = Color.White;
                                    this.xcode.BorderColor = Color.White;
                                    Email email = new Email();
                                    Messenger messenger = new Messenger();
                                    string str8 = (("Dear " + this.xname.Text + ",<br/>") + "You have been registered on the CLD Platform!Please store the details below <br/>") + " Username: " + this.xemail.Text + "<br/>";
                                    string msg = str8 + "Password: " + this.xpass.Text + "<br/>System ID :CLD/RA/" + num2.ToString().PadLeft(5, '0') + ",<br/>Regards";
                                    str8 = (("Dear " + this.xname.Text + ",\r\n") + "You have been registered on the CLD Platform!Please store the details below\r\n") + "Username: " + this.xemail.Text + ";\r\n";
                                    string s = str8 + "Password:\r\n" + this.xpass.Text + ";\r\nSystem ID :CLD/RA/" + num2.ToString().PadLeft(5, '0') + ",\r\nRegards";
                                    string subject = "CLD REGISTRATION";
                                    string from = "admin@cldng.com";
                                    string text = this.xemail.Text;
                                    string sendto = this.xtelephone.Text;
                                    s = base.Server.UrlEncode(s);
                                    if (sendto.StartsWith("0"))
                                    {
                                        sendto = "234" + sendto.Remove(0, 1);
                                    }
                                    email.sendMail("CLD REGISTRATION", text, from, subject, msg, "");
                                    if (messenger.send_sms(s, "CLD REG.", sendto) == "The remote name could not be resolved: 'www.smslive247.com'")
                                    {
                                        base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('PLEASE BE SURE THAT THERE IS AN INTERNET CONNECTION!!'); </script>");
                                    }
                                    base.Response.Redirect("agt_reg_succ.aspx?x=" + this.xname.Text.ToUpper() + "&m=CLD/RA/" + num2.ToString().PadLeft(5, '0'));
                                }
                            }
                        }
                        else
                        {
                            this.btnAddMember.Text = "I CONFIRM THAT THE ABOVE ENTRIES ARE CORRECT";
                            if (flag2)
                            {
                                base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('THE E-MAIL ADDRESS ALREADY EXISTS ON THE SYSTEM'); </script>");
                            }
                            if (flag)
                            {
                                base.Response.Write("<script language=JavaScript  type='text/javascript'>alert('THE MOBILE NUMBER ALREADY EXISTS ON THE SYSTEM'); </script>");
                            }
                        }
                    }
                }
            }
        }

        protected bool doCaptcha()
        {
            bool flag = false;
            string str = "";
            if (this.Session["Captcha"] != null)
            {
                str = this.Session["Captcha"].ToString();
            }
            if (str == this.xcode.Text.ToUpper())
            {
                flag = true;
                this.newState = "0";
                return flag;
            }
            flag = false;
            this.newState = "1";
            this.xcode.Text = "";
            return flag;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.nationality.SelectedIndex = 0x9f;
                this.residence.SelectedIndex = 0x9f;
            }
            this.AllEmails = this.ret.getAllEmails();
            this.AllMobiles = this.ret.getAllMobileNumbers();
        }

        protected void residence_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.residence.SelectedIndex != 0x9f)
            {
                this.state_row = "1";
            }
            else
            {
                this.state_row = "0";
            }
        }
    }
}

