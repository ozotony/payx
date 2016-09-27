namespace XPay.InterSwitch.PayDirect.Classes
{
    using System;

    public class ErrorHandler
    {
        public string getBank(string code)
        {
            switch (code)
            {
                case "7":
                    return "United Bank for Africa";

                case "8":
                    return "First Bank";

                case "9":
                    return "MainStreet Bank";

                case "10":
                    return "GTB";

                case "11":
                    return "Union Bank";

                case "16":
                    return "WEMA Bank";

                case "17":
                    return "IBTC";

                case "31":
                    return "Access Bank";

                case "47":
                    return "Ecobank";

                case "51":
                    return "Fidelity Bank";

                case "72":
                    return "Diamond Bank";

                case "75":
                    return "ETB";

                case "76":
                    return "FCMB";

                case "89":
                    return "Intercontinental Bank";

                case "101":
                    return "Oceanic Bank";

                case "109":
                    return "Standard Chartered Bank";

                case "117":
                    return "Zenith Bank";

                case "120":
                    return "Skye Bank";

                case "121":
                    return "Sterling Bank";

                case "123":
                    return "Keystone Bank";

                case "125":
                    return "Enterprise Bank";

                case "126":
                    return "Citi Bank";

                case "128":
                    return "Fin Bank";

                case "129":
                    return "Unity Bank";

                case "307":
                    return "Heritage Bank";
            }
            return "NA";
        }

        public string getErrorDesc(string code)
        {
            switch (code)
            {
                case "00":
                    return "Approved by Financial Institution";

                case "01":
                    return "Refer to Financial Institution";

                case "02":
                    return "Refer to Financial Institution, Special Condition";

                case "03":
                    return "Invalid Merchant";

                case "04":
                    return "Pick-up card";

                case "05":
                    return "Do Not Honor";

                case "06":
                    return "Error";

                case "07":
                    return "Pick-Up Card, Special Condition";

                case "08":
                    return "Honor with Identification";

                case "09":
                    return "Request in Progress";

                case "10":
                    return "Approved by Financial Institution, Partial";

                case "11":
                    return "Approved by Financial Institution, VIP";

                case "12":
                    return "Invalid Transaction";

                case "13":
                    return "Invalid Amount";

                case "14":
                    return "Invalid Card Number";

                case "15":
                    return "No Such Financial Institution";

                case "16":
                    return "Approved by Financial Institution, Update Track 3";

                case "17":
                    return "Customer Cancellation";

                case "18":
                    return "Customer Dispute";

                case "19":
                    return "Re-enter Transaction";

                case "20":
                    return "Invalid Response from Financial Institution";

                case "21":
                    return "No Action Taken by Financial Institution";

                case "22":
                    return "Suspected Malfunction";

                case "23":
                    return "Unacceptable Transaction Fee";

                case "24":
                    return "File Update not Supported";

                case "25":
                    return "Unable to Locate Record";

                case "26":
                    return "Duplicate Record";

                case "27":
                    return "File Update Field Edit Error";

                case "28":
                    return "File Update File Locked";

                case "29":
                    return "File Update Failed";

                case "30":
                    return "Format Error";

                case "31":
                    return "Bank Not Supported";

                case "32":
                    return "Completed Partially by Financial Institution";

                case "33":
                    return "Expired Card, Pick-Up";

                case "34":
                    return "Suspected Fraud, Pick-Up";

                case "35":
                    return "Contact Acquirer, Pick-Up";

                case "36":
                    return "Restricted Card, Pick-Up";

                case "37":
                    return "Call Acquirer Security, Pick-Up";

                case "38":
                    return "PIN Tries Exceeded, Pick-Up";

                case "39":
                    return "No Credit Account";

                case "40":
                    return "Function not Supported";

                case "41":
                    return "Lost Card, Pick-Up";

                case "42":
                    return "No Universal Account";

                case "43":
                    return "Stolen Card, Pick-Up";

                case "44":
                    return "No Investment Account";

                case "51":
                    return "Insufficient Funds";

                case "52":
                    return "No Check Account";

                case "53":
                    return "No Savings Account";

                case "54":
                    return "Expired Card";

                case "55":
                    return "Transaction Error";

                case "56":
                    return "No Card Record";

                case "57":
                    return "Transaction not permitted to Cardholder";

                case "58":
                    return "Transaction not permitted on Terminal";

                case "59":
                    return "Suspected Fraud";

                case "60":
                    return "Contact Acquirer";

                case "61":
                    return "Exceeds Withdrawal Limit";

                case "62":
                    return "Restricted Card";

                case "63":
                    return "Security Violation";

                case "64":
                    return "Original Amount Incorrect";

                case "65":
                    return "Exceeds withdrawal frequency";

                case "66":
                    return "Call Acquirer Security";

                case "67":
                    return "Hard Capture";

                case "68":
                    return "Response Received Too Late";

                case "75":
                    return "PIN tries exceeded";

                case "76":
                    return "Reserved for Future Postilion Use";

                case "77":
                    return "Intervene, Bank Approval Required";

                case "78":
                    return "Intervene, Bank Approval Required for Partial Amount";

                case "90":
                    return "Cut-off in Progress";

                case "91":
                    return "Issuer or Switch Inoperative";

                case "92":
                    return "Routing Error";

                case "93":
                    return "Violation of law";

                case "94":
                    return "Duplicate Transaction";

                case "95":
                    return "Reconcile Error";

                case "96":
                    return "System Malfunction";

                case "98":
                    return "Exceeds Cash Limit";

                case "A0":
                    return "Unexpected error";

                case "A4":
                    return "Transaction not permitted to card holder, via channels";

                case "Z0":
                    return "Transaction Status Unconfirmed";

                case "Z1":
                    return "Transaction Error";

                case "Z2":
                    return "Bank account error";

                case "Z3":
                    return "Bank collections account error";

                case "Z4":
                    return "Interface Integration Error";

                case "Z5":
                    return "Duplicate Reference Error";

                case "Z6":
                    return "Incomplete Transaction";

                case "Z7":
                    return "Transaction Split Pre-processing Error";

                case "Z8":
                    return "Invalid Card Number, via channels";

                case "Z9":
                    return "Transaction not permitted to card holder, via channels";

                case "20031":
                    return "Invalid value for ProductId";
            }
            return "NA";
        }
    }
}

