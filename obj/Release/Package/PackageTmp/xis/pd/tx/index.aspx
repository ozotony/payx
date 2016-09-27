<%@ Page Language="C#" AutoEventWireup="true" Inherits="XPay.xis.pd.tx.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../../../js/jquery.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

     <style type="text/css">
.item_alt {background-color:#E3EAEB;text-align:left;font-weight:bold;font-size:14px;}
.tiger-stripe{ font-weight:bold;font-size:14px;}
</style>
  <script type="text/javascript">
      $(function () {
        //  $("#tbl_loader").load("./payment_options.aspx");
      });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="container">
            <div class="sidebar">                 
               
            </div>
            <div class="content">
               
                <div id="searchform">
        
    <table align="center" width="100%">
    <tr align="center">
                <td align="left" style="width:50%;">
                    <img alt="Coat Of Arms" height="79" src="../../../images/coat_of_arms.png" 
                        width="85" />
               </td>
                <td align="right" style="width:50%;">
                     <img src="../../../images/payxlogo.jpg"  alt="PayX" width="100px" height="50px" /></td>
            </tr>
            <tr align="center" style=" font-size:11pt;" >
                <td colspan="2"  >
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
            </tr>
        <tr>
            <td colspan="2"  align="center" style="background-color:#1C5E55; color:#ffffff;">
                WELCOME TO THE PAYMENT SECTION, PLEASE FOLLOW THE INSTRUCTIONS BELOW AND THEN CLICK ON PROCEED</td>
        </tr>
        <tr >
            <td colspan="2"  ><strong>PLEASE FOLLOW THE INSTRUCTIONS BELOW:</strong><br /><br />
                (1) To &quot;Add&quot; an item, put in the quantity (QTY) next to the 
                item to purchase and the click on
                <img alt="Add" height="16" src="../../../images/checkmark.gif" width="16" /><br />
                (2) To &quot;Remove an item, click on
                <img alt="Remove" height="16" src="../../../images/x.gif" width="16" /><br />
                (3) After you have finished selecting items, click on the &quot;CONFIRM&quot; button below<br />
                (4) You can &quot;CHANGE ITEMS&quot; after viewing your basket<br />
                (5) Click on &quot;CHECK OUT&quot; to process your invoice and recieve you transaction 
                PIN.</td>
        </tr>
        <tr>
            <td colspan="2"  align="center" style="background-color:#1C5E55; color:#ffffff;">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2"  align="center">
            
                <asp:Button ID="btnProceed" runat="server" class="button" Text="Proceed" onclick="btnProceed_Click" />
                </td>
        </tr>
       <tr>
            <td colspan="2"  align="center" style="background-color:#1C5E55; color:#ffffff;">
                &nbsp;</td>
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
       
    </div>
    </form>
</body>
</html>
