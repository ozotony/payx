<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.cs" Inherits="XPay.P.profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MERCHANT PROFILE SECTION</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" /> 
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
    <table align="center" style="width:80%;">
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
            <td colspan="3" align="center" style="background-color:#1C5E55; color:#ffffff;">
                WELCOME TO THE MERCHANT ADMINSTRATION UNIT</td>
        </tr>
       
        
                
        <tr>
            <td align="center" colspan="3">&nbsp;</td>
        </tr>
        
        <tr>
            <td style="width: 30%;" align="center">
                <a href="./pay_stats.aspx">
                    <img alt="" src="../images/xstats.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;" align="center">
                <a href="./upd_pro.aspx">
                    <img alt="" src="../images/register.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 30%;" align="center">
                <a href="./pay_his.aspx">
                    <img alt="" src="../images/history.png" style="width: 100px; height: 100px" /></a></td>
        </tr>
        
        <tr>
            <td style="width: 30%;" align="center">
                <a href="./pay_stats.aspx">PAYMENT CHARTS</a></td>
            <td style="width: 30%;" align="center">
                <a href="./upd_pro.aspx">PROFILE SETTINGS</a></td>
            <td style="width: 30%;" align="center">
                <a href="./pay_his.aspx" >VIEW TRANSACTIONS</a></td>
        </tr>
        
        <tr>
            <td style="width: 30%;" align="center">
                &nbsp;</td>
            <td style="width: 30%;" align="center">
                &nbsp;</td>
            <td style="width: 30%;" align="center">
                &nbsp;</td>
        </tr>
          <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="3">
              
            </td>
        </tr>
       
          <tr>
            <td align="center" colspan="3">
            <b style="font-family:Cambria;font-size:13px;">POWERED BY EINAO SOLUTIONS</b>
            </td>
        </tr>
        
      
    </table>              
    </div>
    </form>
</body>
</html>
