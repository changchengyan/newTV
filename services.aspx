<%@ Page Language="C#" AutoEventWireup="true"%>
<%@ Import Namespace="Redsz"%>
<%@ Import Namespace="Redsz.Services"%>
<%@ Import Namespace="Redsz.BO"%>
<%@ Import Namespace="Redsz.VO"%>
<%@ Import Namespace="System.Data"%>
<%
string method = Request["method"];
bool execute = true;//cookie放行的方法
if("SourceRarBO.addSourceRARBack".Equals(method))
{
	execute = true;
}



if (Request.Cookies["spaceInfo"] != null || execute)
{
    DuoyueServices services = new DuoyueServices();
    Response.Write(services.RequestExecutive(Request));
}
else
{
    Response.Write("{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}");
}

%>