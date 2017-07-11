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
/// 创建时间:2008-7-14 23:12:52 
/// SysDeptBO业务对象 (此文件由框架自动生成 v1.0)
/// 深圳红网 www.redsz.com QQ3968666
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysDeptBO
    {
        //BO对应主表
        private static string MAIN_TABLE_NAME = "sys_dept";
        //对应的值对象命名空间
        private static string VO_NAMESPACE = "Redsz.VO.SysDeptVO";
        //列表SQL
        private static string LIST_SQL = "select * from " + MAIN_TABLE_NAME;
        private static string LIST_SQL_ORDERBY = " order by topsize asc,id asc";

        //列表RowCount
        private static string LIST_COUNT_SQL = "select count(*) from (" + LIST_SQL + ") ctable ";

        //TOP单页SQL
        private static string LIST_TOP_SQL_begin = "select top ";
        private static string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";


        /// <summary>
        /// 新增
        /// </summary>
        public void Add(HttpRequest req)
        {
            Data.Insert(MAIN_TABLE_NAME, Data.getHashtable(req, VO_NAMESPACE));
        }


        /// <summary>
        /// 修改
        /// </summary>
        public void Mod(HttpRequest req)
        {
           Data.Update(MAIN_TABLE_NAME, "id=@id", Data.getHashtable(req, VO_NAMESPACE)); 
        }


        /// <summary>
        /// 排序
        /// </summary>
        public void TopSize(HttpRequest req) 
        {
           Data.TopSize(MAIN_TABLE_NAME, "id", "topsize",req);
        }


        /// <summary>
        /// 删除(从列表中选择多个删除)
        /// </summary>
        public void DelList(HttpRequest req)
        {
            Data.DelList(MAIN_TABLE_NAME, "id", req);
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
            return Data.TopList(LIST_SQL+LIST_SQL_ORDERBY);
        }


        //获得所有记录的 Hastable
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


        //获得所有记录的 Ilist
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
        /// 获得列表，含分页参数
        /// </summary>
        public DataTable getList(HttpRequest req, string psize)
        {
            DataSet ds = Data.ExecuteDataSet(psize, req, CommandType.Text, LIST_SQL+LIST_SQL_ORDERBY, null);
            DataTable dt = ds.Tables[0];
            return dt;
        }


        /// <summary>
        /// 获得列表同步分页HTML，含分页参数
        /// </summary>
        public string getSplitPageHTML(HttpRequest req,string str_PageSize)
        {
            return Data.getPageHTML(req, str_PageSize, LIST_COUNT_SQL);
        }


        /// <summary>
        /// 根据ID获得单个记录值对象VO
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
