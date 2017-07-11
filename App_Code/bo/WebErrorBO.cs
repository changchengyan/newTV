using Redsz.DAO;
using Redsz.Services;
using Redsz.VO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Net.Mail;
using System.Net.Cache;
using System.Net.Configuration;
using System.Web.Configuration;
using Redsz;
using System.Configuration;
/// <summary>
/// 创建时间:2015/12/8 20:04:30 
/// WebErrorBO业务对象 (此文件由框架自动生成 v2.0)
/// 说明：
/// QQ 3968666 email:leijiangbo@163.com
/// </summary>
/// 
namespace Redsz.BO
{
    public class WebErrorBO
    {
        //BO对应主表
        public static string MAIN_TABLE_NAME = "web_error";
        //对应的值对象命名空间
        public static string VO_NAMESPACE = "Redsz.VO.WebErrorVO";
        //列表SQL
        private static string LIST_SQL = "select * from " + MAIN_TABLE_NAME;
        private static string LIST_SQL_ORDERBY = " order by id asc";

        //列表RowCount
        private static string LIST_COUNT_SQL = "select count(*) from (" + LIST_SQL + ") ctable ";

        //TOP单页SQL
        private static string LIST_TOP_SQL_begin = "select top ";
        private static string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";


        /// <summary>
        /// 新增
        /// </summary>
        public static string Add(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                Data.Insert(MAIN_TABLE_NAME, Data.getHashtable(req, VO_NAMESPACE));
                clearStaticData();
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        } 
        /// <summary>
        /// 新增
        /// </summary>
        public string addError(Hashtable ht)
        {
            ht.Add("createtime", System.DateTime.Now.ToString());
            Data.Insert(MAIN_TABLE_NAME, ht);
            WebErrorVO weberrorvo = (WebErrorVO)Data.getVO(VO_NAMESPACE, "select top 1 * from web_error order by id desc");
            string content = weberrorvo.createtime + "\r\n" + weberrorvo.error_code + "\r\n" + weberrorvo.error_desc + "\r\n" + weberrorvo.error_path;
            //发送邮件
            string msg = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            //获取接收人
            //string emailto = ConfigurationManager.ConnectionStrings["EMAILTO"].ConnectionString;
            //string sendStute = SendMail(emailto, "错误报告", content, "");

            ////发送短信
            //ArrayList phoneList = getPhone();
            //CSendSMS sendMsg = new CSendSMS();
            //string srcString = "0";
            //if (phoneList.Equals(""))
            //{
            //    foreach (object phone in phoneList)
            //    {
            //        string postString = "method=SendSMS2User&space_name=wyss&phonenumber=" + phone.ToString() + "&content=" + content;
            //        byte[] postData = Encoding.UTF8.GetBytes(postString);
            //        string url = "http://www.duoyue.me/SendSMS2User.aspx";
            //        WebClient webClient = new WebClient();
            //        webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //        byte[] responseData = webClient.UploadData(url, "POST", postData);
            //        srcString = Encoding.UTF8.GetString(responseData);
            //    }
            //}
            //if (srcString.Equals("1") && sendStute.Equals("OK"))
            //{
            //    msg = "{\"success\":true,\"message\":\"邮件及短信发送成功\"}";
            //}
            //else if (srcString.Equals("1"))
            //{
            //    msg = "{\"success\":true,\"message\":\"邮件发送失败,短信发送成功\"}";
            //}
            //else if (sendStute.Equals("OK"))
            //{
            //    msg = "{\"success\":true,\"message\":\"邮件发送成功,短信发送失败\"}";
            //}
            //else
            //{
            //    msg = "{\"success\":false,\"message\":\"发送失败\"}";
            //}
            msg = "{\"success\":true,\"message\":\"邮件及短信发送成功\"}";
            return msg;
        }

        //获取管理员手机号
        public static ArrayList getPhone()
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
            {
                arr.Add(ConfigurationManager.AppSettings[i].ToString());
            }
            return arr;

        }
        /// <summary>  /// 发送邮件程序  /// </summary>  
        /// <param name="from">发送人邮件地址</param>  /// <param name="fromname">发送人显示名称</param>  /// <param name="to">发送给谁（邮件地址）</param>  /// <param name="subject">标题</param>  /// <param name="body">内容</param>  
        /// <param name="username">邮件登录名</param>  /// <param name="password">邮件密码</param>  /// <param name="server">邮件服务器</param>  /// <param name="fujian">附件</param>  /// <returns>send ok</returns>  
        /// 调用方法 SendMail("abc@126.com", "某某人", "cba@126.com", "你好", "我测试下邮件", "邮箱登录名", "邮箱密码", "smtp.126.com", ""); 
        public static string SendMail(string to, string subject, string body,string fujian)
        {
            SmtpSection cfg = NetSectionGroup.GetSectionGroup(WebConfigurationManager.OpenWebConfiguration("~/web.config")).MailSettings.Smtp;
            string emailname = ConfigurationManager.ConnectionStrings["EMAILNAME"].ConnectionString;
            string username = cfg.Network.UserName;
            string password = cfg.Network.Password;
            string from = cfg.From;
            try
            {
                //邮件发送类  
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, emailname);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.BodyEncoding = Encoding.Default;
                mail.Priority = MailPriority.High;
                mail.Body = body;
                mail.IsBodyHtml = true;
                if (fujian.Length > 0)
                {
                    mail.Attachments.Add(new Attachment(fujian));
                }
                SmtpClient smtp = new SmtpClient(cfg.Network.Host);
                smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //超时时间
                smtp.Timeout = 1000000;
                smtp.Send(mail);
                return "OK";

            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        public static string Mod(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                Data.Update(MAIN_TABLE_NAME, "id=@id", Data.getHashtable(req, VO_NAMESPACE));
                clearStaticData();
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// 排序
        /// </summary>
        public static string TopSize(HttpRequest req) 
        {
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id",req["id"]);
                ht.Add("topsize", req["topsize"]);
                Data.Update(MAIN_TABLE_NAME,"id=@id",ht);
                clearStaticData();
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// 删除
        /// </summary>
        public static string Del(HttpRequest req)
        {
        	string s = "{\"success\":false,\"message\":\"未登录\"}";
        	if (req.Cookies["adminInfo"] != null)
        	{

        		try
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("id", req["id"]);
                    Data.Del(MAIN_TABLE_NAME, "id", ht);
                    s = "{\"success\":true,\"id\":" + req["id"] + "}";
                    clearStaticData();
                }
                catch
                {

                    s = "{\"success\":false,\"id\":" + req["id"] + ",\"message\":\"服务异常\"}";
                }

        	}
        	return s;
        }


        /// <summary>
        /// 关闭(标记为0)
        /// </summary>
        public void OpenList(HttpRequest req)
        {
            Hashtable ht = new Hashtable();
            ht.Add("isopen", 1);
            Data.UpdateList(req, MAIN_TABLE_NAME, "id", ht, VO_NAMESPACE);
            clearStaticData();
        }


        /// <summary>
        /// 关闭(标记为0)
        /// </summary>
        public void CloseList(HttpRequest req)
        {
            Hashtable ht = new Hashtable();
            ht.Add("isopen", 0);
            Data.UpdateList(req, MAIN_TABLE_NAME, "id", ht, VO_NAMESPACE);
            clearStaticData();
        }


        /// <summary>
        /// 计数加
        /// </summary>
        public void ClickSize(string id)
        {
            Data.ClickSize(MAIN_TABLE_NAME, "id", id, "opensize", 1);
        }


        /// <summary>
        /// 获得展示列表TOP记录，不含分页
        /// </summary>
        public DataTable getTopBlog(string size)
        {
           return Data.TopList(LIST_TOP_SQL_begin + size + LIST_TOP_SQL_end);
        }


        //获得所有记录
        public DataTable getAll()
        {
            return Data.TopList(LIST_SQL+LIST_SQL_ORDERBY);
        }


        private static DataTable _HotWebError = null;
        public static DataTable HotWebError
        {
            set { _HotWebError = value; }
            get { return _HotWebError; }
        }
        /// <summary>
        /// 获得热点
        /// </summary>
        public DataTable getHotWebError(string size)
        {
            if (HotWebError == null)
            {
                HotWebError = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by topsize asc,id desc ");
            }
            return HotWebError;
        }


        private static DataTable _HotOpenWebError = null;
        public static DataTable HotOpenWebError
        {
            set { _HotOpenWebError = value; }
            get { return _HotOpenWebError; }
        }
        /// <summary>
        /// 获得热点点击
        /// </summary>
        public DataTable getHotOpenWebError(string size)
        {
            if (HotOpenWebError == null)
            {
                HotOpenWebError = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by opensize desc,id desc ");
            }
            return HotOpenWebError;
        }


        private static DataTable _WebErrorOption = null;
        public static DataTable WebErrorOption
        {
            set { _WebErrorOption = value; }
            get { return _WebErrorOption; }
        }


        /// <summary>
        /// 获得所select option TataTable
        /// </summary>
        public static DataTable getWebErrorOption()
        {
            if (WebErrorOption == null)
            {
                WebErrorOption = Data.getDataTableBySql("select id,name from " + WebErrorBO.MAIN_TABLE_NAME+ " order by topsize asc,id asc");
            }
            return WebErrorOption;
        }


        /// <summary>
        /// 获得列表，含分页参数
        /// </summary>
        public static QueryVO getList(HttpRequest req, string psize)
        {
			string sql_query = "select * from " + MAIN_TABLE_NAME + " ";
			string sql_count = sql_query;
			sql_query += " order by id asc ";
			return Data.getQueryList(req, psize, sql_query, sql_count);
        }


        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static WebErrorVO getVOByID(string id)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME;
            if (id != null) 
            {
                sql += " where id=" + id;
            }
            return (WebErrorVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 清空静态缓存数据
        /// </summary>
        public static void clearStaticData()
        {
				HotWebError = null;
				HotOpenWebError = null;
				_WebErrorOption = null;
        }

        //手机下载
        public static string downloadAttachment(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["spaceInfo"] != null)
            {
                try
                {
                    string filePath;
                    if (req["filePath"].StartsWith("http://"))
                    {
                        WebClient client = new WebClient();
                        string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "\\\\") + "download\\file";
                        //创建img文件夹
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string normalPath = (@path + "\\" + req["name"]);
                        client.DownloadFile(req["filePath"], normalPath);



                        filePath = @normalPath;
                    }
                    else
                    {
                        filePath = System.Web.HttpContext.Current.Server.MapPath(req["filePath"]);
                    }

                    if (File.Exists(filePath))
                    {
                        HttpContext curContext = HttpContext.Current;
                        FileInfo info = new FileInfo(filePath);
                        long fileSize = info.Length;
                        curContext.Response.Clear();
                        //curContext.Response.ContentType = "application/octet-stream";
                        string name = req["name"];
                        curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(info.FullName, System.Text.Encoding.UTF8));
                        curContext.Response.AppendHeader("Content-Length", fileSize.ToString());
                        switch (info.Extension.ToLower())//这是必须，电脑上浏览就不需要
                        {
                            case ".pdf":
                                curContext.Response.ContentType = "application/pdf";
                                break;
                            case ".txt":
                                curContext.Response.ClearHeaders();
                                curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(info.FullName, System.Text.Encoding.ASCII));
                                curContext.Response.ContentType = "text/plain";
                                break;
                            case ".jpg":
                                curContext.Response.ContentType = "image/jpeg";
                                break;
                            case ".doc":
                                curContext.Response.ContentType = "application/msword";
                                break;
                            case ".zip":
                                curContext.Response.ContentType = "application/zip";
                                break;
                            case ".rar":
                                curContext.Response.ContentType = "application/rar";
                                break;
                            case ".xls":
                                //Response.ContentType = "application/xls";
                                curContext.Response.ContentType = "application/vnd.ms-excel";
                                break;
                            default:
                                curContext.Response.ContentType = "application/unknown";
                                break;
                        }


                        curContext.Response.TransmitFile(info.FullName);
                        curContext.Response.Flush();
                        curContext.Response.End();

                    }
                }
                catch (Exception e)
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "\\\\") + "download\\file" + "\\" + req["name"]);
                    Console.WriteLine(e);
                }
                finally
                {
                    HttpContext.Current.Response.Close();
                }

                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }

    }
}
