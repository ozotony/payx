<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_his.cs" Inherits="XPay.X.pay_his" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" data-ng-app="myModule">
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
    <script src="/js/funk.js" type="text/javascript"></script>
    <script  type="text/javascript" src="../js/angular.min.js"></script>
    <script  type="text/javascript" src="../js/alasql.min.js"></script>

    <script  type="text/javascript" src="../js/ng-csv.min.js"></script>
    <script  type="text/javascript" src="../js/xlsx.core.min.js"></script>
     <script  type="text/javascript" src="../js/angular-sanitize.min.js"></script>
    <script  type="text/javascript" src="../js/AngularLogin3.js"></script>
    <link href="../css/loading-bar.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/loading-bar.js"></script>
     <script src="../js/smart-table.min.js"></script>
   
 
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
<body ng-controller="myController" >
    <form id="form1" runat="server">
    <div>
                 <table align="center"  style="width:80%" class="form" >
                  <tr align="center">
                <td colspan="8" ><img alt="Einao Solutions" src="../images/einao_logo.png" width="140px" height="78px"  /></td>
            </tr>
        <tr>
            <td colspan="8" style="text-align:center;">
               <hr /> </td>
        </tr>        
                
        <tr>
            <td colspan="8" style="text-align:center; background-color:#ccc; font-size:11px;" >
              <a href="profile.aspx">PROFILE</a> | <a href="m_items.aspx">ITEMS</a> | <a href="m_m.aspx">MERCHANTS</a> | <a href="m_a.aspx">ADMINISTRATION</a> | <a href="m_struc.aspx">PAYMENT STRUCTURE</a> 
                | <a href="charge_back.aspx">CLAIMS</a> | <a href="pay_his.aspx">VIEW TRANSACTIONS</a> | <a href="pay_stats.aspx">PAYMENT CHARTS</a><br />
                 <a href="../lo.ashx"><img alt="X Pay"  src="../images/logoff.png" width="30px" height="30px" /></a></td>
        </tr>  
        <tr>
            <td align="right" style="width:25%;font-size:11px;" colspan="2">
              <strong>GRAND TOTAL ITEM(S):</strong></td>
            <td align="left" style="width:25%; font-weight:bold; color:Green;font-size:11px;" colspan="2">
                &nbsp;
                <%=Session["grand_tot_cnt"].ToString()%></td>
            <td align="right" style="width:25%;font-size:11px;" colspan="2">
             <strong>GRAND TOTAL AMOUNT:</strong></td>
            <td align="left" style="width:25%;font-weight:bold; color:Green;font-size:11px;" colspan="2">
                &nbsp;
                <%=Session["new_grand_tot_amt"].ToString()%> NGN</td>

            <td align="right" style="width:25%;font-size:11px;" colspan="2">
             <strong>GRAND TECH AMOUNT:</strong></td>

             <td align="left" style="width:25%;font-weight:bold; color:Green;font-size:11px;" colspan="2">
                &nbsp;
                <%=Session["new_grand_tot_amt2"].ToString()%> NGN</td>
        </tr>
         <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="8">&nbsp;</td>
        </tr>
         <tr style="font-size:11px;">
            <td align="center" >
            
                </td>
            <td align="center" colspan="2" >
               Payment Status<br />
                 <asp:DropDownList ID="ddl_status" runat="server" >
                <asp:ListItem Value="1" Text="Paid" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                </td>
            <td align="center" colspan="2" >
                Mode<br />
                 <asp:DropDownList ID="ddl_mode" runat="server" >
                <asp:ListItem Value="xpay_isw" Text="Interswitch" Selected="True"></asp:ListItem>
                </asp:DropDownList>
                </td>
            <td align="center" colspan="2" >
                 From:<br />
                <asp:TextBox ID="fromDate" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
            <td align="center" >
                To:<br />
                <asp:TextBox ID="toDate" runat="server" CssClass="textbox"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td align="center" colspan="8" >
            
&nbsp;
             &nbsp;<asp:Button ID="btnSearch" runat="server" class="button" Text="SEARCH" 
                    onclick="btnSearch_Click"   />
                    </td>
        </tr>
         <tr>
            <td align="center" colspan="8" >
            
               <hr /></td>
        </tr>
         <tr>
            <td align="center" colspan="8" style="font-size:11px;" >
            
                Enter Transaction ID (E.g. : <strong>"5FAA10DF9943"</strong>)<br />
                <asp:TextBox ID="txt_trans" runat="server" CssClass="textbox" Width="300px"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td align="center" colspan="8" >
            
                <asp:Button ID="btnSearchTransaction" runat="server" class="button" Text="SEARCH" onclick="btnSearchTransaction_Click" />
                    </td>
        </tr>
         <tr>
            <td align="center" colspan="8" >
                <strong><%=search_msg %></strong>  
                </td>
        </tr>
        
        </table>
        <% if (tm_cnt >0)
           {
               if (show_inv == 0)
               { %>   
               <table   style="width:80%" align="center">
      <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="8">
                --- <%=tm_cnt %> RECORDS FOUND FOR THE FOLLOWING CRITERIA ---<br /><strong></strong>
             &nbsp;&nbsp;Payment Status&nbsp;[ <strong>"<%=ddl_status.SelectedItem.Text%>"</strong> ]&nbsp;&nbsp;Mode&nbsp;[ <strong>"<%=ddl_mode.SelectedItem.Text%>"</strong> ]&nbsp;&nbsp;Initial Date&nbsp;[ <strong>"<%=fromDate.Text %>"</strong> ]&nbsp;&nbsp;Final Date&nbsp;[ <strong>"<%=toDate.Text%>"</strong> ]                
                </td>
        </tr>
        <tr class="center-align" style="background-color:#efefef;">
            <td class="center-align" colspan="8">
                &nbsp;</td>
        </tr>
        <tr id="ReportGrid" style="text-align:center;font-size:11px;">
            <td colspan="8">
                <asp:GridView ID="gvTm" runat="server" AutoGenerateColumns="False"  EnableModelValidation="True" 
                style="margin-top: 0px; width:100%;" onrowcommand="gvTm_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="Both" CaptionAlign="Left"  HorizontalAlign="Left"
                    AllowPaging="True"  onpageindexchanging="gvTm_PageIndexChanging" PageSize="50" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                       <asp:TemplateField HeaderText="S/N">
                            <ItemTemplate>
                                 <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="transID" HeaderText="TRANSACTION ID"  >
                             <HeaderStyle HorizontalAlign="Left" Width="170px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                           <asp:BoundField DataField="TransactionDate" HeaderText="PAYMENT DATE" >
                            <HeaderStyle HorizontalAlign="Left" Width="120px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                                               
                         
                             <asp:BoundField DataField="full_amt" HeaderText="AMOUNT (NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                            <asp:BoundField DataField="isw_amt" HeaderText="ISW FEES (NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="applicant_name" HeaderText="APPLICANT NAME (NGN)" >
                            <HeaderStyle HorizontalAlign="Left" Width="300px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:TemplateField HeaderText="DETAILS" >
                             <ItemTemplate>
                              <asp:ImageButton ID="lbDetTm" ImageUrl="../images/search.gif" runat="server" Height="16px" CommandName="TmDetailsClick"  CommandArgument='<%#Eval("transID") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="40px"/>
                             <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          
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
        <tr>
        <td align="center">
          
        <input type="button" name="Printform" id="Printform" value="Print" onclick="PrintPartner('ReportGrid');return false" class="button" />
            <asp:Button ID="btnExportExcel" runat="server" Visible="false" class="button" onclick="btnExportExcel_Click" Text="Export Excel" />
             <button type="button" class="button" ng-click="Export()" >Export To Excel</button> 
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

        <% if (show_details_grid > 0)
             { %>  
             <table style="width:80%;" align="center">
             <tr>
             <td>

                 <table  id="MerchantDetails" class="center-align inv" width="100%" >
             
         <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;border:1px solid #000000;">
            <td colspan="4">
                INVOICE/RECIEPT FOR TRANSACTION :&nbsp;"<%if (twall.xid!=null) { Response.Write(twall.ref_no.ToUpper()); }%>" </td>
        </tr>
        

        <tr>
            <td align="center" style="width:50%;" colspan="2">
               <strong> TRANSACTION ID:</strong><%if (twall.xid != null) { Response.Write(twall.transID.ToUpper()); }%></td>
            <td align="center" style="width:50%;" colspan="2">
                <strong> DATE:</strong>  <%=isw_fields.TransactionDate%></td>
        </tr>
        
          <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                </td>
        </tr>
            <tr style=" text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
                PAYMENT REFERENCE:&nbsp;"<%=isw_fields.pay_ref %>"
                </td>
        </tr>
        <tr style="background-color:#1C5E55; color:#ffffff; text-align:center;font-weight:bold;">
            <td align="center" colspan="4">
               &nbsp;
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
            <td align="left" style="width:43%;"> <% =fullname%></td>
        </tr>
        
        <tr style="background-color:#E3EAEB;">
            <td align="left">ADDRESS:</td>
            <td align="left"> <% =c_app.address%></td>
            <td align="left"> CODE:</td>
            <td align="left"> <%=cust_id%></td>
        </tr>
        <tr>
            <td align="left"> E-MAIL:   </td>
            <td align="left"> <%= c_app.xemail%></td>
            <td align="left"> E-MAIL:</td>
            <td align="left"> <%= email%></td>
        </tr>
        <tr style="background-color:#E3EAEB;">
            <td align="left">MOBILE: </td>
            <td align="left"><%= c_app.xmobile%></td>
            <td align="left"> MOBILE: </td>
            <td align="left"><%= mobile%></td>
        </tr>
        <tr>
            <td align="center" colspan="4"  
                style="background-color:#666; color:#ffffff;font-weight:bold;">
                <strong>--- PAYMENT DETAILS ---</strong></td>
        </tr>

        <tr>
            <td colspan="4" align="left">            
                <table style="width:100%;" id="Table2" class="tiger-stripe" >
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            S/N</td>
                        <td> ITEM CODE</td>
                        
                          <td>ITEM DESCRIPTION</td>
                        <td>
                            QTY</td>
                        <td style="text-align:center;"> APPLICATION FEE(NGN)</td>
                              <td style="text-align:center;"> TECH. FEE(NGN)</td>
                        <td style="text-align:center;">
                            TOTAL (NGN)</td>
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
                           <%=ret.getFee_listByID(f.fee_listID).item%></td>
                        <td>
                           <%=f.xqty%></td>

                        <td align="right">
                         <% string new_init_amt = string.Format("{0:n}", Convert.ToInt32(f.init_amt)); %>
                           <%=new_init_amt%></td>

                            <td align="right">
                         <% string new_tech_amt = string.Format("{0:n}", Convert.ToInt32(f.tech_amt)); %>
                           <%=new_tech_amt%></td>

                        <td align="right">
                         <% string new_tot_amt1 = string.Format("{0:n}", Convert.ToDouble(f.tot_amt)); %>
                             <%=new_tot_amt1 %></td>
                    </tr>
                     <% i++; amt += Convert.ToInt32(f.tot_amt); Session["tot_amtx"] = amt;
                       } %>
                   
                   <tr style="text-align:right;font-weight:bold; font-size:11px;">
                        <td colspan="6" >
                            PayX Convenience Fee:&nbsp;</td>

                        <td align="right">
                            &nbsp;<%=Math.Round(Convert.ToDouble(isw_fields.isw_conv_fee),2) %></td>
                    </tr>  
                </table>
            </td>
        </tr>
          
            <tr >
            <td colspan="4" class="right-align">&nbsp;</td>
        </tr>
       
       <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;font-weight:bold;" align="right">
            <td colspan="4" class="right-align">
            <% string new_tot_amtx = string.Format("{0:n}", (amt + Convert.ToDouble(isw_fields.isw_conv_fee))); %>
               TOTAL AMOUNT:&nbsp; NGN&nbsp;<%=new_tot_amtx%></td>
        </tr>
            
         <tr>
                        <td class="center-align" style="background-color:#1C5E55; color:#ffffff;" 
                            colspan="4">&nbsp;</td>
                    </tr>
    </table>
             </td>
             </tr>

             <tr>
			<td align="center" style="width:100%;">
            <asp:Button ID="btnBack" runat="server" class="button" Text="Back" onclick="btnBack_Click"   />
			<input type="button" name="Printform" id="PrintMerchantDetails" value="Print" onclick="PrintPartner('MerchantDetails');return false" class="button" />
			</td>
            </tr>
             </table> 
         
 <% } %> 
  
           
    </div>
    </form>
   
</body>
</html>
