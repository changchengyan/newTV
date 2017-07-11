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
/// ����ʱ��:2013/11/5 13:51:14 
/// SysRoleBOҵ����� (���ļ��ɿ���Զ����� v2.0)
/// ˵������ɫ
/// ���ں��� hi.baidu.com/dovebo QQ3968666 email:leijiangbo@163.com
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysRoleBO
    {
        //BO��Ӧ����
        public static string MAIN_TABLE_NAME = "sys_role";
        //��Ӧ��ֵ���������ռ�
        public static string VO_NAMESPACE = "Redsz.VO.SysRoleVO";
        //�б�SQL
        private static string LIST_SQL = "select * from " + MAIN_TABLE_NAME;
        private static string LIST_SQL_ORDERBY = " order by id asc";

        //�б�RowCount
        private static string LIST_COUNT_SQL = "select count(*) from (" + LIST_SQL + ") ctable ";

        //TOP��ҳSQL
        private static string LIST_TOP_SQL_begin = "select top ";
        private static string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";


        /// <summary>
        /// �����ɫ�˵��ڵ�Ȩ��
        /// </summary>
        public static string RoleMenuSet(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                if (req.Form["menu_id"] != null)
                {
                    string role_id = req["role_id"];
                    Data.RunSql("delete from sys_role_set where role_id=" + role_id);
                    SysRoleVO rolevo = SysRoleBO.getVOByID(role_id);


                    String[] ids = req.Form.GetValues("menu_id");// req.Form["menu_id"].Split(new Char[] { ',' });
                    for (int i = 0; i < ids.Length; i++)
                    {                        
                        Data.RunSql("insert into sys_role_set(role_id,menu_id) values(" + role_id + "," + ids[i] + ")");
                    }
                    s = "{\"success\":true,\"message\":\"���óɹ���\"}";


                    SysLogVO syslogvo = new SysLogVO();
                    syslogvo.code = "SET";
                    syslogvo.datetime = DateTime.Now.ToString();
                    syslogvo.title = "�����ɫ(" + rolevo.rolename+ ")�˵�Ȩ��";
                    syslogvo.username = usernname;
                    syslogvo.state = "�ɹ�";
                    SysLogBO.Add(syslogvo);

                }
            }

            return s;

        }

        /// <summary>
        /// �޸�
        /// </summary>
        public static string RoleModify(HttpRequest req)
        {
            string s = "{\"success\":false}";
            if (req.Cookies["adminInfo"] != null)
            {

                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                Data.Update(MAIN_TABLE_NAME, "id=@id", Data.getHashtable(req, VO_NAMESPACE));
                s = "{\"success\":true}";

                SysRoleVO rolevo = SysRoleBO.getVOByID(req["id"]);

                SysLogVO syslogvo = new SysLogVO();
                syslogvo.code = "MODIFY";
                syslogvo.datetime = DateTime.Now.ToString();
                syslogvo.title = "�޸Ľ�ɫ��" + rolevo.rolename + "";
                syslogvo.username = usernname;
                syslogvo.state = "�ɹ�";
                SysLogBO.Add(syslogvo);


            }
            return s;
        }


        /// <summary>
        /// ����
        /// </summary>
        public static string RoleAdd(HttpRequest req)
        {
            string s = "{\"success\":false}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                Data.Insert(MAIN_TABLE_NAME, Data.getHashtable(req, VO_NAMESPACE));
                s = "{\"success\":true}";

                SysLogVO syslogvo = new SysLogVO();
                syslogvo.code = "ADD";
                syslogvo.datetime = DateTime.Now.ToString();
                syslogvo.title = "������ɫ��" + req["rolename"]+ "";
                syslogvo.username = usernname;
                syslogvo.state = "�ɹ�";
                SysLogBO.Add(syslogvo);

            }
            return s;
        }


        /// <summary>
        /// ɾ��
        /// </summary>
        public static string RoleDelete(HttpRequest req,string id)
        {
            string s = "{\"success\":false,\"message\":\"�Ự��ʧЧ�������µ�¼��\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                int user_count = Data.GetRsCount("select count(id) from sys_admin where roleid="+id);
                if (user_count > 0)
                {
                    s = "{\"success\":false,\"message\":\"�ý�ɫ��" + user_count + "���û�������ɾ����\"}";
                }
                else
                {
                    SysRoleVO rolevo = SysRoleBO.getVOByID(id);
                    Hashtable ht = new Hashtable();
                    ht.Add("id", id);
                    Data.Del("sys_role", "id=@id", ht);
                    s = "{\"success\":true}";

                    
                    SysLogVO syslogvo = new SysLogVO();
                    syslogvo.code = "DELETE";
                    syslogvo.datetime = DateTime.Now.ToString();
                    syslogvo.title = "ɾ����ɫ��" + rolevo.rolename + "";
                    syslogvo.username = usernname;
                    syslogvo.state = "�ɹ�";
                    SysLogBO.Add(syslogvo);

                }
            }
            return s;
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


        private static DataTable _HotRole = null;
        public static DataTable HotRole
        {
            set { _HotRole = value; }
            get { return _HotRole; }
        }
        /// <summary>
        /// ����ȵ�
        /// </summary>
        public DataTable getHotRole(string size)
        {
            if (HotRole == null)
            {
                HotRole = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by topsize asc,id desc ");
            }
            return HotRole;
        }


        private static DataTable _HotOpenRole = null;
        public static DataTable HotOpenRole
        {
            set { _HotOpenRole = value; }
            get { return _HotOpenRole; }
        }
        /// <summary>
        /// ����ȵ���
        /// </summary>
        public DataTable getHotOpenRole(string size)
        {
            if (HotOpenRole == null)
            {
                HotOpenRole = Data.TopList("select top " + size + " id,title,dtitle,indexpic,indexpic2,datetime from " + MAIN_TABLE_NAME + " where isopen=1 order by opensize desc,id desc ");
            }
            return HotOpenRole;
        }


        private static DataTable _RoleOption = null;
        public static DataTable RoleOption
        {
            set { _RoleOption = value; }
            get { return _RoleOption; }
        }


        /// <summary>
        /// �����select option TataTable
        /// </summary>
        public static DataTable getRoleOption()
        {
            return Data.getDataTableBySql("select id,rolename from " + SysRoleBO.MAIN_TABLE_NAME+ " order by topsize asc,id asc");
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
                sql_query += " and (rolename like '%" + keyword + "%'  )  ";
            }
            string sql_count = sql_query;
			sql_query += " order by id asc ";
			return Data.getQueryList(req, psize, sql_query, sql_count);
        }


        /// <summary>
        /// ����ID��õ�����¼ֵ����VO
        /// </summary>
        public SysRoleVO getvo(string id)
        {
            string sql = "select * from " + MAIN_TABLE_NAME;
            if (id != null) 
            {
                sql += " where id=" + id;
            }
            sql += " order by topsize asc,id asc";
            return (SysRoleVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// ����ID��õ�����¼ֵ����VO
        /// </summary>
        public static SysRoleVO getVOByID(string id)
        {
            string sql = "select * from " + MAIN_TABLE_NAME;
            if (id != null)
            {
                sql += " where id=" + id;
            }
            sql += " order by topsize asc,id asc";
            return (SysRoleVO)Data.getVO(VO_NAMESPACE, sql);
        }



        /// <summary>
        /// ��վ�̬��������
        /// </summary>
        public static void clearStaticData()
        {
				HotRole = null;
				HotOpenRole = null;
				_RoleOption = null;
        }


    }
}
