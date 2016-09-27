<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_optionst.cs" Inherits="XPay.xis.pd.tx.payment_optionst" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../../../js/jquery.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../../js/funk.js" type="text/javascript"></script>
     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;text-align:left;font-weight:bold;font-size:14px;}
.tiger-stripe{ font-weight:bold;font-size:14px;}
        .style1
        {
            width: 50px;
            height: 30px;
        }
    </style>
 
</head>
<body>
    <form id="form1" runat="server">   
                <div id="searchform">
        <table style="width:80%;" align="center">
        <tr>
        <td colspan="2" >
    <table class="center-align" style="width:100%;">
   <tr align="center">
                <td    align="left" style="width:50%;">
                    <img alt="Coat of Arms" height="100%" src="../../../images/LOGOCLD.jpg" width="100%" />
               </td>
                <td   align="right" style="width:50%;">
                     <img src="../../../images/payxlogo.jpg"  alt="PayX" width="100px" height="50px" /></td>
            </tr>

        <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff; font-size:16px;">
              INTER SWITCH PAYMENT GUIDE</td>
        </tr>
        <tr>
            <td colspan="2"  class="left-align">
               <span class="notice_header"> Step One:</span><br />
                <span class="notice_text">You should have either an Interswitch powered Debit card or Verve card</span><br />
                <br />
                <span class="notice_header">Step Two:</span><br />
                <span class="notice_text">Please ensure that the card provided is still valid (Check the expiry date!)</span><br />
                <br />
                <span class="notice_header">Step Three:</span><br />
                <span class="notice_text">Please ensure that you have the required amount (As shown on the details page!) 
                on the selected card type to pay for the required items</span><br />
                <br />
                <span class="notice_header">Step Four:</span><br />
                <span class="notice_text">On the payment gateway, select the card type and provide the card details,PIN 
                and any other required information</span><br />
                <br />
                <span class="notice_header">Step Five:</span><br />
                <span class="notice_text">Always cross-check the details before you proceed to the payment gateway</span><br />
                <br />               
                
                </td>
        </tr>
          <tr>
            <td colspan="2"  class="notice_footer" >
               Thank you for adhering to the above instructions!!</td>
        </tr>
         
          <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;">
               </td>
        </tr>
         <tr>
            <td colspan="2"  class="center-align">
                &nbsp;</td>
        </tr>
          <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;font-size:16px;">
              BANK DEPOSIT PAYMENT GUIDE</td>
        </tr>
         <tr>
            <td colspan="2"  class="center-align">
                &nbsp;</td>
        </tr>
          <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;">
              </td>
        </tr>
         
          <tr>
            <td colspan="2"  class="left-align">
                <span class="notice_header">Step One:</span><br />
                <span class="notice_text">Make sure you have either recieved an alert (SMS/E-mail) or have printed a copy 
                of your invoice</span><br />
                <br />
                <span class="notice_header">Step Two:</span><br />
                <span class="notice_text">Kindly go to any of the designated banks for the service to make payment</span><br />
                <br />
                <span class="notice_header">Step Three:</span><br />
               <span class="notice_text"> Always cross-check the details before you proceed to generate the payment 
                invoice</span><br />
                <br />
                </td>
        </tr>
        <tr>
            <td colspan="2"  class="notice_footer" >
               Thank you for adhering to the above instructions!!</td>
        </tr>
        <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                </td>
        </tr>

        <tr>
            <td colspan="2"  class="center-align">
                &nbsp;</td>
        </tr>

        <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;">
              PLEASE SELECT AN OPTION BELOW</td>
        </tr>

        <tr>
            <td colspan="2"  class="center-align">
                </td>
        </tr>

        <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                </td>
        </tr>

       <tr  align="center" >
                <td colspan="2"  >
                    <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"  CssClass="textbox">
                        <asp:ListItem Value="isw">
                        <img alt="interswitch"  width="92px" height="30px" src="../../../images/isw_logo_small.gif" />
                        <img alt="mastercard" class="style1" src="../../../images/mastercard-logo.png" />
                        <img alt="verve" class="style1" src="../../../images/verve.png" />
                        </asp:ListItem>
                        <asp:ListItem Value="bank">Bank Deposit&nbsp;<img alt="recharge_pin" class="style1" src="../../../images/recharge_pin.png" /></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        
       
      <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            
            </td>
        </tr>

         <tr>
            <td colspan="2"  class="center-align">
             <input type="button" id="back" value="Back" class="button" onclick="doHistory('-1');return false;"/>  
            </td>
        </tr>
         <tr>
            <td colspan="2"  class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            
            </td>
        </tr>

         
        <tr>
            <td colspan="2"  class="center-align">
           POWERED BY<br/><img alt="Einao Solutions" src="../../../images/einao_logo.png"   width="90px" height="39px"/>
            <img alt="interswitch"   src="../../../images/isw_logo_small.gif" /><br />
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +2349038979681 
            </td>
        </tr>
        
     
          </table>
           </td>
        </tr>
        </table>
                </div>
                

    </form>
</body>
</html>
