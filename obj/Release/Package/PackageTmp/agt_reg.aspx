<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agt_reg.cs" Inherits="XPay.agt_reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CLIENT REGISTRATION</title>
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
                AGENT REGISTRATION : PLEASE FILL IN ALL THE DETAILS BELOW  
            </td>
        </tr>
        
        <tr>
            <td width="30%">
                &nbsp;&nbsp;NAME:
            </td>
            <td>
                <asp:TextBox ID="xname" runat="server" Width="400px" CssClass="textbox" ></asp:TextBox> 
                                   
                </td>
        </tr>
        
        <tr>
            <td width="30%">
                &nbsp;COMPANY NAME:
                &nbsp;</td>
            <td>
                <asp:TextBox ID="cname" runat="server" Width="400px" CssClass="textbox" ></asp:TextBox> 
                                   
                </td>
        </tr>
        
        <tr>
            <td class="left-align">
                &nbsp;&nbsp;NATIONALITY :
            </td>
            <td class="left-align">
                <asp:DropDownList ID="nationality" runat="server" CssClass="textbox" DataSourceID="ds_Nationality" DataTextField="name" 
                    DataValueField="ID" >
                </asp:DropDownList>
                <asp:SqlDataSource ID="ds_Nationality" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:cldConnectionString %>" 
                    SelectCommand="SELECT * FROM [country]"></asp:SqlDataSource>
           </td>
        </tr>        
        <tr>
            <td  colspan="2" align="center" style="background-color:#1C5E55; color:#ffffff;">               
                ADDRESS INFORMATION DETAILS</td>
        </tr>
        <tr>
            <td width="22%">
                &nbsp;COUNTRY :
            </td>
            <td>
                <asp:DropDownList ID="residence" runat="server" CssClass="textbox" 
                    DataSourceID="ds_DefaultCountry" DataTextField="name" 
                    DataValueField="ID" AutoPostBack="true" 
                    onselectedindexchanged="residence_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:SqlDataSource ID="ds_DefaultCountry" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:cldConnectionString %>" 
                    SelectCommand="SELECT * FROM [country]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="1" Name="ID" Type="Int64" />
                    </SelectParameters>
                </asp:SqlDataSource>
                </td>
        </tr>
         <% if (state_row == "0")
                    { %>
        <tr>
            <td width="22%">
                &nbsp;STATE:             </td>
            <td>
                 
                <asp:DropDownList ID="xselectState" runat="server" CssClass="textbox" 
                    DataSourceID="ds_State" DataTextField="name" DataValueField="ID" >
                </asp:DropDownList>
                <asp:SqlDataSource ID="ds_State" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:cldConnectionString %>" 
                    SelectCommand="SELECT DISTINCT * FROM [state] WHERE ([countryID] = @countryID)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="residence" DefaultValue="" Name="countryID" 
                            PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                 
            </td>
        </tr>
        <% } %>
        <tr>
            <td width="22%">
                &nbsp;CITY :
            </td>
            <td>
            <asp:TextBox ID="xcity" runat="server" Width="200px" CssClass="textbox"></asp:TextBox>
               </td>
        </tr>
        <tr>
            <td width="22%">
                &nbsp;STREET ADDRESS:
            </td>
            <td>
                <asp:TextBox ID="xaddress" runat="server" Width="400px" CssClass="textbox" 
                    Height="50px" TextMode="MultiLine"></asp:TextBox>
               
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
            <td colspan="2" align="center" style="background-color:#1C5E55; color:#ffffff;">
            Please note that the letters below are not case sensitive!!!
            </td>
        </tr>
        <tr>
            <td class="right-align">
                &nbsp;<img alt="captcha" src="./xcaptcha.ashx" />&nbsp;</td>
            <td>
            <asp:TextBox ID="xcode" runat="server" Width="90px" CssClass="textbox"></asp:TextBox>
                    
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
        <tr>
            <td colspan="2" align="center">
            
                <asp:Button ID="btnAddMember" runat="server" class="button" Text="I confirm that the above entries are correct" 
                    onclick="btnAddMember_Click" />
            </td>
        </tr>
          <tr>
            <td align="center" colspan="2">
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
