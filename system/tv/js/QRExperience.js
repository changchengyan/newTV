 var sizeChange = function(){
        $("body").height($(window).height());

        $(".QR-list li").height($(".QR-list li").width()*1.4)
        $(".imgspan").height($(".imgspan").width());
         $("#conetent").height($(window).height()-205)
    }
    sizeChange();
    function arrowChange(evt){
                    evt = (evt) ? evt : window.event;
                    if (evt.keyCode) {
                       if(evt.keyCode == 37){
                         evt.preventDefault;
                          window.location.href = "user.aspx"
                       }else if(evt.keyCode == 39) {
                         evt.preventDefault;
                         window.location.href = "realTimeBehavior.aspx"
                       }
                    }
                }
 var startY,startX,endX,endY;
         var swiperF = {
               onTouchStart: function(event){
                      var event = event||window.event;
                       var touch = event.touches[0];
                       startY = touch.pageY;
                       startX = touch.pageX;
                       console.log( event.targetTouches[0])
                   },
                   onTouchMove: function(event){
                     var event = event||window.event;
                       var touch = event.touches[0];
                       endX = touch.pageX-startX;
                       endY = touch.pageY-startY;
                   },
                   onTouchEnd: function(){
                   if(endX>100) {
                       window.location.href = "user.aspx"
                   }else if(endX<-100){
                       window.location.href = "realTimeBehavior.aspx"
                   }
                 }
                 }
         window.onload=function(){
          document.body.addEventListener("keydown",arrowChange);
          document.body.addEventListener('touchstart',swiperF.onTouchStart);
          document.body.addEventListener('touchmove',swiperF.onTouchMove);
          document.body.addEventListener('touchend',swiperF.onTouchEnd);
          window.addEventListener('resize',function(){
          	 sizeChange();
          });
         }
          