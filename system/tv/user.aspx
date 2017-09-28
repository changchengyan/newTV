<!DOCTYPE html>
<html lang="en">

	<head>
		<meta http-equiv="content-type" content="text/html;charset=utf-8">
		<title>用户总览</title>
		<link rel="stylesheet" href="css/comment.css">
		<link rel="stylesheet" href="css/user.css">
	</head>

	<body>
		<div class="user">
			<div class="brower_star"></div>
			<div id="container3" class="container">
				<div id="output3"></div>
			</div>
			<div class="shootingStar" id="pooducted">
				<div class="stars"></div>
				<div class="stars stars2"></div>
				<div class="stars stars3"></div>
				<div class="stars stars4"></div>
			</div>
			<div class="user_bg">
				<!--背景星空 -->
				<div class="user_skey"></div>
				<!--背景蒙层 -->
				<!--<div class="user_mengceng"></div>-->
				<!--背景亮光 -->
				<div class="user_light"></div>
				<!--背景星星 -->
				<div class="user_star"></div>
				<!--背景飞碟-->
				<div class="user_ball" id="user_ball"></div>
				<!--背景极光左上角-->
				<div class="user_lt"></div>
				<!--背景极光右下角-->
				<!--<div class="user_rb"></div>-->
				<!--背景极光右上角-->
				<div class="user_rt"></div>
				<!--RAYS读者-->
				<div class="readers"></div>
			</div>
			<div class="RaysNum" id="userCount"></div>
			<div class="userTitle"></div>
			<!--正文 -->
			<div class="contain">

				<!--用户性别比例-->
				<div class="sex">
					<div class="title-user">用户性别比例</div>
					<div class="chart-wrap">
						<div class="sex-chart" id="sex_chart"></div>
						<div class="sex-light"></div>
						<div class="sex-mask"></div>
						<div class="detail">
							<div class="man">男性&nbsp;<span id="male"></span></div>
							<div class="fmale">
								<span class="fmaleName">女性&nbsp;</span><span class="woman" id="fMale"></span>
							</div>
							<div class="other">其他&nbsp;&nbsp;&nbsp;&nbsp;<span id="other"></span></div>
						</div>
					</div>
				</div>
				<!--消费用户比例-->
				<div class="xiaofei">
					<div class="title-user">消费用户比例</div>
					<div class="xiaofei-canvas" id="xiaofei_canvas"></div>
					<div class="xiaofei_big"></div>
					<div class="xiaofei_sm"></div>
					<div class="num" id="payUserPercent"></div>
				</div>
				<div class="gender">
					<div class="mans" ></div>
					<div class="womens"></div>
					<div class="others" ></div>
				</div>
				<!--<div class="payAndNoPay-num">
					<div id="noPay"></div>
					<div id="pay"></div>
				</div>-->
				<!--用户省份排名-->
				<!--<div class="rank ">
					<div id="top1"></div>
					<div id="top2"></div>
					<div id="top3"></div>
				</div>-->
				<!--活跃用户-->
				<div class="active">
					<div class="detail">
						<p class="num" id="activeUserNum"></p>
						<p class="text">活跃用户</p>
					</div>
					<div class="pic"></div>
				</div>
			</div>
		</div>
		<script src="js/jquery.min.js"></script>
		<script src="js/transform.js"></script>
		<script src="js/tween.js"></script>
		<script src="js/echarts.min.js"></script>
		<script src="js/numScrollTop.js"></script>
		<script src="js/user.js"></script>
		<script src="js/comment.js"></script>
	</body>

</html>