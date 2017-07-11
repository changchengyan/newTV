$(document).ready(function() {
	GetBookList();
	document.body.addEventListener('touchstart', swiperF.onTouchStart);
	document.body.addEventListener('touchmove', swiperF.onTouchMove)
	document.body.addEventListener('touchend', swiperF.onTouchEnd)
	document.body.addEventListener('keydown', keyUp);
});

var winWidth = null;
if(window.innerWidth) {
	winWidth = window.innerWidth;
} else if((document.body) && (document.body.clientWidth)) {
	winWidth = document.body.clientWidth;
}

function imposeStr(str) {
	var str = str + "";
	if(winWidth > 2400) {
		return str;
	} else {
		if(str.length > 9) {
			return str.substr(0, 9);
		}
	}
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
		if(endX < 0) {
			window.location.href = "index.aspx"
		} else {
			window.location.href = "realTimeBehavior.aspx"

		}
	}
}

function keyUp(e) {
	if(navigator.appName == "Microsoft Internet Explorer") {
		var keycode = event.keyCode;
		var realkey = String.fromCharCode(event.keyCode);
	} else {
		var keycode = e.which;
		var realkey = String.fromCharCode(e.which);
	}
	if(keycode == 38) {
		window.location.href = "realTimeBehavior.aspx";
	} else if(keycode == 40) {
		window.location.href = "index.aspx";
	}
}

function Int(value) {
	var value = value + "";
	return value.split(".")[0];
}

function decimal(value) {
	var value = value + "";
	return value.split(".")[1];
}

//判断字符串中字母数字空格及汉字个数，返回截取固定个数的字符串
function imposeStr(str) {
	var str = str + "";
	if(/[a-z0-9\s]/i.test(str)) {
		var naN = str.match(/[a-z0-9\s]/ig).length;
	}
	if(/[\u4E00-\u9FA5]/g.test(str)) {
		var chinese = str.match(/[\u4E00-\u9FA5]/g).length;
	}
	var strLength = parseInt(naN / 2) + chinese;
	if(str.length > strLength) {
		return str.substr(0, strLength);
	} else {
		return str;
	}
}
var index;
var url;

function GetBookList() {
	$.ajax({
		url: '/?method=BigDataBO.GetTopSaleBooks',
		type: 'POST',
		dataType: 'json',
		data: {
			num: 5
		},
		success: function(json) {
			if(json.success) {
				var htmlstr = "";
				$.each(json.data, function(i, item) {
					htmlstr += "<li id=\"p" + i + "\" tabindex=\"" + i + "\" class=\"txt\" data-base=\"" + item.isbn + "\"><div class=\"book_pic\"><div class=\"sync-bookPic\"><img src=\"" + item.book_pic + "\" title=\"" + item.book_name + "\" /></div></div>";
					htmlstr += "<div class=\"bookName\">" + item.book_name + "</div>";
					htmlstr += " <div class=\"bookPirce\">增收：￥<span class=\"price\">" + Int(item.total_money) + "</span><span class=\"decimal\">." + decimal(item.total_money) + "</span></div></li>";
				});
				$(".case-bookView").html(htmlstr);

				var inputs = $("li.txt")
				var divsLength = inputs.length;
				inputs[0].className = "sync";
				var count = 0;
				window.onkeydown = function(e) {
					e = e || window.event;
					if(e.keyCode == 39) {
						count++;

					} else if(e.keyCode == 37) {
						count--;

					} else if(e.keyCode == 13) {
						var index = inputs[count].getAttribute("data-base");
						url = "bookMap.aspx?isbn=" + index;
						window.location.href = url;
					}
					if(count > 4) {
						count = 4;
					} else if(count <= 0) {
						count = 0
					}
					console.log(count);
					for(var i = 0; i < divsLength; i++) {
						inputs[i].className = "txt";
					}
					inputs[count].className = "sync";
				}
				for(var i = 0; i < inputs.length; i++) {
					var ipts = inputs[i];
					ipts.addEventListener("click", function() {
						index = $(this).attr("data-base");
						url = "bookMap.aspx?isbn=" + index;
						window.location.href = url;
					});
				}
				//            $("#p0").on('click',function(){
				//		   		index=$(this).attr("data-base");
				//		   		url= "bookMap.aspx?isbn="+index;
				//		   		 window.location.href = url; 
				//			   });
				//			   $("#p1").on('click',function(){
				//			   		index=$(this).attr("data-base");
				//			   		url= "bookMap.aspx?isbn="+index;
				//			   		 window.location.href = url;
				//			   });
				//			   $("#p2").on('click',function(){
				//			   		index=$(this).attr("data-base");
				//			   		url= "bookMap.aspx?isbn="+index;
				//			   		 window.location.href = url;
				//			   });
				//			   $("#p3").on('click',function(){
				//			   		index=$(this).attr("data-base");
				//			   		url= "bookMap.aspx?isbn="+index;
				//			   		 window.location.href = url;
				//			   });
				//			   $("#p4").on('click',function(){
				//			   		index=$(this).attr("data-base");
				//			   		url= "bookMap.aspx?isbn="+index;
				//			   		 window.location.href = url;
				//			   });  
			}
			if(document.body.scrollHeight == window.screen.height && document.body.scrollWidth == window.screen.width) {
				$("#case_bookView li").css("width", "17.3%");
			} else {
				$("#case_bookView li").css("width", "15.6%");
			}
		},
		error: function(xhr, textStatus) {}
	});
}
var earth = document.querySelector(".bottom-earth");
Transform(earth);
var Tween = createjs.Tween,
	sineInOutEase = createjs.Ease.linear;
Tween.get(earth, {
	loop: true
}).to({
	translateX: 0
}, 0, sineInOutEase).to({
	translateX: -30
}, 3000, sineInOutEase).to({
	translateX: 0
}, 3000, sineInOutEase);
window.onresize = function() {
	$("#case_bookView li").css("width", "17.3%");
	window.location.reload();
}