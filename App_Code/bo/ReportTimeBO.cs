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
/// 创建时间:2017/5/24 11:11:36 
/// ReportTimeBO业务对象 (此文件由框架自动生成 v2.0)
/// 说明：
/// QQ:3968666 email:leijiangbo@163.com  mob:18510727587
/// </summary>
namespace Redsz.BO
{
    /// <summary>
    /// 业务对象
    /// </summary>
    public class ReportTimeBO
    {
        /// <summary>
        /// BO的数据库表名
        /// </summary>
        public static string MAIN_TABLE_NAME = "report_time";
        /// <summary>
        /// VO的命名空间
        /// </summary>
        public static string VO_NAMESPACE = "Redsz.VO.ReportTimeVO";

        /// <summary>
        /// 新增
        /// </summary>
        public static string Add(HttpContext context)
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string admin_id = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                string admin_role_id = req.Cookies["adminInfo"].Values["admin_role_id"].ToString();
                string random = sys.getRandomStr();
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                ht.Add("createtime", DateTime.Now.ToString());
                ht.Add("random", random);
                Data.Insert(MAIN_TABLE_NAME, ht);
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// 修改
        /// </summary>
        public static string Modify(HttpContext context)
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string admin_id = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                string admin_role_id = req.Cookies["adminInfo"].Values["admin_role_id"].ToString();
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                ReportTimeVO vo = getVOByID(ht["id"].ToString());
                //判断此记录是否存在
                if (vo.id > 0)
                {
                    Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                    s = "{\"success\":true,\"message\":\"\"}";
                }
                else
                {
                    s = "{\"success\":false,\"message\":\"记录不存在\"}";
                }
            }
            return s;
        }


        /// <summary>
        /// 删除
        /// </summary>
        public static string Delete(HttpContext context)
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string admin_id = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                string admin_role_id = req.Cookies["adminInfo"].Values["admin_role_id"].ToString();
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                ReportTimeVO vo = getVOByID(ht["id"].ToString());
                //判断此记录是否存在
                if (vo.id > 0)
                {
                    Data.Del(MAIN_TABLE_NAME, "id", ht);
                    s = "{\"success\":true,\"message\":\"\"}";
                }
                else
                {
                    s = "{\"success\":false,\"message\":\"记录不存在\"}";
                }
            }
            return s;
        }


        /// <summary>
        /// 排序
        /// </summary>
        public static string TopSize(HttpContext context)
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id", req["id"]);
                ht.Add("topsize", req["topsize"]);
                Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// 获得信息分页列表
        /// </summary>
        public static QueryVO getList(HttpContext context, string psize)
        {
            HttpRequest req = context.Request;
            string admin_id = req.Cookies["adminInfo"].Values["admin_id"].ToString();
            string admin_role_id = req.Cookies["adminInfo"].Values["admin_role_id"].ToString();
            string sql_query = "select id  from " + MAIN_TABLE_NAME + " where 1=1 ";
            string sql_count = sql_query;
            sql_query += " order by id desc ";
            return Data.getQueryList(req, psize, sql_query, sql_count);
        }


        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static ReportTimeVO getVOByID(string id)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where id=" + id;
            return (ReportTimeVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static ReportTimeVO getVOByRandom(string random)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where random='" + random + "'";
            return (ReportTimeVO)Data.getVO(VO_NAMESPACE, sql);
        }

        /// <summary>
        /// 根据时间获得单个记录值对象VO
        /// </summary>
        public static ReportTimeVO getVOByTime(DateTime time)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where year=" + time.Year + " and month=" + time.Month;
            return (ReportTimeVO)Data.getVO(VO_NAMESPACE, sql);
        }

    }
}
