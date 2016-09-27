<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.cs" Inherits="XPay.X.profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    body
    {
        font-size:12px;
    }
a 
{
    color:#000;    
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="text-align:center;width:100%">
     <tr align="center">
                <td colspan="3">
                   <img alt="X Pay"  src="../images/einao_logo.png" width="140px" height="78px" />
               </td>
            </tr>
        <tr>
            <td colspan="3" style="text-align:center;">
               <hr /> </td>
        </tr>
        
                
        <tr>
            <td colspan="3" style="text-align:center; background-color:#ccc;" >
               <a href="profile.aspx">PROFILE</a> | <a href="m_items.aspx">ITEMS</a> | <a href="m_m.aspx">MERCHANTS</a> | <a href="m_a.aspx">ADMINISTRATION</a> | <a href="m_struc.aspx">PAYMENT STRUCTURE</a> | <a href="charge_back.aspx">CLAIMS</a>
                | <a href="pay_his.aspx">VIEW TRANSACTIONS</a> | <a href="pay_stats.aspx">PAYMENT CHARTS</a><br />
                 <a href="../lo.ashx"><img alt="X Pay"  src="../images/logoff.png" width="30px" height="30px" /></a></td>
        </tr>  
                
        <tr>
            <td colspan="3" style="text-align:center;" >
                <hr /></td>
        </tr>
                
        <tr>
            <td colspan="3"  style="background-color:#1C5E55; color:#ffffff;text-align:center;">
                WELCOME TO THE ADMINSTRATION UNIT</td>
        </tr>
       
        
                
        <tr>
            <td align="center" colspan="3">&nbsp;</td>
        </tr>
        
        <tr>
            <td style="width: 33%;" align="center">
                <a href="./m_items.aspx">
                    <img alt="" src="../images/basket.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 33%;" align="center">
                <a href="./m_m.aspx">
                    <img alt="" src="../images/history.png" style="width: 100px; height: 100px" /></a></td>
            <td  style="width: 33%;" align="center">
                <a href="./m_a.aspx">
                    <img alt="" src="../images/admin.png" style="width: 100px; height: 100px" /></a></td>
        </tr>
        
        <tr>
            <td style="width: 33%;" align="center">
                <a href="./m_items.aspx">MANAGE ITEMS</a></td>
            <td  style="width: 33%;" align="center">
                <a href="./m_m.aspx" >MANAGE MERCHANTS</a></td>
            <td  style="width: 33%;" align="center">
                <a href="./m_a.aspx">MANAGE ADMINISTRATORS</a></td>
        </tr>
        
        <tr>
            <td align="center" colspan="3">
                <hr /></td>
        </tr>
        
         <tr>
            <td  align="center">
                <a href="./m_struc.aspx">
                    <img alt="" src="../images/payments.png" style="width: 100px; height: 100px" /></a></td>
            <td  align="center">
                <a href="./charge_back.aspx">
                    <img alt="" src="../images/payments.png" style="width: 100px; height: 100px" /></a></td>
            <td  align="center">
                <a href="./pay_his.aspx">
                    <img alt="" src="../images/history.png" style="width: 100px; height: 100px" /></a></td>
        </tr>
         
        <tr>
            <td  align="center">
                <a href="./m_struc.aspx" >MANAGE PAYMENT STRUCTURE</a></td>
            <td  align="center">
                <a href="./charge_back.aspx">MANAGE CLAIMS</a></td>
            <td  align="center">
                <a href="./pay_his.aspx">PAYMENT HISTORY</a></td>
        </tr>
         <tr>
            <td align="center" colspan="3">
                <hr /></td>
        </tr>
         <tr>
            <td style="width: 33%;" align="center">
                <a href="../Requery_Tool.aspx">
                    <img alt="" src="../images/history.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 33%;" align="center">
                <a href="./pay_stats.aspx">
                    <img alt="" src="../images/history.png" style="width: 100px; height: 100px" /></a></td>
            <td style="width: 33%;" align="center">
                &nbsp;</td>
        </tr> 
         <tr>
            <td align="center" colspan="3">
               <table></table>
                  <tr>
            <td style="width: 33%;" align="center">
                <a href="../Requery_Tool.aspx">REQUERY TRANSACTION</a></td>
            <td style="width: 33%;" align="center">
                <a href="./pay_stats.aspx">PAYMENTS CHARTS</a></td>
            <td style="width: 33%;" align="center">
                &nbsp;</td>
        </tr> 

            </td>
        </tr> 
         <tr>
            <td style="width: 33%;" align="center">
                &nbsp;</td>
            <td style="width: 33%;" align="center">
                &nbsp;</td>
            <td style="width: 33%;" align="center">
                &nbsp;</td>
        </tr> 
          <tr>
            <td  colspan="3">
              <hr /> 
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
