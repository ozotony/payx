namespace XPay.Classes
{
    using PayX.Classes;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class Retriever
    {
        private Helpers hf = new Helpers();

        public XObjs.Pwallet a_xadminz(string uname, string xpass)
        {
            string str = hf.ConnectXpay();
            XObjs.Pwallet pwallet = new XObjs.Pwallet();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select * from pwallet WHERE  xemail='" + uname + "' AND xpass='" + xpass + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Pwallet pwallet2 = new XObjs.Pwallet();
                pwallet.xid = reader["xid"].ToString();
                pwallet.xmembertype = reader["xmembertype"].ToString();
                pwallet.xmemberID = reader["xmemberID"].ToString();
                pwallet.xemail = reader["xemail"].ToString();
                pwallet.xmobile = reader["xmobile"].ToString();
                pwallet.xpass = reader["xpass"].ToString();
                pwallet.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return pwallet;
        }

        public string addAdminLog(string adminID, string ip_addy, string remote_host, string remote_user, string server_name, string server_url)
        {
            string str = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str2 = DateTime.Now.ToLongTimeString();
            string connectionString = hf.ConnectXpay();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO admin_lg (adminID,ip_addy,remote_host,remote_user,server_name,server_url,log_date,log_time) VALUES (@adminID,@ip_addy,@remote_host,@remote_user,@server_name,@server_url,@log_date,@log_time) SELECT SCOPE_IDENTITY()";
            connection.Open();command.CommandTimeout = 0;
            command.Parameters.Add("@adminID", SqlDbType.VarChar, 200);
            command.Parameters.Add("@ip_addy", SqlDbType.Text);
            command.Parameters.Add("@remote_host", SqlDbType.Text);
            command.Parameters.Add("@remote_user", SqlDbType.Text);
            command.Parameters.Add("@server_name", SqlDbType.Text);
            command.Parameters.Add("@server_url", SqlDbType.Text);
            command.Parameters.Add("@log_date", SqlDbType.VarChar, 200);
            command.Parameters.Add("@log_time", SqlDbType.VarChar, 200);
            command.Parameters["@adminID"].Value = adminID;
            command.Parameters["@ip_addy"].Value = ip_addy;
            command.Parameters["@remote_host"].Value = remote_host;
            command.Parameters["@remote_user"].Value = remote_user;
            command.Parameters["@server_name"].Value = server_name;
            command.Parameters["@server_url"].Value = server_url;
            command.Parameters["@log_date"].Value = str;
            command.Parameters["@log_time"].Value = str2;
            str4 = command.ExecuteScalar().ToString();
            connection.Close();
            return str4;
        }

        public XObjs.Address getAddressByID(string xid)
        {
            XObjs.Address address = new XObjs.Address();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + xid + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                address.ID = reader["ID"].ToString();
                address.city = reader["city"].ToString();
                address.countryID = reader["countryID"].ToString();
                address.email1 = reader["email1"].ToString();
                address.email2 = reader["email2"].ToString();
                address.lgaID = reader["lgaID"].ToString();
                address.log_staff = reader["log_staff"].ToString();
                address.reg_date = reader["reg_date"].ToString();
                address.stateID = reader["stateID"].ToString();
                address.street = hf.ConvertTab2Apos(reader["street"].ToString());
                address.telephone1 = reader["telephone1"].ToString();
                address.telephone2 = reader["telephone2"].ToString();
                address.visible = reader["visible"].ToString();
                address.xsync = reader["xsync"].ToString();
                address.zip = reader["zip"].ToString();
            }
            reader.Close();
            return address;
        }

        public XObjs.XMember getAdminByID(string xid)
        {
            XObjs.XMember member = new XObjs.XMember();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xadmin WHERE xid='" + xid + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                member.xid = reader["xid"].ToString();
                member.xname = reader["xname"].ToString();
                member.cname = reader["cname"].ToString();
                member.xpassword = reader["xpassword"].ToString();
                member.nationality = reader["nationality"].ToString();
                member.addressID = reader["addressID"].ToString();
                member.sys_ID = reader["sys_ID"].ToString();
                member.xreg_date = reader["xreg_date"].ToString();
                member.xvisible = reader["xvisible"].ToString();
                member.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return member;
        }

        public XObjs.XAgent getAgentByID(string xid)
        {
            XObjs.XAgent agent = new XObjs.XAgent();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xagent WHERE xid='" + xid + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                agent.xid = reader["xid"].ToString();
                agent.xname = hf.ConvertTab2Apos(reader["xname"].ToString());
                agent.cname = hf.ConvertTab2Apos(reader["cname"].ToString());
                agent.xpassword = reader["xpassword"].ToString();
                agent.nationality = reader["nationality"].ToString();
                agent.addressID = reader["addressID"].ToString();
                agent.sys_ID = reader["sys_ID"].ToString();
                agent.xreg_date = reader["xreg_date"].ToString();
                agent.xvisible = reader["xvisible"].ToString();
                agent.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return agent;
        }

        public string getAgentLogDetails(string uname, string xpass)
        {
            string str = "0";
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("select xid from registrations WHERE  Email='" + uname + "' AND xpassword LIKE'%" + xpass + "%' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xid"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<string> getAllEmails()
        {
            List<string> list = new List<string>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT email1 FROM address", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                string item = reader["email1"].ToString();
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<XObjs.Fee_list> getAllFee_list()
        {
            List<XObjs.Fee_list> list = new List<XObjs.Fee_list>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Fee_list item = new XObjs.Fee_list {
                    xid = reader["xid"].ToString(),
                    init_amt = reader["init_amt"].ToString(),
                    item = reader["item"].ToString(),
                    item_code = reader["item_code"].ToString(),
                    qt_code = reader["qt_code"].ToString(),
                    tech_amt = reader["tech_amt"].ToString(),
                    xcategory = reader["xcategory"].ToString(),
                    xdesc = reader["xdesc"].ToString(),
                    xlogstaff = reader["xlogstaff"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xsync = reader["xsync"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<string> getAllMobileNumbers()
        {
            List<string> list = new List<string>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT telephone1 FROM address", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                string item = reader["telephone1"].ToString();
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public int getAllPaidFee_detail_ItemsCntByCat(string memberID, string cat, string xmembertype)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(hwallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid  INNER JOIN twallet ON  twallet.xid=fee_details.twalletID  INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xmembertype='" + xmembertype + "' AND twallet.xpay_status='1' AND twallet.xgt<>'xpay' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return num;
        }

        public int getCountTrans(string cat)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(InterSwitchPostFields.txn_ref) as cnt from InterSwitchPostFields   where txn_ref='" + cat + "'  ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return num;
        }

        public int getMaxSysId()
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("select  max(convert(int,substring(Sys_ID,8,LEN(Sys_ID))) ) as max_sysid from  registrations ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["max_sysid"]);
            }
            reader.Close();
            return num;
        }

        public List<XObjs.Registration> getAllRegistrations()
        {
            List<XObjs.Registration> list = new List<XObjs.Registration>();
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations  ", connection);
            connection.Open(); 
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Registration item = new XObjs.Registration {
                    xid = reader["xid"].ToString(),
                    AccrediationType = reader["AccrediationType"].ToString(),
                    Sys_ID = reader["Sys_ID"].ToString(),
                    Firstname = hf.ConvertTab2Apos(reader["Firstname"].ToString()),
                    Surname = hf.ConvertTab2Apos(reader["Surname"].ToString()),
                    Email = hf.ConvertTab2Apos(reader["Email"].ToString()),
                    xpassword = reader["xpassword"].ToString(),
                    DateOfBrith = reader["DateOfBrith"].ToString(),
                    IncorporatedDate = reader["IncorporatedDate"].ToString(),
                    Nationality = reader["Nationality"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    CompanyName = reader["CompanyName"].ToString(),
                    CompanyAddress = reader["CompanyAddress"].ToString(),
                    ContactPerson = reader["ContactPerson"].ToString(),
                    ContactPersonPhone = reader["ContactPersonPhone"].ToString(),
                    CompanyWebsite = reader["CompanyWebsite"].ToString(),
                    Certificate = reader["Certificate"].ToString(),
                    Introduction = reader["Introduction"].ToString(),
                    Principal = reader["Principal"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xstatus = reader["xstatus"].ToString(),
                    xvisible = reader["xvisible"].ToString(),
                    xsync = reader["xsync"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<XObjs.Subagent> getAllSubAgents()
        {
            List<XObjs.Subagent> list = new List<XObjs.Subagent>();
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Subagent item = new XObjs.Subagent {
                    xid = reader["xid"].ToString(),
                    RegistrationID = reader["RegistrationID"].ToString(),
                    Surname = hf.ConvertTab2Apos(reader["Surname"].ToString()),
                    Firstname = hf.ConvertTab2Apos(reader["Firstname"].ToString()),
                    Email = hf.ConvertTab2Apos(reader["Email"].ToString()),
                    xpassword = reader["xpassword"].ToString(),
                    Telephone = reader["Telephone"].ToString(),
                    AssignID = reader["AssignID"].ToString(),
                    Sys_ID = reader["Sys_ID"].ToString(),
                    Address = reader["Address"].ToString(),
                    AgentPassport = reader["AgentPassport"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xstatus = reader["xstatus"].ToString(),
                    xvisible = reader["xvisible"].ToString(),
                    xsync = reader["xsync"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public XObjs.Applicant getApplicantByID(string xid)
        {
            XObjs.Applicant applicant = new XObjs.Applicant();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE xid='" + xid + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                applicant.xid = reader["xid"].ToString();
                applicant.xname = hf.ConvertTab2Apos(reader["xname"].ToString());
                applicant.address = hf.ConvertTab2Apos(reader["address"].ToString());
                applicant.xemail = hf.ConvertTab2Apos(reader["xemail"].ToString());
                applicant.xmobile = reader["xmobile"].ToString();
            }
            reader.Close();
            return applicant;
        }

       
        public XObjs.XBanker getBankerByID(string xid)
        {
            XObjs.XBanker banker = new XObjs.XBanker();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xbanker WHERE xid='" + xid + "' ", connection);
            connection.Open(); 
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                banker.xid = reader["xid"].ToString();
                banker.xname = reader["xname"].ToString();
                banker.bankname = reader["bankname"].ToString();
                banker.xpassword = reader["xpassword"].ToString();
                banker.nationality = reader["nationality"].ToString();
                banker.addressID = reader["addressID"].ToString();
                banker.xposition = reader["xposition"].ToString();
                banker.sys_ID = reader["sys_ID"].ToString();
                banker.xreg_date = reader["xreg_date"].ToString();
                banker.xvisible = reader["xvisible"].ToString();
                banker.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return banker;
        }

        public int getCntTotalTransAdmin(string fromDate, string toDate)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(twallet.xid) as cnt from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["cnt"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getCntTotalTransAdminGraph(string year)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(twallet.xid) as cnt from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date LIKE '%" + year + "%' ", connection);
            connection.Open(); 
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["cnt"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getCombinedPaidFee_detail_ItemsCntByCat(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(DISTINCT twallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid  where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "'  AND twallet.xgt<>'xpay' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return num;
        }

        public int getFee_detailCntByCat(string memberID, string cat, string xmembertype)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(fee_details.xid) AS cnt from fee_list INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "'  AND twallet.xmembertype='" + xmembertype + "'  AND twallet.xgt<>'xpay' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["cnt"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public XObjs.Fee_details getFee_detailsByHwalletID(string hID)
        {
            XObjs.Fee_details _details = new XObjs.Fee_details();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select fee_details.* from fee_details where xid in (select hwallet.fee_detailsID from hwallet where hwallet.xid='" + hID + "') ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                _details.xid = reader["xid"].ToString();
                _details.fee_listID = reader["fee_listID"].ToString();
                _details.twalletID = reader["twalletID"].ToString();
                _details.xqty = reader["xqty"].ToString();
                _details.xused = reader["xused"].ToString();
                _details.tot_amt = reader["tot_amt"].ToString();
                _details.init_amt = reader["init_amt"].ToString();
                _details.tech_amt = reader["tech_amt"].ToString();
                _details.xreg_date = reader["xlogstaff"].ToString();
                _details.xreg_date = reader["xreg_date"].ToString();
                _details.xsync = reader["xsync"].ToString();
                _details.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return _details;
        }

        public XObjs.Fee_details getFee_detailsByID(string xid)
        {
            XObjs.Fee_details _details = new XObjs.Fee_details();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select * from  fee_details where xid='" + xid + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                _details.xid = reader["xid"].ToString();
                _details.fee_listID = reader["fee_listID"].ToString();
                _details.twalletID = reader["twalletID"].ToString();
                _details.xqty = reader["xqty"].ToString();
                _details.xused = reader["xused"].ToString();
                _details.tot_amt = reader["tot_amt"].ToString();
                _details.init_amt = reader["init_amt"].ToString();
                _details.tech_amt = reader["tech_amt"].ToString();
                _details.xreg_date = reader["xlogstaff"].ToString();
                _details.xreg_date = reader["xreg_date"].ToString();
                _details.xsync = reader["xsync"].ToString();
                _details.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return _details;
        }

        public XObjs.Fee_details getFee_detailsByID(string xid, string cat, string xmemberID)
        {
            XObjs.Fee_details _details = new XObjs.Fee_details();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select fee_details.*,fee_list.* from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid  INNER JOIN twallet ON  twallet.xid=fee_details.twalletID where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + xmemberID + "' AND twallet.xpay_status='1' AND xid='" + xid + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                _details.xid = reader["xid"].ToString();
                _details.fee_listID = reader["fee_listID"].ToString();
                _details.twalletID = reader["twalletID"].ToString();
                _details.xqty = reader["xqty"].ToString();
                _details.xused = reader["xused"].ToString();
                _details.tot_amt = reader["tot_amt"].ToString();
                _details.init_amt = reader["init_amt"].ToString();
                _details.tech_amt = reader["tech_amt"].ToString();
                _details.xreg_date = reader["xlogstaff"].ToString();
                _details.xreg_date = reader["xreg_date"].ToString();
                _details.xsync = reader["xsync"].ToString();
                _details.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return _details;
        }

        public List<XObjs.Fee_details> getFee_detailsByTwalletID(string twalletID)
        {
            List<XObjs.Fee_details> list = new List<XObjs.Fee_details>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from fee_details where twalletID='" + twalletID + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Fee_details item = new XObjs.Fee_details {
                    xid = reader["xid"].ToString(),
                    fee_listID = reader["fee_listID"].ToString(),
                    twalletID = reader["twalletID"].ToString(),
                    xqty = reader["xqty"].ToString(),
                    xused = reader["xused"].ToString(),
                    tot_amt = reader["tot_amt"].ToString(),
                    init_amt = reader["init_amt"].ToString(),
                    tech_amt = reader["tech_amt"].ToString(),
                    xreg_date = reader["xlogstaff"].ToString(),
                    xsync = reader["xsync"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<XObjs.Fee_list> getFee_listByCat(string cat)
        {
            List<XObjs.Fee_list> list = new List<XObjs.Fee_list>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list WHERE xcategory='" + cat + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Fee_list item = new XObjs.Fee_list {
                    xid = reader["xid"].ToString(),
                    init_amt = reader["init_amt"].ToString(),
                    item = reader["item"].ToString(),
                    item_code = reader["item_code"].ToString(),
                    qt_code = reader["qt_code"].ToString(),
                    tech_amt = reader["tech_amt"].ToString(),
                    xcategory = reader["xcategory"].ToString(),
                    xdesc = reader["xdesc"].ToString(),
                    xlogstaff = reader["xlogstaff"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xsync = reader["xsync"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public XObjs.Fee_list getFee_listByID(string xid)
        {
            XObjs.Fee_list _list = new XObjs.Fee_list();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list WHERE xid='" + xid + "' ORDER BY  xcategory DESC", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                _list.xid = reader["xid"].ToString();
                _list.init_amt = reader["init_amt"].ToString();
                _list.item = reader["item"].ToString();
                _list.item_code = reader["item_code"].ToString();
                _list.qt_code = reader["qt_code"].ToString();
                _list.tech_amt = reader["tech_amt"].ToString();
                _list.xcategory = reader["xcategory"].ToString();
                _list.xdesc = reader["xdesc"].ToString();
                _list.xlogstaff = reader["xlogstaff"].ToString();
                _list.xreg_date = reader["xreg_date"].ToString();
                _list.xsync = reader["xsync"].ToString();
                _list.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return _list;
        }

        public XObjs.Fee_list getFee_listByItemCode(string item_code)
        {
            XObjs.Fee_list _list = new XObjs.Fee_list();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list WHERE item_code='" + item_code + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                _list.xid = reader["xid"].ToString();
                _list.init_amt = reader["init_amt"].ToString();
                _list.item = reader["item"].ToString();
                _list.item_code = reader["item_code"].ToString();
                _list.qt_code = reader["qt_code"].ToString();
                _list.tech_amt = reader["tech_amt"].ToString();
                _list.xcategory = reader["xcategory"].ToString();
                _list.xdesc = reader["xdesc"].ToString();
                _list.xlogstaff = reader["xlogstaff"].ToString();
                _list.xreg_date = reader["xreg_date"].ToString();
                _list.xsync = reader["xsync"].ToString();
                _list.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return _list;
        }

        public List<XObjs.Fee_list> getFee_listByMerchant(string merchant)
        {
            List<XObjs.Fee_list> list = new List<XObjs.Fee_list>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM fee_list WHERE xsync='" + merchant + "' ORDER BY  xcategory DESC", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Fee_list item = new XObjs.Fee_list {
                    xid = reader["xid"].ToString(),
                    init_amt = reader["init_amt"].ToString(),
                    item = reader["item"].ToString(),
                    item_code = reader["item_code"].ToString(),
                    qt_code = reader["qt_code"].ToString(),
                    tech_amt = reader["tech_amt"].ToString(),
                    xcategory = reader["xcategory"].ToString(),
                    xdesc = reader["xdesc"].ToString(),
                    xlogstaff = reader["xlogstaff"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xsync = reader["xsync"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<XObjs.Hwallet> getHwalletByFee_detailsID(string fee_detailsID)
        {
            List<XObjs.Hwallet> list = new List<XObjs.Hwallet>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM hwallet WHERE fee_detailsID='" + fee_detailsID + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Hwallet item = new XObjs.Hwallet {
                    xid = reader["xid"].ToString(),
                    transID = reader["transID"].ToString(),
                    fee_detailsID = reader["fee_detailsID"].ToString(),
                    used_status = reader["used_status"].ToString(),
                    xreg_date = reader["xreg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public XObjs.Hwallet getHwalletByID(string xid)
        {
            XObjs.Hwallet hwallet = new XObjs.Hwallet();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM hwallet WHERE xid='" + xid + "' ", connection);
            connection.Open();
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                hwallet.xid = reader["xid"].ToString();
                hwallet.transID = reader["transID"].ToString();
                hwallet.fee_detailsID = reader["fee_detailsID"].ToString();
                hwallet.used_status = reader["used_status"].ToString();
                hwallet.xreg_date = reader["xreg_date"].ToString();
            }
            reader.Close();
            return hwallet;
        }

        public List<XObjs.Hwallet> getHwalletByTransID(string transID)
        {
            List<XObjs.Hwallet> list = new List<XObjs.Hwallet>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM hwallet WHERE transID='" + transID + "' ", connection);
            connection.Open(); 
            command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Hwallet item = new XObjs.Hwallet {
                    xid = reader["xid"].ToString(),
                    transID = reader["transID"].ToString(),
                    fee_detailsID = reader["fee_detailsID"].ToString(),
                    used_status = reader["used_status"].ToString(),
                    xreg_date = reader["xreg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        //public XObjs.InterSwitchPostFields getISWtransactionByTransactionID(string txnref)
        //{
        //    XObjs.InterSwitchPostFields fields = new XObjs.InterSwitchPostFields();
        //    SqlConnection connection = new SqlConnection(hf.ConnectXpay());
        //    SqlCommand command = new SqlCommand("SELECT * FROM InterSwitchPostFields WHERE txn_ref='" + txnref + "' AND InterSwitchPostFields.trans_status='00' ", connection);
        //    connection.Open(); command.CommandTimeout = 0;
        //    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
        //    while (reader.Read())
        //    {
        //        fields.xid = reader["xid"].ToString();
        //        fields.product_id = reader["product_id"].ToString();
        //        fields.amount = reader["amount"].ToString();
        //        fields.isw_conv_fee = reader["isw_conv_fee"].ToString();
        //        fields.currency = reader["currency"].ToString();
        //        fields.site_redirect_url = reader["site_redirect_url"].ToString();
        //        fields.txn_ref = reader["txn_ref"].ToString();
        //        fields.hash = reader["hash"].ToString();
        //        fields.mackey = reader["mackey"].ToString();
        //        fields.pay_item_id = reader["pay_item_id"].ToString();
        //        fields.site_name = reader["site_name"].ToString();
        //        fields.cust_id = reader["cust_id"].ToString();
        //        fields.cust_id_desc = reader["cust_id_desc"].ToString();
        //        fields.cust_name = reader["cust_name"].ToString();
        //        fields.resp_desc = reader["resp_desc"].ToString();
        //        fields.pay_item_name = reader["pay_item_name"].ToString();
        //        fields.local_date_time = reader["local_date_time"].ToString();
        //        fields.TransactionDate = reader["TransactionDate"].ToString();
        //        fields.MerchantReference = reader["MerchantReference"].ToString();
        //        fields.trans_status = reader["trans_status"].ToString();
        //        fields.pay_ref = reader["pay_ref"].ToString();
        //        fields.ret_ref = reader["ret_ref"].ToString();
        //        fields.xreg_date = reader["xreg_date"].ToString();
        //        fields.xvisible = reader["xvisible"].ToString();
        //        fields.xsync = reader["xsync"].ToString();
        //    }
        //    reader.Close();
        //    return fields;
        //}
        public XObjs.InterSwitchPostFields getISWtransactionByTransactionID(string txnref)
        {
            XObjs.InterSwitchPostFields fields = new XObjs.InterSwitchPostFields();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM InterSwitchPostFields WHERE txn_ref='" + txnref + "' order by xid desc ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                fields.xid = reader["xid"].ToString();
                fields.product_id = reader["product_id"].ToString();
                fields.amount = reader["amount"].ToString();
                fields.isw_conv_fee = reader["isw_conv_fee"].ToString();
                fields.currency = reader["currency"].ToString();
                fields.site_redirect_url = reader["site_redirect_url"].ToString();
                fields.txn_ref = reader["txn_ref"].ToString();
                fields.hash = reader["hash"].ToString();
                fields.mackey = reader["mackey"].ToString();
                fields.pay_item_id = reader["pay_item_id"].ToString();
                fields.site_name = reader["site_name"].ToString();
                fields.cust_id = reader["cust_id"].ToString();
                fields.cust_id_desc = reader["cust_id_desc"].ToString();
                fields.cust_name = reader["cust_name"].ToString();
                fields.resp_desc = reader["resp_desc"].ToString();
                fields.pay_item_name = reader["pay_item_name"].ToString();
                fields.local_date_time = reader["local_date_time"].ToString();
                fields.TransactionDate = reader["TransactionDate"].ToString();
                fields.MerchantReference = reader["MerchantReference"].ToString();
                fields.trans_status = reader["trans_status"].ToString();
                fields.pay_ref = reader["pay_ref"].ToString();
                fields.ret_ref = reader["ret_ref"].ToString();
                fields.xreg_date = reader["xreg_date"].ToString();
                fields.xvisible = reader["xvisible"].ToString();
                fields.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return fields;
        }
        public List<XObjs.InterSwitchPostFields> getISWtransactionBadRecords(string vtrans_id)
        {
          //  string cmd_string = "SELECT * FROM InterSwitchPostFields WHERE xreg_date >'2014-09-15' and trans_status='20031' ";
            string cmd_string = "SELECT * FROM InterSwitchPostFields WHERE txn_ref= '" + vtrans_id + "'    ";
            List<XObjs.InterSwitchPostFields> fields = new List<XObjs.InterSwitchPostFields>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand(cmd_string, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.InterSwitchPostFields x = new XObjs.InterSwitchPostFields();
                x.xid = reader["xid"].ToString();
                x.product_id = reader["product_id"].ToString();
                x.amount = (Convert.ToDecimal(reader["amount"])/100).ToString();
                x.vamount=reader["amount"].ToString();
                x.isw_conv_fee = reader["isw_conv_fee"].ToString();
                x.currency = reader["currency"].ToString();
                x.site_redirect_url = reader["site_redirect_url"].ToString();
                x.txn_ref = reader["txn_ref"].ToString();
                x.hash = reader["hash"].ToString();
                x.mackey = reader["mackey"].ToString();
                x.pay_item_id = reader["pay_item_id"].ToString();
                x.site_name = reader["site_name"].ToString();
                x.cust_id = reader["cust_id"].ToString();
                x.cust_id_desc = reader["cust_id_desc"].ToString();
                x.cust_name = reader["cust_name"].ToString();
                x.resp_desc = reader["resp_desc"].ToString();
                x.pay_item_name = reader["pay_item_name"].ToString();
                x.local_date_time = reader["local_date_time"].ToString();
                x.TransactionDate = reader["TransactionDate"].ToString();
                x.MerchantReference = reader["MerchantReference"].ToString();
                x.trans_status = reader["trans_status"].ToString();
                x.pay_ref = reader["pay_ref"].ToString();
                x.ret_ref = reader["ret_ref"].ToString();
                x.xreg_date = reader["xreg_date"].ToString();
                x.xvisible = reader["xvisible"].ToString();
                x.xsync = reader["xsync"].ToString();
                fields.Add(x);
            }
            reader.Close();
            return fields;
        }
        public string getLatestDate()
        {
            string str = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select top 1 twallet.xreg_date from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' order by twallet.xreg_date DESC ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xreg_date"].ToString();
            }
            reader.Close();
            return str.Substring(0, 4);
        }

        public XObjs.XMember getMemberByID(string xid)
        {
            XObjs.XMember member = new XObjs.XMember();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xmember WHERE xid='" + xid + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                member.xid = reader["xid"].ToString();
                member.xname = reader["xname"].ToString();
                member.cname = reader["cname"].ToString();
                member.xpassword = reader["xpassword"].ToString();
                member.nationality = reader["nationality"].ToString();
                member.addressID = reader["addressID"].ToString();
                member.sys_ID = reader["sys_ID"].ToString();
                member.xreg_date = reader["xreg_date"].ToString();
                member.xvisible = reader["xvisible"].ToString();
                member.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return member;
        }

        public string getMemberTypeByID(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT xmembertype from pwallet where xid='" + id + "'", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xmembertype"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<XObjs.MerchantCatList> getMerchantCatList(string code)
        {
            List<XObjs.MerchantCatList> list = new List<XObjs.MerchantCatList>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select DISTINCT(xcategory),xsync from fee_list where xsync='" + code + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.MerchantCatList item = new XObjs.MerchantCatList {
                    cat_name = reader["xcategory"].ToString(),
                    code = reader["xsync"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<XObjs.MerchantDropList> getMerchantDropList()
        {
            List<XObjs.MerchantDropList> list = new List<XObjs.MerchantDropList>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select xpartner.cname,p_ratio.xsync from xpartner INNER JOIN pwallet on xpartner.xid=pwallet.xmemberID INNER JOIN p_ratio on p_ratio.xpartnerID=pwallet.xid WHERE pwallet.xmembertype='rp' AND p_ratio.p_type='merchant' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.MerchantDropList item = new XObjs.MerchantDropList {
                    cname = reader["cname"].ToString(),
                    code = reader["xsync"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getOldestDate()
        {
            string str = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select top 1 twallet.xreg_date from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' order by twallet.xreg_date ASC ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xreg_date"].ToString();
            }
            reader.Close();
            return str.Substring(0, 4);
        }

        public int getPaidFee_detail_ItemsCntByCatBk(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(DISTINCT twallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "' AND twallet.xgt='xpay_bk' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return num;
        }

        public int getPaidFee_detail_ItemsCntByCatISW(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(DISTINCT twallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid INNER JOIN InterSwitchPostFields ON twallet.transID=InterSwitchPostFields.txn_ref  where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "' AND InterSwitchPostFields.xvisible='1' AND twallet.xgt<>'xpay' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return num;
        }

        public int getPaidFee_detail_ItemsCntByCatOld(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(hwallet.xid) as cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid  INNER JOIN twallet ON  twallet.xid=fee_details.twalletID  INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return num;
        }

        public int getPaidFee_detailCntByCat(string memberID, string cat, string xpaystatus, string xmembertype)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(fee_details.xid) AS cnt from fee_list INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xmembertype='" + xmembertype + "'   AND twallet.xgt<>'xpay' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["cnt"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getPaidUsedCntByCat(string memberID, string cat, string xpaystatus, string xmembertype, string used_status)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select count(fee_details.xid) AS cnt from fee_list  INNER JOIN fee_details ON fee_details.fee_listID=fee_list.xid INNER JOIN twallet ON  twallet.xid=fee_details.twalletID INNER JOIN hwallet ON hwallet.fee_detailsID=fee_details.xid where fee_list.xcategory='" + cat + "' AND twallet.xmemberID='" + memberID + "' AND twallet.xpay_status='" + xpaystatus + "' AND twallet.xgt<>'xpay'  AND twallet.xmembertype='" + xmembertype + "' AND hwallet.used_status='" + used_status + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return num;
        }

        public XObjs.XPartner getPartnerByCode(string xcode)
        {
            XObjs.XPartner partner = new XObjs.XPartner();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xpartner WHERE xsync='" + xcode + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                partner.xid = reader["xid"].ToString();
                partner.xname = reader["xname"].ToString();
                partner.cname = reader["cname"].ToString();
                partner.xpassword = reader["xpassword"].ToString();
                partner.nationality = reader["nationality"].ToString();
                partner.addressID = reader["addressID"].ToString();
                partner.sys_ID = reader["sys_ID"].ToString();
                partner.xreg_date = reader["xreg_date"].ToString();
                partner.xvisible = reader["xvisible"].ToString();
                partner.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return partner;
        }

        public XObjs.XPartner getPartnerByID(string xid)
        {
            XObjs.XPartner partner = new XObjs.XPartner();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM xpartner WHERE xid='" + xid + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                partner.xid = reader["xid"].ToString();
                partner.xname = reader["xname"].ToString();
                partner.cname = reader["cname"].ToString();
                partner.xpassword = reader["xpassword"].ToString();
                partner.nationality = reader["nationality"].ToString();
                partner.addressID = reader["addressID"].ToString();
                partner.sys_ID = reader["sys_ID"].ToString();
                partner.xreg_date = reader["xreg_date"].ToString();
                partner.xvisible = reader["xvisible"].ToString();
                partner.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return partner;
        }

        public List<XObjs.PartnerGrid> getPartnerGridMerchantList(string fromDate, string toDate)
        {
            List<XObjs.PartnerGrid> list = new List<XObjs.PartnerGrid>();
            int num = 1;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select twallet.xid,twallet.transID,twallet.xmemberID,twallet.xmembertype,twallet.xgt,twallet.ref_no,twallet.xbankerID,fee_details.fee_listID,fee_details.init_amt,fee_details.tech_amt,fee_details.tot_amt,fee_details.xqty,twallet.xreg_date from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' AND twallet.xgt<>'xpay' ORDER BY twallet.xid DESC ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.PartnerGrid item = new XObjs.PartnerGrid {
                    sn = num.ToString(),
                    xid = reader["xid"].ToString(),
                    transID = reader["transID"].ToString(),
                    xmemberID = reader["xmemberID"].ToString(),
                    xmembertype = reader["xmembertype"].ToString(),
                    xgt = reader["xgt"].ToString(),
                    ref_no = reader["ref_no"].ToString(),
                    xbankerID = reader["xbankerID"].ToString(),
                    fee_listID = reader["fee_listID"].ToString(),
                    init_amt = reader["init_amt"].ToString(),
                    tech_amt = reader["tech_amt"].ToString(),
                    tot_amt = reader["tot_amt"].ToString(),
                    xqty = reader["xqty"].ToString(),
                    xreg_date = reader["xreg_date"].ToString()
                };
                list.Add(item);
                num++;
            }
            reader.Close();
            return list;
        }

        public List<XObjs.ReportItem> getPaymentReportItemISW(string xcategory, string xpay_status,string fromDate, string toDate, string merchant_type)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            
                command_text += "select InterSwitchPostFields.amount as 'full_amt',fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
                command_text += " twallet.xid,twallet.applicantID,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
                command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID,InterSwitchPostFields.isw_conv_fee AS isw_amt ";
                command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
                command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
                command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";
                command_text += " left outer join InterSwitchPostFields on twallet.transID=InterSwitchPostFields.txn_ref ";
                if(xpay_status=="1")
                {  command_text += " WHERE ( InterSwitchPostFields.trans_status='00')  ";   }
                else 
                {       command_text += " WHERE ( InterSwitchPostFields.trans_status <> '00')  "; }
                
           
            if (xcategory != "all") { command_text += "AND (fee_list.xcategory='" + xcategory + "')  "; }

            command_text += " AND (SUBSTRING(InterSwitchPostFields.TransactionDate,1,10) BETWEEN '" + fromDate + "' AND '" + toDate + "') ";
            if(merchant_type!="")  {   command_text += " AND (fee_list.xsync='" + merchant_type + "') ";     }
            else
            { }
            
            command_text += "  ORDER BY twallet.xid DESC";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.full_amt = reader["full_amt"].ToString();
                x.applicantID = reader["applicantID"].ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.isw_amt = reader["isw_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }

        public List<XObjs.ReportItemGenISW> getPaymentReportItemGenISW(string xpay_status, string fromDate, string toDate, string merchant_type, string xorder)
        {
            List<XObjs.ReportItemGenISW> xlist = new List<XObjs.ReportItemGenISW>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());

            command_text += "select CONVERT(varchar, CAST( CAST(InterSwitchPostFields.amount AS int)*.01  as money),1) as 'full_amt', twallet.xid as 'twallet_xid', ";
          //  command_text += " SUBSTRING(InterSwitchPostFields.TransactionDate,1,19) as TransactionDate,twallet.transID,applicant.xname,applicant.address,applicant.xemail,applicant.xmobile,InterSwitchPostFields.isw_conv_fee AS isw_amt,fee_details.tech_amt *convert(int,fee_details.xqty) as tech_amt ,fee_details.xqty as  Qty ,fee_list.init_amt  as Cld_Fees,InterSwitchPostFields.pay_ref as pay_ref ,fee_list.item_code as item_code  ,fee_list.xdesc as xdesc  FROM InterSwitchPostFields ";
            command_text += " SUBSTRING(InterSwitchPostFields.TransactionDate,1,19) as TransactionDate,(hwallet.transid + '-' + hwallet.fee_detailsID + '-' + cast( hwallet.xid as varchar) ) as transid,applicant.xname,applicant.address,applicant.xemail,applicant.xmobile,InterSwitchPostFields.isw_conv_fee AS isw_amt,fee_details.tech_amt *convert(int,fee_details.xqty) as tech_amt ,fee_details.xqty as  Qty ,fee_list.init_amt  as Cld_Fees,InterSwitchPostFields.pay_ref as pay_ref ,fee_list.item_code as item_code  ,fee_list.xdesc as xdesc  FROM InterSwitchPostFields ";
            command_text += " LEFT OUTER JOIN twallet ON InterSwitchPostFields.txn_ref=twallet.transID  ";
            command_text += " LEFT OUTER JOIN hwallet ON hwallet.transID=twallet.transID  ";
            command_text += " LEFT OUTER JOIN applicant ON applicant.xid=twallet.applicantID ";

            command_text += " LEFT OUTER JOIN fee_details ON fee_details.twalletID=twallet.xid ";

          

            command_text += " LEFT OUTER JOIN fee_list ON fee_details.fee_listID=fee_list.xid ";
            if (xpay_status == "1")
            { command_text += " WHERE ( InterSwitchPostFields.trans_status='00')  "; }
            else
            { command_text += " WHERE ( InterSwitchPostFields.trans_status <> '00')  "; }

            command_text += " AND (SUBSTRING(InterSwitchPostFields.TransactionDate,1,10) BETWEEN '" + fromDate + "' AND '" + toDate + "') ";
           

            command_text += "  ORDER BY twallet.xid "+xorder;

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItemGenISW x = new XObjs.ReportItemGenISW();
                x.sn = sn.ToString();
                x.full_amt = reader["full_amt"].ToString();
                x.isw_amt = reader["isw_amt"].ToString();
                x.twallet_xid = reader["twallet_xid"].ToString();
                x.TransactionDate = reader["TransactionDate"].ToString();
                x.transID = reader["transID"].ToString();
                x.applicant_name = reader["xname"].ToString();
                x.applicant_address = reader["address"].ToString();
                x.applicant_xemail = reader["xemail"].ToString();
                x.applicant_xmobile = reader["xmobile"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.payment_ref = reader["pay_ref"].ToString();

                x.Qty = reader["Qty"].ToString();

                x.Cld_Fees = reader["Cld_Fees"].ToString();

                x.item_code = reader["item_code"].ToString();
                x.item_description = reader["xdesc"].ToString();
             //   x.used_status = reader["used_status"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }



        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["cldConnectionString"].ConnectionString;
        }

        public MarkInfo getMarkInfo(string stage)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select * from mark_info LEFT OUTER JOIN pwallet ON mark_info.log_staff=pwallet.ID WHERE pwallet.ID ='" + stage + "'  ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            MarkInfo item = new MarkInfo();
               
            while (reader.Read())
            {
                 item = new MarkInfo
                {
                    xID = reader["xID"].ToString(),
                    reg_number = reader["reg_number"].ToString(),
                    tm_typeID = reader["tm_typeID"].ToString(),
                    logo_descriptionID = reader["logo_descriptionID"].ToString(),
                    national_classID = reader["national_classID"].ToString(),
                    product_title = reader["product_title"].ToString(),
                    nice_class = reader["nice_class"].ToString(),
                    nice_class_desc = reader["nice_class_desc"].ToString(),
                    sign_type = reader["sign_type"].ToString(),
                    vienna_class = reader["vienna_class"].ToString(),
                    disclaimer = reader["disclaimer"].ToString(),
                    logo_pic = reader["logo_pic"].ToString(),
                    auth_doc = reader["auth_doc"].ToString(),
                    sup_doc1 = reader["sup_doc1"].ToString(),
                    sup_doc2 = reader["sup_doc2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
               // list.Add(item);
            }
            reader.Close();
            connection.Close();
            return item;
        }


        public List<XObjs.ReportItemGenISW> getPaymentReportItemGenISWByTransID(string transID, string merchant_type)
        {
            List<XObjs.ReportItemGenISW> xlist = new List<XObjs.ReportItemGenISW>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            command_text += "select CONVERT(varchar, CAST( CAST(InterSwitchPostFields.amount AS int)*.01  as money),1) as 'full_amt', twallet.xid as 'twallet_xid', ";
            command_text += " SUBSTRING(InterSwitchPostFields.TransactionDate,1,19) as TransactionDate,twallet.transID,applicant.xname,applicant.address,applicant.xemail,applicant.xmobile,InterSwitchPostFields.isw_conv_fee AS isw_amt FROM InterSwitchPostFields ";
            command_text += " LEFT OUTER JOIN twallet ON InterSwitchPostFields.txn_ref=twallet.transID  ";
            command_text += " LEFT OUTER JOIN applicant ON applicant.xid=twallet.applicantID ";         

            if (merchant_type != "")
            {
                command_text += " WHERE ( InterSwitchPostFields.txn_ref='" + transID + "')  AND (fee_list.xsync='" + merchant_type + "') AND (InterSwitchPostFields.trans_status='00') ORDER BY twallet.xid DESC";
            }
            else
            {
                command_text += " WHERE ( InterSwitchPostFields.txn_ref='" + transID + "')  AND (InterSwitchPostFields.trans_status='00') ORDER BY twallet.xid DESC";
            }


            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItemGenISW x = new XObjs.ReportItemGenISW();
                x.sn = sn.ToString();
                x.full_amt = reader["full_amt"].ToString();
                x.isw_amt = reader["isw_amt"].ToString();
                x.twallet_xid = reader["twallet_xid"].ToString();
                x.TransactionDate = reader["TransactionDate"].ToString();
                x.transID = reader["transID"].ToString();
                x.applicant_name = reader["xname"].ToString();
                x.applicant_address = reader["address"].ToString();
                x.applicant_xemail = reader["xemail"].ToString();
                x.applicant_xmobile = reader["xmobile"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }

        public string getPaymentReportItemGenISWSum(string xpay_status, string fromDate, string toDate,string merchant_type )
        {
            string total_amt = "";
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());

            command_text += "select CONVERT(varchar, CAST( SUM(CAST(InterSwitchPostFields.amount AS int)*.01 ) as money),1) as 'total_amt' FROM InterSwitchPostFields ";
            command_text += " LEFT OUTER JOIN twallet ON InterSwitchPostFields.txn_ref=twallet.transID  ";
            command_text += " LEFT OUTER JOIN applicant ON applicant.xid=twallet.applicantID ";
            if (xpay_status == "1")
            { command_text += " WHERE ( InterSwitchPostFields.trans_status='00')  "; }
            else
            { command_text += " WHERE ( InterSwitchPostFields.trans_status <> '00')  "; }
            command_text += " AND (SUBSTRING(InterSwitchPostFields.TransactionDate,1,10) BETWEEN '" + fromDate + "' AND '" + toDate + "') ";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                total_amt = reader["total_amt"].ToString();
            }
            reader.Close();
            return total_amt;
        }

        public string getPaymentReportItemGenISWSum2(string xpay_status, string fromDate, string toDate, string merchant_type)
        {
            string total_amt = "";
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());

            command_text += "select CONVERT(varchar, CAST( SUM(CAST((fee_details.tech_amt * CAST((fee_details.xqty) AS int) )  AS int) ) as money),1) as 'total_amt' FROM fee_details ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid  ";
            command_text += " LEFT OUTER JOIN InterSwitchPostFields ON InterSwitchPostFields.txn_ref=twallet.transID  ";
            command_text += " LEFT OUTER JOIN applicant ON applicant.xid=twallet.applicantID ";
            if (xpay_status == "1")
            { command_text += " WHERE ( InterSwitchPostFields.trans_status='00')  "; }
            else
            { command_text += " WHERE ( InterSwitchPostFields.trans_status <> '00')  "; }
            command_text += " AND (SUBSTRING(InterSwitchPostFields.TransactionDate,1,10) BETWEEN '" + fromDate + "' AND '" + toDate + "') ";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                total_amt = reader["total_amt"].ToString();
            }
            reader.Close();
            return total_amt;
        }


        public List<XObjs.ReportItem> getPaymentReportItemBank(string xcategory, string fromDate, string toDate)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            command_text += "select fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
            command_text += " twallet.xid,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
            command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID ";
            command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
            command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";
            command_text += " WHERE (twallet.xpay_status='1') AND (twallet.xgt='xpay_bk') ";

            if (xcategory != "all") { command_text += "  AND (fee_list.xcategory='" + xcategory + "') "; }

            command_text += " AND (twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "') ORDER BY twallet.xid DESC";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }

        public List<XObjs.ReportItem> getChargeBackReportItemByMerchantRefD(string MerchantReference)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            command_text += "select fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
            command_text += " twallet.xid,twallet.applicantID,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
            command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID,InterSwitchPostFields.isw_conv_fee AS isw_amt ";
            command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
            command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";
            command_text += " left outer join InterSwitchPostFields on twallet.transID=InterSwitchPostFields.txn_ref ";
            command_text += " WHERE (InterSwitchPostFields.MerchantReference='" + MerchantReference + "')";


            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.applicantID = reader["applicantID"].ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.isw_amt = reader["isw_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }
        public List<XObjs.ReportItem> getPaymentReportItemByTransID(string transID, string fee_detailsID, string hwalletID, string merchant_type)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            command_text += "select fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
            command_text += " twallet.xid,twallet.applicantID,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
            command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID,InterSwitchPostFields.isw_conv_fee AS isw_amt  ";
            command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
            command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";
            command_text += " left outer join InterSwitchPostFields on twallet.transID=InterSwitchPostFields.txn_ref ";

            if(merchant_type!="")
            {
             command_text += " WHERE (twallet.transID ='" + transID + "') AND (fee_details.xid  ='" + fee_detailsID + "') AND (hwallet.xid ='" + hwalletID + "') AND (fee_list.xsync='" + merchant_type + "') AND (InterSwitchPostFields.trans_status='00') ORDER BY twallet.xid DESC";
            }
            else
            {
                command_text += " WHERE (twallet.transID ='" + transID + "') AND (fee_details.xid  ='" + fee_detailsID + "') AND (hwallet.xid ='" + hwalletID + "') AND (InterSwitchPostFields.trans_status='00') ORDER BY twallet.xid DESC";           
            }
           

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.applicantID = reader["applicantID"].ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }
        public List<XObjs.ReportItem> getPaymentReportItemByTransIDBank(string transID, string merchant_type)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            command_text += "select fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
            command_text += " twallet.xid,twallet.applicantID,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
            command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID ";
            command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
            command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";

            command_text += " WHERE (twallet.transID LIKE'%" + transID + "%') AND (fee_list.xsync='" + merchant_type + "') AND (twallet.xpay_status='1') AND (twallet.xgt='xpay_bk') ORDER BY twallet.xid DESC";


            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }
        public List<XObjs.ReportItem> getApplicationReportItem(string xcategory, string used_status, string xgt, string fromDate, string toDate)
        {
            List<XObjs.ReportItem> xlist = new List<XObjs.ReportItem>();
            int sn = 1; string command_text = "";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            command_text += "select fee_list.item_code,fee_list.xdesc,fee_list.init_amt,fee_list.tech_amt, fee_details.xid AS fdID, fee_details.xqty,fee_details.tot_amt, ";
            command_text += " twallet.xid,twallet.transID,twallet.xpay_status,twallet.xgt,twallet.xreg_date ,hwallet.xid AS hID,hwallet.used_status,hwallet.product_title, ";
            command_text += " CAST(hwallet.transID AS nvarchar)+'-'+CAST(hwallet.fee_detailsID AS nvarchar)+'-'+CAST(hwallet.xid AS nvarchar) AS newtransID ";
            command_text += " FROM fee_list LEFT OUTER JOIN fee_details ON fee_list.xid=fee_details.fee_listID ";
            command_text += " LEFT OUTER JOIN twallet ON fee_details.twalletID=twallet.xid ";
            command_text += " LEFT OUTER JOIN hwallet ON twallet.transID=hwallet.transID ";
            command_text += " WHERE (twallet.xpay_status='1') AND (twallet.xgt='" + xgt + "') AND (fee_list.xcategory='" + xcategory + "') AND (hwallet.used_status='" + used_status + "')  ";
            command_text += " AND (twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "') ORDER BY twallet.xid DESC ";

            SqlCommand command = new SqlCommand(command_text, connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.ReportItem x = new XObjs.ReportItem();
                x.sn = sn.ToString();
                x.hID = reader["hID"].ToString();
                x.fdID = reader["fdID"].ToString();
                x.transID = reader["transID"].ToString();
                x.newtransID = reader["newtransID"].ToString();
                x.item_code = reader["item_code"].ToString();
                x.item_desc = reader["xdesc"].ToString();
                x.payment_date = reader["xreg_date"].ToString();
                x.tech_amt = reader["tech_amt"].ToString();
                x.init_amt = reader["init_amt"].ToString();
                x.total_amt = reader["tot_amt"].ToString();
                x.qty = reader["xqty"].ToString();
                x.used_status = reader["used_status"].ToString();
                x.product_title = reader["product_title"].ToString();
                xlist.Add(x);
                sn++;
            }
            reader.Close();
            return xlist;
        }

        public XObjs.PRatio getPratioByMemberID(string xmemberID)
        {
            XObjs.PRatio ratio = new XObjs.PRatio();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from p_ratio where xpartnerID='" + xmemberID + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                ratio.xid = reader["xid"].ToString();
                ratio.xpartnerID = reader["xpartnerID"].ToString();
                ratio.p_type = reader["p_type"].ToString();
                ratio.xratio = reader["xratio"].ToString();
                ratio.r_type = reader["r_type"].ToString();
                ratio.xreg_date = reader["xreg_date"].ToString();
                ratio.xsync = reader["xsync"].ToString();
                ratio.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return ratio;
        }

        public List<XObjs.PRatio> getPratioByMerchant(string merchant)
        {
            List<XObjs.PRatio> list = new List<XObjs.PRatio>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from p_ratio where xsync='" + merchant + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.PRatio item = new XObjs.PRatio {
                    xid = reader["xid"].ToString(),
                    xpartnerID = reader["xpartnerID"].ToString(),
                    p_type = reader["p_type"].ToString(),
                    xratio = reader["xratio"].ToString(),
                    r_type = reader["r_type"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xsync = reader["xsync"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public XObjs.Pwallet getPwalletByID(string xid)
        {
            XObjs.Pwallet pwallet = new XObjs.Pwallet();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE xid='" + xid + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                pwallet.xid = reader["xid"].ToString();
                pwallet.xmembertype = reader["xmembertype"].ToString();
                pwallet.xmemberID = reader["xmemberID"].ToString();
                pwallet.xemail = reader["xemail"].ToString();
                pwallet.xmobile = reader["xmobile"].ToString();
                pwallet.xpass = reader["xpass"].ToString();
                pwallet.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return pwallet;
        }

        public XObjs.Pwallet getPwalletByMemberID(string xmemberID)
        {
            XObjs.Pwallet pwallet = new XObjs.Pwallet();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE xmemberID='" + xmemberID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                pwallet.xid = reader["xid"].ToString();
                pwallet.xmembertype = reader["xmembertype"].ToString();
                pwallet.xmemberID = reader["xmemberID"].ToString();
                pwallet.xemail = reader["xemail"].ToString();
                pwallet.xmobile = reader["xmobile"].ToString();
                pwallet.xpass = reader["xpass"].ToString();
                pwallet.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return pwallet;
        }

        public List<XObjs.Pwallet> getPwalletByMemberType(string xmembertype)
        {
            List<XObjs.Pwallet> list = new List<XObjs.Pwallet>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE xmembertype='" + xmembertype + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Pwallet item = new XObjs.Pwallet {
                    xid = reader["xid"].ToString(),
                    xmembertype = reader["xmembertype"].ToString(),
                    xmemberID = reader["xmemberID"].ToString(),
                    xemail = reader["xemail"].ToString(),
                    xmobile = reader["xmobile"].ToString(),
                    xpass = reader["xpass"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public XObjs.Pwallet getPwalletByMobile(string xmobile)
        {
            XObjs.Pwallet pwallet = new XObjs.Pwallet();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE xmobile='" + xmobile + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                pwallet.xid = reader["xid"].ToString();
                pwallet.xmembertype = reader["xmembertype"].ToString();
                pwallet.xmemberID = reader["xmemberID"].ToString();
                pwallet.xemail = reader["xemail"].ToString();
                pwallet.xmobile = reader["xmobile"].ToString();
                pwallet.xpass = reader["xpass"].ToString();
                pwallet.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return pwallet;
        }

        public XObjs.Scard getRandomScard()
        {
            XObjs.Scard scard = new XObjs.Scard();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT top 1 * FROM scard WHERE xid >= RAND() * (SELECT MAX(xid) FROM scard)   ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                scard.xid = reader["xid"].ToString();
                scard.xnum = reader["xnum"].ToString();
                scard.xvalid = reader["xvalid"].ToString();
                scard.xlogstaff = reader["xlogstaff"].ToString();
                scard.xvalid = reader["xvalid"].ToString();
                scard.xreg_date = reader["xreg_date"].ToString();
                scard.xsync = reader["xsync"].ToString();
                scard.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return scard;
        }

        public XObjs.Registration getRegistrationByID(string xid)
        {
            XObjs.Registration registration = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE xid='" + xid + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                registration.xid = reader["xid"].ToString();
                registration.AccrediationType = reader["AccrediationType"].ToString();
                registration.Sys_ID = reader["Sys_ID"].ToString();
                registration.Firstname = hf.ConvertTab2Apos(reader["Firstname"].ToString());
                registration.Surname = hf.ConvertTab2Apos(reader["Surname"].ToString());
                registration.Email = hf.ConvertTab2Apos(reader["Email"].ToString());
                registration.xpassword = hf.ConvertTab2Apos(reader["xpassword"].ToString());
                registration.DateOfBrith = reader["DateOfBrith"].ToString();
                registration.IncorporatedDate = reader["IncorporatedDate"].ToString();
                registration.Nationality = reader["Nationality"].ToString();
                registration.PhoneNumber = reader["PhoneNumber"].ToString();
                registration.CompanyName = hf.ConvertTab2Apos(reader["CompanyName"].ToString());
                registration.CompanyAddress = hf.ConvertTab2Apos(reader["CompanyAddress"].ToString());
                registration.ContactPerson = hf.ConvertTab2Apos(reader["ContactPerson"].ToString());
                registration.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                registration.CompanyWebsite = hf.ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                registration.Certificate = reader["Certificate"].ToString();
                registration.Introduction = reader["Introduction"].ToString();
                registration.Principal = reader["Principal"].ToString();
                registration.xreg_date = reader["xreg_date"].ToString();
                registration.xstatus = reader["xstatus"].ToString();
                registration.xvisible = reader["xvisible"].ToString();
                registration.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return registration;
        }

        public XObjs.Registration getRegistrationByPhoneNumber(string PhoneNumber)
        {
            XObjs.Registration registration = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE PhoneNumber='" + PhoneNumber + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                registration.xid = reader["xid"].ToString();
                registration.AccrediationType = reader["AccrediationType"].ToString();
                registration.Sys_ID = reader["Sys_ID"].ToString();
                registration.Firstname = hf.ConvertTab2Apos(reader["Firstname"].ToString());
                registration.Surname = hf.ConvertTab2Apos(reader["Surname"].ToString());
                registration.Email = hf.ConvertTab2Apos(reader["Email"].ToString());
                registration.xpassword = hf.ConvertTab2Apos(reader["xpassword"].ToString());
                registration.DateOfBrith = reader["DateOfBrith"].ToString();
                registration.IncorporatedDate = reader["IncorporatedDate"].ToString();
                registration.Nationality = reader["Nationality"].ToString();
                registration.PhoneNumber = reader["PhoneNumber"].ToString();
                registration.CompanyName = hf.ConvertTab2Apos(reader["CompanyName"].ToString());
                registration.CompanyAddress = hf.ConvertTab2Apos(reader["CompanyAddress"].ToString());
                registration.ContactPerson = hf.ConvertTab2Apos(reader["ContactPerson"].ToString());
                registration.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                registration.CompanyWebsite = hf.ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                registration.Certificate = reader["Certificate"].ToString();
                registration.Introduction = reader["Introduction"].ToString();
                registration.Principal = reader["Principal"].ToString();
                registration.xreg_date = reader["xreg_date"].ToString();
                registration.xstatus = reader["xstatus"].ToString();
                registration.xvisible = reader["xvisible"].ToString();
                registration.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return registration;
        }

        public XObjs.Registration getRegistrationBySubagentRegistrationID(string RegistrationID)
        {
            XObjs.Registration registration = new XObjs.Registration();
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM registrations WHERE (xid='" + RegistrationID + "')  ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                registration.xid = reader["xid"].ToString();
                registration.AccrediationType = reader["AccrediationType"].ToString();
                registration.Sys_ID = reader["Sys_ID"].ToString();
                registration.Firstname = hf.ConvertTab2Apos(reader["Firstname"].ToString());
                registration.Surname = hf.ConvertTab2Apos(reader["Surname"].ToString());
                registration.Email = hf.ConvertTab2Apos(reader["Email"].ToString());
                registration.xpassword = hf.ConvertTab2Apos(reader["xpassword"].ToString());
                registration.DateOfBrith = reader["DateOfBrith"].ToString();
                registration.IncorporatedDate = reader["IncorporatedDate"].ToString();
                registration.Nationality = reader["Nationality"].ToString();
                registration.PhoneNumber = reader["PhoneNumber"].ToString();
                registration.CompanyName = hf.ConvertTab2Apos(reader["CompanyName"].ToString());
                registration.CompanyAddress = hf.ConvertTab2Apos(reader["CompanyAddress"].ToString());
                registration.ContactPerson = hf.ConvertTab2Apos(reader["ContactPerson"].ToString());
                registration.ContactPersonPhone = reader["ContactPersonPhone"].ToString();
                registration.CompanyWebsite = hf.ConvertTab2Apos(reader["CompanyWebsite"].ToString());
                registration.Certificate = reader["Certificate"].ToString();
                registration.Introduction = reader["Introduction"].ToString();
                registration.Principal = reader["Principal"].ToString();
                registration.xreg_date = reader["xreg_date"].ToString();
                registration.xstatus = reader["xstatus"].ToString();
                registration.xvisible = reader["xvisible"].ToString();
                registration.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return registration;
        }

        public XObjs.Subagent getSubAgentByID(string xid)
        {
            XObjs.Subagent subagent = new XObjs.Subagent();
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents WHERE xid='" + xid + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                subagent.xid = reader["xid"].ToString();
                subagent.RegistrationID = reader["RegistrationID"].ToString();
                subagent.Surname = hf.ConvertTab2Apos(reader["Surname"].ToString());
                subagent.Firstname = hf.ConvertTab2Apos(reader["Firstname"].ToString());
                subagent.Email = hf.ConvertTab2Apos(reader["Email"].ToString());
                subagent.xpassword = hf.ConvertTab2Apos(reader["xpassword"].ToString());
                subagent.Telephone = reader["Telephone"].ToString();
                subagent.DateOfBrith = reader["DateOfBrith"].ToString();
                subagent.AssignID = reader["AssignID"].ToString();
                subagent.Sys_ID = reader["Sys_ID"].ToString();
                subagent.Address = hf.ConvertTab2Apos(reader["Address"].ToString());
                subagent.AgentPassport = reader["AgentPassport"].ToString();
                subagent.xreg_date = reader["xreg_date"].ToString();
                subagent.xstatus = reader["xstatus"].ToString();
                subagent.xvisible = reader["xvisible"].ToString();
                subagent.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return subagent;
        }

        public XObjs.Subagent getSubAgentByPhoneNumber(string PhoneNumber)
        {
            XObjs.Subagent subagent = new XObjs.Subagent();
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("SELECT * FROM subagents WHERE Telephone='" + PhoneNumber + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                subagent.xid = reader["xid"].ToString();
                subagent.RegistrationID = reader["RegistrationID"].ToString();
                subagent.Surname = hf.ConvertTab2Apos(reader["Surname"].ToString());
                subagent.Firstname = hf.ConvertTab2Apos(reader["Firstname"].ToString());
                subagent.Email = hf.ConvertTab2Apos(reader["Email"].ToString());
                subagent.xpassword = hf.ConvertTab2Apos(reader["xpassword"].ToString());
                subagent.Telephone = reader["Telephone"].ToString();
                subagent.DateOfBrith = reader["DateOfBrith"].ToString();
                subagent.AssignID = reader["AssignID"].ToString();
                subagent.Sys_ID = reader["Sys_ID"].ToString();
                subagent.Address = hf.ConvertTab2Apos(reader["Address"].ToString());
                subagent.AgentPassport = reader["AgentPassport"].ToString();
                subagent.xreg_date = reader["xreg_date"].ToString();
                subagent.xstatus = reader["xstatus"].ToString();
                subagent.xvisible = reader["xvisible"].ToString();
                subagent.xsync = reader["xsync"].ToString();
            }
            reader.Close();
            return subagent;
        }

        public string getSubAgentLogDetails(string uname, string xpass)
        {
            string str = "0";
            SqlConnection connection = new SqlConnection(hf.ConnectXhome());
            SqlCommand command = new SqlCommand("select xid from subagents WHERE  Email='" + uname + "' AND xpassword LIKE'%" + xpass + "%' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xid"].ToString();
            }
            reader.Close();
            return str;
        }

        public int getSumTotalByMonthAdmin(string year, string month)
        {
            int num = 0;
            string str = year + "-" + month + "-01";
            string str2 = year + "-" + month + "-31";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tot_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + str + "' AND '" + str2 + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getSumTotalByMonthMerchant(string year, string month)
        {
            int num = 0;
            string str = year + "-" + month + "-01";
            string str2 = year + "-" + month + "-31";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.init_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + str + "' AND '" + str2 + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getSumTotalByMonthWingman(string year, string month)
        {
            int num = 0;
            string str = year + "-" + month + "-01";
            string str2 = year + "-" + month + "-31";
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tech_amt AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + str + "' AND '" + str2 + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getSumTotalTransAdmin(string fromDate, string toDate)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tot_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getSumTotalTransMerchant(string fromDate, string toDate)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.init_amt AS int) * CAST(fee_details.xqty AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public int getSumTotalTransWingman(string fromDate, string toDate)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("select CAST(SUM(CAST(fee_details.tech_amt AS int)) as money) as xsum from twallet INNER JOIN fee_details ON twallet.xid=fee_details.twalletID WHERE twallet.xpay_status='1' AND twallet.xgt<>'xpay' AND twallet.xreg_date BETWEEN '" + fromDate + "' AND '" + toDate + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                try
                {
                    num = Convert.ToInt32(reader["xsum"]);
                }
                catch (Exception)
                {
                    num = 0;
                }
            }
            reader.Close();
            return num;
        }

        public List<XObjs.Twallet> getTwalletByMemberID(string xmemberID, string transID, string xmembertype)
        {
            List<XObjs.Twallet> list = new List<XObjs.Twallet>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where (xmemberID='" + xmemberID + "') AND (transID='" + transID + "') AND (xmembertype='" + xmembertype + "') ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Twallet item = new XObjs.Twallet {
                    xid = reader["xid"].ToString(),
                    transID = reader["transID"].ToString(),
                    xmemberID = reader["xmemberID"].ToString(),
                    xmembertype = reader["xmembertype"].ToString(),
                    xpay_status = reader["xpay_status"].ToString(),
                    xgt = reader["xgt"].ToString(),
                    ref_no = reader["ref_no"].ToString(),
                    xbankerID = reader["xbankerID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xsync = reader["xsync"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public XObjs.Twallet getTwalletByTransID(string transID)
        {
            XObjs.Twallet twallet = new XObjs.Twallet();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where transID='" + transID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                twallet.xid = reader["xid"].ToString();
                twallet.transID = reader["transID"].ToString();
                twallet.xmemberID = reader["xmemberID"].ToString();
                twallet.xmembertype = reader["xmembertype"].ToString();
                twallet.xpay_status = reader["xpay_status"].ToString();
                twallet.xgt = reader["xgt"].ToString();
                twallet.ref_no = reader["ref_no"].ToString();
                twallet.xbankerID = reader["xbankerID"].ToString();
                twallet.applicantID = reader["applicantID"].ToString();
                twallet.xreg_date = reader["xreg_date"].ToString();
                twallet.xsync = reader["xsync"].ToString();
                twallet.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return twallet;
        }

        public int getTransIDCnt(string transID)
        {
            int cnt = 0;
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT count (xid)  as cnt  from twallet where transID='" + transID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                cnt = Convert.ToInt32(reader["cnt"]);
            }
            reader.Close();
            return cnt;
        }

        public XObjs.Twallet getTwalletByTransIDAdminID(string transID, string xmemberID)
        {
            XObjs.Twallet twallet = new XObjs.Twallet();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where (transID='" + transID + "') AND  (xmemberID='" + xmemberID + "') ", connection);
            connection.Open();command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                twallet.xid = reader["xid"].ToString();
                twallet.transID = reader["transID"].ToString();
                twallet.xmemberID = reader["xmemberID"].ToString();
                twallet.xmembertype = reader["xmembertype"].ToString();
                twallet.xpay_status = reader["xpay_status"].ToString();
                twallet.xgt = reader["xgt"].ToString();
                twallet.ref_no = reader["ref_no"].ToString();
                twallet.xbankerID = reader["xbankerID"].ToString();
                twallet.applicantID = reader["applicantID"].ToString();
                twallet.xreg_date = reader["xreg_date"].ToString();
                twallet.xsync = reader["xsync"].ToString();
                twallet.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return twallet;
        }

        public List<XObjs.Twallet> getValidatedTwalletByMemberID(string xmemberID, string transID)
        {
            List<XObjs.Twallet> list = new List<XObjs.Twallet>();
            SqlConnection connection = new SqlConnection(hf.ConnectXpay());
            SqlCommand command = new SqlCommand("SELECT * from twallet where xmemberID='" + xmemberID + "'  AND transID='" + transID + "' ", connection);
            connection.Open(); command.CommandTimeout = 0;
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                XObjs.Twallet item = new XObjs.Twallet {
                    xid = reader["xid"].ToString(),
                    transID = reader["transID"].ToString(),
                    xmemberID = reader["xmemberID"].ToString(),
                    xpay_status = reader["xpay_status"].ToString(),
                    xgt = reader["xgt"].ToString(),
                    ref_no = reader["ref_no"].ToString(),
                    xbankerID = reader["xbankerID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    xreg_date = reader["xreg_date"].ToString(),
                    xsync = reader["xsync"].ToString(),
                    xvisible = reader["xvisible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }
    }
}

