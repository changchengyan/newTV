<%@ Page Language="C#" AutoEventWireup="true" %>

<%@ Import Namespace="Redsz" %>
<%@ Import Namespace="Redsz.Services" %>
<%@ Import Namespace="Redsz.BO" %>
<%@ Import Namespace="Redsz.VO" %>
<%@ Import Namespace="System.Data" %>
<%
    string method = Request["method"];
    if (UtilBO.isNotNull(method))
    {
        SystemServices services = new SystemServices();
        Response.Write(services.ContextExecutiveDebug(HttpContext.Current));
    }
    else
    {
        Response.Redirect("/system/tv/index.aspx");
    }
    
%>