<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xmail.cs" Inherits="XPay.P.xmail"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PAYMENT HISTORY</title>
     <link href="../css/style.css" rel="stylesheet" type="text/css" /> 
     <link rel="stylesheet" href="../css/jquery.ui.all.css" />
     <link rel="stylesheet" href="../css/jquery.ui.theme.css" />
     <link rel="stylesheet" href="../css/jquery.ui.tabs.css" />
     <link href="../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/ui/jquery.ui.datepicker.js"></script>
    <script src="../js/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../js/funk.js" type="text/javascript"></script>

 
<script type="text/javascript">
    $(function () {
        $("#toDate").datepicker({ changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'yy-mm-dd'
        });
    });
    $(function () {
        $("#fromDate").datepicker({ changeMonth: true,
            changeYear: true,
            showButtonPanel: true
        });
    });		
		
</script>

<style>
a 
{
    color:#000;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
                
<table  align="center" style="width:80%;" class="form">
    <tr align="center">
                <td ><img alt="X Pay" src="../images/einao_logo.png" width="140px" height="78px"  /></td>
            </tr>
        <tr>
            <td  style="text-align:center;">
               <hr /> </td>
        </tr>
        
                
        <tr>
            <td  style="text-align:center; background-color:#ccc;" >
               <a href="upd_pro.aspx">PROFILE SETTINGS</a> | <a href="pay_his.aspx">VIEW TRANSACTIONS</a> | <a href="pay_stats.aspx">PAYMENT CHARTS</a> | <a href="xmail.aspx">CONTACT SUPPORT</a><br />
                 <a href="../lo.ashx"><img alt="X Pay"  src="../images/logoff.png" width="30px" height="30px" /></a></td>
        </tr>  
        <tr>
            <td class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                CATEGORY</td>
        </tr>
        <tr >
            <td  align="center">
                <asp:RadioButtonList ID="rbl_mail_cat" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                <asp:ListItem Value="Complaints" Text="Complaints" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Enquires" Text="Enquires"></asp:ListItem>
                <asp:ListItem Value="Feedback" Text="Feedback"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>

         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
        <tr >
            <td style="text-align:center;" >
                MESSAGE:<br />
                <br />
                <asp:TextBox ID="txt_msg" runat="server" Rows="10" TextMode="MultiLine" Width="80%" CssClass="textbox"></asp:TextBox>
                <br />
                </td>
        </tr>      
       
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
         <tr  class="center-align">
            <td > <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="button" onclick="btnSubmit_Click" /></td>    
            </tr>      
            <%if (xsucc == 1)
              { %>   
        
           <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align">MESSAGE SENT SUCCESSFULLY!!</td>
        </tr>
        <% } %> 
        <%if (xsucc == 2)
              { %>  
       
           <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align"></td>
        </tr>
         <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align">PLEASE ENTER A VALID MESSAGE!!</td>
        </tr>
        <% } %> 
          </table>
           
    </div>
    </form>
</body>
</html>
