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
/// ����ʱ��:2008-7-14 23:12:52 
/// SysDeptBOҵ����� (���ļ��ɿ���Զ����� v1.0)
/// ���ں��� www.redsz.com QQ3968666
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysDeptBO
    {
        //BO��Ӧ����
        private static string MAIN_TABLE_NAME = "sys_dept";
        //��Ӧ��ֵ���������ռ�
        private static string VO_NAMESPACE = "Redsz.VO.SysDeptVO";
        //�б�SQL
        private static string LIST_SQL = "select * from " + MAIN_TABLE_NAME;
        private static string LIST_SQL_ORDERBY = " order by topsize asc,id asc";

        //�б�RowCount
        private static string LIST_COUNT_SQL = "select count(*) from (" + LIST_SQL + ") ctable ";

        //TOP��ҳSQL
        private static string LIST_TOP_SQL_begin = "select top ";
        private static string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";


        /// <summary>
        /// ����
        /// </summary>
        public void Add(HttpRequest req)
        {
            Data.Insert(MAIN_TABLE_NAME, Data.getHashtable(req, VO_NAMESPACE));
        }


        /// <summary>
        /// �޸�
        /// </summary>
        public void Mod(HttpRequest req)
        {
           Data.Update(MAIN_TABLE_NAME, "id=@id", Data.getHashtable(req, VO_NAMESPACE)); 
        }


        /// <summary>
        /// ����
        /// </summary>
        public void TopSize(HttpRequest req) 
        {
           Data.TopSize(MAIN_TABLE_NAME, "id", "topsize",req);
        }


        /// <summary>
        /// ɾ��(���б���ѡ����ɾ��)
        /// </summary>
        public void DelList(HttpRequest req)
        {
            Data.DelList(MAIN_TABLE_NAME, "id", req);
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


        //������м�¼�� Hastable
        public Hashtable getHastable()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = getAll();
            for (int i = 0;i< dt.Rows.Count;i++ )
            {
                ht.Add(dt.Rows[i]["title"], dt.Rows[i]["id"]);
            }
            return ht;
        }


        //������м�¼�� Ilist
        public IList getOptionsList()
        {
            IList list = new ArrayList();
            Hashtable ht = new Hashtable();
            DataTable dt = getAll();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string[] options = new string[3];
                options[0] = dt.Rows[i]["title"].ToString();
                options[1] = dt.Rows[i]["id"].ToString();
                options[2] = "0";
                list.Add(options);
            }
            return list;
        }


        /// <summary>
        /// ����б�����ҳ����
        /// </summary>
        public DataTable getList(HttpRequest req, string psize)
        {
            DataSet ds = Data.ExecuteDataSet(psize, req, CommandType.Text, LIST_SQL+LIST_SQL_ORDERBY, null);
            DataTable dt = ds.Tables[0];
            return dt;
        }


        /// <summary>
        /// ����б�ͬ����ҳHTML������ҳ����
        /// </summary>
        public string getSplitPageHTML(HttpRequest req,string str_PageSize)
        {
            return Data.getPageHTML(req, str_PageSize, LIST_COUNT_SQL);
        }


        /// <summary>
        /// ����ID��õ�����¼ֵ����VO
        /// </summary>
        public SysDeptVO getvo(string id)
        {
            string sql = "select * from " + MAIN_TABLE_NAME;
            if (id != null) 
            {
                sql += " where id=" + id;
            }
            sql += " order by topsize asc,id asc";
            return (SysDeptVO)Data.getVO(VO_NAMESPACE, sql);
        }

    }
}
