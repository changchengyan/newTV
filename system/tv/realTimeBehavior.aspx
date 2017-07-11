<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" http-equiv="refresh" content="3000" />
    <title>实时行为动态</title>
    <link rel="stylesheet" type="text/css" href="./css/comment.css"/>
    <link rel="stylesheet" type="text/css" href="./css/realTimeBehavior.css"/>
    <script src="js/jquery.min.js"></script>
    <script src="js/iPad.js"></script>
</head>
<body>
	
	<div class="brower_star"></div>
	<div class="shootingStar" id="pooducted">
		<div class="stars"></div>
		<div class="stars stars2"></div>
		<div class="stars stars3"></div>
		<div class="stars stars4"></div>
	</div>
    <div class="realTimeBehavior-title"></div>
    <div class="content-all clearfix">
        <div class="content-left left">
            <h4 style="padding-bottom: 20px">用户实时行为</h4>
            <div class="behavior-list">
                 <div class="line"></div>
                <div class="list-box">
                     <div class="item-behavior">
                        <div class="user-time">13:08:16</div>
                        <div class="user-img"><img src="images/tx1.png" onerror="imgChange($(this))"/></div>
                        <div class="behavior-desc">
                            <div class="desc-word">
                                <p>张佳静浏览了《世界简史第二版——之诺曼底
                                   登陆的结局以及对大战的影响》</p>
                                <span class="behavior-title">出版社名称</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <div class="content-right right">
            <div class="right-item">
                <h4>最新关注</h4>
                <div class="user-list">

                    <span class="user-item">
                        <img class="item-img" src="images/tx1.png" alt="user-img" onerror="imgChange($(this))"/>
                        <span class="user-name a-line">微信昵称</span>
                    </span>

                </div>

            </div>
            <div class="right-item">
                <h4>最新扫码</h4>
                <div class="user-list">
                    <span class="user-item">
                        <img class="item-img" src="images/tx1.png" alt="user-img" onerror="imgChange($(this))"/>
                        <span class="user-name a-line">微信昵称</span>
                    </span>
                </div>
            </div>
            <div class="right-item">
                <h4>最新支付</h4>
                <div class="user-list">
                    <span class="user-item">
                        <img class="item-img" src="images/tx1.png" alt="user-img" onerror="imgChange($(this))"/>
                        <span class="user-name a-line">微信昵称</span>
                    </span>
                </div>
            </div>
        </div>
    </div>
        <script src="js/transform.js"></script>
		<script src="js/tween.js"></script>
		<script src="js/realTimeBehavior.js"></script>
		<script src="js/comment.js"></script>
</body>
</html>