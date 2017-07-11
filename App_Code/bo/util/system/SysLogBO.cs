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
/// 创建时间:2012/11/23 13:56:35 
/// SysLogBO业务对象 (此文件由框架自动生成 v2.0)
/// 说明：系统日志
/// 深圳红网 hi.baidu.com/dovebo QQ3968666 email:leijiangbo@163.com
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysLogBO
    {
        //BO对应主表
        public static string MAIN_TABLE_NAME = "sys_log";
        //对应的值对象命名空间
        public static string VO_NAMESPACE = "Redsz.VO.SysLogVO";
        //列表SQL
        private static string LIST_SQL = "select * from " + MAIN_TABLE_NAME;
        private static string LIST_SQL_ORDERBY = " order by id asc";

        //列表RowCount
        private static string LIST_COUNT_SQL = "select count(*) from (" + LIST_SQL + ") ctable ";

        //TOP单页SQL
        private static string LIST_TOP_SQL_begin = "select top ";
        private static string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";


        /// <summary>
        /// 新增系统日志
        /// </summary>
        public static void Add(SysLogVO syslogvo)
        {
            Hashtable ht = UtilBO.voToHashtable(syslogvo);
            ht.Remove("id");
            //Data.Insert(MAIN_TABLE_NAME, ht);
            clearStaticData();
        }




        /// <summary>
        /// 修改
        /// </summary>
        public void Mod(HttpRequest req)
        {
            Data.Update(MAIN_TABLE_NAME, "id=@id", Data.getHashtable(req, VO_NAMESPACE));
            clearStaticData();
        }


        /// <summary>
        /// 排序
        /// </summary>
        public void TopSize(HttpRequest req)
        {
            Data.TopSize(MAIN_TABLE_NAME, "id", "topsize", req);
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
        public DataTable getAll()
        {
            return Data.TopList(LIST_SQL + LIST_SQL_ORDERBY);
        }


        private static DataTable _HotSyslog = null;
        public static DataTable HotSyslog
        {
            set { _HotSyslog = value; }
            get { return _HotSyslog; }
        }
        /// <summary>
        /// 获得热点
        /// </summary>
        public DataTable getHotSyslog(string size)
        {
            if (HotSyslog == null)
            {
                HotSyslog = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by topsize asc,id desc ");
            }
            return HotSyslog;
        }


        private static DataTable _HotOpenSyslog = null;
        public static DataTable HotOpenSyslog
        {
            set { _HotOpenSyslog = value; }
            get { return _HotOpenSyslog; }
        }
        /// <summary>
        /// 获得热点点击
        /// </summary>
        public DataTable getHotOpenSyslog(string size)
        {
            if (HotOpenSyslog == null)
            {
                HotOpenSyslog = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by opensize desc,id desc ");
            }
            return HotOpenSyslog;
        }


        private static DataTable _SyslogOption = null;
        public static DataTable SyslogOption
        {
            set { _SyslogOption = value; }
            get { return _SyslogOption; }
        }


        /// <summary>
        /// 获得所select option TataTable
        /// </summary>
        public static DataTable getSyslogOption()
        {
            if (SyslogOption == null)
            {
                SyslogOption = Data.getDataTableBySql("select id,name from " + SysLogBO.MAIN_TABLE_NAME + " order by topsize asc,id asc");
            }
            return SyslogOption;
        }


        /// <summary>
        /// 获得列表，含分页参数
        /// </summary>
        public static QueryVO getList(HttpRequest req, string psize)
        {
            string sql_query = "select * from " + MAIN_TABLE_NAME + " where 1=1 ";
            string keyword = req["keyword"];
            if (UtilBO.isNotNull(keyword))
            {
                sql_query += " and(code like'%" + keyword + "%' or title like'%" + keyword + "%' or username like'%" + keyword + "%') ";
            }
            string sql_count = sql_query;
            sql_query += " order by id desc ";
            return Data.getQueryList(req, psize, sql_query, sql_count);
        }


        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public SysLogVO getvo(string id)
        {
            string sql = "select * from " + MAIN_TABLE_NAME;
            if (id != null)
            {
                sql += " where id=" + id;
            }
            sql += " order by topsize asc,id asc";
            return (SysLogVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 清空静态缓存数据
        /// </summary>
        public static void clearStaticData()
        {
            HotSyslog = null;
            HotOpenSyslog = null;
            _SyslogOption = null;
        }


    }
}
