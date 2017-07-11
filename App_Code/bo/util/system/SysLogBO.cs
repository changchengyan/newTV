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
/// ����ʱ��:2012/11/23 13:56:35 
/// SysLogBOҵ����� (���ļ��ɿ���Զ����� v2.0)
/// ˵����ϵͳ��־
/// ���ں��� hi.baidu.com/dovebo QQ3968666 email:leijiangbo@163.com
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysLogBO
    {
        //BO��Ӧ����
        public static string MAIN_TABLE_NAME = "sys_log";
        //��Ӧ��ֵ���������ռ�
        public static string VO_NAMESPACE = "Redsz.VO.SysLogVO";
        //�б�SQL
        private static string LIST_SQL = "select * from " + MAIN_TABLE_NAME;
        private static string LIST_SQL_ORDERBY = " order by id asc";

        //�б�RowCount
        private static string LIST_COUNT_SQL = "select count(*) from (" + LIST_SQL + ") ctable ";

        //TOP��ҳSQL
        private static string LIST_TOP_SQL_begin = "select top ";
        private static string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";


        /// <summary>
        /// ����ϵͳ��־
        /// </summary>
        public static void Add(SysLogVO syslogvo)
        {
            Hashtable ht = UtilBO.voToHashtable(syslogvo);
            ht.Remove("id");
            //Data.Insert(MAIN_TABLE_NAME, ht);
            clearStaticData();
        }




        /// <summary>
        /// �޸�
        /// </summary>
        public void Mod(HttpRequest req)
        {
            Data.Update(MAIN_TABLE_NAME, "id=@id", Data.getHashtable(req, VO_NAMESPACE));
            clearStaticData();
        }


        /// <summary>
        /// ����
        /// </summary>
        public void TopSize(HttpRequest req)
        {
            Data.TopSize(MAIN_TABLE_NAME, "id", "topsize", req);
            clearStaticData();
        }


        /// <summary>
        /// ɾ��(���б���ѡ����ɾ��)
        /// </summary>
        public void DelList(HttpRequest req)
        {
            Data.DelList(MAIN_TABLE_NAME, "id", req);
            clearStaticData();
        }


        /// <summary>
        /// �ر�(���Ϊ0)
        /// </summary>
        public void OpenList(HttpRequest req)
        {
            Hashtable ht = new Hashtable();
            ht.Add("isopen", 1);
            Data.UpdateList(req, MAIN_TABLE_NAME, "id", ht, VO_NAMESPACE);
            clearStaticData();
        }


        /// <summary>
        /// �ر�(���Ϊ0)
        /// </summary>
        public void CloseList(HttpRequest req)
        {
            Hashtable ht = new Hashtable();
            ht.Add("isopen", 0);
            Data.UpdateList(req, MAIN_TABLE_NAME, "id", ht, VO_NAMESPACE);
            clearStaticData();
        }


        /// <summary>
        /// ������
        /// </summary>
        public void ClickSize(string id)
        {
            Data.ClickSize(MAIN_TABLE_NAME, "id", id, "opensize", 1);
        }


        /// <summary>
        /// ���չʾ�б�TOP��¼��������ҳ
        /// </summary>
        public DataTable getTopBlog(string size)
        {
            return Data.TopList(LIST_TOP_SQL_begin + size + LIST_TOP_SQL_end);
        }


        //������м�¼
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
        /// ����ȵ�
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
        /// ����ȵ���
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
        /// �����select option TataTable
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
        /// ����б�����ҳ����
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
        /// ����ID��õ�����¼ֵ����VO
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
        /// ��վ�̬��������
        /// </summary>
        public static void clearStaticData()
        {
            HotSyslog = null;
            HotOpenSyslog = null;
            _SyslogOption = null;
        }


    }
}
