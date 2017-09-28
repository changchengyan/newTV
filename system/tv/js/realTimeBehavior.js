var sizeChange = function() {
            $(".content-all").height($(window).height()-$(".realTimeBehavior-title").height()-20);
            $(".behavior-list").height($(".content-left").height()-$(".content-left h4").height())
            $(".user-list").height($(".right-item").height()-$(".right-item h4").height())
        }

var fisrtFiveshow ={
    behaviorArray :new Array(),
    subscribeArray:new Array(),
    scanArray : new Array(),
    payArray : new Array(),
    getFirstRealBehavior:function() {
         $.get("http://api.chubanyun.net/exapi/v1.0/tv/getGeneralData?count=10", {}, function (json,status) {
                        if(status != "success") {
                            alert(status)
                         }
                        if (json.errCode == 0 && json.data.length>0) {
                            var data = json.data;
                            fisrtFiveshow.behaviorArray = json.data;
                            var dataTime,
                            fullYear,fullTime;
                             var imgsrc = "images/tx1.png";
                               for (var i = data.length - 1; i >= 0; i--) {

                                   if (data[i].headImgUrl !== "") {
                                       imgsrc = data[i].headImgUrl;
                                   }else {
                                       imgsrc = "images/tx1.png";
                                   }
                                 dataTime = data[i].behaviorTime;
                                 
                                if(dataTime.indexOf(".") !== -1) {
                                    dataTime = dataTime.slice(0,length-2)
                                }
                                	fullYear=dataTime.slice(0,10);
                                    fullTime=dataTime.slice(11,20);

                                var newItem = $(".item-behavior").eq(0).clone();
                                newItem.find(".user-time").html("<span class='fullYear'>"+fullYear+"</span><br><span class='fullTime'>"+fullTime+"</span>").end()
                                       .find(".user-img img").attr("src", imgsrc).end()
                                       .find(".desc-word p").html(data[i].nickname + data[i].behavior + data[i].behaviorRelationName).end()
                                       .find(".behavior-title").html(data[i].spaceName);
                                $(".behavior-list .list-box").append(newItem);
                            }
                            $(".item-behavior").eq(0).remove();

                        }
                        else {
                            alert(json.message);
                        }
                    });
    },
     getFirstSubscribeUser : function (url) {
                $.get(url, {}, function (json,status) {
                 if(status != "success") {
                    alert(status)
                 }
                    if (json.errCode == 0 &&  json.data.length>0) {
                        fisrtFiveshow.subscribeArray = json.data;
                        changeInfo.newUserItem(0, fisrtFiveshow.subscribeArray);
                    }
                    else {
                        alert(json.message);
                    }
                });
            },
      getFirstScanUserItem : function (url) {
                 $.get(url, {}, function (json,status) {
                      if(status != "success") {
                         alert(status)
                      }
                     if (json.errCode == 0) {
                         fisrtFiveshow.scanArray = json.data;
                         changeInfo.newUserItem(1, fisrtFiveshow.scanArray);
                     }
                     else {
                         alert(json.message);
                     }
                 });
             },
           getFirstPayUserItem :function (url) {
                    $.get(url, {}, function (json,status) {
                     if(status != "success") {
                        alert(status)
                     }
                        if (json.errCode == 0 &&  json.data.length>0) {
                            fisrtFiveshow.payArray = json.data;
                            changeInfo.newUserItem(2, fisrtFiveshow.payArray);
                        }
                        else {
                            alert(json.message);
                        }
                    });
                },
        getScanUserItem: function (url, timeField) {
               $.get(url, {}, function (json,status) {
                   if(status != "success") {
                       alert(status)
                   }
                   if (json.errCode == 0 &&  json.data.length>0) {
                       var newArray = json.data;
                       var data = getNewItems(fisrtFiveshow.scanArray, newArray, timeField);
                       changeInfo.newUserItem(1, data);
                       fisrtFiveshow.scanArray = newArray;
                   }
                   else {
                       alert(json.message);
                   }
               });
           },
     getPayUserItem: function (url, timeField) {
                $.get(url, {}, function (json,status) {
                     if(status != "success") {
                        alert(status)
                    }
                    if (json.errCode == 0 &&  json.data.length>0) {
                        var newArray = json.data;
                        var data = getNewItems(fisrtFiveshow.payArray, newArray, timeField);
                        changeInfo.newUserItem(2, data);
                        fisrtFiveshow.payArray = newArray;
                    }
                    else {
                        alert(json.message);
                    }
                });
            },
            getSubscribeUserItem : function (url, timeField) {
                        $.get(url, {}, function (json,status) {
                             if(status != "success") {
                                alert(status)
                            }
                            if (json.errCode == 0 &&  json.data.length>0) {
                                var newArray = json.data;
                                var data = getNewItems(fisrtFiveshow.subscribeArray, newArray, timeField);
                                changeInfo.newUserItem(0, data);
                                fisrtFiveshow.subscribeArray = newArray;
                            }
                            else {
                                alert(json.message);
                            }
                        });
                    }
}

        //获取界面上需要刷新的数据数组
        var getNewItems = function (oldArray, newArray,timeField) {
            var array = new Array();
            var index = 0;
            for (var i = newArray.length-1; i >=0; i--) {
                if ((new Date(newArray[i][timeField])) > (new Date(oldArray[0][timeField]))) {
                    break;
                } else {
                    index++;
                }
            }

            for (var i = 0; i < (newArray.length - index); i++) {
                array.push(newArray[i]);
            }
            return array;
            
        }
        changeInfo = {
          "start":0,
          newBuy: function () { //更新实时行为
              $.get("http://api.chubanyun.net/exapi/v1.0/tv/getGeneralData?count=10", {},
                  function (json,status) {
                   if(status != "success") {
                      alert(status)
                  }
                  var lastNewInfo =$(".behavior-list .item-behavior").eq(0).text().trim();
                      //console.log(lastNewInfo);
                      //console.log(json.data[0].behaviorTime.trim())

                      if (json.errCode == 0 &&  json.data.length>0) {
                          var newArray = json.data;
                          var data = getNewItems(fisrtFiveshow.behaviorArray, newArray, "behaviorTime");
                           var dataTime,
                            fullYear,fullTime;
                            var imgsrc = "images/tx1.png";
                          for (var i = data.length - 1; i >= 0; i--) {

                               if (data[i].headImgUrl !== "") {
                                     imgsrc = data[i].headImgUrl;
                                 }else {
                                     imgsrc = "images/tx1.png";
                                 }
                               dataTime = data[i].behaviorTime;
                                if(dataTime.indexOf(".") !== -1) {
                                    dataTime = dataTime.slice(0,length-2)
                                }
                                fullYear=dataTime.slice(0,10);
                                    fullTime=dataTime.slice(11,20);
                                var newItem = $(".item-behavior").eq(0).clone();
                                newItem.find(".user-time").html("<span class='fullYear'>"+fullYear+"</span><br><span class='fullTime'>"+fullTime+"</span>").end()
                                       .find(".user-img img").attr("src", imgsrc).end()
                                       .find(".desc-word p").html(data[i].nickname + data[i].behavior + data[i].behaviorRelationName).end()
                                       .find(".behavior-title").html(data[i].spaceName);
                                $(".behavior-list .list-box").append(newItem);

                                $(".behavior-list .list-box .item-behavior").eq(0).remove();

                                var newY;
                                changeInfo.start = changeInfo.start + newItem.outerHeight();
                                newY = -changeInfo.start;
                                //$(".behavior-list .list-box").css({ "margin-top": newY + "px" })
                            }
                          //$(".behavior-list  .list-box").append($(".item-behavior").eq(0))
                          fisrtFiveshow.behaviorArray = newArray;

                        }
                        else {
                            alert(json.message);
                        }

                    }
            );
          },
          newUserItem:function(listIndex,data) //更新关注、扫码、支付用户
          {
              for (var i = data.length - 1; i >= 0 ; i--) {
                  var imgsrc = "images/tx1.png";
                  if (data[i].headImgUrl !== "") {
                      imgsrc = data[i].headImgUrl;
                  }
                  var whichfirst = $(".user-list").eq(listIndex).find(".user-item").eq(0);
                  var newItem = "";
                   newItem = '<span class="user-item '+ i + data[i].nickname+' "><img class="item-img" src="'+ imgsrc +'"  onerror="imgChange($(this))"/><span class="user-name a-line">'+data[i].nickname+'</span></span>';
//                  newItem.find(".user-name").html(data[i].nickname).end()
//                  .find(".item-img").attr("src", imgsrc);

                  whichfirst.before(newItem);
                    $(".user-list").eq(listIndex).find(".user-item").eq(0).find("img").delay(500).animate({ "width": 5.5 + 'rem', "height": 5.5 + 'rem' });
                    $(".user-list").eq(listIndex).find(".user-item").eq(0).find("img").delay(500).animate({ "width": 4.5 + 'rem', "height": 4.5 + 'rem' }, 500);
                  $(".user-list").eq(listIndex).find(".user-item").eq(5).remove();
                  newItem="";
              }
              
          }

        }

        setInterval("changeInfo.newBuy()", 5000)
        setInterval("fisrtFiveshow.getPayUserItem('http://api.chubanyun.net/exapi/v1.0/tv/getNewOrder?count=5','payTime')", 5000);
        setInterval("fisrtFiveshow.getScanUserItem('http://api.chubanyun.net/exapi/v1.0/tv/getNewScan?count=5','scanTime')", 5000);
        setInterval("fisrtFiveshow.getSubscribeUserItem('http://api.chubanyun.net/exapi/v1.0/tv/getNewUser?count=5','subscribeTime')", 5000)
    function imgChange(whichImg) {
      whichImg.attr("src", "images/tx1.png")
     }
           function arrowChange(evt) {
                      evt = (evt) ? evt : window.event;
                      if (evt.keyCode) {
                          if (evt.keyCode == 37) {
                              evt.preventDefault;
                              window.location.href = "QRExperience.aspx"
                          } else if (evt.keyCode == 39) {
                              evt.preventDefault;

                              window.location.href = "book.aspx"
                              // location.reload()
                          }
                      }
                  }

            //滑动切换
            var startY,startX,endX,endY;
            var swiperF = {
                  onTouchStart: function(event){

                         var event = event||window.event;
                          var touch = event.touches[0];
                          startY = touch.pageY;
                          startX = touch.pageX;
                          //console.log( event.targetTouches[0])
                      },
                      onTouchMove: function(event){
                        var event = event||window.event;
                          var touch = event.touches[0];
                          endX = touch.pageX-startX;
                          endY = touch.pageY-startY;
                      },
                      onTouchEnd: function(){
                       //console.log(endX)
                      if(endX>100) {
                          window.location.href = "QRExperience.aspx"
                      }else if(endX<-100){
                          window.location.href = "book.aspx"
                      }
                    }
                    }
             
             


window.onload=function(){
    document.body.addEventListener("keydown", arrowChange);
	document.body.addEventListener('touchstart',swiperF.onTouchStart);
    document.body.addEventListener('touchmove',swiperF.onTouchMove);
    document.body.addEventListener('touchend',swiperF.onTouchEnd);
    sizeChange();
    //首次获取五条实时行为
    fisrtFiveshow.getFirstRealBehavior();
    //首次获取五条关注用户
    fisrtFiveshow.getFirstSubscribeUser("http://api.chubanyun.net/exapi/v1.0/tv/getNewUser?count=5");
    //首次获取五条扫码用户
    fisrtFiveshow.getFirstScanUserItem("http://api.chubanyun.net/exapi/v1.0/tv/getNewScan?count=5");
    //首次获取五条支付用户
    fisrtFiveshow.getFirstPayUserItem("http://api.chubanyun.net/exapi/v1.0/tv/getNewOrder?count=5");
    window.addEventListener("resize",function(){
    	sizeChange();
    });
}
$(".list-box").scroll(function(){console.log("1")})
$(".list-box").animate(
    function(){
        $(this).css({"marginTop":"-50%"})
    },20000
)
//function myFresh(){
//    window.location.reload();
//}

/*
setInterval(function(){
    myFresh();
},60*60*1000);*/
