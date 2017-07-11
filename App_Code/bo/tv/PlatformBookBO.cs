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
/// ����ʱ��:2017/6/6 11:22:54 
/// PlatformBookBOҵ����� (���ļ��ɿ���Զ����� v2.0)
/// ˵����
/// QQ:3968666 email:leijiangbo@163.com  mob:18510727587
/// </summary>
namespace Redsz.BO
{
    /// <summary>
    /// ҵ�����
    /// </summary>
    public class PlatformBookBO
    {
        /// <summary>
        /// BO�����ݿ����
        /// </summary>
        public static string MAIN_TABLE_NAME = "platform_book";
        /// <summary>
        /// VO�������ռ�
        /// </summary>
        public static string VO_NAMESPACE = "Redsz.VO.PlatformBookVO";

        /// <summary>
        /// ����
        /// </summary>
        public static string Add(HttpContext context)
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string admin_id = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                string admin_role_id = req.Cookies["adminInfo"].Values["admin_role_id"].ToString();
                string random = sys.getRandomStr();
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                ht.Add("createtime",DateTime.Now.ToString());
                ht.Add("random", random);
                Data.Insert(MAIN_TABLE_NAME , ht);               
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// �޸�
        /// </summary>
        public static string Modify(HttpContext context)
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string admin_id = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                string admin_role_id = req.Cookies["adminInfo"].Values["admin_role_id"].ToString();
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                PlatformBookVO vo = getVOByID(ht["id"].ToString());
                //�жϴ˼�¼�Ƿ����
                if (vo.id > 0)
                {
                    Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                    s = "{\"success\":true,\"message\":\"\"}";
                }
                else
                {
                    s = "{\"success\":false,\"message\":\"��¼������\"}";
                }               
            }
            return s;
        }


        /// <summary>
        /// ɾ��
        /// </summary>
        public static string Delete(HttpContext context)
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string admin_id = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                string admin_role_id = req.Cookies["adminInfo"].Values["admin_role_id"].ToString();
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                PlatformBookVO vo = getVOByID(ht["id"].ToString());
                //�жϴ˼�¼�Ƿ����
                if (vo.id > 0)
                {
                    Data.Del(MAIN_TABLE_NAME, "id", ht);
                    s = "{\"success\":true,\"message\":\"\"}";
                }
                else
                {
                    s = "{\"success\":false,\"message\":\"��¼������\"}";
                }
            }
            return s;
        }


        /// <summary>
        /// ����
        /// </summary>
        public static string TopSize(HttpContext context) 
        {
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                Hashtable ht = new Hashtable();
                ht.Add("id",req["id"]);
                ht.Add("topsize", req["topsize"]);
                Data.Update(MAIN_TABLE_NAME,"id=@id",ht);
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// �����Ϣ��ҳ�б�
        /// </summary>
        public static QueryVO getList(HttpContext context,string psize)
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
        /// ����ID��õ�����¼ֵ����VO
        /// </summary>
        public static PlatformBookVO getVOByID(string id)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where id=" + id;
            return (PlatformBookVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// ����ID��õ�����¼ֵ����VO
        /// </summary>
        public static PlatformBookVO getVOByRandom(string random)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where random='" + random + "'";
            return (PlatformBookVO)Data.getVO(VO_NAMESPACE, sql);
        }
        /// <summary>
        /// ����ISBN��õ�����¼ֵ����VO
        /// </summary>
        public static PlatformBookVO getVOByIsbn(string isbn)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where isbn='" + isbn + "' ";
            return (PlatformBookVO)Data.getVO(VO_NAMESPACE, sql);
        }

    }
}
