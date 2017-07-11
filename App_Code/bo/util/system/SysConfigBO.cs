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
/// BlogBO 的摘要说明
/// </summary>
/// 
namespace Redsz.BO
{
    public class SysConfigBO
    {
        //BO对应主表
        private static string MAIN_TABLE_NAME = "sys_config";
        //对应的值对象命名空间
        public static string VO_NAMESPACE = "Redsz.VO.SysConfigVO";
        
        /// <summary>
        /// 新增
        /// </summary>
        public void Add(HttpRequest req)
        {
            Data.Insert(MAIN_TABLE_NAME, Data.getHashtable(req, VO_NAMESPACE));
        }

        //20151012 
        //panyapeng
        //get logo
        public static DataTable getLogo(string space_id)
        {
            string sql_query = "select * from space where id=" + space_id;
            return Data.getDataTable(sql_query);
        }
        /// <summary>
        /// 修改
        /// </summary>
       
        public static string ConfigModify(HttpRequest req)
        {

            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                Hashtable ht = Data.getHashtable(req, VO_NAMESPACE);
                ht.Add("weixin_access_token_time",DateTime.Now.AddDays(-1));
                Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                _CONFIG = null;
                s = "{\"success\":true,\"message\":\"\"}";
            }
            return s;
        }

        //测试函数
        private static DataTable _test = null;
        public static DataTable test
        {
            set { _test = value; }
            get { return _test; }
        }
        public DataTable getTest(string size)
        {
            if (test == null)
            {
                test = Data.TopList("select top 1 group_icon from groups where space_id=5 ");
            }
            return test;
        }



        /// <summary>
        /// 删除(从列表中选择多个删除)
        /// </summary>
        public void DelList(HttpRequest req)
        {
            Data.DelList(MAIN_TABLE_NAME, "id", req);
        }


        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public SysConfigVO getvo(string id)
        {
            string sql = "select * from " + MAIN_TABLE_NAME;
            if (id != null) 
            {
                sql += " where id=" + id;
            }
            sql += " order by topsize asc,id asc";
            return (SysConfigVO)Data.getVO(VO_NAMESPACE, sql);
        }
        
        public static SysConfigVO _CONFIG = null; 
        public static SysConfigVO CONFIG()
        {
            if (_CONFIG == null)
            {
                
                string sql = "select top 1 * from " + MAIN_TABLE_NAME;
                _CONFIG = (SysConfigVO)Data.getVO("Redsz.VO.SysConfigVO", sql);

            }
            return _CONFIG;
        
        }
        
        /// <summary>
        /// 获得单个记录值对象VO
        /// </summary>
        public SysConfigVO getvo()
        {
            string sql = "select * from " + MAIN_TABLE_NAME;
            return (SysConfigVO)Data.getVO(VO_NAMESPACE, sql);
        }


        /// <summary>
        /// 专供Flash  upload.swf 上传用 
        /// </summary>
        public static string flashUploadPic(HttpRequest req, HttpPostedFile myFile)
        {

            string s = "{\"success\":false}";
            string returnstr = "";
            if (myFile != null)
            {
                //定义一些变量
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i);
                string filetype = nam.Substring(i + 1).ToLower();

                string folder = "/upload/config/" + sys.getDateStr() + "/";
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folder)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folder));
                }

                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(folder + newnm));
                returnstr = folder + newnm;

                s = "{\"success\":true,\"path\":\"" + returnstr + "\"}";
            }

            return s;
        }

        public static string GetFileServerFlashJS()
        {
            return ConfigurationManager.ConnectionStrings["FILESERVERJS"].ConnectionString;
        }

        public static string GetFileServerAddress()
        {
            return ConfigurationManager.ConnectionStrings["FILESERVERADDRESS"].ConnectionString;
        }
    }
}