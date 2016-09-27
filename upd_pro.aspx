<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upd_pro.cs" Inherits="XPay.upd_pro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PROFILE UPDATE</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
<script src="js/funk.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="container">
        <div class="sidebar">
            </div>
        <div class="content">
            
            
    <table  id="registration_form" align="center" width="100%" class="form" >           
     <tr align="center">
                <td colspan="4">
                    <img alt="Coat Of Arms" height="79" src="./images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr align="center" style=" font-size:11pt;" >
                <td colspan="4">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
            </tr>
        <tr>
            <td colspan="2" align="center" style="background-color:#1C5E55; color:#ffffff;">
                PROFILE UPDATE : PLEASE FILL IN THE DETAILS BELOW  
            </td>
        </tr>
        
        <tr>
            <td width="30%">
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
            <asp:TextBox ID="xtelephone" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
                </td>
        </tr>
        
        <tr>
            <td>
                &nbsp;E-MAIL:
                </td>
            <td>
            <asp:TextBox ID="xemail" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
                 </td>
        </tr>
        
        <tr>
            <td>
                &nbsp;PASSWORD:&nbsp;</td>
            <td>
            <asp:TextBox ID="xpass" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
                    
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
    </div>
    </div>
    </form>
</body>
</html>
