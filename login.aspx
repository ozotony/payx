<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.cs" Inherits="XPay.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XPAY: ADMIN LOGIN</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/funk.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
            <table align="center" width="60%" style="border:1px dashed #000; border-radius:5px;">
             <tr align="center">
                <td colspan="4">
                   <img alt="X Pay"  src="./images/einao_logo.png" width="140px" height="78px" />
               </td>
            </tr>
            
        <tr>
            <td colspan="2" align="center" style="background-color:#1C5E55; color:#ffffff; font-size:18px;">
                &nbsp;PLEASE LOGIN IN USING YOUE E-MAIL ADDRESS AND PASSWORD</td>
        </tr>
       
        
        <tr>
            <td class="right-align">
                &nbsp;
                E-MAIL: &nbsp;</td>
                
            <td rowspan="5" style="width: 50%;">
                <img alt="login" src="images/login.png" style="width: 128px; height: 128px" /></td>
        </tr>
        <tr>
            <td class="right-align">
            <asp:TextBox ID="xemail" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
                <% if (email_text == "1")
                   { %>
                <img src="images/arrow-left.gif" alt="" width="16px" height="16px" />
                <%  } if (enable_Save == "0")
                   { %><img src="images/checkmark.gif" alt="" width="16px" height="16px" />
                <% }%></td>
        </tr>
        
        <tr align="center" style="background-color:#1C5E55; color:#ffffff; font-size:14px;">
            <td class="right-align" > Please note that the letters below are not case sensitive!!!
                </td>
        </tr>
        
        <tr>
            <td class="right-align"> <img alt="captcha" src="./xcaptcha.ashx" /><br />
                </td>
        </tr>
        
        <tr>
            <td class="right-align">
                ENTER CODE : 
            <asp:TextBox ID="xcode" runat="server" Width="90px" CssClass="textbox"></asp:TextBox>
            <% if (code_text == "1")
                   { %>
                <img src="images/arrow-left.gif" alt="" width="16px" height="16px" />
                <% } if (enable_Save == "0")
                   { %><img src="images/checkmark.gif" alt="" width="16px" height="16px" />
                <% }%>     
                </td>
        </tr>
        
        <% if (newState!= "0")
           { %>
        <tr>
            <td colspan="2" align="center">
                <strong>SORRY BUT THE CODE YOU ENTERED IS INVALID.</strong>
            </td>
        </tr>
        <% } %>
        <% if (newp != "0")
           { %>
        <tr>
            <td style="text-align:center;" colspan="2">
                <hr /></td>
        </tr>
        
        
        <tr>
            <td class="right-align">
                PASSWORD: </td>
            <td>
            <asp:TextBox ID="xpassword" runat="server" Width="200px" CssClass="textbox" TextMode="Password" onunload="xpassword_Unload" ></asp:TextBox>
                 </td>
        </tr>        
        <% } %>
         <tr>
            <td style="text-align:center;" colspan="2">
                <hr /></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <% if (enable_Confirm == "0")
                   { %>
                <asp:Button ID="ConfirmDetails" runat="server" Text="Confirm Details" 
                    OnClick="ConfirmDetails_Click" class="button" />
               
                <% }if (enable_Save == "0")
                   { %>
                <asp:Button ID="Save" runat="server" Text="Login" OnClick="Save_Click" 
                    class="button" />
                <% }%>
            </td>
        </tr>
         <tr>
            <td style="text-align:center;" colspan="2">
                <hr /></td>
        </tr>
          <tr>
            <td align="center" colspan="2">
            <b style="font-family:Cambria;font-size:13px;">POWERED BY EINAO SOLUTIONS</b>
            </td>
        </tr>
    </table>
      
</div>
    </form>
</body>
</html>
