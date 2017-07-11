using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Reflection;
using Redsz.DAO;
using Redsz.VO;
using Redsz;

/// <summary>
/// BlogBO 的摘要说明
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysAdminBO
    {
        //BO对应主表
        public static string MAIN_TABLE_NAME = "sys_admin";
        //对应的值对象命名空间
        public static string VO_NAMESPACE = "Redsz.VO.SysAdminVO";
        //列表SQL
        public static string LIST_SQL_begin = "select a.*,b.rolename rolename from " + MAIN_TABLE_NAME + " a," + SysRoleBO.MAIN_TABLE_NAME + " b where a.roleid=b.id ";
        public static string LIST_SQL_end = " order by a.topsize asc,a.datetime desc,a.id desc";
        public static string LIST_SQL = LIST_SQL_begin + LIST_SQL_end;

        public static string LIST_TOP_SQL_begin = "select count(*) from ( select a.*,b.rolename rolename from " + MAIN_TABLE_NAME + " a," + SysRoleBO.MAIN_TABLE_NAME + " b where a.roleid=b.id  ";
        public static string LIST_TOP_SQL_end = " ) c";

        //列表RowCount
        private string _LIST_COUNT_SQL = "";

        public string LIST_COUNT_SQL
        {
            get { return _LIST_COUNT_SQL; }
            set { _LIST_COUNT_SQL = value; }
        }


        /// <summary>
        /// 管理员登录
        /// </summary>
        public static string adminLogin(String username, String password, bool httponly)
        {
            string s = "{\"success\":false,\"message\":\"用户名或密错误！\"}";
            password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5");
            SysAdminVO vo = getVOByUserName(username);
            if (vo != null&& vo.id>0)
            {
                if (vo.username.Equals(username) && vo.password.Equals(password))
                {

                    HttpCookie cookie = new HttpCookie("adminInfo");

                    if (httponly)
                    {
                        cookie.HttpOnly = true;
                    }
                    else
                    {
                        DateTime dt = DateTime.Now;
                        TimeSpan ts = new TimeSpan(7, 0, 0, 0);
                        cookie.Expires = dt.Add(ts);
                    }
                    SysRoleVO sysrolevo = SysRoleBO.getVOByID(vo.roleid.ToString());

                    cookie.Values.Add("admin_username", HttpUtility.UrlEncode(vo.username.ToString()));
                    cookie.Values.Add("admin_id", HttpUtility.UrlEncode(vo.id.ToString()));
                    cookie.Values.Add("admin_role_id", HttpUtility.UrlEncode(vo.roleid.ToString()));
                    cookie.Values.Add("admin_role_issuper", HttpUtility.UrlEncode(sysrolevo.issuper.ToString()));
                    cookie.Values.Add("admin_lastlogin", HttpUtility.UrlEncode(vo.lastdatetime.ToString()));
                    cookie.Values.Add("admin_loginsize", HttpUtility.UrlEncode(vo.loginsize.ToString()));
                    cookie.Values.Add("password", HttpUtility.UrlEncode(vo.password.ToString()));
                    System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                    s = "{\"success\":true,\"vo\":" + UtilBO.voToJson(vo) + "}";
                    Data.RunSql("update " + MAIN_TABLE_NAME + " set lastdatetime='" + System.DateTime.Now + "',loginsize=loginsize+1 where username='" + vo.username.ToString() + "'");
                }

            }
            return s;
        }

        /// <summary>
        /// 管理员注销登录
        /// </summary>
        public static void adminLogOut(HttpRequest req, HttpResponse res)
        {
            HttpCookie cookie = req.Cookies["adminInfo"];
            if (cookie != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie.Values.Clear();
                System.Web.HttpContext.Current.Response.Cookies.Set(cookie);




            }
            res.Redirect("/");
        }















        /// <summary>
        /// 新增
        /// </summary>
        public static string addAdmin(HttpContext contex)
        {
            HttpRequest req = contex.Request;
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());


                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);

                SysAdminVO vo=getVOByUserName(ht["username"].ToString());
                if (vo.id>0)
                {
                    return s = "{\"success\":false,\"message\":\"用户名已存在！\"}";
                }
                ht["password"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(req.Form["password"], "md5");

                Data.Insert(MAIN_TABLE_NAME, ht);
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public static string updateAdmin(HttpContext contex)
        {
            HttpRequest req = contex.Request;
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE, false);

                SysAdminVO vo = getVOByUserName(ht["username"].ToString());
                if (vo.id > 0&& vo.id.ToString()!=ht["id"].ToString())
                {
                    return s = "{\"success\":false,\"message\":\"用户名已存在！\"}";
                }
                if (!"".Equals(req.Form["password"]))
                {
                    ht["password"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(req.Form["password"], "md5");
                }
                Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                s = "{\"success\":true,\"message\":\"\"}";

            }
            return s;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public static string deleteAdmin(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                SysAdminVO adminvo = getVOByID(req["id"]);
                Data.RunSql("delete  " + MAIN_TABLE_NAME + " where id=" + req["id"]);
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// 排序
        /// </summary>
        public static void TopSize(HttpRequest req)
        {
            Data.TopSize(MAIN_TABLE_NAME, "id", "topsize", req);
        }


        /// <summary>
        /// 删除(从列表中选择多个删除)
        /// </summary>
        public static void DelList(HttpRequest req)
        {
            Data.DelList(MAIN_TABLE_NAME, "id", req);
        }


        //获得所有记录
        public static DataTable getAll()
        {
            return Data.TopList(LIST_SQL);
        }


        //获得所有记录的 Hastable
        public static Hashtable getHastable()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = getAll();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ht.Add(dt.Rows[i]["username"], dt.Rows[i]["id"]);
            }
            return ht;
        }

        /// <summary>
        /// 获得列表，含分页参数
        /// </summary>
        public static QueryVO getList(HttpRequest req, string psize)
        {
            string sql_query = "select a.*,(select rolename from sys_role where id=a.roleid) rolename from " + MAIN_TABLE_NAME + " a  where 1=1 ";
            string keyword = req["keyword"];
            if (UtilBO.isNotNull(keyword))
            {
                sql_query += " and (a.zname like '%" + keyword + "%'  )  ";
            }
            string sql_count = sql_query;
            sql_query += " order by a.id asc ";
            return Data.getQueryList(req, psize, sql_query, sql_count);
        }

        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static SysAdminVO getvo(string id)
        {
            string sql = "select * from " + MAIN_TABLE_NAME;
            if (id != null)
            {
                sql += " where id=" + id;
            }
            sql += " order by id asc";
            return (SysAdminVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 根据用户名和密码获得用户信息
        /// </summary>
        public static SysAdminVO getVOByUserName(string username)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME;
            sql += " where username='" + username + "' ";
            return (SysAdminVO)Data.getVO(VO_NAMESPACE, sql);
        }

        /// <summary>
        /// 根据用户名和密码获得用户信息
        /// </summary>
        public static SysAdminVO getvo(string username, string password)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME;
            sql += " where username='" + username + "' and password='" + password + "'";
            return (SysAdminVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 根据用户名和密码获得用户信息
        /// </summary>
        public static SysAdminVO getVOByID(string id)
        {
            string sql = "select * from " + MAIN_TABLE_NAME + " where id=" + id;
            return (SysAdminVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 根据用户名和密码获得用户信息
        /// </summary>
        public static SysAdminVO getVOBySession(HttpRequest req)
        {
            string username = "";

            if (req.Cookies["adminInfo"] != null)
            {
                username = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
            }

            string sql = "select * from " + MAIN_TABLE_NAME + " where username='" + username + "'";
            return (SysAdminVO)Data.getVO(VO_NAMESPACE, sql);
        }




    }
}