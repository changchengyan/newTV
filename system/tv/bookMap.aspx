<%@ Page Language="C#" %>

<!DOCTYPE html>

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>bookMap</title>
	<link rel="stylesheet" href="css/comment.css" />
	<link rel="stylesheet" href="css/bookMap.css" />
	<script src="js/jquery.min.js"></script>
	<script src="js/shine.min.js"></script>
    <script src="js/echarts.min.js"></script>
    <script src="js/china.js"></script>
    <!--<script src="libs/china_cityAndCounty.min.js"></script>-->
	<script src="js/transform.js"></script>
	<script src="js/tween.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bookMap">
		<div class="shootingStar">
            <div class="stars"></div>
            <div class="stars stars2"></div>
            <div class="stars stars3"></div>  
            <div class="stars stars4"></div>   
        </div>
        <div class="brower_star"></div>
		<div class="bookMap-left">
			<div class="book-pic">
				<div class="sync-bookpic">
					<img  id="book_pic"  src="" alt="" />
				</div>
			</div>
			<div class="book-baseMsg">
				<div class="bookName" id="bookName"></div>
				<div class="book-isbn">ISBN:<span id="isbn"></span></div>
				<div class="book-press" id="book_press"></div>
			</div>
			<div class="book-eleMsg">
					<!--<div class="RQ_pic"></div>                                   
					<div class="resource_pic"></div>                                   
					<div class="app_pic"></div>                                   -->
				<div class="RQ">

					<div class="RQ_pic"></div> <span class="str">二维码</span>：<span class="RQ-num" id="RQ_num"></span>
				</div>
				<div class="resource">

					<div class="resource_pic"></div> 配套资源<span class="dolt1">：</span><span class="resource-num" id="resource_num"></span>
				</div>
				<!--<div class="app">

					<div class="app_pic"></div>配套应用<span class="dolt2">：</span><span class="app-num" id="app_num"></span>
				</div>-->
			</div>
			<dl class="RQ-pic">
				<dd class="RQ-wrap">
					<div class="sync_RQPic">
						<img  id="sync_RQPic" src="images/qrcode.jpg" alt="" />
					</div>
				</dd>
				<dt>
					<span>敬请扫码体验</span>
				</dt>
			</dl>
		</div>
		<div class="bookMap-right">
			<div class="top-tips">
					<div class="top_bg"></div>
					<div class="wrap-topThings">
						<dl>
						<dd><div class="right-top">￥<span class="pay" id="pay"></span>.<span class="decimal" id="decimal"></span></div></dd>
						<dt><span>成交额（元）</span></dt>
					</dl>
					<dl>
						<dd><div class="right-top"><span class="pay" id="brower"></span></div></dd>
						<dt><span>浏览数（次）</span></dt>
					</dl>
					<dl>
						<dd><div class="right-top"><span class="pay" id="readers"></span></div></dd>
						<dt><span>读者数（人）</span></dt>
					</dl>
					</div>
			</div>
			<div class="bottom-bookmap">
				<div id="main"></div>
			</div>
		</div>
	</div>
    </form>
    <script src="js/bookMap.js"></script>
	<script src="js/comment.js"></script>
</body>
</html>
