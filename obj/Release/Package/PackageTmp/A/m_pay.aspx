<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_pay.cs" Inherits="XPay.A.m_pay"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

     <style type="text/css">
.item_alt {background-color:#E3EAEB; color:#000000;text-align:left;font-weight:bold;font-size:14px;}
.tiger-stripe{ font-weight:bold;font-size:14px;}
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
        <table style="width:80%;" align="center">
        <tr>
        <td> 
    <table align="center" width="100%">
    <tr >
            <td  style="width:50%;" align="left">
                <img alt="Coat of Arms" height="100%" src="../images/LOGOCLD.jpg" width="100%" /></td>
            <td class="right-align" style="width:50%;" align="right">
               <img src="../images/payxlogo.jpg"  alt="XPay" width="100px" height="50px" /></td>
        </tr>
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
                &nbsp;</td>
        </tr>
        <tr >
            <td colspan="2" ><strong>PLEASE FOLLOW THE INSTRUCTIONS BELOW:</strong><br /><br />
                (1) To &quot;Add&quot; an item, put in the quantity&nbsp; next to the 
                item to purchase and the click on
                <img alt="Add" height="16" src="../images/checkmark.gif" width="16" /><br />
                (2) To &quot;Remove an item, click on
                <img alt="Remove" height="16" src="../images/x.gif" width="16" /><br />
                (3) After you have finished selecting items, click on the &quot;CONFIRM&quot; button below<br />
                (4) You can &quot;CHANGE ITEMS&quot; after viewing your basket<br />
                (5) Click on &quot;CHECK OUT&quot; to process your invoice and recieve you transaction 
                PIN.</td>
        </tr>
  <% if (show_inv == 0)
     { %>
         <tr >
            <td  colspan="2">
            <table id="applicant_info" style="width:100%;" class="center-align">
            <tr style="background-color:#666; color:#ffffff;" class="center-align" >
            <td>
            
                APPLICANT INFORMATION FORM</td>
            </tr>
             <tr>
            <td>
            
                NAME:
                <br />
            <asp:TextBox ID="txt_app_name" runat="server" CssClass="textbox" Width="600px"></asp:TextBox>
            </td>
            </tr>
             <tr>
            <td>
            
                ADDRESS:<br />
           <asp:TextBox ID="txt_app_addy" runat="server" CssClass="textbox" Width="600px" Rows="6" TextMode="MultiLine"></asp:TextBox>
            </td>
            </tr>
             <tr>
            <td>
            
                E-MAIL:<br />
                <asp:TextBox ID="txt_app_email" runat="server" CssClass="textbox" Width="600px"></asp:TextBox>
                 </td>
            </tr>
             <tr>
            <td>
            
                MOBILE NUMBER:<br />
                <asp:TextBox ID="txt_app_no" runat="server" CssClass="textbox" Width="600px" ></asp:TextBox>
                 </td>
            </tr>
             <tr style="background-color:#666; color:#ffffff;">
            <td>
            
                &nbsp;</td>
            </tr>
             <tr align="center">
            <td>
            
              <asp:Button ID="BtnDashboard1" runat="server" class="button" 
                    Text="Back to Dashboard" onclick="BtnDashboard_Click" />

                <asp:Button ID="btnProceedToPurchase" runat="server" class="button"  Text="Proceed to purchase" onclick="btnProceedToPurchase_Click" />
            
            </td>
            </tr>
            </table>
            </td>
        </tr>
  <%} %>

        <% if (show_inv == 1)
           { %>
         <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center" colspan="2"></td>
        </tr>
        <tr align="center" >
            <td align="center" colspan="2">
            
                <asp:Button ID="btnApplicant" runat="server" class="button"  Text="Back to Applicant Form" onclick="btnApplicant_Click"/>
            
            </td>
        </tr>
       
        <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center" colspan="2">
                --- TRADEMARK ---</td>
        </tr>
        <tr align="center" style="background-color:#efefef;">
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:GridView ID="gvTm" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flTm" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvTm_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="Both">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                        <asp:BoundField DataField="xid" HeaderText="S/N" InsertVisible="False" 
                            ReadOnly="True" SortExpression="xid" >
                             <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                        <asp:BoundField DataField="item_code" HeaderText="ITEM CODE" 
                            SortExpression="item_code" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>

                       <asp:BoundField DataField="xdesc" HeaderText="DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="750px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Left" Width="10px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtTm" runat="server" Height="21px" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:ImageButton ID="lbAddTm" ImageUrl="../images/checkmark.gif" runat="server" Height="16px" CommandName="TmStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>


                <asp:SqlDataSource ID="flAg" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='ag'">
                </asp:SqlDataSource>

                 <asp:SqlDataSource ID="flTm" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' ">
                </asp:SqlDataSource>

                 <asp:SqlDataSource ID="flDs" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='ds' ">
                </asp:SqlDataSource>

                 <asp:SqlDataSource ID="flPt" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='pt'">
                </asp:SqlDataSource>
            </td>
        </tr>
       <tr >
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
                            
                <asp:Label ID="Label2" runat="server" Text="&lt;i&gt;Total Items&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblTmTotItems" runat="server" Text="0"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Quantiy&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblTmTotQty" runat="server" Text="0"></asp:Label>
                <asp:Label ID="Label1" runat="server" 
                    Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Amount&lt;/i&gt; : NGN "></asp:Label>
                <asp:Label ID="lblTmTotAmt" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
       
         <tr >
            <td align="center" colspan="2">&nbsp;</td>
        </tr>
       <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center" colspan="2">
                --- PATENT ---</td>
        </tr>
       <tr align="center" style="background-color:#efefef;">
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
         <tr >
            <td align="center" colspan="2">
                <asp:GridView ID="gvPt" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flPt" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvPt_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="Both">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                        <asp:BoundField DataField="xid" HeaderText="S/N" InsertVisible="False" 
                            ReadOnly="True" SortExpression="xid" >
                             <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                        
                        <asp:BoundField DataField="item_code" HeaderText="ITEM CODE" 
                            SortExpression="item_code" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>

                         <asp:BoundField DataField="xdesc" HeaderText="DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="790px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Left" Width="15px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPt" runat="server" Height="21px" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:ImageButton ID="lbAddPt" ImageUrl="../images/checkmark.gif" runat="server" Height="16px" CommandName="PtStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView></td>
        </tr>
        <tr >
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
                            
                <asp:Label ID="Label3" runat="server" Text="&lt;i&gt;Total Items&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblPtTotItems" runat="server" Text="0"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Quantiy&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblPtTotQty" runat="server" Text="0"></asp:Label>
                <asp:Label ID="Label5" runat="server" 
                    Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Amount&lt;/i&gt; : NGN "></asp:Label>
                <asp:Label ID="lblPtTotAmt" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
      
         <tr >
            <td align="center" colspan="2">&nbsp;</td>
        </tr>

         <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center" colspan="2">
                --- DESIGN ---</td>
        </tr>

         <tr align="center" style="background-color:#efefef;">
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
         <tr >
            <td align="center" colspan="2">
                <asp:GridView ID="gvDs" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flDs" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvDs_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="Both">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                        <asp:BoundField DataField="xid" HeaderText="S/N" InsertVisible="False" 
                            ReadOnly="True" SortExpression="xid" >
                             <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                        <asp:BoundField DataField="item_code" HeaderText="ITEM CODE" 
                            SortExpression="item_code" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>

                       <asp:BoundField DataField="xdesc" HeaderText="DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="790px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Left" Width="15px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDs" runat="server" Height="21px" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:ImageButton ID="lbAddDs" ImageUrl="../images/checkmark.gif" runat="server" Height="16px" CommandName="DsStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView></td>
        </tr>
        <tr >
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
                            
                <asp:Label ID="Label7" runat="server" Text="&lt;i&gt;Total Items&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblDsTotItems" runat="server" Text="0"></asp:Label>
                 <asp:Label ID="Label8" runat="server" Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Quantiy&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblDsTotQty" runat="server" Text="0"></asp:Label>
                <asp:Label ID="Label9" runat="server" 
                    Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Amount&lt;/i&gt; : NGN "></asp:Label>
                <asp:Label ID="lblDsTotAmt" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
      
         <tr >
            <td align="center" colspan="2">&nbsp;</td>
        </tr>

         <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center" colspan="2">
                --- ACCREDITATION ---</td>
        </tr>

         <tr align="center" style="background-color:#efefef;">
            <td align="center" colspan="2">
                &nbsp;</td>
        </tr>
         <tr >
            <td align="center" colspan="2">
                <asp:GridView ID="gvAg" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="Both">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                        <asp:BoundField DataField="xid" HeaderText="S/N" InsertVisible="False" 
                            ReadOnly="True" SortExpression="xid" >
                             <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                        <asp:BoundField DataField="item_code" HeaderText="ITEM CODE" 
                            SortExpression="item_code" >
                            <HeaderStyle HorizontalAlign="Left" Width="70px" />
                            <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>

                       <asp:BoundField DataField="xdesc" HeaderText="DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="750px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Left" Width="15px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="QUANTITY">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAg" runat="server" Height="21px" Width="60px"></asp:TextBox>
                            </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/checkmark.gif" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView></td>
        </tr>
        <tr >
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="2">
                            
                <asp:Label ID="Label11" runat="server" Text="&lt;i&gt;Total Items&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblAgTotItems" runat="server" Text="0"></asp:Label>
                 <asp:Label ID="Label10" runat="server" Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Quantiy&lt;/i&gt; : "></asp:Label>
                <asp:Label ID="lblAgTotQty" runat="server" Text="0"></asp:Label>
                <asp:Label ID="Label13" runat="server" 
                    Text="&lt;i&gt;&nbsp;&nbsp;&nbsp;&nbsp;Total Amount&lt;/i&gt; : NGN "></asp:Label>
                <asp:Label ID="lblAgTotAmt" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
         <tr >
            <td align="center" colspan="2">
              <asp:Button ID="BtnDashboard0" runat="server" class="button" 
                    Text="Back to Dashboard" onclick="BtnDashboard_Click" />

                <asp:Button ID="btnConfirm" runat="server" class="button" Text="Confirm Selected Items" 
                    onclick="btnConfirm_Click"  />
               
                <asp:Button ID="btnApplicant0" runat="server" class="button"  Text="Back to Applicant Form" onclick="btnApplicant_Click"/>
            
                </td>
        </tr>
          <% } %>
      
     
          <% if (show_inv ==2)
             { %>
         <tr >
            <td align="center" colspan="2">
                <table id="mitems" class="tiger-stripe" style="width:100%;">
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            <strong>S/N</strong></td>
                        <td>
                            <strong>ITEM CODE</strong></td>
                        <td>
                            <strong>QUANTITY</strong></td>
                        <td>
                            <strong>AMOUNT</strong></td>
                        <td>
                            <strong>TOTAL (<em>NGN</em> )</strong></td>
                    </tr>
                    <% int i = 1;
                       foreach (XPay.Classes.XObjs.Shopping_card f in lt_cart)
                       { %>
                    <tr>
                        <td>
                            <%=i%>
                        </td>
                        <td>
                           <%=f.item_code%>
                        </td>
                        <td>
                           <%=f.qty%>
                        </td>
                        <td>
                        <% string new_amt = string.Format("{0:n}", f.amt); %>
                           <%=new_amt%>
                        </td>
                        <td>
                        <% string new_qty = string.Format("{0:n}", f.amt * Convert.ToInt32(f.qty)); %>
                             <%=new_qty%>
                        </td>
                    </tr>
                     <% i++; tot_amtx += Convert.ToInt32(f.amt) * Convert.ToInt32(f.qty);
                       } %>
                   
                    <tr>
                        <td class="right-align" colspan="5"></td>
                    </tr>
                  
                </table>
             </td>
        </tr>
          <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;">
            <td >
                            
                </td> <% string new_tot_amtx = string.Format("{0:n}", tot_amtx); %>
            <td  class="right-align"> <strong><em>TOTAL AMOUNT:</em></strong>&nbsp;<strong><%=new_tot_amtx%> NGN</strong></td>
        </tr>
          <tr >
            <td colspan="2"  align="center"> 
              <asp:Button ID="BtnDashboard" runat="server" class="button" 
                    Text="Back to Dashboard" onclick="BtnDashboard_Click" />

                <asp:Button ID="btnChangeItems" runat="server" class="button" 
                    Text="Change Items" onclick="btnChangeItems_Click"  />
               
                <asp:Button ID="btnGo" runat="server" class="button" Text="Check Out" 
                    onclick="btnGo_Click" /></td>
        </tr>
          <% } %>
        
          <tr >
            <td colspan="2"  align="center" style="background-color:#1C5E55; color:#ffffff;"> 
                </td>
        </tr>
          <tr >
            <td colspan="2"  align="center"> 
              POWERED BY<br/><img alt="Einao Solutions" src="../images/einao_logo.png"   width="90px" height="39px"/>
            <img alt="interswitch"   src="../images/isw_logo_small.gif" /><br />
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +2349038979681 </td>
        </tr>
                  
          </table>
                </td>
        </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
