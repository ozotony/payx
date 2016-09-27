<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="m_struc.cs" Inherits="XPay.X.m_struc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ADMINISTRATION SECTION</title>
     <link href="../../css/style.css" rel="stylesheet" type="text/css" /> 
     <link rel="stylesheet" href="../../css/jquery.ui.all.css" />
     <link rel="stylesheet" href="../../css/jquery.ui.theme.css" />
     <link rel="stylesheet" href="../../css/jquery.ui.tabs.css" />
     <link href="../../css/jquery.ui.timepicker.css" rel="stylesheet" type="text/css" />

    <script src="../../js/jquery.js" type="text/javascript"></script>
    <script src="../../js/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../ui/jquery.ui.datepicker.js"></script>
    <script src="../../js/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../js/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../js/ui/jquery.ui.tabs.js" type="text/javascript"></script>
    <script src="../../js/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../../js/funk.js" type="text/javascript"></script>

 <% if (false)  
   { %>  
    <script src="~/js/jquery-1.4.1-vsdoc.js" type="text/javascript">  
    </script>  
<%} %> 

<script type="text/javascript">
    $(function () {
        $(".xdate").datepicker({ changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'yy-mm-dd'
        });
    });   
		
</script>


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
     
                 <table align="center" width="100%" class="form" >
                  <tr align="center">
                <td colspan="2">
                    &nbsp;<img alt="X Pay" src="../images/einao_logo.png" width="140px" height="78px" /></td>
            </tr>
         <tr>
            <td colspan="3" style="text-align:center;">
               <hr /> </td>
        </tr>
        
                
        <tr>
            <td colspan="3" style="text-align:center; background-color:#ccc;" >
               <a href="profile.aspx">PROFILE</a> | <a href="m_items.aspx">ITEMS</a> | <a href="m_m.aspx">MERCHANTS</a> | <a href="m_a.aspx">ADMINISTRATION</a> | <a href="m_struc.aspx">PAYMENT STRUCTURE</a> | <a href="charge_back.aspx">CLAIMS</a><br />
                <a href="../lo.ashx"><img alt="X Pay"  src="../images/logoff.png" width="30px" height="30px" /></a></td>
        </tr>  
                
        <tr>
            <td colspan="3" style="text-align:center;" >
                <hr /></td>
        </tr>
        <% if (Session["x_msg"] != null) {Response.Write(Session["x_msg"].ToString()); } %>
      
         <tr>
            <td style="text-align:left;" colspan="2">
            <asp:LinkButton ID="btnNewRecLink" runat="server" Text="New Record" onclick="btnNewRecLink_Click" ></asp:LinkButton>
        <asp:ImageButton ID="btnNewrecord" runat="server"  ImageUrl="../images/plus.gif" AlternateText="Add new record" onclick="btnNewrecord_Click">
        </asp:ImageButton></td>
        </tr>
          <tr>
            <td style="background-color:#1C5E55; color:#ffffff; text-align:left;" colspan="2">
           </td>
        </tr>
         <tr>
            <td  style="text-align:right;width:50%;" >
              Merchant:                
                </td>
            <td align="left">
               <asp:DropDownList ID="ddl_merchants" runat="server" >
                </asp:DropDownList>
                </td>
        </tr>
         <tr>
            <td align="center" colspan="2" >
            
&nbsp;
             &nbsp;<asp:Button ID="btnSearch" runat="server" class="button" Text="SEARCH" 
                    onclick="btnSearch_Click" />
                    </td>
        </tr>         
        
        </table>
        <% if (x_cnt >0)
           {
               if (show_inv >0)
               { %>   
               <table   style="width:100%;">
      <tr class="center-align" style="background-color:#1C5E55; color:#ffffff;">
            <td class="center-align" colspan="8">
                --- <%=x_cnt %> RECORDS FOUND FOR CLIENT <strong>"<%=ddl_merchants.SelectedItem.Text.ToUpper() %>"</strong> ---</td>
        </tr>
        <tr class="center-align" style="background-color:#efefef;">
            <td class="center-align" colspan="8">
                &nbsp;</td>
        </tr>
        <tr id="ReportGrid" style="text-align:center;">
            <td colspan="8">
                <asp:GridView ID="gvX" runat="server" AutoGenerateColumns="False"  EnableModelValidation="True" 
                style="margin-top: 0px; width:100%;" onrowcommand="gvX_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" CaptionAlign="Left"  HorizontalAlign="Left"
                    AllowPaging="True"  onpageindexchanging="gvX_PageIndexChanging" PageSize="50" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>   
                       <asp:TemplateField HeaderText="S/N">
                            <ItemTemplate>
                                 <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                         <asp:BoundField DataField="xpartnerID" HeaderText="PARTNER NAME" ReadOnly="True">
                             <HeaderStyle HorizontalAlign="Left" Width="150px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:BoundField DataField="xvisible" HeaderText="PARTNER COMPANY" ReadOnly="True">
                             <HeaderStyle HorizontalAlign="Left" Width="200px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                          <asp:BoundField DataField="p_type" HeaderText="RELATIONSHIP" ReadOnly="True">
                             <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                           <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="xratio" HeaderText="RATIO VALUE (%)"  >
                             <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         
                             <asp:BoundField DataField="r_type" HeaderText="RATIO TYPE" >
                            <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                         <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                                                

                          <asp:TemplateField HeaderText="EDIT" >
                             <ItemTemplate>
                              <asp:ImageButton ID="btnXEdit" ImageUrl="../images/edit.gif" runat="server" Height="16px" CommandName="TmEditClick"  CommandArgument='<%#Eval("xid") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
                             <ItemStyle HorizontalAlign="Left" />
                             </asp:TemplateField>

                         <asp:TemplateField HeaderText="DELETE" >
                             <ItemTemplate>
                              <asp:ImageButton ID="btnXDelete" ImageUrl="../images/x.gif" runat="server" Height="16px" CommandName="TmDeleteClick"  CommandArgument='<%#Eval("xid") %>'  />
                             </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" Width="30px"/>
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
            <asp:Button ID="btnExportExcel" runat="server" class="button" onclick="btnExportExcel_Click" Text="Export Excel" />
        </td>
        </tr>
</table>
          <% }
               else
               {%> 
            <table align="center" width="100%" class="gridtable form">
       
        <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">&nbsp;</td>
        </tr>
        <tr>
        <td align="center" style="background-color:#000000; color:#ffffff;  font-weight:bold;">
               <strong> NO RECORDS FOUND FOR THE SEARCH QUERY!!</strong></td>
        </tr>
        <tr>
        <td align="center" style="background-color:#1C5E55; color:#ffffff;">&nbsp;</td>
        </tr>
        
        </table>   
 <% }
           }%>

        <% if (show_add > 0)
             { %>              
<table id="Additem" class="gridtable form" style="text-align:center; width:100%;">
        <tr  style="background-color:#1C5E55; color:#ffffff;text-align:center;">
            <td >
              ADD PARTNER STRUCTURE
                </td>
        </tr>
        <tr >
            <td  style="text-align:left;">               
            <strong>PARTNER</strong><br />
              <asp:DropDownList ID="a_xpartner" runat="server" CssClass="textbox">
                </asp:DropDownList><hr />

            <strong>RELATIONSHIP</strong> <br />
             <asp:DropDownList ID="a_p_type" runat="server" CssClass="textbox">
                <asp:ListItem Value="merchant" Text="Merchant"></asp:ListItem>
                <asp:ListItem Value="wingman" Text="Wingman"></asp:ListItem>
                </asp:DropDownList><hr />

                 <strong>RATIO TYPE</strong> <br />
               <asp:DropDownList ID="a_r_type" runat="server" CssClass="textbox">
                <asp:ListItem Value="fee_list" Text="Fee List"></asp:ListItem>
                <asp:ListItem Value="perc" Text="Percentage"></asp:ListItem>
                </asp:DropDownList><hr />

            
            <strong>RATIO</strong> <strong>(%)</strong><br />
            <asp:TextBox ID="a_xratio" runat="server" CssClass="textbox"  Width="100px"></asp:TextBox><hr />

           </td>
        </tr>

         <tr>
			<td>
             <asp:Button ID="btnAddItem" runat="server" class="button" Text="Submit" onclick="btnAddItem_Click"/>
             &nbsp;
            <asp:Button ID="btnBack" runat="server" class="button" Text="Back" onclick="btnBack_Click"   />
			</td>
            </tr>
        
          </table>
 <% } %> 
  <% if (show_edit > 0)
             { %> 
             <table id="EditItems" class="gridtable form" style="text-align:center; width:100%;">
        <tr  style="background-color:#1C5E55; color:#ffffff;text-align:center;">
            <td >
              EDIT PARTNER STRUCTURE
                </td>
        </tr>
        <tr >
            <td  style="text-align:left;">  
                <asp:HiddenField ID="e_xid" runat="server" />
            <strong>PARTNER</strong><br />
             <asp:DropDownList ID="e_xpartner" runat="server" CssClass="textbox">
                </asp:DropDownList><hr />

            <strong>RELATIONSHIP</strong> <br />
              <asp:DropDownList ID="e_p_type" runat="server" CssClass="textbox">
                <asp:ListItem Value="merchant" Text="Merchant"></asp:ListItem>
                <asp:ListItem Value="wingman" Text="Wingman"></asp:ListItem>
                </asp:DropDownList><hr />

              <strong>RATIO TYPE</strong> <br />
             <asp:DropDownList ID="e_r_type" runat="server" CssClass="textbox">
                <asp:ListItem Value="fee_list" Text="Fee List"></asp:ListItem>
                <asp:ListItem Value="perc" Text="Percentage"></asp:ListItem>
                </asp:DropDownList><hr />

            <strong>RATIO</strong> <strong>(%)</strong><br />
            <asp:TextBox ID="e_xratio" runat="server" CssClass="textbox"  Width="100px"></asp:TextBox><hr />

          

            </td>
        </tr>

         <tr>
			<td>
             <asp:Button ID="btnEditItem" runat="server" class="button" Text="Submit" onclick="btnEditItem_Click"/>
             &nbsp;
            <asp:Button ID="btnEditBack" runat="server" class="button" Text="Back" onclick="btnEditBack_Click"  />
			</td>
            </tr>
        
          </table>   
         
 <% } %> 
    </div>
    </form>
</body>
</html>
