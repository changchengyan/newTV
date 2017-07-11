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
    public class SysMenuBO
    {
        //BO对应主表
        public static string MAIN_TABLE_NAME = "sys_menu";
        //对应的值对象命名空间
        public static string VO_NAMESPACE = "Redsz.VO.SysMenuVO";
        //列表SQL
        public string LIST_SQL = "select * from " + MAIN_TABLE_NAME + "  order by topsize asc,id asc";
        //列表RowCount
        public string LIST_COUNT_SQL = "select count(*) from " + MAIN_TABLE_NAME;
        //TOP单页SQL
        public string LIST_TOP_SQL_begin = "select top ";
        public string LIST_TOP_SQL_end = " id,title from " + MAIN_TABLE_NAME + " order by topsize asc,id asc";

        /// <summary>
        /// 新增
        /// </summary>
        public static string MenuAdd(HttpRequest req)
        {
            string s = "{\"success\":false}";
            if (req.Cookies["adminInfo"] != null)
            {
                try
                {
                    string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                    Hashtable ht = Data.getHashtable(req, SysMenuBO.VO_NAMESPACE);
                    Data.Insert(MAIN_TABLE_NAME, ht);
                    s = "{\"success\":true}";
                    updateMenuLevel(ht["parentid"].ToString());

                }
                catch { }
            }
            return s;
        }

        /// <summary>
        /// 更新菜单的深度
        /// </summary>
        public static void updateMenuLevel(string parentid)
        {
            if (UtilBO.isNull(parentid))
            {
                parentid = "0";
            }
            SysMenuVO sysmenuvo = getVOByID(parentid);
            DataTable list = Data.getDataTable("select * from sys_menu where parentid=" + parentid + "");
            for (int i = 0; i < list.Rows.Count; i++)
            {
                Data.RunSql("update sys_menu set menulevel='" + (int.Parse(sysmenuvo.menulevel) + 1) + "' where id=" + list.Rows[i]["id"]);
                updateMenuLevel(list.Rows[i]["id"].ToString());
            }
        }


        /// <summary>
        /// 修改
        /// </summary>
        public static string MenuModify(HttpRequest req)
        {
            string s = "{\"success\":false}";
            if (req.Cookies["adminInfo"] != null)
            {
                try
                {
                    string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                    Hashtable ht = Data.getHashtable(req, SysMenuBO.VO_NAMESPACE);
                    Data.Update(MAIN_TABLE_NAME, "id=@id", ht);
                    s = "{\"success\":true}";
                    updateMenuLevel(ht["parentid"].ToString());
                }
                catch { }
            }

            return s;

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
        public  static string deleteMenu(HttpRequest req,string id)
        {

            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string usernname = HttpUtility.UrlDecode(req.Cookies["adminInfo"].Values["admin_username"].ToString());
                int childCount = Data.GetRsCount("select count(id) from " + MAIN_TABLE_NAME + " where parentid=" + id);
                if (childCount > 0)
                {

                    s = "{\"success\":false,\"message\":\"包含子菜单，不能删除！\"}";
                }
                else
                {

                    SysMenuVO menuvo = getVOByID(id);

                    Hashtable ht = new Hashtable();
                    ht.Add("id", id);
                    Data.Del(MAIN_TABLE_NAME, "id@=id", ht);
                    s = "{\"success\":true,\"message\":\"删除菜单成功！\"}";
                
                }


            }
            else
            {
                s = "{\"success\":false,\"message\":\"管理员未登录！\"}";            
            }


            return s;
        }

        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static string getMenuByID(HttpRequest req)
        {
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string id = req["id"];
                string sql = "select top 1 a.*,(select menuname from " + MAIN_TABLE_NAME + " where id=a.parentid )as parentname from " + MAIN_TABLE_NAME + " a ";
                if (id != null)
                {
                    sql += " where a.id=" + id;
                }
                SysMenuVO vo = (SysMenuVO)Data.getVO(SysMenuBO.VO_NAMESPACE, sql);
                string json= UtilBO.voToJson(vo);
                return json;
            }
            return s;
        }


        /// <summary>
        /// 获得目标窗口的选项
        /// </summary>
        public static DataTable getTargetOption()
        {

            DataTable table = new DataTable();
            DataRow row;

            table.Columns.Add("value", typeof(string));
            table.Columns.Add("text", typeof(string));

            row = table.NewRow();
            row["value"] = "";
            row["text"] = "无";
            table.Rows.Add(row);

            row = table.NewRow();
            row["value"] = "rightFrame";
            row["text"] = "主窗口";
            table.Rows.Add(row);

            row = table.NewRow();
            row["value"] = "_blank";
            row["text"] = "新窗口";
            table.Rows.Add(row);

            return table;
        }




        /// <summary>
        /// 更新表单中的childCount字段
        /// </summary>
        public void updateChildCount()
        {
            Data.RunSql("update " + MAIN_TABLE_NAME + " set child_count=(select count(*)from " + MAIN_TABLE_NAME + " b where b.parentid=[" + MAIN_TABLE_NAME + "].id )");
        }

        /// <summary>
        /// 获得展示列表TOP记录，不含分页
        /// </summary>
        public DataTable getTopBlog(string size)
        {
           return Data.TopList(this.LIST_TOP_SQL_begin + size + this.LIST_TOP_SQL_end);
        }

        //获得所有记录
        public DataTable getAll()
        {
            return Data.getDataTable("select * from " + MAIN_TABLE_NAME + "  order by topsize asc,id asc");  
        }


        //获得所有记录
        public DataTable getWorktable()
        {
            return Data.TopList("select top 20 * from " + MAIN_TABLE_NAME + " where iswork=1 order by topsize asc ");
        }


        //获得角色所有记录 
        public static DataTable getRoleTree(string role_id)
        {
            return Data.getDataTable("select a.*,(select count(*) from sys_role_set where menu_id=a.id and role_id=" + role_id + " )as ischecked from " + MAIN_TABLE_NAME + " a order by a.topsize asc,id asc");        
        }


        //获得所有用户菜单
        public DataTable getMenu(string role_id)
        {
            return Data.TopList("select * from " + MAIN_TABLE_NAME + " where ismenu=1 and id in(select menu_id from sys_role_set where role_id=" + role_id + ") order by topsize asc,id asc ");        
        }


        public static DataTable getUserMenu(string username,string parentid)
        {
            SysAdminVO adminvo = SysAdminBO.getVOByUserName(username);
            string sql = " select a.* from " + MAIN_TABLE_NAME + " a, sys_role_set b where a.ismenu=1 and a.parentid=" + parentid + " and a.id=b.menu_id and b.role_id=" + adminvo.roleid + " order by a.topsize asc,a.id asc ";


            return Data.getDataTable(sql);
        }


        //获得所有记录的 Hastable
        public Hashtable getHastable()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = this.getAll();
            for (int i = 0; i < dt.Rows.Count;i++ )
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
            DataTable dt = this.getAll();
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
            DataSet ds = Data.ExecuteDataSet(psize, req, CommandType.Text, this.LIST_SQL, null);
            DataTable dt = ds.Tables[0];            
            return dt;
        }
      
        /// <summary>
        /// 获得列表同步分页HTML，含分页参数
        /// </summary>
        public string getSplitPageHTML(HttpRequest req,string str_PageSize)
        {
            return Data.getPageHTML(req, str_PageSize, this.LIST_COUNT_SQL);        
        }



        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static SysMenuVO getVOByID(string id)
        {
            string sql = "select top 1 a.*,(select menuname from " + MAIN_TABLE_NAME + " where id=a.parentid )as parentname from " + MAIN_TABLE_NAME + " a ";
            if (id != null)
            {
                sql += " where a.id=" + id;
            }
            return (SysMenuVO)Data.getVO(SysMenuBO.VO_NAMESPACE, sql);
        }
        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public static SysMenuVO getVOByID(HttpRequest req,string id)
        {
            string sql = "select top 1 a.*,(select menuname from " + MAIN_TABLE_NAME + " where id=a.parentid )as parentname from " + MAIN_TABLE_NAME + " a ";
            if (id != null)
            {
                sql += " where a.id=" + id;
            }
            return (SysMenuVO)Data.getVO(SysMenuBO.VO_NAMESPACE, sql);
        }

        /// <summary>
        /// 根据ID获得单个记录值对象VO
        /// </summary>
        public  SysMenuVO getvo(string id)
        {
            string sql = "select a.*,(select menuname from " + MAIN_TABLE_NAME + " where id=a.parentid )as parentname from " + MAIN_TABLE_NAME + " a ";
            if (id != null) 
            {
                sql += " where a.id=" + id;
            }
            sql += " order by a.topsize asc,a.id asc";
            return (SysMenuVO)Data.getVO(SysMenuBO.VO_NAMESPACE, sql);
        }

        /// <summary>
        /// 是否拥有当前菜单的权限
        /// </summary>
        public static bool getPurview(HttpContext context, int sys_menu_id)
        {
            HttpRequest req = context.Request;
            string userid = req.Cookies["adminInfo"].Values["admin_id"].ToString();
            SysAdminVO adminvo = SysAdminBO.getVOByID(userid);
            string sql = " select count(a.id) from " + MAIN_TABLE_NAME + " a, sys_role_set b where a.ismenu=1 and a.id=" + sys_menu_id + " and a.id=b.menu_id and b.role_id=" + adminvo.roleid + " ";
            int count = Data.GetRsCount(sql);
            return count>0;
        }

        /// <summary>
        /// 获得当前页面的 SpaceMenuTmpVO
        /// </summary>
        public static SysMenuVO getTopVOByContext(HttpContext context)
        {
            HttpRequest req = context.Request;
            string userid = req.Cookies["adminInfo"].Values["admin_id"].ToString();
            SysAdminVO adminvo = SysAdminBO.getVOByID(userid);
            string path = req.Url.AbsolutePath.Replace("index.aspx", "");
            return getVOByPath(path, adminvo.roleid.ToString());
        }

        /// <summary>
        /// 获得当前页面的 SpaceMenuTmpVO
        /// </summary>
        public static SysMenuVO getVOByPath(string path,string role_id)
        {
            string sql = "select top 1 a.* from sys_menu a, sys_role_set b where a.ismenu = 1 and a.id = b.menu_id and b.role_id =  "+role_id + " and a.url = '"+path+"'  order by a.menulevel desc, a.id desc";
            return (SysMenuVO)Data.getVO(VO_NAMESPACE, sql);
        }

        /// <summary>
        /// 获得某个菜单的顶级菜单
        /// </summary>
        public static SysMenuVO getTopVO(SysMenuVO tmpvo, string root_tmp_id)
        {
            if (!tmpvo.parentid.ToString().Equals(root_tmp_id))
            {
                string sql = "select top 1 * from sys_menu where id=" + tmpvo.parentid + "";
                SysMenuVO overtmpvo = (SysMenuVO)Data.getVO(VO_NAMESPACE, sql);
                return getTopVO(overtmpvo, root_tmp_id);
            }
            else
            {
                return tmpvo;
            }
        }

        /// <summary>
        /// 获得一级菜单
        /// </summary>
        public static DataTable GetOneMenuList(HttpContext context)
        {
            DataTable list = null;
            HttpRequest req = context.Request;
            string s = "{\"success\":false,\"message\":\"会话已失效，请重新登录！\"}";
            if (req.Cookies["adminInfo"] != null)
            {
                string userid = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                SysAdminVO adminvo = SysAdminBO.getVOByID(userid);
                list = Data.getDataTable("select a.* from sys_menu a,sys_role_set b where a.parentid=0 and a.id=b.menu_id and role_id=" + adminvo.roleid.ToString());
            }
            return list;
        }


        /// <summary>
        ///获得二级菜单
        /// </summary>
        public static DataTable GetTwoMenuList(HttpContext context)
        {
            DataTable list = new DataTable();
            HttpRequest req = context.Request;
            if (req.Cookies["adminInfo"] != null)
            {
                string userid = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                SysAdminVO adminvo = SysAdminBO.getVOByID(userid);
                string path = req.Url.AbsolutePath.Replace("index.aspx", "");//当前一级菜单的路径
                SysMenuVO tmpvo = SysMenuBO.getVOByPath(path, adminvo.roleid.ToString());//
                if (tmpvo.id > 0)
                {
                    SysMenuVO topmenuvo = SysMenuBO.getTopVO(tmpvo, "0");
                    string sql = "select a.* from sys_menu a,sys_role_set b where a.parentid="+ topmenuvo.id+ " and a.id=b.menu_id and role_id="+ adminvo.roleid.ToString()+" order by a.topsize asc,a.id asc";
                    list = Data.getDataTable(sql);
                }
            }
            return list;
        }

        /// <summary>
        ///获得三级菜单
        /// </summary>
        public static DataTable GetThreeMenuList(HttpContext context, SysMenuVO twosysmenuvo)
        {
            DataTable list = new DataTable();
            HttpRequest req = context.Request;
            if (req.Cookies["adminInfo"] != null)
            {
                string userid = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                SysAdminVO adminvo = SysAdminBO.getVOByID(userid);
                if (twosysmenuvo.id > 0)
                {
                    string sql = "select a.* from sys_menu a,sys_role_set b where a.parentid=" + twosysmenuvo.id + " and a.id=b.menu_id and role_id=" + adminvo.roleid.ToString()+" order by a.topsize asc,a.id asc";
                    list = Data.getDataTable(sql);
                }
            }
            return list;
        }

        /// <summary>
        ///获得四级菜单
        /// </summary>
        public static DataTable GetFourMenuList(HttpContext context, SysMenuVO systhreesysmenuvo)
        {
            DataTable list = new DataTable();
            HttpRequest req = context.Request;
            if (req.Cookies["adminInfo"] != null)
            {
                string userid = req.Cookies["adminInfo"].Values["admin_id"].ToString();
                SysAdminVO adminvo = SysAdminBO.getVOByID(userid);
                if (systhreesysmenuvo.id > 0)
                {
                    string sql = "select a.* from sys_menu a,sys_role_set b where a.parentid=" + systhreesysmenuvo.id + " and a.id=b.menu_id and role_id=" + adminvo.roleid.ToString() + " order by a.topsize asc,a.id asc";
                    list = Data.getDataTable(sql);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        public static DataTable getMenuNavigationList(SysMenuVO overmenutmpvo)
        {
            DataTable list = UtilBO.getDataTable("sys_menu");
            return GetMenuNavigation(list, overmenutmpvo.id);

        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        public static DataTable GetMenuNavigation(DataTable list, int overid)
        {

            if (overid != 0)
            {
                DataTable table = Data.getDataTable("select * from sys_menu where id=" + overid);
                if (table.Rows.Count > 0)
                {
                    DataRow parentrow = table.Rows[0];
                    DataRow row = list.NewRow();
                    row["id"] = parentrow["id"];
                    row["parentid"] = parentrow["parentid"];
                    row["parentname"] = parentrow["parentname"];
                    row["menuname"] = parentrow["menuname"];
                    row["menucode"] = parentrow["menucode"];
                    row["url"] = parentrow["url"];
                    row["menulevel"] = parentrow["menulevel"];
                    row["topsize"] = parentrow["topsize"];
                    row["ismenu"] = parentrow["ismenu"];
                    row["iswork"] = parentrow["iswork"];
                    row["child_count"] = parentrow["child_count"];
                    row["menudesc"] = parentrow["menudesc"];
                    list.Rows.Add(row);
                    list = GetMenuNavigation(list, int.Parse(parentrow["parentid"].ToString()));

                }

            }
            return list;
        }


        /// <summary>
        ///获得菜单URL的变量信息
        /// </summary>
        public static string GetMenuParameter(HttpContext context, string url_parameter)
        {
            string parameter = "";
            if (UtilBO.isNotNull(url_parameter))
            {
                HttpRequest req = context.Request;
                string[] plist = url_parameter.Split('&');
                for (int i = 0; i < plist.Length; i++)
                {
                    parameter += i > 0 ? "&" : "?";
                    string[] vlist = plist[i].Split('@');
                    parameter += vlist[0] + req[vlist[1]];
                }

            }
            return parameter;
        }
    }
}