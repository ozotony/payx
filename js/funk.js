///<reference path="jquery-1.4.1-vsdoc.js"/>

////////////////////////////-----------HELPERS---------------------------------///////////////////////////
function postwith (to,p) {
  var myForm = document.createElement("form");
  myForm.method="post" ;
  myForm.action = to ;
  for (var k in p) {
    var myInput = document.createElement("input") ;
    myInput.setAttribute("name", k) ;
    myInput.setAttribute("value", p[k]);
    myForm.appendChild(myInput) ;
  }
  document.body.appendChild(myForm) ;
  myForm.submit() ;
  document.body.removeChild(myForm) ;
}

function isw_merc_postwith(to, p, txn_ref, einao_split_amt, cust_name, split_amt, split_bank, split_acc) {
    var item_details = "";
    var myForm = document.createElement("form");
    myForm.method = "post";
    myForm.action = to;
    for (var k in p) {
        var myInput = document.createElement("input");
        myInput.setAttribute("name", k);
        myInput.setAttribute("value", p[k]);
        myForm.appendChild(myInput);
    }
    item_details = "<payment_item_detail>";
    item_details +="<item_details detail_ref='"+txn_ref+"' institution='Einao Solutions' sub_location='Abuja' location='Lagos'>";
    item_details += "<item_detail item_id='1' item_name='Einao Solutions' item_amt='" + einao_split_amt + "' bank_id='120' acct_num='1771364037' />";
    item_details += "<item_detail item_id='2' item_name='" + cust_name + "' item_amt='" + split_amt + "' bank_id='" + split_bank + "' acct_num='" + split_acc + "' />";
    item_details +="</item_details>";
    item_details += "</payment_item_detail>";
    var itemInput = document.createElement("input");
    itemInput.setAttribute("name", "xml_data");
    itemInput.setAttribute("value", item_details);
    myForm.appendChild(itemInput);
    document.body.appendChild(myForm);
   myForm.submit();
   document.body.removeChild(myForm);
}

function validate_number(inp)
{
	var pattern = /^\d{1,20}$/;	
	if (inp.search(pattern) > -1) {	return true;	} 
	else {	return false;	}
}

function validate_email (inputstr) {
var pattern =/^[a-zA-Z0-9_\-.]+@([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,3}$/;
if (inputstr.search(pattern) > -1) {
return true;
} else {
return false;
}
}
//function validate_email(inputstr) {
//    var pattern = /^[a-zA-Z0-9_\-.]+@([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,3}/;
//    if (inputstr.search(pattern) > -1) {
//        return true;
//    }
//    else {
//        return false;
//    }
//}

function validate_mobile(inp)
{
var pattern = /^\d{11}$/;	
if (inp.search(pattern) > -1) {
return true;
} else {
return false;
}
}

//function validate_mobile(inp) {
//    var pattern = /^\d{11}/;
//    if (inp.search(pattern) > -1) {
//        return true;
//    }
//    else {
//        return false;
//    }
//}


function doPrev(page)
{
window.location.href=page;	
}

function doView(page)
{
window.open(page);	
}

function doSplash(page)
{
	if(page=="1")
	{
window.location.href='profile.php';	
	}
	else if(page=="2")
	{
window.location.href='admin_profile.php';	
	}
	else if(page=="3")
	{
window.location.href='agent_profile.php';	
	}
	else if(page=="4")
	{
window.location.href='exec_profile.php';	
	}
	else if(page=="5")
	{
window.location.href='tenant_profile.php';	
	}
	else if(page=="6")
	{
window.location.href='landlord_profile.php';	
	}
}

function doHistory(d)
{
history.go(d);	
}

function printSelection(node)
{
  var content = document.getElementById(node).innerHTML
  var pwin=window.open('','print_content');
  pwin.document.open();
  pwin.document.write('<html><body onload="window.print()" >'+content+'</body></html>');
  pwin.document.close(); 
 //setTimeout(function(){pwin.close();},1000);
}

function printAssessment(node) {
   
  var content=document.getElementById(node).innerHTML
  var pwin=window.open('','print_content');
  pwin.document.open();
  pwin.document.write('<html><link href="../css/style.css" rel="stylesheet" type="text/css" /><body onload="window.print()" ><style>.tiger-stripe{ font-size:14px;text-align:left;font-weight:normal;} .tiger-stripe tr:nth-child(odd) {background: #E3EAEB;color:#000000;font-size:14px;text-align:left;font-weight:normal;} .item_alt {background-color:#E3EAEB; color:#000000;font-size:14px;text-align:left;font-weight:normal;   }</style>' + content + '</body></html>');
  pwin.document.close();
}

function printA(node) {   
  var content=document.getElementById(node).innerHTML
  var pwin=window.open('','print_content');
  pwin.document.open();
  pwin.document.write('<html><link href="../css/style.css" rel="stylesheet" type="text/css" /><body onload="window.print()" ><style>.tiger-stripe{ font-size:14px;font-weight:normal;} .tiger-stripe tr:nth-child(odd) {background: #E3EAEB;color:#000000;font-size:11px;font-weight:normal;} .item_alt {background-color:#E3EAEB; color:#000000;font-size:11px;font-weight:normal;   }</style>' + content + '</body></html>');
  pwin.document.close();
}

function printXreturnAssessment(node) {

    var content = document.getElementById(node).innerHTML
    var pwin = window.open('', 'print_content');
    pwin.document.open();
    pwin.document.write('<html><link href="../../../css/xrep_style.css" rel="stylesheet" type="text/css" /><body onload="window.print()" ><style>.tiger-stripe{ font-size:14px;text-align:left;font-weight:normal;} .tiger-stripe tr:nth-child(odd) {background: #E3EAEB;font-size:14px;text-align:left;font-weight:normal;} .item_alt {background-color:#E3EAEB; color:#000000;font-size:14px;text-align:left;font-weight:normal;   }</style>' + content + '</body></html>');
    pwin
}

function PrintPartner(node) {

    var content = document.getElementById(node).innerHTML
    var pwin = window.open('', 'print_content');
    pwin.document.open();
    pwin.document.write('<html><link href="../css/style.css" rel="stylesheet" type="text/css" /><body onload="window.print()" ><style>.tiger-stripe{ font-size:14px;text-align:center;font-weight:bold;} .tiger-stripe tr:nth-child(odd) {background: #E3EAEB;color:#000000;font-size:14px;text-align:center;font-weight:bold;} .item_alt {background-color:#E3EAEB; color:#000000;font-size:14px;text-align:center;font-weight:bold;   } .tbbg { padding:0; margin:0 auto; width:1200px; height:20px; background:url(../images/green_header.gif) top repeat-x; background-color:green; text-align:center; color:#fff; font-weight:bold; border-color:green;}</style>' + content + '</body></html>');
    pwin.document.close();
}

function printArk(node) {

    var content = document.getElementById(node).innerHTML
    var pwin = window.open('', 'print_content');
    pwin.document.open();
    pwin.document.write('<html><link href="css/style.css" rel="stylesheet" type="text/css" /><body onload="window.print()" >' + content + '</body></html>');
    pwin.document.close();
}
function printTm(node) {

    var content = document.getElementById(node).innerHTML
    var pwin = window.open('', 'print_content');
    pwin.document.open();
    pwin.document.write('<html><link href="../../css/print_style.css" rel="stylesheet" type="text/css" /><body onload="window.print()" >' + content + '</body></html>');
    pwin.document.close();
}

function logout(pwalletID)
{	
 window.location.href='signact.php?id='+pwalletID+'&x=0';	
}	

function doLogin()
{
	 if((document.getElementById("uname").value != "")  && (document.getElementById("pword").value != "") )
	 {
	postwith ('log_act.php',{'uname':document.getElementById("uname").value,'pword':document.getElementById("pword").value});
	 }
	  else
		 {
		alert("Please enter a vaild username and password!!");	 
		 }	 
}
/////////////////////////////////---END HELPERS------////////////////////////////////////////////////////


function confirmFilingDetails()
{
var cnt=0;
	document.getElementById("file_number").style.backgroundColor='#fff';
	document.getElementById("renewed_reg_number").style.backgroundColor='#fff';
	document.getElementById("law_code").style.backgroundColor='#fff';
	document.getElementById("file_number_x").style.display='none';	
	document.getElementById("renewed_reg_number_x").style.display='none';
	document.getElementById("law_code_x").style.display='none';
	  if(document.getElementById("code") != null)
	 {
	document.getElementById("code").style.backgroundColor='#fff';
	document.getElementById("code_x").style.display='none';
	 }
	 
	 if(document.getElementById("file_number").value == "")
	 {
	document.getElementById("file_number").style.backgroundColor='#4C787E';
	document.getElementById("file_number_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("renewed_reg_number").value == "")
	 {
	document.getElementById("renewed_reg_number").style.backgroundColor='#4C787E';
	document.getElementById("renewed_reg_number_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("law_code").value == "")
	 {
	document.getElementById("law_code").style.backgroundColor='#4C787E';
	document.getElementById("law_code_x").style.display='inline';
	cnt=cnt+1;
	 }		
	 
	 if( document.getElementById("code") != null)
	 { 
		  if(document.getElementById("code").value == "")
		 {
			document.getElementById("code").style.backgroundColor='#4C787E';			
			document.getElementById("code_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
	 
	  if(cnt!=0)
	 {
	 alert("Please fill in the marked fileds");
	 }
	 
	  else  if(	(document.getElementById("file_number").value != "")&& (document.getElementById("renewed_reg_number").value != "")&&
	  (document.getElementById("law_code").value != "") )
	  {
	 document.getElementById("file_number").style.backgroundColor='#fff';
	 document.getElementById("renewed_reg_number").style.backgroundColor='#fff';
	 document.getElementById("law_code").style.backgroundColor='#fff';
	  if(document.getElementById("code") != null)
	 {	document.getElementById("code").style.backgroundColor='#fff';	 }
	 
	document.getElementById("file_number_g").style.display='inline';
	document.getElementById("renewed_reg_number_g").style.display='inline';
	document.getElementById("law_code_g").style.display='inline';
	  if(document.getElementById("code_g") != null)
	 {
	document.getElementById("code_g").style.display='inline';
	 }
	document.getElementById("regConfirm").style.display="none";
	document.getElementById("Save").style.display="inline";
	 }	
            
}

function confirmApplicationDetails()
{
var cnt=0;
	document.getElementById("name").style.backgroundColor='#fff';
	document.getElementById("type").style.backgroundColor='#fff';
	document.getElementById("tax_id_type").style.backgroundColor='#fff';
	document.getElementById("tax_id_number").style.backgroundColor='#fff';	
	document.getElementById("individual_id_type").style.backgroundColor='#fff';
	document.getElementById("individual_id_number").style.backgroundColor='#fff';
	document.getElementById("city").style.backgroundColor='#fff';
	document.getElementById("street").style.backgroundColor='#fff';	
	document.getElementById("zip").style.backgroundColor='#fff';
	document.getElementById("telephone1").style.backgroundColor='#fff';
	document.getElementById("email").style.backgroundColor='#fff';
	document.getElementById("shared_ownership_notes").style.backgroundColor='#fff';
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	document.getElementById("name_x").style.display='none';
	document.getElementById("type_x").style.display='none';
	document.getElementById("tax_id_type_x").style.display='none';
	document.getElementById("tax_id_number_x").style.display='none';	
	document.getElementById("individual_id_type_x").style.display='none';
	document.getElementById("individual_id_number_x").style.display='none';
	document.getElementById("city_x").style.display='none';
	document.getElementById("street_x").style.display='none';	
	document.getElementById("zip_x").style.display='none';
	document.getElementById("telephone1_x").style.display='none';
	document.getElementById("email_x").style.display='none';
	document.getElementById("shared_ownership_notes_x").style.display='none';
	
	if(document.getElementById("state") != null)
	 {
	document.getElementById("state").style.backgroundColor='#fff';
	document.getElementById("state_x").style.display='none';
	 }
	 if(document.getElementById("lga") != null)
	 {
	document.getElementById("lga").style.backgroundColor='#fff';
	document.getElementById("lga_x").style.display='none';
	 }
	 
	  if(document.getElementById("code") != null)
	 {
	document.getElementById("code").style.backgroundColor='#fff';
	document.getElementById("code_x").style.display='none';
	 }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	 
	 if(document.getElementById("name").value == "")
	 {
	document.getElementById("name").style.backgroundColor='#4C787E';
	document.getElementById("name_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("type").value == "")
	 {
	document.getElementById("type").style.backgroundColor='#4C787E';
	document.getElementById("type_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("tax_id_type").value == "")
	 {
	document.getElementById("tax_id_type").style.backgroundColor='#4C787E';
	document.getElementById("tax_id_type_x").style.display='inline';
	cnt=cnt+1;
	 }		
	 
	 if(document.getElementById("tax_id_number").value == "")
	 {
	document.getElementById("tax_id_number").style.backgroundColor='#4C787E';
	document.getElementById("tax_id_number_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("individual_id_type").value == "")
	 {
	document.getElementById("individual_id_type").style.backgroundColor='#4C787E';
	document.getElementById("individual_id_type_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("individual_id_number").value == "")
	 {
	document.getElementById("individual_id_number").style.backgroundColor='#4C787E';
	document.getElementById("individual_id_number_x").style.display='inline';
	cnt=cnt+1;
	 }		
	 if(document.getElementById("city").value == "")
	 {
	document.getElementById("city").style.backgroundColor='#4C787E';
	document.getElementById("city_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("type").value == "")
	 {
	document.getElementById("type").style.backgroundColor='#4C787E';
	document.getElementById("type_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("tax_id_type").value == "")
	 {
	document.getElementById("tax_id_type").style.backgroundColor='#4C787E';
	document.getElementById("tax_id_type_x").style.display='inline';
	cnt=cnt+1;
	 }		
	 if(document.getElementById("street").value == "")
	 {
	document.getElementById("street").style.backgroundColor='#4C787E';
	document.getElementById("street_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("zip").value == "")
	 {
	document.getElementById("zip").style.backgroundColor='#4C787E';
	document.getElementById("zip_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if((document.getElementById("telephone1").value == "")||(validate_mobile(document.getElementById("telephone1").value)==false))
	 {
	document.getElementById("telephone1").style.backgroundColor='#4C787E';
	document.getElementById("telephone1_x").style.display='inline';
	cnt=cnt+1;
	 }		
	  
	  if((document.getElementById("email").value == "")||(validate_email (document.getElementById("email").value) == false))
	 {
	document.getElementById("email").style.backgroundColor='#4C787E';
	document.getElementById("email_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("shared_ownership_notes").value == "")
	 {
	document.getElementById("shared_ownership_notes").style.backgroundColor='#4C787E';
	document.getElementById("shared_ownership_notes_x").style.display='inline';
	cnt=cnt+1;
	 }			 
	 		
	 if( document.getElementById("state") != null)
	 { 
		  if(document.getElementById("state").value == "")
		 {
			document.getElementById("state").style.backgroundColor='#4C787E';			
			document.getElementById("state_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
	  if( document.getElementById("lga") != null)
	 { 
		  if(document.getElementById("lga").value == "")
		 {
			document.getElementById("lga").style.backgroundColor='#4C787E';			
			document.getElementById("lga_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
	  if( document.getElementById("code") != null)
	 { 
		  if(document.getElementById("code").value == "")
		 {
			document.getElementById("code").style.backgroundColor='#4C787E';			
			document.getElementById("code_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	  if(cnt!=0)
	 {
	 alert("Please fill in the marked fileds");
	 }
	 
	  else 
	   if(cnt==0)
	  {
	document.getElementById("name").style.backgroundColor='#fff';
	document.getElementById("type").style.backgroundColor='#fff';
	document.getElementById("tax_id_type").style.backgroundColor='#fff';
	document.getElementById("tax_id_number").style.backgroundColor='#fff';	
	document.getElementById("individual_id_type").style.backgroundColor='#fff';
	document.getElementById("individual_id_number").style.backgroundColor='#fff';
	document.getElementById("city").style.backgroundColor='#fff';
	document.getElementById("street").style.backgroundColor='#fff';	
	document.getElementById("zip").style.backgroundColor='#fff';
	document.getElementById("telephone1").style.backgroundColor='#fff';
	document.getElementById("email").style.backgroundColor='#fff';
	document.getElementById("shared_ownership_notes").style.backgroundColor='#fff';
	
	   if(document.getElementById("state") != null) {	document.getElementById("state").style.backgroundColor='#fff';	 }
	 
	   if(document.getElementById("lga") != null)  {	document.getElementById("lga").style.backgroundColor='#fff';	 }
	 
	   if(document.getElementById("code") != null) {	document.getElementById("code").style.backgroundColor='#fff';	 }
	   
	document.getElementById("name_g").style.display='inline';
	document.getElementById("type_g").style.display='inline';
	document.getElementById("tax_id_type_g").style.display='inline';
	document.getElementById("tax_id_number_g").style.display='inline';	
	document.getElementById("individual_id_type_g").style.display='inline';
	document.getElementById("individual_id_number_g").style.display='inline';
	document.getElementById("city_g").style.display='inline';
	document.getElementById("street_g").style.display='inline';	
	document.getElementById("zip_g").style.display='inline';
	document.getElementById("telephone1_g").style.display='inline';
	document.getElementById("email_g").style.display='inline';
	document.getElementById("shared_ownership_notes_g").style.display='inline';
	
	  if(document.getElementById("state_g") != null)	 {	document.getElementById("state_g").style.display='inline';	 }
	  if(document.getElementById("lga_g") != null)	 {	document.getElementById("lga_g").style.display='inline';	 }
	  if(document.getElementById("code_g") != null)	 {	document.getElementById("code_g").style.display='inline';	 }
	  
	document.getElementById("regConfirm").style.display="none";
	document.getElementById("Save").style.display="inline";
	 }	
            
}

function confirmRepresentativeDetails()
{
var cnt=0;
	document.getElementById("name").style.backgroundColor='#fff';
	document.getElementById("type").style.backgroundColor='#fff';
	document.getElementById("auth_type").style.backgroundColor='#fff';
	document.getElementById("agent_code").style.backgroundColor='#fff';	
	document.getElementById("individual_id_type").style.backgroundColor='#fff';
	document.getElementById("individual_id_number").style.backgroundColor='#fff';
	document.getElementById("city").style.backgroundColor='#fff';
	document.getElementById("street").style.backgroundColor='#fff';	
	document.getElementById("zip").style.backgroundColor='#fff';
	document.getElementById("telephone1").style.backgroundColor='#fff';
	document.getElementById("email").style.backgroundColor='#fff';
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	document.getElementById("name_x").style.display='none';
	document.getElementById("type_x").style.display='none';
	document.getElementById("auth_type_x").style.display='none';
	document.getElementById("agent_code_x").style.display='none';	
	document.getElementById("individual_id_type_x").style.display='none';
	document.getElementById("individual_id_number_x").style.display='none';
	document.getElementById("city_x").style.display='none';
	document.getElementById("street_x").style.display='none';	
	document.getElementById("zip_x").style.display='none';
	document.getElementById("telephone1_x").style.display='none';
	document.getElementById("email_x").style.display='none';
	
	if(document.getElementById("state") != null)
	 {
	document.getElementById("state").style.backgroundColor='#fff';
	document.getElementById("state_x").style.display='none';
	 }
	 if(document.getElementById("lga") != null)
	 {
	document.getElementById("lga").style.backgroundColor='#fff';
	document.getElementById("lga_x").style.display='none';
	 }
	 
	  if(document.getElementById("code") != null)
	 {
	document.getElementById("code").style.backgroundColor='#fff';
	document.getElementById("code_x").style.display='none';
	 }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	 
	 if(document.getElementById("name").value == "")
	 {
	document.getElementById("name").style.backgroundColor='#4C787E';
	document.getElementById("name_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("type").value == "")
	 {
	document.getElementById("type").style.backgroundColor='#4C787E';
	document.getElementById("type_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("auth_type").value == "")
	 {
	document.getElementById("auth_type").style.backgroundColor='#4C787E';
	document.getElementById("auth_type_x").style.display='inline';
	cnt=cnt+1;
	 }		
	 
	 if(document.getElementById("agent_code").value == "")
	 {
	document.getElementById("agent_code").style.backgroundColor='#4C787E';
	document.getElementById("agent_code_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("individual_id_type").value == "")
	 {
	document.getElementById("individual_id_type").style.backgroundColor='#4C787E';
	document.getElementById("individual_id_type_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("individual_id_number").value == "")
	 {
	document.getElementById("individual_id_number").style.backgroundColor='#4C787E';
	document.getElementById("individual_id_number_x").style.display='inline';
	cnt=cnt+1;
	 }		
	 if(document.getElementById("city").value == "")
	 {
	document.getElementById("city").style.backgroundColor='#4C787E';
	document.getElementById("city_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("type").value == "")
	 {
	document.getElementById("type").style.backgroundColor='#4C787E';
	document.getElementById("type_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("auth_type").value == "")
	 {
	document.getElementById("auth_type").style.backgroundColor='#4C787E';
	document.getElementById("auth_type_x").style.display='inline';
	cnt=cnt+1;
	 }		
	 if(document.getElementById("street").value == "")
	 {
	document.getElementById("street").style.backgroundColor='#4C787E';
	document.getElementById("street_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("zip").value == "")
	 {
	document.getElementById("zip").style.backgroundColor='#4C787E';
	document.getElementById("zip_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if((document.getElementById("telephone1").value == "")||(validate_mobile(document.getElementById("telephone1").value)==false))
	 {
	document.getElementById("telephone1").style.backgroundColor='#4C787E';
	document.getElementById("telephone1_x").style.display='inline';
	cnt=cnt+1;
	 }		
	  	  
	  if((document.getElementById("email").value == "")||(validate_email (document.getElementById("email").value) == false))
	 {
	document.getElementById("email").style.backgroundColor='#4C787E';
	document.getElementById("email_x").style.display='inline';
	cnt=cnt+1;
	 }	
	 	 		
	 if( document.getElementById("state") != null)
	 { 
		  if(document.getElementById("state").value == "")
		 {
			document.getElementById("state").style.backgroundColor='#4C787E';			
			document.getElementById("state_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
	  if( document.getElementById("lga") != null)
	 { 
		  if(document.getElementById("lga").value == "")
		 {
			document.getElementById("lga").style.backgroundColor='#4C787E';			
			document.getElementById("lga_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
	  if( document.getElementById("code") != null)
	 { 
		  if(document.getElementById("code").value == "")
		 {
			document.getElementById("code").style.backgroundColor='#4C787E';			
			document.getElementById("code_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	 
	  if(cnt!=0)
	 {
	 alert("Please fill in the marked fileds");
	 }
	 
	  else 
	   if(cnt==0)
	   {
	document.getElementById("name").style.backgroundColor='#fff';
	document.getElementById("type").style.backgroundColor='#fff';
	document.getElementById("auth_type").style.backgroundColor='#fff';
	document.getElementById("agent_code").style.backgroundColor='#fff';	
	document.getElementById("individual_id_type").style.backgroundColor='#fff';
	document.getElementById("individual_id_number").style.backgroundColor='#fff';
	document.getElementById("city").style.backgroundColor='#fff';
	document.getElementById("street").style.backgroundColor='#fff';	
	document.getElementById("zip").style.backgroundColor='#fff';
	document.getElementById("telephone1").style.backgroundColor='#fff';
	document.getElementById("email").style.backgroundColor='#fff';
	
	   if(document.getElementById("state") != null) {	document.getElementById("state").style.backgroundColor='#fff';	 }
	 
	   if(document.getElementById("lga") != null)  {	document.getElementById("lga").style.backgroundColor='#fff';	 }
	 
	   if(document.getElementById("code") != null) {	document.getElementById("code").style.backgroundColor='#fff';	 }
	   
	document.getElementById("name_g").style.display='inline';
	document.getElementById("type_g").style.display='inline';
	document.getElementById("auth_type_g").style.display='inline';
	document.getElementById("agent_code_g").style.display='inline';	
	document.getElementById("individual_id_type_g").style.display='inline';
	document.getElementById("individual_id_number_g").style.display='inline';
	document.getElementById("city_g").style.display='inline';
	document.getElementById("street_g").style.display='inline';	
	document.getElementById("zip_g").style.display='inline';
	document.getElementById("telephone1_g").style.display='inline';
	document.getElementById("email_g").style.display='inline';
	
	  if(document.getElementById("state_g") != null)	 {	document.getElementById("state_g").style.display='inline';	 }
	  if(document.getElementById("lga_g") != null)	 {	document.getElementById("lga_g").style.display='inline';	 }
	  if(document.getElementById("code_g") != null)	 {	document.getElementById("code_g").style.display='inline';	 }
	  
	document.getElementById("regConfirm").style.display="none";
	document.getElementById("Save").style.display="inline";
	 }	
            
}
function getEditFileID()
{
var selectFiling=document.getElementById("selectFiling").value;
	if(selectFiling=="0")
	{
	alert("Please select a valid file number!!");	
	}
	else
	{
	window.location.href='e_filing.php?selectFiling='+selectFiling;
	}
}


function getDeleteFileID()
{
var selectFiling=document.getElementById("selectFiling").value;
	if(selectFiling=="0")
	{
	alert("Please select a valid file number!!");	
	}
	else
	{
	window.location.href='d_filing.php?selectFiling='+selectFiling;
	}
}


function confirmMarkDetails() {
    var cnt = 0;
    document.getElementById("sign_type").style.backgroundColor = '#fff';
    document.getElementById("title_of_product").style.backgroundColor = '#fff';
    document.getElementById("nice_class").style.backgroundColor = '#fff';
    document.getElementById("nice_class_desc").style.backgroundColor = '#fff';
    document.getElementById("vienna_classes").style.backgroundColor = '#fff';

    document.getElementById("sign_type_x").style.display = 'none';
    document.getElementById("title_of_product_x").style.display = 'none';
    document.getElementById("nice_class_x").style.display = 'none';
    document.getElementById("nice_class_desc_x").style.display = 'none';
    document.getElementById("vienna_classes_x").style.display = 'none';
    if (document.getElementById("xcode") != null) {
        document.getElementById("xcode").style.backgroundColor = '#fff';
        document.getElementById("xcode_x").style.display = 'none';
    }

    if (document.getElementById("sign_type").value == "") {
        document.getElementById("sign_type").style.backgroundColor = '#4C787E';
        document.getElementById("sign_type_x").style.display = 'inline';
        cnt = cnt + 1;
    }
    if (document.getElementById("title_of_product").value == "") {
        document.getElementById("title_of_product").style.backgroundColor = '#4C787E';
        document.getElementById("title_of_product_x").style.display = 'inline';
        cnt = cnt + 1;
    }
    if (document.getElementById("nice_class").value == "") {
        document.getElementById("nice_class").style.backgroundColor = '#4C787E';
        document.getElementById("nice_class_x").style.display = 'inline';
        cnt = cnt + 1;
    }

    if (document.getElementById("nice_class_desc").value == "") {
        document.getElementById("nice_class_desc").style.backgroundColor = '#4C787E';
        document.getElementById("nice_class_desc_x").style.display = 'inline';
        cnt = cnt + 1;
    }
    if (document.getElementById("vienna_classes").value == "") {
        document.getElementById("vienna_classes").style.backgroundColor = '#4C787E';
        document.getElementById("vienna_classes_x").style.display = 'inline';
        cnt = cnt + 1;
    }
    
    if (document.getElementById("xcode") != null) {
        if (document.getElementById("xcode").value == "") {
            document.getElementById("xcode").style.backgroundColor = '#4C787E';
            document.getElementById("xcode_x").style.display = 'inline';
            cnt = cnt + 1;
        }
    }

    if (cnt != 0) {
        alert("Please fill in the marked fileds");
    }

    else {
        document.getElementById("sign_type").style.backgroundColor = '#fff';
        document.getElementById("title_of_product").style.backgroundColor = '#fff';
        document.getElementById("nice_class").style.backgroundColor = '#fff';
        document.getElementById("nice_class_desc").style.backgroundColor = '#fff';
        document.getElementById("vienna_classes").style.backgroundColor = '#fff';
        if (document.getElementById("xcode") != null)
        { document.getElementById("xcode").style.backgroundColor = '#fff'; }

        document.getElementById("sign_type_g").style.display = 'inline';
        document.getElementById("title_of_product_g").style.display = 'inline';
        document.getElementById("nice_class_g").style.display = 'inline';
        document.getElementById("nice_class_desc_g").style.display = 'inline';
        document.getElementById("vienna_classes_g").style.display = 'inline';
        if (document.getElementById("xcode_g") != null) {
            document.getElementById("xcode_g").style.display = 'inline';
        }
        document.getElementById("regConfirm").style.display = "none";
        document.getElementById("Save").style.display = "inline";
    }

}

function getEditMarkID()
{
var selectMark=document.getElementById("selectMark").value;
	if(selectMark=="0")
	{
	alert("Please select a valid mark number!!");	
	}
	else
	{
	window.location.href='e_mark.php?selectMark='+selectMark;
	}
}


function getDeleteMarkID()
{
var selectMark=document.getElementById("selectMark").value;
	if(selectMark=="0")
	{
	alert("Please select a valid mark number!!");	
	}
	else
	{
	window.location.href='d_mark.php?selectMark='+selectMark;
	}
}

function confirmGoods_serviceDetails()  
{
var cnt=0;
	document.getElementById("nice_classes").style.backgroundColor='#fff';
	document.getElementById("national_classes").style.backgroundColor='#fff';
	
	document.getElementById("nice_classes_x").style.display='none';	
	document.getElementById("national_classes_x").style.display='none';
	  if(document.getElementById("code") != null)
	 {
	document.getElementById("code").style.backgroundColor='#fff';
	document.getElementById("code_x").style.display='none';
	 }
	 
	 if(document.getElementById("nice_classes").value == "")
	 {
	document.getElementById("nice_classes").style.backgroundColor='#4C787E';
	document.getElementById("nice_classes_x").style.display='inline';
	cnt=cnt+1;
	 }	
	  if(document.getElementById("national_classes").value == "")
	 {
	document.getElementById("national_classes").style.backgroundColor='#4C787E';
	document.getElementById("national_classes_x").style.display='inline';
	cnt=cnt+1;
	 }
	 if( document.getElementById("code") != null)
	 { 
		  if(document.getElementById("code").value == "")
		 {
			document.getElementById("code").style.backgroundColor='#4C787E';			
			document.getElementById("code_x").style.display='inline';
			cnt=cnt+1;
		 }
	 }
	 
	  if(cnt!=0)
	 {
	 alert("Please fill in the marked fileds");
	 }
	 
	  else  if(	(document.getElementById("nice_classes").value != "")&& (document.getElementById("national_classes").value != ""))
	  {
	 document.getElementById("nice_classes").style.backgroundColor='#fff';
	 document.getElementById("national_classes").style.backgroundColor='#fff';
	  if(document.getElementById("code") != null)
	 {	document.getElementById("code").style.backgroundColor='#fff';	 }
	 
	document.getElementById("nice_classes_g").style.display='inline';
	document.getElementById("national_classes_g").style.display='inline';
	  if(document.getElementById("code_g") != null)
	 {
	document.getElementById("code_g").style.display='inline';
	 }
	document.getElementById("regConfirm").style.display="none";
	document.getElementById("Save").style.display="inline";
	 }	
            
}

function getEditGoods_serviceID()
{
var selectGoods_service=document.getElementById("selectGoods_service").value;
	if(selectGoods_service=="0")
	{
	alert("Please select a valid service number!!");	
	}
	else
	{
	window.location.href='e_goods_service.php?selectGoods_service='+selectGoods_service;
	}
}


function getDeleteGoods_serviceID()
{
var selectGoods_service=document.getElementById("selectGoods_service").value;
	if(selectGoods_service=="0")
	{
	alert("Please select a valid service number!!");	
	}
	else
	{
	window.location.href='d_goods_service.php?selectGoods_service='+selectGoods_service;
	}
}


function getCountry()
{
var name=document.getElementById("name").value;
var type=document.getElementById("type").value;
var tax_id_type=document.getElementById("tax_id_type").value;
var tax_id_number=document.getElementById("tax_id_number").value;
var individual_id_type=document.getElementById("individual_id_type").value;
var individual_id_number=document.getElementById("individual_id_number").value;
var selectNationality=document.getElementById("selectNationality").value;
var selectCountry=document.getElementById("selectCountry").value;
if(document.getElementById("selectState")!=null)
{ var selectState=document.getElementById("selectState").value;}
if(document.getElementById("selectLga")!=null)
{ var selectLga=document.getElementById("selectLga").value;}
if(document.getElementById("state")!=null)
{ var state=document.getElementById("state").value;}
if(document.getElementById("lga")!=null)
{ var lga=document.getElementById("lga").value;}
var city=document.getElementById("city").value;
var street=document.getElementById("street").value;
var zip=document.getElementById("zip").value;
var telephone1=document.getElementById("telephone1").value;
var telephone2=document.getElementById("telephone2").value;
var email=document.getElementById("email").value;
var shared_ownership_notes=document.getElementById("shared_ownership_notes").value;	
	
	window.location.href='applicant.php?selectCountry='+selectCountry+'&name='+name+'&type='+type	+'&tax_id_type='+tax_id_type+'&tax_id_number='+tax_id_number+'&individual_id_type='+individual_id_type+'&individual_id_number='+individual_id_number+'&selectNationality='+selectNationality+'&selectState='+selectState	+'&selectLga='+selectLga+'&state='+state+'&lga='+lga+'&city='+city+'&street='+street+'&zip='+zip+'&telephone1='+telephone1+'&telephone2='+telephone2+'&email='+email+'&shared_ownership_notes='+shared_ownership_notes;
}

function getEditCountry()
{
var name=document.getElementById("name").value;
var type=document.getElementById("type").value;
var tax_id_type=document.getElementById("tax_id_type").value;
var tax_id_number=document.getElementById("tax_id_number").value;
var individual_id_type=document.getElementById("individual_id_type").value;
var individual_id_number=document.getElementById("individual_id_number").value;
var selectNationality=document.getElementById("selectNationality").value;
var selectCountry=document.getElementById("selectCountry").value;
if(document.getElementById("selectState")!=null)
{ var selectState=document.getElementById("selectState").value;}
if(document.getElementById("selectLga")!=null)
{ var selectLga=document.getElementById("selectLga").value;}
if(document.getElementById("state")!=null)
{ var state=document.getElementById("state").value;}
if(document.getElementById("lga")!=null)
{ var lga=document.getElementById("lga").value;}
var city=document.getElementById("city").value;
var street=document.getElementById("street").value;
var zip=document.getElementById("zip").value;
var telephone1=document.getElementById("telephone1").value;
var telephone2=document.getElementById("telephone2").value;
var email=document.getElementById("email").value;
var shared_ownership_notes=document.getElementById("shared_ownership_notes").value;	
	
	window.location.href='applicant.php?selectCountry='+selectCountry+'&name='+name+'&type='+type	+'&tax_id_type='+tax_id_type+'&tax_id_number='+tax_id_number+'&individual_id_type='+individual_id_type+'&individual_id_number='+individual_id_number+'&selectNationality='+selectNationality+'&selectState='+selectState	+'&selectLga='+selectLga+'&state='+state+'&lga='+lga+'&city='+city+'&street='+street+'&zip='+zip+'&telephone1='+telephone1+'&telephone2='+telephone2+'&email='+email+'&shared_ownership_notes='+shared_ownership_notes;
}

function getRepCountry()
{
var name=document.getElementById("name").value;
var type=document.getElementById("type").value;
var auth_type=document.getElementById("auth_type").value;
var agent_code=document.getElementById("agent_code").value;
var individual_id_type=document.getElementById("individual_id_type").value;
var individual_id_number=document.getElementById("individual_id_number").value;
var selectNationality=document.getElementById("selectNationality").value;
var selectCountry=document.getElementById("selectCountry").value;
if(document.getElementById("selectState")!=null)
{ var selectState=document.getElementById("selectState").value;}
if(document.getElementById("selectLga")!=null)
{ var selectLga=document.getElementById("selectLga").value;}
if(document.getElementById("state")!=null)
{ var state=document.getElementById("state").value;}
if(document.getElementById("lga")!=null)
{ var lga=document.getElementById("lga").value;}
var city=document.getElementById("city").value;
var street=document.getElementById("street").value;
var zip=document.getElementById("zip").value;
var telephone1=document.getElementById("telephone1").value;
var telephone2=document.getElementById("telephone2").value;
var email=document.getElementById("email").value;
	
window.location.href='representative.php?selectCountry='+selectCountry+'&name='+name+'&type='+type+'&auth_type='+auth_type+'&agent_code='+agent_code+'&individual_id_type='+individual_id_type+'&individual_id_number='+individual_id_number+'&selectNationality='+selectNationality+'&selectState='+selectState	+'&selectLga='+selectLga+'&state='+state+'&lga='+lga+'&city='+city+'&street='+street+'&zip='+zip+'&telephone1='+telephone1+'&telephone2='+telephone2+'&email='+email;
}

function getEditRepCountry()
{
var name=document.getElementById("name").value;
var type=document.getElementById("type").value;
var auth_type=document.getElementById("auth_type").value;
var agent_code=document.getElementById("agent_code").value;
var individual_id_type=document.getElementById("individual_id_type").value;
var individual_id_number=document.getElementById("individual_id_number").value;
var selectNationality=document.getElementById("selectNationality").value;
var selectCountry=document.getElementById("selectCountry").value;
if(document.getElementById("selectState")!=null)
{ var selectState=document.getElementById("selectState").value;}
if(document.getElementById("selectLga")!=null)
{ var selectLga=document.getElementById("selectLga").value;}
if(document.getElementById("state")!=null)
{ var state=document.getElementById("state").value;}
if(document.getElementById("lga")!=null)
{ var lga=document.getElementById("lga").value;}
var city=document.getElementById("city").value;
var street=document.getElementById("street").value;
var zip=document.getElementById("zip").value;
var telephone1=document.getElementById("telephone1").value;
var telephone2=document.getElementById("telephone2").value;
var email=document.getElementById("email").value;
	
window.location.href='representative.php?selectCountry='+selectCountry+'&name='+name+'&type='+type+'&auth_type='+auth_type+'&agent_code='+agent_code+'&individual_id_type='+individual_id_type+'&individual_id_number='+individual_id_number+'&selectNationality='+selectNationality+'&selectState='+selectState	+'&selectLga='+selectLga+'&state='+state+'&lga='+lga+'&city='+city+'&street='+street+'&zip='+zip+'&telephone1='+telephone1+'&telephone2='+telephone2+'&email='+email;
}

function getEditApplicationID()
{
var selectApplicant=document.getElementById("selectApplicant").value;
	if(selectApplicant=="0")
	{
	alert("Please select a valid applicant!!");	
	}
	else
	{
	window.location.href='e_applicant.php?selectApplicant='+selectApplicant;
	}
}


function getDeleteApplicationID()
{
var selectApplicant=document.getElementById("selectApplicant").value;
	if(selectApplicant=="0")
	{
	alert("Please select a valid applicant!!");	
	}
	else
	{
	window.location.href='d_applicant.php?selectApplicant='+selectApplicant;
	}
}


function getEditRepresentativeID()
{
var selectRepresentative=document.getElementById("selectRepresentative").value;
	if(selectRepresentative=="0")
	{
	alert("Please select a valid representative!!");	
	}
	else
	{
	window.location.href='e_representative.php?selectRepresentative='+selectRepresentative;
	}
}


function getDeleteRepresentativeID()
{
var selectRepresentative=document.getElementById("selectRepresentative").value;
	if(selectRepresentative=="0")
	{
	alert("Please select a valid representative!!");	
	}
	else
	{
	window.location.href='d_representative.php?selectRepresentative='+selectRepresentative;
	}
}


function doGetCat()
{
var selectCat=document.getElementById("selectCat").value;
	if(selectCat=="0")
	{
	alert("Please select a valid category!!");	
	}
	else
	{
	window.location.href='search.php?selectCat='+selectCat;
	}	
}

function doGetCri()
{
var selectCat=document.getElementById("selectCat").value;
var selectCri=document.getElementById("selectCri").value;
	if(selectCri=="0")
	{
	alert("Please select a valid criteria!!");	
	}
	else
	{
	window.location.href='search.php?selectCat='+selectCat+'&selectCri='+selectCri;
	}	
}

function logout(pwalletID)
{	
 window.location.href='signact.php?id='+pwalletID+'&x=0';	
}	

function doLogin()
{
	 if((document.getElementById("uname").value != "")  && (document.getElementById("pword").value != "") )
	 {
	postwith ('log_act.php',{'uname':document.getElementById("uname").value,'pword':document.getElementById("pword").value});
	 }
	  else
		 {
		alert("Please enter a vaild username and password!!");	 
		 }	 
}