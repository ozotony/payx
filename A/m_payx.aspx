<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_payx.cs" Inherits="XPay.A.m_payx"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FRESH PAYMENT SECTION</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" /> 
     <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

     <style type="text/css">
.tiger-stripe{text-align:left;font-weight:normal;font-size:11px; border:1px solid #000;}
.tiger-stripe tr:nth-child(odd) {background: #E3EAEB;color:#000000;text-align:left;font-weight:normal;font-size:11px; border:1px solid #000;}
</style>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table style="width:90%;" align="center">
        <tr>
        <td> 
                <div id="searchform">
        
    <table align="center" width="100%" style="width:100%; border:1px dashed #999;border-radius:5px; ">
   <tr >
            <td align="center">
                 <img alt="Coat of Arms"  src="../images/LOGOCLD.jpg" width="458" height="76" /></td>
        </tr>
           
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">
                &nbsp;</td>
        </tr>
        <tr >
            <td ><span class="notice_header">USER GUIDE:</span><br /><br />
            <% if (show_inv == 0)
     { %>
      <strong> Step 1:</strong> Please complete the Applicant Information form and Click &quot;Proceed to Purchase&quot; to continue<br />
    <% }%>

     <% if (show_inv == 1)
     { %>
     <strong> Step 2:</strong>  This section enables you add and remove items from your online basket. 
                <br />
                <br />
     You can add/remove as many items as needed to generate a single invoice and transaction ID for bulk payment using either the Web payment or Bank Deposit gateway<br /><br />


     <strong> --&nbsp;</strong>To add a service to your basket, type in the quantity and Click &quot;Add&quot; -->&nbsp;<img alt="Add" height="16" src="../images/add_btn.png"  /><br />
     <strong> --&nbsp;</strong>To remove a service from your basket,  Click &quot;Remove&quot; -->&nbsp;<img alt="Remove" height="16" src="../images/remove.png"  /><br />
     <strong> --&nbsp;</strong>To edit the applicant information, Click &quot;Back to Applicant Form&quot;  <br />         
     <strong> --&nbsp;</strong>To continue, click &quot;Confirm Selected Items&quot;    
                <br />
                <br />       
    <% }%>

     <% if (show_inv == 2)
     { %>
     <strong> Step 3:</strong>  This section enables you preview your selections and the Amount due.<br /><br />
                 <strong> --&nbsp;</strong>To review items selected , Click &quot;Change Items&quot;<br />
                 <strong> --&nbsp;</strong>To continue and generate an invoice/ Transaction ID, Click &quot;Check Out&quot;<br />     
    <% }%>

                </td>
        </tr>
  <% if (show_inv == 0)
     { %>
         <tr >
            <td>
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
            
              <asp:Button ID="BtnDashboard0" runat="server" class="button" 
                    Text="Back to Dashboard" onclick="BtnDashboard_Click" />

                <asp:Button ID="btnProceedToPurchase" runat="server" class="button"  Text="Proceed to Purchase" onclick="btnProceedToPurchase_Click" />
            
            </td>
            </tr>
            </table>
            </td>
        </tr>
  <%} %>

        <% if (show_inv == 1)
           { %>
         <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center"></td>
        </tr>
        <tr align="center" >
            <td align="center">
            
                <asp:Button ID="btnApplicant" runat="server" class="button"  Text="Back to Applicant Form" onclick="btnApplicant_Click"/>
            
            </td>
        </tr>
       
        <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center">
                --- TRADEMARK ---</td>
        </tr>
        <tr align="center" style="background-color:#efefef;">
            <td align="center">
                &nbsp;</td>
        </tr>
        <asp:Panel ID="ppp" runat="server"> 
        <tr>
            <td align="center">
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddTm" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="TmStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
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
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='ag' and item_code not in ('T003','T008') ">
                </asp:SqlDataSource>

                 <asp:SqlDataSource ID="flAg2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='ag' and xid='10061'">
                </asp:SqlDataSource>


                 <asp:SqlDataSource ID="flAg3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='3'">
                </asp:SqlDataSource>

                  <asp:SqlDataSource ID="flAg4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='8'">
                </asp:SqlDataSource>

                  <asp:SqlDataSource ID="flAg5" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='21'">
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="flAg6" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='14'">
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="flAg7" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='9'">
                </asp:SqlDataSource>

                  <asp:SqlDataSource ID="flAg8" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='22'">
                </asp:SqlDataSource>

                  <asp:SqlDataSource ID="flAg9" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='17'">
                </asp:SqlDataSource>

                  <asp:SqlDataSource ID="flAg10" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and xid='10067'">
                </asp:SqlDataSource>
                 <asp:SqlDataSource ID="flTm" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='tm' and  item_code not in ('T003','T008','T009','T021','T014') ">
                </asp:SqlDataSource>

                <asp:SqlDataSource ID="flPt2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:xpayConnectionString %>" 
                    SelectCommand="SELECT *,init_amt+tech_amt AS amt FROM [fee_list] WHERE xcategory='pt' and xid='34' ">
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
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">
                            
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
            <td align="center">&nbsp;</td>
        </tr>
       <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center">
                --- PATENT ---</td>
        </tr>
       <tr align="center" style="background-color:#efefef;">
            <td align="center">
                &nbsp;</td>
        </tr>
         <tr >
            <td align="center">
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

                          <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddPt" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="PtStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                         <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                 

            </td>
        </tr>
        <tr >
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">
                            
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
            <td align="center">&nbsp;</td>
        </tr>

         <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center">
                --- DESIGN ---</td>
        </tr>

         <tr align="center" style="background-color:#efefef;">
            <td align="center">
                &nbsp;</td>
        </tr>
         <tr >
            <td align="center">
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddDs" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="DsStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

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
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">
                            
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
            <td align="center">&nbsp;</td>
        </tr>
        </asp:Panel>
         <tr align="center" style="background-color:#666; color:#ffffff;">
            <td align="center">
                --- ACCREDITATION ---</td>
        </tr>

         <tr align="center" style="background-color:#efefef;">
            <td align="center">
                &nbsp;</td>
        </tr>
         <tr >
            <td align="center">
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>


                 <asp:GridView ID="gvAg2" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg2" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand2" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                   <asp:GridView ID="gvAg3" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg3" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand3" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                  <asp:GridView ID="gvAg4" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg4" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand3" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                <asp:GridView ID="gvAg5" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg5" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand5" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                   <asp:GridView ID="gvAg6" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg6" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand6" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                 <asp:GridView ID="gvAg7" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg7" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand7" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                  <asp:GridView ID="gvAg8" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg8" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand6" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>


                  <asp:GridView ID="gvAg9" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg9" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand9" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>


                  <asp:GridView ID="gvPt3"  runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flPt2" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand10" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
             
                 <asp:GridView ID="gvAg10" runat="server" Visible="false" AutoGenerateColumns="False" 
                    DataKeyNames="xid" DataSourceID="flAg10" EnableModelValidation="True" 
                    style="margin-top: 0px; width:100%;" onrowcommand="gvAg_RowCommand10" 
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

                       <asp:BoundField DataField="xdesc" HeaderText="ITEM DESCRIPTION" 
                            SortExpression="xdesc" >
                             <HeaderStyle HorizontalAlign="Left" Width="550px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="init_amt" HeaderText="APPLICATION FEE(NGN)" >
                             <HeaderStyle HorizontalAlign="Center"  Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         
                         <asp:BoundField DataField="tech_amt"  HeaderText="TECH FEE(NGN)"  >
                             <HeaderStyle HorizontalAlign="Center" Width="100px"/>
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                        <asp:BoundField DataField="amt" HeaderText="AMOUNT(NGN)" ReadOnly="True" 
                            SortExpression="amt" >
                             <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                        <ItemStyle HorizontalAlign="Right" />
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
                                 <asp:ImageButton ID="lbAddAg" ImageUrl="../images/add_btn.png" runat="server" Height="16px" CommandName="AgStatusClick"  CommandArgument='<%#Eval("item_code") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                        <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>
                        
                          <asp:BoundField DataField="init_amt" Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>  
                         
                         <asp:BoundField DataField="tech_amt"  Visible="false"  >
                             <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

                
            </td>
        </tr>
        <tr >
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">
                            
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
            <td align="center">
              <asp:Button ID="BtnDashboard1" runat="server" class="button" 
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
            <td align="left">
                <table id="mitems" class="tiger-stripe" style="width:100%; ">
                    <tr style="background-color:#1C5E55; color:#ffffff;">
                        <td>
                            S/N</td>
                        <td>
                            ITEM CODE</td>
                             <td>
                            ITEM DESCRIPTION</td>
                        <td>
                            QTY</td>
                        <td style="text-align:center;"> APPLICATION FEE(NGN)</td>
                              <td style="text-align:center;"> TECH. FEE(NGN)</td>
                        <td>
                            TOTAL (NGN)</td>
                    </tr>
                    <% int i = 1;
                       foreach (XPay.Classes.XObjs.Shopping_card f in lt_cart)
                       { %>
                    <tr style="border:1px solid #000;">
                        <td style="border:1px solid #000;">
                            <%=i%>
                        </td>
                        <td style="border:1px solid #000;">
                           <%=f.item_code%>
                        </td>
                         <td style="border:1px solid #000;">
                           <%=f.item_desc%>
                        </td>
                        <td style="border:1px solid #000;">
                           <%=f.qty%>
                        </td>
                         <td align="right" style="border:1px solid #000;">
                        <% string new_init_amt = string.Format("{0:n}", Convert.ToInt32(f.init_amt)); %>
                           <%=new_init_amt%>
                        </td>
                         <td align="right" style="border:1px solid #000;">
                        <% string new_tech_amt = string.Format("{0:n}", Convert.ToInt32(f.tech_amt)); %>
                           <%=new_tech_amt%>
                        </td>
                         <td align="right" style="border:1px solid #000;">
                        <% string new_tot_amt1 = string.Format("{0:n}", f.total_amt); %>
                           <%=new_tot_amt1%>
                        </td>
                       
                    </tr>
                     <% i++; tot_amtx += Convert.ToInt32(f.amt) * Convert.ToInt32(f.qty);
                       } %>
                   
                  
                  
                </table>
             </td>
        </tr>
          <tr>
            <td  class="right-align"> &nbsp;</td>
        </tr>
          <tr style="font-size:13px;text-decoration:underline; color:#1C5E55;">
            <% string new_tot_amtx = string.Format("{0:n}", tot_amtx); %>
            <td  class="right-align"> <strong>TOTAL AMOUNT:</strong>&nbsp;NGN&nbsp;<strong><%=new_tot_amtx%></strong></td>
        </tr>
          <tr >
            <td  align="center"> 
              <asp:Button ID="BtnDashboard" runat="server" class="button" 
                    Text="Back to Dashboard" onclick="BtnDashboard_Click" />

                <asp:Button ID="btnChangeItems" runat="server" class="button" 
                    Text="Change Items" onclick="btnChangeItems_Click"  />
               
                <asp:Button ID="btnGo" runat="server" class="button" Text="Check Out" 
                    onclick="btnGo_Click" /></td>
        </tr>
          <% } %>
        
          <tr >
            <td  align="center" style="background-color:#1C5E55; color:#ffffff;"> 
                </td>
        </tr>
          <tr >
            <td  align="center"> 
                POWERED BY<br/>
           <img src="../images/payxlogo.jpg"  alt="XPay" width="90px" height="40px" /> <br />
                Plot 4. Oluwakayode Jacobs Street Ikate,Lekki Phase 1<br />
                <a href="http://www.einaosolutions.com">www.einaosolutions.com</a><br />
                Support E-mail(s): <a href="mailto:paymentsupport@einaosolutions.com">paymentsupport@einaosolutions.com</a><br />
                Customer Contact Support Line(s): +2349038979681 </td>
        </tr>
                  
          </table>
                </div>
                </td>
        </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
