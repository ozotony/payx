<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pay_his3.aspx.cs" Inherits="PayX.X.pay_his3" %>

<!DOCTYPE html>

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
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/sweet-alert.min.js"></script>
    <link href="../css/sweet-alert.css" rel="stylesheet" />
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
<body ng-controller="myController2" >
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
              </td>
        </tr>  
        <tr>
            <td align="right" style="width:25%;font-size:11px;" colspan="2">
              </td>
            <td align="left" style="width:25%; font-weight:bold; color:Green;font-size:11px;" colspan="2">
               </td>
            <td align="right" style="width:25%;font-size:11px;" colspan="2">
            
            <td align="left" style="width:25%;font-weight:bold; color:Green;font-size:11px;" colspan="2">
                &nbsp;
              </td>

           

            
        </tr>
         <tr>
            <td align="center" style="background-color:#1C5E55; color:#ffffff;" colspan="8">&nbsp;</td>
        </tr>
         <tr style="font-size:11px;">
            <td align="center" >
            
                </td>
            <td align="center" colspan="2" >
              
                </td>
            <td align="center" colspan="2" >
                Mode<br />
                <select ng-model="Payment">
<option ng-repeat="x in PaymentType" value="{{x}}">{{x}}</option>
</select>
                
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
             &nbsp;
                 <button type="button" class="button" ng-click="Export2()" >SEARCH</button> 

                <button type="button" ng-show="displayedCollection" class="button" ng-click="Export()" >EXPORT EXCEL </button> 
               
                    </td>
        </tr>
         <tr>
            <td align="center" colspan="8" >
            
               <hr /></td>
        </tr>
         <tr>
            <td align="center" colspan="8" style="font-size:11px;" >
            
                </td>
        </tr>
         <tr>
            <td align="center" colspan="8" >
            
            
                    </td>
        </tr>
         <tr>
            <td align="center" colspan="8" >
               
                </td>
        </tr>
        
        </table>


          <table st-table="displayedCollection" st-safe-src="ListAgent" class="table table-responsive table-bordered table3">
        <thead>
            <tr>
                 <th  st-sort="sn" class="tbbg2">Sn</th>
                <th  st-sort="code" class="tbbg2">Code</th>
                <th  st-sort="xdesc" class="tbbg2">Description</th>
                 <th  st-sort="transID" class="tbbg2"> TransId</th>
                 <th  st-sort="init_amt" class="tbbg2">Init Amount</th>
                 <th  st-sort="tech_amt" class="tbbg2">Tech Amount</th>
                 <th  st-sort="convenient_fee" class="tbbg2">Convenient Fee</th>
                   <th  st-sort="TransactionDate" class="tbbg2">Transaction Date</th>
                  

            </tr>
            <tr>
                <th colspan="8"><input st-search="" class="form-control" placeholder="global search ..." type="text" /></th>
            </tr>
        </thead>
        <tbody>
            <tr ng-repeat="row in displayedCollection" >
               
               <td  align="center">{{row.sn}}</td>
                <td  align="center">{{row.code}}</td>
                <td  align="center">{{row.xdesc}}</td>
                 <td  align="center">{{row.transID}}</td>
                <td  align="center">{{row.init_amt|currency:""}}</td>
                <td  align="center">{{row.tech_amt|currency:""}}</td>
                 <td  align="center">{{row.convenient_fee|currency:""}}</td>
                 <td align="center">{{row.TransactionDate}}</td>
                 
                

              
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="8" class="text-center">
                    <div st-pagination="" st-items-by-page="itemsByPage" st-displayed-pages="7"></div>
                </td>
            </tr>
        </tfoot>
    </table>
      
     

           
    </div>
    </form>
   
</body>
</html>
