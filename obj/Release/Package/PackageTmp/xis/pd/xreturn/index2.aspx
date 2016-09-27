<%@ Page Language="C#" AutoEventWireup="true"  Inherits="XPay.xis.pd.xreturn.index2" %>

<!DOCTYPE html>

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
        
    <table style="width:100%;" align="center" class="form">
    <tr align="center">
                <td  align="left" style="width:50%;">
                    <img alt="Coat Of Arms" height="79" src="../../../images/coat_of_arms.png" 
                        width="85" />
               </td>
                <td   align="right" style="width:50%;">
                     <img src="../images/payxlogo.jpg"  alt="PayX" width="100px" height="50px" /></td>
            </tr>
     <tr align="center" style=" font-size:11pt;" >
                <td colspan="2">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
            </tr>
     <tr>
        <td colspan="2" align="center" style="background-color:#1C5E55; color:#ffffff;">
            PAYMENT CONFIRMATION SECTION
        </td>
     </tr>
        <% if (isr.ResponseCode == "00")
           { %>
        <tr align="center">
            <td  colspan="2" style="font-size:20px;"><strong>PAYMENT COMPLETED SUCCESSFULLY</strong><br />
               <div class="payment_success">
                 <div class="x_succ_img">
                An e-mail has been sent to: <%=email%><br />
                Transaction Reference:&nbsp;<%=txnref%><br />
                Payment Reference:&nbsp;<%=payRef%><br /><br />
                Please check your "Payment Status" or "History Log" to view more details!!
                </div>
                </div>
            </td>
        </tr>

        <tr id="invoice">
        <td colspan="2">
        <table  style="font-size:12px;width:100%;">
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
                INVOICE/RECIEPT FOR TRANSACTION :&nbsp;"<%=c_twall.ref_no%>" </td>
        </tr>
        <tr >
            <td  style="width:50%;">
                <img alt="Coat of Arms" height="69" src="../../../images/coat_of_arms.png" 
                    width="88" /></td>
            <td class="right-align" >
              <img src="../images/payxlogo.jpg"  alt="PayX" width="100px" height="50px" /></td>
        </tr>
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td align="center" colspan="2">
                </td>
        </tr>
        <tr>
            <td align="center">
               <strong> DATE:</strong> <%=xreg_date %></td>
            <td align="center">
                <strong> INVOICE DATE:</strong>  <%=c_twall.xreg_date%></td>
        </tr>
        
        <tr>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;">
                ---
                APPLICANT INFORMATION ---</td>
        </tr>
        
        <tr>
            <td align="center" colspan="2">
                <strong> NAME:</strong><br /><% =c_app.xname%></td>
        </tr>
        
        <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <strong> E-MAIL ADDRESS:</strong><br /><%= c_app.xemail%></td>
            <td align="center">
               <strong>MOBILE NUMBER:</strong><br /><%= c_app.xmobile%> </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;">
                ---
                AGENT INFORMATION ---</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <strong> NAME:</strong><br /><% =fullname%></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <strong> E-MAIL ADDRESS:</strong><br /><%= email%></td>
            <td align="center">
               <strong>MOBILE NUMBER:</strong><br /><%= mobile%> </td>
        </tr>
        <tr>
            <td align="center" colspan="2"  style="background-color:#666; color:#ffffff;">
                <strong>--- PAYMENT DETAILS ---</strong></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="font-size:12px;">
                <table style="width:100%;" id="mitems" class="tiger-stripe" >
                    <tr style="background-color:#1C5E55; color:#ffffff;">

                        <td style="width:5%;">
                            <strong>S/N</strong></td>
                        <td style="width:20%;">
                            <strong>TRANSACTION ID</strong></td>
                        <td style="width:10%;">
                            <strong>ITEM CODE</strong></td>
                        <td style="width:50%;">
                            <strong>ITEM DESCRIPTION</strong></td>
                        <td style="width:15%;">
                            <strong>AMOUNT (<em>NGN</em> )</strong></td>
                    </tr>
                    <% int i = 1; 
                        foreach (XPay.Classes.XObjs.PaymentReciept pr in lt_pr)
                       { %>
                    <tr>
                        <td>
                            <%=pr.sn%></td>
                        <td>
                            <%=pr.transID%></td>
                        <td>
                            <%=pr.item_code%></td>
                        <td>
                             <%=pr.item_desc%></td>
                        <td>
                        <%=pr.amount%></td>
                    </tr>
                     <%  } %>
                   
                    
                </table>
            </td>
        </tr>
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
               
            </td>
        </tr>
       <tr style="font-size:16px;text-decoration:underline; color:#1C5E55; font-weight:bolder; text-align:right;">
            <td colspan="2" >
               <em>TOTAL AMOUNT:</em>&nbsp;<%=total_amt%> NGN</td>
        </tr>
       
       <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
               
            </td>
        </tr>

        </table>
        </td>
        </tr>
         <% } %>
          <% if ((isr.ResponseCode != "00")&&(isr.ResponseCode != "XXXX"))
             { %>
          <tr align="center">
            <td colspan="2" style="font-size:20px;"><strong>PAYMENT NOT COMPLETED SUCCESSFULLY</strong><br />                
                <div class="payment_failure">
                 <div class="x_fail_img">                
                An e-mail has been sent to : <%=email%><br />
                Reason:&nbsp;<%=isr.ResponseDescription%><br />
                Transaction Reference:&nbsp;<%=txnref%><br />
                Payment Reference:&nbsp;<%=payRef%><br /><br />
                Please check your "Payment Status" or "History Log" to view more details!!
                 </div>
                </div>
            </td>
        </tr>
         <% } %>
          <% if ((isr.ResponseCode != "00")&&(isr.ResponseCode == "XXXX"))
             { %>
          <tr align="center">
            <td colspan="2" style="font-size:20px;"><strong>PAYMENT PENDING</strong><br />                
                <div class="payment_failure">
                 <div class="x_fail_img"> 
                Reason:&nbsp; <%=isr.ResponseDescription%><br />
                Please check your "Payment Status" or "History Log" to view more details!!
                 </div>
                </div>
            </td>
        </tr>
         <% } %>
        <tr>
            <td align="center" colspan="2">
                Click on <strong>&quot;Proceed&quot;</strong> to return your <strong>Dashboard</strong></td>
        </tr>
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">            
                
                </td>
        </tr>

        <tr align="center">
            <td colspan="2">            
                <asp:Button ID="btnProceed" runat="server" class="button" Text="Proceed" 
                    onclick="btnProceed_Click" />
            <input type="button" name="Printform" id="Printform" value="Print" onclick="printAssessment('invoice');return false" class="button" /></td>
        </tr>
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
            
               </td>
        </tr>
      <tr>
            <td align="center" colspan="2">
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
