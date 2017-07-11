

var stars = document.querySelector(".stars");
    Transform(stars);
    var Tween = createjs.Tween,
	sineInOutEase = createjs.Ease.linear;
Tween.get(stars, {loop: true}).to({translateX:0}, 0, sineInOutEase).to({translateX:-700}, 5000, sineInOutEase).to({translateX:-800}, 700, sineInOutEase);
Tween.get(stars, {loop: true}).to({opacity:0}, 0, sineInOutEase).to({opacity:1}, 5000, sineInOutEase).to({opacity:0}, 700, sineInOutEase);
Tween.get(stars, {loop: true}).to({translateY:0}, 0, sineInOutEase).to({translateY:700}, 5000, sineInOutEase).to({translateY:800}, 700, sineInOutEase);

var stars2 = document.querySelector(".stars2");
    Transform(stars2);
Tween.get(stars2, {loop: true}).to({translateX:0},4000, sineInOutEase).to({translateX:-700}, 5000, sineInOutEase).to({translateX:-800}, 700, sineInOutEase);
Tween.get(stars2, {loop: true}).to({opacity:0}, 4000, sineInOutEase).to({opacity:1}, 5000, sineInOutEase).to({opacity:0}, 700, sineInOutEase);
Tween.get(stars2, {loop: true}).to({translateY:0}, 4000, sineInOutEase).to({translateY:700}, 5000, sineInOutEase).to({translateY:800}, 700, sineInOutEase);

var stars3 = document.querySelector(".stars3");
    Transform(stars3);
Tween.get(stars3, {loop: true}).to({translateX:0},4000, sineInOutEase).to({translateX:-700}, 5000, sineInOutEase).to({translateX:-800}, 700, sineInOutEase);
Tween.get(stars3, {loop: true}).to({opacity:0}, 4000, sineInOutEase).to({opacity:1}, 5000, sineInOutEase).to({opacity:0}, 700, sineInOutEase);
Tween.get(stars3, {loop: true}).to({translateY:0}, 4000, sineInOutEase).to({translateY:700}, 5000, sineInOutEase).to({translateY:800}, 700, sineInOutEase);

var stars4 = document.querySelector(".stars4");
    Transform(stars4);
Tween.get(stars4, {loop: true}).to({translateX:0},0, sineInOutEase).to({translateX:-700}, 5000, sineInOutEase).to({translateX:-800}, 700, sineInOutEase);
Tween.get(stars4, {loop: true}).to({opacity:0}, 0, sineInOutEase).to({opacity:1}, 5000, sineInOutEase).to({opacity:0}, 5000, sineInOutEase);
Tween.get(stars4, {loop: true}).to({translateY:0}, 0, sineInOutEase).to({translateY:700}, 5000, sineInOutEase).to({translateY:800}, 700, sineInOutEase);

var brower_star = document.querySelector(".brower_star");
    Transform(brower_star);
Tween.get(brower_star, {loop: true}).to({translateX:0}, 0, sineInOutEase).to({translateX:-500}, 60000, sineInOutEase).to({translateX:0}, 60000, sineInOutEase);
