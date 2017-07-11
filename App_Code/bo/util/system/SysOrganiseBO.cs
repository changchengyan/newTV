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
/// ����ʱ��:2013/11/5 19:01:26 
/// SysOrganiseBOҵ����� (���ļ��ɿ���Զ����� v2.0)
/// ˵������֯����
/// QQ 3968666 email:leijiangbo@163.com
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysOrganiseBO
    {
        //BO��Ӧ����
        public static string MAIN_TABLE_NAME = "sys_organise";
        //��Ӧ��ֵ���������ռ�
        public static string VO_NAMESPACE = "Redsz.VO.SysOrganiseVO";
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
        public static string OrganiseAdd(HttpRequest req)
        {
            string s = "{\"success\":false}";
            if (req.Cookies["adminInfo"] != null)
            {
                try
                {
                    string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                    Hashtable ht = Data.getHashtable(req, SysOrganiseBO.VO_NAMESPACE);

                    Data.Insert(MAIN_TABLE_NAME, ht);
                    s = "{\"success\":true}";

                    SysLogVO syslogvo = new SysLogVO();
                    syslogvo.code = "ADD";
                    syslogvo.datetime = DateTime.Now.ToString();
                    syslogvo.title = "������֯������" + ht["org_name"];
                    syslogvo.username = usernname;
                    syslogvo.state = "�ɹ�";
                    SysLogBO.Add(syslogvo);


                }
                catch { }
            }
            return s;
        }

        /// <summary>
        /// �޸�
        /// </summary>
        public static string OrganiseModify(HttpRequest req)
        {

            string s = "{\"success\":false}";
            if (req.Cookies["adminInfo"] != null)
            {
                try
                {
                    string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                    Hashtable ht = Data.getHashtable(req, SysOrganiseBO.VO_NAMESPACE);


                    Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                    s = "{\"success\":true}";


                    SysLogVO syslogvo = new SysLogVO();
                    syslogvo.code = "MODIFY";
                    syslogvo.datetime = DateTime.Now.ToString();
                    syslogvo.title = "�޸���֯������" + ht["org_name"];
                    syslogvo.username = usernname;
                    syslogvo.state = "�ɹ�";
                    SysLogBO.Add(syslogvo);

                }
                catch { }
            }

            return s;

        }

        /// <summary>
        /// ɾ��(���б���ѡ����ɾ��)
        /// </summary>
        public static string deleteOrganise(HttpRequest req, string id)
        {

            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                int childCount = Data.GetRsCount("select count(id) from organise where parentid=" + id);
                if (childCount > 0)
                {

                    s = "{\"success\":false,\"message\":\"�����Ӳ˵�������ɾ����\"}";
                }
                else
                {

                    SysOrganiseVO organisevo = getVOByID(id);

                    Hashtable ht = new Hashtable();
                    ht.Add("id", id);
                    Data.Del("organise", "id@=id", ht);
                    s = "{\"success\":true,\"message\":\"ɾ���˵��ɹ���\"}";


                    SysLogVO syslogvo = new SysLogVO();
                    syslogvo.code = "DELETE";
                    syslogvo.datetime = DateTime.Now.ToString();
                    syslogvo.title = "ɾ��ϵͳ�˵���" + organisevo.org_name;
                    syslogvo.username = usernname;
                    syslogvo.state = "�ɹ�";
                    SysLogBO.Add(syslogvo);

                }


            }
            else
            {
                s = "{\"success\":false,\"message\":\"����Աδ��¼��\"}";
            }


            return s;
        }



        /// <summary>
        /// ����
        /// </summary>
        public void Add(HttpRequest req)
        {
            Data.Insert(MAIN_TABLE_NAME, Data.getHashtable(req, VO_NAMESPACE));
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
        public DataTable getAll()
        {
            return Data.TopList(LIST_SQL+LIST_SQL_ORDERBY);
        }


        private static DataTable _HotOrganise = null;
        public static DataTable HotOrganise
        {
            set { _HotOrganise = value; }
            get { return _HotOrganise; }
        }
        /// <summary>
        /// ����ȵ�
        /// </summary>
        public DataTable getHotOrganise(string size)
        {
            if (HotOrganise == null)
            {
                HotOrganise = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by topsize asc,id desc ");
            }
            return HotOrganise;
        }


        private static DataTable _HotOpenOrganise = null;
        public static DataTable HotOpenOrganise
        {
            set { _HotOpenOrganise = value; }
            get { return _HotOpenOrganise; }
        }
        /// <summary>
        /// ����ȵ���
        /// </summary>
        public DataTable getHotOpenOrganise(string size)
        {
            if (HotOpenOrganise == null)
            {
                HotOpenOrganise = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by opensize desc,id desc ");
            }
            return HotOpenOrganise;
        }


        private static DataTable _OrganiseOption = null;
        public static DataTable OrganiseOption
        {
            set { _OrganiseOption = value; }
            get { return _OrganiseOption; }
        }


        /// <summary>
        /// �����select option TataTable
        /// </summary>
        public static DataTable getOrganiseOption()
        {
            if (OrganiseOption == null)
            {
                OrganiseOption = Data.getDataTableBySql("select id,name from " + SysOrganiseBO.MAIN_TABLE_NAME+ " order by topsize asc,id asc");
            }
            return OrganiseOption;
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
        public static SysOrganiseVO getVOByID(string id)
        {
            string sql = "select top 1 * from " + MAIN_TABLE_NAME;
            if (id != null) 
            {
                sql += " where id=" + id;
            }
            return (SysOrganiseVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// ��վ�̬��������
        /// </summary>
        public static void clearStaticData()
        {
				HotOrganise = null;
				HotOpenOrganise = null;
				_OrganiseOption = null;
        }


    }
}
