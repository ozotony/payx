<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_invoicex_old.cs" Inherits="XPay.A.m_invoicex_old"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" /> 
    <script src="../js/funk.js" type="text/javascript"></script>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
  

    <script type="text/javascript">
    $(function(){
        $("table.tiger-stripe tr:odd").addClass("item_alt");
    });
    </script>

     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000; font-weight:normal;font-size:14px; }
.tiger-stripe{ font-weight:normal;font-size:14px;}

</style>
    </head>
<body>
    <form id="form1" runat="server">
   
                
                <div id="searchform">
      <table style="width:90%;" align="center">
        <tr>
        <td>   
    <table class="form left-align"  style="font-size:16px;text-align:center;font-weight:bold;width:100%;border:1px solid #000000;">
        <tr >
            <td align="center" colspan="2">
                 <img alt="Coat of Arms"  src="../images/LOGOCLD.jpg" width="458" height="76" /></td>
        </tr>
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td colspan="2">
                INVOICE FOR TRANSACTION :&nbsp;"<%if (lt_twall.Count > 0) { Response.Write(lt_twall[0].ref_no.ToUpper()); }%>" </td>
        </tr>
      
       <tr>
            <td align="left" colspan="2">
                <span class="notice_header">USER GUIDE:</span><br /><br />
               <div style="font-weight:normal;">
               This section displays your Payment Invoice Slip<br /><br />
                 <strong> --&nbsp;</strong>To select your preferred mode of payment, Click &quot;Proceed to Payment&quot;<br />
               </div>   
                
                </td>
        </tr>
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="2">
               &nbsp;
                </td>
        </tr>

        <tr>
            <td align="center" style="width:50%;">
               <strong> TRANSACTION ID:</strong><%if (lt_twall.Count > 0) { Response.Write(lt_twall[0].transID.ToUpper()); }%></td>
            <td align="center" style="width:50%;">
                <strong> DATE:</strong>  <%=xreg_date%></td>
        </tr>
        
          <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="2">
               &nbsp;
                </td>
        </tr>

        <tr>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                APPLICANT INFORMATION ---</td>
        </tr>
        
        <tr>
            <td align="center" colspan="2">
                <strong> FULL NAME:</strong>  
                <% =c_app.xname%></td>
        </tr>
        
        <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <strong> E-MAIL ADDRESS:</strong>  <%= c_app.xemail%></td>
            <td align="center">
               <strong>MOBILE NUMBER:</strong> <%= c_app.xmobile%> </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                AGENT INFORMATION ---</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <strong> FULL NAME:</strong>  
                <% =fullname%></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <strong> E-MAIL ADDRESS:</strong>  <%= email%></td>
            <td align="center">
               <strong>MOBILE NUMBER:</strong> <%= mobile%> </td>
        </tr>
        <tr>
            <td align="center" colspan="2"  style="background-color:#666; color:#ffffff;font-weight:bold;">
                <strong>--- PAYMENT DETAILS ---</strong></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="font-size:14px;">
                <table style="width:100%;" id="mitems" class="tiger-stripe" >
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            <strong>S/N</strong></td>
                        <td> <strong>ITEM CODE</strong></td>
                        
                          <td> <strong>DESCRIPTION</strong></td>
                        <td>
                            <strong>QTY</strong></td>
                        <td>
                            <strong>AMOUNT (<em>NGN</em> )</strong></td>
                        <td>
                            <strong>TOTAL (<em>NGN</em> )</strong></td>
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
                        <td>
                         <% string new_qty = string.Format("{0:n}", Convert.ToInt32(f.init_amt) + Convert.ToInt32(f.tech_amt)); %>
                           <%=new_qty%></td>
                        <td>
                         <% string new_tot_amt1 = string.Format("{0:n}", Convert.ToDouble(f.tot_amt)); %>
                             <%=new_tot_amt1 %></td>
                    </tr>
                     <% i++; amt += Convert.ToInt32(f.tot_amt); Session["tot_amtx"] = amt;
                       } %>
                   
                    
                </table>
            </td>
        </tr>
       
       <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;font-weight:bold;" align="right">
            <td colspan="2" class="right-align">
            <% string new_tot_amtx = string.Format("{0:n}", amt); %>
               <strong><em>TOTAL AMOUNT:</em></strong>&nbsp;<strong> NGN&nbsp;<%=new_tot_amtx%></strong></td>
        </tr>
       
       <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
               
            </td>
        </tr>
       
        
          <tr>
            <td align="center" colspan="2">
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
              <asp:Button ID="BtnDashboard" runat="server" class="button" 
                    Text="Back to Dashboard" onclick="BtnDashboard_Click" />

                &nbsp;<input type="button" name="Printform" id="Printform" value="Print" onclick="printA('searchform');return false" class="button" />
<asp:Button ID="ProceedToPayment" runat="server" Text="Proceed To Payment"  class="button" onclick="ProceedToPayment_Click"/>
         </td>
        </tr>
        </table>
                              

           
    </form>
</body>
</html>
