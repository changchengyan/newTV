using System;
using System.IO;
using System.Web;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Text;
using Redsz.DAO;
using Redsz.VO;
using Redsz;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Globalization;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;

/// <summary>
/// UtilBO 的摘要说明

/// </summary>
/// 
namespace Redsz.BO
{
    public class UtilBO
    {

        /// <summary>
        /// 请求通信管理平台时需要的加密字符串比对
        /// MD5(MD5(adviser_id) + MD5(adviser_loginname) + MD5(adviser_password)) 
        /// </summary>
        public static string MethotMD5(string adviser_id, string adviser_loginname, string adviser_password)
        {
            string s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(adviser_id, "md5") + System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(adviser_loginname, "md5") + System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(adviser_password, "md5");
            s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5");
            s = s.ToLower();
            return s;
        }

        /// <summary>
        /// 获得随机值（begin - end 之间的）
        /// </summary>
        public static int getRandomNumber(int begin,int end)
        {            
            Random randObj = new Random();
            return randObj.Next(begin, end);
        }


        /// <summary>
        /// DataTable list 转换为 Chart.js 可用数据
        /// </summary>
        public static string TableToChartData(DataTable list,string text_column_name,string value_column_name)
        {
            string labels = "[";
            string data = "[";

            for (int i = 0; i < list.Rows.Count; i++)
            {
                if (i > 0)
                {
                    labels += ",";
                    data += ",";
                }
                labels += "\""+list.Rows[i][text_column_name].ToString()+"\"";
                data += list.Rows[i][value_column_name].ToString();

            }
            labels += "]";
            data += "]";

            return "{labels:" + labels + ",data:" + data + "}";
        }

        /// <summary>
        /// 格式化需要跳转的URL,get请求用的
        /// </summary>
        public static string formatPassurl(string url)
        {
            return url.Replace("&", "|").Replace("=", "*");
        }

        /// <summary>
        /// 解开已格式化需要跳转的URL,get请求用的
        /// </summary>
        public static string toFormatPassurl(string url)
        {
            return url.Replace("|", "&");
        }

         /// <summary>
        /// 获得本地或远程请求的页面HTML源代码
        /// </summary>
        public static string getWebPageHTML(string url, string encoding, HttpContext httpcontext)
        {

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            httpcontext.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encoding);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream resStream = response.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(resStream, System.Text.Encoding.GetEncoding(encoding));
            string str = sr.ReadToEnd();
            resStream.Close();
            sr.Close();
            return str;


        }


        /// <summary>
        /// 获得本地或远程请求的页面HTML源代码
        /// </summary>
        public static string getWebPageHTML(string url,string encoding)
        {           

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encoding);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream resStream = response.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(resStream, System.Text.Encoding.GetEncoding(encoding));
            string str = sr.ReadToEnd();
            resStream.Close();
            sr.Close();
            return str;
        }


        /// <summary>
        /// 在本地创建静态HTML页
        /// </summary>
        public static void applicationStaticHTML(HttpRequest req, HttpResponse res)
        {
            string iscratehtml = req.QueryString["iscratehtml"];

            if (!"true".Equals(iscratehtml))
            {

                
                
                string path = req.Url.ToString();
                path = path.Substring(path.LastIndexOf("/") + 1, path.Length - path.LastIndexOf("/") - 1);
                path = path.Replace(".aspx?", "_");
                path = path.Replace(".aspx", "");
                path = path.Replace("&", "+");
                path = path.Replace("=", "-");
                path += ".html";
                path = "/_html/" + path;             
               
                string pathHead = "http://"+req.ServerVariables["Server_Name"];
                string port = req.ServerVariables["Server_Port"];
                if (!"".Equals(port) && port!=null) 
                {
                    pathHead += ":" + port;
                }


                if (sys.existsFile(path))
                {
                    string html = getWebPageHTML(pathHead + path, "UTF-8");
                    res.Write(html);                    
                    res.End();

                }
                else
                {
                    string url = req.Url.ToString();
                    if(url.IndexOf("?")>0)
                    {
                        url += "&iscratehtml=true";
                    }
                    else
                    {
                        url += "?iscratehtml=true";
                    }
                    string html = getWebPageHTML(url, "UTF-8");
                    sys.createTextFile(path, html, "UTF-8");
                }


                

            }


        }

        /// <summary>
        /// 发送手机短信
        /// </summary>
        public static string sendSMS(string content,string mobile)
        {
            string s = "";

            if (content.Length > 60) { content = content.Substring(0, 60); }
            string sms_content = HttpUtility.UrlEncode(content, System.Text.Encoding.GetEncoding("gb2312"));
            string sms_url = "http://sms.3gbmcc.com/MTSubmit2.jsp?group_id=clwx115&user=wgbjqy&user_pwd=123456&mobile=" + mobile + "&type=0&cell=001&order_time=&content=" + sms_content;
            WebRequest wRequest = WebRequest.Create(sms_url);
            WebResponse wResponse = wRequest.GetResponse();
            Stream stream = wResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.Default);
            string r = reader.ReadToEnd();
            wResponse.Close();


            return s;


        }



        /// <summary>
        /// 分页的代码HTML
        /// </summary>
        public static string StaticHTMLFilePageStr = "";
        /// <summary>
        /// 获得在本地创建静态文件名列表，返回的是本地全路径
        /// </summary>
        public static DataTable getStaticHTMLFile(HttpRequest req, string str_PageSize)
        {
            return getStaticHTMLFile(req, str_PageSize, "/_html/");

        }
        /// <summary>
        /// (实现方法)获得在本地某个目录文件名列表，返回的是本地全路径
        /// </summary>
        public static DataTable getStaticHTMLFile(HttpRequest req, string str_PageSize,string pathname)
        {
            DataTable list = sys.getgetFolderFileToDatTable(pathname);
            DataTable pageList = new DataTable();
            for (int i = 0; i < list.Columns.Count; i++)
            {
                pageList.Columns.Add(list.Columns[i].ColumnName, list.Columns[i].DataType);

            }
            int pagesize = int.Parse(str_PageSize);//默认为10
            string topage = req.QueryString["topage"];
            if (topage == null || "".Equals(topage))
            {
                topage = "1";
            }
            int int_topage = int.Parse(topage);
            int i_begin = 0 + (pagesize * (int_topage - 1));
            int i_end = i_begin + pagesize;
            DataRow row;



            for (int i = i_begin; i < i_end && i < list.Rows.Count; i++)
            {

                row = pageList.NewRow();
                for (int j = 0; j < pageList.Columns.Count; j++)
                {
                    row[j] = list.Rows[i][j];
                }

                pageList.Rows.Add(row);


            }

            StaticHTMLFilePageStr = Data.getPageHTML(req, list.Rows.Count, pagesize);
            return pageList;

        }


        /// <summary>
        /// 格式化查询字符串，防止sql语句出错
        /// </summary>
        public static string formatSql(string s)
        {
            if (s != null && !"".Equals(s))
            {
                s = s.Replace("'", "");
                s = s.Replace("(", "");
                s = s.Replace(")", "");
                s = s.Replace("%", "");
                s = s.Replace("!", "");
            }
            else
            {
                s = "";
            }
            return s;
        }



        /// <summary>
        /// 自动创建单表VO的方法
        /// </summary>
        public static void createValueObject(string desc, string tablename, string classname)
        {

            //SQL2000与2008用法不一致
            if ("MSSQL2008".Equals(ConfigurationManager.ConnectionStrings["VERSION"].ConnectionString))
            {
                string sql = "";
                DataTable version_table = Data.getDataTable(" select serverproperty('productversion') v ");
                string version = version_table.Rows[0]["v"].ToString().Split('.')[0];
                if ("9".Equals(version) || "10".Equals(version) || "11".Equals(version) || "12".Equals(version))
                {
                    sql = "SELECT syscolumns.name 字段名,systypes.name 类型,syscolumns.isnullable, ";
                    sql += " syscolumns.length 长度,(select top 1 value from sys.extended_properties where major_id=syscolumns.id and minor_id=syscolumns.colid) 字段说明 ";
                    sql += " FROM syscolumns, systypes  ";
                    sql += " WHERE syscolumns.xusertype = systypes.xusertype ";
                    sql += " AND syscolumns.id = object_id('" + tablename + "')";

                }
                else
                {
                    sql = "SELECT  表名=case when a.colorder=1 then d.name else '' end, 表说明=case when a.colorder=1 then isnull(f.value,'') else '' end, 字段序号=a.colorder, 字段名=a.name, 标识=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end, 主键=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (  SELECT name FROM sysindexes WHERE indid in(   SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid  ))) then '√' else '' end, 类型=b.name, 占用字节数=a.length, 长度=COLUMNPROPERTY(a.id,a.name,'PRECISION'), 小数位数=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), 允许空=case when a.isnullable=1 then '√'else '' end, 默认值=isnull(e.text,''), 字段说明=isnull(g.[value],'')FROM syscolumns a left join systypes b on a.xtype=b.xusertype inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id left join sysproperties g on a.id=g.id and a.colid=g.smallid   left join sysproperties f on d.id=f.id and f.smallid=0 where d.name='" + tablename + "' order by a.id,a.colorder ";

                }


                string str = "";
                string s1 = "";
                string s2 = "";
                DataTable table = Data.TopList(sql);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    s1 += "        ";
                    if ("int".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s1 += "private int _";
                    }

                    else if ("float".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s1 += "private double _";
                    }
                    else
                    {
                        s1 += "private string _";
                    }
                    s1 += table.Rows[i]["字段名"].ToString();
                    s1 += "; //";
                    s1 += table.Rows[i]["字段说明"].ToString().Replace("\n", "").Replace("\r", "").Replace("\t", "") + "      " + table.Rows[i]["类型"].ToString() + ",length:" + table.Rows[i]["长度"].ToString() + ",";
                    s1 += "\n";



                    s2 += "\n\n";
                    s2 += "        /// <summary>\n";
                    s2 += "        ///" + table.Rows[i]["字段说明"].ToString().Replace("\n", "").Replace("\r", "").Replace("\t", "") + "      " + table.Rows[i]["类型"].ToString() + ",length:" + table.Rows[i]["长度"].ToString() + "\n";
                    s2 += "        /// </summary>\n";
                    s2 += "        public ";
                    if ("int".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s2 += "int";
                    }
                    else if ("float".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s2 += "double";
                    }
                    else
                    {
                        s2 += "string";
                    }

                    s2 += " " + table.Rows[i]["字段名"].ToString() + "\n";
                    s2 += "        {\n          get{return _" + table.Rows[i]["字段名"].ToString() + ";}\n";
                    s2 += "          set{_" + table.Rows[i]["字段名"].ToString() + " = value;}\n        }";


                }

                str = "/// <summary>\n/// " + tablename + "表" + classname + " 值对象,创建时间:" + System.DateTime.Now.ToString() + "(此文件由框架自动生成 v2.0)\n/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com\n///描述：" + desc + "值对象\n/// </summary>\n\nnamespace Redsz.VO\n{\n\n	public class " + classname + "\n	{\n" + s1 + s2 + "\n	}\n\n}";
                sys.createTextFile("/App_code/vo/" + classname + ".cs", str);

            }
            else
            {

                string str = "";
                string s1 = "";
                string s2 = "";
                DataTable table = Data.TopList("SELECT  表名=case when a.colorder=1 then d.name else '' end, 表说明=case when a.colorder=1 then isnull(f.value,'') else '' end, 字段序号=a.colorder, 字段名=a.name, 标识=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end, 主键=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (  SELECT name FROM sysindexes WHERE indid in(   SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid  ))) then '√' else '' end, 类型=b.name, 占用字节数=a.length, 长度=COLUMNPROPERTY(a.id,a.name,'PRECISION'), 小数位数=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), 允许空=case when a.isnullable=1 then '√'else '' end, 默认值=isnull(e.text,''), 字段说明=isnull(g.[value],'')FROM syscolumns a left join systypes b on a.xtype=b.xusertype inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id left join sysproperties g on a.id=g.id and a.colid=g.smallid   left join sysproperties f on d.id=f.id and f.smallid=0 where d.name='" + tablename + "' order by a.id,a.colorder ");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    s1 += "        ";
                    if ("int".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s1 += "private int _";
                    }
                    else if ("nvarchar".Equals(table.Rows[i]["类型"].ToString()) || "ntext".Equals(table.Rows[i]["类型"].ToString()) || "datetime".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s1 += "private string _";
                    }
                    else if ("float".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s1 += "private double _";
                    }

                    s1 += table.Rows[i]["字段名"].ToString();
                    s1 += "; //";
                    s1 += table.Rows[i]["字段说明"].ToString() + "      " + table.Rows[i]["类型"].ToString() + ",length:" + table.Rows[i]["长度"].ToString() + ",default:" + table.Rows[i]["默认值"].ToString();
                    s1 += "\n";



                    s2 += "\n\n";
                    s2 += "        /// <summary>\n";
                    s2 += "        ///" + table.Rows[i]["字段说明"].ToString() + "      " + table.Rows[i]["类型"].ToString() + ",length:" + table.Rows[i]["长度"].ToString() + ",default:" + table.Rows[i]["默认值"].ToString() + "\n";
                    s2 += "        /// </summary>\n";
                    s2 += "        public ";
                    if ("int".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s2 += "int";
                    }
                    else if ("nvarchar".Equals(table.Rows[i]["类型"].ToString()) || "ntext".Equals(table.Rows[i]["类型"].ToString()) || "datetime".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s2 += "string";
                    }
                    else if ("float".Equals(table.Rows[i]["类型"].ToString()))
                    {
                        s2 += "double";
                    }
                    s2 += " " + table.Rows[i]["字段名"].ToString() + "\n";
                    s2 += "        {\n          get{return _" + table.Rows[i]["字段名"].ToString() + ";}\n";
                    s2 += "          set{_" + table.Rows[i]["字段名"].ToString() + " = value;}\n        }";


                }

                str = "/// <summary>\n/// " + table.Rows[0]["表名"].ToString() + "表" + classname + " 值对象,创建时间:" + System.DateTime.Now.ToString() + "(此文件由框架自动生成 v2.0)\n/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com\n///描述：" + desc + "值对象\n/// </summary>\n\nnamespace Redsz.VO\n{\n\n	public class " + classname + "\n	{\n" + s1 + s2 + "\n	}\n\n}";
                sys.createTextFile("/App_code/vo/" + classname + ".cs", str);
            }
        }


        /// <summary>
        /// 自动创建单表BO的方法
        /// </summary>
        public static void createBusinessObject(string desc, string tablename, string voname, string classname)
        {
            string objectname = classname.Replace("BO", "");
            string str = "";
            str += "using System;\n";
            str += "using System.Web;\n";
            str += "using System.Collections;\n";
            str += "using System.Collections.Generic;\n";
            str += "using System.Data;\n";
            str += "using System.Data.SqlClient;\n";
            str += "using System.Data.OleDb;\n";
            str += "using System.Configuration;\n";
            str += "using System.Reflection;\n";
            str += "using Redsz.DAO;\n";
            str += "using Redsz.VO;\n";
            str += "using Redsz;\n";
            str += "/// <summary>\n";
            str += "/// 创建时间:" + System.DateTime.Now.ToString() + " \n";
            str += "/// " + classname + "业务对象 (此文件由框架自动生成 v2.0)\n";
            str += "/// 说明：" + desc + "\n";
            str += "/// QQ:3968666 email:leijiangbo@163.com  mob:18510727587\n";
            str += "/// </summary>\n";
            str += "namespace Redsz.BO\n";
            str += "{\n";
            str += "    /// <summary>\n";
            str += "    /// " + desc + "业务对象\n";
            str += "    /// </summary>\n";
            str += "    public class " + classname + "\n";
            str += "    {\n";
            str += "        /// <summary>\n";
            str += "        /// BO的数据库表名\n";
            str += "        /// </summary>\n";
            str += "        public static string MAIN_TABLE_NAME = \"" + tablename + "\";\n";
            str += "        /// <summary>\n";
            str += "        /// VO的命名空间\n";
            str += "        /// </summary>\n";
            str += "        public static string VO_NAMESPACE = \"Redsz.VO." + voname + "\";\n";
            str += "\n";
            str += "        /// <summary>\n";
            str += "        /// 新增" + desc + "\n";
            str += "        /// </summary>\n";
            str += "        public static string Add(HttpContext context)\n";
            str += "        {\n";
            str += "            HttpRequest req = context.Request;\n";
            str += "            string s = \"{\\\"success\\\":false,\\\"message\\\":\\\"会话已失效，请重新登录！\\\"}\";\n";
            str += "            if (req.Cookies[\"adminInfo\"] != null)\n";
            str += "            {\n";
            str += "                string admin_id = req.Cookies[\"adminInfo\"].Values[\"admin_id\"].ToString();\n";
            str += "                string admin_role_id = req.Cookies[\"adminInfo\"].Values[\"admin_role_id\"].ToString();\n";
            str += "                string random = sys.getRandomStr();\n";
            str += "                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);\n";
            str += "                ht.Add(\"createtime\",DateTime.Now.ToString());\n";
            str += "                ht.Add(\"random\", random);\n";
            str += "                Data.Insert(MAIN_TABLE_NAME , ht);               \n";
            str += "                s = \"{\\\"success\\\":true,\\\"message\\\":\\\"\\\"}\";\n";
            str += "            }\n";
            str += "            return s;\n";
            str += "        }\n";
            str += "\n";
            str += "\n";
            str += "        /// <summary>\n";
            str += "        /// 修改" + desc + "\n";
            str += "        /// </summary>\n";
            str += "        public static string Modify(HttpContext context)\n";
            str += "        {\n";
            str += "            HttpRequest req = context.Request;\n";
            str += "            string s = \"{\\\"success\\\":false,\\\"message\\\":\\\"会话已失效，请重新登录！\\\"}\";\n";
            str += "            if (req.Cookies[\"adminInfo\"] != null)\n";
            str += "            {\n";
            str += "                string admin_id = req.Cookies[\"adminInfo\"].Values[\"admin_id\"].ToString();\n";
            str += "                string admin_role_id = req.Cookies[\"adminInfo\"].Values[\"admin_role_id\"].ToString();\n";
            str += "                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);\n";
            str += "                " + voname + " vo = getVOByID(ht[\"id\"].ToString());\n";
            str += "                //判断此记录是否存在\n";
            str += "                if (vo.id > 0)\n";
            str += "                {\n";
            str += "                    Data.Update(MAIN_TABLE_NAME, \"id=@id\", ht);\n";
            str += "                    s = \"{\\\"success\\\":true,\\\"message\\\":\\\"\\\"}\";\n";
            str += "                }\n";
            str += "                else\n";
            str += "                {\n";
            str += "                    s = \"{\\\"success\\\":false,\\\"message\\\":\\\"记录不存在\\\"}\";\n";
            str += "                }               \n";
            str += "            }\n";
            str += "            return s;\n";
            str += "        }\n";
            str += "\n";
            str += "\n";
            str += "        /// <summary>\n";
            str += "        /// 删除" + desc + "\n";
            str += "        /// </summary>\n";
            str += "        public static string Delete(HttpContext context)\n";
            str += "        {\n";
            str += "            HttpRequest req = context.Request;\n";
            str += "            string s = \"{\\\"success\\\":false,\\\"message\\\":\\\"会话已失效，请重新登录！\\\"}\";\n";
            str += "            if (req.Cookies[\"adminInfo\"] != null)\n";
            str += "            {\n";
            str += "                string admin_id = req.Cookies[\"adminInfo\"].Values[\"admin_id\"].ToString();\n";
            str += "                string admin_role_id = req.Cookies[\"adminInfo\"].Values[\"admin_role_id\"].ToString();\n";
            str += "                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);\n";
            str += "                " + voname + " vo = getVOByID(ht[\"id\"].ToString());\n";
            str += "                //判断此记录是否存在\n";
            str += "                if (vo.id > 0)\n";
            str += "                {\n";
            str += "                    Data.Del(MAIN_TABLE_NAME, \"id\", ht);\n";
            str += "                    s = \"{\\\"success\\\":true,\\\"message\\\":\\\"\\\"}\";\n";
            str += "                }\n";
            str += "                else\n";
            str += "                {\n";
            str += "                    s = \"{\\\"success\\\":false,\\\"message\\\":\\\"记录不存在\\\"}\";\n";
            str += "                }\n";
            str += "            }\n";
            str += "            return s;\n";
            str += "        }\n";
            str += "\n";
            str += "\n";
            str += "        /// <summary>\n";
            str += "        /// 排序\n";
            str += "        /// </summary>\n";
            str += "        public static string TopSize(HttpContext context) \n";
            str += "        {\n";
            str += "            HttpRequest req = context.Request;\n";
            str += "            string s = \"{\\\"success\\\":false,\\\"message\\\":\\\"会话已失效，请重新登录！\\\"}\";\n";
            str += "            if (req.Cookies[\"adminInfo\"] != null)\n";
            str += "            {\n";
            str += "                Hashtable ht = new Hashtable();\n";
            str += "                ht.Add(\"id\",req[\"id\"]);\n";
            str += "                ht.Add(\"topsize\", req[\"topsize\"]);\n";
            str += "                Data.Update(MAIN_TABLE_NAME,\"id=@id\",ht);\n";
            str += "                s = \"{\\\"success\\\":true,\\\"message\\\":\\\"\\\"}\";\n";
            str += "            }\n";
            str += "            return s;\n";
            str += "        }\n";
            str += "\n";
            str += "\n";
            str += "        /// <summary>\n";
            str += "        /// 获得信息分页列表\n";
            str += "        /// </summary>\n";
            str += "        public static QueryVO getList(HttpContext context,string psize)\n";
            str += "        {\n";
            str += "            HttpRequest req = context.Request;\n";
            str += "            string admin_id = req.Cookies[\"adminInfo\"].Values[\"admin_id\"].ToString();\n";
            str += "            string admin_role_id = req.Cookies[\"adminInfo\"].Values[\"admin_role_id\"].ToString();\n";
            str += "            string sql_query = \"select id  from \" + MAIN_TABLE_NAME + \" where 1=1 \";\n";
            str += "            string sql_count = sql_query;\n";
            str += "            sql_query += \" order by id desc \";\n";
            str += "            return Data.getQueryList(req, psize, sql_query, sql_count);\n";
            str += "        }\n";
            str += "\n";
            str += "\n";
            str += "        /// <summary>\n";
            str += "        /// 根据ID获得单个记录值对象VO\n";
            str += "        /// </summary>\n";
            str += "        public static " + voname + " getVOByID(string id)\n";
            str += "        {\n";
            str += "            string sql = \"select top 1 * from \" + MAIN_TABLE_NAME + \" where id=\" + id;\n";
            str += "            return (" + voname + ")Data.getVO(VO_NAMESPACE, sql);\n";
            str += "        }\n";
            str += "\n";
            str += "\n";
            str += "        /// <summary>\n";
            str += "        /// 根据ID获得单个记录值对象VO\n";
            str += "        /// </summary>\n";
            str += "        public static " + voname + " getVOByRandom(string random)\n";
            str += "        {\n";
            str += "            string sql = \"select top 1 * from \" + MAIN_TABLE_NAME + \" where random='\" + random + \"'\";\n";
            str += "            return (" + voname + ")Data.getVO(VO_NAMESPACE, sql);\n";
            str += "        }\n";
            str += "\n";
            str += "\n";
            str += "    }\n";
            str += "}\n";
            sys.createTextFile("/App_code/bo/" + classname + ".cs", str);
        }

        /// <summary>
        /// 根据表名获得命名
        /// </summary>
        public static string getFileName(string table_name)
        {
            string[] list = table_name.Split('_');
            string name = "";
            for (int i=0;i<list.Length;i++)
            {
                string s= list[i];
                name +=s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length-1);
            }
            return name;
        }


        /// <summary>
        /// 下载外部文件
        /// </summary>
        public static string getMcode(string html)
        {
            string s = html;
            string urllist = "";
            Regex reg = new Regex("<a href=\"http://space.chinaui.com/show/\\?uid=(?<desc>.*)\" target=\"_blank\"><img");
            MatchCollection mc = reg.Matches(s);
            for (int i = 0; i < mc.Count; i++)
            {
                string url = "http://space.chinaui.com/show/?uid=" + mc[i].Groups["desc"].Value;
                urllist += url + "\n";
               // Hashtable ht = new Hashtable();
                //ht.Add("url", url);
                Data.RunSql("insert into gwp_url_list(url) values('" + url + "')");
                //Data.Insert("gwp_url_list",ht);
            }


            return urllist;
        }


        public static string getMcodeFile(string html)
        {
            string fileUrlList = "";
            //html = "xxx aPic[0] = \"螺丝刀|螺丝刀|/108000/108000/108095/2399/8719.jpg|4238|三维习作\" xx ";
            Regex reg = new Regex("\\|/(?<desc>.*).jpg\\|");
            MatchCollection mc = reg.Matches(html);
            for (int i = 0; i < mc.Count; i++)
            {
                string url = "http://myphoto.chinaui.com/res/" + mc[i].Groups["desc"].Value + ".jpg";
                Data.RunSql("insert into gwp_file_list(url) values('" + url + "')");
                fileUrlList += url + "\n";
            }
            return fileUrlList;
        }




        /// <summary>
        /// 下载外部文件
        /// <returns>返回下载文件的物理路径</returns>
        /// </summary>
        public static string downloadfile(HttpContext context,string uri, string folder, string savename)
        {
            if (!System.IO.Directory.Exists(context.Server.MapPath(folder)))
            {
                System.IO.Directory.CreateDirectory(context.Server.MapPath(folder));
            }
            string mappath = lastReplace(AppDomain.CurrentDomain.BaseDirectory, "\\", "");
            string savepath = mappath + folder.Replace("/", "\\") + savename;            
            WebClient wc = new WebClient();
            wc.DownloadFile(uri, savepath);
            return savepath;
        }

      
        public static string getPageUrl(HttpRequest req)
        {
            string Path_Info = req.ServerVariables["Path_Info"].ToString();

            Path_Info = Path_Info.Substring(Path_Info.LastIndexOf("/") + 1);
            Path_Info += "?" + req.ServerVariables["Query_String"].ToString();
            return Path_Info;
        }


        /// <summary>
        /// 获得本月第一天日期
        /// </summary>
        public static string getFirstDate()
        {

            return System.DateTime.Now.Date.Year.ToString() + "-"+System.DateTime.Now.Date.Month.ToString() + "-" +"1";
        }


        /// <summary>
        /// 根据获得星期几
        /// </summary>
        public static int getDayOfWeek(string datetime)
        {

            DateTime d0 = Convert.ToDateTime(datetime);
            return Convert.ToInt32(d0.DayOfWeek);
        }


        /// <summary>
        /// 获得本月数据库中最后一天日期
        /// </summary>
        public static string getLastDate()
        {
            return System.DateTime.Now.Date.Year.ToString() + "-" + System.DateTime.Now.Date.Month.ToString() + "-" + System.DateTime.Now.Date.Day.ToString();
        }

        public static string getBeginTime()
        {
            return System.DateTime.Now.Date.Year.ToString() + "-" + System.DateTime.Now.Date.Month.ToString() + "-" + System.DateTime.Now.Date.Day.ToString();
        }

        public static string getEndTime()
        {
            DateTime dt = System.DateTime.Now;
            dt = dt.AddDays(1);
            return dt.Year.ToString() + "-" + dt.Date.Month.ToString() + "-" + dt.Day.ToString();
        }



        /// <summary>
        /// 计算某个日期与系统时间相隔的年
        /// </summary>
        public static string getComDay(string datetime)
        {
            if (!"".Equals(datetime) && datetime != null)
            {
                DateTime dt1 = DateTime.Now;

                DateTime dt2 = DateTime.Parse(datetime);
                TimeSpan ts = dt1.Subtract(dt2);
                return (ts.Days/365).ToString();
            }
            else
            {
                return "-";
            }

        }

        /// <summary>
        /// 计算某个时间与系统时间的间隔说明
        /// </summary>
        public static string getComTime(Object time)
        {
            string s = "刚刚";
            string datetime = time.ToString();
            if (!"".Equals(datetime) && datetime != null)
            {
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = DateTime.Parse(datetime);
                TimeSpan ts = dt1.Subtract(dt2);
                if (ts.Days > 365)
                {
                    s = (ts.Days / 365) + "年前";
                }
                else if (ts.Days > 30)
                {
                    s = (ts.Days / 30) + "月前";
                }
                else if (ts.Days>6) 
                {
                    s = (ts.Days / 6) + "周前";
                
                }
                else if (ts.Days > 0)
                {
                    s = (ts.Days) + "天前";

                }
                else if (ts.Hours > 0)
                {
                    s = ts.Hours + "小时前";

                }
                else if (ts.Minutes>0)
                {
                    s = ts.Minutes + "分钟前";

                }
                else if (ts.Seconds > 0)
                {
                    s = ts.Seconds + "秒前";

                }
               
            }

            return s;

        }



        /// <summary>
        /// 长日期字符串转换为短日期字符串
        /// </summary>
        public static string dateToShort(object datetime)
        {
            string s = "";
            DateTime time = DateTime.Now;
            try
            {
                time = DateTime.Parse(datetime.ToString());
                s = time.ToString("yyyy-MM-dd");
            }
            catch
            {       
            
            
            }

            return s;

            


        }

        /// <summary>
        /// 长日期字符串转换为指定格式的长日期字符串
        /// </summary>
        public static string dateToLong(object datetime)
        {
            string s = "";
            DateTime time = DateTime.Now;
            try
            {
                time = DateTime.Parse(datetime.ToString());
                s = time.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {


            }
            return s;

        }

        /// <summary>
        /// 长日期字符串转换为短日期字符串
        /// </summary>
        public static string dateToChinese(string datetime)
        {
            DateTime t = DateTime.Parse(datetime);
            return t.Year + "年" + t.Month + "月" + t.Day + "日";
        }


        /// <summary>
        /// 长日期字符串转换为短日期字符串
        /// </summary>
        public static string dateToChineseShort(string datetime)
        {
            DateTime t = DateTime.Parse(datetime);
            return  t.Month + "月" + t.Day + "日";

        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        public static DateTime StampToDateTime(object timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp.ToString() + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dateTimeStart.Add(toNow);
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }


        /// <summary>
        /// 把字符串转为*号
        /// </summary>
        public static string stringAsterisk(string str)
        {
            if (!"".Equals(str) && str != null)
            {
                string s = str.Substring(0,1); 
                for(int i=1;i<str.Length;i++)
                 {
                     s += "*";
                 }
                 return s;
            }
            else
            {
                return "";
            }

        }


        /// <summary>
        /// 清空所有静态缓存数据
        /// </summary>
        public static void clearAllStaticData()
        {
           
        
        }

        /// <summary>
        /// 获得日期不含时间
        /// </summary>
        public static string getDate(string datetime)
        {
            string name = "";
            if (datetime != null && !"".Equals(datetime))
            {
                name = DateTime.Parse(datetime).ToShortDateString();
            }
            return name;
        }



        /// <summary>
        /// ValueObject转json对象字符
        /// </summary>
        public static string voToJsonDovebo(Object ValueObject)
        {
            System.Reflection.PropertyInfo[] ps = ValueObject.GetType().GetProperties();
            string key = "\"key\":{";
            string values = "\"values\":[";
            int index = 0;
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name;
                string value = Convert.ToString(pi.GetValue(ValueObject, null));
                //把 javascript 中的特殊字符转成转义字符
                value = value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace(Convert.ToChar(10).ToString(), "").Replace(Convert.ToChar(13).ToString(), "");

                key += "\""+name+"\":"+index;
                values += "\"" + value + "\"";

                if (index < ps.Length-1)
                {
                    values += ",";
                    key += ",";
                }
                index++;
            }
            values += "]";
            key += "}";
            string json = "{"+key+","+values+"}";
            return json;
        }

        /// <summary>
        /// ValueObject转json对象字符(标准JSON)
        /// </summary>
        public static string voToJson(Object ValueObject)
        {
            System.Reflection.PropertyInfo[] ps = ValueObject.GetType().GetProperties();
            string json = "{";
            int index = 1;
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name;
                string value = Convert.ToString(pi.GetValue(ValueObject, null)).Replace("\"", "").Replace("\n", "").Replace("\r", "").Trim();
                //把 javascript 中的特殊字符转成转义字符
                value = value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace(Convert.ToChar(10).ToString(), "").Replace(Convert.ToChar(13).ToString(), "");
                json += "\"" + name + "\"" + ":\"" + value + "\"";
                if (index != ps.Length)
                {
                    json += ",";
                }
                index++;
            }
            json += "}";
            return json;
        }

        /// <summary>
        /// table list 转json对象字符
        /// </summary>
        public static Hashtable tableToHashtable(DataTable table)
        {

            Hashtable ht = null;
            if (table.Rows.Count > 0)
            {
                ht = new Hashtable();
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    ht.Add(table.Columns[k].ColumnName, table.Rows[0][k].ToString());
                }
            }
            return ht;
        }


        /// <summary>
        /// table list 转json对象字符
        /// </summary>
        public static string tableToJson(DataTable table)
        {
            string json = "{\"count\":\"" + table.Rows.Count + "\",\"key\":{";
            for (int k = 0; k < table.Columns.Count; k++)
            {
                json += "\""+table.Columns[k].ColumnName+"\":\""+k+"\"";
                if (k < table.Columns.Count - 1) { json += ","; }
            }
            
            json += "},\"values\":[";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                json += "[";
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    json += "\"" + table.Rows[i][j].ToString().Replace("\\", "").Replace("\"", "").Replace("\n", "<br>").Replace("\r", "").Trim() + "\"";
                    if (j < table.Columns.Count - 1) { json += ","; }
                }
                json += "]";
                if (i < table.Rows.Count-1){json += ",";}
            }
            json += "]";

            json += "}";
            return json;
        }

        /// <summary>
        /// table list 转json对象字符
        /// </summary>
        public static string tableToAndroidJson(DataTable table)
        {
            string json = "{\"count\":\"" + table.Rows.Count + "\",\"key\":{";

            json += "},\"values\":[";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                json += "{";
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    json +=  "\"" + table.Columns[j].ColumnName + "\":" + "\"" + table.Rows[i][j].ToString().Replace("\"", "").Trim() + "\"";
                    if (j < table.Columns.Count - 1) { json += ","; }
                }
                json += "}";
                if (i < table.Rows.Count - 1) { json += ","; }
            }
            json += "]";

            json += "}";
            return json;
        }



       /// <summary>
        /// DataRow 转 ValueObject
        /// </summary>
        public static Object datarowToValueObject(string class_name, DataRow row)
        {

            Type myType = Type.GetType(class_name);// 获得“类”类型
            Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
            PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                // 循环属性数组，并给数组属性赋值


                for (int k = 0; k < myPropertyInfo1.Length; k++)
                {
                    PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];


                    if (row.Table.Columns[i].ColumnName.ToLower().Equals(myPropInfo.Name.ToLower()))
                    {

                        switch (myPropInfo.PropertyType.ToString())
                        {
                            case "System.Int32":
                                myPropInfo.SetValue(o_Instance, int.Parse(row[i].ToString().Trim()), null);
                                break;
                            case "System.Double":
                                myPropInfo.SetValue(o_Instance, double.Parse(row[i].ToString().Trim()), null);
                                break;
                            case "System.String":
                                myPropInfo.SetValue(o_Instance, sys.showInfo(row[i].ToString().Trim()), null);
                                break;
                            case "System.DateTime":
                                myPropInfo.SetValue(o_Instance, Convert.ToDateTime(row[i].ToString().Trim()), null);
                                break;

                        }

                    }

                }
            }


            return o_Instance;
        }
        

      
        /// <summary>
        /// ValueObject转Hashtable
        /// </summary>
        public static Hashtable voToHashtable(Object ValueObject)
        {
            System.Reflection.PropertyInfo[] ps = ValueObject.GetType().GetProperties();
            Hashtable ht = new Hashtable();
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name;
                object value = pi.GetValue(ValueObject, null);
                if (value!=null)
                {
                    ht.Add(name, Convert.ToString(value));
                }

            }

            return ht;
        }

        /// <summary>
        /// ValueObject转 XML
        /// </summary>
        public static string hashtableToXML(Hashtable ht)
        {
            string xml = "<xml>";

            IDictionaryEnumerator ide = ht.GetEnumerator();
            while (ide.MoveNext())
            {
                xml += "<" + ide.Key.ToString() + "><![CDATA[" + ide.Value.ToString() + "]]></" + ide.Key.ToString() + ">";                
            }

            xml += "</xml>";

            return xml;
        }

        /// <summary>
        /// ValueObject转 XML
        /// </summary>
        public static string voToXML(Object ValueObject)
        {
            string xml = "<xml>";
            System.Reflection.PropertyInfo[] ps = ValueObject.GetType().GetProperties();
            Hashtable ht = new Hashtable();
            foreach (System.Reflection.PropertyInfo pi in ps)
            {
                //Name 为属性名称,GetValue 得到属性值(参数this 为对象本身,null)
                string name = pi.Name;
                object value = pi.GetValue(ValueObject, null);
                if (value != null)
                {
                    xml += "<" + name + "><![CDATA[" + value + "]]></" + name + ">";                    
                }

            }
            xml += "</xml>";

            return xml;
        }



        /// <summary>
        /// Hashtable转Json
        /// </summary>
        public static string hashtableToJson(Hashtable ht)
        {

            string json = "{";

            IDictionaryEnumerator ide = ht.GetEnumerator();
            while (ide.MoveNext())
            {
                json += "\"" + ide.Key.ToString() + "\":\"" + ide.Value.ToString() + "\",";
            }
            json += "}";
            json = json.Replace(",}", "}");
            return json;

        }


        /// <summary>
        /// json转vo
        /// </summary>
        public static Object xmlToVO(XmlDocument xml, string class_name)
        {

            XmlNodeList xml_list = xml.SelectSingleNode("xml").ChildNodes;



            //对应到  VO 值对象
            Type myType = Type.GetType(class_name);// 获得“类”类型
            Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
            PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < xml_list.Count; i++)
            {
                

                for (int k = 0; k < myPropertyInfo1.Length; k++)
                {
                    PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];
                    if (xml_list.Item(i).Name.ToLower().Equals(myPropInfo.Name.ToLower()))
                    {
                        switch (myPropInfo.PropertyType.ToString())
                        {
                            case "System.Int32":
                                myPropInfo.SetValue(o_Instance, int.Parse(xml_list.Item(i).InnerText), null);
                                break;
                            case "System.Double":
                                myPropInfo.SetValue(o_Instance, double.Parse(xml_list.Item(i).InnerText), null);
                                break;
                            case "System.String":
                                myPropInfo.SetValue(o_Instance, xml_list.Item(i).InnerText, null);
                                break;
                        }

                    }

                }
            }


            return o_Instance;

        }


        /// <summary>
        /// json转Hashtable（只支持一层xml结构）
        /// </summary>
        public static Hashtable xmlToHashtable(string xmlData)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlData);

            Hashtable ht = new Hashtable();
            XmlNodeList xml_list = xml.SelectSingleNode("xml").ChildNodes;
            for (int i = 0; i < xml_list.Count; i++)
            {
                ht.Add(xml_list.Item(i).Name, xml_list.Item(i).InnerText);
            }
            return ht;

        }


        /// <summary>
        /// json转vo
        /// </summary>
        public static Object jsonToVO(string jsonData, string class_name)
        {

            JsonReader reader = new JsonTextReader(new StringReader(jsonData));
            //JObject json = (JObject)JObject.Parse(jsonData);

            //对应到  VO 值对象
            Type myType = Type.GetType(class_name);// 获得“类”类型
            Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
            PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            while (reader.Read())
            {
                if ("PropertyName".Equals(reader.TokenType.ToString()))
                {

                    string key = reader.Value.ToString();
                    reader.Read();

                    for (int k = 0; k < myPropertyInfo1.Length; k++)
                    {
                        PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];

                        if (key.ToLower().Equals(myPropInfo.Name.ToLower()))
                        {

                            switch (myPropInfo.PropertyType.ToString())
                            {
                                case "System.Int32":
                                    myPropInfo.SetValue(o_Instance, int.Parse(reader.Value.ToString()), null);
                                    break;
                                case "System.Double":
                                    myPropInfo.SetValue(o_Instance, 0, null);
                                    break;
                                case "System.String":
                                    myPropInfo.SetValue(o_Instance, reader.Value.ToString(), null);
                                    break;
                                case "System.DateTime":
                                    myPropInfo.SetValue(o_Instance, "", null);
                                    break;
                            }

                        }



                    }

                }

            }

            return o_Instance;

        }



        /// <summary>
        /// 检查数据是否为空
        /// </summary>
        public static bool isNotNull(Object o)
        {
            if (o != null)
            {
                string s = o.ToString();
                if (s != null && !"".Equals(s.Trim()))
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 格式化  textarea 发布的文字显示
        /// </summary>
        public static string formatDisplayText(string s)
        {
            if (isNotNull(s))
            {
                s = s.Replace("\n", "<br>").Replace("\r", "").Replace(" ", "&nbsp;").Trim();
            }
           

            //s = replaceURL(s);
            return s;
        }

        /// <summary>
        /// 检查数据是否为空
        /// </summary>
        public static bool isNull(Object o)
        {
            return !isNotNull(o);
        }


        /// <summary>
        /// 检查数据是否为空
        /// </summary>
        public static bool isNumber(string s)
        {
            bool b = false;

            try
            {
                int i = int.Parse(s);
                b = true;
            }
            catch
            {

            }
            return b;
        }

        /// <summary>
        /// 检查数据是否为空
        /// </summary>
        public static bool isFloat(string s)
        {
            bool b = false;

            try
            {
                float i = float.Parse(s);
                b = true;
            }
            catch
            {

            }
            return b;
        }


        /// <summary>
        /// 检查数据是否为空
        /// </summary>
        public static bool isDouble(string s)
        {
            bool b = false;

            try
            {
                double i = double.Parse(s);
                b = true;
            }
            catch
            {

            }
            return b;
        }




        /// <summary>
        ///  单行记录table 转json对象字符,一般是 select top 1 a.x ,b.x,b.xx from ....
        /// </summary>
        public static string tableFirstToJson(DataTable table)
        {
            string json = "{";
            int i = 0;
            if (table.Rows.Count > 0)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    json += "\"" + table.Columns[j].ColumnName + "\":" + "\"" + table.Rows[i][j].ToString().Replace("\"", "").Trim() + "\"";
                    if (j < table.Columns.Count - 1) { json += ","; }
                }
                if (i < table.Rows.Count - 1) { json += ","; }
            }
            json += "}";
            return json;
        }










        public static string replaceURL(string strContent)
	    {
           //参考
           // Regex urlregex = new Regex(@"(http:\/\/([\w.]+\/?)\S*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            


            //链接
            Regex urlregex = new Regex(@"((http?|ftp|file):\/\/[-a-zA-Z0-9+&@#\/%?=~_|!:,.;]\S*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            strContent = urlregex.Replace(strContent, "<a href=\"$1\" target=\"_blank\">$1</a>");


            //@功能
            Regex urlregex2 = new Regex(@"(@\S{2,14}\s*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            strContent = urlregex2.Replace(strContent, "<a href=\"/u/index.aspx?nickname=$1&q=1\" >$1</a>");
            strContent = strContent.Replace("nickname=@", "nickname=");

            //## 话题功能
            Regex urlregex3 = new Regex(@"(#\S{2,14}#)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            strContent = urlregex3.Replace(strContent, " <a href=\"/u/search_mblog.aspx?method=mblog&sh_key=$1&q=1\" >$1</a> ");
            strContent = strContent.Replace("sh_key=#", "sh_key=%23");
            strContent = strContent.Replace("#&q=1", "%23&q=1");


            //表情
            Regex urlregex4 = new Regex(@"(\[face\d{1,2}\])", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            strContent = urlregex4.Replace(strContent, "<img src=\"/u/images/face/$1xxx.gif\">");
            strContent = strContent.Replace("/u/images/face/[", "/u/images/face/");
            strContent = strContent.Replace("]xxx.gif", ".gif");

            //strContent = strContent.replace(/(\[face\d{1,2}\])/g,'<img src="/u/images/face/$1xxx.gif"> ');
			  
			//  srcString = srcString.replace(/\/u\/images\/face\/\[/g, "/u/images/face/");
			//  srcString = srcString.replace(/\]xxx.gif/g, ".gif");		



            return strContent;

            //System.Web.HttpContext.Current.Server.UrlEncode


            //javascript
            //return (srcString.Replace(/((http?|ftp|file):\/\/[-a-zA-Z0-9+&@#\/%?=~_|!:,.;]*)/g,"<a href=\"$1\" target=_blank>$1</a>"));
		}

        //获得服务器的域名路径，包含端口
        public static string getServerPath(HttpRequest req)
        {

            string serverpath = req.ServerVariables["SERVER_NAME"];
            bool ishascurrentport = false;
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                if (ConfigurationManager.ConnectionStrings[i].Name == "CURRENTPORT")
                {
                    ishascurrentport = true;
                }
            }
            if (ishascurrentport)
            {
                string currentport = ConfigurationManager.ConnectionStrings["CURRENTPORT"].ConnectionString;
                if (req.ServerVariables["SERVER_PORT"] != null && !"".Equals(req.ServerVariables["SERVER_PORT"]) && !currentport.Equals(req.ServerVariables["SERVER_PORT"])) { serverpath += ":" + req.ServerVariables["SERVER_PORT"]; }
            }
            else
            {
                if (req.ServerVariables["SERVER_PORT"] != null && !"".Equals(req.ServerVariables["SERVER_PORT"]) && !"80".Equals(req.ServerVariables["SERVER_PORT"])) { serverpath += ":" + req.ServerVariables["SERVER_PORT"]; }
            }
            return "http://"+serverpath;

        }

        //获得服务器的域名路径，包含端口
        public static string getServerPath()
        {
            HttpRequest req = HttpContext.Current.Request;
            string serverpath = req.ServerVariables["SERVER_NAME"];
            bool ishascurrentport = false;
            for (int i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++) {
                if (ConfigurationManager.ConnectionStrings[i].Name == "CURRENTPORT") {
                    ishascurrentport = true;
                }
            }
            if (ishascurrentport)
            {
                string currentport = ConfigurationManager.ConnectionStrings["CURRENTPORT"].ConnectionString;
                if (req.ServerVariables["SERVER_PORT"] != null && !"".Equals(req.ServerVariables["SERVER_PORT"]) && !currentport.Equals(req.ServerVariables["SERVER_PORT"])) { serverpath += ":" + req.ServerVariables["SERVER_PORT"]; }
            }
            else
            {
                if (req.ServerVariables["SERVER_PORT"] != null && !"".Equals(req.ServerVariables["SERVER_PORT"]) && !"80".Equals(req.ServerVariables["SERVER_PORT"])) { serverpath += ":" + req.ServerVariables["SERVER_PORT"]; }
            }
            return "http://" + serverpath;

        }

        public static string getPubDate(string time)
        {
            if (time == null) { time = DateTime.Now.ToString(); }
            return DateTime.Parse(time).ToString("ddd  dd  MMMM yyyy hh:mm:ss 'GMT'", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }


        //去掉html标签
        public static string removeXMLHTMLTag(string s)
        {


            //链接
            s = new Regex(@"(<a(.[^>]*)>)", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(s, "");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&gt;");
            s = s.Replace("\n", "");
            s = s.Replace("\r", "");
            s = Regex.Replace(s, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            s = s.Replace("&nbsp;", "　");
            s = s.Replace("\"", "");
            return s;
        }

        //去掉html表圈
        public static string removeHTMLTag(Object o)
        {
            string s = o.ToString();
            return Regex.Replace(s, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase); 

        }


        public static string encode(string value)
        {
            return System.Web.HttpContext.Current.Server.UrlEncode(value) + "";
        }


        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }






        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static string[] GetHtmlLinkUrlList(string sHtmlText)
        {
            



            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<a\b[^<>]*?\bhref[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }


        static Random imgHostRand = new Random();
        /// <summary> 
        /// 图片服务器 
        /// </summary> 
        /// <returns>http://img1.eqingyu.com</returns> 
        public static string GetImgHost()
        {
            
            string so = "1,2,3,4";
            string[] strArr = so.Split(',');
            string code = strArr[imgHostRand.Next(0, strArr.Length)];
            return "http://img"+code+".eqingyu.com";
        }


        /// <summary> 
        /// 图片服务器 
        /// </summary> 
        /// <returns>http://img1.eqingyu.com</returns> 
        public static string lastReplace(string str,string oldString,string newString)
        {
            string s = "/system/images/error.png";
            if (isNotNull(str))
            {
               
                int index = str.LastIndexOf(oldString);
                //s = index + "";
                if (index > -1)
                {
                    string str0 = str.Substring(0, index);
                    string str1 = str.Substring(index + oldString.Length, str.Length - index - oldString.Length);
                    s = str0 + newString + str1;
                }
                //s = str0 + "-" + index +"-";
            }
            else
            {
                s = "/system/images/error.jpg";
            }
            return s;			
        }


        /// <summary> 
        /// 获取客户端IP
        /// </summary> 
        /// <returns>http://img1.eqingyu.com</returns> 
        public static string GetWebClientIp()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }


        /// <summary> 
        /// 获取物理路径
        /// </summary> 
        /// <returns>D:\微云网盘\webwork\口袋大兄\weixin.koudai.com\log\timer\</returns> 
        public static string GetServerMapPath(string path)
        {

            string mappath = lastReplace(AppDomain.CurrentDomain.BaseDirectory, "\\", "");

            mappath += (path.Replace("/", "\\"));

            return mappath;


        }

        /// <summary>
        /// 获得时间戳
        /// </summary>
        public static long getLongTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1); //得到1970年的时间戳
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000; //注意这里有时区
            return a;
        }




        /// <summary> 
        /// 获得星期几的日期  2014-12-12 22:30:30
        /// </summary> 
        /// <returns>2014-12-12 22:30:30</returns> 
        public static string getNextWeekdayDate(int nextWeekday,int hours,int minute)
        {
            DateTime overtime = DateTime.Now;

            int overWeek = getWeekday(overtime);//得到今天是星期几
            if (nextWeekday - overWeek >= 0)
            {
                overtime = overtime.AddDays(nextWeekday - overWeek);
            }
            else
            {
                overtime = overtime.AddDays(7 - Math.Abs(nextWeekday - overWeek));
            
            }
            DateTime datetime = DateTime.Parse(overtime.ToString("yyyy-MM-dd") + " " + hours + ":" + minute + ":00");

            DateTime systemtime = DateTime.Now;

            TimeSpan ts = systemtime.Subtract(datetime);


            //同一天必须计算是大于还是小于当前系统时间
            if(ts.Days==0)
            {
                if (ts.Hours>0)
                {
                    datetime = datetime.AddDays(7);

                }
                else if (ts.Minutes > 0)
                {

                    datetime = datetime.AddDays(7);
                }
                else if (ts.Seconds > 0)
                {
                    datetime = datetime.AddDays(7);

                }
            
            }
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary> 
        /// 获得某天是星期几
        /// </summary> 
        /// <returns>1-7</returns> 
        public static int getWeekday(DateTime time)
        {
            int week = 0;

            string weekstr = time.DayOfWeek.ToString();
            switch (weekstr)
            {
                case "Monday": week = 1; break;
                case "Tuesday": week = 2; break;
                case "Wednesday": week = 3; break;
                case "Thursday": week = 4; break;
                case "Friday": week = 5; break;
                case "Saturday": week = 6; break;
                case "Sunday": week = 7; break;
            }

            return week;



        }


        /// <summary> 
        /// 获得农历 
        /// </summary> 
        /// <returns>http://img1.eqingyu.com</returns> 
        public static string cDate(DateTime dt)
        {
            ChineseLunisolarCalendar l = new ChineseLunisolarCalendar();
            //DateTime dt = DateTime.Today; //转换当日的日期　　//
            // dt = new DateTime(2006, 1,29);
            //农历2006年大年初一（测试用），也可指定日期转换　　
            string[] aMonth ={ "", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "腊月", "腊月" };　　//a10表示日期的十位!　　
            string[] a10 ={ "初", "十", "廿", "卅" }; string[] aDigi ={ "O", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string sYear = "", sYearArab = "", sMonth = "", sDay = "", sDay10 = "", sDay1 = "", sLuniSolarDate = "";
            int iYear, iMonth, iDay; iYear = l.GetYear(dt); iMonth = l.GetMonth(dt); iDay = l.GetDayOfMonth(dt);　　//Format Year　　
            sYearArab = iYear.ToString();
            for (int i = 0; i < sYearArab.Length; i++)
            {
                sYear += aDigi[Convert.ToInt16(sYearArab.Substring(i, 1))];
            }
            //Format Month　　
            int iLeapMonth = l.GetLeapMonth(iYear);
            //获取闰月　　
            if (iLeapMonth > 0 && iMonth <= iLeapMonth)
            {
                //
                for (int i = iLeapMonth + 1; i < 13; i++)
                {
                    aMonth[i] = aMonth[i - 1];
                    aMonth[iLeapMonth] = "闰" + aMonth[iLeapMonth - 1];
                    sMonth = aMonth[l.GetMonth(dt)];
                }
            }
            else if (iLeapMonth > 0 && iMonth > iLeapMonth)
            {
                sMonth = aMonth[l.GetMonth(dt) - 1];
            }
            else
            {
                sMonth = aMonth[l.GetMonth(dt)];
            }
            //Format Day　　
            sDay10 = a10[iDay / 10];
            sDay1 = aDigi[(iDay % 10)];
            sDay = sDay10 + sDay1;
            if (iDay == 10) sDay = "初十";
            if (iDay == 20) sDay = "二十";
            if (iDay == 30) sDay = "三十";
            //Format Lunar Date　　//
            sLuniSolarDate = dt.Year + "年" + dt.Month + "月" + dt.Day + "日 " + Weeks(dt) + " 农历" + sYear + "年" + sMonth + sDay;
            sLuniSolarDate = "" + sMonth + sDay;
            return sLuniSolarDate;
        }


        public static string Weeks(DateTime dt)
        {

            string Weeken = dt.DayOfWeek.ToString();

            switch (Weeken)
            {
                case "Monday":
                    return "一";
                    break;
                case "Tuesday":
                    return "二";
                    break;
                case "Wednesday":
                    return "三";
                    break;
                case "Thursday":
                    return "四";
                    break;
                case "Friday":
                    return "五";
                    break;
                case "Saturday":
                    return "六";
                    break;
                case "Sunday":
                    return "日";
                    break;
                default: return " ";
            }
        }


        ///<summary>
        ///获得用户的文件目录
        ///用来存储与获取用户的专用文件
        ///</summary>
        ///<param name="uid">用户ID</param>        
        ///<returns>如： /upload/iuser/000/002/199/ </returns>
        public static string getUserFolder(string uid)
        {
            string folder = "/upload/iuser/";
            string left = "";
            for (int i = uid.Length; i < 9; i++)
            {
                left += "0";
            }
            string ufolder = left+uid;
            string newfolder = "";
            for (int i = 0; i < ufolder.Length;i++ )
            {
                newfolder += ufolder.Substring(i,1);
                if(i%3==2)
                {
                    newfolder += "/";
                }
            }
            return folder + newfolder;
        }


        ///<summary>
        ///采用https协议访问网络
        ///</summary>
        ///<param name="URL">url地址</param>
        ///<param name="strPostdata">发送的数据</param>
        ///<returns></returns>
        public static string MethodPOST(string URL, string strPostdata, string strEncoding)
        {
            Encoding encoding = System.Text.Encoding.GetEncoding(strEncoding);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "post";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] buffer = encoding.GetBytes(strPostdata);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(strEncoding)))
            {
                return reader.ReadToEnd();
            }
        }


        /// <summary>
        /// 获得本地或远程请求的页面HTML源代码
        /// </summary>
        public static string MethodGET(string url, string encoding)
        {

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encoding);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream resStream = response.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(resStream, System.Text.Encoding.GetEncoding(encoding));
            string str = sr.ReadToEnd();
            resStream.Close();
            sr.Close();

            return str;
        }

        /// <summary>
        ///获得当前服务器的域名路径，包含端口（被getServerPath搞死了，只好重新写方法）
        /// </summary>
        public static string getServerPathOver()
        {
            HttpRequest req = HttpContext.Current.Request;
            string serverpath = req.ServerVariables["SERVER_NAME"];
            if (req.ServerVariables["SERVER_PORT"] != null && !"".Equals(req.ServerVariables["SERVER_PORT"]) && !"80".Equals(req.ServerVariables["SERVER_PORT"])) { serverpath += ":" + req.ServerVariables["SERVER_PORT"]; }
            return "http://" + serverpath;

        }

        //获得服务器的域名路径，包含端口
        public static string getWechatServerPath()
        {
            return ConfigurationManager.ConnectionStrings["WechatPath"].ConnectionString;

        }

        //获得服务器的域名路径，包含端口
        public static string getFileServerPath()
        {
            return ConfigurationManager.ConnectionStrings["FilePath"].ConnectionString;

        }

        /// <summary>
        /// 创建匹配数据库表的列的DataRow的空DataTable
        /// </summary>
        public static DataTable getDataTable(string tablename)
        {
            string sql = "SELECT syscolumns.name column_name ,systypes.name type_name  FROM syscolumns, systypes WHERE syscolumns.xusertype = systypes.xusertype  AND syscolumns.id = object_id('" + tablename + "')";
            DataTable table = Data.getDataTable(sql);
            DataTable returnDataTable = new DataTable();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                if ("int".Equals(table.Rows[i]["type_name"].ToString()))
                {
                    returnDataTable.Columns.Add(table.Rows[i]["column_name"].ToString(), typeof(int));
                }
                else if ("float".Equals(table.Rows[i]["type_name"].ToString()))
                {
                    returnDataTable.Columns.Add(table.Rows[i]["column_name"].ToString(), typeof(float));
                }
                else if ("nvarchar".Equals(table.Rows[i]["type_name"].ToString()) || "ntext".Equals(table.Rows[i]["type_name"].ToString()) || "datetime".Equals(table.Rows[i]["type_name"].ToString()) || "time".Equals(table.Rows[i]["type_name"].ToString()))
                {
                    returnDataTable.Columns.Add(table.Rows[i]["column_name"].ToString(), typeof(string));
                }              
            }

            return returnDataTable;
        }

        public static string CreateQRCode(string szSpaceID, string szURL)
        {
            string szFolder = "/QRCode/"+ szSpaceID +"/" + sys.getDateStr() + "/";
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(szFolder)))
            {
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(szFolder));
            }
            string szFileName = sys.getRandomStr() + ".png";
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 16;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(szURL);
            image.Save(System.Web.HttpContext.Current.Server.MapPath(szFolder + szFileName), System.Drawing.Imaging.ImageFormat.Png);
            image.Dispose();
            return getServerPath() + szFolder + szFileName;
        }
        
    }
}
