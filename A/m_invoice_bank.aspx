<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_invoice_bank.cs" Inherits="XPay.A.m_invoice_bank"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" /> 
    <script src="../js/funk.js" type="text/javascript"></script>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
  

     <style type="text/css">
.tiger-stripe{text-align:left;font-weight:normal;font-size:11px;}
.tiger-stripe tr:nth-child(odd) {background: #E3EAEB;color:#000000;text-align:left;font-weight:normal;font-size:11px;}
         </style>
    </head>
<body>
    <form id="form1" runat="server">   
                <div id="searchform">
        <table style="width:90%;" align="center">
        <tr>
        <td> 
    <table class="inv left-align" style="font-size:12px;width:100%;border:1px solid #000;">
    
        <tr >
            <td align="center" colspan="4">
                 <img alt="Coat of Arms"  src="../images/LOGOCLD.jpg" width="458" height="76" /></td>
        </tr>
          <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td colspan="4">
                &nbsp; </td>
        </tr>
         <tr style="text-align:center; font-weight:bold; font-size:15px;">
            <td colspan="4">
                <div style="font-weight:bolder; font-size:16px; color:#f00; text-align:center"> 
                DEAR CUSTOMER, KINDLY GO TO ANY OF OUR DESIGNATED BANKS</div>
                 <div style="font-weight:bolder; font-size:16px; color:#f00; text-align:center">
                 TO COMPLETE THE TRANSACTION WITH THE FOLLOWING DETAILS BELOW
                 </div>
                 </td>
        </tr>

         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td colspan="4" style="font-size:14px;">
                PAYMENT INVOICE SLIP FOR TRANSACTION :&nbsp;"<%=lt_twall[0].ref_no.ToUpper() %>" </td>
        </tr>
       
        <tr style="font-weight:bold;">
            <td align="center" style="width:50%;" colspan="2">
               <strong> TRANSACTION ID:</strong> <%=lt_twall[0].transID.ToUpper()%></td>
            <td align="center" style="width:50%;" colspan="2">
                <strong> DATE:</strong>  <%=xreg_date %></td>
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
            <td align="left" style="width:7%;border:1px solid #000;">
                 NAME:
                 </td>
            <td align="left" style="width:43%;border:1px solid #000;">
                  <% =c_app.xname%></td>
            <td align="left" style="width:7%;border:1px solid #000;">
                 NAME:  </td>
            <td align="left" style="width:43%;border:1px solid #000;">
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
                        <td>
                            S/N</td>
                        <td>
                            ITEM CODE</td>
                              <td> ITEM DESCRIPTION</td>
                        <td>
                            QTY</td>
                          <td style="text-align:center;"> APPLICATION FEE(NGN)</td>
                              <td style="text-align:center;"> TECH. FEE(NGN)</td>
                        <td>
                            TOTAL(NGN)</td>
                    </tr>
                    <% int i = 1; 
                        foreach (XPay.Classes.XObjs.Fee_details f in lt_fdets)
                       { %>
                    <tr>
                        <td>
                            <%=i%></td>
                        <td>
                           <%=ret.getFee_listByID(f.fee_listID).item_code%></td>
                            <td>
                           <%=ret.getFee_listByID(f.fee_listID).item%></td>
                        <td>
                           <%=f.xqty%></td>
                        
                         <td align="right">
                         <% string new_init_amt = string.Format("{0:n}", Convert.ToInt32(f.init_amt)); %>
                           <%=new_init_amt%></td>

                            <td align="right">
                         <% string new_tech_amt = string.Format("{0:n}", Convert.ToInt32(f.tech_amt)); %>
                           <%=new_tech_amt%></td>

                        <td align="right">
                         <% string new_tot_amt1 = string.Format("{0:n}", Convert.ToDouble(f.tot_amt)); %>
                             <%=new_tot_amt1 %></td>
                    </tr>
                     <% i++; amt += Convert.ToInt32(f.tot_amt); Session["tot_amtx"] = amt;
                       } %>
                   
                   
                </table>
            </td>
        </tr>
       <tr >
            <td colspan="4" class="right-align">&nbsp;</td>
        </tr>
       <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;">
            <td colspan="4" class="right-align">
            <% string new_tot_amtx = string.Format("{0:n}", amt); %>
               <strong>TOTAL AMOUNT:</strong>&nbsp;<strong> NGN&nbsp;<%=new_tot_amtx%></strong></td>
        </tr>
       
       <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="4">
            </td>
        </tr>
       
        
          <tr>
            <td align="center" colspan="4">
         POWERED BY<br/>
           <img src="../images/payxlogo.jpg"  alt="XPay" width="90px" height="40px" /> <br />
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
     <table width="100%">
        <tr>
        <td style="text-align:center;">
           <asp:Button ID="BtnDashboard" runat="server" class="button" Text="Back to Dashboard" onclick="BtnDashboard_Click" />
                &nbsp;<input type="button" name="Printform" id="Printform" value="Print" onclick="printA('searchform');return false" class="button" />
        
         </td>
        </tr>
        </table>
    </form>
</body>
</html>
