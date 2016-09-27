<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form.aspx.cs" Inherits="XPay.xm.tx.form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FRESH PAYMENT SECTION</title>
   <link href="../../css/style.css" rel="stylesheet" type="text/css" /> 
    <script src="../../js/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
     <script src="../../js/jquery.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../js/funk.js" type="text/javascript"></script>

     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;  }
.tiger-stripe{ font-size:12px;}
 </style>

</head>
<body>
 
   <div id="searchform">
              
<table style="width:100%;">
<tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">
               DETAILS CONFIRMED</td>
        </tr>
<tr align="center">
            <td>
            <div class="notice_proceed">YOUR TRANSACTION DETAILS HAVE BEEN CONFIRMED AND YOU ARE ABOUT TO BE REDIRECTED TO THE INTER SWITCH PAYMENT GATEWAY<br />
            PLEASE VERIFY YOUR INTERNET CONNECTION IS UP AND RUNNING BEFORE PROCEEDING TO MAKE PAYMENT!!<br />
            BEST REGARDS!!!<br /><br />
            <img src="../../images/check.png" alt="Details Confirmed"/>
            </div>
                
            </td>
        </tr>
<tr align="center">
<td>
    <form id="form1" runat="server" action="https://stageserv.interswitchng.com/test_paydirect/pay" method="post">
    <input name="product_id" type="hidden" value="<%=product_id %>" />
    <input name="pay_item_id" type="hidden" value="<%=pay_item_id %>" />
    <input name="amount" type="hidden" value="<%=total_amt %>" />
    <input name="currency" type="hidden" value="<%=currency %>" />
    <input name="site_redirect_url" type="hidden" value="<%=site_redirect_url %>" />
    <input name="txn_ref" type="hidden" value="<%=txn_ref %>" />
    <input name="cust_name" type="hidden" value="<%=cust_name %>" />
    <input name="cust_name_desc" type="hidden" value="<%=cust_name %>" />
    <input name="cust_id" type="hidden" value="<%=xproduct_name %>" />
    <input name="cust_id_desc" type="hidden" value="<%=xproduct_desc %>" />
    <input name="pay_item_name" type="hidden" value="<%=xproduct_name %>" />
    <input name="hash" type="hidden" value="<%=hash %>" />
    <input name="payment_params" type="hidden" value="payment_split" />
    <input name="xml_data" type="hidden" value='<payment_item_detail>
    <item_details detail_ref="<%=txn_ref %>" institution="Einao Solutions" sub_location="Abuja" location="Lagos"> 
    <item_detail item_id="1" item_name="Einao Solutions" item_amt="<%=einao_split_amt %>" bank_id="120" acct_num="1771364037" /> 
    <item_detail item_id="2" item_name="<%=cust_name %>" item_amt="<%=split_amt %>" bank_id="<%=split_bank %>" acct_num="<%=split_acc %>" />
    </item_details>    
    </payment_item_detail>'/>
    <br />  
    </form>
    </td>
    </tr>
    <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">
            <input id="btnDashboard" type="button" value="Dashboard" class="button" onclick="doPost()" />    
              </td>
        </tr>
    <tr>
            <td align="center">
            <img alt="Einao Solutions" src="../../images/einao_logo.png"   width="180px" height="39px"/><br />
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +234-8098367527, +234-11111                 
                </td>
        </tr>
</table>

   </div>
</body>
<script  type="text/javascript">

    isw_merc_postwith('https://stageserv.interswitchng.com/test_paydirect/pay',
        { product_id: '<%=product_id %>', pay_item_id: '<%=pay_item_id %>', amount: '<%=total_amt %>', currency: '<%=currency %>', site_redirect_url: '<%=site_redirect_url %>'
        , txn_ref: '<%=txn_ref %>', cust_name: '<%=cust_name %>', cust_name_desc: '<%=cust_name %>', cust_id: '<%=xproduct_name %>', cust_id_desc: '<%=xproduct_desc %>', pay_item_name: '<%=xproduct_name %>'
        , hash: '<%=hash %>', payment_params: 'payment_split'
        }, '<%=txn_ref %>', '<%=einao_split_amt %>', '<%=cust_name %>', '<%=split_amt %>', '<%=split_bank %>', '<%=split_acc %>');
    
</script>
</html>