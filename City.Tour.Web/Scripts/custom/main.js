function detectmob() { 
	if( navigator.userAgent.match(/Android/i)
	|| navigator.userAgent.match(/webOS/i)
	|| navigator.userAgent.match(/iPhone/i)
	|| navigator.userAgent.match(/iPad/i)
	|| navigator.userAgent.match(/iPod/i)
	|| navigator.userAgent.match(/BlackBerry/i)
	|| navigator.userAgent.match(/Windows Phone/i)
	){
		return true;
	}
	else {
		return false;
	}
}

if(!detectmob()){
	console.log("請使用手機瀏覽！");
}

window.addEventListener("orientationchange",onOrientationchange ,false);
   function onOrientationchange() {
	if (window.orientation === 180 || window.orientation === 0) {
	      //直式
	}
	if (window.orientation === 90 || window.orientation === -90 ){
	    console.log("手機請使用直向瀏覽！");
	} 
}