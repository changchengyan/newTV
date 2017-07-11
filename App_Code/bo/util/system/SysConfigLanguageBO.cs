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
/// ����ʱ��:2014/5/26 10:47:23 
/// SysConfigLanguageBOҵ����� (���ļ��ɿ���Զ����� v2.0)
/// ˵�������԰汾
/// QQ 3968666 email:leijiangbo@163.com
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysConfigLanguageBO
    {
        //BO��Ӧ����
        public static string MAIN_TABLE_NAME = "sys_config_language";
        //��Ӧ��ֵ���������ռ�
        public static string VO_NAMESPACE = "Redsz.VO.SysConfigLanguageVO";
        //�б�SQL
        private static string LIST_SQL = "select * from " + MAIN_TABLE_NAME;
        private static string LIST_SQL_ORDERBY = " order by id asc";

        //�б�RowCount
        private static string LIST_COUNT_SQL = "select count(*) from (" + LIST_SQL + ") ctable ";

        //TOP��ҳSQL
        private static string LIST_TOP_SQL_begin = "select top ";
        private static string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";


        /// <summary>
        /// ����
        /// </summary>
        public static string Add(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
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
        /// �޸�
        /// </summary>
        public static string Mod(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
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
        /// ����
        /// </summary>
        public void TopSize(HttpRequest req) 
        {
           Data.TopSize(MAIN_TABLE_NAME, "id", "topsize",req);
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
        /// ����ȵ�
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
        /// ����ȵ���
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
        /// �����select option TataTable
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
        /// ����б�����ҳ����
        /// </summary>
        public static QueryVO getList(HttpRequest req, string psize)
        {
			string sql_query = "select * from " + MAIN_TABLE_NAME + " ";
			string sql_count = sql_query;
			sql_query += " order by id asc ";
			return Data.getQueryList(req, psize, sql_query, sql_count);
        }


        /// <summary>
        /// ����ID��õ�����¼ֵ����VO
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
        /// ����ID��õ�����¼ֵ����VO
        /// </summary>
        public static SysConfigLanguageVO getVOByCode(string code)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME + " where code='"+code+"' ";
            return (SysConfigLanguageVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// ��վ�̬��������
        /// </summary>
        public static void clearStaticData()
        {
				HotSysConfigLanguage = null;
				HotOpenSysConfigLanguage = null;
				_SysConfigLanguageOption = null;
        }


    }
}
