<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="charge_back.aspx.cs" Inherits="PayX.P.charge_back" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CLAIMS</title>
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
    <script src="/js/funk.js" type="text/javascript"></script>

 
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

<style type="text/css">
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
                 <table align="center"  style="width:80%" class="form" >
                  <tr align="center">
                <td ><img alt="Einao Solutions" src="../images/einao_logo.png" width="140px" height="78px"  /></td>
            </tr>
        <tr>
            <td style="text-align:center;">
               <hr /> </td>
        </tr>        
                
        <tr>
            <td style="text-align:center; background-color:#ccc; font-size:11px;" >
               <a href="upd_pro.aspx">PROFILE SETTINGS</a> |  <a href="charge_back.aspx">CLAIMS</a> | <a href="pay_his.aspx">VIEW TRANSACTIONS</a> | <a href="pay_stats.aspx">PAYMENT CHARTS</a> | <a href="xmail.aspx">CONTACT SUPPORT</a> <br />
                 <a href="../lo.ashx"><img alt="X Pay"  src="../images/logoff.png" width="30px" height="30px" /></a></td>
        </tr>  
         <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">&nbsp;</td>
        </tr>
         <tr>
            <td align="center" style="font-size:11px;" >
            
                Enter Transaction ID<br />
                <asp:TextBox ID="txt_trans" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td align="center" >
            
                <asp:Button ID="btnSearchTransaction" runat="server" class="button" Text="SEARCH" onclick="btnSearchTransaction_Click" />
                    </td>
        </tr>
         <tr>
            <td align="center" >
                <strong><%=search_msg %></strong>  
                </td>
        </tr>
        
        </table>
        <% if (tm_cnt >0)
           {
               if (show_inv == 0)
               { %>   
               <table id="MerchantDetails"  style="width:80%" align="center">
      <tr  style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td >
                ---   PAYMENT REFERENCE:&nbsp; <strong>"<%=isw_fields.pay_ref %>"</strong> ---
                            
                </td>
        </tr>
        <tr class="center-align" style="background-color:#efefef;">
            <td class="center-align">
                &nbsp;</td>
        </tr>
        
        <tr id="ReportGrid" style="text-align:center;font-size:11px;">
            <td>
                <asp:GridView ID="gvTm" runat="server" AutoGenerateColumns="False"  EnableModelValidation="True" 
                style="margin-top: 0px; width:100%;" CellPadding="4" ForeColor="#333333" GridLines="Both" 
                    CaptionAlign="Left"  HorizontalAlign="Left"
                    AllowPaging="True" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                       <asp:TemplateField HeaderText="S/N">
                            <ItemTemplate>
                                 <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="newtransID" HeaderText="TRANSACTION ID"  >
                             <HeaderStyle HorizontalAlign="Left" Width="170px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="item_code" HeaderText="CODE" ReadOnly="True">
                             <HeaderStyle HorizontalAlign="Left" Width="10px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:BoundField DataField="item_desc" HeaderText="DESCRIPTION" ReadOnly="True">
                             <HeaderStyle HorizontalAlign="Left" Width="200px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         
                             <asp:BoundField DataField="init_amt" HeaderText="AMOUNT (NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                            <asp:BoundField DataField="tech_amt" HeaderText="TECH FEES (NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="isw_amt" HeaderText="ISW FEES (NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="total_amt" HeaderText="TOTAL AMOUNT (NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         
                         <asp:BoundField DataField="used_status" HeaderText="Used Status" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="office_status" HeaderText="Office" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="payment_date" HeaderText="PAYMENT DATE" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left"/>
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB"/>
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>                 
                        
            </td>
        </tr>
<tr class="center-align" style="background-color:#efefef;">
            <td class="center-align">
                &nbsp;</td>
        </tr>
<tr class="center-align" style="background-color:#efefef;">
            <td class="center-align">
               <table  class="center-align inv" width="100%" >
             
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td colspan="4">
                APPLICATION SUMMARY :
            </td>
        </tr>
        

          <tr>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                APPLICANT INFORMATION ---</td>
            <td align="center" colspan="2" style="background-color:#666; color:#ffffff;font-weight:bold;">
                ---
                AGENT INFORMATION ---</td>
        </tr>
        
        <tr>
            <td align="left" style="width:7%;"> NAME:</td>
            <td align="left" style="width:43%;"> <% =c_app.xname%></td>
            <td align="left" style="width:7%;">NAME:  </td>
            <td align="left" style="width:43%;"> <% =isw_fields.cust_name%></td>
        </tr>
        
        <tr style="background-color:#E3EAEB;">
            <td align="left">ADDRESS:</td>
            <td align="left"> <% =c_app.address%></td>
            <td align="left"> CODE:</td>
            <td align="left"> <%=isw_fields.cust_id%></td>
        </tr>
        <tr>
            <td align="left"> E-MAIL:   </td>
            <td align="left"> <%= c_app.xemail%></td>
            <td align="left"> </td>
            <td align="left"></td>
        </tr>
        <tr style="background-color:#E3EAEB;">
            <td align="left">MOBILE: </td>
            <td align="left"><%= c_app.xmobile%></td>
            <td align="left">  </td>
            <td align="left"></td>
        </tr>
          
         <tr>
                        <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" 
                            colspan="4">&nbsp;</td>
                    </tr>
            
         </table></td>
        </tr>
        </table>
        <table style="width:80%;" align="center">
            

             <tr>
			<td align="center" style="width:100%;">
            <asp:Button ID="btnBack" runat="server" class="button" Text="Back" onclick="btnBack_Click"   />
			<input type="button" name="Printform" id="PrintMerchantDetails" value="Print" onclick="PrintPartner('MerchantDetails'); return false" class="button" />
			</td>
            </tr>
             </table>
         <% }
              
           }%>
           <% else
               {%> 
            <table align="center" style="width:80%" >       
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;"></td>
        </tr>
        <tr>
        <td align="center" style="background-color:#000000; color:#ffffff;  font-weight:bold;">
               <strong> NO RECORDS FOUND FOR THE SEARCH QUERY!!</strong></td>
        </tr>
        <tr>
        <td align="center" style="background-color:#1C5E55; color:#ffffff;"></td>
        </tr>
        
        </table>   
 <% }%>
 
                
           
    </div>
    </form>
</body>
</html>
