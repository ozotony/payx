<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_status.aspx.cs" Inherits="XPay.B.m_status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TRANSACTION VALIDATION</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
<script src="../js/funk.js" type="text/javascript"></script>
 <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    
     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;  }
.tiger-stripe{ font-size:12px;}
</style>
  <script type="text/javascript">
      $(function () {
          $("table.tiger-stripe tr:odd").addClass("item_alt");
      });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="container">
        <div class="sidebar">
        <a href="./profile.aspx">PROFILE</a> 
                
                <a href="../upd_pro.aspx">UPDATE PROFILE</a> 
                <a href="./m_status.aspx">VALIDATE PAYMENT</a>
                <a href="../lo.ashx">SIGN OUT</a>  
            </div>
        <div class="content">
            
            
    <table  id="registration_form" class="center-align" width="100%" class="form"  >           
     <tr class="center-align">
                <td colspan="2">
                    <img alt="Coat Of Arms" height="79" src="../images/coat_of_arms.png" 
                        width="85" />
               </td>
            </tr>
            <tr class="center-align" style=" font-size:11pt;" >
                <td colspan="2">
                    FEDERAL REPUBLIC OF NIGERIA<br />
                    FEDERAL MINISTRY OF INDUSTRY, TRADE AND INVESTMENT<br />
                    COMMERCIAL LAW DEPARTMENT<br />
                    TRADEMARKS, PATENTS AND DESIGNS REGISTRY
</td>
            </tr>
        <tr>
            <td colspan="2" class="center-align" style="background-color:#1C5E55; color:#ffffff;">
                TRANSACTION VALIDATION : PLEASE FILL IN ALL THE DETAILS BELOW  
            </td>
        </tr>
        
          
        <tr>
            <td width="30%" colspan="2" class="center-align" style="color:Red;">
                <strong><%=status_msg.ToUpper() %></strong></td>
        </tr>
        <% if (show_inv == 0)
             { %>
        <tr>
            <td width="30%" align="left">
                &nbsp;&nbsp;TRANSACTION NUMBER:
            </td>
            <td align="left">
                <asp:TextBox ID="xtrans" runat="server" Width="400px" CssClass="textbox" ></asp:TextBox> 
                                   
                </td>
        </tr>
        
        <tr>
            <td align="left">
                &nbsp;&nbsp;MOBILE NUMBER :
            </td>
            <td align="left">
                <asp:TextBox ID="xmob" runat="server" Width="400px" CssClass="textbox" ></asp:TextBox> 
                                   
           </td>
        </tr>        
        <tr>
            <td  colspan="2"  class="center-align">
            <asp:RadioButtonList ID="rblagentType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                <asp:ListItem Text="Agent" Value="Agent"></asp:ListItem>
                 <asp:ListItem Text="Sub-Agent" Value="SubAgent"></asp:ListItem>
                </asp:RadioButtonList>
                </td>
        </tr>
              
        
        <tr>
            <td colspan="2" class="center-align">
            
                <asp:Button ID="btnConfirm" runat="server" class="button" Text="Confirm" 
                    onclick="btnConfirm_Click" />
            </td>
        </tr>
             
          <% } %> 
        
          <% if (show_inv > 0)
             { %>
              <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td colspan="2">
                INVOICE FOR TRANSACTION :&nbsp;"<%=lt_twall[0].ref_no.ToUpper() %>" </td>
        </tr>
        <tr >
            <td  width="50%">
                <img alt="Coat of Arms" height="69" src="../images/coat_of_arms.png" 
                    width="88" /></td>
            <td align="right" >
               <img src="../images/einao_logo.png"  alt="XPay" width="30%" height="30%" /></td>
        </tr>
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;">
            <td class="center-align" colspan="2" style="font-size:12px;">
                TRANSACTION ID: "<%=lt_twall[0].transID.ToUpper() %>"
                </td>
        </tr>
        <tr>
            <td class="center-align">
               <strong> DATE:</strong> <%=xreg_date %></td>
            <td class="center-align">
                <strong> INVOICE DATE:</strong>  <%=lt_twall[0].xreg_date.ToUpper() %></td>
        </tr>
        
        <tr>
            <td class="center-align" colspan="2">
                <strong> FULL NAME:</strong>  
                <% = Session["fullname"].ToString()%></td>
        </tr>
        
        <tr>
            <td class="center-align" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="center-align">
                <strong> E-MAIL ADDRESS:</strong>  <%= Session["email"].ToString()%></td>
            <td class="center-align">
               <strong>MOBILE NUMBER:</strong> <%= Session["mobile"].ToString()%> </td>
        </tr>
        <tr>
            <td class="center-align" colspan="2"  style="background-color:#1C5E55; color:#ffffff;">
                <strong>--- PAYMENT DETAILS ---</strong></td>
        </tr>
        <tr>
            <td class="center-align" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" class="center-align">
            
                <table id="mitems" class="tiger-stripe" style="width:100%;">
                    
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            <strong>S/N</strong></td>
                        <td>
                            <strong>ITEM CODE</strong></td>
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
                           <%=f.xqty%></td>
                        <td>
                         <% string new_qty = string.Format("{0:n}", Convert.ToInt32(f.init_amt) + Convert.ToInt32(f.tech_amt)); %>
                           <%=new_qty%></td>
                        <td>
                         <% string new_tot_amt1 = string.Format("{0:n}", f.tot_amt); %>
                             <%=new_tot_amt1%></td>
                    </tr>
                     <% i++; tot_amtx += Convert.ToInt32(f.tot_amt);
                       } %>
                   
                    <tr  style="font-size:13px;text-decoration:underline; color:#1C5E55;">
                        <td align="right" colspan="5"> <% string new_tot_amtx = string.Format("{0:n}", tot_amtx); %>
               <strong><em>TOTAL AMOUNT:</em></strong>&nbsp;<strong><%=new_tot_amtx%> NGN</strong></td>
                    </tr>
                   
                  
        
                    
                 
                  
                </table>
            </td>
        </tr>
              
         <tr>
                        <td class="center-align" style="background-color:#1C5E55; color:#ffffff;"colspan="2">---STATUS: [ <%=paid_status_msg%> ] ---</td>
                    </tr>
                  
                    <tr>
                        <td class="center-align" colspan="2">
            
                <asp:Button ID="btnBack" runat="server" class="button" Text="Search Again" 
                                onclick="btnBack_Click" />
            
                <asp:Button ID="btnValidate" runat="server" class="button" Text="Validate" 
                                onclick="btnValidate_Click" />
                        </td>
                    </tr> 
                  
                     <% } %>
          <tr>
            <td class="center-align" colspan="2">
            <b style="font-family:Cambria;font-size:13px;">POWERED BY EINAO SOLUTIONS</b>
            </td>
        </tr>
    </table> 
       

        </div>
    </div>
    </div>
    </form>
</body>
</html>
