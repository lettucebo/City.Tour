var minute = 0;
var sec = 0;
var msec = 0;
var isCountDown = false;
var prompt = 0;

function pauseCountDown(){
	isCountDown = false;
}

function startCountDown(callback){
	isCountDown = true;
	countDown(callback);
}

function setTimesupPrompt(promptFunc){
	prompt = promptFunc
}

function countDown(callback, timesupMinute = 5){ // timesupMinute = 5 代表每過5分鐘就會觸發的行為
	
	if(!isCountDown){
		return;
	}

	// console.log(minute + ":" + sec + ":" + msec);

	setTimeout(function(){

		if(msec > 0){
			setMSec(msec - 1);
		}else{
			if(sec > 0){
				setMSec(99);
				setSec(sec - 1);
			}else{
				if(minute > 0){
					setSec(59);
					setMinute(minute - 1);
					if(minute % timesupMinute == 0)
						(prompt && typeof(prompt) === "function") && prompt(minute);
				}else{
					setMinute(0);
					isCountDown = false;
				}
			}
		}

		if(isCountDown){

			countDown(callback);
		}else{
			if(msec == 0 && minute == 0 && sec == 0)
				(callback && typeof(callback) === "function") && callback();
			else
				console.log('countDown continue');
		}
	}, 1);
}


/*輸入目標的時間字串 "年-月-日 時:分：秒" e.g. "2018-1-21 00:20:53" */
function getTimeDiffString(targetTimeStr){
	
	var timeDiff = calcTime(targetTimeStr) - calcTime();

	console.log(timeDiff);

	var totalSeconds = Math.floor(timeDiff / 1000 );

	hours = Math.floor(totalSeconds / 3600);
	totalSeconds %= 3600;
	minutes = Math.floor(totalSeconds / 60);
	seconds = totalSeconds % 60;

	minutes = minutes + 60 * hours;

	return minutes + ":" + seconds + ":00";
}

/*注意！只有for demo 使用*/
function getDemoTime(){

	var addMinute = 99;
	var date = new Date();
	date = new Date(date.getTime() + addMinute * 60000);
	console.log("demo date: " + date);
	return date.getTime();
}

function calcTime(targetTimeStr = '', offset = '+8') {
    // create Date object for current location
    var date = new Date();
    if(targetTimeStr != ''){
    	date = new Date(targetTimeStr);
    }

    console.log('date: ' + date);
   
    var utc = date.getTime() + (date.getTimezoneOffset() * 60000);

    console.log('utc: ' + utc);

    var ndate = new Date(utc + (3600000*offset));

    console.log('ndate: ' + ndate);
    return ndate.getTime();
}

/*使用格式 分:秒:毫秒，e.g. '20:00:00'*/
function setTime(strTime){
	var arr = strTime.split(':');
	setMinute(arr[0]);
	setSec(arr[1]);
	setMSec(arr[2]);
}

function setMSec(num){
	num = checkNum(num);
	msec = num;
	setNum(num, '#timer-msec');
}

function setMinute(num){
	num = checkNum(num);
	minute = num;
	setNum(num, '#timer-min');
}

function setSec(num){
	num = checkNum(num);
	sec = num;
	setNum(num, '#timer-sec');
}

function setNum(num, elemID){
	var first = Math.floor(num / 10);
	var last = num % 10;
	// console.log("setNum: " + first + "," + last);
	$(elemID).find('.first').css('background-position-y',getPosition(first));
	$(elemID).find('.last').css('background-position-y',getPosition(last));
}

function checkNum(num){
	if(num > 99)
		return 99;
	else if(num < 0)
		return 0;
	return num;
}

function getPosition(num){
	if(num > 9 || num < 0){
		return '0';
	}

	switch (num){
		case 0:
			return '1%';
		case 1:
			return '12%';
		case 2:
			return '23%';
		case 3:
			return '34%';
		case 4:
			return '44.5%';
		case 5:
			return '55.5%';
		case 6:
			return '66.5%';
		case 7:
			return '77.5%';
		case 8:
			return '88.3%';
		case 9:
			return '99%';
		default:
			return '1%';
	}
	return '0';
}

