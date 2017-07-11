 var myChart = echarts.init(document.getElementById('main'), 'shine');
 //本demo中图例搜集于官网文档和demo,本站demo,以及网上各种图形
 var iconArray = [
     //  'image://images/user_top.png'									//实心圆
 ];
 //获取当前时刻所有读者的地理坐标
 // var data = [];
 var user_top = [
     //	who:"小明",behaver:"浏览了",whichBook:"《皇帝》"
     //"小明  浏览了 《皇帝》"，"小刚  购买了 《我的一家》"
 ];
 //进入页面当前时刻第一名的地理信息
 var data2 = [];
 //动态获取第一名地理信息及浏览行为信息
 var virsualBox = [];
 var virsualBox_max = null;
 //实时获取当前地理所有坐标集合
 var geo = [];
 //实时获取当前去掉重复的坐标信息
 var result = [];
 var nowLiveProvince = [];
 var option = {
     tooltip: {},
     visualMap: {
         min: 0,
         max: 0,
         left: '10%',
         bottom: '10%',
         text: ['High', 'Low'],
         seriesIndex: [4],
         calculable: true,
         show: true,
         inRange: {
             color: ['#ffdddd', '#fd5454']
         },

     },
     geo: {
         map: 'china',
         roam: true,
         label: {
             normal: {
                 show: true,
                 textStyle: {
                     color: 'rgba(0,0,0,0.4)'
                 }
             }
         },
         itemStyle: {
             normal: {
                 borderColor: 'rgba(0, 0, 0, 0.2)'
             },
             emphasis: {
                 show: false
             }
         }
     },
     series: [
         //除了第一名之外的读者的地理坐标
         {
             type: 'scatter',
             coordinateSystem: 'geo',
             tooltip: {
                 trigger: 'item',
                 formatter: "{b}"
             },
             data: [],
             symbol: 'image://images/position_pic.png',
             symbolSize: [15, 15],
             symbolOffset: [0, -8],
             label: {
                 normal: {
                     formatter: '{b}',
                     position: 'right',
                     show: false
                 },
                 emphasis: {
                     show: true
                 }
             },
             itemStyle: {
                 normal: {
                     color: '#F06C00'
                 }
             }
         },
         //当前时刻第一名的头像
         {
             type: 'scatter',
             coordinateSystem: 'geo',
             data: [],
             symbolSize: [24, 22],
             symbol: "",
             symbolOffset: [0, -29],
             label: {
                 normal: {
                     formatter: '{b}',
                     position: 'right',
                     show: false
                 },
                 emphasis: {
                     show: true
                 }
             },
             itemStyle: {
                 normal: {
                     color: '#F06C00'
                 }
             },
             zlevel: 19
         },
         //当前时刻第一名的地理坐标
         {
             type: 'scatter',
             coordinateSystem: 'geo',
             data: [],
             symbol: 'path://M18.000,48.000 C18.000,48.000 -0.000,28.936 -0.000,18.000 C-0.000,7.504 8.058,0.001 18.000,0.001 C27.943,0.001 36.000,7.505 36.000,18.000 C36.000,28.919 18.000,48.000 18.000,48.000 ZM18.000,6.991 C11.920,6.991 6.990,11.916 6.990,17.991 C6.990,24.066 11.920,28.991 18.000,28.991 C24.081,28.991 29.010,24.066 29.010,17.991 C29.010,11.916 24.081,6.991 18.000,6.991 Z',
             symbolSize: [40, 46],
             symbolOffset: [0, -23],
             label: {
                 normal: {
                     formatter: '{b}',
                     position: 'right',
                     show: false
                 },
                 emphasis: {
                     show: true
                 }
             },
             itemStyle: {
                 normal: {
                     color: 'rgba(64,200,223,1)'
                 }
             },
             zlevel: 20
         },
         //当前时刻第一名的相关信息
         {
             type: 'scatter',
             coordinateSystem: 'geo',
             data: [],
             symbolSize: [],
             symbol: 'rect',
             symbolOffset: [],
             label: {
                 normal: {
                     formatter: "",
                     position: 'inside',
                     show: true
                 },
                 emphasis: {
                     show: true
                 }
             },
             itemStyle: {
                 normal: {
                     color: 'rgba(0,0,0,0.3)'
                 }
             }
         },
         //当前时刻  全国读者行为  热力图
         {
             name: '全国各省读者分布情况',
             type: 'map',
             geoIndex: 0,
             legendHoverLink: false,
             data: [],
             itemStyle: {
                 normal: {
                     areaColor: 'transparent'
                 },
                 emphasis: {
                     areaColor: 'transparent',
                     label: {
                         shadowColor: 'transparent', //默认透明
                         shadowBlur: 10,
                         show: true
                     }
                 }
             }
         }
     ]
 };
 myChart.setOption(option);
 function GetRequest(value) {
     var url = decodeURI(location.search); //?id="123456"&Name="bicycle";
     var object = {};
     if (url.indexOf("?") != -1) //url中存在问号，也就说有参数。  
     {
         var str = url.substr(1); //得到?后面的字符串
         var strs = str.split("&"); //将得到的参数分隔成数组[id="123456",Name="bicycle"];
         for (var i = 0; i < strs.length; i++) {　　　　　　　　
             object[strs[i].split("=")[0]] = strs[i].split("=")[1]　　　　　　
         }　　
     }
     return object[value];
 }
 var startY, startX, endX, endY;
 var swiperF = {
     onTouchStart: function (event) {
         var event = event || window.event;
         var touch = event.touches[0];
         startY = touch.pageY;
         startX = touch.pageX;
     },
     onTouchMove: function (event) {
         var event = event || window.event;
         var touch = event.touches[0];
         endX = touch.pageX - startX;
         endY = touch.pageY - startY;
     },
     onTouchEnd: function () {
         if (endX < 0) {
             window.location.href = "index.aspx"
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
         window.location.href = "index.aspx";
     } else if (keycode == 37) {
         window.location.href = "book.aspx";
     }
 }
 $(document).ready(function () {
     Init();
     dataBase();
     sync();
     document.body.addEventListener('touchstart', swiperF.onTouchStart);
     document.body.addEventListener('touchmove', swiperF.onTouchMove)
     document.body.addEventListener('touchend', swiperF.onTouchEnd)
     document.body.addEventListener('keydown', keyUp);
 });
 var cutStr = function (str) {
     return str.split('').reverse().join('').replace(/(\d{3})/g, '$1,').replace(/\,$/, '').split('').reverse().join('');
 }
 function GetRequest(value) {
     //url例子：www.bicycle.com?id="123456"&Name="bicycle"；  
     var url = decodeURI(location.search); //?id="123456"&Name="bicycle";
     var object = {};
     if (url.indexOf("?") != -1) //url中存在问号，也就说有参数。  
     {
         var str = url.substr(1); //得到?后面的字符串
         var strs = str.split("&"); //将得到的参数分隔成数组[id="123456",Name="bicycle"];
         for (var i = 0; i < strs.length; i++) {　　　　　　　　
             object[strs[i].split("=")[0]] = strs[i].split("=")[1]　　　　　　
         }　　
     }
     return object[value];
 }
 function decimal(value) {
     var value = value + "";
     return value.split(".")[1];
 }
 var isbn = GetRequest("isbn");
 function Init() {
     //获取书籍基本信息
     $.ajax({
         url: '/?method=BigDataBO.GetBookInfoByISBN',
         type: 'POST',
         dataType: 'json',
         data: {
             isbn: isbn
         },
         success: function (json) {
             if (json.success) {

                 $('#book_pic').attr("src", json.data.book_pic);
                 $('#book_pic').attr("title", json.data.book_name);
                 $('#bookName').html(json.data.book_name);
                 $('#isbn').html(json.data.isbn);
                 $('#book_press').html(json.data.publishing_house);
                 $('#RQ_num').html(json.data.ticket_count);
                 $('#resource_num').html(json.data.seed_count);
                 //              	$('#app_num').html(json.data.app_count);
                 $('#sync_RQPic').attr("src", json.data.ticket);
             }
         },
         error: function (xhr, textStatus) {}
     });

     //获取书籍统计信息
     $.ajax({
         url: '/?method=BigDataBO.GetBookSts',
         type: 'POST',
         dataType: 'json',
         data: {
             isbn: isbn
         },
         success: function (json) {
             if (json.success) {
                 var total_money = cutStr(parseInt(json.data.total_money) + " ");
                 $('#pay').html(total_money);
                 $('#decimal').html(decimal(json.data.total_money + " "));
                 $('#brower').html(cutStr(json.data.browser_count + " "));
                 $('#readers').html(cutStr(json.data.user_count + " "));
             }
         },
         error: function (xhr, textStatus) {}
     });
     //根据ISBN返回此书籍读者分布
     $.ajax({
         url: '/?method=BigDataBO.GetBookUser',
         type: 'POST',
         dataType: 'json',
         data: {
             isbn: isbn
         },
         success: function (json) {
             if (json.success) {
                 nowLiveProvince = [];
                 virsualBox = [];
                 var provinceList = json.data;
                 for (var i = 0; i < provinceList.length; i++) {
                     var json1 = {};
                     json1.name = provinceList[i].province;
                     json1.value = provinceList[i].count;
                     nowLiveProvince.push(json1);
                     virsualBox.push(provinceList[i].count);
                 }

                 virsualBox_max = virsualBox[0];
             }


             myChart.setOption({
                 visualMap: {
                     max: virsualBox_max
                 },
                 series: [{},
                     {},
                     {},
                     {},
                     {
                         data: nowLiveProvince
                     }
                 ]
             });
         },
         error: function (xhr, textStatus) {}
     });

 }
 var count = null;
 function dataBase() {
     //根据ISBN返回此书籍最近N条实时行为动态数据
     $.ajax({
         url: '/?method=BigDataBO.GetBookActive',
         type: 'POST',
         dataType: 'json',
         data: {
             isbn: isbn
         },
         success: function (json) {
             if (json.success) {
                 iconArray = [];
                 result = [];
                 user_top = [];
                 data2 = [];
                 geo = [];
                 var cityList = json.data;
                 var province = null;
                 var city = null;
                 var nickname = null;
                 var behavior = null;
                 var lon = null;
                 var lat = null;
                 var behaviorRelationName = null;
                 for (let i = 0; i < cityList.length; i++) {
                     var arr_ll = [];
                     arr_ll.push(cityList[i].lon + "");
                     arr_ll.push(cityList[i].lat + "");
                     nickname = cityList[i].nickname;
                     behavior = cityList[i].behavior;
                     behaviorRelationName = cityList[i].behaviorRelationName;
                     var formatterStr = nickname + " " + behavior + "了" + " " + behaviorRelationName;
                     user_top.push(formatterStr);
                     geo.push(arr_ll);
                     iconArray.push("image://" + cityList[i].headImgUrl);
                 }
                 var hash = {};
                 for (var i = 0, len = geo.length; i < len; i++) {
                     result.push(geo[i]);
                 }
                 var readerNum_last = result.length - 1;
                 data2.push(result[readerNum_last]);
                 var strLength = user_top[readerNum_last].length;
                 var symbolX = (strLength + 10) * 10;
                 var symbolOffsetX = (parseInt(symbolX * 0.55) + 3);
                 count = user_top.length - 1;
                 myChart.setOption({
                     series: [
                         //当前时刻除了第一名的读者的标记分布情况
                         {},
                         //当前时刻第一名的头像
                         {
                             data: data2,
                             symbol: iconArray[readerNum_last]
                         },
                         //当前时刻第一名的地理坐标标记
                         {
                             data: data2
                         },
                         //当前时刻第一名的详细行为信息
                         {
                             data: data2,
                             symbolSize: [symbolX, 30],
                             symbolOffset: [symbolOffsetX, -23],
                             label: {
                                 normal: {
                                     formatter: user_top[readerNum_last]
                                 }
                             }
                         },
                         //当前时刻各省的热力情况
                         {}
                     ]
                 });
             }
         },
         error: function (xhr, textStatus) {}
     });
 }
 //动态获取第一名的地理信息和相关行为信息
 function sync() {
     console.log(user_top);
     var strLength = "";
     var tips = "";
     var readerPhoto = null;
     function dataSync() {
         data2 = [result[count]];
         console.log(result[count]);
         strLength = user_top[count].length;
         tips = user_top[count];
         readerPhoto = iconArray[count];
         var symbolX = (strLength + 10) * 10;
         var symbolOffsetX = (parseInt(symbolX * 0.55) + 3);
         myChart.setOption({
             series: [
                 //当前时刻除了第一名的读者的标记分布情况
                 {},
                 //当前时刻第一名的头像
                 {
                     data: data2,
                     symbol: readerPhoto
                 },
                 //当前时刻第一名的地理坐标标记
                 {
                     data: data2
                 },
                 //当前时刻第一名的详细行为信息
                 {
                     data: data2,
                     symbolSize: [symbolX, 30],
                     symbolOffset: [symbolOffsetX, -23],
                     label: {
                         normal: {
                             formatter: tips
                         }
                     }
                 },
                 //当前时刻各省的热力情况
                 {}
             ]
         });
         count--;
         if (count <= 0) {
             count = 0;
         }
     }
     var timer1 = setInterval(function () {
         if (user_top.length) {
             dataSync();
             if (count == 0) {
                 dataBase();
             }
         }
     }, 3000);
 }
 setInterval("Init()", 50000);
 myChart.on("mouseover", function () {
     myChart.dispatchAction({
         type: 'downplay'

     });
 });
 if (window.outerHeigth == window.screen.heigth && window.outerWidth == window.screen.width) {
     $(".book-pic").css("width", "52%");
     $(".RQ-pic").css("width", "33%");
 } else {
     $(".book-pic").css("width", "53.76%");
     $(".RQ-pic").css("width", "36.02%");
 }
 window.onresize = function () {
     setTimeout(function () {
         myChart.resize();
     }, 1000)
     $(".book-pic").css("width", "53.76%");
     $(".RQ-pic").css("width", "36.02%");
     window.location.reload();
 }