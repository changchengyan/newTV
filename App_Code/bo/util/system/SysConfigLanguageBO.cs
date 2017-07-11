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
/// 创建时间:2014/5/26 10:47:23 
/// SysConfigLanguageBO业务对象 (此文件由框架自动生成 v2.0)
/// 说明：语言版本
/// QQ 3968666 email:leijiangbo@163.com
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysConfigLanguageBO
    {
        //BO对应主表
        public static string MAIN_TABLE_NAME = "sys_config_language";
        //对应的值对象命名空间
        public static string VO_NAMESPACE = "Redsz.VO.SysConfigLanguageVO";
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
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                Data.Insert(MAIN_TABLE_NAME, ht);
                clearStaticData();
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// 修改
        /// </summary>
        public static string Mod(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {

                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                clearStaticData();
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }


        /// <summary>
        /// 排序
        /// </summary>
        public void TopSize(HttpRequest req) 
        {
           Data.TopSize(MAIN_TABLE_NAME, "id", "topsize",req);
           clearStaticData();
        }


        /// <summary>
        /// 删除(从列表中选择多个删除)
        /// </summary>
        public void DelList(HttpRequest req)
        {
            Data.DelList(MAIN_TABLE_NAME, "id", req);
            clearStaticData();
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
        public static DataTable getAll()
        {
            return Data.getDataTable("select * from sys_config_language order by topsize asc,id asc");
        }


        private static DataTable _HotSysConfigLanguage = null;
        public static DataTable HotSysConfigLanguage
        {
            set { _HotSysConfigLanguage = value; }
            get { return _HotSysConfigLanguage; }
        }
        /// <summary>
        /// 获得热点
        /// </summary>
        public DataTable getHotSysConfigLanguage(string size)
        {
            if (HotSysConfigLanguage == null)
            {
                HotSysConfigLanguage = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by topsize asc,id desc ");
            }
            return HotSysConfigLanguage;
        }


        private static DataTable _HotOpenSysConfigLanguage = null;
        public static DataTable HotOpenSysConfigLanguage
        {
            set { _HotOpenSysConfigLanguage = value; }
            get { return _HotOpenSysConfigLanguage; }
        }
        /// <summary>
        /// 获得热点点击
        /// </summary>
        public DataTable getHotOpenSysConfigLanguage(string size)
        {
            if (HotOpenSysConfigLanguage == null)
            {
                HotOpenSysConfigLanguage = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by opensize desc,id desc ");
            }
            return HotOpenSysConfigLanguage;
        }


        private static DataTable _SysConfigLanguageOption = null;
        public static DataTable SysConfigLanguageOption
        {
            set { _SysConfigLanguageOption = value; }
            get { return _SysConfigLanguageOption; }
        }


        /// <summary>
        /// 获得所select option TataTable
        /// </summary>
        public static DataTable getSysConfigLanguageOption()
        {
            if (SysConfigLanguageOption == null)
            {
                SysConfigLanguageOption = Data.getDataTableBySql("select id,name from " + SysConfigLanguageBO.MAIN_TABLE_NAME+ " order by topsize asc,id asc");
            }
            return SysConfigLanguageOption;
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
        public static SysConfigLanguageVO getVOByID(string id)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME;
            if (id != null) 
            {
                sql += " where id=" + id;
            }
            return (SysConfigLanguageVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static SysConfigLanguageVO getVOByCode(string code)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where code='"+code+"' ";
            return (SysConfigLanguageVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 清空静态缓存数据
        /// </summary>
        public static void clearStaticData()
        {
				HotSysConfigLanguage = null;
				HotOpenSysConfigLanguage = null;
				_SysConfigLanguageOption = null;
        }


    }
}
