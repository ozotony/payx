<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test_form.cs" Inherits="XPay.xis.pd.tx.test_form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../../../js/jquery.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;text-align:left;font-weight:bold;font-size:14px;}
.tiger-stripe{ font-weight:bold;font-size:14px;}
 </style>
 
<script language="javascript" type="text/javascript">
// <![CDATA[

    function btnDashboard_onclick() {
        window.location.href = "../../../A/profile.aspx";
}

// ]]>
</script>
</head>
<body>
  <div class="container">
            <div class="sidebar">                 
               
            </div>
            <div class="content">
               
                <div id="searchform">
<table width="100%">
 <tr align="center">
                <td  align="left" style="width:50%;">
                    <img alt="Coat Of Arms" height="79" src="../../../images/coat_of_arms.png" 
                        width="85" />
               </td>
                <td   align="right" style="width:50%;">
                     <img src="../../../images/payxlogo.jpg"  alt="PayX" width="100px" height="50px" /></td>
            </tr>
            <tr align="center" style=" font-size:11pt;" >
                <td colspan="2" >
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
            </tr>
 <tr>
            <td colspan="2"  align="center" style="background-color:#1C5E55; color:#ffffff;" >
               DETAILS CONFIRMED</td>
        </tr>
<tr align="center">
            <td colspan="2" >
            <div class="notice_proceed">YOUR TRANSACTION DETAILS HAVE BEEN CONFIRMED AND YOU ARE ABOUT TO BE REDIRECTED TO THE INTER SWITCH PAYMENT GATEWAY<br />
            PLEASE VERIFY YOUR INTERNET CONNECTION IS UP AND RUNNING BEFORE PROCEEDING TO MAKE PAYMENT!!<br />
            BEST REGARDS!!!<br /><br />
            <img src="../../../images/check.png" alt="Details Confirmed"/>
            </div>
                
            </td>
        </tr>
<tr align="center">
<td colspan="2"  colspan="2">
    <form id="form1" runat="server" action="https://stageserv.interswitchng.com/test_paydirect/pay" method="post">
    <input name="product_id" type="hidden" value="<%=product_id %>" />
    <input name="pay_item_id" type="hidden" value="<%=pay_item_id %>" />
    <input name="amount" type="hidden" value="<%=amount %>" />
    <input name="currency" type="hidden" value="<%=currency %>" />
    <input name="site_redirect_url" type="hidden" value="<%=site_redirect_url %>" />
    <input name="txn_ref" type="hidden" value="<%=txn_ref %>" />
    <input name="cust_name" type="hidden" value="<%=name %>" />
    <input name="cust_name_desc" type="hidden" value="<%=name %>" />
    <input name="cust_id" type="hidden" value="<%=coy_name %>" />
    <input name="cust_id_desc" type="hidden" value="<%=coy_name %>" />
    <input name="pay_item_name" type="hidden" value="<%=coy_name %>" />
    <input name="hash" type="hidden" value="<%=hash %>" />
    <input name="payment_params" type="hidden" value="payment_split" />
    <input name="xml_data" type="hidden" value='<payment_item_detail>
    <item_details detail_ref="<%=txn_ref %>" institution="Einao Solutions" sub_location="Abuja" location="Lagos"> 
    <item_detail item_id="1" item_name="Einao Solutions" item_amt="<%=einao_split_amt %>" bank_id="120" acct_num="1771364037" /> 
    <item_detail item_id="2" item_name="Federal Ministry Of Commerce" item_amt="<%=cld_split_amt %>" bank_id="120" acct_num="1770393883" />
    </item_details>    
    </payment_item_detail>'/>
    <br />
    <input id="btnDashboard" type="button" value="Dashboard" class="button" onclick="return btnDashboard_onclick()" />
    
    <input id="btnPayment" type="submit" value="Make Payment" class="button" />
    
    </form>
    </td>
    </tr>
    <tr>
            <td colspan="2"  align="center" style="background-color:#1C5E55; color:#ffffff;">
              </td>
        </tr>
    <tr>
            <td colspan="2"  align="center">
            POWERED BY<br/><img alt="Einao Solutions" src="../../../images/einao_logo.png"   width="180px" height="39px"/><br />
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +2349038979681                 
                </td>
        </tr>
</table>

   </div>
            </div>
        </div>
</body>
</html>