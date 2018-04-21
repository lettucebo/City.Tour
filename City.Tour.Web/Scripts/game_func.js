
var successDialog = $('#success-dialog');
var failDialog = $('#fail-dialog');

/*蛋蛋依照不同等級亮燈，level為 0 - 5 數字*/
function setEgg(level){

	var elem = $('#story-content').find('img.main-pic-egg');
	/*<!-- <img class="main-pic-egg active1" src="/assets/imgs/main-egg-active1.png" id="active" /> -->
	<!-- <img class="main-pic-egg active2" src="/assets/imgs/main-egg-active2.png" id="active" /> -->
	<img class="main-pic-egg active3" src="/assets/imgs/main-egg-active3.png" id="active" />
	<!-- <img class="main-pic-egg active4" src="/assets/imgs/main-egg-active4.png" id="active" /> -->
	<!-- <img class="main-pic-egg active5" src="/assets/imgs/main-egg-active5.png" id="active" /> -->*/
	if(level > 0){
		elem.addClass('active');
		//更改蛋蛋等級的圖
		elem.attr('src', '/assets/imgs/main-egg-active'+level+'.png');
		elem.addClass('active' + level);
	}
}

function setDialog(submitCallback){
	$( "#dialog" ).dialog({
		autoOpen: false,
		show: {
			effect: "blind",
			duration: 1000
		},
		hide: {
			effect: "explode",
			duration: 0
		},
		buttons:{
			"送出": function() {
	  			(submitCallback && typeof(submitCallback) === "function") && submitCallback();
			}
		}
	});
}

function showFailDialog(callback){
	
	failDialog.show(0);
	var bg = failDialog.find('.dialog-bg');
	bg.off('click');
	bg.on('click', function(){

		failDialog.hide(0);
		(callback && typeof(callback) === "function") && callback();
	})
}

function showSuccessDialog(callback){
	
	setEgg(5);
	successDialog.show(0);
	var bg = successDialog.find('.dialog-bg');
	bg.off('click');
	bg.on('click', function(){

		successDialog.hide(0);
		(callback && typeof(callback) === "function") && callback();
	})
}


