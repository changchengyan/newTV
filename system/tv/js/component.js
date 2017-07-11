var cutStr = function(str) {
            return str.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
        }
function InitBaseInfo() {
    $.ajax({
        url: '/?method=BigDataBO.GetProfitReport',
                type: 'POST',
                dataType: 'json',
                success: function (json) {
                	console.log(json);
                    $("#adviserMoney").html("¥" + cutStr(json.money_data.adviserMoney + " "));
                    $("#merchantMoney").html("¥" + cutStr(json.money_data.merchantMoney + " "));
                    $("#coperNum").html(cutStr(json.money_data.cooperate_count + " "));
                    $("#press_num").html(json.money_data.spacePercent);
                    $("#reader_nums").html(json.money_data.merchantPercent);
                    $("#editer_nums").html(json.money_data.adviserPercent);
                    $("#runs_num").html(json.money_data.channelPercent);
                },
                error: function (xhr, textStatus) {

                }
    });
    $.ajax({
        url: '/?method=BigDataBO.GetRoleChart',
        type: 'POST',
        dataType: 'json',
        success: function(json) {
            $("#people1").html(json.people_data[0].value);
            $("#people2").html(json.people_data[1].value);
            $("#people3").html(json.people_data[2].value);
            $("#people4").html(json.people_data[3].value);
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
            window.location.href = "user.aspx"
        } else {
            window.location.href = "index.aspx"

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
        window.location.href = "user.aspx";
    } else if (keycode == 37) {
        window.location.href = "index.aspx";

    }
}

  var Tween = createjs.Tween,
    sineInOutEase = createjs.Ease.linear;
 var moon = document.getElementById("moon");
    Transform(moon);
 var Tween = createjs.Tween,
    sineInOutEase = createjs.Ease.linear;
Tween.get(moon, {loop: true}).to({rotateZ:-20}, 0, sineInOutEase).to({rotateZ:20}, 5000, sineInOutEase).to({rotateZ:-20}, 5000, sineInOutEase);
 var cbr = document.getElementById("cbr");
    Transform(cbr);
Tween.get(cbr, {loop: true}).to({rotateZ:-20}, 0, sineInOutEase).to({rotateZ:20}, 5000, sineInOutEase).to({rotateZ:-20}, 5000, sineInOutEase);
var sun = document.getElementById("conposition_sun");
    Transform(sun);
Tween.get(sun, {loop: true}).to({rotateZ:0},0, sineInOutEase).to({rotateZ:360}, 5000, sineInOutEase);
var earth = document.getElementById("conposition_earth");
    Transform(earth);
Tween.get(earth, {loop: true}).to({rotateZ:0},0, sineInOutEase).to({rotateZ:360}, 5000, sineInOutEase);
var Mars = document.getElementById("composition_Mars");
    Transform(Mars);
Tween.get(Mars, {loop: true}).to({rotateZ:0},0, sineInOutEase).to({rotateZ:360}, 5000, sineInOutEase);
var Mercury = document.getElementById("composition_Mercury");
    Transform(Mercury);
Tween.get(Mercury, {loop: true}).to({rotateZ:0},0, sineInOutEase).to({rotateZ:360}, 5000, sineInOutEase);
var salate = document.getElementById("salate");
    Transform(salate);
Tween.get(salate, {loop: true}).to({translateX:0},0, sineInOutEase).to({translateX:20}, 5000, sineInOutEase).to({translateX:0}, 5000, sineInOutEase);
var earth40 = document.getElementById("composition_Earth_40");
    Transform(earth40);
Tween.get(earth40, {loop: true}).to({translateX:0},0, sineInOutEase).to({translateX:-20}, 5000, sineInOutEase).to({translateX:0}, 5000, sineInOutEase);
var earth5 = document.getElementById("composition_Earth_5");
    Transform(earth5);
Tween.get(earth5, {loop: true}).to({translateY:0},0, sineInOutEase).to({translateY:-20}, 5000, sineInOutEase).to({translateY:0}, 5000, sineInOutEase);
var earth20 = document.getElementById("composition_Earth_20");
    Transform(earth20);
Tween.get(earth20, {loop: true}).to({translateX:0},0, sineInOutEase).to({translateX:20}, 5000, sineInOutEase).to({translateX:0}, 5000, sineInOutEase);
var earth25 = document.getElementById("composition_Earth_25");
    Transform(earth25);
Tween.get(earth25, {loop: true}).to({translateY:0},0, sineInOutEase).to({translateY:20}, 5000, sineInOutEase).to({translateY:0}, 5000, sineInOutEase);
 
window.onload=function (){
    InitBaseInfo();
    document.body.addEventListener('touchstart', swiperF.onTouchStart);
    document.body.addEventListener('touchmove', swiperF.onTouchMove)
    document.body.addEventListener('touchend', swiperF.onTouchEnd)
    document.body.addEventListener('keydown', keyUp)
};