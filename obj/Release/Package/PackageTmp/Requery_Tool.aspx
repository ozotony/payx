<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Requery_Tool.aspx.cs" Inherits="PayX.Requery_Tool" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" data-ng-app="myModule">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.7.2.min.js"></script>
    <script src="js/angular.min.js"></script>
    <script src="js/AngularLogin.js"></script>

    <script src="js/sweet-alert.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/sweet-alert.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/angular-datepicker.min.css" rel="stylesheet" />
    <script src="js/angular-datepicker.min.js"></script>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <style type="text/css">

        .form table a {
color:navy;
font-size: 14px;
font-weight: bold;
}

    </style>
</head>
<body ng-controller="myController">
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
            <td style="text-align:center; background-color:#ccc;" >
              <%-- <a href="profile.aspx">PROFILE</a> | <a href="m_items.aspx">ITEMS</a> | <a href="m_m.aspx">MERCHANTS</a> | <a href="m_a.aspx">ADMINISTRATION</a> | <a href="m_struc.aspx">PAYMENT STRUCTURE</a> | <a href="charge_back.aspx">CLAIMS</a><br />--%>
                 <a href="../lo.ashx"><img alt="X Pay"  src="../images/logoff.png" width="30px" height="30px" /></a></td>
        </tr>
         <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;">&nbsp;</td>
        </tr>

                     <tr>
                         <td>

                             <table> 
                                 <tr> 
                                 <td>
                                     Start Date

                                 </td>

                                     <td>
                                   <datepicker button-next="&lt;i class='fa fa-arrow-right'&gt;&lt;/i&gt;" button-prev="&lt;i class='fa fa-arrow-left'&gt;&lt;/i&gt;" date-format="yyyy-M-d">
                                <input name="application_date" id="txt_application_date"  class="form-control"  ng-model="application_date"    type="text" />
                            </datepicker>

                                     </td>

                                 </tr>
                                 <tr>

                                 <td>

                                     End Date 
                                 </td>

                                      <td>

                                     <datepicker button-next="&lt;i class='fa fa-arrow-right'&gt;&lt;/i&gt;" button-prev="&lt;i class='fa fa-arrow-left'&gt;&lt;/i&gt;" date-format="yyyy-M-d">
                                <input name="application_date2" id="txt_application_date2"  class="form-control"  ng-model="application_date2"    type="text" />
                            </datepicker>
                                 </td>

                                 </tr>
                             </table>
                                <input id="Button1" type="button" ng-click="search2()" value="Search" />

              
                         </td>


                     </tr>
         <tr>
            <td align="center" style="font-size:11px;" >
           

               

                Enter Transaction ID<br />
               <input id="Text1" ng-model="trans" type="text" />
                </td>
        </tr>
         <tr>
            <td align="center" >
            
               <input id="Button1" type="button" ng-click="search()" value="Search" />
                    </td>
        </tr>
         <tr>
            <td align="center" >
                
                </td>
        </tr>

                     <tr> 

                         <td>
                         <table  class="table" >
                                        <thead>
                                            <tr>
                                                <th>
                                                    Transaction Reference

                                                </th>
                                                <th>Customer Id</th>

                                                <th>Amount</th>
                                                <th>Merchant Reference</th>
                                                <th>Transaction Status</th>
                                                <th>Payment Reference</th>
                                                
                                                <th></th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-repeat="user in BranchCollect" >
                                                <td>{{user.txn_ref}}</td>
                                                <td>{{user.cust_id}}</td>
                                                <td>{{user.amount|currency:"":0}}</td>
                                                <td>{{user.MerchantReference}}</td>

                                                <td>{{user.trans_status}}</td>

                                                <td>{{user.pay_ref }}</td>

                                                <td>
                                                    <a href="#" ng-click="EditRow(user);$event.preventDefault(); $event.stopPropagation();">Requery</a>
                                                </td>

                                            </tr>

                                        </tbody>
                                        <tfoot>
                                        
                                    </table>

                         </td>

                     </tr>
        
        </table>
       
       
 
                
           
    </div>

    <div class="col-lg-4">
   <%-- <table class="table table-condensed ">
<tr>
    <td> Transaction id </td>
   
    <td>  <input id="Text1" ng-model="trans" type="text" /> </td>
    <td> <input id="Button1" type="button" ng-click="search()" value="button" /> </td>

</tr>
        

    </table>--%>


        
    </div>
    </form>
</body>
</html>
