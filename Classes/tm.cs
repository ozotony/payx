namespace XPay.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public class tm
    {
        public string a_address_service(string xstate, string xcity, string street, string xzip, string xtelephone, string xemail, string validationID, string agentID, string amt, string stage)
        {
            string connectionString = this.Connect();
            DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str2 = "";
            string str3 = this.getPwalletID(validationID);
            if (this.addAddress_Service("", xstate, "0", xcity, street, xzip, xtelephone, "", xemail, "", str3) > 0)
            {
                return Convert.ToInt32(this.e_pwallet(validationID, agentID, amt, stage, "")).ToString();
            }
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("DELETE FROM address_service WHERE xID='" + str2 + "' ", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return "0";
        }

        public string a_applicant(string name, string type, string tax_id_type, string tax_id_number, string individual_id_number, string nationality, string residence, string xstate, string xcity, string street, string xzip, string xtelephone, string xemail, string agentID, string amt, string validationID, string stage)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str3 = "";
            int num = this.addAddress(residence, xstate, "0", xcity, street, xzip, xtelephone, "", xemail, "", validationID);
            if (num <= 0)
            {
                return str3;
            }
            str3 = Convert.ToInt32(this.e_pwallet(validationID, agentID, amt, stage, "")).ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(string.Concat(new object[] { "UPDATE address SET log_staff='", str3, "' WHERE ID='", num, "' " }), connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            if (str3 != "0")
            {
                SqlConnection connection2 = new SqlConnection(connectionString);
                connection2.Open();
                SqlCommand command2 = new SqlCommand("sp_a_applicant", connection2) {
                    CommandType = CommandType.StoredProcedure
                };
                command2.Parameters.Add(new SqlParameter("@name", name));
                command2.Parameters.Add(new SqlParameter("@type", type));
                command2.Parameters.Add(new SqlParameter("@tax_id_type", tax_id_type));
                command2.Parameters.Add(new SqlParameter("@tax_id_number", tax_id_number));
                command2.Parameters.Add(new SqlParameter("@individual_id_number", individual_id_number));
                command2.Parameters.Add(new SqlParameter("@nationality", nationality));
                command2.Parameters.Add(new SqlParameter("@addressID", num.ToString()));
                command2.Parameters.Add(new SqlParameter("@log_staff", str3));
                command2.Parameters.Add(new SqlParameter("@reg_date", str2));
                command2.Parameters.Add(new SqlParameter("@visible", "1"));
                SqlParameter parameter = command2.Parameters.Add("@ReturnVal", SqlDbType.Int);
                parameter.Direction = ParameterDirection.ReturnValue;
                command2.ExecuteNonQuery();
                str3 = parameter.Value.ToString();
                connection2.Close();
                return str3;
            }
            SqlConnection connection3 = new SqlConnection(connectionString);
            SqlCommand command4 = new SqlCommand("DELETE FROM address WHERE xID='" + num + "' ", connection3);
            connection3.Open();
            command4.ExecuteNonQuery();
            connection3.Close();
            SqlConnection connection4 = new SqlConnection(connectionString);
            SqlCommand command5 = new SqlCommand("DELETE FROM applicant WHERE xID='" + str3 + "' ", connection4);
            connection4.Open();
            command5.ExecuteNonQuery();
            connection4.Close();
            return "0";
        }

        public string a_markInfo(string tm_type, string logo_description, string title_of_product, string nice_class, string nice_class_desc, string national_class, string sign_type, string vienna_class, string disclaimer, string logo_pic, string auth_doc, string sup_doc1, string sup_doc2, string validationID, string agentID, string amt, string stage)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str3 = "";
            string str4 = "";
            string str5 = this.getMarkPwalletID(validationID, agentID, amt, stage, "");
            if (tm_type == "1")
            {
                str4 = "NG/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/";
            }
            else
            {
                str4 = "F/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/";
            }
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_a_MarkInfo", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@reg_number", str4));
            command.Parameters.Add(new SqlParameter("@tm_typeID", tm_type));
            command.Parameters.Add(new SqlParameter("@logo_descriptionID", logo_description));
            command.Parameters.Add(new SqlParameter("@product_title", title_of_product));
            command.Parameters.Add(new SqlParameter("@nice_class", nice_class));
            command.Parameters.Add(new SqlParameter("@nice_class_desc", nice_class_desc));
            command.Parameters.Add(new SqlParameter("@national_classID", national_class));
            command.Parameters.Add(new SqlParameter("@sign_type", sign_type));
            command.Parameters.Add(new SqlParameter("@vienna_class", vienna_class));
            command.Parameters.Add(new SqlParameter("@disclaimer", disclaimer));
            command.Parameters.Add(new SqlParameter("@logo_pic", logo_pic));
            command.Parameters.Add(new SqlParameter("@auth_doc", auth_doc));
            command.Parameters.Add(new SqlParameter("@sup_doc1", sup_doc1));
            command.Parameters.Add(new SqlParameter("@sup_doc2", sup_doc2));
            command.Parameters.Add(new SqlParameter("@log_staff", str5));
            command.Parameters.Add(new SqlParameter("@reg_date", str2));
            command.Parameters.Add(new SqlParameter("@xvisible", "1"));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            str3 = parameter.Value.ToString();
            connection.Close();
            if (!(str3 != "0"))
            {
                return str3;
            }
            str4 = str4 + str3;
            if (this.e_MarkInfoRegNumber(str4, str5) != 0)
            {
                if (this.e_pwallet(validationID, agentID, amt, stage, "").ToString() == "0")
                {
                    str3 = "0";
                }
                return str3;
            }
            SqlConnection connection2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand("DELETE FROM mark_info WHERE xID='" + str3 + "' ", connection2);
            connection2.Open();
            command2.ExecuteNonQuery();
            connection2.Close();
            return "0";
        }

        public string a_representative(string xcode, string xname, string individual_id_type, string individual_id_number, string nationality, string residence, string xstate, string xcity, string street, string xzip, string xtelephone, string xemail, string validationID, string agentID, string amt, string stage)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str3 = "";
            string str4 = this.getPwalletID(validationID);
            int num = this.addAddress(residence, xstate, "0", xcity, street, xzip, xtelephone, "", xemail, "", str4);
            if (num > 0)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("sp_a_representative", connection) {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@xname", xname));
                command.Parameters.Add(new SqlParameter("@agent_code", xcode));
                command.Parameters.Add(new SqlParameter("@individual_id_type", individual_id_type));
                command.Parameters.Add(new SqlParameter("@individual_id_number", individual_id_number));
                command.Parameters.Add(new SqlParameter("@nationality", nationality));
                command.Parameters.Add(new SqlParameter("@addressID", num.ToString()));
                command.Parameters.Add(new SqlParameter("@log_staff", str4));
                command.Parameters.Add(new SqlParameter("@reg_date", str2));
                command.Parameters.Add(new SqlParameter("@visible", "1"));
                SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                parameter.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                str3 = parameter.Value.ToString();
                connection.Close();
            }
            if (str3 != "0")
            {
                str3 = Convert.ToInt32(this.e_pwallet(validationID, agentID, amt, stage, "Fresh")).ToString();
                Convert.ToInt32(this.e_PwalletStatus(str4, "1", "Fresh")).ToString();
                return str3;
            }
            SqlConnection connection2 = new SqlConnection(connectionString);
            SqlCommand command3 = new SqlCommand("DELETE FROM address WHERE xID='" + num + "' ", connection2);
            connection2.Open();
            command3.ExecuteNonQuery();
            connection2.Close();
            SqlConnection connection3 = new SqlConnection(connectionString);
            SqlCommand command4 = new SqlCommand("DELETE FROM representative WHERE xID='" + str3 + "' ", connection3);
            connection3.Open();
            command4.ExecuteNonQuery();
            connection3.Close();
            return "0";
        }

        public string addAddress(Address c_app_addy, string pwalletID)
        {
            string str = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str2 = "1";
            if (c_app_addy.countryID == null)
            {
                c_app_addy.countryID = "";
            }
            if (c_app_addy.stateID == null)
            {
                c_app_addy.stateID = "";
            }
            if (c_app_addy.city == null)
            {
                c_app_addy.city = "";
            }
            if (c_app_addy.street == null)
            {
                c_app_addy.street = "";
            }
            if (c_app_addy.telephone1 == null)
            {
                c_app_addy.telephone1 = "";
            }
            if (c_app_addy.email1 == null)
            {
                c_app_addy.email1 = "";
            }
            string connectionString = this.Connect();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_app_addy.countryID;
            command.Parameters["@stateID"].Value = c_app_addy.stateID;
            command.Parameters["@city"].Value = c_app_addy.city;
            command.Parameters["@street"].Value = c_app_addy.street;
            command.Parameters["@telephone1"].Value = c_app_addy.telephone1;
            command.Parameters["@email1"].Value = c_app_addy.email1;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str;
            command.Parameters["@visible"].Value = str2;
            str4 = command.ExecuteScalar().ToString();
            connection.Close();
            return str4;
        }

        public int addAddress(string countryID, string stateID, string lgaID, string city, string street, string zip, string telephone1, string telephone2, string email1, string email2, string log_staff)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_a_address", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@countryID", countryID));
            command.Parameters.Add(new SqlParameter("@stateID", stateID));
            command.Parameters.Add(new SqlParameter("@lgaID", lgaID));
            command.Parameters.Add(new SqlParameter("@city", city));
            command.Parameters.Add(new SqlParameter("@street", street));
            command.Parameters.Add(new SqlParameter("@zip", zip));
            command.Parameters.Add(new SqlParameter("@telephone1", telephone1));
            command.Parameters.Add(new SqlParameter("@telephone2", telephone2));
            command.Parameters.Add(new SqlParameter("@email1", email1));
            command.Parameters.Add(new SqlParameter("@email2", email2));
            command.Parameters.Add(new SqlParameter("@log_staff", log_staff));
            command.Parameters.Add(new SqlParameter("@reg_date", str2));
            command.Parameters.Add(new SqlParameter("@visible", "1"));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            return num;
        }

        public int addAddress_Service(string countryID, string stateID, string lgaID, string city, string street, string zip, string telephone1, string telephone2, string email1, string email2, string log_staff)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_a_address_service", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@countryID", countryID));
            command.Parameters.Add(new SqlParameter("@stateID", stateID));
            command.Parameters.Add(new SqlParameter("@lgaID", lgaID));
            command.Parameters.Add(new SqlParameter("@city", city));
            command.Parameters.Add(new SqlParameter("@street", street));
            command.Parameters.Add(new SqlParameter("@zip", zip));
            command.Parameters.Add(new SqlParameter("@telephone1", telephone1));
            command.Parameters.Add(new SqlParameter("@telephone2", telephone2));
            command.Parameters.Add(new SqlParameter("@email1", email1));
            command.Parameters.Add(new SqlParameter("@email2", email2));
            command.Parameters.Add(new SqlParameter("@log_staff", log_staff));
            command.Parameters.Add(new SqlParameter("@reg_date", str2));
            command.Parameters.Add(new SqlParameter("@visible", "1"));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            return num;
        }

        public string addAos(AddressService c_aos, string pwalletID)
        {
            if (c_aos.countryID == null)
            {
                c_aos.countryID = "";
            }
            if (c_aos.stateID == null)
            {
                c_aos.stateID = "";
            }
            if (c_aos.city == null)
            {
                c_aos.city = "";
            }
            if (c_aos.street == null)
            {
                c_aos.street = "";
            }
            if (c_aos.telephone1 == null)
            {
                c_aos.telephone1 = "";
            }
            if (c_aos.email1 == null)
            {
                c_aos.email1 = "";
            }
            string connectionString = this.Connect();
            string str2 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address_service (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_aos.countryID;
            command.Parameters["@stateID"].Value = c_aos.stateID;
            command.Parameters["@city"].Value = c_aos.city;
            command.Parameters["@street"].Value = c_aos.street;
            command.Parameters["@telephone1"].Value = c_aos.telephone1;
            command.Parameters["@email1"].Value = c_aos.email1;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = c_aos.reg_date;
            command.Parameters["@visible"].Value = c_aos.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }

        public string addApplicant(Applicant c_app, Address c_app_addy, string pwalletID)
        {
            string str = "0";
            if (c_app.xname == null)
            {
                c_app.xname = "";
            }
            if (c_app.nationality == null)
            {
                c_app.nationality = "";
            }
            str = this.addAddress(c_app_addy, pwalletID);
            string connectionString = this.Connect();
            string str3 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO applicant (xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@xname", SqlDbType.VarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 20);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 10);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@xname"].Value = c_app.xname;
            command.Parameters["@nationality"].Value = c_app.nationality;
            command.Parameters["@addressID"].Value = str;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = c_app.reg_date;
            command.Parameters["@visible"].Value = c_app.visible;
            str3 = command.ExecuteScalar().ToString();
            connection.Close();
            return str3;
        }

        public string addCurrentAddress(Address c_app_addy, string pwalletID, string date)
        {
            string str = date;
            string str2 = "1";
            if (c_app_addy.countryID == null)
            {
                c_app_addy.countryID = "";
            }
            if (c_app_addy.stateID == null)
            {
                c_app_addy.stateID = "";
            }
            if (c_app_addy.city == null)
            {
                c_app_addy.city = "";
            }
            if (c_app_addy.street == null)
            {
                c_app_addy.street = "";
            }
            if (c_app_addy.telephone1 == null)
            {
                c_app_addy.telephone1 = "";
            }
            if (c_app_addy.email1 == null)
            {
                c_app_addy.email1 = "";
            }
            string connectionString = this.Connect();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_app_addy.countryID;
            command.Parameters["@stateID"].Value = c_app_addy.stateID;
            command.Parameters["@city"].Value = c_app_addy.city;
            command.Parameters["@street"].Value = c_app_addy.street;
            command.Parameters["@telephone1"].Value = c_app_addy.telephone1;
            command.Parameters["@email1"].Value = c_app_addy.email1;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str;
            command.Parameters["@visible"].Value = str2;
            str4 = command.ExecuteScalar().ToString();
            connection.Close();
            return str4;
        }

        public string addCurrentAos(AddressService c_aos, string pwalletID, string date)
        {
            string str = date;
            string str2 = "1";
            if (c_aos.countryID == null)
            {
                c_aos.countryID = "";
            }
            if (c_aos.stateID == null)
            {
                c_aos.stateID = "";
            }
            if (c_aos.city == null)
            {
                c_aos.city = "";
            }
            if (c_aos.street == null)
            {
                c_aos.street = "";
            }
            if (c_aos.telephone1 == null)
            {
                c_aos.telephone1 = "";
            }
            if (c_aos.email1 == null)
            {
                c_aos.email1 = "";
            }
            string connectionString = this.Connect();
            string str4 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address_service (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_aos.countryID;
            command.Parameters["@stateID"].Value = c_aos.stateID;
            command.Parameters["@city"].Value = c_aos.city;
            command.Parameters["@street"].Value = c_aos.street;
            command.Parameters["@telephone1"].Value = c_aos.telephone1;
            command.Parameters["@email1"].Value = c_aos.email1;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str;
            command.Parameters["@visible"].Value = str2;
            str4 = command.ExecuteScalar().ToString();
            connection.Close();
            return str4;
        }

        public string addCurrentApplicant(Applicant c_app, Address c_app_addy, string pwalletID, string date)
        {
            string str = "0";
            string str2 = date;
            string str3 = "1";
            if (c_app.xname == null)
            {
                c_app.xname = "";
            }
            if (c_app.nationality == null)
            {
                c_app.nationality = "";
            }
            str = this.addCurrentAddress(c_app_addy, pwalletID, date);
            string connectionString = this.Connect();
            string str5 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO applicant (xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@xname", SqlDbType.VarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 20);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 10);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@xname"].Value = c_app.xname;
            command.Parameters["@nationality"].Value = c_app.nationality;
            command.Parameters["@addressID"].Value = str;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str2;
            command.Parameters["@visible"].Value = str3;
            str5 = command.ExecuteScalar().ToString();
            connection.Close();
            return str5;
        }

        public string addCurrentMark(MarkInfo c_mark, string pwalletID, string date)
        {
            string str = "";
            string str2 = date;
            string str3 = "1";
            if (c_mark.tm_typeID == null)
            {
                c_mark.tm_typeID = "";
            }
            if (c_mark.logo_descriptionID == null)
            {
                c_mark.logo_descriptionID = "";
            }
            if (c_mark.product_title == null)
            {
                c_mark.product_title = "";
            }
            if (c_mark.nice_class == null)
            {
                c_mark.nice_class = "";
            }
            if (c_mark.nice_class_desc == null)
            {
                c_mark.nice_class_desc = "";
            }
            if (c_mark.national_classID == null)
            {
                c_mark.national_classID = "";
            }
            if (c_mark.disclaimer == null)
            {
                c_mark.disclaimer = "";
            }
            string connectionString = this.Connect();
            string str5 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO mark_Info (reg_number,tm_typeID,logo_descriptionID,product_title,nice_class,nice_class_desc,national_classID,disclaimer,log_staff,reg_date,xvisible) VALUES (@reg_number,@tm_typeID,@logo_descriptionID,@product_title,@nice_class,@nice_class_desc,@national_classID,@disclaimer,@log_staff,@reg_date,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@reg_number", SqlDbType.VarChar, 50);
            command.Parameters.Add("@tm_typeID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@logo_descriptionID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@product_title", SqlDbType.VarChar, 50);
            command.Parameters.Add("@nice_class", SqlDbType.VarChar, 50);
            command.Parameters.Add("@nice_class_desc", SqlDbType.NVarChar);
            command.Parameters.Add("@national_classID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@disclaimer", SqlDbType.VarChar);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 50);
            command.Parameters.Add("@xvisible", SqlDbType.VarChar, 1);
            command.Parameters["@reg_number"].Value = str;
            command.Parameters["@tm_typeID"].Value = c_mark.tm_typeID;
            command.Parameters["@logo_descriptionID"].Value = c_mark.logo_descriptionID;
            command.Parameters["@product_title"].Value = c_mark.product_title;
            command.Parameters["@nice_class"].Value = c_mark.nice_class;
            command.Parameters["@nice_class_desc"].Value = c_mark.nice_class_desc;
            command.Parameters["@national_classID"].Value = c_mark.national_classID;
            command.Parameters["@disclaimer"].Value = c_mark.disclaimer;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str2;
            command.Parameters["@xvisible"].Value = str3;
            str5 = command.ExecuteScalar().ToString();
            connection.Close();
            return str5;
        }

        public string addCurrentRepresentative(Representative c_rep, Address c_rep_addy, string pwalletID, string date)
        {
            string str = "0";
            string str2 = date;
            string str3 = "1";
            if (c_rep.agent_code == null)
            {
                c_rep.agent_code = "";
            }
            if (c_rep.xname == null)
            {
                c_rep.xname = "";
            }
            if (c_rep.nationality == null)
            {
                c_rep.nationality = "";
            }
            str = this.addCurrentAddress(c_rep_addy, pwalletID, date);
            string connectionString = this.Connect();
            string str5 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO representative (agent_code,xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@agent_code,@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@agent_code", SqlDbType.VarChar);
            command.Parameters.Add("@xname", SqlDbType.NVarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@agent_code"].Value = c_rep.agent_code;
            command.Parameters["@xname"].Value = c_rep.xname;
            command.Parameters["@nationality"].Value = c_rep.nationality;
            command.Parameters["@addressID"].Value = str;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = str2;
            command.Parameters["@visible"].Value = str3;
            str5 = command.ExecuteScalar().ToString();
            connection.Close();
            return str5;
        }

        public string addCurrentTrademark(Applicant c_app, MarkInfo c_mark, AddressService c_aos, Representative c_rep, Address c_app_addy, Address c_rep_addy, string pwallet, string log_officer)
        {
            Stage stage = this.getStageClassByUserID(pwallet);
            string xID = "";
            string date = stage.reg_date;
            string year = stage.reg_date.Substring(0, 4);
            this.addCurrentApplicant(c_app, c_app_addy, pwallet, date);
            xID = this.addCurrentMark(c_mark, pwallet, date);
            this.updateCurrentMarkReg(xID, c_mark.tm_typeID, year);
            this.addCurrentAos(c_aos, pwallet, date);
            this.addCurrentRepresentative(c_rep, c_rep_addy, pwallet, date);
            this.updatePwalletStatus(pwallet, log_officer);
            return xID;
        }

        public string addExcelAddress(Address c_app_addy)
        {
            if (c_app_addy.countryID == null)
            {
                c_app_addy.countryID = "";
            }
            if (c_app_addy.stateID == null)
            {
                c_app_addy.stateID = "";
            }
            if (c_app_addy.city == null)
            {
                c_app_addy.city = "";
            }
            if (c_app_addy.street == null)
            {
                c_app_addy.street = "";
            }
            if (c_app_addy.telephone1 == null)
            {
                c_app_addy.telephone1 = "";
            }
            if (c_app_addy.email1 == null)
            {
                c_app_addy.email1 = "";
            }
            string connectionString = this.Connect();
            string str2 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_app_addy.countryID;
            command.Parameters["@stateID"].Value = c_app_addy.stateID;
            command.Parameters["@city"].Value = c_app_addy.city;
            command.Parameters["@street"].Value = c_app_addy.street;
            command.Parameters["@telephone1"].Value = c_app_addy.telephone1;
            command.Parameters["@email1"].Value = c_app_addy.email1;
            command.Parameters["@log_staff"].Value = c_app_addy.log_staff;
            command.Parameters["@reg_date"].Value = c_app_addy.reg_date;
            command.Parameters["@visible"].Value = c_app_addy.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }

        public string addExcelAos(AddressService c_aos)
        {
            if (c_aos.countryID == null)
            {
                c_aos.countryID = "";
            }
            if (c_aos.stateID == null)
            {
                c_aos.stateID = "";
            }
            if (c_aos.city == null)
            {
                c_aos.city = "";
            }
            if (c_aos.street == null)
            {
                c_aos.street = "";
            }
            if (c_aos.telephone1 == null)
            {
                c_aos.telephone1 = "";
            }
            if (c_aos.email1 == null)
            {
                c_aos.email1 = "";
            }
            string connectionString = this.Connect();
            string str2 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO address_service (countryID,stateID,city,street,telephone1,email1,log_staff,reg_date,visible) VALUES (@countryID,@stateID,@city,@street,@telephone1,@email1,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@countryID", SqlDbType.VarChar, 10);
            command.Parameters.Add("@stateID", SqlDbType.NVarChar, 10);
            command.Parameters.Add("@city", SqlDbType.VarChar, 40);
            command.Parameters.Add("@street", SqlDbType.Text);
            command.Parameters.Add("@telephone1", SqlDbType.NVarChar, 40);
            command.Parameters.Add("@email1", SqlDbType.VarChar, 40);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 40);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 40);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@countryID"].Value = c_aos.countryID;
            command.Parameters["@stateID"].Value = c_aos.stateID;
            command.Parameters["@city"].Value = c_aos.city;
            command.Parameters["@street"].Value = c_aos.street;
            command.Parameters["@telephone1"].Value = c_aos.telephone1;
            command.Parameters["@email1"].Value = c_aos.email1;
            command.Parameters["@log_staff"].Value = c_aos.log_staff;
            command.Parameters["@reg_date"].Value = c_aos.reg_date;
            command.Parameters["@visible"].Value = c_aos.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }

        public string addExcelApplicant(Applicant c_app, Address c_app_addy)
        {
            string str = "0";
            if (c_app.xname == null)
            {
                c_app.xname = "";
            }
            if (c_app.nationality == null)
            {
                c_app.nationality = "";
            }
            str = this.addExcelAddress(c_app_addy);
            string connectionString = this.Connect();
            string str3 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO applicant (xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@xname", SqlDbType.VarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 20);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 10);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@xname"].Value = c_app.xname;
            command.Parameters["@nationality"].Value = c_app.nationality;
            command.Parameters["@addressID"].Value = str;
            command.Parameters["@log_staff"].Value = c_app.log_staff;
            command.Parameters["@reg_date"].Value = c_app.reg_date;
            command.Parameters["@visible"].Value = c_app.visible;
            str3 = command.ExecuteScalar().ToString();
            connection.Close();
            return str3;
        }

        public string addExcelMark(MarkInfo c_mark)
        {
            string str = "";
            if (c_mark.tm_typeID == null)
            {
                c_mark.tm_typeID = "";
            }
            if (c_mark.logo_descriptionID == null)
            {
                c_mark.logo_descriptionID = "";
            }
            if (c_mark.product_title == null)
            {
                c_mark.product_title = "";
            }
            if (c_mark.nice_class == null)
            {
                c_mark.nice_class = "";
            }
            if (c_mark.nice_class_desc == null)
            {
                c_mark.nice_class_desc = "";
            }
            if (c_mark.national_classID == null)
            {
                c_mark.national_classID = "";
            }
            if (c_mark.disclaimer == null)
            {
                c_mark.disclaimer = "";
            }
            string connectionString = this.Connect();
            string str3 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO mark_Info (reg_number,tm_typeID,logo_descriptionID,product_title,nice_class,nice_class_desc,national_classID,disclaimer,log_staff,reg_date,xvisible) VALUES (@reg_number,@tm_typeID,@logo_descriptionID,@product_title,@nice_class,@nice_class_desc,@national_classID,@disclaimer,@log_staff,@reg_date,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@reg_number", SqlDbType.VarChar, 50);
            command.Parameters.Add("@tm_typeID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@logo_descriptionID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@product_title", SqlDbType.VarChar, 50);
            command.Parameters.Add("@nice_class", SqlDbType.VarChar, 50);
            command.Parameters.Add("@nice_class_desc", SqlDbType.NVarChar);
            command.Parameters.Add("@national_classID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@disclaimer", SqlDbType.VarChar);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 50);
            command.Parameters.Add("@xvisible", SqlDbType.VarChar, 1);
            command.Parameters["@reg_number"].Value = str;
            command.Parameters["@tm_typeID"].Value = c_mark.tm_typeID;
            command.Parameters["@logo_descriptionID"].Value = c_mark.logo_descriptionID;
            command.Parameters["@product_title"].Value = c_mark.product_title;
            command.Parameters["@nice_class"].Value = c_mark.nice_class;
            command.Parameters["@nice_class_desc"].Value = c_mark.nice_class_desc;
            command.Parameters["@national_classID"].Value = c_mark.national_classID;
            command.Parameters["@disclaimer"].Value = c_mark.disclaimer;
            command.Parameters["@log_staff"].Value = c_mark.log_staff;
            command.Parameters["@reg_date"].Value = c_mark.reg_date;
            command.Parameters["@xvisible"].Value = c_mark.xvisible;
            str3 = command.ExecuteScalar().ToString();
            connection.Close();
            return str3;
        }

        public string addExcelPwallet(string serverpath, Stage c_pwallet)
        {
            string connectionString = this.Connect();
            this.cleanseTmByValidation(serverpath, c_pwallet.validationID);
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO pwallet (validationID,applicantID,log_officer,amt,stage,status,data_status,reg_date,visible )  VALUES ( @validationID,@applicantID,@log_officer,@amt,@stage,@status,@data_status,@regdate,@visible ) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@validationID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@applicantID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_officer", SqlDbType.VarChar, 50);
            command.Parameters.Add("@amt", SqlDbType.VarChar, 50);
            command.Parameters.Add("@stage", SqlDbType.NChar, 10);
            command.Parameters.Add("@status", SqlDbType.VarChar, 20);
            command.Parameters.Add("@data_status", SqlDbType.VarChar, 50);
            command.Parameters.Add("@regdate", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@validationID"].Value = c_pwallet.validationID;
            command.Parameters["@applicantID"].Value = c_pwallet.applicantID;
            command.Parameters["@log_officer"].Value = c_pwallet.log_officer;
            command.Parameters["@amt"].Value = c_pwallet.amt;
            command.Parameters["@stage"].Value = c_pwallet.stage;
            command.Parameters["@status"].Value = c_pwallet.status;
            command.Parameters["@data_status"].Value = c_pwallet.data_status;
            command.Parameters["@regdate"].Value = c_pwallet.reg_date;
            command.Parameters["@visible"].Value = c_pwallet.visible;
            return command.ExecuteScalar().ToString();
        }

        public string addExcelRepresentative(Representative c_rep, Address c_rep_addy)
        {
            string str = "0";
            if (c_rep.agent_code == null)
            {
                c_rep.agent_code = "";
            }
            if (c_rep.xname == null)
            {
                c_rep.xname = "";
            }
            if (c_rep.nationality == null)
            {
                c_rep.nationality = "";
            }
            str = this.addAddress(c_rep_addy, c_rep_addy.log_staff);
            string connectionString = this.Connect();
            string str3 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO representative (agent_code,xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@agent_code,@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@agent_code", SqlDbType.VarChar);
            command.Parameters.Add("@xname", SqlDbType.NVarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@agent_code"].Value = c_rep.agent_code;
            command.Parameters["@xname"].Value = c_rep.xname;
            command.Parameters["@nationality"].Value = c_rep.nationality;
            command.Parameters["@addressID"].Value = str;
            command.Parameters["@log_staff"].Value = c_rep.log_staff;
            command.Parameters["@reg_date"].Value = c_rep.reg_date;
            command.Parameters["@visible"].Value = c_rep.visible;
            str3 = command.ExecuteScalar().ToString();
            connection.Close();
            return str3;
        }

        public string addMark(MarkInfo c_mark, string pwalletID)
        {
            string str = "";
            if (c_mark.tm_typeID == null)
            {
                c_mark.tm_typeID = "";
            }
            if (c_mark.logo_descriptionID == null)
            {
                c_mark.logo_descriptionID = "";
            }
            if (c_mark.product_title == null)
            {
                c_mark.product_title = "";
            }
            if (c_mark.nice_class == null)
            {
                c_mark.nice_class = "";
            }
            if (c_mark.nice_class_desc == null)
            {
                c_mark.nice_class_desc = "";
            }
            if (c_mark.national_classID == null)
            {
                c_mark.national_classID = "";
            }
            if (c_mark.disclaimer == null)
            {
                c_mark.disclaimer = "";
            }
            string connectionString = this.Connect();
            string str3 = "0";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO mark_Info (reg_number,tm_typeID,logo_descriptionID,product_title,nice_class,nice_class_desc,national_classID,disclaimer,log_staff,reg_date,xvisible) VALUES (@reg_number,@tm_typeID,@logo_descriptionID,@product_title,@nice_class,@nice_class_desc,@national_classID,@disclaimer,@log_staff,@reg_date,@xvisible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@reg_number", SqlDbType.VarChar, 50);
            command.Parameters.Add("@tm_typeID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@logo_descriptionID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@product_title", SqlDbType.NVarChar);
            command.Parameters.Add("@nice_class", SqlDbType.VarChar, 50);
            command.Parameters.Add("@nice_class_desc", SqlDbType.NVarChar);
            command.Parameters.Add("@national_classID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@disclaimer", SqlDbType.VarChar);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 50);
            command.Parameters.Add("@xvisible", SqlDbType.VarChar, 1);
            command.Parameters["@reg_number"].Value = str;
            command.Parameters["@tm_typeID"].Value = c_mark.tm_typeID;
            command.Parameters["@logo_descriptionID"].Value = c_mark.logo_descriptionID;
            command.Parameters["@product_title"].Value = c_mark.product_title;
            command.Parameters["@nice_class"].Value = c_mark.nice_class;
            command.Parameters["@nice_class_desc"].Value = c_mark.nice_class_desc;
            command.Parameters["@national_classID"].Value = c_mark.national_classID;
            command.Parameters["@disclaimer"].Value = c_mark.disclaimer;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = c_mark.reg_date;
            command.Parameters["@xvisible"].Value = c_mark.xvisible;
            str3 = command.ExecuteScalar().ToString();
            connection.Close();
            return str3;
        }

        public string addPwallet(string serverpath, string validationID, string agent_code, string amt, string log_officer)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            string str3 = "1";
            string str4 = "1";
            string str5 = "1";
            string str6 = "Fresh";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO pwallet (validationID,applicantID,log_officer,amt,stage,status,data_status,reg_date,visible )  VALUES ( @validationID,@applicantID,@log_officer,@amt,@stage,@status,@data_status,@regdate,@visible ) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@validationID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@applicantID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_officer", SqlDbType.VarChar, 50);
            command.Parameters.Add("@amt", SqlDbType.VarChar, 50);
            command.Parameters.Add("@stage", SqlDbType.NChar, 10);
            command.Parameters.Add("@status", SqlDbType.VarChar, 20);
            command.Parameters.Add("@data_status", SqlDbType.VarChar, 50);
            command.Parameters.Add("@regdate", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@validationID"].Value = validationID;
            command.Parameters["@applicantID"].Value = agent_code;
            command.Parameters["@log_officer"].Value = log_officer;
            command.Parameters["@amt"].Value = amt;
            command.Parameters["@stage"].Value = str5;
            command.Parameters["@status"].Value = str4;
            command.Parameters["@data_status"].Value = str6;
            command.Parameters["@regdate"].Value = str2;
            command.Parameters["@visible"].Value = str3;
            return command.ExecuteScalar().ToString();
        }

        public string addRepresentative(Representative c_rep, Address c_rep_addy, string pwalletID)
        {
            string str = "0";
            if (c_rep.agent_code == null)
            {
                c_rep.agent_code = "";
            }
            if (c_rep.xname == null)
            {
                c_rep.xname = "";
            }
            if (c_rep.nationality == null)
            {
                c_rep.nationality = "";
            }
            str = this.addAddress(c_rep_addy, pwalletID);
            string connectionString = this.Connect();
            string str3 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO representative (agent_code,xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@agent_code,@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@agent_code", SqlDbType.VarChar);
            command.Parameters.Add("@xname", SqlDbType.NVarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@agent_code"].Value = c_rep.agent_code;
            command.Parameters["@xname"].Value = c_rep.xname;
            command.Parameters["@nationality"].Value = c_rep.nationality;
            command.Parameters["@addressID"].Value = str;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = c_rep.reg_date;
            command.Parameters["@visible"].Value = c_rep.visible;
            str3 = command.ExecuteScalar().ToString();
            connection.Close();
            return str3;
        }

        public int addReversal(TmOffice c_tm_office)
        {
            string connectionString = this.Connect();
            int num = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO tm_office (pwalletID,admin_status,data_status,xcomment,xdoc1,xdoc2,xdoc3,xofficer,reg_date,xvisible) VALUES ('" + c_tm_office.pwalletID + "','" + c_tm_office.admin_status + "','" + c_tm_office.data_status + "','" + c_tm_office.xcomment + "','" + c_tm_office.xdoc1 + "','" + c_tm_office.xdoc2 + "','" + c_tm_office.xdoc3 + "','" + c_tm_office.xofficer + "','" + c_tm_office.reg_date + "','" + c_tm_office.xvisible + "') SELECT SCOPE_IDENTITY()";
            connection.Open();
            num = Convert.ToInt32(command.ExecuteScalar());
            if (num > 0)
            {
                SqlCommand command2 = connection.CreateCommand();
                command2.CommandText = "update pwallet set  status='" + c_tm_office.admin_status + "' ,data_status='" + c_tm_office.data_status + "' where ID='" + c_tm_office.pwalletID + "' ";
                num += command2.ExecuteNonQuery();
            }
            connection.Close();
            return num;
        }

        public string addSoloApplicant(Applicant c_app, string addyID, string pwalletID)
        {
            if (c_app.xname == null)
            {
                c_app.xname = "";
            }
            if (c_app.nationality == null)
            {
                c_app.nationality = "";
            }
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO applicant (xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@xname", SqlDbType.VarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 20);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 10);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@xname"].Value = c_app.xname;
            command.Parameters["@nationality"].Value = c_app.nationality;
            command.Parameters["@addressID"].Value = addyID;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = c_app.reg_date;
            command.Parameters["@visible"].Value = c_app.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }

        public string addSoloRepresentative(Representative c_rep, string addID, string pwalletID)
        {
            if (c_rep.agent_code == null)
            {
                c_rep.agent_code = "";
            }
            if (c_rep.xname == null)
            {
                c_rep.xname = "";
            }
            if (c_rep.nationality == null)
            {
                c_rep.nationality = "";
            }
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO representative (agent_code,xname,nationality,addressID,log_staff,reg_date,visible) VALUES (@agent_code,@xname,@nationality,@addressID,@log_staff,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@agent_code", SqlDbType.VarChar);
            command.Parameters.Add("@xname", SqlDbType.NVarChar);
            command.Parameters.Add("@nationality", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@addressID", SqlDbType.VarChar, 50);
            command.Parameters.Add("@log_staff", SqlDbType.VarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.VarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.VarChar, 1);
            command.Parameters["@agent_code"].Value = c_rep.agent_code;
            command.Parameters["@xname"].Value = c_rep.xname;
            command.Parameters["@nationality"].Value = c_rep.nationality;
            command.Parameters["@addressID"].Value = addID;
            command.Parameters["@log_staff"].Value = pwalletID;
            command.Parameters["@reg_date"].Value = c_rep.reg_date;
            command.Parameters["@visible"].Value = c_rep.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }

        public string addSwallet(SWallet s)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO search_wallet (mark_infoID,search_str,search_cri,xclass,log_officer,reg_date,visible) VALUES (@mark_infoID,@search_str,@search_cri,@xclass,@log_officer,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@mark_infoID", SqlDbType.NVarChar);
            command.Parameters.Add("@search_str", SqlDbType.Text);
            command.Parameters.Add("@search_cri", SqlDbType.Text);
            command.Parameters.Add("@xclass", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@log_officer", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.NVarChar, 1);
            command.Parameters["@mark_infoID"].Value = s.mark_infoID;
            command.Parameters["@search_str"].Value = s.search_str;
            command.Parameters["@search_cri"].Value = s.search_cri;
            command.Parameters["@xclass"].Value = s.xclass;
            command.Parameters["@log_officer"].Value = s.log_officer;
            command.Parameters["@reg_date"].Value = s.reg_date;
            command.Parameters["@visible"].Value = s.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }

        public string addSwallet2(SWallet s)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO search_wallet2 (mark_infoID,search_str,search_cri,xclass,log_officer,reg_date,visible) VALUES (@mark_infoID,@search_str,@search_cri,@xclass,@log_officer,@reg_date,@visible) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@mark_infoID", SqlDbType.NVarChar);
            command.Parameters.Add("@search_str", SqlDbType.Text);
            command.Parameters.Add("@search_cri", SqlDbType.Text);
            command.Parameters.Add("@xclass", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@log_officer", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@reg_date", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@visible", SqlDbType.NVarChar, 1);
            command.Parameters["@mark_infoID"].Value = s.mark_infoID;
            command.Parameters["@search_str"].Value = s.search_str;
            command.Parameters["@search_cri"].Value = s.search_cri;
            command.Parameters["@xclass"].Value = s.xclass;
            command.Parameters["@log_officer"].Value = s.log_officer;
            command.Parameters["@reg_date"].Value = s.reg_date;
            command.Parameters["@visible"].Value = s.visible;
            str2 = command.ExecuteScalar().ToString();
            connection.Close();
            return str2;
        }

        public string addTrademark(Applicant c_app, MarkInfo c_mark, AddressService c_aos, Representative c_rep, Address c_app_addy, Address c_rep_addy, string pwallet, string log_officer)
        {
            string xID = "";
            this.addApplicant(c_app, c_app_addy, pwallet);
            xID = this.addMark(c_mark, pwallet);
            this.updateMarkReg(xID, c_mark.tm_typeID);
            this.addAos(c_aos, pwallet);
            this.addRepresentative(c_rep, c_rep_addy, pwallet);
            this.updatePwalletStatus(pwallet, log_officer);
            return xID;
        }

        public int addXTransaction(XTransaction x)
        {
            string connectionString = this.Connect();
            int num = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO xtransactions (transactionID,xcode,ptype,amt,status,adminID) VALUES (@transactionID,@xcode,@ptype,@amt,@status,@adminID) SELECT SCOPE_IDENTITY()";
            connection.Open();
            command.Parameters.Add("@transactionID", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@xcode", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@ptype", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@amt", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@status", SqlDbType.NVarChar, 1);
            command.Parameters.Add("@adminID", SqlDbType.NVarChar, 50);
            command.Parameters["@transactionID"].Value = x.transactionID;
            command.Parameters["@xcode"].Value = x.xcode;
            command.Parameters["@ptype"].Value = x.ptype;
            command.Parameters["@amt"].Value = x.amt;
            command.Parameters["@status"].Value = x.status;
            command.Parameters["@adminID"].Value = x.adminID;
            num = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return num;
        }

        public int checkValidation(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command = new SqlCommand("sp_s_IsValid", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@id", id));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            return num;
        }

        public int CheckXTransactionID(string transactionID)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM xtransactions WHERE transactionID='" + transactionID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["ID"].ToString());
            }
            reader.Close();
            return num;
        }

        public void cleanseTmByValidation(string serverpath, string vid)
        {
            string id = "0";
            string connectionString = this.Connect();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * from pwallet where validationID='" + vid + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                reader["stage"].ToString();
                id = reader["ID"].ToString();
            }
            reader.Close();
            if (id != "0")
            {
                SqlConnection connection2 = new SqlConnection(connectionString);
                SqlCommand command2 = new SqlCommand("DELETE from pwallet where validationID='" + vid + "'", connection2);
                connection2.Open();
                command2.ExecuteNonQuery();
                connection2.Close();
                this.flushApplicant(id);
                this.flushMark_info(serverpath, id);
                this.flushAddress_service(id);
                this.flushRepresentative(id);
                this.flushAddress(id);
            }
        }

        public string Connect()
        {
            return ConfigurationManager.ConnectionStrings["cldConnectionString"].ConnectionString;
        }
        public string ConnectPt()
        {
            return ConfigurationManager.ConnectionStrings["ptConnectionString"].ConnectionString;
        }
        public string ConnectDs()
        {
            return ConfigurationManager.ConnectionStrings["dsConnectionString"].ConnectionString;
        }
        public void doDeleteDir(string serverpath, long markID)
        {
            if (markID > 0L)
            {
                string path = serverpath + "admin/tm/docz/" + markID.ToString() + "/";
                string str2 = serverpath + "admin/tm/Picz/" + markID.ToString() + "/";
                try
                {
                    if (Directory.Exists(path))
                    {
                        foreach (string str3 in Directory.GetFiles(path))
                        {
                            File.Delete(str3);
                        }
                    }
                }
                catch (Exception)
                {
                }
                try
                {
                    if (Directory.Exists(str2))
                    {
                        foreach (string str4 in Directory.GetFiles(str2))
                        {
                            File.Delete(str4);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void doDeleteExcelDir(string filepath)
        {
            try
            {
                if (Directory.Exists(filepath))
                {
                    foreach (string str in Directory.GetFiles(filepath))
                    {
                        File.Delete(str);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public string doUploadDoc(string ID, string path, FileUpload fu)
        {
            string str = "";
            try
            {
                if (!Directory.Exists(path + ID + "/"))
                {
                    Directory.CreateDirectory(path + ID + "/");
                }
                string str2 = Path.GetFileName(fu.FileName).Replace(" ", "_");
                fu.SaveAs(path + ID + "/" + str2);
                return (str = path + ID + "/" + str2);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string doUploadDocNoLimit(string ID, string path, FileUpload fu)
        {
            string str = "";
            try
            {
                if (!Directory.Exists(path + ID + "/"))
                {
                    Directory.CreateDirectory(path + ID + "/");
                }
                string str2 = Path.GetFileName(fu.FileName).Replace(" ", "_");
                fu.SaveAs(path + ID + "/" + str2);
                return (str = path + ID + "/" + str2);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string doUploadExcelDoc(string ID, string path, FileUpload fu)
        {
            string str = "";
            string destFileName = "";
            try
            {
                if (!Directory.Exists(path + ID + "/"))
                {
                    Directory.CreateDirectory(path + ID + "/");
                }
                string filename = Path.GetFileName(fu.FileName).Replace(" ", "_");
                string str4 = this.getExtension(filename);
                if (str4 == "xls")
                {
                    fu.SaveAs(path + ID + "/" + filename);
                    str = DateTime.Now.TimeOfDay.ToString().Replace(".", "_").Replace(":", "+").Substring(0, 8);
                    destFileName = path + ID + "/" + str + "." + str4;
                    File.Move(path + ID + "/" + filename, destFileName);
                    return destFileName;
                }
                return "bformat";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string doUploadPic(string ID, string newfilename, string path, FileUpload fu)
        {
            try
            {
                if (!Directory.Exists(path + ID + "/"))
                {
                    Directory.CreateDirectory(path + ID + "/");
                }
                newfilename = newfilename.Replace(" ", "_");
                fu.SaveAs(path + ID + "/" + newfilename);
                return (path + ID + "/" + newfilename);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string doUploadUpDoc(string ID, string path, string oldpath, FileUpload fu)
        {
            if (ID != "")
            {
                string filename = "";
                string str2 = Path.GetFileName(fu.FileName).Replace(" ", "_");
                filename = filename = path + ID + "/" + str2;
                try
                {
                    if (!Directory.Exists(path + ID + "/"))
                    {
                        Directory.CreateDirectory(path + ID + "/");
                    }
                    else if (File.Exists(oldpath))
                    {
                        File.Delete(oldpath);
                    }
                    fu.SaveAs(filename);
                }
                catch (Exception)
                {
                }
            }
            return "";
        }

        public int e_MarkInfoDocz(string logo_pic, string auth_doc, string sup_doc1, string sup_doc2, string mark_infoID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command = new SqlCommand("sp_u_MarkInfoWithDocz", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@logo_pic", logo_pic));
            command.Parameters.Add(new SqlParameter("@auth_doc", auth_doc));
            command.Parameters.Add(new SqlParameter("@sup_doc1", sup_doc1));
            command.Parameters.Add(new SqlParameter("@sup_doc2", sup_doc2));
            command.Parameters.Add(new SqlParameter("@ID", mark_infoID));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            return num;
        }

        public int e_MarkInfoRegNumber(string reg_number, string pwalletID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command = new SqlCommand("sp_u_MarkInfoRegNumber", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@reg_number", reg_number));
            command.Parameters.Add(new SqlParameter("@ID", pwalletID));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            return num;
        }

        public int e_markpwallet(string validationID, string agentID, string amt, string stage, string data_status)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            int num = 0;
            this.checkValidation(validationID);
            string str3 = "1";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("sp_a_pwallet", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@validationID", validationID));
            command.Parameters.Add(new SqlParameter("@applicantID", agentID));
            command.Parameters.Add(new SqlParameter("@amt", amt));
            command.Parameters.Add(new SqlParameter("@stage", stage));
            command.Parameters.Add(new SqlParameter("@data_status", data_status));
            command.Parameters.Add(new SqlParameter("@reg_date", str2));
            command.Parameters.Add(new SqlParameter("@visible", str3));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            num = (int) parameter.Value;
            connection.Close();
            return num;
        }

        public int e_pwallet(string validationID, string agentID, string amt, string stage, string data_status)
        {
            string connectionString = this.Connect();
            string str2 = DateTime.Today.Date.ToString("yyyy-MM-dd");
            int num = 0;
            if (this.checkValidation(validationID) > 0)
            {
                string str3 = this.getPwalletID(validationID);
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("sp_u_PwalletStage", connection) {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@id", str3));
                command.Parameters.Add(new SqlParameter("@applicantID", agentID));
                command.Parameters.Add(new SqlParameter("@amt", amt));
                command.Parameters.Add(new SqlParameter("@stage", stage));
                command.Parameters.Add(new SqlParameter("@data_status", data_status));
                SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                parameter.Direction = ParameterDirection.ReturnValue;
                command.ExecuteNonQuery();
                num = (int) parameter.Value;
                connection.Close();
                return num;
            }
            string str4 = "1";
            SqlConnection connection2 = new SqlConnection(connectionString);
            connection2.Open();
            SqlCommand command3 = new SqlCommand("sp_a_pwallet", connection2) {
                CommandType = CommandType.StoredProcedure
            };
            command3.Parameters.Add(new SqlParameter("@validationID", validationID));
            command3.Parameters.Add(new SqlParameter("@applicantID", agentID));
            command3.Parameters.Add(new SqlParameter("@amt", amt));
            command3.Parameters.Add(new SqlParameter("@stage", stage));
            command3.Parameters.Add(new SqlParameter("@data_status", data_status));
            command3.Parameters.Add(new SqlParameter("@reg_date", str2));
            command3.Parameters.Add(new SqlParameter("@visible", str4));
            SqlParameter parameter2 = command3.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter2.Direction = ParameterDirection.ReturnValue;
            command3.ExecuteNonQuery();
            num = (int) parameter2.Value;
            connection2.Close();
            return num;
        }

        public int e_PwalletStatus(string xID, string status, string data_status)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command = new SqlCommand("sp_u_PwalletStatus", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@ID", xID));
            command.Parameters.Add(new SqlParameter("@status", status));
            command.Parameters.Add(new SqlParameter("@data_status", data_status));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            if (num > 0)
            {
                num = Convert.ToInt32(xID);
            }
            return num;
        }

        public int e_xscard(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            connection.Open();
            SqlCommand command = new SqlCommand("sp_u_XScardStatus", connection) {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@id", id));
            SqlParameter parameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
            parameter.Direction = ParameterDirection.ReturnValue;
            command.ExecuteNonQuery();
            int num = (int) parameter.Value;
            connection.Close();
            return num;
        }

        public void flushAddress(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushAddress_service(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address_service where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushAddress_serviceByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address_service where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushAddressByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from address where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushApplicant(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from applicant where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushApplicantByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from applicant where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushMark_info(string serverpath, string id)
        {
            long markID = 0L;
            string connectionString = this.Connect();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT xID from mark_info where log_staff='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                markID = Convert.ToInt64(reader["xID"]);
            }
            reader.Close();
            SqlConnection connection2 = new SqlConnection(connectionString);
            SqlCommand command2 = new SqlCommand("DELETE from mark_info where log_staff='" + id + "'", connection2);
            connection2.Open();
            command2.ExecuteNonQuery();
            connection2.Close();
            if (markID > 0L)
            {
                this.doDeleteDir(serverpath, markID);
            }
        }

        public void flushMark_infoByID(string serverpath, string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from mark_info where xID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            if (Convert.ToInt64(id) > 0L)
            {
                this.doDeleteDir(serverpath, Convert.ToInt64(id));
            }
        }

        public void flushPwalletByID(string id)
        {
            string connectionString = this.Connect();
            if ((id != "0") && (id != ""))
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("DELETE from pwallet where ID='" + id + "'", connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void flushRepresentative(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from representative where log_staff='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void flushRepresentativeByID(string id)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("DELETE from representative where ID='" + id + "'", connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string formatExcelDate(string date)
        {
            if ((date == "0") || (date == null))
            {
                date = DateTime.Today.Date.ToString("yyyy-MM-dd");
            }
            return date;
        }

        public List<Address> getAddressByID(string ID)
        {
            List<Address> list = new List<Address>();
            new Address();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Address item = new Address {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = reader["city"].ToString(),
                    street = reader["street"].ToString(),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = reader["email1"].ToString(),
                    email2 = reader["email2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Address getAddressByLog_staffID(string pwalletID)
        {
            Address address = new Address();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE log_staff='" + pwalletID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                address.ID = reader["ID"].ToString();
                address.countryID = reader["countryID"].ToString();
                address.stateID = reader["stateID"].ToString();
                address.lgaID = reader["lgaID"].ToString();
                address.city = reader["city"].ToString();
                address.street = reader["street"].ToString();
                address.zip = reader["zip"].ToString();
                address.telephone1 = reader["telephone1"].ToString();
                address.telephone2 = reader["telephone2"].ToString();
                address.email1 = reader["email1"].ToString();
                address.email2 = reader["email2"].ToString();
                address.zip = reader["zip"].ToString();
                address.log_staff = reader["log_staff"].ToString();
                address.reg_date = reader["reg_date"].ToString();
                address.visible = reader["visible"].ToString();
            }
            reader.Close();
            return address;
        }

        public Address getAddressClassByID(string ID)
        {
            Address address = new Address {
                ID = Convert.ToInt64("0").ToString(),
                countryID = "",
                stateID = "",
                lgaID = "",
                city = "",
                street = "",
                zip = "",
                telephone1 = "",
                telephone2 = "",
                email1 = "",
                email2 = "",
                log_staff = "",
                reg_date = "",
                visible = ""
            };
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                address.ID = Convert.ToInt64(reader["ID"]).ToString();
                address.countryID = reader["countryID"].ToString();
                address.stateID = reader["stateID"].ToString();
                address.lgaID = reader["lgaID"].ToString();
                address.city = reader["city"].ToString();
                address.street = reader["street"].ToString();
                address.zip = reader["zip"].ToString();
                address.telephone1 = reader["telephone1"].ToString();
                address.telephone2 = reader["telephone2"].ToString();
                address.email1 = reader["email1"].ToString();
                address.email2 = reader["email2"].ToString();
                address.zip = reader["zip"].ToString();
                address.log_staff = reader["log_staff"].ToString();
                address.reg_date = reader["reg_date"].ToString();
                address.visible = reader["visible"].ToString();
            }
            reader.Close();
            return address;
        }

        public List<Address> getAddressListByID(string ID)
        {
            List<Address> list = new List<Address>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Address item = new Address {
                    ID = reader["ID"].ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = reader["city"].ToString(),
                    street = reader["street"].ToString(),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = reader["email1"].ToString(),
                    email2 = reader["email2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Address> getAddressListByValidationID(string log_staff)
        {
            List<Address> list = new List<Address>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE log_staff='" + log_staff + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Address item = new Address {
                    ID = reader["ID"].ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = reader["city"].ToString(),
                    street = reader["street"].ToString(),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = reader["email1"].ToString(),
                    email2 = reader["email2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public AddressService getAddressServiceByID(string ID)
        {
            AddressService service = new AddressService {
                ID = Convert.ToInt64("0").ToString(),
                countryID = "",
                stateID = "",
                lgaID = "",
                city = "",
                street = "",
                zip = "",
                telephone1 = "",
                telephone2 = "",
                email1 = "",
                email2 = "",
                log_staff = "",
                reg_date = "",
                visible = ""
            };
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address_service WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                service.ID = Convert.ToInt64(reader["ID"]).ToString();
                service.countryID = reader["countryID"].ToString();
                service.stateID = reader["stateID"].ToString();
                service.lgaID = reader["lgaID"].ToString();
                service.city = reader["city"].ToString();
                service.street = reader["street"].ToString();
                service.zip = reader["zip"].ToString();
                service.telephone1 = reader["telephone1"].ToString();
                service.telephone2 = reader["telephone2"].ToString();
                service.email1 = reader["email1"].ToString();
                service.email2 = reader["email2"].ToString();
                service.zip = reader["zip"].ToString();
                service.log_staff = reader["log_staff"].ToString();
                service.reg_date = reader["reg_date"].ToString();
                service.visible = reader["visible"].ToString();
            }
            reader.Close();
            return service;
        }

        public List<AddressService> getAddressServiceListByID(string ID)
        {
            List<AddressService> list = new List<AddressService>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address_service WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                AddressService item = new AddressService {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    countryID = reader["countryID"].ToString(),
                    stateID = reader["stateID"].ToString(),
                    lgaID = reader["lgaID"].ToString(),
                    city = reader["city"].ToString(),
                    street = reader["street"].ToString(),
                    zip = reader["zip"].ToString(),
                    telephone1 = reader["telephone1"].ToString(),
                    telephone2 = reader["telephone2"].ToString(),
                    email1 = reader["email1"].ToString(),
                    email2 = reader["email2"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public bool getAdminExtension(string filename)
        {
            string str = "";
            bool flag = false;
            int num = filename.LastIndexOf(".");
            str = filename.Substring(num + 1);
            if ((((str != "pdf") && (str != "jpg")) && ((str != "jpeg") && (str != "PDF"))) && ((str != "JPG") && (str != "JPEG")))
            {
                return flag;
            }
            return true;
        }

        public string getAgentEmail1ByID(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT email1 FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["email1"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getAgentTelephone1ByID(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT telephone1 FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["telephone1"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<Applicant> getApplicantByRefID(string ID)
        {
            List<Applicant> list = new List<Applicant>();
            new Applicant();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("select * from applicant where log_staff IN (select ID from pwallet where validationID='" + ID + "'') ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Applicant item = new Applicant {
                    ID = reader["ID"].ToString(),
                    xtype = reader["xtype"].ToString(),
                    xname = reader["xname"].ToString(),
                    tax_id_type = reader["tax_id_type"].ToString(),
                    tax_id_number = reader["tax_id_number"].ToString(),
                    individual_id_number = reader["individual_id_number"].ToString(),
                    nationality = reader["nationality"].ToString(),
                    addressID = reader["addressID"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Applicant getApplicantByUserID(string ID)
        {
            Applicant applicant = new Applicant {
                ID = "",
                xtype = "",
                xname = "",
                tax_id_type = "",
                tax_id_number = "",
                individual_id_number = "",
                nationality = "",
                addressID = "",
                log_staff = "",
                reg_date = "",
                visible = ""
            };
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                applicant.ID = reader["ID"].ToString();
                applicant.xtype = reader["xtype"].ToString();
                applicant.xname = reader["xname"].ToString();
                applicant.tax_id_type = reader["tax_id_type"].ToString();
                applicant.tax_id_number = reader["tax_id_number"].ToString();
                applicant.individual_id_number = reader["individual_id_number"].ToString();
                applicant.nationality = reader["nationality"].ToString();
                applicant.addressID = reader["addressID"].ToString();
                applicant.log_staff = reader["log_staff"].ToString();
                applicant.reg_date = reader["reg_date"].ToString();
                applicant.visible = reader["visible"].ToString();
            }
            reader.Close();
            return applicant;
        }

        public List<Applicant> getApplicantByvalidationID(string validationID)
        {
            List<Applicant> list = new List<Applicant>();
            new Applicant();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE log_staff='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Applicant item = new Applicant {
                    ID = reader["ID"].ToString(),
                    xtype = reader["xtype"].ToString(),
                    xname = reader["xname"].ToString(),
                    tax_id_type = reader["tax_id_type"].ToString(),
                    tax_id_number = reader["tax_id_number"].ToString(),
                    individual_id_number = reader["individual_id_number"].ToString(),
                    nationality = reader["nationality"].ToString(),
                    addressID = reader["addressID"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Applicant getApplicantClassByID(string pwalletID)
        {
            Applicant applicant = new Applicant();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE log_staff='" + pwalletID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                applicant.ID = reader["ID"].ToString();
                applicant.xtype = reader["xtype"].ToString();
                applicant.xname = reader["xname"].ToString();
                applicant.tax_id_type = reader["tax_id_type"].ToString();
                applicant.tax_id_number = reader["tax_id_number"].ToString();
                applicant.individual_id_number = reader["individual_id_number"].ToString();
                applicant.nationality = reader["nationality"].ToString();
                applicant.addressID = reader["addressID"].ToString();
                applicant.log_staff = reader["log_staff"].ToString();
                applicant.reg_date = reader["reg_date"].ToString();
                applicant.visible = reader["visible"].ToString();
            }
            reader.Close();
            return applicant;
        }

        public List<Applicant> getApplicantListByUserID(string ID)
        {
            List<Applicant> list = new List<Applicant>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM applicant WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Applicant item = new Applicant {
                    ID = reader["ID"].ToString(),
                    xtype = reader["xtype"].ToString(),
                    xname = reader["xname"].ToString(),
                    tax_id_type = reader["tax_id_type"].ToString(),
                    tax_id_number = reader["tax_id_number"].ToString(),
                    individual_id_number = reader["individual_id_number"].ToString(),
                    nationality = reader["nationality"].ToString(),
                    addressID = reader["addressID"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getCheckStatusDetails(string validationID, string agentID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM pwallet WHERE validationID='" + validationID + "'  AND applicantID='" + agentID + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = Convert.ToInt64(reader["ID"]).ToString();
            }
            reader.Close();
            return str;
        }

        public int getCheckTransactionID(string transactionID, string amt, string ptype, string xcode)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM xtransactions WHERE transactionID='" + transactionID + "' AND amt='" + amt + "' AND ptype='" + ptype + "' AND xcode='" + xcode + "'  AND status='0' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["ID"].ToString());
            }
            reader.Close();
            return num;
        }

        public string getClientNumber()
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT TOP 1 ID,pin FROM xscard where usedstatus='0'";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["ID"].ToString() + "_" + reader["pin"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<Country> getCountry()
        {
            List<Country> list = new List<Country>();
            new Country();
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT * FROM country";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Country item = new Country {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    name = reader["name"].ToString(),
                    code = reader["code"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getCountryIDByCode(string code)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM country WHERE code='" + code + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["ID"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getCountryName(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT name FROM country WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["name"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getExtension(string filename)
        {
            int num = filename.LastIndexOf(".");
            return filename.Substring(num + 1);
        }

        public string getFormattedAddressByID(string ID)
        {
            string str = "";
            Address address = new Address();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM address WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                address.ID = reader["ID"].ToString();
                address.countryID = reader["countryID"].ToString();
                address.stateID = reader["stateID"].ToString();
                address.lgaID = reader["lgaID"].ToString();
                address.city = reader["city"].ToString();
                address.street = reader["street"].ToString();
                address.zip = reader["zip"].ToString();
                address.telephone1 = reader["telephone1"].ToString();
                address.telephone2 = reader["telephone2"].ToString();
                address.email1 = reader["email1"].ToString();
                address.email2 = reader["email2"].ToString();
                address.zip = reader["zip"].ToString();
                address.log_staff = reader["log_staff"].ToString();
                address.reg_date = reader["reg_date"].ToString();
                address.visible = reader["visible"].ToString();
            }
            reader.Close();
            if ((address.stateID != null) || (address.stateID != ""))
            {
                if (address.countryID != "160")
                {
                    string str2 = str;
                    return (str2 + address.street + "," + address.city + "," + this.getCountryName(address.countryID));
                }
                string str3 = str;
                return (str3 + address.street + "," + address.city + "," + this.getStateName(address.stateID) + "," + this.getCountryName(address.countryID));
            }
            string str4 = str;
            return (str4 + address.street + "," + address.city + "," + this.getCountryName(address.countryID));
        }

        public List<NClass> getJNationalClasses()
        {
            List<NClass> list = new List<NClass>();
            new NClass();
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT xID,type,description FROM national_classes";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                NClass item = new NClass {
                    xID = Convert.ToInt64(reader["xID"]).ToString(),
                    xtype = reader["type"].ToString(),
                    xdescription = reader["description"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Lga> getLga()
        {
            List<Lga> list = new List<Lga>();
            new Lga();
            SqlConnection connection = new SqlConnection(this.Connect());
            string cmdText = "SELECT * FROM lga";
            SqlCommand command = new SqlCommand(cmdText, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Lga item = new Lga {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    name = reader["name"].ToString(),
                    stateID = reader["stateID"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getLogoDescriptionID(string name)
        {
            if (name == "NAME AND DEVICE")
            {
                name = "WORD AND DEVICE";
            }
            if ((name == "MARK NAME") || (name == "MARK ONLY"))
            {
                name = "WORD MARK";
            }
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT xID from logo_description where type='" + name + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["xID"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getLogoDescriptionName(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT type from logo_description where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["type"].ToString();
            }
            reader.Close();
            return str;
        }

        public List<MarkInfo> getMarkInfoByUserID(string ID)
        {
            List<MarkInfo> list = new List<MarkInfo>();
            new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM mark_info WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                MarkInfo item = new MarkInfo {
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
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public MarkInfo getMarkInfoClassByUserID(string ID)
        {
            MarkInfo info = new MarkInfo();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM mark_info WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                info.xID = reader["xID"].ToString();
                info.reg_number = reader["reg_number"].ToString();
                info.tm_typeID = reader["tm_typeID"].ToString();
                info.logo_descriptionID = reader["logo_descriptionID"].ToString();
                info.national_classID = reader["national_classID"].ToString();
                info.product_title = reader["product_title"].ToString();
                info.nice_class = reader["nice_class"].ToString();
                info.nice_class_desc = reader["nice_class_desc"].ToString();
                info.sign_type = reader["sign_type"].ToString();
                info.vienna_class = reader["vienna_class"].ToString();
                info.disclaimer = reader["disclaimer"].ToString();
                info.logo_pic = reader["logo_pic"].ToString();
                info.auth_doc = reader["auth_doc"].ToString();
                info.sup_doc1 = reader["sup_doc1"].ToString();
                info.sup_doc2 = reader["sup_doc2"].ToString();
                info.log_staff = reader["log_staff"].ToString();
                info.reg_date = reader["reg_date"].ToString();
                info.xvisible = reader["xvisible"].ToString();
            }
            reader.Close();
            return info;
        }

        public string getMarkPwalletID(string validationID, string agentID, string amt, string stage, string data_status)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM pwallet WHERE validationID='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = Convert.ToInt64(reader["ID"]).ToString();
            }
            reader.Close();
            if (str == "")
            {
                str = this.e_markpwallet(validationID, agentID, amt, stage, data_status).ToString();
            }
            return str;
        }

        public string getNationalClassDesc(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT description from national_classes where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["description"].ToString();
            }
            reader.Close();
            return str;
        }

        public string getPwalletID(string validationID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM pwallet WHERE validationID='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = Convert.ToInt64(reader["ID"]).ToString();
            }
            reader.Close();
            return str;
        }

        public Representative getRepByUserID(string ID)
        {
            Representative representative = new Representative {
                ID = "",
                agent_code = "",
                xname = "",
                individual_id_type = "",
                individual_id_number = "",
                nationality = "",
                addressID = "",
                log_staff = "",
                reg_date = "",
                visible = ""
            };
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM representative WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                representative.ID = reader["ID"].ToString();
                representative.agent_code = reader["agent_code"].ToString();
                representative.xname = reader["xname"].ToString();
                representative.individual_id_type = reader["individual_id_type"].ToString();
                representative.individual_id_number = reader["individual_id_number"].ToString();
                representative.nationality = reader["nationality"].ToString();
                representative.addressID = reader["addressID"].ToString();
                representative.log_staff = reader["log_staff"].ToString();
                representative.reg_date = reader["reg_date"].ToString();
                representative.visible = reader["visible"].ToString();
            }
            reader.Close();
            return representative;
        }

        public Representative getRepClassByUserID(string ID)
        {
            Representative representative = new Representative();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM representative WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                representative.ID = reader["ID"].ToString();
                representative.agent_code = reader["agent_code"].ToString();
                representative.xname = reader["xname"].ToString();
                representative.individual_id_type = reader["individual_id_type"].ToString();
                representative.individual_id_number = reader["individual_id_number"].ToString();
                representative.nationality = reader["nationality"].ToString();
                representative.addressID = reader["addressID"].ToString();
                representative.log_staff = reader["log_staff"].ToString();
                representative.reg_date = reader["reg_date"].ToString();
                representative.visible = reader["visible"].ToString();
            }
            reader.Close();
            return representative;
        }

        public List<Representative> getRepListByUserID(string ID)
        {
            List<Representative> list = new List<Representative>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM representative WHERE log_staff='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Representative item = new Representative {
                    ID = reader["ID"].ToString(),
                    agent_code = reader["agent_code"].ToString(),
                    xname = reader["xname"].ToString(),
                    individual_id_type = reader["individual_id_type"].ToString(),
                    individual_id_number = reader["individual_id_number"].ToString(),
                    nationality = reader["nationality"].ToString(),
                    addressID = reader["addressID"].ToString(),
                    log_staff = reader["log_staff"].ToString(),
                    reg_date = reader["reg_date"].ToString(),
                    visible = reader["visible"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Stage> getStageByClientIDAcc(string validationID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'  AND stage='5' AND data_status <>'' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }
        public List<Stage> getTmGenStageByClientIDAcc(string validationID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM g_pwallet WHERE validationID='" + validationID + "'  AND stage='5' AND data_status <>'' ", connection);
            command.CommandTimeout = 300;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Stage> getPtStageByClientIDAcc(string validationID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.ConnectPt());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'  AND stage='5' AND data_status <>'' ", connection);
            command.CommandTimeout = 300;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Stage> getPtRenStageByClientIDAcc(string validationID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.ConnectPt());
            SqlCommand command = new SqlCommand("SELECT * FROM x_pwallet WHERE validationID='" + validationID + "'  AND stage='5' AND data_status <>'' ", connection);
            command.CommandTimeout = 300;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Stage> getDsStageByClientIDAcc(string validationID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.ConnectDs());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'  AND stage='5' AND data_status <>'' ", connection);
            command.CommandTimeout = 300;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage
                {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }
        public List<Stage> getStageByUserID(string ID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Stage> getStageByUserIDAcc(string validationID, string agentID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "'  AND applicantID='" + agentID + "' AND stage='5' AND data_status <>'' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public List<Stage> getStageByValidationID(string validationID)
        {
            List<Stage> list = new List<Stage>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE validationID='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Stage item = new Stage {
                    ID = reader["ID"].ToString(),
                    applicantID = reader["applicantID"].ToString(),
                    validationID = reader["validationID"].ToString(),
                    stage = reader["stage"].ToString(),
                    status = reader["status"].ToString(),
                    data_status = reader["data_status"].ToString(),
                    amt = reader["amt"].ToString(),
                    reg_date = reader["reg_date"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public Stage getStageClassByUserID(string ID)
        {
            Stage stage = new Stage();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                stage.ID = reader["ID"].ToString();
                stage.applicantID = reader["applicantID"].ToString();
                stage.validationID = reader["validationID"].ToString();
                stage.stage = reader["stage"].ToString();
                stage.status = reader["status"].ToString();
                stage.data_status = reader["data_status"].ToString();
                stage.amt = reader["amt"].ToString();
                stage.reg_date = reader["reg_date"].ToString();
            }
            reader.Close();
            return stage;
        }

        public int getStageIDByvalidationID(string validationID)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT ID FROM pwallet WHERE validationID='" + validationID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["ID"]);
            }
            reader.Close();
            return num;
        }

        public List<State> getState(string countryID)
        {
            List<State> list = new List<State>();
            new State();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM state WHERE countryID='" + countryID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                State item = new State {
                    ID = Convert.ToInt64(reader["ID"]).ToString(),
                    name = reader["name"].ToString(),
                    countryID = reader["countryID"].ToString()
                };
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getStateName(string ID)
        {
            string str = "";
            try
            {
                SqlConnection connection = new SqlConnection(this.Connect());
                SqlCommand command = new SqlCommand("SELECT name FROM state WHERE ID='" + Convert.ToInt64(ID) + "' ", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    str = reader["name"].ToString();
                }
                reader.Close();
            }
            catch (FormatException)
            {
            }
            finally
            {
                if (str == "")
                {
                    str = "N/A";
                }
            }
            return str;
        }

        public SWallet getSwalletByID(string ID)
        {
            SWallet wallet = new SWallet();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM search_wallet WHERE mark_infoID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                wallet.ID = Convert.ToInt64(reader["ID"]).ToString();
                wallet.mark_infoID = reader["mark_infoID"].ToString();
                wallet.search_cri = reader["search_cri"].ToString();
                wallet.search_str = reader["search_str"].ToString();
                wallet.xclass = reader["xclass"].ToString();
                wallet.log_officer = reader["log_officer"].ToString();
                wallet.reg_date = reader["reg_date"].ToString();
                wallet.visible = reader["visible"].ToString();
            }
            reader.Close();
            return wallet;
        }

        public SWallet getSwalletByID2(string ID)
        {
            SWallet wallet = new SWallet();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM search_wallet2 WHERE mark_infoID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                wallet.ID = Convert.ToInt64(reader["ID"]).ToString();
                wallet.mark_infoID = reader["mark_infoID"].ToString();
                wallet.search_cri = reader["search_cri"].ToString();
                wallet.search_str = reader["search_str"].ToString();
                wallet.xclass = reader["xclass"].ToString();
                wallet.log_officer = reader["log_officer"].ToString();
                wallet.reg_date = reader["reg_date"].ToString();
                wallet.visible = reader["visible"].ToString();
            }
            reader.Close();
            return wallet;
        }

        public List<TmOffice> getTmOfficeDetailsByID(string ID)
        {
            List<TmOffice> list = new List<TmOffice>();
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT * FROM tm_office WHERE pwalletID='" + ID + "' order by ID desc ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                TmOffice item = new TmOffice {
                    ID = "",
                    pwalletID = "",
                    admin_status = "",
                    data_status = "",
                    xcomment = "",
                    xdoc1 = "",
                    xdoc2 = "",
                    xdoc3 = "",
                    xofficer = "",
                    reg_date = "",
                    xvisible = ""
                };
                item.ID = reader["ID"].ToString();
                item.pwalletID = reader["pwalletID"].ToString();
                item.admin_status = reader["admin_status"].ToString();
                item.data_status = reader["data_status"].ToString();
                item.xcomment = reader["xcomment"].ToString();
                item.xdoc1 = reader["xdoc1"].ToString();
                item.xdoc2 = reader["xdoc2"].ToString();
                item.xdoc3 = reader["xdoc3"].ToString();
                item.xofficer = reader["xofficer"].ToString();
                item.reg_date = reader["reg_date"].ToString();
                item.xvisible = reader["xvisible"].ToString();
                list.Add(item);
            }
            reader.Close();
            return list;
        }

        public string getTmTypeName(string id)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT type from tm_type where xID='" + id + "'", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["type"].ToString();
            }
            reader.Close();
            return str;
        }

        public string updateCurrentMarkReg(string xID, string typ, string year)
        {
            string str = "0";
            string str2 = "";
            if (typ == "1")
            {
                str2 = "NG/TM/O/" + year + "/" + xID;
            }
            else
            {
                str2 = "F/TM/O/" + year + "/" + xID;
            }
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET reg_number=@reg_number WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters.Add("@reg_number", SqlDbType.NVarChar, 50);
            command.Parameters["@xID"].Value = Convert.ToInt64(xID);
            command.Parameters["@reg_number"].Value = str2;
            str = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str;
        }

        public int UpdateMarkAuth(string auth_doc, string ID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET auth_doc=@auth_doc WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@auth_doc", SqlDbType.Text);
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters["@auth_doc"].Value = auth_doc;
            command.Parameters["@xID"].Value = ID;
            int num = command.ExecuteNonQuery();
            connection.Close();
            return num;
        }

        public string updateMarkDocz(string logo_pic, string auth_doc, string sup_doc1, string sup_doc2, string pwalletID)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET logo_pic=@logo_pic,auth_doc=@auth_doc,sup_doc1=@sup_doc1,sup_doc2=@sup_doc2 WHERE log_staff=@log_staff ";
            connection.Open();
            command.Parameters.Add("@logo_pic", SqlDbType.Text);
            command.Parameters.Add("@auth_doc", SqlDbType.Text);
            command.Parameters.Add("@sup_doc1", SqlDbType.Text);
            command.Parameters.Add("@sup_doc2", SqlDbType.Text);
            command.Parameters.Add("@log_staff", SqlDbType.NVarChar, 50);
            command.Parameters["@logo_pic"].Value = logo_pic;
            command.Parameters["@auth_doc"].Value = auth_doc;
            command.Parameters["@sup_doc1"].Value = sup_doc1;
            command.Parameters["@sup_doc2"].Value = sup_doc2;
            command.Parameters["@log_staff"].Value = pwalletID;
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public int UpdateMarkDocz(string logo_pic, string auth_doc, string sup_doc1, string sup_doc2, string ID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET logo_pic=@logo_pic,auth_doc=@auth_doc,sup_doc1=@sup_doc1,sup_doc2=@sup_doc2 WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@logo_pic", SqlDbType.Text);
            command.Parameters.Add("@auth_doc", SqlDbType.Text);
            command.Parameters.Add("@sup_doc1", SqlDbType.Text);
            command.Parameters.Add("@sup_doc2", SqlDbType.Text);
            command.Parameters["@logo_pic"].Value = logo_pic;
            command.Parameters["@auth_doc"].Value = auth_doc;
            command.Parameters["@sup_doc1"].Value = sup_doc1;
            command.Parameters["@sup_doc2"].Value = sup_doc2;
            command.Parameters["@xID"].Value = ID;
            int num = command.ExecuteNonQuery();
            connection.Close();
            return num;
        }

        public int UpdateMarkLogo(string logo_pic, string ID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET logo_pic=@logo_pic WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@logo_pic", SqlDbType.Text);
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters["@logo_pic"].Value = logo_pic;
            command.Parameters["@xID"].Value = ID;
            int num = command.ExecuteNonQuery();
            connection.Close();
            return num;
        }

        public string updateMarkReg(string xID, string typ)
        {
            string str = "0";
            string str2 = "";
            if (typ == "1")
            {
                str2 = "NG/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" + xID;
            }
            else
            {
                str2 = "F/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" + xID;
            }
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET reg_number=@reg_number WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters.Add("@reg_number", SqlDbType.NVarChar, 50);
            command.Parameters["@xID"].Value = Convert.ToInt64(xID);
            command.Parameters["@reg_number"].Value = str2;
            str = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str;
        }

        public int UpdateMarkSupDoc1(string sup_doc1, string ID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET sup_doc1=@sup_doc1 WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@sup_doc1", SqlDbType.Text);
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters["@sup_doc1"].Value = sup_doc1;
            command.Parameters["@xID"].Value = ID;
            int num = command.ExecuteNonQuery();
            connection.Close();
            return num;
        }

        public int UpdateMarkSupDoc2(string sup_doc2, string ID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET sup_doc2=@sup_doc2 WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@sup_doc2", SqlDbType.Text);
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters["@sup_doc2"].Value = sup_doc2;
            command.Parameters["@xID"].Value = ID;
            int num = command.ExecuteNonQuery();
            connection.Close();
            return num;
        }

        public string updateOldDateMarkReg(string xID, string typ, string yr)
        {
            string str = "0";
            string str2 = "";
            if (typ == "1")
            {
                str2 = "NG/TM/O/" + yr + "/" + xID;
            }
            else
            {
                str2 = "F/TM/O/" + yr + "/" + xID;
            }
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE mark_info SET reg_number=@reg_number WHERE xID=@xID ";
            connection.Open();
            command.Parameters.Add("@xID", SqlDbType.BigInt);
            command.Parameters.Add("@reg_number", SqlDbType.NVarChar, 50);
            command.Parameters["@xID"].Value = Convert.ToInt64(xID);
            command.Parameters["@reg_number"].Value = str2;
            str = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str;
        }

        public string updatePwalletStage(string pwalletID, string log_officer, string stage)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pwallet SET stage=@stage,log_officer=@log_officer WHERE ID=@ID ";
            connection.Open();
            command.Parameters.Add("@ID", SqlDbType.BigInt);
            command.Parameters.Add("@stage", SqlDbType.NVarChar, 50);
            command.Parameters.Add("@log_officer", SqlDbType.NVarChar, 50);
            command.Parameters["@ID"].Value = Convert.ToInt64(pwalletID);
            command.Parameters["@stage"].Value = stage;
            command.Parameters["@log_officer"].Value = log_officer;
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public string updatePwalletStatus(string pwalletID, string log_officer)
        {
            string connectionString = this.Connect();
            string str2 = "";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE pwallet SET stage=5,log_officer=@log_officer WHERE ID=@ID ";
            connection.Open();
            command.Parameters.Add("@ID", SqlDbType.BigInt);
            command.Parameters.Add("@log_officer", SqlDbType.NVarChar, 50);
            command.Parameters["@ID"].Value = Convert.ToInt64(pwalletID);
            command.Parameters["@log_officer"].Value = log_officer;
            str2 = command.ExecuteNonQuery().ToString();
            connection.Close();
            return str2;
        }

        public int UpdateTmOfficeDoc(string xdoc1, string xdoc2, string xdoc3, string ID)
        {
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE tm_office SET xdoc1=@xdoc1,xdoc2=@xdoc2,xdoc3=@xdoc3 WHERE ID=@ID ";
            connection.Open();
            command.Parameters.Add("@xdoc1", SqlDbType.Text);
            command.Parameters.Add("@xdoc2", SqlDbType.Text);
            command.Parameters.Add("@xdoc3", SqlDbType.Text);
            command.Parameters.Add("@ID", SqlDbType.BigInt);
            command.Parameters["@xdoc1"].Value = xdoc1;
            command.Parameters["@xdoc2"].Value = xdoc2;
            command.Parameters["@xdoc3"].Value = xdoc3;
            command.Parameters["@ID"].Value = ID;
            int num = command.ExecuteNonQuery();
            connection.Close();
            return num;
        }

        public int updateTransaction(string id)
        {
            int num = 0;
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("UPDATE xtransactions set status=1 where ID='" + id + "'", connection);
            connection.Open();
            num = command.ExecuteNonQuery();
            connection.Close();
            return num;
        }

        public string ValidationIDByPwalletID(string ID)
        {
            string str = "";
            SqlConnection connection = new SqlConnection(this.Connect());
            SqlCommand command = new SqlCommand("SELECT validationID FROM pwallet WHERE ID='" + ID + "' ", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                str = reader["validationID"].ToString();
            }
            reader.Close();
            return str;
        }

        public class Address
        {
            public string city { get; set; }

            public string countryID { get; set; }

            public string email1 { get; set; }

            public string email2 { get; set; }

            public string ID { get; set; }

            public string lgaID { get; set; }

            public string log_staff { get; set; }

            public string reg_date { get; set; }

            public string stateID { get; set; }

            public string street { get; set; }

            public string telephone1 { get; set; }

            public string telephone2 { get; set; }

            public string visible { get; set; }

            public string zip { get; set; }
        }

        public class AddressService
        {
            public string city { get; set; }

            public string countryID { get; set; }

            public string email1 { get; set; }

            public string email2 { get; set; }

            public string ID { get; set; }

            public string lgaID { get; set; }

            public string log_staff { get; set; }

            public string reg_date { get; set; }

            public string stateID { get; set; }

            public string street { get; set; }

            public string telephone1 { get; set; }

            public string telephone2 { get; set; }

            public string visible { get; set; }

            public string zip { get; set; }
        }

        public class Applicant
        {
            public string addressID { get; set; }

            public string ID { get; set; }

            public string individual_id_number { get; set; }

            public string log_staff { get; set; }

            public string nationality { get; set; }

            public string reg_date { get; set; }

            public string tax_id_number { get; set; }

            public string tax_id_type { get; set; }

            public string visible { get; set; }

            public string xname { get; set; }

            public string xtype { get; set; }
        }

        public class Country
        {
            public string code { get; set; }

            public string ID { get; set; }

            public string name { get; set; }
        }

        public class Lga
        {
            public string ID { get; set; }

            public string name { get; set; }

            public string stateID { get; set; }
        }

        public class MarkInfo
        {
            public string auth_doc { get; set; }

            public string disclaimer { get; set; }

            public string log_staff { get; set; }

            public string logo_descriptionID { get; set; }

            public string logo_pic { get; set; }

            public string national_classID { get; set; }

            public string nice_class { get; set; }

            public string nice_class_desc { get; set; }

            public string product_title { get; set; }

            public string reg_date { get; set; }

            public string reg_number { get; set; }

            public string sign_type { get; set; }

            public string sup_doc1 { get; set; }

            public string sup_doc2 { get; set; }

            public string tm_typeID { get; set; }

            public string vienna_class { get; set; }

            public string xID { get; set; }

            public string xvisible { get; set; }
        }

        public class NClass
        {
            public string xdescription { get; set; }

            public string xID { get; set; }

            public string xtype { get; set; }
        }

        public class Representative
        {
            public string addressID { get; set; }

            public string agent_code { get; set; }

            public string ID { get; set; }

            public string individual_id_number { get; set; }

            public string individual_id_type { get; set; }

            public string log_staff { get; set; }

            public string nationality { get; set; }

            public string reg_date { get; set; }

            public string visible { get; set; }

            public string xname { get; set; }
        }

        public class Stage
        {
            public string amt { get; set; }

            public string applicantID { get; set; }

            public string data_status { get; set; }

            public string ID { get; set; }

            public string log_officer { get; set; }

            public string reg_date { get; set; }

            public string stage { get; set; }

            public string status { get; set; }

            public string validationID { get; set; }

            public string visible { get; set; }
        }

        public class State
        {
            public string countryID { get; set; }

            public string ID { get; set; }

            public string name { get; set; }
        }

        public class SWallet
        {
            public string ID { get; set; }

            public string log_officer { get; set; }

            public string mark_infoID { get; set; }

            public string reg_date { get; set; }

            public string search_cri { get; set; }

            public string search_str { get; set; }

            public string visible { get; set; }

            public string xclass { get; set; }
        }

        public class TmOffice
        {
            public string admin_status { get; set; }

            public string data_status { get; set; }

            public string ID { get; set; }

            public string pwalletID { get; set; }

            public string reg_date { get; set; }

            public string xcomment { get; set; }

            public string xdoc1 { get; set; }

            public string xdoc2 { get; set; }

            public string xdoc3 { get; set; }

            public string xofficer { get; set; }

            public string xvisible { get; set; }
        }

        public class XTransaction
        {
            public string adminID { get; set; }

            public string amt { get; set; }

            public string ID { get; set; }

            public string ptype { get; set; }

            public string status { get; set; }

            public string transactionID { get; set; }

            public string xcode { get; set; }
        }
    }
}

