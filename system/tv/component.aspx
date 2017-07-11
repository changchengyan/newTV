<!DOCTYPE html>
<html lang="en">

<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8">
    <title>出版收益结构</title>
    <link rel="stylesheet" href="css/comment.css">
    <link rel="stylesheet" href="css/composition.css">
</head>

<body>
    <div class="brower_star"></div>
    <div id="container1" class="container">
        <div id="output1"></div>
    </div>
    <div class="composition-title"></div>
    <div class="stone"></div>
    <div class="shootingStar">
       <div class="stars"></div>
            <div class="stars stars2"></div>
            <div class="stars stars3"></div>  
            <div class="stars stars4"></div>   
    </div>
                        <div class="earth-tips"></div>
    
    <div class="superWrap">
        <div class="wrap">
            <div class="wrap-left">
                <!--出版收益-->
                <div class="composition-publisher-profit">
                    <div class="moon" id="moon"></div>
                    <div class="opacity-box">
                        <span class="title-earn">编辑收益</span>
                        <span class="num-earn" id="adviserMoney"></span>
                    </div>
                </div>
                <!-- 出版收益end-->
                <!-- 出版人员构成-->
                <div class="publisher-constitute">
                    <h5>出版人员构成</h5>
                    <div class="through-line"></div>
                    <div class="blue-opacity-box">
                        <ul class="publisher-type">
                            <li class="first">
                                <div class="cbr" id="cbr"></div>
                                <p class="title-publisher" >rays出版人</p>
                                <p class="num-publisher" id="coperNum"></p>
                            </li>
                            <li>
                                <div class="bj">
                                    <div class="conposition_sun" id="conposition_sun"></div>
                                </div>
                                <p class="title-publisher">编辑</p>
                                <p class="num-publisher" id="people1"></p>
                            </li>
                            <li>
                                <div class="bj earth">
                                    <div class="conposition_earth" id="conposition_earth"></div>
                                </div>
                                <p class="title-publisher">作者</p>
                                <p class="num-publisher" id="people2"></p>
                            </li>
                            <li>
                                <div class="bj mars">
                                    <div class="composition_Mars" id="composition_Mars"></div>
                                </div>
                                <p class="title-publisher">运营</p>
                                <p class="num-publisher" id="people3"></p>
                            </li>
                            <li>
                                <div class="bj mercury">
                                    <div class="composition_Mercury" id="composition_Mercury"></div>
                                </div>
                                <p class="title-publisher">出版社</p>
                                <p class="num-publisher" id="people4"></p>
                            </li>
                        </ul>
                    </div>
                </div>
                <!-- 出版人员构成end-->
            </div>
            <div class="Earth_40">
                		
                		<div class="press-num" id="press_num">
                			40%
                		</div>
                		<span class="press">
                			出版社
                		</span>
                	</div>
                	<div class="Earth_5">
                		<div class="reader_nums" id="reader_nums">
                			5%
                		</div>
                		<span class="reader">
                			作者
                		</span>
                		
                	</div>
                	<div class="Earth_20">
                		<span class="editer_nums" id="editer_nums">
                			20%
                		</span>
                		<div class="editer">
                			编辑
                		</div>
                		
                	</div>
                	<div class="Earth_25">
                		<span class="runs-num" id="runs_num">
                			25%
                		</span>
                		<div class="runs">
                			运营
                		</div>
                		
                	</div>
            <div class="wrap-middle">
                <!-- 地球-->
                <div class="composition-Earth" id="canvas">
                	
                    <div class="earth_fourPart">
                        <div class="partOne">
                            <div class="composition_Earth_40" id="composition_Earth_40"></div>
                        </div>
                        <div class="partTwo">
                            <div class="composition_Earth_5" id="composition_Earth_5"></div>
                        </div>
                        <div class="partThree">
                            <div class="composition_Earth_20" id="composition_Earth_20"></div>
                        </div>
                        <div class="partFour">
                            <div class="composition_Earth_25" id="composition_Earth_25"></div>
                        </div>
                    </div>
                    <div class="Earth-all">
                        <!--<div class="Earth-one">
                            <div class="earth-word">
                                <p class="num-part"></p>
                                <p class="title-part">出版社</p>
                            </div>
                            <span class="earth-line"></span>
                            <span class="earth-point"></span>
                        </div>
                        <div class="Earth-two">
                            <span class="earth-point"></span>
                            <span class="earth-line"></span>
                            <div class="earth-word">
                                <p class="num-part"></p>
                                <p class="title-part">作者</p>
                            </div>
                        </div>
                        <div class="Earth-three">
                            <span class="earth-point"></span>
                            <span class="earth-line"></span>
                            <div class="earth-word">
                                <p class="num-part"></p>
                                <p class="title-part">编辑</p>
                            </div>
                        </div>
                        <div class="Earth-four">
                            <span class="earth-point"></span>
                            <span class="earth-line"></span>
                            <div class="earth-word">
                                <p class="num-part"></p>
                                <p class="title-part">运营</p>
                            </div>
                        </div>-->

                    </div>
                </div>
                <!-- 地球en-->
            </div>
            <div class="wrap-right">
                <!-- 运营终端-->
                <div class="terminal-salate" id="salate"></div>
                <div class="opacity-box">
                    <span class="title-run">作者收益</span>
                    <span class="num-run" id="merchantMoney"></span>
                </div>
                <!-- 运营终端 end-->
            </div>
            <!-- stone -->
            <div class="composition-stone" id="stone"></div>
            <!-- stone end-->
        </div>
    </div>
    <script src="js/jquery.min.js"></script>
    <!-- <script src="./js/FSS.js"></script> -->
    <script src="js/transform.js"></script>
        <script src="./js/tween.js"></script>
    <script src="js/component.js"></script>
    <script src="js/comment.js"></script>
</body>

</html>