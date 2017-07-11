var cutStr = function(str) {
            return str.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
        }

function InitBaseInfo() {
    $.ajax({
        url: '/?method=BigDataBO.GetTopChannelMoney',
        type: 'POST',
        dataType: 'json',
        data: "parameter=7",
        success: function(json) {
            if (json.success) {
                var items = json.data.channelList;
                var topitems = items.slice(0, 3);
                var tophtml = "";
                var bottomhtml = "";
                $.each(topitems, function(i, topitem) {
                    tophtml += " <div class=\"same top" + (i + 1) + "\"><!--点-线 --><div class=\"dolt-line floatL\"><div class=\"num\">¥" + cutStr(topitem.Value) + "</div></div><div class=\"name floatL\">" + topitem.Name + "</div></div>";
                });
                $("#divTop").html(tophtml);
                if (items.length > 3) {
                    var bottomitems = items.slice(3, items.length);
                    $.each(bottomitems, function(j, bottomitem) {
                        bottomhtml += "<div class=\"same bottom" + (j + 1) + "\"><!-- 点-线 --><div class=\"dolt-line floatL\"><div class=\"num\"></div></div><div class=\"name floatL\">" + bottomitem.Name + "</div></div>";
                    });
                    $("#divBottom").html(bottomhtml);
                }
            } else {
                alert(json.message);
            }
        }
    });
}
setInterval('InitBaseInfo()', 60 * 1000);
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
        window.location.href = "user.aspx";
    } else if (keycode == 37) {
        window.location.href = "component.aspx";

    }
}
$(document).ready(function() {
    InitBaseInfo();
    document.body.addEventListener('touchstart', swiperF.onTouchStart);
    document.body.addEventListener('touchmove', swiperF.onTouchMove);
    document.body.addEventListener('touchend', swiperF.onTouchEnd);
    document.body.addEventListener('keydown', keyUp);
});