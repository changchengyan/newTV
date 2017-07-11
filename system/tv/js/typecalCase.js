
var index; 
var url;
$(function(){ 
   $("#p1").on('click',function(){
   		index="1";
   		url= "bookMap.html?index="+index;
   		 window.location.href = url; 
   });
   $("#p2").on('click',function(){
   		index="2";
   		url= "bookMap.html?index="+index;
   		 window.location.href = url;
   });
   $("#p3").on('click',function(){
   		index="3";
   		url= "bookMap.html?index="+index;
   		 window.location.href = url;
   });
   $("#p4").on('click',function(){
   		index="4";
   		url= "bookMap.html?index="+index;
   		 window.location.href = url;
   });
   $("#p5").on('click',function(){
   		index="5";
   		url= "bookMap.html?index="+index;
   		 window.location.href = url;
   });
}); 
var earth = document.querySelector(".bottom-earth");
Transform(earth);
var Tween = createjs.Tween,
sineInOutEase = createjs.Ease.linear;
Tween.get(earth, {loop: true}).to({translateX:0}, 0, sineInOutEase).to({translateX:-30}, 3000, sineInOutEase).to({translateX:0}, 3000, sineInOutEase);