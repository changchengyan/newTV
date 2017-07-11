using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using Redsz;
using System.Management;

/// <summary>
/// redsz 的摘要说明


/// </summary>
namespace Redsz.DAO{
public class Data
{

    /// <summary>
    /// 执行各种SQL语句的实现方法-增删改
    /// </summary>
    public static int ExecuteNonQuery(CommandType cmdType, string cmdText, Hashtable ht)
    {
        return ExecuteNonQuery(cmdType, cmdText, ht, "MSSQL");
    }
    /// <summary>
    /// 执行各种SQL语句的实现方法-增删改
    /// </summary>
    public static int ExecuteNonQuery(CommandType cmdType, string cmdText, Hashtable ht, string conn_type)
    {
        string DataType = ConfigurationManager.ConnectionStrings[conn_type].ConnectionString;
        int val = 0;
        if (conn_type.IndexOf("MSSQL") > -1)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = getSqlConnection(conn_type);

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                if (ht != null)
                {
                    IDictionaryEnumerator ide = ht.GetEnumerator();
                    SqlParameter[] cmdParms = new SqlParameter[ht.Count];
                    int i = 0;
                    while (ide.MoveNext())
                    {
                        SqlParameter sp = Data.MakeParam("@" + ide.Key.ToString(), ide.Value.ToString());
                        cmdParms[i] = sp;
                        cmd.Parameters.Add(sp);
                        i++;
                    }
                }
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
            }
            catch
            {
                conn.Close();

                throw;
            }

        }
        else if (conn_type.IndexOf("ACCESS") > -1)
        {

        }


        return val;

    }

        
    public static void BulkToDB(DataTable dt, string szTableName)
    {
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString);
        SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConn);
        bulkCopy.DestinationTableName = szTableName;
        bulkCopy.BatchSize = dt.Rows.Count;
        try
        {
            sqlConn.Open();
            if (dt != null && dt.Rows.Count != 0)
                bulkCopy.WriteToServer(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            sqlConn.Close();
            if (bulkCopy != null)
                bulkCopy.Close();
        }
    } 


    /// <summary>
    /// 直接执行Sql语句
    /// </summary>
    public static void RunSql(string str_Sql)
    {
        Data.ExecuteNonQuery(CommandType.Text, str_Sql,null);
    }

    /// <summary>
    /// 直接执行Sql语句
    /// </summary>
    public static void RunSql(string str_Sql, string conn_type)
    {
        Data.ExecuteNonQuery(CommandType.Text, str_Sql, null, conn_type);
    }

    /// <summary>
    /// 获得某个表将要新增记录的id标识
    /// </summary>
    public static int getTableNextId(string tablename)
    {
        DataTable table = getDataTable("select   IDENT_CURRENT('" + tablename + "')+1 nextid");
        return int.Parse(table.Rows[0][0].ToString());
    }

    /// <summary>
    /// 获得某个表最后一条记录的id标识
    /// </summary>
    public static int getTableOverId(string tablename)
    {
        DataTable table = getDataTable("select  top 1 id from " + tablename + " order by id desc");
        return int.Parse(table.Rows[0][0].ToString());
    }


    /// <summary>
    /// 批量新增记录（DataTable）
    /// </summary>
    public static void InsertDataTable(string TableName, DataTable table)  
    {

        SqlBulkCopy sqlRevdBulkCopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString);
        try
        {
            
            sqlRevdBulkCopy.DestinationTableName = TableName;
            sqlRevdBulkCopy.NotifyAfter = table.Rows.Count;//有几行数据 
            sqlRevdBulkCopy.WriteToServer(table);//数据导入数据库  
            sqlRevdBulkCopy.Close();//关闭连接 
        }
        catch(Exception e)
        {
                sqlRevdBulkCopy.Close();//关闭连接 
                throw e;
        }
    }

    /// <summary>
    /// 新增记录
    /// </summary>
    public static void Insert(string TableName, Hashtable ht)
    {
        Insert(TableName, ht, "MSSQL");
    }

    /// <summary>
    /// 新增记录
    /// </summary>
    public static void Insert(string TableName, Hashtable ht, string conn_type)
    {
        // insert into iuser([nickname],[mobile]) values(@nickname,@mobile)
        string cmd_set = "";
        string cmd_val = "";
        IDictionaryEnumerator ide = ht.GetEnumerator();
        while (ide.MoveNext())
        
        {
            cmd_set += "[" + ide.Key.ToString() + "],";
            cmd_val += "@" + ide.Key.ToString() + ",";
        }
        cmd_set = "(" + cmd_set + ")";
        cmd_set = cmd_set.Replace(",)",")");

        cmd_val = " values("+cmd_val+")";
        cmd_val = cmd_val.Replace(",)",")");

        string cmd_sql = "insert into [" + TableName+"] " + cmd_set + cmd_val;
        Data.ExecuteNonQuery(CommandType.Text, cmd_sql, ht, conn_type);

    }

    /// <summary>
    /// 新增多条记录
    /// </summary>
    public static void InsertList(HttpRequest req, string tableName, string fieldKey, Hashtable nexus, string class_name)
    {
        Type myType = Type.GetType(class_name);// 获得“类”类型

        Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        string[] fks = req.Form.GetValues(fieldKey);
        for (int i = 0; i < fks.Length; i++)
        {
            if (!"".Equals(fks[i].Trim()))
            {

                Hashtable ht = new Hashtable();
                for (int k = 0; k < myPropertyInfo1.Length; k++)
                {
                    PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];
                    IDictionaryEnumerator enumerator = nexus.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        try
                        {
                            if (enumerator.Key.ToString().ToLower().Equals(myPropInfo.Name.ToLower()))
                            {
                                ht.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
                            }

                        }
                        catch
                        {

                        }
                    }


                    try
                    {
                        if (req.Form.GetValues(myPropInfo.Name)[i] != null && !"".Equals(req.Form.GetValues(myPropInfo.Name)[i]))
                        {
                            ht.Add(myPropInfo.Name, req.Form.GetValues(myPropInfo.Name)[i].Trim());
                        }
                    }
                    catch
                    {

                    }

                }
                Data.Insert(tableName, ht);
            }

        }
    }

    /// <summary>
    /// 批量跟新记录（DataTable）
    /// </summary>
    public static void UpdateDataTable(string TableName, DataTable table, string whereKey)
    {


        string sql = "SELECT syscolumns.name column_name ,systypes.name type_name,syscolumns.prec leng  FROM syscolumns, systypes WHERE syscolumns.xusertype = systypes.xusertype  AND syscolumns.id = object_id('" + TableName + "')";
        DataTable root_table = Data.getDataTable(sql);




        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString);
        conn.Open();
        SqlDataAdapter adapter = new SqlDataAdapter();
        string command_txt = "UPDATE [" + TableName + "]  SET ";
        int k = 0;
        for (int i = 0; i < table.Columns.Count; i++)
        {
            if (!whereKey.Equals(table.Columns[i].ColumnName))
            {
                if (k > 0)
                {
                    command_txt += ",";
                }
                command_txt += "[" + table.Columns[i].ColumnName + "]=@" + table.Columns[i].ColumnName + "";

                k++;
            }
        }
        command_txt += " WHERE id=@id";
        //sys.createTextFile("/log/command_txt.txt", command_txt);
        adapter.UpdateCommand = new SqlCommand(command_txt, conn);

        for (int i = 0; i < table.Columns.Count; i++)
        {
            for (int j = 0; j < root_table.Rows.Count; j++)
            {
                if (root_table.Rows[j]["column_name"].ToString().ToLower().Equals(table.Columns[i].ColumnName.ToLower()))
                {
                    int leng = int.Parse(root_table.Rows[j]["leng"].ToString());
                    if ("int".Equals(root_table.Rows[j]["type_name"].ToString()))
                    {
                        adapter.UpdateCommand.Parameters.Add("@" + table.Columns[i].ColumnName + "", SqlDbType.Int, leng, "" + table.Columns[i].ColumnName + "");
                    }
                    else if ("float".Equals(root_table.Rows[j]["type_name"].ToString()))
                    {
                        adapter.UpdateCommand.Parameters.Add("@" + table.Columns[i].ColumnName + "", SqlDbType.Float, leng, "" + table.Columns[i].ColumnName + "");
                    }
                    else if ("nvarchar".Equals(root_table.Rows[j]["type_name"].ToString()))
                    {
                        adapter.UpdateCommand.Parameters.Add("@" + table.Columns[i].ColumnName + "", SqlDbType.NVarChar, leng, "" + table.Columns[i].ColumnName + "");
                    }
                    else if ("ntext".Equals(root_table.Rows[j]["type_name"].ToString()))
                    {
                        adapter.UpdateCommand.Parameters.Add("@" + table.Columns[i].ColumnName + "", SqlDbType.NText, leng, "" + table.Columns[i].ColumnName + "");
                    }
                    else if ("datetime".Equals(root_table.Rows[j]["type_name"].ToString()))
                    {
                        adapter.UpdateCommand.Parameters.Add("@" + table.Columns[i].ColumnName + "", SqlDbType.DateTime, leng, "" + table.Columns[i].ColumnName + "");
                    }
                }
            }

        }
        adapter.Update(table);
        conn.Close();

    }

    /// <summary>
    /// 修改多条记录
    /// nexus 修改为固定的字段与值
    /// </summary>
    public static void UpdateList(HttpRequest req, string tableName,string fieldKey, Hashtable nexus, string class_name)
    {
        Type myType = Type.GetType(class_name);// 获得“类”类型

        Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        string[] fks = req.Form.GetValues(fieldKey);
        if (fks != null)
        {

            for (int i = 0; i < fks.Length; i++)
            {
                if (!"".Equals(fks[i].Trim()))
                {

                    Hashtable ht = new Hashtable();
                    for (int k = 0; k < myPropertyInfo1.Length; k++)
                    {
                        PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];
                        IDictionaryEnumerator enumerator = nexus.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            try
                            {
                                if (enumerator.Key.ToString().ToLower().Equals(myPropInfo.Name.ToLower()))
                                {
                                    ht.Add(enumerator.Key.ToString(), enumerator.Value.ToString());
                                }

                            }
                            catch
                            {

                            }
                        }


                        try
                        {
                            if (req.Form.GetValues(myPropInfo.Name)[i] != null && !"".Equals(req.Form.GetValues(myPropInfo.Name)[i]))
                            {
                                ht.Add(myPropInfo.Name, req.Form.GetValues(myPropInfo.Name)[i].Trim());
                            }
                        }
                        catch
                        {

                        }

                    }
                    Data.Update(tableName, fieldKey + "=@" + fieldKey, ht);
                }

            }
        }


    }
    /// <summary>
    /// 修改记录
    /// </summary>
    /// <param name="TableName">数据库表名</param>
    /// <param name="str_Where">传递条件,比如Id=@Id</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void Update(string TableName, string ht_Where, Hashtable ht)
    {

        Update( TableName, ht_Where, ht,"MSSQL");
    }

    /// <summary>
    /// 修改记录
    /// </summary>
    /// <param name="TableName">数据库表名</param>
    /// <param name="str_Where">传递条件,比如id=@id,@name=@name</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void Update(string TableName, string ht_Where, Hashtable ht,string conn_type)
    {
        // update iuser set [nickname]=@nickname,[mobile]=@mobile,[domainname]=@domainname,[mydesc]=@mydesc,[email]=@email,[provinceid]=@provinceid,[address]=@address,[cityid]=@cityid where id=@id 
        string cmd_set = "";        
        IDictionaryEnumerator ide = ht.GetEnumerator();
        while (ide.MoveNext())
        {
            if (ht_Where.ToLower().IndexOf(ide.Key.ToString())==-1)
            {
                cmd_set += "[" + ide.Key.ToString() + "]=@" + ide.Key.ToString()+",";
            }
        }
        string cmd_where = ht_Where.Replace(","," and ");
        string cmd_sql = "update " + TableName + " set " + cmd_set + "where " + cmd_where;
        cmd_sql = cmd_sql.Replace(",where"," where");
        Data.ExecuteNonQuery(CommandType.Text, cmd_sql, ht, conn_type);
    }


    /// <summary>
    /// Hashtable 转为 DataTable,并将where 字段放在最后行
    /// </summary>
    /// <param name="TableName">数据库表名</param>
    /// <param name="str_Where">传递条件,比如Id=@Id</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static DataTable HashtableToDataTable(string ht_where,Hashtable ht)
    {
        DataTable table = new DataTable();
        table.Columns.Add("key", typeof(string));
        table.Columns.Add("value", typeof(string));

        DataRow row;
        IDictionaryEnumerator ide = ht.GetEnumerator();
        while (ide.MoveNext())
        {
            if ((" " + ht_where + " ").ToString().ToLower().IndexOf((ide.Key.ToString() + "=@" + ide.Key.ToString()).ToLower()) == -1)
            {
                row = table.NewRow();
                row["key"] = ide.Key.ToString();
                row["value"] = ide.Value.ToString();
                table.Rows.Add(row);
            }
        }

        IDictionaryEnumerator ide2 = ht.GetEnumerator();
        while (ide2.MoveNext())
        {
            if ((" " + ht_where + " ").ToString().ToLower().IndexOf((ide2.Key.ToString() + "=@" + ide2.Key.ToString()).ToLower()) != -1)
            {
                row = table.NewRow();
                row["key"] = ide2.Key.ToString();
                row["value"] = ide2.Value.ToString();
                table.Rows.Add(row);
            }
        }

        
        return table;

    }

    /// <summary>
    /// 排序记录
    /// </summary>
    /// <param name="connString">数据库连接</param>
    /// <param name="TableName">数据库表名</param>
    /// <param name="str_Where">传递条件,比如Id=@Id</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void TopSize(string TableName, string wh_filed, string st_filed, HttpRequest req)
    {

        TopSize(TableName, wh_filed, st_filed, req, "MSSQL");

    }

    /// <summary>
    /// 排序记录
    /// </summary>
    /// <param name="connString">数据库连接</param>
    /// <param name="TableName">数据库表名</param>
    /// <param name="str_Where">传递条件,比如Id=@Id</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void TopSize(string TableName, string wh_filed, string st_filed, HttpRequest req, string conn_type)
    {
        if(req.Form[wh_filed]!=null && req.Form[st_filed]!=null)
        {
            string[] wh_list = req.Form.GetValues(wh_filed);
            string[] st_list = req.Form.GetValues(st_filed);
            if (wh_list.Length == st_list.Length)
            {
                for (int i = 0; i < wh_list.Length; i++)
                {
                    RunSql("update " + TableName + " set " + st_filed + "=" + st_list[i].ToString() + " where " + wh_filed + "=" + wh_list[i].ToString(), conn_type);
                }
            }

        }


    }


    /// <summary>
    /// 浏览次数累加
    /// </summary>
    /// <param name="connString">数据库连接</param>
    /// <param name="TableName">数据库表名</param>
    /// <param name="str_Where">传递条件,比如Id=@Id</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void ClickSize(string TableName, string id_filed, string idvalue, string click_filed, int clickAddValue)
    {
        RunSql("update " + TableName + " set " + click_filed + "=" + click_filed + "+" + clickAddValue + " where " + id_filed + "=" + idvalue);


    }




    /// <summary>
    /// 审核记录
    /// </summary>
    /// <param name="TableName">数据库表名</param>
    /// <param name="str_Where">传递条件,比如Id=@Id</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void Pass(string TableName, string wh_filed, string st_filed, HttpRequest req, bool b)
    {
        string v = "0";
        if (b) { v = "1"; }
        if (req.Form[wh_filed] != null)
        {
            string[] wh_list = req.Form.GetValues(wh_filed);
            for (int i = 0; i < wh_list.Length; i++)
            {
                RunSql("update " + TableName + " set " + st_filed + "=" + v + " where " + wh_filed + "=" + wh_list[i].ToString());
                //sys.errMessage += "update " + TableName + " set " + st_filed + "=" + v + " where " + wh_filed + "=" + wh_list[i].ToString();
            }
        }


    }



    /// 删除记录
    /// <param name="cmdType">sql语句类型</param>
    /// <param name="str_Sql">sql语句</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void Del(string TableName, string ht_Where, Hashtable ht)
    {
        string cmd_set = "";
        IDictionaryEnumerator ide = ht.GetEnumerator();
        while (ide.MoveNext())
        {
            if (ht_Where.ToLower().IndexOf(ide.Key.ToString())> -1)
            {
                cmd_set += ",[" + ide.Key.ToString() + "]=@" + ide.Key.ToString() + "";
               
            }
        }
        string cmd_where = cmd_set.Replace(",", " and ");
        string cmd_sql = "delete " + TableName + " where" + cmd_where;
        cmd_sql = cmd_sql.Replace("where and", "where ");
        //sys.errMessage = cmd_sql;

        Data.ExecuteNonQuery(CommandType.Text, cmd_sql, ht);



    }


    /// 删除记录－后台单表列表通用
    /// <param name="str_Sql">sql语句</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void DelList(string TableName, string wh_filed, HttpRequest req)
    {
        DelList(TableName, wh_filed, req,true);       
    }


    /// 删除记录－后台单表列表通用
    /// <param name="str_Sql">sql语句</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    public static void DelList(string TableName, string wh_filed, HttpRequest req,bool isNumberKey)
    {          
        
        if (req.Form[wh_filed] != null)
        {
            string[] wh_list = req.Form.GetValues(wh_filed);
            for (int i = 0; i < wh_list.Length; i++)
            {
                if (isNumberKey)
                {
                    RunSql("delete from " + TableName + " where " + wh_filed + "=" + wh_list[i].ToString());
                }
                else {
                    RunSql("delete from " + TableName + " where " + wh_filed + "='" + wh_list[i].ToString()+"'");
                }
            }
        }


    }


    /// <summary>
    /// 通过页大小，当前页数返回IList数据源


    /// </summary>
    /// <param name="int_PageSize">一页记录数</param>
    /// <param name="int_CurrentPageIndex">当前页数</param>
    /// <param name="Sql_Sel_Code">SQl语句</param>
    /// <param name="ht">传递条件哈希表</param>
    /// <param name="class_Name">实体类名</param>
    /// <returns>表示层传递过来的条件字段参数</returns>
    public static IList GetPageList(int int_PageSize, int int_CurrentPageIndex, string sql, string class_Name)
    {
        // ＝＝＝获得数据库源，返回IList为数据源＝＝＝


        IList Ilst = new ArrayList();


        // 当没有传递条件参数时作的操作

        DataSet ds = ExecuteDataSet(int_PageSize, int_CurrentPageIndex, CommandType.Text, sql, null);
        DataTable dt = ds.Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            Type myType = Type.GetType(class_Name);// 获得“类”类型


            Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
            // 获得类的所有属性数组


            PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // 循环属性数组，并给数组属性赋值


            for (int k = 0; k < myPropertyInfo1.Length; k++)
            {

                PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];
                Object filed_Val = dt.Rows[i][myPropInfo.Name];
                switch (myPropInfo.PropertyType.ToString())
                {
                    case "System.Int32":
                        myPropInfo.SetValue(o_Instance, (int)filed_Val, null);
                        break;
                    case "System.String":
                        myPropInfo.SetValue(o_Instance, filed_Val.ToString(), null);
                        break;
                    case "System.DateTime":
                        myPropInfo.SetValue(o_Instance, Convert.ToDateTime(filed_Val.ToString()), null);
                        break;
                }

            }
            Ilst.Add(o_Instance);

        }

        return Ilst;
    }

    /// <summary>
    /// SQL-SERVER 数据库连接
    /// </summary>
    public static SqlConnection getSqlConnection(string conn_type)
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings[conn_type].ConnectionString);
    }

    /// <summary>
    /// ACCESS 数据库连接
    /// </summary>
    public static OleDbConnection getAccessConnection(string conn_type)
    {
       return new OleDbConnection(ConfigurationManager.ConnectionStrings[conn_type].ConnectionString + System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.ConnectionStrings[conn_type].ProviderName));
    }


   

    /// <summary>
    /// 将DataTable 分页
    /// </summary>
    public static DataTable splitDatatable(DataTable dt, HttpRequest req, string str_PageSize) 
    {
        int int_PageSize = 10;
        int int_topage = 1;
        string str_topage = req.QueryString["topage"];

        if (str_PageSize != "" && str_PageSize != null)
        {
            int_PageSize = Int32.Parse(str_PageSize);
        }

        if (str_topage != "" && str_topage != null)
        {
            int_topage = Int32.Parse(str_topage);
        }  
        
        DataTable dtn = new DataTable();
        dtn = dt.Clone();
        for (int i = (int_topage - 1) * int_PageSize; i < int_topage * int_PageSize && i < dt.Rows.Count; i++)
        {
            DataRow row = dtn.NewRow();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                row[j] = dt.Rows[i][j];
            }
            dtn.Rows.Add(row);
        }

        return dtn;
    
    }


    /// <summary>
    /// 获得SQL_COUNT(*)分页HTML
    /// </summary>
    public static string getPageHTML(HttpRequest req, string str_PageSize, string sql)
    {
        int int_PageSize = 10;
        if (str_PageSize != "" && str_PageSize != null)
        {
            int_PageSize = Int32.Parse(str_PageSize);
        }

        //总记录数
        int rscount = Data.GetRsCount(sql);
        return getPageHTML(req, rscount, int_PageSize);
     }



     /// <summary>
     /// 获得DataTable分页HTML
     /// </summary>
     public static string getPageHTML(HttpRequest req, string str_PageSize, DataTable dt)
    {
        int int_PageSize = 10;
        if (str_PageSize != "" && str_PageSize != null)
        {
            int_PageSize = Int32.Parse(str_PageSize);
        }
        int count = dt.Rows.Count;
        return getPageHTML(req, count, int_PageSize);
        
        
    }



     /// <summary>
     /// 获得DataTable分页HTML（前台分页样式<< 1 2 3 4 ...5 >>）
     /// </summary>
    public static string getPageHTMLWeb(HttpRequest req, string str_PageSize, string sql)
    {
        int int_PageSize = 10;
        if (str_PageSize != "" && str_PageSize != null)
        {
            int_PageSize = Int32.Parse(str_PageSize);
        }
        //总记录数
        int rscount = Data.GetRsCount(sql);
        return getPageHTMLWeb(req, rscount, int_PageSize);
    }



     /// <summary>
     /// 获得DataTable分页HTML（前台迷你分页样式<<  下一页>>）
     /// </summary>
    public static string getPageHTMLWebMini(HttpRequest req, string str_PageSize, string sql)
    {
        int int_PageSize = 10;
        if (str_PageSize != "" && str_PageSize != null)
        {
            int_PageSize = Int32.Parse(str_PageSize);
        }

        //总记录数
        int rscount = Data.GetRsCount(sql);
        return getPageHTMLWebMini(req, rscount, int_PageSize);
        
        
    }


    /// <summary>
    /// 分页HTML实现方法(后台常见的分页模式)
    /// </summary>
    public static string getPageHTML(HttpRequest req, int count, int pagesize) 
    {
        string html = "";
        int int_PageSize = pagesize;
        //总记录数
        int rscount = count;
        //总页数


        int pagecount = 1;
        //URL条件
        string queryStringStr = "";
        //上一页


        string prevPage = "1";
        //下一页


        string nextPage = "1";
        //首页
        string firstPage = "1";
        //尾页
        string lastPage = "1";
        //下拉选框HTML
        string selectStr = "<select onchange=\"window.location=this.value\">";
        //当前页


        string topage = req.QueryString["topage"];
        if (topage == null || "".Equals(topage))
        {
            topage = "1";
        }
        int int_topage = int.Parse(topage);

        if (rscount % int_PageSize == 0)
        {
            pagecount = (int)rscount / int_PageSize;
        }
        else
        {
            pagecount = (rscount - (rscount % int_PageSize)) / int_PageSize + 1;
        }

        lastPage = pagecount + "";

        if (int_topage < pagecount)
        {
            nextPage = (int_topage + 1) + "";
        }
        else
        {
            nextPage = pagecount + "";
        }

        if (int_topage > 1)
        {
            prevPage = (int_topage - 1) + "";
        }

        foreach (string name in req.QueryString)
        {
            if (!name.Equals("topage") && !name.Equals("iscratehtml"))
            {
                queryStringStr += "&sort=" + req["sort"] + "&" + name + "=" + System.Web.HttpContext.Current.Server.UrlEncode(req.QueryString[name]);
            }
        }


        /* 禁止 Request.From 查询
        foreach (string fname in req.Form)
        {

            bool isaddvalue = true;
            foreach (string name in req.QueryString)
            {

                if (fname.Equals(name) && !fname.Equals("topage") || fname.Equals("__VIEWSTATE") || fname.Equals("__EVENTVALIDATION"))
                {
                    isaddvalue = false;
                }


            }

            if (isaddvalue) { queryStringStr += "&" + fname + "=" + req.Form[fname]; }
            
        }

         */
        int i_begin = 1;
        int i_end = pagecount;

        if (pagecount>50)
        {
            i_begin = int_topage - 24;

            if (i_begin<1)
            {
                i_begin = 1;
            }
            i_end = i_begin + 49;

        }

        for (int i = i_begin; i <= i_end; i++)
        {
            if (int_topage == i)
            {
                selectStr += "<option value=\"?topage=" + i + queryStringStr + "\" selected>" + i + "</option>";
            }
            else
            {
                selectStr += "<option value=\"?topage=" + i + queryStringStr + "\">" + i + "</option>";
            }

        }
        selectStr += "</select>";
        if (pagecount > 1)
        {
            html = "共" + rscount + "记录，" + pagecount + "页 <a href=\"?sort=" + req["sort"] + "&topage=1" + queryStringStr + "\" class=\"page_first\">首页</a> <a href=\"sort=" + req["sort"] + "&?topage=" + prevPage + queryStringStr + "\" class=\"page_prev\">上一页</a> <a href=\"?sort=" + req["sort"] + "&topage=" + nextPage + queryStringStr + "\" class=\"page_next\">下一页</a> <a href=\"?sort=" + req["sort"] + "&topage=" + lastPage + queryStringStr + "\" class=\"page_last\">尾页</a>  转到" + selectStr + "页";
        }
        else if (rscount==0)
        {
            html = "<font color=#ff3300>很抱歉，目前没有适合您搜索条件的信息</font>";        
        }
        return html;
    }




    /// <summary>
    /// 分页HTML实现方法(前台常见的分页模式)
    /// </summary>
    public static string getPageHTMLWeb(HttpRequest req, int count, int pagesize)
    {
        string html = "";
        int int_PageSize = pagesize;
        //总记录数
        int rscount = count;
        //总页数


        int pagecount = 1;
        //URL条件
        string queryStringStr = "";
        //上一页


        string prevPage = "1";
        //下一页


        string nextPage = "1";
        //首页
        string firstPage = "1";
        //尾页
        string lastPage = "1";
        //下拉选框HTML
        string selectStr = "";
        //当前页


        string topage = req.QueryString["topage"];
        if (topage == null || "".Equals(topage))
        {
            topage = "1";
        }
        int int_topage = int.Parse(topage);

        if (rscount % int_PageSize == 0)
        {
            pagecount = (int)rscount / int_PageSize;
        }
        else
        {
            pagecount = (rscount - (rscount % int_PageSize)) / int_PageSize + 1;
        }

        lastPage = pagecount + "";

        if (int_topage < pagecount)
        {
            nextPage = (int_topage + 1) + "";
        }
        else
        {
            nextPage = pagecount + "";
        }

        if (int_topage > 1)
        {
            prevPage = (int_topage - 1) + "";
        }

        foreach (string name in req.QueryString)
        {
            if (!name.Equals("topage") && !name.Equals("iscratehtml"))
            {
                queryStringStr += "&" + name + "=" + System.Web.HttpContext.Current.Server.UrlEncode(req.QueryString[name]);
            }
        }



        int i_begin = 1;
        int i_end = pagecount;

        if (pagecount > 5)
        {
            i_begin = int_topage - 2;

            if (i_begin < 1)
            {
                i_begin = 1;
            }
            i_end = i_begin + 4;

            if (i_end > pagecount)
            {
                i_end = pagecount;
                i_begin = i_end - 4;
            }

        }

        //左边 省略号之前的数字
        for (int i = 1; i < i_begin && i <= 2; i++)
        {
            selectStr += "<a  class=\"page_number\" href=\"?topage=" + i + queryStringStr + "\">" + i + "</a>";
        }
        if (i_begin > 3) { selectStr += "<span class=\"page_dot\">...</span>"; }


        //中间 数字（范围5页）
        for (int i = i_begin; i <= i_end; i++)
        {
            if (int_topage == i)
            {
                selectStr += "<a class=\"page_number_over\" href=\"?topage=" + i + queryStringStr + "\" selected>" + i + "</a>";
            }
            else
            {
                selectStr += "<a  class=\"page_number\" href=\"?topage=" + i + queryStringStr + "\">" + i + "</a>";
            }

        }



        //右边 省略号之后的数字
        if (i_end < pagecount)
        {
            if (i_end <= pagecount - 3) { selectStr += "<span class=\"page_dot\">...</span>"; }

            for (int i = pagecount - 1; i <= pagecount; i++)
            {
                if (i > i_end)
                {
                    selectStr += "<a  class=\"page_number\" href=\"?topage=" + i + queryStringStr + "\">" + i + "</a>";
                }
            }
        }


        if (pagecount > 1)
        {
            html = "<a title=\"上一页\" href=\"?topage=" + prevPage + queryStringStr + "\" class=\"page_number_prev\">上一页</a>   " + selectStr + "<a href=\"?topage=" + nextPage + queryStringStr + "\" title=\"下一页\" class=\"page_number_next\">下一页</a> <div class=\"page_total\">共 " + count + " 条记录，每页"+pagesize+"条</div>";
        }
        else if (rscount == 0)
        {
            html = "<font color=#ff3300>对不起，目前没有适合您搜索条件的数据...</font>";
        }
        return html;

    }

/// <summary>
    /// 分页HTML实现方法(前台常见的分页模式)
    /// </summary>
    public static string getPageHTMLWebMini(HttpRequest req, int count, int pagesize)
    {
        string html = "";
        int int_PageSize = pagesize;
        //总记录数
        int rscount = count;
        //总页数
        int pagecount = 1;

        //URL条件
        string queryStringStr = "";
        //上一页


        string prevPage = "1";
        //下一页


        string nextPage = "1";
        //首页
        string firstPage = "1";
        //尾页
        string lastPage = "1";
        //下拉选框HTML
        string selectStr = "";
        //当前页


        string topage = req.QueryString["topage"];
        if (topage == null || "".Equals(topage))
        {
            topage = "1";
        }
        int int_topage = int.Parse(topage);

        if (rscount % int_PageSize == 0)
        {
            pagecount = (int)rscount / int_PageSize;
        }
        else
        {
            pagecount = (rscount - (rscount % int_PageSize)) / int_PageSize + 1;
        }

        lastPage = pagecount + "";

        if (int_topage < pagecount)
        {
            nextPage = (int_topage + 1) + "";
        }
        else
        {
            nextPage = pagecount + "";
        }

        if (int_topage > 1)
        {
            prevPage = (int_topage - 1) + "";
        }

        foreach (string name in req.QueryString)
        {
            if (!name.Equals("topage") && !name.Equals("iscratehtml"))
            {
                queryStringStr += "&" + name + "=" + System.Web.HttpContext.Current.Server.UrlEncode(req.QueryString[name]);
            }
        }

        
        if (pagecount > 1)
        {
            html = "<a title=\"上一页\" href=\"?topage=" + prevPage + queryStringStr + "\" class=\"page_number_prev\">&nbsp;</a>  <a href=\"?topage=" + nextPage + queryStringStr + "\" class=\"page_number_next\">下一页</a>";
        }
        else if (rscount == 0)
        {
            html = "";
        }
        return html;

    }


    /// <summary>
    /// 分页HTML实现方法，触摸屏普通分页
    /// </summary>
    public static string getPageHTMLTouchBase(HttpRequest req, string count, string sql)
    {
        string html = "";
        int int_PageSize = int.Parse(count);
        //总记录数

        int rscount = Data.GetRsCount(sql);
        //总页数

        int pagecount = 1;
        //URL条件
        string queryStringStr = "";

        //上一页
        string prevPage = "1";

        //下一页
        string nextPage = "1";

        //首页
        string firstPage = "1";

        //尾页
        string lastPage = "1";

        string topage = req.QueryString["topage"];
        if (topage == null || "".Equals(topage))
        {
            topage = "1";
        }
        int int_topage = int.Parse(topage);

        if (rscount % int_PageSize == 0)
        {
            pagecount = (int)rscount / int_PageSize;
        }
        else
        {
            pagecount = (rscount - (rscount % int_PageSize)) / int_PageSize + 1;
        }

        lastPage = pagecount + "";

        if (int_topage < pagecount)
        {
            nextPage = (int_topage + 1) + "";
        }
        else
        {
            nextPage = pagecount + "";
        }

        if (int_topage > 1)
        {
            prevPage = (int_topage - 1) + "";
        }

        foreach (string name in req.QueryString)
        {
            if (!name.Equals("topage") && !name.Equals("iscratehtml"))
            {
                queryStringStr += "&" + name + "=" + System.Web.HttpContext.Current.Server.UrlEncode(req.QueryString[name]);
            }
        }


        int i_begin = 1;
        int i_end = pagecount;

        if (pagecount > 50)
        {
            i_begin = int_topage - 24;

            if (i_begin < 1)
            {
                i_begin = 1;
            }
            i_end = i_begin + 49;

        }

       
        if (pagecount > 1)
        {
            html = "<div class='base_page_left'>共" + rscount + "条记录 </div> <div onclick=\"window.location='?topage=1" + queryStringStr + "'\" class=\"base_page_first\">&nbsp;</div> <div onclick=\"window.location='?topage=" + prevPage + queryStringStr + "'\" class=\"base_page_prev\">&nbsp;</div> <div onclick=\"window.location='?topage=" + nextPage + queryStringStr + "'\" class=\"base_page_next\">&nbsp;</div> <div onclick=\"window.location='?topage=" + lastPage + queryStringStr + "'\" class=\"base_page_last\">&nbsp;</div><div class='base_page_right'>当前第" + int_topage + "/" + pagecount + "页</div> ";
            
        }
        else if (rscount == 0)
        {
            html = "<font color=#ff3300>很抱歉，目前没有适合您搜索条件的信息</font>";
        }
        return html;

    }


    /// <summary>
    /// 分页HTML实现方法，触摸屏(可拖动式的分页)
    /// </summary>
    public static string getPageHTMLTouch(HttpRequest req, string count, int rscount)
    {
        string html = "";
        int int_PageSize = int.Parse(count);
       
        //总页数

        int pagecount = 1;
        //URL条件
        string queryStringStr = "";

        //上一页
        string prevPage = "1";

        //下一页
        string nextPage = "1";

        //首页
        string firstPage = "1";

        //尾页
        string lastPage = "1";


        string topage = req.QueryString["topage"];
        if (topage == null || "".Equals(topage))
        {
            topage = "1";
        }
        int int_topage = int.Parse(topage);

        if (rscount % int_PageSize == 0)
        {
            pagecount = (int)rscount / int_PageSize;
        }
        else
        {
            pagecount = (rscount - (rscount % int_PageSize)) / int_PageSize + 1;
        }

        lastPage = pagecount + "";

        if (int_topage < pagecount)
        {
            nextPage = (int_topage + 1) + "";
        }
        else
        {
            nextPage = pagecount + "";
        }

        if (int_topage > 1)
        {
            prevPage = (int_topage - 1) + "";
        }

        foreach (string name in req.QueryString)
        {
            if (!name.Equals("topage") && !name.Equals("iscratehtml"))
            {
                queryStringStr += "&" + name + "=" + System.Web.HttpContext.Current.Server.UrlEncode(req.QueryString[name]);
            }
        }


        int i_begin = 1;
        int i_end = pagecount;

        if (pagecount > 50)
        {
            i_begin = int_topage - 24;

            if (i_begin < 1)
            {
                i_begin = 1;
            }
            i_end = i_begin + 49;

        }

        if (pagecount > 1)
        {
            string path = req.ServerVariables["Path_Info"];
            html = "<div class='base_page_left'>共" + rscount + "条记录 </div> <div ";
            if (int_topage > 1)
            {
                html += "  onclick=\"top.touch.openPrevBrother('" + path + "?topage=" + prevPage + queryStringStr + "',window)\" ";
            }
            else
            {
                html += "  onclick=\"top.touch.showMessage('已经是第一页了');\" ";

            }
            html += " class=\"base_page_prev\">&nbsp;</div> <div  ";

            if (int_topage < pagecount)
            {
                html += " onclick=\"top.touch.openNextBrother('" + path + "?topage=" + nextPage + queryStringStr + "',window)\" ";
            }
            else
            {
                html += " onclick=\"top.touch.showMessage('已经是最后一页了');\" ";

            }

            html += " class=\"base_page_next\">&nbsp;</div>  <div class='base_page_right'>当前第" + int_topage + "/" + pagecount + "页</div> ";
            if (int_topage < pagecount)
            {
                html += "<script>window.document.body.nextpage=\"" + path + "?topage=" + nextPage + queryStringStr + "\";</script>";
            }
            else
            {
                html += "<script>window.document.body.nextpage=\"\";</script>";
            }
        }
        else if (rscount == 0)
        {
            html = "<font color=#ff3300>很抱歉，目前没有适合您搜索条件的信息</font>";
        }
        return html;

    }


    /// <summary>
    /// 分页HTML实现方法，触摸屏(可拖动式的分页)
    /// </summary>
    public static string getPageHTMLTouch(HttpRequest req, string count, string sql)
    {

        //总记录数
        int rscount = Data.GetRsCount(sql);
        return getPageHTMLTouch(req,count,rscount);
    }



    /// 返回一个SqlParameter实例
    /// </summary>
    /// <param name="ParamName">字段名</param>
    /// <param name="stype">字段类型</param>
    /// <param name="size">范围</param>
    /// <param name="Value">赋值</param>
    /// <returns>返回一个SqlParameter实例</returns>
    public static SqlParameter MakeParam(string ParamName, System.Data.SqlDbType stype, int size, Object Value)
    {
        SqlParameter para = new SqlParameter(ParamName, Value);
        para.SqlDbType = stype;
        para.Size = size;
        return para;
    }
    /// 获得SqlParameter实例
    /// </summary>
    /// <param name="ParamName">字段名</param>
    /// <param name="Value">赋值</param>
    /// <returns>返回一个SqlParameter实例</returns>
    public static SqlParameter MakeParam(string ParamName, string Value)
    {
        return new SqlParameter(ParamName, Value);
    }

    /// 获得SqlParameter实例
    /// </summary>
    /// <param name="ParamName">字段名</param>
    /// <param name="Value">赋值</param>
    /// <returns>返回一个SqlParameter实例</returns>
    public static OleDbParameter MakeOleParam(string ParamName, string Value)
    {
        return new OleDbParameter(ParamName, Value);
    }


    /// 获得插入Sql语句
    /// </summary>
    /// <param name="TableName">数据库表名</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    /// <returns>返回插入Sql语句</returns>
    public static string GetInsertSqlbyHt(string TableName, DataTable datatable)
    {
        string str_Sql = "";
        string before = "";
        string behide = "";
        for (int i = 0; i < datatable.Rows.Count;i++ )
        {
            if (i == 0)
            {
                before = "([" + datatable.Rows[i]["key"].ToString() + "]";
            }
            else if (i + 1 == datatable.Rows.Count)
            {
                before = before + ",[" + datatable.Rows[i]["key"].ToString() + "])";
            }
            else
            {
                before = before + ",[" + datatable.Rows[i]["key"].ToString() + "]";
            }

        }
        behide = " Values" + before.Replace(",", ",@").Replace("(", "(@");
        behide = behide.Replace("[","");
        behide = behide.Replace("]","");
        str_Sql = "Insert into " + TableName + before + behide;
        return str_Sql;

    }


    /// 获得删除Sql语句
    /// <param name="Table">数据库表名</param>
    /// <param name="ht_Where">传递条件,比如Id=@Id</param>
    /// <param name="ht">表示层传递过来的哈希表对象</param>
    /// <returns>返回删除sql语句</returns>
    public static string GetDelSqlbyHt(string Table, string ht_Where, DataTable datatable)
    {
        string str_Sql = "";

        for (int i = 0; i < datatable.Rows.Count; i++)
        {
            if (i == 0)
            {
                if (ht_Where.ToString().ToLower().IndexOf((datatable.Rows[i]["key"].ToString() + "=@" + datatable.Rows[i]["key"].ToString()).ToLower()) == -1)
                {
                    str_Sql = datatable.Rows[i]["key"].ToString() + "=@" + datatable.Rows[i]["key"].ToString();
                }
            }
            else
            {
                if (ht_Where.ToString().ToLower().IndexOf(("@" + datatable.Rows[i]["key"].ToString() + " ").ToLower()) == -1)
                {
                    str_Sql = str_Sql + "," + datatable.Rows[i]["key"].ToString() + "=@" + datatable.Rows[i]["key"].ToString();
                }

            }
        }
        if (ht_Where == null || ht_Where.Replace(" ", "") == "")  // 更新时候没有条件
        {
            str_Sql = "Delete " + Table;
        }
        else
        {
            str_Sql = "Delete " + Table + " where " + ht_Where;
        }
        return str_Sql;
    }


    /// <summary>
    /// 通过传递条件获得记录条数


    /// </summary>
    /// <param name="connString">数据库连接</param>
    /// <param name="str_Sql">Sql语句</param>
    /// <returns>返回记录条数</returns>
    public static int GetRsCount(string str_Sql)
    {
        int count = 0;
        DataTable table = getDataTable(str_Sql);
        if(table.Rows.Count>0)
        {
            if (table.Rows[0][0] != null && !"".Equals(table.Rows[0][0].ToString()))
            {
            count = int.Parse(table.Rows[0][0].ToString());
            }
        }
        return count;
        
        
        //return (int)ExecuteScalar(CommandType.Text, str_Sql, null);
    }


    /// <summary>
    /// 根据SQL获得Hashtable记录
    /// </summary>
    public static Hashtable getListBySql(string sql)
    {
        DataTable dt = TopList(sql);
        Hashtable ht = new Hashtable();
        for (int i = 0; i < dt.Rows.Count; i++) 
        {
            ht.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());        
        }
        return ht;
    }


    /// <summary>
    /// 根据SQL获得DataTable记录
    /// </summary>
    public static DataTable getDataTableBySql(string sql)
    {
        DataTable dt = TopList(sql);
        return dt;
    }


    public static IList getLevelListBySql(string sql)
    {


        IList list = new ArrayList();

        DataTable dt = TopList(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string[] options = new string[3];
            options[0] = dt.Rows[i][0].ToString();
            options[1] = dt.Rows[i][1].ToString();
            options[2] = dt.Rows[i][2].ToString();
            list.Add(options);
        }
        return list; 
      
    }

/// <summary>
    /// 获得DateSet实例(获得TopList记录)
    /// </summary>
    public static DataTable TopList(string sql)
    {
        return TopList(sql, "MSSQL");
    }
    /// <summary>
    /// 获得DateSet实例(获得TopList记录)
    /// </summary>
    public static DataTable TopList(string sql, string conn_type)
    {

        string DataType = ConfigurationManager.ConnectionStrings[conn_type].ConnectionString;
        DataTable dt = new DataTable();
        if (conn_type.IndexOf("MSSQL") > -1)
        {
            SqlConnection conn = getSqlConnection(conn_type);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                conn.Close();
                DataSet ds = new DataSet();
                da.Fill(ds, "12news1234567890");
                dt = ds.Tables[0];
            }
            catch
            {
                conn.Close();
                throw;
            }

        }
        else if (conn_type.IndexOf("ACCESS") > -1)
        {
            OleDbConnection conn = getAccessConnection(conn_type);
             try
             {
                 conn.Open();
                 OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
                 conn.Close();
                 DataSet ds = new DataSet();
                 adapter.Fill(ds, "12news1234567890");
                 adapter.Dispose();
                 dt = ds.Tables[0];
             }
             catch
             {
                 conn.Close();
                 throw;
             }
        }



        return dt;


    }

    //根据SQL获得DataTable
    public static DataTable getDataTable(string sql)
    {
        return TopList(sql);
    }
    //根据SQL获得DataTable
    public static DataTable getDataTable(string sql,string conn_type)
    {
        return TopList(sql,conn_type);
    }


    /// 获得DateSet实例(获得单页记录)
    public static DataSet ExecuteDataSet(string str_PageSize, HttpRequest req, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {

        string topage = req.QueryString["topage"];
        if (topage == null || "".Equals(topage))
        {
            topage = "1";
        }
        int int_topage = int.Parse(topage);
        int int_PageSize = 1;
        if (str_PageSize != "" && str_PageSize != null)
        {
            int_PageSize = Int32.Parse(str_PageSize);
        }

        return ExecuteDataSet(int_PageSize, int_topage, cmdType, cmdText, cmdParms);

    }
/// <summary>
    ///根据起始值和数量获得DataTable
    /// </summary>
    public static DataTable ExecuteDataTable(int begin, int count, string sql)
    {

        return ExecuteDataTable(begin, count, sql, "MSSQL");
    }

    /// <summary>
    ///根据起始值和数量获得DataTable
    /// </summary>
    public static DataTable ExecuteDataTable(int begin, int count, string sql, string conn_type)
    {
        DataTable table = new DataTable();
        DataSet ds = new DataSet();
        if (conn_type.IndexOf("MSSQL") > -1)
        {
            SqlConnection conn = getSqlConnection(conn_type);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.CommandType = CommandType.Text;                
                conn.Close();                   
                da.Fill(ds, begin, count, "12news1234567890");                
                table = ds.Tables[0];
                da.Dispose();
                conn.Close();
            }
            catch
            {
                conn.Close();              
            }

        }
        else if (conn_type.IndexOf("ACCESS") > -1)
        {
            OleDbConnection conn = getAccessConnection(conn_type);
            try
            {
                conn.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
                da.SelectCommand.CommandType = CommandType.Text;
                conn.Close();
                da.Fill(ds, begin, count, "12news1234567890");
                table = ds.Tables[0];
                da.Dispose();
                conn.Close();
            }
            catch
            {
                conn.Close();
            }
        }

        return table;

    }


 /// 获得DateSet实例(获得单页记录)
    /// <param name="int_PageSize">一页显示的记录数</param>
    /// <param name="int_CurrentPageIndex">当前页码</param>
    /// <param name="connString">数据库连接串</param>
    /// <param name="cmdType">Sql语句类型</param>
    /// <param name="cmdText">Sql语句</param>
    /// <param name="cmdParms">Parm数组</param>
    /// <returns></returns>
    public static DataSet ExecuteDataSet(int int_PageSize, int int_CurrentPageIndex, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
        return ExecuteDataSet("MSSQL",int_PageSize, int_CurrentPageIndex, cmdType, cmdText, cmdParms);
    }
    /// 获得DateSet实例(获得单页记录)
    /// <param name="int_PageSize">一页显示的记录数</param>
    /// <param name="int_CurrentPageIndex">当前页码</param>
    /// <param name="connString">数据库连接串</param>
    /// <param name="cmdType">Sql语句类型</param>
    /// <param name="cmdText">Sql语句</param>
    /// <param name="cmdParms">Parm数组</param>
    /// <returns></returns>
    public static DataSet ExecuteDataSet(string conn_type,int int_PageSize, int int_CurrentPageIndex, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
    {
       
        DataSet ds = new DataSet();
        if (conn_type.IndexOf("MSSQL") > -1)
        {
            SqlConnection conn = getSqlConnection(conn_type);
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
                da.SelectCommand.CommandType = cmdType;
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        da.SelectCommand.Parameters.Add(parm);
                }
                conn.Close();

                if (int_PageSize == 0 && int_CurrentPageIndex == 0)
                {
                    da.Fill(ds, "12news1234567890");
                }
                else
                {
                    int int_Page = int_PageSize * (int_CurrentPageIndex - 1);
                    if (int_Page < 0)
                    {
                        int_Page = 0;
                    }
                    da.Fill(ds, int_Page, int_PageSize, "12news1234567890");
                }
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }

        }
        else if (conn_type.IndexOf("ACCESS")>-1)
        {
            OleDbConnection conn = getAccessConnection(conn_type);
            conn.Open();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(cmdText, conn);
                if (cmdParms != null)
                {
                    foreach (SqlParameter parm in cmdParms)
                        da.SelectCommand.Parameters.Add(parm);
                }

                conn.Close();
                if (int_PageSize == 0 && int_CurrentPageIndex == 0)
                {
                    da.Fill(ds, "12news1234567890");
                }
                else
                {
                    int int_Page = int_PageSize * (int_CurrentPageIndex - 1);
                    if (int_Page < 0)
                    {
                        int_Page = 0;
                    }
                    da.Fill(ds, int_Page, int_PageSize, "12news1234567890");
                }
                da.Dispose();
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        return ds;
    }





    /// <summary>
    /// 获得IList分页Sql语句
    /// </summary>
    /// <param name="Table">数据库表</param>
    /// <param name="ht_Where">条件</param>
    /// <param name="orderby">排序</param>
    /// <param name="ht">表示层传递过来的条件字段参数</param>
    /// <param name="class_Name">实体类名</param>
    /// <returns></returns>
    public static string GetPageListSqlbyHt(string Table, string ht_Where, string orderby, Hashtable ht, String class_Name)
    {
        string str_Sql = "";

        // 选择类型只能实现 Select * from table where a=@a and b=@b效果
        // where 后面优先权，当ht_Where不为空或者不为null，条件应该是ht_Where参数，否则，用ht做循环



        Type myType = Type.GetType(class_Name);// 获得“类”类型


        Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
        // 获得类的所有属性数组


        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        // 循环属性数组，并给数组属性赋值


        for (int k = 0; k < myPropertyInfo1.Length; k++)
        {
            PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];
            if (k == 0)
            {
                str_Sql = myPropInfo.Name.ToString();
            }
            else
            {
                str_Sql = str_Sql + "," + myPropInfo.Name.ToString();
            }

        }
        if (ht_Where == "" || ht_Where == null)
        {
            string str_Ht = "";
            if (ht != null) // 用ht做条件


            {

                IDictionaryEnumerator et = ht.GetEnumerator();
                int k = 0;
                while (et.MoveNext())
                {
                    if (k == 0)
                    {
                        str_Ht = " " + et.Key.ToString() + "=@" + et.Key.ToString();
                    }
                    else
                    {
                        str_Ht = str_Ht + " and " + et.Key.ToString() + "=@" + et.Key.ToString();
                    }
                    k = k + 1;
                }
            }
            if (orderby == "" || orderby == null)
            {
                if (str_Ht != "")
                {
                    str_Sql = "Select " + str_Sql + " From " + Table + " where " + str_Ht;
                }
                else
                {
                    str_Sql = "Select " + str_Sql + " From " + Table;
                }
            }
            else
            {
                if (str_Ht != "")
                {
                    str_Sql = "Select " + str_Sql + " From " + Table + " where " + str_Ht + "  order by " + orderby;

                }
                else
                {
                    str_Sql = "Select " + str_Sql + " From " + Table;
                }

            }

        }
        else // 用ht_Where做条件


        {
            if (orderby == "" || orderby == null)
            {
                str_Sql = "Select " + str_Sql + " From " + Table + " Where " + ht_Where;
            }
            else
            {
                str_Sql = "Select " + str_Sql + " From " + Table + " where " + ht_Where + "  order by " + orderby;
            }

        }

        return str_Sql;

    }


    /// <summary>
    /// 获得数据库所有表与字段及属性的方法
    /// </summary>
    public static Redsz.VO.QueryVO getQueryList(HttpRequest req, string psize, string sql_query, string sql_count)
    {
        Redsz.VO.QueryVO v = new Redsz.VO.QueryVO();
        DataSet ds = Data.ExecuteDataSet(psize, req, CommandType.Text, sql_query, null);
        v.list = ds.Tables[0];
        v.html = Data.getPageHTMLWeb(req, psize, " select count(*) from (" + sql_count + ") ct ");
        v.total = Data.GetRsCount(" select count("+(v.list.Columns[0].ColumnName) +") from (" + sql_count + ") ct ");
        return v;
    }

    /// <summary>
    /// 获得数据库所有表与字段及属性的方法（手机专用）
    /// </summary>
    public static Redsz.VO.QueryVO getQueryListMini(HttpRequest req, string psize, string sql_query, string sql_count)
    {
        Redsz.VO.QueryVO v = new Redsz.VO.QueryVO();
        DataSet ds = Data.ExecuteDataSet(psize, req, CommandType.Text, sql_query, null);
        v.list = ds.Tables[0];
        v.html = Data.getPageHTMLWebMini(req, psize, " select count(*) from (" + sql_count + ") ct ");
        return v;
    }

    /// <summary>
    /// 个人用户登录,并更新登录时间和登录次数等信息
    /// </summary>
    public static bool userLogin(String username, String password)
    {
        return userLogin(username, password, true, false);
    }


    /// <summary>
    /// 个人用户登录,实现
    /// </summary>
    public static bool userLogin(String username, String password, bool update,bool httponly)
    {
        bool b = false;
        SqlConnection connection = getSqlConnection("MSSQL_USER");
        try
        {
            


            SqlCommand command = new SqlCommand("select * from iuser a where username='" + username + "' and password='" + password + "'", connection);
            command.CommandType = CommandType.Text;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {
                b = true;
                
                HttpCookie cookie = new HttpCookie("iuserInfo");

                if (httponly)
                {
                    cookie.HttpOnly = true;
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(7, 0, 0, 0);
                    cookie.Expires = dt.Add(ts);
                }

                cookie.Values.Add("username", HttpUtility.UrlEncode(reader["username"].ToString()));
                cookie.Values.Add("nickname", HttpUtility.UrlEncode(reader["nickname"].ToString()));
                cookie.Values.Add("id", HttpUtility.UrlEncode(reader["id"].ToString()));
                cookie.Values.Add("lastlogin", HttpUtility.UrlEncode(reader["lastlogin"].ToString()));
                cookie.Values.Add("logincount", HttpUtility.UrlEncode(reader["logincount"].ToString()));
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);                
                
                //如果登录的用户未出现当前企业用户表中则需要增加    
                
                
                if (update)
                {
                    RunSql("update iuser set lastlogin='" + System.DateTime.Now + "',logincount=logincount+1 where username='" + reader["username"].ToString() + "'", "MSSQL_USER");
                }
            }
            connection.Close();
        }
        catch
        {
            connection.Close();
            throw;
        }
        return b;
    }

    /// <summary>
    /// 自动将Sql查询中(SqlDataReader)的单条(第一条)记录的字段对应到VO中，返回 Object，默认为 MSSQL 主业务数据库
    /// </summary>
    public static Object getVO(string class_name, string sql)
    {
        return getVO(class_name, sql, "MSSQL");
    }
   

    /// <summary>
    /// 自动将Sql查询中(SqlDataReader)的单条(第一条)记录的字段对应到VO中，返回 Object
    /// </summary>
    public static Object getVO(string class_name,string sql,string conn_type)
    {

        string DataType = ConfigurationManager.ConnectionStrings[conn_type].ConnectionString;
        
        Type myType = Type.GetType(class_name);// 获得“类”类型
        Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);


        if (conn_type.IndexOf("MSSQL")>-1)
        {
            SqlConnection conn = getSqlConnection(conn_type);
            try
            {
                SqlCommand command = new SqlCommand(sql, conn);
                command.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();         

                if (reader.Read())
                {


                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // 循环属性数组，并给数组属性赋值


                        for (int k = 0; k < myPropertyInfo1.Length; k++)
                        {
                            PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];


                            if (reader.GetName(i).ToLower().Equals(myPropInfo.Name.ToLower()) && reader[i] != null)
                            {

                                switch (myPropInfo.PropertyType.ToString())
                                {
                                    case "System.Int32":
                                        myPropInfo.SetValue(o_Instance, int.Parse(reader[i].ToString().Trim()), null);
                                        break;
                                    case "System.Double":
                                        myPropInfo.SetValue(o_Instance, double.Parse(reader[i].ToString().Trim()), null);
                                        break;
                                    case "System.String":
                                        myPropInfo.SetValue(o_Instance, sys.showInfo(reader[i].ToString().Trim()), null);
                                        break;
                                    case "System.DateTime":
                                        myPropInfo.SetValue(o_Instance, Convert.ToDateTime(reader[i].ToString().Trim()), null);
                                        break;

                                }

                            }

                        }
                    }
                }
                conn.Close();

            }
            catch
            {
                conn.Close();
                throw;
            }

        }
        else if (conn_type.IndexOf("ACCESS")>-1 )
        {
            OleDbConnection conn = getAccessConnection(conn_type);
            conn.Open();
            try
            {
                OleDbCommand command = new OleDbCommand(sql, conn);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {


                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        // 循环属性数组，并给数组属性赋值


                        for (int k = 0; k < myPropertyInfo1.Length; k++)
                        {
                            PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];


                            if (reader.GetName(i).ToLower().Equals(myPropInfo.Name.ToLower()) && reader[i] != null)
                            {

                                switch (myPropInfo.PropertyType.ToString())
                                {
                                    case "System.Int32":
                                        myPropInfo.SetValue(o_Instance, int.Parse(reader[i].ToString().Trim()), null);
                                        break;
                                    case "System.Double":
                                        myPropInfo.SetValue(o_Instance, double.Parse(reader[i].ToString().Trim()), null);
                                        break;
                                    case "System.String":
                                        myPropInfo.SetValue(o_Instance, sys.showInfo(reader[i].ToString().Trim()), null);
                                        break;
                                    case "System.DateTime":
                                        myPropInfo.SetValue(o_Instance, Convert.ToDateTime(reader[i].ToString().Trim()), null);
                                        break;

                                }

                            }

                        }
                    }
                }
                reader.Dispose();
                conn.Close();
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        return o_Instance;
    }


    /// <summary>
    /// 自动将Request中的字段对应到VO中，返回 Object
    /// </summary>
    /// <param name="req">HttpRequest</param>
    public static Object getVO(HttpRequest req,string class_name) 
    {
        Type myType = Type.GetType(class_name);// 获得“类”类型


        Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        // 循环属性数组，并给数组属性赋值


        for (int k = 0; k < myPropertyInfo1.Length; k++)
        {
            PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];

            foreach (string name in req.Form)
            {
                if (name.Equals(myPropInfo.Name))
                {                 

                    switch (myPropInfo.PropertyType.ToString())
                    {
                        case "System.Int32":
                            myPropInfo.SetValue(o_Instance, int.Parse(req.Form[name]), null);
                            break;
                        case "System.Double":
                            myPropInfo.SetValue(o_Instance, double.Parse(req.Form[name]), null);
                            break;
                        case "System.String":
                            myPropInfo.SetValue(o_Instance, req.Form[name].Trim(), null);                          
                            break;
                        case "System.DateTime":
                            myPropInfo.SetValue(o_Instance, Convert.ToDateTime(req.Form[name].Trim()), null);
                            break;
                    }

                }
            }

        }

       return o_Instance;
    }



    /// <summary>
    /// 自动将DataTable 中的单行Row 转换为VO，返回 Object
    /// </summary>
    public static DataTable getTableAppendVO(DataTable datatale, string class_name,string append_cell_name)
    {

        datatale.Columns.Add(append_cell_name, typeof(Object));
        Type myType = Type.GetType(class_name);// 获得“类”类型

        for (int i = 0; i < datatale.Rows.Count; i++)
        {

            Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
            PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
              
                for (int k = 0; k < myPropertyInfo1.Length; k++)
                {
                    PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];
                    for (int j = 0; j < datatale.Columns.Count;j++ )
                    {

                        if (datatale.Columns[j].ColumnName.ToLower().Equals(myPropInfo.Name.ToLower()) && datatale.Rows[i][datatale.Columns[j].ColumnName] != null && !class_name.Equals(datatale.Columns[j].ColumnName.ToLower()))
                        {
                            switch (myPropInfo.PropertyType.ToString())
                            {
                                case "System.Int32":
                                    myPropInfo.SetValue(o_Instance, int.Parse(datatale.Rows[i][datatale.Columns[j].ColumnName].ToString()), null);
                                    break;
                                case "System.String":
                                    myPropInfo.SetValue(o_Instance, sys.showInfo(datatale.Rows[i][datatale.Columns[j].ColumnName].ToString()), null);
                                    break;
                                case "System.DateTime":
                                    myPropInfo.SetValue(o_Instance, Convert.ToDateTime(datatale.Rows[i][datatale.Columns[j].ColumnName].ToString()), null);
                                    break;
                            }

                        }

                    }
                   
                }
                datatale.Rows[i][append_cell_name] = o_Instance;

        }

        return datatale;
          
    }



    /// <summary>
    /// 获得 ValueObject 的所有字段及字段类型
    /// </summary>
    public static Hashtable getObjectParameter(string class_name)
    {

        Hashtable ht = new Hashtable();
        Type myType = Type.GetType(class_name);// 获得“类”类型       

        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        // 循环属性数组，并给数组属性赋值

        for (int k = 0; k < myPropertyInfo1.Length; k++)
        {
            PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];

                    switch (myPropInfo.PropertyType.ToString())
                    {
                        case "System.Int32":
                            ht.Add(myPropInfo.Name,"int");
                            break;
                        case "System.String":
                            ht.Add(myPropInfo.Name, "string");
                            break;
                        case "System.DateTime":
                            ht.Add(myPropInfo.Name, "datetime");
                            break;
                    }
        }

        return ht;
    }




    /*
    *自动将Request中的字段与VO字段相同的数据将填充到Hashtable，返回 Hashtable
    */

    public static Hashtable getHashtable(HttpRequest req, string class_name)
    {
        Type myType = Type.GetType(class_name);// 获得“类”类型


        Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        Hashtable ht = new Hashtable();
        // 循环属性数组，Hashtable赋值        
        for (int k = 0; k < myPropertyInfo1.Length; k++)
        {
            PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];

            foreach (string name in req.Form)
            {
                if (name.ToLower().Equals(myPropInfo.Name.ToLower()))
                {
                    ht.Add(name, req.Form[name].Trim());
                }
            }

        }

        return ht;
    }



    /*
    *自动将Request中的字段与VO字段相同的数据将填充到Hashtable，返回 Hashtable
    */

    public static Hashtable getHashtable(HttpRequest req, string class_name, bool nullWrite)
    {
        Type myType = Type.GetType(class_name);// 获得“类”类型



        Object o_Instance = System.Activator.CreateInstance(myType); // 实例化类
        PropertyInfo[] myPropertyInfo1 = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        Hashtable ht = new Hashtable();
        // 循环属性数组，Hashtable赋值        
        for (int k = 0; k < myPropertyInfo1.Length; k++)
        {
            PropertyInfo myPropInfo = (PropertyInfo)myPropertyInfo1[k];

            foreach (string name in req.Form)
            {
                if (name.ToLower().Equals(myPropInfo.Name.ToLower()))
                {
                    if (nullWrite)
                    {
                        //允许空值

                        ht.Add(name, req.Form[name].Trim());
                    }
                    else if (!"".Equals(req.Form[name].Trim()))
                    {   
                        //允许非空值

                        ht.Add(name, req.Form[name].Trim());                    
                    }
                }
            }

        }

        return ht;
    }




    /// <summary>
    /// 自动将VO中string不为"",int 不为0 的值转换为Hashtable
    /// </summary>
    public static Hashtable getHashtableByVO(Object obj)
    {
        Hashtable ht = new Hashtable();
        Type myType = obj.GetType();
        MemberInfo[] myMemberInfor = myType.GetMembers();
        //sys.errMessage = "";

        for (int i = 0; i < myMemberInfor.Length; i++)
        {

            if ("Property".Equals(myMemberInfor[i].MemberType.ToString()))
            {
                object v = myType.GetProperty(myMemberInfor[i].Name).GetValue(obj, null);
                if (v != null)
                {

                    string protype = myType.GetProperty(myMemberInfor[i].Name).PropertyType.ToString();
                    string sv = "";
                    switch (protype)
                    {
                        case "System.Int32":
                            if (!v.ToString().Equals("0")) { ht.Add(myMemberInfor[i].Name, v.ToString()); }
                            break;
                        case "System.String":
                            if (!v.ToString().Equals("")) { ht.Add(myMemberInfor[i].Name, v.ToString()); }
                            break;
                        default: break;
                    }


                }

            }
        }
        return ht;

    }


    /// <summary>
    /// 获得数据库所有表与字段及属性的方法
    /// </summary>
    public static DataTable getTableAll(string tablename)
    {
       return  Data.TopList("SELECT  表名=case when a.colorder=1 then d.name else '' end, 表说明=case when a.colorder=1 then isnull(f.value,'') else '' end, 字段序号=a.colorder, 字段名=a.name, 标识=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end, 主键=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (  SELECT name FROM sysindexes WHERE indid in(   SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid  ))) then '√' else '' end, 类型=b.name, 占用字节数=a.length, 长度=COLUMNPROPERTY(a.id,a.name,'PRECISION'), 小数位数=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), 允许空=case when a.isnullable=1 then '√'else '' end, 默认值=isnull(e.text,''), 字段说明=isnull(g.[value],'')FROM syscolumns a left join systypes b on a.xtype=b.xusertype inner join sysobjects d on a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties' left join syscomments e on a.cdefault=e.id left join sysproperties g on a.id=g.id and a.colid=g.smallid   left join sysproperties f on d.id=f.id and f.smallid=0 where d.name='" + tablename + "' order by a.id,a.colorder "); 

    }

    //GetDatabaseConnStr
    public static string _0x0001(int iTag)
    {
        string szConnType = string.Empty;
        if(iTag == 0x0001)
            szConnType = "MSSQL";
        else
            szConnType = "MSSQL_WEIXIN";
        return ConfigurationManager.ConnectionStrings[szConnType].ConnectionString;
    }

    public static string getWenzhangTitle(int iTag)
    {
        string szConnType = string.Empty;
        if (iTag == 1)
            szConnType = "MSSQL";
        else
            szConnType = "MSSQL_WEIXIN";
        return ConfigurationManager.ConnectionStrings[szConnType].ConnectionString;
    }

}
}
