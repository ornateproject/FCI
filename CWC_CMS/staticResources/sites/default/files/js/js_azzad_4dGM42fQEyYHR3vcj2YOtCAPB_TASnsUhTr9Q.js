var fontSize = 100;
jQuery(document).ready(function(){
		//alert("ok");
			if(_getCookie("fontSize") != null){
				var fontSize = _getCookie("fontSize");
			}else{
				var fontSize = 100;
			}
			jQuery(".fontSize").css("font-size",fontSize + "%");
});
function _getCookie (name) {
	var arg = name + "=";
	var alen = arg.length;
	var clen = document.cookie.length;
	var i = 0;
	while (i < clen) {
		var j = i + alen;
		if (document.cookie.substring(i, j) == arg) {
			return _getCookieVal (j);
		}
		i = document.cookie.indexOf(" ", i) + 1;
		if (i == 0) 
			break;
	}
	return null;
}
function _deleteCookie (name,path,domain) {
	if (_getCookie(name)) {
		document.cookie = name + "=" +
		((path) ? "; path=" + path : "") +
		((domain) ? "; domain=" + domain : "") +
		"; expires=Thu, 01-Jan-70 00:00:01 GMT";
	}
}
function _setCookie (name,value,expires,path,domain,secure) {
	var vurl = true;
	if(path != '' && path != undefined){
		vurl = validUrl(path);
	}
	if(jQuery.type(name) == "string" &&  vurl){
		document.cookie = name + "=" + escape (value) +
		((expires) ? "; expires=" + expires.toGMTString() : "") +
		((path) ? "; path=" + path : "") +
		((domain) ? "; domain=" + domain : "") +
		((secure) ? "; secure" : "");
	}
}
function _getCookieVal (offset) {
	var endstr = document.cookie.indexOf (";", offset);
	if (endstr == -1) { endstr = document.cookie.length; }
	return unescape(document.cookie.substring(offset, endstr));
}
/*********Font size resize**********/
function set_font_size(fontType){
	if(fontType == "increase"){
			 if(fontSize < 130){
			  fontSize = parseInt(fontSize) + 15;
			 }
		  }else if(fontType == "decrease"){
			  if(fontSize > 70){
				fontSize = parseInt(fontSize) - 15;
			  }
		  }else{
			  fontSize = 100;
		  }
	_setCookie("fontSize",fontSize);
	jQuery(".fontSize").css("font-size",fontSize + "%");
	//jQuery("#template_three_column").css("font-size",fontSize + "%");
} 
;

// date 24-2-2016   code for add class in mega menu  written by waliullah 
/*
( function($) {
$(document).ready(function(){
	//alert('hello');
if($('.nav-menu li ul[class="sub-nav-group"] li ul[class="sub-nav-group"] li').find('li')){
 alert('hello');	
}
			
});

} ) ( jQuery );
*/
// code end
jQuery(document).ready(function(){
	jQuery("#edit-search-block-form--2").attr("placeholder", "Search - Keyword, Phrase");
	jQuery(".gtranslate select").attr("id","gtranslate");			   
	jQuery("#gtranslate").before('<label class="notdisplay" for="gtranslate">Google Translate</label>');
	//contrast
	if(getCookie('contrast') == 0 || getCookie('contrast') == null){
	jQuery(".light").hide();
	jQuery(".dark").show();
    }else{
	jQuery(".light").show();
	jQuery(".dark").hide();
	
    }
    jQuery(".search-drop").css("display", "none");
    jQuery(".common-right ul li ul").css("visibility", "hidden");

// Fix Header

	var num = 36; //number of pixels before modifying styles
    jQuery(window).bind('scroll', function () {
        if (jQuery(window).scrollTop() > num) {
        jQuery('.fixed-wrapper').addClass('sticky');
		
    
        } else {
        jQuery('.fixed-wrapper').removeClass('sticky');
    
        }
    });		
			
		
	
// Mobile Nav	
jQuery('.sub-menu').append('<i class="fa fa-caret-right"></i>');
	jQuery('.toggle-nav-bar').click(function(){	
	jQuery('#nav').slideToggle();
	jQuery('#nav li').removeClass('open');
    jQuery("#nav li").click(function(){
		jQuery("#nav li").removeClass('open');
		jQuery(this).addClass('open');
	}); 
		
	});

	
//Skip Content
jQuery('a[href^="#skipCont"]').click(function() {
jQuery('html,body').animate({ scrollTop: jQuery(this.hash).offset().top}, 500);
return false;
e.preventDefault();

});

// Toggle Search



    jQuery("#toggleSearch").click(function(e) {
        jQuery(".search-drop").toggle();
        e.stopPropagation();
    });

    jQuery(document).click(function(e) {
        if (!jQuery(e.target).is('.search-drop, .search-drop *')) {
            jQuery(".search-drop").hide();
        }
    });


});


jQuery(document).ready(function(){
	
	jQuery("#main-menu div > ul" ).attr("id","nav");

	dropdown1('nav','hover',10);

	dropdown1("header-nav", "hover", 20);

});




//Drop down menu for Keyboard accessing

function dropdown1(dropdownId, hoverClass, mouseOffDelay) { 
	//if(dropdown = document.getElementById(dropdownId)) {
	//	var listItems = dropdown.getElementsByTagName('li');
	//	for(var i = 0; i < listItems.length; i++) {
	//		listItems[i].onmouseover = function() { this.className = addClass(this); }
	//		listItems[i].onmouseout = function() {
	//			var that = this;
	//			setTimeout(function() { that.className = removeClass(that); }, mouseOffDelay);
	//			this.className = that.className;
	//		}
	//		var anchor = listItems[i].getElementsByTagName('a');
	//		anchor = anchor[0];
	//		anchor.onfocus = function() { tabOn(this.parentNode); }
	//		anchor.onblur = function() { tabOff(this.parentNode); }
	//	}
	//}
	
	function tabOn(li) {
		if(li.nodeName == 'LI') {
			li.className = addClass(li);
			tabOn(li.parentNode.parentNode);
		}
	}
	
	function tabOff(li) {
		if(li.nodeName == 'LI') {
			li.className = removeClass(li);
			tabOff(li.parentNode.parentNode);
		}
	}
	
	function addClass(li) { return li.className + ' ' + hoverClass; }
	function removeClass(li) { return li.className.replace(hoverClass, ""); }
}

//<![CDATA[
jQuery(function ()
{
jQuery('table').wrap('<div class="scroll-table1"></div>');
jQuery(".scroll-table1").before( "<div class='guide-text'>Swipe to view <i class='fa fa-long-arrow-right'></i></div>" );

});
//]]>


jQuery(document).ready(function(){
	var params = new Array();
	var count = 0;
	jQuery('table.views-table thead tr th').each(function () {
		params[count] = jQuery(this).text();
		count++;	
	});
	jQuery('table.views-table tbody tr').each(function () {
		for(var j = 1; j <= count; j++){
			jQuery('td:nth-child('+j+')', this).attr("data-label",params[j-1]);
		}
	});
});


 function burstCache() {
if (!navigator.onLine) {
document.body.innerHTML = "Loading...";
window.location = "/";
}
}
//window.onload = burstCache;






;
//Style Sheet Switcher version 1.1 Oct 10th, 2006

//Author: Dynamic Drive: http://www.dynamicdrive.com
//Usage terms: http://www.dynamicdrive.com/notice.htm

//Unofficial Update to fix Safari 5.1 glitch re: alternate stylesheets or the disabled property in regards to them
// See: http://www.dynamicdrive.com/forums/showthread.php?p=259199 for more info

var manual_or_random="manual" //"manual" or "random"
var randomsetting="3 days" //"eachtime", "sessiononly", or "x days (replace x with desired integer)". Only applicable if mode is random.

//////No need to edit beyond here//////////////

function getCookie(Name) { 
	var re=new RegExp(Name+"=[^;]+", "i"); //construct RE to search for target name/value pair
	if (document.cookie.match(re)) //if cookie found
		return document.cookie.match(re)[0].split("=")[1] //return its value
	return null
}

function setCookie(name, value, days) {
	var expireDate = new Date()
	//set "expstring" to either future or past date, to set or delete cookie, respectively
	var expstring=(typeof days!="undefined")? expireDate.setDate(expireDate.getDate()+parseInt(days)) : expireDate.setDate(expireDate.getDate()-5)
	document.cookie = name+"="+value+"; expires="+expireDate.toGMTString()+"; path=/";
}
 
function deleteCookie(name){
	setCookie(name, "moot")
}

function setStylesheet(title, randomize){ //Main stylesheet switcher function. Second parameter if defined causes a random alternate stylesheet (including none) to be enabled
	var i, cacheobj, altsheets=[""];
	if(setStylesheet.chosen)
	try{
		document.getElementsByTagName('head')[0].removeChild(setStylesheet.chosen);
	}catch(e){}
	for(i=0; (cacheobj=document.getElementsByTagName("link")[i]); i++) {
		if(cacheobj.getAttribute("rel").toLowerCase()=="alternate stylesheet" && cacheobj.getAttribute("title")) { //if this is an alternate stylesheet with title
		cacheobj.disabled = true
		altsheets.push(cacheobj) //store reference to alt stylesheets inside array
			if(cacheobj.getAttribute("title") == title){ //enable alternate stylesheet with title that matches parameter
				cacheobj.disabled = false //enable chosen style sheet
				setStylesheet.chosen = document.createElement('link');//cloneNode(false);
				setStylesheet.chosen.rel = 'stylesheet';
				setStylesheet.chosen.type = 'text/css';
				if(cacheobj.media)
					setStylesheet.chosen.media = cacheobj.media;
				setStylesheet.chosen.href = cacheobj.href;
				document.getElementsByTagName('head')[0].appendChild(setStylesheet.chosen);
			}
		}
	}
	if (typeof randomize!="undefined"){ //if second paramter is defined, randomly enable an alt style sheet (includes non)
		var randomnumber=Math.floor(Math.random()*altsheets.length)
		altsheets[randomnumber].disabled=false
	}
	return (typeof randomize!="undefined" && altsheets[randomnumber]!="")? altsheets[randomnumber].getAttribute("title") : "" //if in "random" mode, return "title" of randomly enabled alt stylesheet
}

function chooseStyle(styletitle, days){ //Interface function to switch style sheets plus save "title" attr of selected stylesheet to cookie
	if (document.getElementById){
		setStylesheet(styletitle)
		setCookie("mysheet", styletitle, days)
	}
}

function indicateSelected(element){ //Optional function that shows which style sheet is currently selected within group of radio buttons or select menu
	if (selectedtitle!=null && (element.type==undefined || element.type=="select-one")){ //if element is a radio button or select menu
		var element=(element.type=="select-one") ? element.options : element
		for (var i=0; i<element.length; i++){
			if (element[i].value==selectedtitle){ //if match found between form element value and cookie value
				if (element[i].tagName=="OPTION") //if this is a select menu
					element[i].selected=true
				else{ //else if it's a radio button
					element[i].checked=true
				}
			break
			}
		}
	}
}
if (manual_or_random=="manual"){ //IF MANUAL MODE
	var selectedtitle=getCookie("mysheet")
	if (document.getElementById && selectedtitle!=null) //load user chosen style sheet from cookie if there is one stored
		setStylesheet(selectedtitle)
}else if (manual_or_random=="random"){ //IF AUTO RANDOM MODE
	if (randomsetting=="eachtime")
		setStylesheet("", "random")
	else if (randomsetting=="sessiononly"){ //if "sessiononly" setting
		if (getCookie("mysheet_s")==null) //if "mysheet_s" session cookie is empty
			document.cookie="mysheet_s="+setStylesheet("", "random")+"; path=/" //activate random alt stylesheet while remembering its "title" value
		else
			setStylesheet(getCookie("mysheet_s")) //just activate random alt stylesheet stored in cookie
	}
	else if (randomsetting.search(/^[1-9]+ days/i)!=-1){ //if "x days" setting
		if (getCookie("mysheet_r")==null || parseInt(getCookie("mysheet_r_days"))!=parseInt(randomsetting)){ //if "mysheet_r" cookie is empty or admin has changed number of days to persist in "x days" variable
			setCookie("mysheet_r", setStylesheet("", "random"), parseInt(randomsetting)) //activate random alt stylesheet while remembering its "title" value
			setCookie("mysheet_r_days", randomsetting, parseInt(randomsetting)) //Also remember the number of days to persist per the "x days" variable
		}
		else
		setStylesheet(getCookie("mysheet_r")) //just activate random alt stylesheet stored in cookie
	} 
}

jQuery(document).ready(function(){		
	jQuery('#menu-item-278 > a, #menu-item-194 > a, #menu-item-192 >  a').click(function(){return false;});		
	jQuery('.dark').click(function(){	
		var thirtyDays = 1000*60*60*24*30;
		var expireDate = new Date((new Date()).valueOf() + thirtyDays);		
		document.cookie = 'contrast' +"="+ 1 +"; expires="+expireDate.toGMTString()+"; path=/";
		document.cookie="username=John Doe";
		jQuery(".light").show();
		jQuery(".dark").hide();
		//jQuery('head').append('<link rel="stylesheet" type="text/css" media="screen" href="'+ base_url +'/'+ modulePath +'/assets/css/change.css">');
		jQuery('head').append('<link rel="stylesheet" type="text/css" media="screen" href="sites/all/themes/upsc/css/site-change.css">');
	});
	jQuery('.light').click(function(){	
		var thirtyDays = 1000*60*60*24*30;
		var expireDate = new Date((new Date()).valueOf() + thirtyDays);		
		document.cookie = 'contrast' +"="+ 0 +"; expires="+expireDate.toGMTString()+"; path=/";		
		jQuery(".light").hide();
		jQuery(".dark").show();		
		jQuery("[href*='change.css']").remove();
	});
	if(getCookie('contrast') == "1") {
		//jQuery('head').append('<link rel="stylesheet" type="text/css" media="screen" href="'+ base_url +'/'+ modulePath +'/assets/css/change.css">');
		jQuery('head').append('<link rel="stylesheet" type="text/css" media="screen" href="'+ base_url +'/'+ themePath +'/css/site-change.css">');
	}
	if(getCookie('contrast') == "0" ) {
		jQuery("[href*='/css/change.css']").remove();
	}
});
;
var se_controls = new Object(); 
se_controls.timerId;
function loadResultControls(settings){ 
	se_controls.searchServer = settings["searchServer"];
	se_controls.q = trim(settings["q"]);
	se_controls.tId = settings["textBoxId"];
	if (se_controls.startDate)
		se_controls.startDate =  trim(settings["startDate"]);
	else
		se_controls.startDate = "";

	if (se_controls.endDate)
		se_controls.endDate =   trim(settings["endDate"]);
	else
		se_controls.endDate = "";

	if (se_controls["count"] != undefined)
		se_controls.count =  trim(settings["count"]);
	else
		se_controls.count = "10";

	if (settings["fileType"] != undefined)
		se_controls.fileType =  trim(settings["fileType"]);
	else
		se_controls.fileType = "ALL";

	if (settings["lang"] != undefined)
		se_controls.lang =  trim(settings["lang"]);
	else
		se_controls.lang = "en";

	if (settings["site"] != undefined)
		se_controls.site =  trim(settings["site"]);
	else
		se_controls.site = "";

	if (settings["like_path"] != undefined)
		se_controls.like_path =  trim(settings["like_path"]);
	else
		se_controls.like_path = se_controls.searchServer+ "/content/images/like.png"

	if (settings["liked_path"] != undefined)
		se_controls.liked_path =  trim(settings["liked_path"]);
	else
		se_controls.liked_path = se_controls.searchServer+ "/content/images/liked.png"

	if (settings["DisLike_path"] != undefined)
		se_controls.disLike_path =  trim(settings["DisLike_path"]);
	else
		se_controls.disLike_path = se_controls.searchServer+ "/content/images/dislike.png"

	if (settings["liked_path"] != undefined)
		se_controls.disLiked_path =  trim(settings["DisLiked_path"]);
	else
		se_controls.disLiked_path = se_controls.searchServer+ "/content/images/disliked.png"

	if (settings["styleSheet"] != undefined)
		se_controls.styleSheet =  trim(settings["styleSheet"]);
	else
		se_controls.styleSheet = "default_JSON_1.0";

	if (settings["window"] != undefined)
		se_controls.window =  trim(settings["window"]);
	else
		se_controls.window = "_blank";
	
	if(settings["resultStartRow"] != undefined)
		se_controls.resultStartRow=settings["resultStartRow"];
	else
		se_controls.resultStartRow=0;
	if(settings["requestTimeOut"] != undefined)
		se_controls.requestTimeOut=Number(settings["requestTimeOut"]);
	else
		se_controls.requestTimeOut=10000;
	
	if(se_controls.q != undefined &&  se_controls.q != "")
	{
		var urlEntry =
			"searchKeyword="+ encodeURIComponent(se_controls.q)
			+ "&currentPage=1&startRow="+se_controls.resultStartRow
			+"&count="+ encodeURIComponent(se_controls.count)
			+ "&startDate="+encodeURIComponent(se_controls.startDate)+"&endDate="+encodeURIComponent(se_controls.endDate)
			+"&fileType="+ encodeURIComponent(se_controls.fileType)
			+"&stylesheet="+encodeURIComponent(se_controls.styleSheet)+"&output=json&lang="+encodeURIComponent(se_controls.lang)+"&type=SAS";

			sendAjaxRequest(urlEntry);
	}
	else
	{
		if(document.getElementById("result_area") != undefined)
	       document.getElementById("result_area").innerHTML ="";
	}
}	



function errorMsg()
{
	if(se_controls.resultDisplay != 2) // If successful Result is not displayed.
	{
		document.getElementById("result_area").innerHTML	= "<div class='internalError'>Request has timed out due to slow response from search server.</div>";
		se_controls.resultDisplay=0; //Error message is displayed
	}
}
function getSearchKeyword(response){
	return response['response']['header']['searchKeyword'];
}

function htmlEncode(value){
	  return htmlEscape(value);
	}
	
function htmlEscape(str) {
    return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
}

function getNoResultErrorHTML(response){
	var query = htmlEncode(getSearchKeyword(response));
	var retValue = '<div class="noResultError">' +
	       			'Your search - ' +
                                '<span class="searchKey">' +
                                        query +
                                 '</span>'+
                                 ' did not match any documents.<br/>' +
                                 'Suggestions :' +
                                  '<ul>'+
                                        '<li>  Make sure all words are spelled correctly.</li>'+
                                        '<li>Try different keywords.</li>' +
                                        '<li>Try more general keywords.</li>' +
                                        '<li>Try removing filter option.</li>' +
                                '</ul>' +
			'</div>';
	return retValue;
}

function getInvalidFromDateError(response){
	return "<div class='Error'>"+response['response']['error']['message']+"</div>";
}
function getInvalidToDateError(response){
	return "<div class='Error'>"+response['response']['error']['message']+"</div>";
}
function getInvalidYearError(response){
	return "<div class='Error'>"+response['response']['error']['message']+"</div>";
}
function getInvalidaDateRangeError(response){
	return "<div class='Error'>"+response['response']['error']['message']+"</div>";
}
function getFutureDateError(response){
	return "<div class='Error'>"+response['response']['error']['message']+"</div>";
}
function getInternalError(response){
	return "<div class='Error'>"+response['response']['error']['message']+"</div>";
}


/* Get error message, if any along with formatting. */
function getErrorHTML(response){
	var retValue=""
	var error_message=response['response']['error'];
	if(error_message!=undefined){
		error_id=error_message['id'];
		
		switch(error_id){
			case "noResult":{
				retValue=getNoResultErrorHTML(response);
				break;
			}
			case "invalidFromDate":{
				retValue=getInvalidToDateError(response);
				break;
			}
			case "invalidToDate":{
				retValue=getInvalidYearError(response);
				break;
			}

			case "invalidYear":{
				retValue=getInvalidYearError(response);
				break;
			}
			case "invalidDateRange":{
				retValue=getInvalidaDateRangeError(response);
				break;
			}
			case "futureDate":{
				retValue=getFutureDateError(response);
				break;
			}
			default:{ //internal error
				retValue = getInternalError(response);
				break;

			}
		}//switch ends
	} 
	
	return retValue;
}

function sendSpellCheckedQuery(response){
	// create a url for sending all paramtes found in response header 
	//except 
	// 1. searchKeyword = should be suggested spelling
	// 2. start = 0
	var suggestion = "";
	if(response['response']['result'] != undefined && response['response']['result'] ['spellcheck'] != undefined &&
		response['response']['result'] ['spellcheck']['suggestion'] != undefined) 
		{
		
	
		urlParam=response['response']['result'] ['spellcheck']['suggestion-link'];
		//urlParam=getExtraParamOfHeader(response,urlParam);
		sendAjaxRequest(urlParam);
		}
}

function getSpellingSuggestionHTML(response) {
	var spellCheckSuggestion = "";
	if(response['response']['result'] != undefined &&
		response['response']['result']['spellcheck'] != undefined) {

		var searchKeyword = htmlEncode(getSearchKeyword(response));
		var suggestion = htmlEncode(response['response']['result']['spellcheck']['suggestion']);
		spellCheckSuggestion  = '<div class="spellCheckerMain">' +
						'Showing Result For : ' + searchKeyword + '<br/>' +
						'<div class="spellChecker">' +
							'Did You Mean : ' +
					 		'<a class="spellCheckSuggestion" onclick="sendSpellCheckedQuery(se_controls.current_response)">' +
								suggestion +
							'</a>' +
						'</div>' +
					'</div>';
	}
	return spellCheckSuggestion;
}

function getResultsHTML(response){
	
	var resultHTML="";
		if(response['response']['result']!=undefined &&  response['response']['result']['doc'] != undefined)
		{
			resultHTML = '<div class="resultsBody">';

			for ( var i = 0; i < response['response']['result']['doc'].length; i++) {
				resultHTML += 
				"<div class='resultBody'>" +
				 	"<span class='contentType'>"+ getContentType(response,i)	+ "</span>" +
				 	"<div class='title'>"+getTitle(response,i)+"</div>" +
				 	"<span>"+ getLikeImage(response,i,se_controls.like_path) + "</span>" +
				 	"<span>"+getDisLikeImage(response,i,se_controls.disLike_path)+"</span>" +
				 	"<div class='content'>"+ getSnippet(response,i)+ "</div>" +
				 	"<div>" +
				 		"<span class='url'>"+getURL(response,i)+"</span> " + 
				 	/*	"<span class='docDate'>"+getDate(response,i)+"</span>" +*/
					"</div>" + 
				"</div>";
			}
			resultHTML += '</div>';
		}
	return resultHTML;
}
function getResultSummaryHTML(response){
	var totalResultHTML = "";
	if(response['response']['result'] != undefined){
		var totalResults = getTotalResult(response);
		if(totalResults != 0){
			var resultStart = Number(response['response']['header']['start']);  
			var pageSize = Number(response['response']['header']['count']);
			var resultEnd = totalResults;
			if(resultStart + pageSize < totalResults ){
				resultEnd = resultStart + pageSize;
			}

			if(totalResults <= pageSize){
				//Condition where all results can fit on single page
				// the display in that case should be "Showing 3 result"
				totalResultHTML = '<div class="resultSummary">Showing ' +  totalResults + ' result</div>';
			} else {
				//resultStart counts from 0, make it 1 for display purpose
				resultStart = resultStart + 1;
				totalResultHTML = '<div class="resultSummary">Showing ' + resultStart + ' - ' + resultEnd + ' of Total ' + totalResults + ' results </div>';
			}
		}
	}
	return totalResultHTML;
}

function getPageSummaryHTML(response){

	var pageDetailsHTML = "";	
	if(response['response']['result']!=undefined &&  response['response']['result']['doc'] != undefined) {
		var currentPageNo = getCurrentPageNumber(response);
		var totalPages = getTotalPages(response);
		//TODO:take care of duplicate detection logic and how this will work
		if(totalPages > 1) {
			pageDetailsHTML = "<div class='pageDetails'> Page " + currentPageNo	+ " of " + totalPages +" Pages</div>";
		}
	}
	return pageDetailsHTML;
}

function sendPaginationRequest(response,linkOffset)
{
     urlParam=response['response']['navigation']['pageLink'][linkOffset]['params'];
   // urlParam=getExtraParamOfHeader(response,urlParam);
    sendAjaxRequest(urlParam);
}
function sendNextPaginationRequest(response)
{
   urlParam=getNextPageLink(response);
    //urlParam=getExtraParamOfHeader(response,urlParam);
    sendAjaxRequest(urlParam);

                      
}
function sendPreviousPaginationRequest(response)
{
urlParam=getPreviousPageLink(response);
    //urlParam=getExtraParamOfHeader(response,urlParam);
    sendAjaxRequest(urlParam);
}
function getNavigationHTML(response){
	var navigationHTML = "";
	if(response['response']['navigation']!=undefined  && response['response']['navigation']['pageLink'] != undefined ) {
		var currentPage = getCurrentPageNumber(response);

		navigationHTML='<div class="navigationHTML">';

		// get previous page link
		if (getPreviousPageLink(response)  != "") {
			navigationHTML += '<a onclick="sendPreviousPaginationRequest(se_controls.current_response)" class="previousLink">' 
					+ "Prev" + '</a>';
		}

		// get all other links
		// current page link should have a special class

	
		var first_page = Number(response['response']['navigation']['pageLink'][0]['pageNum']);
	        if(response['response']['navigation'] != undefined && response['response']['navigation']['pageLink'] != undefined) {
			for ( var j = 0; j < response['response']['navigation']['pageLink'].length; j++) {
				var pageNum = response['response']['navigation']['pageLink'][j]['pageNum'];

				if(pageNum == currentPage) {
					navigationHTML += '<span class="currentLink">' + pageNum + '</span>';
				} else {
					navigationHTML += '<a onclick="sendPaginationRequest(se_controls.current_response,'+(pageNum - first_page) + ')" class="navigationLink">'
						+ pageNum + '</a>';
				}
			}
		}
			
		// get next page link	
		if(getNextPageLink(response)!= "") {
			navigationHTML += '<a onclick="sendNextPaginationRequest( se_controls.current_response)" class="nextLink"> Next </a>';
		}
		navigationHTML += "</div>";
	}
	return navigationHTML;
}



function displayResult(response)
{
	/**
		structure of the page
		spelling suggestion if any.
		error-message if any
		result summary
		results
		page summary
		navigation
	 */	
if(se_controls.resultDisplay == 1)// Loading Message is currently displayed
{
		var divHTML = getSpellingSuggestionHTML(response) +
				getErrorHTML(response) +
				getResultSummaryHTML(response)+
				getResultsHTML(response) +
				getPageSummaryHTML(response) +
				getNavigationHTML(response);

			se_controls.resultDisplay=2;//Successful result is displayed.

		//TODO: this should come from configuration
		document.getElementById("result_area").innerHTML = divHTML;
		document.getElementById(se_controls.tId).value =getSearchKeyword(response); 
}
}

function captureFeedback(feedback, redirectLink, i) {
	var like="like"+i;
	var dislike="dislike"+i;
	
	var urlEntry =  se_controls.searchServer + "/userFeedback?feedback="+ feedback + "&" + redirectLink+"&site="+se_controls.site;
		
	if (feedback == '1') {
		document.getElementById(like).src = se_controls.liked_path;
		document.getElementById(dislike).src = se_controls.disLike_path;

	} else {

		document.getElementById(dislike).src = se_controls.disLiked_path;
		document.getElementById(like).src = se_controls.like_path;
	}
	getJSONP(urlEntry, function(response){	
								});
}

function getSnippet(result,i)
{
return (result['response']['result']['doc'][i]['snnipetOne']);
}

function getURL(result,i)
{
return (result['response']['result']['doc'][i]['url']);
}
function getDate(result,i)
{
return (result['response']['result']['doc'][i]['date']);
}
function getNextPageLink(result)
{
if(result['response']['navigation'] != undefined && result['response']['navigation']['nextPageLink'] != undefined)
	return (result['response']['navigation']['nextPageLink']);
else 
	return "";
}

function getPreviousPageLink(result)
{
if( result['response']['navigation'] != undefined &&result['response']['navigation']['previousPageLink'] != undefined)
	return (result['response']['navigation']['previousPageLink']);
else 
	return "";
}
function getTitle(result,i)
{
return ("<a href=\" "+se_controls.searchServer+"/ClickedLink?"	+ result['response']['result']['doc'][i]['resultClickData'] + "\" target=\""+se_controls.window+"\">" + 
					result['response']['result']['doc'][i]['title']+ "</a>");
}
function getContentType(result,i)
{
	return (result['response']['result']['doc'][i]['contentType']);
}
function getLikeImage(result,i,imagePath)
{
likeUrl = result['response']['result']['doc'][i]['feedbackLike'];
return ("<img title='Recommend this result'  class='feedbackImg' src='"
					+imagePath + "' onclick=\"captureFeedback('1','"
					+ likeUrl
					+ "','"
					+ i
					+ "');\" id='like"+i+"'></img>");
}

function getDisLikeImage(result,i,imagePath)
{
disLikeUrl = result['response']['result']['doc'][i]['feedbackDislike'];
return ("<img class='feedbackImg'  src='" 
					+imagePath+ "'onclick=\"captureFeedback('-1','"
					+ disLikeUrl + "','"
					+ i	+ "');\" id='dislike"+i+"' title='Downgrade this result'></img>");
}
function getTotalPages(result)
{
	if(result['response']['result']['totalPages'] != undefined)
	   return (result['response']['result']['totalPages']);
	else
		return "";
}
function getTotalResult(result)
{
if(result['response']['result']['totalResult'] != undefined)
	return (result['response']['result']['totalResult']);
else
	return "0";
}


function getCurrentPageNumber(result)
{
	
    if(result['response']['result']['currentPage'] != undefined)
		return (Number(result['response']['result']['currentPage']) + 1);
	else 
		return "";
}

/*function getErrorMessage()
{
if(result['response']['error']['message'] != undefined)
  return (result['response']['error']['message'])
else
	return "";
}*/
/*
se_controls.resultDisplay 

When message is loading at such a time se_controls.resultDisplay=1; , timer starts and control transfer towards displayResult(response); .
When result display successfully ,se_controls.resultDisplay=2 ; 
When time out by specified duration ,check for se_controls.resultDisplay is not 2 ,then load error message.

se_controls.resultDisplay = 1 Loading Message Displayed
se_controls.resultDisplay = 2 Successful Result Displayed
se_controls.resultDisplay = 0 Error Message Displayed
*/

function sendAjaxRequest(urlEntry)
{
	
	if( document.getElementById("result_area") != null && se_controls.q != "")
  		document.getElementById("result_area").innerHTML = "Loading...";
	var urlEntry = se_controls.searchServer+ "/Search?" + urlEntry+"&site="+se_controls.site+"&callback=?";
	 se_controls.resultDisplay=1;	//Loading Message is displayed.
	 se_controls.timerId=setTimeout("errorMsg();",se_controls.requestTimeOut);
	getJSONP(urlEntry, function(response){
			se_controls.current_response=response;
			displayResult(response);
			clearTimeout(se_controls.timerId);
		}
	);
};
var se_controls = new Object();
function loadSuggestionControls(settings)
{
se_controls.searchServer=settings["searchServer"];
se_controls.tId=settings["textBoxId"];
se_controls.lang=settings["lang"];
se_controls.callBackFun=settings["callBackFunction"];
se_controls.site=settings["site"];
se_controls.request_count=0;
se_controls.cachedResponse;
se_controls.openFlag=0;

if (settings["siteSpcificSuggestion"] != undefined)
	se_controls.siteSpcificSuggestion =   trim(settings["siteSpcificSuggestion"]);
else
	se_controls.siteSpcificSuggestion = "true";

}
function getJSONP(url, success) 
{
    var ud = '_' + +new Date,
        script = document.createElement('script'),
        head = document.getElementsByTagName('head')[0] 
               || document.documentElement;

    window[ud] = function(data) {
        head.removeChild(script);
        success && success(data);
    };

    script.src = url.replace('callback=?', 'callback=' + ud);
    head.appendChild(script);

}
function trim(str)
{
	if(str)
		return str.replace(/^\s+|\s+$/g, "");
}
var noOfAuto = 0;
var currentLi = 1;
function autoComplete(){
			var val=document.getElementById(se_controls.tId).value;
			val=encodeURIComponent(val);
			if(val!=null && val!="")
			{
				showAutoComplete();
				se_controls.request_count = se_controls.request_count + 1;
				if(se_controls.siteSpcificSuggestion == "true")
					se_controls.urlEntry= se_controls.searchServer+"/AutoSuggestImpl?prefix="+val+"&output=json&site="+se_controls.site+"&reqc="+se_controls.request_count+ "&siteSpecificSuggestion="+se_controls.siteSpcificSuggestion+"&callback=?";
				else
					se_controls.urlEntry=se_controls.searchServer+"/AutoSuggestImpl?prefix="+val+"&output=json&reqc="+se_controls.request_count+ "&siteSpecificSuggestion="+se_controls.siteSpcificSuggestion+"&callback=?";
				getJSONP(se_controls.urlEntry, function(jsonResponse){
					var results = [];
					var currentResponse = jsonResponse['request_count'];
                    var json = jsonResponse['content'];
				var resultStr = '<ul class="ui-autocomplete ui-menu ui-widget ui-widget-content ui-corner-all" role="listbox" aria-activedescendant="ui-active-menuitem">';
				
				if(currentResponse == se_controls.request_count ){
					if(json['response']!= undefined &&   json['response']['docs'] != undefined )
					{
						se_controls.cachedResponse =  json['response']['docs'];
						noOfAuto = json['response']['docs'].length;
						for(var i=0;i<json['response']['docs'].length;i++)
						{
							results.push(json['response']['docs'][i]['suggestionPhrase']);
							var clsstr = '';
							if(i == 0){
								//clsstr = 'current';
							}
							resultStr += '<li class="ui-menu-item" role="menuitem"><a class="ui-corner-all '+clsstr+'"  onclick="linkClick(\''+json['response']['docs'][i]['suggestionPhrase']+'\')" href="javascript:;">'+json['response']['docs'][i]['suggestionPhrase']+'</a></li>';
						}

					}
					}else{
						if(se_controls.cachedResponse != undefined && se_controls.openFlag == 1)
						{
							for(var i=0;i<json['response']['docs'].length;i++)
							{
								var clsstr = '';
								if(i == 0){
									//clsstr = 'current';
								}
								results.push(json['response']['docs'][i]['suggestionPhrase']);
								resultStr += '<li class="ui-menu-item" role="menuitem"><a  href="javascript:;" class="ui-corner-all '+clsstr+'">'+json['response']['docs'][i]['suggestionPhrase']+'</a></li>';
							}
						}
					}
					resultStr += '</ul>';
					document.getElementById("auto_suggesion").innerHTML=resultStr;		
					document.getElementById('search_key').onkeydown = khandle;
					document.getElementById('auto_suggesion').firstChild.onkeydown = khandle1;					
				});
			}

		}
	
function showAutoComplete(){
		document.getElementById("auto_suggesion").style.display='block';
	}
	function hideAutoComplete(){
		document.getElementById("auto_suggesion").style.display='none';
	}
	function linkClick(value)
{
	document.getElementById('search_key').value=value;						
	callBack();
	document.getElementById("auto_suggesion").style.display='none';
}

function khandle(e) {
	e = e || event
	
	if(e.keyCode==40)
	{
		if(noOfAuto > 0 && currentLi >= 0 && currentLi < noOfAuto){
			currentLi = 1;
			currentKey = 1;
			var x=document.getElementById("auto_suggesion");  
			element=x.firstChild.firstChild.firstChild;
			element.className = element.className + " ui-state-hover"
			element.focus();
			document.getElementById("search_key").value=element.innerHTML;
			e.preventDefault();
		}
	}
}
function khandle1(e) {
	e = e || event
	if(currentKey == 1)
	{
		if (e.keyCode == 38) 
		{
			// go UP	
			upwards(e);
			
		} 
		else if (e.keyCode == 40) 
		{
			// go Down
			downwards(e);
		}
		else if (e.keyCode == 27) 
		{
			// go Esc
			hideAutoComplete();
		}
	}
	
}
function getElementsByClass(node,searchClass,tag) {
    var classElements;
    var els = node.getElementsByTagName(tag);
    for (var i = 0; i < els.length; i++)
	{
         if ( els[i].className.indexOf(searchClass) !== -1)
		 {
             classElements= els[i];
		 }
	}
    return classElements;
}

function upwards(e)
{
	if(noOfAuto > 0 && currentLi > 1){
		currentLi--;
		var t=getElementsByClass(document.getElementById("auto_suggesion"),"ui-state-hover","a");
		var tt=t.parentNode.previousSibling.firstChild;
		tt.className = tt.className + " ui-state-hover"
		t.className = "ui-corner-all";
		document.getElementById('search_key').value=tt.innerHTML;
		tt.focus();
		e.preventDefault();
	}
}
function downwards(e)
{	if(noOfAuto > 0 && currentLi < noOfAuto){
		currentLi++;
		var t=getElementsByClass(document.getElementById("auto_suggesion"),"ui-state-hover","a");
		var tt=t.parentNode.nextSibling.firstChild;
		tt.className = tt.className + " ui-state-hover"
		t.className = "ui-corner-all";			
		document.getElementById('search_key').value=tt.innerHTML;
		tt.focus();
		e.preventDefault();		
	}
};
