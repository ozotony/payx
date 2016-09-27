namespace XPay.b_collect
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;
    using XPay.Classes;
    using PayX.com.branchcollect.www;

    public class xservices
    {
        public BranchCollectWebServicesGT bc_service = new BranchCollectWebServicesGT();
        public XmlNodeList Message;
        public XmlNodeList RespCode;

        public SortedList<string, string> GenerateTransactionTP(List<XObjs.Bc_AccountDetails> acc_details, List<XObjs.Bc_CustomFieldDetails> customfield_details, XObjs.Bc_ItemDetails item_detials, XObjs.Bc_Merchant merchant, XObjs.Bc_TransactionDetails transaction_details, string CBankID, string BankID)
        {
            SortedList<string, string> list = new SortedList<string, string>();
            string xMLRequest = "";
            StringBuilder output = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings {
                Encoding = Encoding.Default,
                Indent = true
            };
            XmlWriter writer = XmlWriter.Create(output, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("BranchCollectRequest");
            writer.WriteStartElement("MerchantDetails");
            writer.WriteStartElement("Merchant");
            writer.WriteStartAttribute("DevID");
            writer.WriteValue(merchant.DevID);
            writer.WriteStartAttribute("MerchantID");
            writer.WriteValue(merchant.MerchantID);
            writer.WriteStartAttribute("MerchantCode");
            writer.WriteValue(merchant.MerchantCode);
            writer.WriteEndAttribute();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("TransactionDetails");
            writer.WriteStartElement("Transaction");
            writer.WriteStartAttribute("TransactionID");
            writer.WriteValue(transaction_details.TransactionID);
            writer.WriteStartAttribute("MAC");
            writer.WriteValue(transaction_details.MAC);
            writer.WriteStartAttribute("TotalAmount");
            writer.WriteValue(transaction_details.TotalAmount);
            writer.WriteStartAttribute("CustomerID");
            writer.WriteValue(transaction_details.CustomerID);
            writer.WriteStartAttribute("CustomerSurname");
            writer.WriteValue(transaction_details.CustomerSurname);
            writer.WriteStartAttribute("CustomerFirstname");
            writer.WriteValue(transaction_details.CustomerFirstname);
            writer.WriteStartAttribute("CustomerOthernames");
            writer.WriteValue(transaction_details.CustomerOthernames);
            writer.WriteStartAttribute("CustomerEmail");
            writer.WriteValue(transaction_details.CustomerEmail);
            writer.WriteStartAttribute("CustomerGSM");
            writer.WriteValue(transaction_details.CustomerGSM);
            writer.WriteStartAttribute("UpdateURL");
            writer.WriteValue(transaction_details.UpdateURL);
            writer.WriteStartAttribute("UpdateURLThirdParty");
            writer.WriteValue(transaction_details.UpdateURLThirdParty);
            writer.WriteEndAttribute();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("ItemDetails");
            writer.WriteStartAttribute("ItemDescription");
            writer.WriteValue(item_detials.ItemDescription);
            writer.WriteStartAttribute("InstallmentID");
            writer.WriteValue(item_detials.InstallmentID);
            writer.WriteStartAttribute("Split");
            writer.WriteValue(item_detials.Split);
            writer.WriteStartAttribute("ItemCode");
            writer.WriteValue(item_detials.ItemCode);
            writer.WriteStartAttribute("ExpiryDate");
            writer.WriteValue(item_detials.ExpiryDate);
            writer.WriteEndAttribute();
            foreach (XObjs.Bc_Items items in item_detials.lt_Item)
            {
                writer.WriteStartElement("Item");
                writer.WriteStartAttribute("ItemName");
                writer.WriteValue(items.ItemName);
                writer.WriteStartAttribute("ItemAmount");
                writer.WriteValue(items.ItemAmount);
                writer.WriteEndAttribute();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteStartElement("CustomFieldDetails");
            foreach (XObjs.Bc_CustomFieldDetails details in customfield_details)
            {
                writer.WriteStartElement("CustomField");
                writer.WriteStartAttribute("CustomFieldLabel");
                writer.WriteValue(details.CustomFieldLabel);
                writer.WriteStartAttribute("CustomFieldValue");
                writer.WriteValue(details.CustomFieldValue);
                writer.WriteEndAttribute();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteStartElement("CollectionBankDetails");
            writer.WriteStartElement("CollectionBank");
            writer.WriteStartAttribute("CBankID");
            writer.WriteValue(BankID);
            writer.WriteEndAttribute();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("BankAccounts");
            writer.WriteStartElement("BankDetails");
            writer.WriteStartAttribute("BankID");
            writer.WriteValue(BankID);
            writer.WriteEndAttribute();
            foreach (XObjs.Bc_AccountDetails details2 in acc_details)
            {
                writer.WriteStartElement("AccountDetails");
                writer.WriteStartAttribute("AccountID");
                writer.WriteValue(details2.AccountID);
                writer.WriteStartAttribute("AccountName");
                writer.WriteValue(details2.AccountName);
                writer.WriteStartAttribute("AccountNo");
                writer.WriteValue(details2.AccountNo);
                writer.WriteStartAttribute("SplitAmount");
                writer.WriteValue(details2.SplitAmount);
                writer.WriteEndAttribute();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
            xMLRequest = output.ToString().Replace("utf-16", "UTF-8");
            XmlTextReader reader = new XmlTextReader(new StringReader(this.bc_service.GenerateTransactionTP(xMLRequest)));
            XmlDocument document = new XmlDocument();
            document.Load(reader);
            this.RespCode = document.GetElementsByTagName("RespCode");
            this.Message = document.GetElementsByTagName("Message");
            list.Add("Response_code", this.RespCode[0].InnerText);
            list.Add("Message", this.Message[0].InnerText);
            return list;
        }

        public static byte[] GetHash(string inputString)
        {
            return SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public string GetHashString(string inputString)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte num in GetHash(inputString))
            {
                builder.Append(num.ToString("X2"));
            }
            return builder.ToString();
        }
    }
}

