function searchNoticesByDepartment(){
	 var tendorStatus = $("#Department").val();
	//alert(tendorStatus);
   
   	
   	

	
	if(tendorStatus!="Planning Department" && tendorStatus!="Building Permission Department" &&  tendorStatus!="Engineering Department"&&  tendorStatus!="Illegal Construction Department" 
		&& 	tendorStatus!="Land and Estate Department" && tendorStatus!="Accounts andFinance Department" && tendorStatus!="Administration Department"
		&& tendorStatus!="Centre for Excellence"  && tendorStatus!="Private and Foreign Direct Investment Department" 
			 && tendorStatus!="Fire Department"  && tendorStatus!="All Department"    )
				{
	
	
	
				}
	
	else if (tendorStatus=="Planning Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		       
		        if( $(this).text() != 'Planning Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	else if (tendorStatus=="Building Permission Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Building Permission Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	else if (tendorStatus=="Engineering Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Engineering Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	
	
	else if (tendorStatus=="Illegal Construction Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Illegal Construction Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	
	
	else if (tendorStatus=="Land and Estate Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Land and Estate Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	
	else if (tendorStatus=="Accounts andFinance Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Accounts andFinance Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	else if (tendorStatus=="Administration Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Administration Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	
	else if (tendorStatus=="Centre for Excellence")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Centre for Excellence')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	else if (tendorStatus=="Private and Foreign Direct Investment Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Private and Foreign Direct Investment Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	else if (tendorStatus=="Fire Department")
	{
		$(function() {
		    $('.A').filter(function() {
		    	
		      
		        if( $(this).text() != 'Fire Department')
		        	{
		       
		        	$(this).parent().hide();
		        	}
		        else
		        	{
		        	$(this).parent().show();
		        	}
		      
		        
		        
		    })
		});
	}
	else
	{
		
		location.reload();
	}
}