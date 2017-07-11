<%@ Application Language="C#" %>
<%@ Import Namespace="Redsz"%>
<%@ Import Namespace="Redsz.DAO"%>
<%@ Import Namespace="Redsz.BO"%>
<%@ Import Namespace="Redsz.VO"%>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 在应用程序启动时运行的代码
        
        Redsz.Timer.Init(HttpContext.Current);
       
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    {
        //排除本地调试
        //        if (Request.Url.ToString().IndexOf("loc") > -1 || Request.Url.ToString().IndexOf("127.") > -1 || Request.Url.ToString().IndexOf("192.168.") > -1)
       if (Request.Url.ToString().IndexOf("loc") > -1 || Request.Url.ToString().IndexOf("127.") > -1 || Request.Url.ToString().IndexOf("192.168.") > -1)
        {


        }
        else
        {
            // 在出现未处理的错误时运行的代码
            Exception lastError = Server.GetLastError();

            if (lastError != null)
            {
                try
                {
                    int errorCode = ((HttpException)(lastError)).GetHttpCode();
                    if (lastError is HttpException)
                    {
                        
                        string errorDesc = "系统错误";
                        if (errorCode == 404)
                        {
                            errorDesc = "未找到页面";
                        }
                        string source_space_name = "";
                        string source_space_id = "";
                        string source_weixin_id = "";
                        string source_weixin_name = "";
                        string source_uid = "";
                        string source_system = "";

                        

                        Hashtable ht = new Hashtable();
                        ht.Add("error_code", errorCode);
                        ht.Add("error_desc", errorDesc);
                        ht.Add("error_path", Request.Url.ToString());

                        ht.Add("exception", lastError.Message.Replace("\n", "<br/>") + lastError.Source.Replace("\n", "<br/>") + lastError.StackTrace.Replace("\n", "<br/>"));
                        ht.Add("all_http", Request.ServerVariables["ALL_HTTP"]);
                        ht.Add("all_raw", Request.ServerVariables["ALL_RAW"]);
                        ht.Add("client_ip", UtilBO.GetWebClientIp());

                        ht.Add("source_space_id", source_space_id);
                        ht.Add("source_weixin_id", source_weixin_id);
                        ht.Add("source_uid", source_uid);
                        ht.Add("source_system", source_system);
                        ht.Add("source_space_name", source_space_name);
                        ht.Add("source_weixin_name", source_weixin_name);

                        string source_post_data = "";
                        foreach (string name in Request.Form)
                        {
                            source_post_data += "&" + name + "=" + System.Web.HttpContext.Current.Server.UrlEncode(Request.Form[name]);
                        }
                        ht.Add("source_post_data", source_post_data);

                        WebErrorBO weberrorbo = new WebErrorBO();
                        weberrorbo.addError(ht);
                        Response.StatusCode = errorCode;


                    }

                    Server.ClearError();
                    if (errorCode == 404)
                    {
                        Response.Redirect("/404.aspx");
                        Response.End();
                    }
                    else
                    {
                        Response.Redirect("/500.aspx");
                        Response.End();
                    }

                }
                catch (Exception ge)
                {
                    Server.ClearError();
                    Response.Redirect("/error.aspx?error=Global");
                    Response.End();
                }
            }
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。

    }
       
</script>
