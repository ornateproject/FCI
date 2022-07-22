function SubmitDates(){
	    	   //alert("hi");
	 		  var fromdate = $("#fromdate").val(); 
	           var d = Date.parse(fromdate);
	           var todate = $("#todate").val(); 
	           var dd = Date.parse(todate);
	          // alert("hi"+dd);
	           $(function() {
	 			$('.filterDates').filter(function() {
	 	        var ColumnDate = $(this).text();
	 		    var day = ColumnDate.substring(0, 2);
	 			var monttoh = ColumnDate.substring(3, 5);
	 			var year = ColumnDate.substring(6, 10);
	 			var dx = Date.parse(year+"-"+monttoh+"-"+day);
	 			  //alert("hi"+dx);
	 				if(dx>d && dx<dd){
	 				  $(this).parent().hide();
	 				// alert("hi");
	 				     var year1 =  dx.getFullYear();
	 				 	 var monthq =  dx.getMonth()+1;
	 					 var day1  =dx.getDate();
	 					 if(monthq==2 || monthq=="2" || monthq==1 || monthq=="1" || monthq==3 || monthq=="3" || monthq==4 || monthq=="4" || monthq==5 || monthq=="5" || monthq==6 || monthq=="6" || monthq==7 || monthq=="7" || monthq==8 || monthq=="8" || monthq==9 || monthq=="9"){
	 						var c = day1+"-"+0+monthq+"-"+year1;
	 						if(ColumnDate==c){
	 							$(this).parent().hide();
	 							('.filterDates').hide();
	 							$(".filterDates").parents("tr").hide();
	 							$(".filterDates").parents("td").hide();
	 							
	 						}
	 					    }
	 					 else if(ColumnDate==day1+"-"+monthq+"-"+year1){
	 							$(this).parent().hide();
	 							('.filterDates').hide();
	 							$(".filterDates").parents("tr").hide();
	 							$(".filterDates").parents("td").hide();
	 							
	 						}
	 						 {
	 						 
	 						 }
	 				        }
	 				else{
	 					$(this).parent().hide();
	 				}
	 				        })
	 				        });
	 		                }