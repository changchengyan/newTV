		setInterval('ReloadPage()', 30 * 60 * 1000);

		function ReloadPage() {
		    if (getCookie("isExsist") != "1") {
		        setCookie('isExsist', '1', 1);
		        window.location.reload();
		    }
		}
		//设置cookie
		function setCookie(cname, cvalue, exdays) {
		    var d = new Date();
		    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
		    var expires = "expires=" + d.toUTCString();
		    document.cookie = cname + "=" + cvalue + "; " + expires;
		}
		//获取cookie
		function getCookie(cname) {
		    var name = cname + "=";
		    var ca = document.cookie.split(';');
		    for (var i = 0; i < ca.length; i++) {
		        var c = ca[i];
		        while (c.charAt(0) == ' ') c = c.substring(1);
		        if (c.indexOf(name) != -1) return c.substring(name.length, c.length);
		    }
		    return "";
		}
		//使用示例
		setCookie('isExsist', '1', 1);
		var cutStr = function(str) {
		    return str.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
		}
		var startY, startX, endX, endY;
		var swiperF = {
		    onTouchStart: function(event) {
		        var event = event || window.event;
		        var touch = event.touches[0];
		        startY = touch.pageY;
		        startX = touch.pageX;
		    },
		    onTouchMove: function(event) {
		        var event = event || window.event;
		        var touch = event.touches[0];
		        endX = touch.pageX - startX;
		        endY = touch.pageY - startY;
		    },
		    onTouchEnd: function() {
		        if (endX < 0) {
		            window.location.href = "component.aspx"
		        } else {
		            window.location.href = "book.aspx"
		        }
		    }
		}

		function keyUp(e) {
		    if (navigator.appName == "Microsoft Internet Explorer") {
		        var keycode = event.keyCode;
		        var realkey = String.fromCharCode(event.keyCode);
		    } else {
		        var keycode = e.which;
		        var realkey = String.fromCharCode(e.which);
		    }
		    if (keycode == 39) {
		        window.location.href = "component.aspx";
		    } else if (keycode == 37) {
		        window.location.href = "book.aspx"
		    }
		}
		   var myChart = echarts.init(document.getElementById('main'));
		   var option = {
		    backgroundColor: 'transparents',
            color: ['yellow', 'white', 'red'],
			tooltip: {
		        trigger: 'axis'
		    },
		    grid: {
		        top:"5%",
		        left: '2%',
		        right: '0%',
		        bottom: '2%',
		        containLabel: true
		    },
		    toolbox: {
		        "show": false
		    },
		    xAxis: {
		        type: 'category',
		        "axisLine": {
		            lineStyle: {
		                color: '#fff'
		            }
		        },
		        "axisTick": {
		            "show": false
		        },
		        axisLabel: {
		            textStyle: {
		                color: '#fff'
		            }
		        },
		        boundaryGap: false,
		        data: []
		    },
		    yAxis: {
		        "axisLine": {
		            lineStyle: {
		                color: '#fff'
		            }
		        },
		        splitLine: {
		            show: true,
		            lineStyle: {
		                color: '#fff'
		            }
		        },
		        "axisTick": {
		            "show": false
		        },
		        axisLabel: {
		            textStyle: {
		                color: '#fff'
		            }
		        },
		        type: 'value',
		        axisLabel : {
		            textStyle: {
		                color: '#fff',
		                fontSize:"12px"
		            }
		        }
		    },
		    series: [
		        {
		            name:'前三个月',
		            symbol:'none',  //这句就是去掉点的  
		           smooth:true,
		           //这句就是让曲线变平滑的
		           symbolSize: 8,
		            type:'line',
		            data: []
		        },
		        {
		            name:'前两月',
		            symbol:'none',  //这句就是去掉点的  
		           smooth:true,  //这句就是让曲线变平滑的
		           symbolSize: 8,
		            type:'line',
		            data: []
		        },
		        {
		            name:'前一月',
		            symbol:'none',  //这句就是去掉点的  
		           smooth:true,  //这句就是让曲线变平滑的
		           symbolSize: 8,
		            type:'line',
		             data: []
		        }
		    ]
				};
		var dataChange = function() {
		    $.ajax({
		        url: '/?method=BigDataBO.GetCurrData',
		        type: 'POST',
		        dataType: 'json',
		        success: function(json) {
//		            $("#nowNum").html(cutStr(json.today_user_count + " "));
		            $("#inLineNum").html(cutStr(json.online_user_count + " "));
		        },
		        error: function(xhr, textStatus) {

		        }
		    });
		}
		setInterval('dataChange()', 40 * 1000);

		function ShowTotalMoney(totalmoney) {
			   var totalMoney=totalmoney;
		    var numScroll = $(".trideNumScroll").numberAnimate({
		        num: totalMoney,
		        dot:2,
		        speed: 1000,
		        symbol: ","
		    });
		    setInterval(function() {
		        $.ajax({
		            url: '/?method=BigDataBO.GetTotalMoney',
		            type: 'POST',
		            dataType: 'json',
		            success: function(json) {
		            	if(json.total_money!=totalMoney){
		                numScroll.resetData(json.total_money);
		           		 }
		            }
		        });;
		    }, 10 * 1000);
		}

		function InitBaseInfo() {
		    $.ajax({
		        url: '/?method=BigDataBO.GetBaseData',
		        type: 'POST',
		        dataType: 'json',
		        success: function(json) {
		            $("#userNum").html(cutStr(json.user_count + " "));
		            $("#book_view").html(cutStr(json.book_count + " "));
		            ShowTotalMoney(json.total_money);
		        },
		        error: function(xhr, textStatus) {}
		    });
		}
		function initBaseInfo(){
			 // 16 17年用户浏览趋势对比
		    $.ajax({
		        url: '/?method=BigDataBO.GetTwoYearData',
		        type: 'POST',
		        dataType: 'json',
		        success: function (json) {
		        	var datalastOne=[],
		        	datalastTwo=[],
		        	datalastThree=[];
		        	for(var k=0;k<json.lastOneMonthList.length;k++){
		        		datalastOne[k]=json.lastOneMonthList[k].num;
		        	}
		        	for(var i=0;i<json.lastTwoMonthList.length;i++){
		        		datalastTwo[i]=json.lastTwoMonthList[i].num;
		        	}
		        	for(var i=0;i<json.lastThreeMonthList.length;i++){
		        		datalastThree[i]=json.lastThreeMonthList[i].num;
		        	}
		        	myChart.setOption({
				        series: [{
				            // 根据名字对应到相应的系列
				            data: datalastThree
				        },{
				        	data:datalastTwo
				        },{
				        	data:datalastOne
				        }]
				    });
		        },
		        error: function (xhr, textStatus) { }
		    });
		}
		setInterval("initBaseInfo()",20*1000);
var data_astronaut = document.querySelector(".data_astronaut");
var redFly=document.querySelector(".red_fly");
var whiteFly=document.querySelector(".white_fly");
var yellowFly=document.querySelector(".yellow_fly");
    Transform(data_astronaut);
    Transform(redFly);
    Transform(whiteFly);
    Transform(yellowFly);
	var Tween = createjs.Tween,
	sineInOutEase = createjs.Ease.linear;
	Tween.get(data_astronaut, {loop: true}).to({tranlateY: 0}, 0, sineInOutEase).to({translateY:-20}, 5000, sineInOutEase).to({translateY:0}, 5000, sineInOutEase);
	Tween.get(redFly, {loop: true}).to({tranlateX: 0}, 0, sineInOutEase).to({translateX:20}, 3000, sineInOutEase).to({translateX:0}, 3000, sineInOutEase);
	Tween.get(whiteFly, {loop: true}).to({tranlateX: 0}, 0, sineInOutEase).to({translateX:20}, 3000, sineInOutEase).to({translateX:0}, 3000, sineInOutEase);
	Tween.get(yellowFly, {loop: true}).to({tranlateX: 0}, 0, sineInOutEase).to({translateX:20}, 3000, sineInOutEase).to({translateX:0}, 3000, sineInOutEase);
var satePic = document.querySelector("#satePic");
    Transform(satePic);
Tween.get(satePic, {loop: true}).to({translateX:0}, 0, sineInOutEase).to({translateX:30}, 4000, sineInOutEase).to({translateX:0}, 4000, sineInOutEase);
var earthRotate = document.querySelector(".earthRotate");
    Transform(earthRotate);
    earthRotate.originY=-50;
Tween.get(earthRotate, {loop: true}).to({rotateZ:-5}, 0, sineInOutEase).to({rotateZ:5}, 5000, sineInOutEase).to({rotateZ:-5}, 5000, sineInOutEase);
		

var dates=new Date();
var nowYear=dates.getFullYear();
var nowMoth=dates.getMonth()+1;
var nowYearLastOne=nowYear;
var  nowMothLastOne=nowMoth-1;
if(nowMothLastOne===0){
	nowYearLastOne=nowYear-1;
	nowMothLastOne=12;
}
var nowYearLastTwo=nowYearLastOne;
var nowYearLastThree=nowYearLastTwo;
var nowMothLastTwo=nowMothLastOne-1;
console.log(nowMothLastTwo);
var nowMothLastThree=nowMothLastTwo-1;
console.log(nowMothLastThree);
if(nowMothLastTwo===0){
	nowYearLastTwo=nowYearLastOne-1;
	nowMothLastTwo=12;
}
if(nowYearLastThree===0){
	nowYearLastThree=nowYearLastTwo-1;
	nowMothLastThree=12;
}
if(nowMothLastThree<10){
	nowMothLastThree="0"+nowMothLastThree;
}
if(nowMothLastTwo<10){
	nowMothLastTwo="0"+nowMothLastTwo;
}
if(nowMothLastOne<10){
	nowMothLastOne="0"+nowMothLastOne;
}
$("#nowMothDate").html(nowYearLastOne+"/"+nowMothLastOne);
$("#lastMothDate").html(nowYearLastTwo+"/"+nowMothLastTwo);
$("#lastThreeMothDate").html(nowYearLastThree+"/"+nowMothLastThree);
var lastOneMonth=nowMoth-1;
if(lastOneMonth===0){
	lastOneMonth=12;
}
var lastTowMonth=lastOneMonth-1;
if(lastTowMonth===0){
	lastTowMonth=12;
}
var lastthreeMonth=lastTowMonth-1;
if(lastthreeMonth===0){
	lastthreeMonth=12;
}
$("#lastOneMonth").html(lastOneMonth);
$("#lastTwoMonth").html(lastTowMonth);
$("#lastThreeMonth").html(lastthreeMonth);

var numScroll=$(".mt-number-animate").html();
//var numLastThree=numScroll[numScroll.length-3];
//numLastThree.height(124).width(5);

		window.onload=function() {
		    initBaseInfo();
		    document.body.addEventListener('touchstart', swiperF.onTouchStart);
		    document.body.addEventListener('touchmove', swiperF.onTouchMove)
		    document.body.addEventListener('touchend', swiperF.onTouchEnd)
		    document.body.addEventListener('keydown', keyUp);
		    InitBaseInfo();
		    dataChange();
		    myChart.setOption(option);
		    window.addEventListener('resize',function(){
		    	myChart.setOption(option);
			    myChart.resize(); 
			    });
		};
		var explorer =navigator.userAgent ;
 if (explorer.indexOf("Firefox") >= 0) {
	$(".cover").css("margin-top","7%");
}
//Chrome
else if(explorer.indexOf("Chrome") >= 0){
	$(".cover").css("margin-top","31%");
}