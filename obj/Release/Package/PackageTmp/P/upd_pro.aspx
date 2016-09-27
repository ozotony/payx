<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upd_pro.cs" Inherits="XPay.P.upd_pro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PROFILE UPDATE</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
<script src="../js/funk.js" type="text/javascript"></script>
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
    <table  id="registration_form" align="center" style="width:80%;" class="form" >           
     <tr align="center">
                <td colspan="4"><img alt="X Pay" src="../images/einao_logo.png" width="140px" height="78px"  /></td>
            </tr>
        <tr>
            <td colspan="4" style="text-align:center;">
               <hr /> </td>
        </tr>
        
                
        <tr>
            <td colspan="4" style="text-align:center; background-color:#ccc;" >
               <a href="upd_pro.aspx">PROFILE SETTINGS</a> |  <a href="charge_back.aspx">CLAIMS</a> | <a href="pay_his.aspx">VIEW TRANSACTIONS</a> | <a href="pay_stats.aspx">PAYMENT CHARTS</a> | <a href="xmail.aspx">CONTACT SUPPORT</a><br />
                 <a href="../lo.ashx"><img alt="X Pay"  src="../images/logoff.png" width="30px" height="30px" /></a></td>
        </tr>  
        <tr>
            <td colspan="2" align="center" style="background-color:#1C5E55; color:#ffffff;">
                PROFILE UPDATE : PLEASE FILL IN THE DETAILS BELOW  
            </td>
        </tr>
        
        <tr>
            <td style="width:30%;">
                &nbsp;&nbsp;NAME:
            </td>
            <td>
                <asp:TextBox ID="xname" runat="server" Width="400px" CssClass="textbox"  ReadOnly="true"></asp:TextBox> 
                                   
                </td>
        </tr>        
        <tr>
            <td>
                                &nbsp;TELEPHONE: &nbsp;</td>
            <td>
            <asp:TextBox ID="xtelephone" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                </td>
        </tr>
        
        <tr>
            <td>
                &nbsp;E-MAIL:
                </td>
            <td>
            <asp:TextBox ID="xemail" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                 </td>
        </tr>
        
        <tr>
            <td>
                &nbsp;PASSWORD:&nbsp;</td>
            <td>
            <asp:TextBox ID="xpass" runat="server" Width="400px" CssClass="textbox"></asp:TextBox>
                    
                </td>
        </tr>
        
        <tr>
            <td colspan="2" align="center" style="background-color:#1C5E55; color:#ffffff;"></td>
        </tr>
        
        <tr>
            <td colspan="2" align="center">
            
                <asp:Button ID="btnBack" runat="server" class="button" Text="Back" 
                    onclick="btnBack_Click"/>
            
                <asp:Button ID="btnAddMember" runat="server" class="button" Text="I confirm that the above entries are correct" 
                    onclick="btnAddMember_Click" />
            </td>
        </tr>
    </table> 
    </div>
    </form>
</body>
</html>
