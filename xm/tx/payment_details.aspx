<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_details.aspx.cs" Inherits="XPay.xm.tx.payment_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../../js/jquery.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;  }
.tiger-stripe{ font-size:12px;}
        </style>
 
</head>
<body>
    <form id="form1" runat="server"> 

                <div id="searchform">
      <table style="width:80%;" align="center">
        <tr>
        <td>  
    <table  style="width:100%;" class="center-align">
      <tr style="margin: 0 auto; ">
                <td colspan="2">
                    <img alt="Coat Of Arms" height="79" src="../../images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr style="font-size:11pt;" class="center-align" >
                <td colspan="2">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
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
                    Einao Convenience Fee: </td>
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
                <asp:Button ID="btnBack" runat="server" class="button" Text="Back" 
                        onclick="btnBack_Click" />
               
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
            <img alt="Einao Solutions" src="../../images/einao_logo.png"   width="180px" height="39px"/><br />
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +234-8098367527, +234-11111 
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
