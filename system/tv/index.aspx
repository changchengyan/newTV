<!DOCTYPE html>
<html lang="en">

<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8">
    <title>数据总览</title>
    <link rel="stylesheet" href="css/comment.css">
    <link rel="stylesheet" href="css/index.css">
</head>

<body>
    <div class="brower-total">
        <div class="brower_star"></div>
        <div id="container" class="container">
            <div id="output"></div>
        </div>
        <div class="trideNumScroll"></div>
        <div class="showMove">
            <div class="star_upLeft">
                <div class='circle'>
                    <div class='circle1'>&nbsp;</div>
                    <div class='circle2'>&nbsp;</div>
                    <div class='circle3'>&nbsp;</div>
                    <div class='center'></div>
                </div>
            </div>
            <div class="star_upright">
                <div class='circle'>
                    <div class='circle1'>&nbsp;</div>
                    <div class='circle2'>&nbsp;</div>
                    <div class='circle3'>&nbsp;</div>
                    <div class='center'></div>
                </div>
            </div>
            <div class="star_middleRight">
            </div>
            <div class="bg_starBlink"></div>
            <div class="shootingStar">
            <div class="stars"></div>
            <div class="stars stars2"></div>
            <div class="stars stars3"></div>  
            <div class="stars stars4"></div>   
            </div>
            <div class="earthRotate">
            </div>
        </div>
        <div class="wrap-container">
            <div class="brower-title"></div>
            <div class="wrap">
                <div class="leftInfo">
                    <div class="cover">
                        <div class="left_pic">
                            <div class="data_astronaut_ball"></div>
                        </div>
                        <div class="context">
                            <!--<div class="top">
                                <span id="nowNum" class="number"></span>
                                <span>今日访问（人）</span>
                            </div>-->
                            <div class="bottom">
                                <span id="inLineNum" class="number"></span>
                                <span>实时在线（人）</span>
                            </div>
                        </div>
                    </div>
                    <div class="bot_userqulity">
                        <div class="man_pic container animated">
                            <div class="data_astronaut"></div>
                        </div>
                        <div class="user_quantityContext">
                            <span>90天新增用户(人)</span>
                            <span id="userNum"></span>
                        </div>
                    </div>
                </div>
                <div class="middleInfo">
                    <div class="tride-num">
                        90天交易总额:
                    </div>
                    <div class="data-chart" >
                    <div class="title">
                    	<span id="lastThreeMonth"></span>、
                    	<span id="lastTwoMonth"></span>、
                    	<span id="lastOneMonth"></span>
                    	月&nbsp;&nbsp;日流量趋势对比(pv)</div>
                        <div class="data-chartPic" >
                            <div class="main" id="main"></div>
                        </div>
                        <div class="red_fly"></div>
                        <div class="white_fly"></div>
                        <div class="yellow_fly"></div>
                        <div class="tips_2017">
                        <div class="nowMothDate" id="nowMothDate"></div>
                        </div>
                        <div class="tips_2016">
                        <div class="lastMothDate" id="lastMothDate"></div>     
                        </div>
                        <div class="tips_2016 lastLastMonth">
                        <div class="lastLastMothDate" id="lastThreeMothDate"></div>     
                        </div>
                    </div>
                </div>
                <div class="rightInfo">
                    <div class="rightWrap">
                        <div class="satelite_pic" id="satePic">
                        </div>
                        <div class="user_quantityContext">
                            <span>书籍总数(册)</span>
                            <span id="book_view"></span>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <%--<img src="images/index.png" style="width: 100%"/>--%>
        <script src="js/jquery.min.js"></script>
        <script src="js/numScrollTop.js"></script>
        <script src="js/echarts.min.js"></script>
        <script src="js/transform.js"></script>
        <script src="js/tween.js"></script>
        <script src="js/index.js"></script>
        <script src="js/comment.js"></script>
</body>

</html>