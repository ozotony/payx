<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_invoice.cs" Inherits="XPay.A.m_invoice"  MaintainScrollPositionOnPostback="true"%>

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
.item_alt {background-color:#E3EAEB; color:#000000; text-align:center;font-weight:bold;font-size:14px; }
.tiger-stripe{ text-align:center;font-weight:bold;font-size:14px;}
</style>
    </head>
<body>
    <form id="form1" runat="server">
   
                
                <div id="searchform">
      <table style="width:80%;" align="center">
        <tr>
        <td>   
    <table class="form left-align"  style="font-size:16px;width:100%;border:1px solid #000000;">
        <tr >
             <td  style="width:50%;" align="left">
                 <img alt="Coat of Arms" height="100%" src="../images/LOGOCLD.jpg" width="100%" /></td>
            <td class="right-align" style="width:50%;"  align="right">
                <img src="../images/payxlogo.jpg"  alt="XPay" width="100px" height="50px" /></td>
        </tr>
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td colspan="2">
                INVOICE FOR TRANSACTION :&nbsp;"<%=lt_twall[0].ref_no.ToUpper() %>" </td>
        </tr>
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="2">
                TRANSACTION ID: "<%=lt_twall[0].transID.ToUpper() %>"
                </td>
        </tr>
        <tr>
            <td align="center">
               <strong> DATE:</strong> <%=xreg_date %></td>
            <td align="center">
                <strong> INVOICE DATE:</strong>  <%=lt_twall[0].xreg_date.ToUpper() %></td>
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
            <td align="center" colspan="2" style="font-size:14px;">
                <table style="width:100%;" id="mitems" class="tiger-stripe" >
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            <strong>S/N</strong></td>
                        <td>
                            <strong>ITEM CODE</strong></td>
                             <td> <strong>DESCRIPTION</strong></td>
                        <td>
                            <strong>QTY</strong></td>
                        <td>
                            <strong>AMOUNT</strong></td>
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
               <strong><em>TOTAL AMOUNT:</em></strong>&nbsp;<strong><%=new_tot_amtx%> NGN</strong></td>
        </tr>
       
       <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
               
            </td>
        </tr>
       
        
          <tr>
            <td align="center" colspan="2">
            POWERED BY<br/><img alt="Einao Solutions" src="../images/einao_logo.png"   width="90px" height="39px"/>
            <img alt="interswitch"   src="../images/isw_logo_small.gif" /><br />
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

                &nbsp;<input type="button" name="Printform" id="Button1" value="Print" onclick="printAssessment('searchform');return false" class="button" />
<asp:Button ID="Button2" runat="server" Text="Proceed To Payment"  class="button" onclick="ProceedToPayment_Click"/>
         </td>
        </tr>
        </table>
                            

           
    </form>
</body>
</html>
