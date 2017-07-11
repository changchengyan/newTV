<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vo.aspx.cs" Inherits="system_valueObject" %>
<%@ Import Namespace="Redsz"%>
<%@ Import Namespace="Redsz.BO"%>
<%@ Import Namespace="Redsz.VO"%>
<%@ Import Namespace="System.Data"%>
<html>
<head runat="server">
    <title>VO值对象生成工具v1.0</title>
</head>
<!-- #include file="include_meta.aspx" -->
        <Script language="javascript">
function Checkform(fm)
{ 		    
		
		if (fm.tablename.value == ""){
		    alert("tablename不能为空!");
			fm.tablename.focus();
	    	return false;
  		}
		
		if (fm.filename.value == ""){
		    alert("filename不能为空!");
			fm.filename.focus();
	    	return false;
  		}
			return true;		
}		


//自动命名
function autoname(v)
{
	var a = v.split("_");
	var b = [];
	for(var i=0;i<a.length;i++)
	{
		var s = a[i];
		var s1 = s.substring(0,1).toUpperCase() + s.substring(1,s.length);
		b.push(s1);
	}

	var _filename = document.getElementById("filename");
	_filename.value = b.join("")+"VO";
	
}			
</script>
<style>
.button_submit{ width:100%;}
</style>
<body>
<div style="width:500px; margin:auto; margin-top:2%;">
    <div style="padding:20px; font-size:22px; background-color:#c81623; color:#fff;">Platform<span style="float:right;">Value Object</span></div>
    <div style="background-color:#f8f8f8; padding:50px;">
        <form id="Form1" name="myform" runat="server" method="post" onSubmit="return Checkform(this);">
            <div style="padding:10px 0px 5px 0px;">数据库表名</div>
            <div><input name="tablename" type="text" id="tablename" style="width:100%;" onBlur="autoname(this.value)"></div>
        
            <div style="padding:10px 0px 5px 0px;">生成文件名</div>
            <div><input name="filename" type="text" id="filename" style="width:100%;" value="VO"></div>
    
    
            <div style="padding:10px 0px 5px 0px;">业务说明</div>
            <div><input name="desc" type="text" id="desc" style="width:100%;" ></div>
    
            <div style="padding:20px 0px 0px 0px;"><asp:Button ID="add" OnClick="click_add" runat="server" Text="确定创建 VO" CssClass="button_submit"/></div>
    		<div style="padding:20px 0px 0px 0px; text-align:center;"><a href="bo.aspx">前往BO文件创建</a></div>
      </form>
    </div>
</div>
</body>
</html>
