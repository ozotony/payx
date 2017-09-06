<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.cs" Inherits="XPay.xis.pd.xreturn.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" data-ng-app="myModule">
<head id="Head1" runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../../../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../../../js/jquery.js" type="text/javascript"></script>
    <script src="../../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
      <script src="../../../js/funk.js" type="text/javascript"></script>
    <script src="../../../js/angular.min.js"></script>
    <script src="../../../js/sweet-alert.min.js"></script>
    <link href="../../../css/sweet-alert.css" rel="stylesheet" />
    <script src="../../../js/AngularLogin2.js"></script>
     <style type="text/css">
.tiger-stripe{text-align:left;font-weight:normal;font-size:14px;}
.tiger-stripe tr:nth-child(odd) {background: #E3EAEB;color:#000000;text-align:left;font-weight:normal;font-size:14px;}


</style>
 
</head>
<body ng-controller="myController">
    <form id="form1" runat="server">
    
                <div id="searchform">
        <table style="width:90%;" align="center">
        <tr>
        <td>
    <table style="width:100%;" align="center" class="form">
     <tr align="center">
                <td colspan="2">
                     <img alt="Coat of Arms"  src="../../../images/LOGOCLD.jpg" width="458" height="76" />
               </td>
            </tr>
  
     <tr>
        <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
            PAYMENT CONFIRMATION SECTION
        </td>
     </tr>
        <% if (isr.ResponseCode == "00")
           { %>
        <tr align="center">
            <td  style="font-size:20px;" colspan="2"><strong>PAYMENT COMPLETED SUCCESSFULLY</strong><br />
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
        <table  style="font-size:16px;text-align:center;font-weight:normal;width:100%;  border:1px solid #000000;" class="form">
       
        <tr >
            <td colspan="4" style="text-align:center;">
                <img alt="Coat of Arms"  src="../../../images/LOGOCLD.jpg" width="458" height="76" /></td>
        </tr>
       
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="4">
                PAYMENT RECIEPT FOR TRANSACTION :&nbsp;"<%=c_twall.ref_no%>" 
                </td>
        </tr>
        <tr>
            <td align="center" style="width:50%;" colspan="2">
               <strong> TRANSACTION ID:</strong> <%=txnref%></td>
            <td align="center" style="width:50%;" colspan="2">
                <strong>DATE:</strong>  <%=xreg_date%></td>
        </tr>
             <% if (kkx!=null)
           { %>
             <tr>
            <td align="center" style="width:50%;" colspan="2">
               <strong> PRODUCT TITLE:</strong> <%=kkx.product_title%></td>
            <td align="center" style="width:50%;" colspan="2">
                <strong>FILE NUMBER:</strong>  <%=kkx.reg_number%></td>
        </tr>
        
             <tr>
            <td align="center" style="width:50%;" colspan="2">
               <strong> CLASS:</strong> <%=kkx.nice_class%></td>
            <td align="center" style="width:50%;" colspan="2">
               </td>
        </tr>
           <%   } %>

          <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                </td>
        </tr>
  <tr style=" text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                PAYMENT REFERENCE:&nbsp;"<%=payRef %>"
                </td>
        </tr>
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                APPLICANT INFORMATION ---</td>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                AGENT INFORMATION ---</td>
        </tr>
        
        <tr>
            <td align="left" style="width:7%;">
                 NAME:
                 </td>
            <td align="left" style="width:43%;">
                  <% =c_app.xname%></td>
            <td align="left" style="width:7%;">
                 NAME:  </td>
            <td align="left" style="width:43%;">
                 <% =fullname%></td>
        </tr>
        
        <tr style="background-color:#E3EAEB;">
            <td align="left">
                ADDRESS:</td>
            <td align="left">
                <% =c_app.address%></td>
            <td align="left">
                CODE:</td>
            <td align="left">
                <%=cust_id%></td>
        </tr>
        <tr>
            <td align="left">
                 E-MAIL:   </td>
            <td align="left">
                 <%= c_app.xemail%></td>
            <td align="left">
                 E-MAIL:</td>
            <td align="left">
                <%= email%></td>
        </tr>
        <tr style="background-color:#E3EAEB;">
            <td align="left">
               MOBILE: </td>
            <td align="left">
                 <%= c_app.xmobile%></td>
            <td align="left">
               MOBILE: </td>
            <td align="left">
                <%= mobile%></td>
        </tr>

        <tr>
            <td align="center" colspan="4"  
                style="background-color:#666; color:#ffffff;font-weight:bold;">
                <strong>--- PAYMENT DETAILS ---</strong></td>
        </tr>
       
        <tr>
            <td align="left" colspan="4" style="font-size:12px;">
                <table style="width:100%;" id="mitems" class="tiger-stripe" >
                    <tr style="background-color:#1C5E55; color:#ffffff;">

                        <td >
                            <strong>S/N</strong></td>
                        <td >
                            <strong>TRANSACTION ID</strong></td>
                        <td>
                            <strong>ITEM CODE</strong></td>
                        <td> <strong>ITEM DESCRIPTION</strong></td>
                        <td> <strong>QTY</strong></td>
                        <td style="text-align:center;"><strong>APPLICATION FEE(NGN)</strong></td>
                         <td style="text-align:center;"><strong>TECH. FEE(NGN)</strong></td>
                          <td style="text-align:center;"><strong>TOTAL (NGN)</strong></td>
                    </tr>
                    <% 
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
                        <td > <%=pr.qty%></td>
                         <td style="text-align:right;"> <%=pr.init_amt%></td>
                          <td style="text-align:right;"> <%=pr.tech_amt%></td>
                           <td style="text-align:right;"> <%=pr.tot_amount%></td>
                    </tr>
                     <%  } %>
                   
                     <tr>
                        <td colspan="7" style="text-align:right;font-weight:bold;">
                            PayX Convenience Fee:&nbsp;</td>

                        <td align="right">
                            &nbsp;<%=Math.Round(Convert.ToDouble(isw_fields.isw_conv_fee),2)  %></td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="4">
               
            </td>
        </tr>
       <tr >
            <td colspan="4" >
                &nbsp;</td>
        </tr>
       
       <tr style="font-size:16px;text-decoration:underline; color:#1C5E55; font-weight:bolder; text-align:right;">
            <td colspan="4" >
               TOTAL AMOUNT:&nbsp;NGN&nbsp;<%=total_amt%></td>
        </tr>
       
       <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="4">
               
            </td>
        </tr>
       
      <tr>
            <td align="center" colspan="4">
                       POWERED BY<br/>
                        <img alt="Pay X" src="../../../images/payxlogo.jpg"   width="90px" height="40px"/>
            <br />    
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com" style="color:#0000ff;font-weight:normal;">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com" style="color:#0000ff; font-weight:normal;">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +2349038979681   
            </td>
        </tr>
        </table>
        </td>
        </tr>

         <tr align="center">
        <td colspan="2">
            <input type="button" name="Printform" id="Printform" value="Print" onclick="printXreturnAssessment('invoice');return false" class="button" /></td>
        <</tr>
         <% } %>
          <% if ((isr.ResponseCode != "00")&&(isr.ResponseCode != "XXXX"))
             { %>
          <tr align="center">
            <td  style="font-size:20px;" colspan="2"><strong>PAYMENT NOT COMPLETED SUCCESSFULLY</strong><br />                
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
            <td  style="font-size:20px;" colspan="2"><strong>PAYMENT PENDING</strong><br />                
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
                Click on <strong>&quot;Proceed&quot;</strong> to return to your <strong>Dashboard</strong></td>
        </tr>
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">            
                
                </td>
        </tr>

        <tr align="center">
            <td colspan="2">            
                <asp:Button ID="btnProceed" runat="server" class="button" Text="Proceed to Dashboard" 
                    onclick="btnProceed_Click" />
            </td>
        </tr>
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
            &nbsp;
               </td>
        </tr>
           
          </table>
      </td>
        </tr>
        </table> 
                    
           <asp:HiddenField ID="xname" runat="server" />
              <asp:HiddenField ID="xname2" runat="server" />   
               <asp:HiddenField ID="vamount" runat="server" />   
                <asp:HiddenField ID="vtransactionid" runat="server" />   
                 <asp:HiddenField ID="vtype" runat="server" />                   
    </div>
    </form>
</body>
</html>
