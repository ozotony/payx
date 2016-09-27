<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_detailst.cs" Inherits="XPay.xis.pd.tx.payment_detailst" %>

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
        </style>
 
</head>
<body>
    <form id="form1" runat="server"> 

                <div id="searchform">
      <table style="width:80%;" align="center">
        <tr>
        <td>  
    <table  style="width:100%;" class="center-align">
    <tr align="center">
                <td  align="left" style="width:50%;">
                   <img alt="Coat of Arms" height="100%" src="../../../images/LOGOCLD.jpg" width="100%" />
               </td>
                <td   align="right" style="width:50%;">
                     <img src="../../../images/payxlogo.jpg"  alt="PayX" width="100px" height="50px" /></td>
            </tr>
        <tr>
            <td  style="background-color:#1C5E55; color:#ffffff;margin: 0 auto; " colspan="2">
               PLEASE VERIFY YOUR DETAILS</td>
        </tr>
       <tr>
            <td colspan="2" style="background-color:#666; color:#ffffff;" class="center-align">
                ---
                APPLICANT INFORMATION ---</td>
        </tr>
        
       <tr  >
                <td  style="width:50%;" class="right-align">
                    Name:&nbsp; </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=c_app.xname%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" class="right-align">
                    Mobile Number: &nbsp;</td>
                <td style="width:50%;" class="left-align">
                  &nbsp;<b><%=c_app.xmobile%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" class="right-align">
                     E-mail: &nbsp;</td>
                <td style="width:50%;" class="left-align">
                      &nbsp;<b><%=c_app.xemail%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" class="right-align">
                    Address: </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=c_app.address%></b></td>
            </tr>
        
        <tr>
            <td class="center-align" colspan="2" style="background-color:#666; color:#ffffff;">
                ---
                AGENT INFORMATION ---</td>
        </tr>
        
       <tr  >
                <td  style="width:50%;" class="right-align">
                    Name:&nbsp; </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=name%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" class="right-align">
                    Company: </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=coy_name%></b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" class="right-align">
                    Address: </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=addy%></b></td>
            </tr>
        <tr>
            <td class="center-align" colspan="2" style="background-color:#666; color:#ffffff;">
                ---
                PAYMENT INFORMATION ---</td>
        </tr>
        

       <tr>
                <td  style="width:50%;" class="right-align">
                    Reference Number: </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=refno.ToUpper()%></b></td>
            </tr>
         <% string amtx = string.Format("{0:n}", Convert.ToDouble(Convert.ToInt32(amt) / 100)); %>
       <tr>
                <td  style="width:50%;" class="right-align">
                    Amount: </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=amtx%> NGN</b></td>
            </tr>
         <% string isw_conv_feex = string.Format("{0:n}", Convert.ToDouble(isw_conv_fee)); %>
       <tr  >
                <td  style="width:50%;" class="right-align">
                    PayX Convenience Fee: </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=isw_conv_feex%> NGN</b></td>
            </tr>
             <% string total_amtx = string.Format("{0:n}", Convert.ToDouble(total_amt) / 100); %>
              <tr  >
                <td  style="width:50%;" class="right-align">
                    Total Amount to be charged: </td>
                <td style="width:50%;" class="left-align">
                    &nbsp;<b><%=total_amtx %> NGN</b></td>
            </tr>
        
       <tr>
                <td  style="width:50%;" class="right-align">
                <input type="button" id="back" value="Back" class="button" onclick="doHistory('-1');return false;"/> 
                </td>
                <td style="width:50%;" class="left-align">
                
                <asp:Button ID="btnPay" runat="server" class="button" Text="Confirm Details" 
                        onclick="btnPay_Click" />
                         
                </td>
            </tr>
     <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" colspan="2" >
               </td>
        </tr>
       
     <tr>
            <td class="center-align" colspan="2">
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
