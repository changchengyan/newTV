
 var data1=[],data2=[],data3=[],payUserPercent,noPay;
var cutStr = function(str) {
            return str.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
        }
var sexChart = echarts.init(document.getElementById('sex_chart'));
 var option1 = {
    grid: {
        left:'-24%',
        y: '-20%',
        y2: '1%',
    },
    xAxis: [
        {
            type: 'category',
            data: [],
             show:false
        }
    ],
    yAxis: [
        {
            show:false
        }
    ],
    series: [
        {
            name:'man',
            type:'bar',
            stack:'xxx',
            barWidth:"29.14%",
            itemStyle: {
              normal: {
                  color: '#2cd9f3',
              }  
           },
            data: []
        },
        {
            name:'women',
            type:'bar',
            stack:'xxx',
            barWidth:"29.14%",
            itemStyle: {
              normal: {
                  color: '#f400e4'
              }  
            },
            data:[]
        },
        {
            name:'other',
            type:'bar',
            stack:'xxx',
            barWidth:"29.14%",
            itemStyle: {
              normal: {
                  color: '#e56c0a'
              }  
            },
            data:[]
        }
    ]
};

var xiaoFeiChart = echarts.init(document.getElementById('xiaofei_canvas'));
 var option2 = {
        tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b}: {c} ({d}%)"
    },
    series: [
        {
            name:'Poor households - housing characteristic',
            type:'pie',
            radius: ['40%', '55%'],
            avoidLabelOverlap: false,
            label: {
                normal: {
                    show: false,
                    position: 'center'
                },
                emphasis: {
                    show: true,
                    textStyle: {
                        fontSize: '30',
                        fontWeight: 'bold'
                    }
                }
            },
            labelLine: {
                normal: {
                    show: false
                }
            },
            data:[
            ]
        }
    ],
    color:['#ffab1b','transparent']
};

function InitBaseInfo() {
//  $.ajax({
//      url: '/?method=BigDataBO.GetMapChart',
//      type: 'POST',
//      dataType: 'json',
//      success: function(json) {
//          $("#top1").html(json.cityList[0].name);
//          $("#top2").html(json.cityList[1].name);
//          $("#top3").html(json.cityList[2].name);
//      }
//  });
    $.ajax({
        url: '/?method=BigDataBO.GetSexChart',
        type: 'POST',
        dataType: 'json',
        success: function(json) {
            $("#male").html(json.sexList[0].percent);
            $("#fMale").html(json.sexList[1].percent);
            $("#other").html(json.sexList[2].percent);
            data1[0]=parseFloat(json.sexList[0].percent);
            data2[1]=parseFloat(json.sexList[1].percent);
            data3[2]=parseFloat(json.sexList[2].percent);
             sexChart.setOption({
                series: [{
                    data:[data3[2]]
                },{
                    data:[data2[1]]
                    
                },{
                    data:[data1[0]]
                   
                }]
            });
        }
    });

    $.ajax({
        url: '/?method=BigDataBO.GetUserBaseReport',
        type: 'POST',
        dataType: 'json',
        success: function(json) {
            $("#activeUserNum").html(cutStr(parseInt(json.active_user) + " "));
            $("#payUserPercent").html(json.pay_user_percent);
             payUserPercent= parseFloat(json.pay_user_percent);
            noPay=100-payUserPercent;
//          $("#pay").html(payUserPercent+'%');
//			$("#noPay").html(noPay+"%");
            xiaoFeiChart.setOption({
                series: [{
                    data:[{value:payUserPercent, name:payUserPercent+'%'},
                {value:noPay, name:noPay+"%"}]
                }]
            });
        }
    });

}
setInterval('InitBaseInfo()', 60 * 1000);

function ShowTotalMoney(totalmoney) {
               var totalMoney=totalmoney;
            var numScroll = $("#userCount").numberAnimate({
                num: totalMoney,
                speed: 1000,
                symbol: ","
            });
            setInterval(function() {
                $.ajax({
                    url: '/?method=BigDataBO.GetUserBaseReport',
                    type: 'POST',
                    dataType: 'json',
                    success: function(json) {
                        numScroll.resetData(json.user_count);
                    }
                });;
            }, 10 * 1000);
        }
function initBaseInfo() {
    $.ajax({
        url: '/?method=BigDataBO.GetUserBaseReport',
        type: 'POST',
        dataType: 'json',
        success: function(json) {
            ShowTotalMoney(json.user_count);
        },
        error: function(xhr, textStatus) {}
    });
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
            window.location.href = "QRExperience.aspx"
        } else {
            window.location.href = "component.aspx"

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
        window.location.href = "QRExperience.aspx";
    } else if (keycode == 37) {
        window.location.href = "component.aspx";
    }
}
var user_ball = document.querySelector("#user_ball");
Transform(user_ball);
var Tween = createjs.Tween,
sineInOutEase = createjs.Ease.linear;
Tween.get(user_ball, {loop: true}).to({translateX:0}, 800, sineInOutEase).to({translateX:40}, 3000, sineInOutEase).to({translateX:0}, 3000, sineInOutEase);
window.onload=function() {
    InitBaseInfo();
    initBaseInfo();
    document.body.addEventListener('touchstart', swiperF.onTouchStart);
    document.body.addEventListener('touchmove', swiperF.onTouchMove)
    document.body.addEventListener('touchend', swiperF.onTouchEnd)
    document.body.addEventListener('keydown', keyUp);
    sexChart.setOption(option1);
    xiaoFeiChart.setOption(option2);
    window.addEventListener('resize',function(){
    sexChart.resize(); 
    xiaoFeiChart.resize(); 
    });
};