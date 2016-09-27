namespace XPay.svs
{
    using System;
    using System.ComponentModel;
    using System.Web.Services;
    using XPay.Classes;

    [WebService(Namespace="http://xpay.cldng.com/xpay/"), WebServiceBinding(ConformsTo=WsiProfiles.BasicProfile1_1), ToolboxItem(false)]
    public class agt_mgr : WebService
    {
        private XPay.Classes.Registration reg = new XPay.Classes.Registration();
        private XObjs.Address xaddy = new XObjs.Address();
        private XObjs.XAgent xmem = new XObjs.XAgent();
        private XObjs.Pwallet xpwallet = new XObjs.Pwallet();
        protected string xreg_date = DateTime.Now.ToString("yyyy-MM-dd");
        protected string xsync = "0";
        protected string xvisible = "1";

        [WebMethod(EnableSession=true)]
        public string Agt_imp(string name, string cname, string code, string nationality, string a_country, string a_state, string a_city, string a_street, string a_tel, string a_mail, string pw)
        {
            string str = "0";
            int num = 0;
            int num2 = 0;
            if ((((((name == "") || (cname == "")) || ((code == "") || (nationality == ""))) || (((a_country == "") || (a_state == "")) || ((a_city == "") || (a_street == "")))) || ((a_tel == "") || (a_mail == ""))) || (pw == ""))
            {
                return "0";
            }
            if ((((((name != "") || (cname != "")) || ((code != "") || (nationality != ""))) || (((a_country != "") || (a_state != "")) || ((a_city != "") || (a_street != "")))) || ((a_tel != "") || (a_mail != ""))) || (pw != ""))
            {
                this.xaddy.city = a_city;
                this.xaddy.countryID = a_country;
                this.xaddy.email1 = a_mail;
                this.xaddy.lgaID = "0";
                this.xaddy.log_staff = "0";
                this.xaddy.reg_date = this.xreg_date;
                this.xaddy.stateID = a_state;
                this.xaddy.street = a_street;
                this.xaddy.telephone1 = a_tel;
                this.xaddy.visible = this.xvisible;
                this.xaddy.zip = "";
                this.xaddy.xsync = this.xsync;
                num = this.reg.addXpayAddress(this.xaddy);
                if (num <= 0)
                {
                    return str;
                }
                this.xmem.xname = name;
                this.xmem.cname = cname;
                this.xmem.xreg_date = this.xreg_date;
                this.xmem.xvisible = this.xvisible;
                this.xmem.xsync = this.xsync;
                this.xmem.xpassword = pw;
                this.xmem.nationality = nationality;
                this.xmem.addressID = num.ToString();
                this.xmem.sys_ID = code;
                num2 = this.reg.addImpXpayAgent(this.xmem);
                if (num2 > 0)
                {
                    this.xpwallet.xemail = a_mail;
                    this.xpwallet.xmobile = a_tel;
                    this.xpwallet.xmemberID = num2.ToString();
                    this.xpwallet.xmembertype = "ra";
                    this.xpwallet.xpass = pw;
                    this.xpwallet.reg_date = this.xreg_date;
                    this.reg.addPwallet(this.xpwallet);
                    str = "1";
                }
            }
            return str;
        }
    }
}

