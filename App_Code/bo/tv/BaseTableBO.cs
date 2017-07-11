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
using Newtonsoft.Json;

namespace Redsz.BO
{
    /// <summary>
    /// rays大数据总览
    /// </summary>
    public class BaseTableBO
    {
        public static double getBaseByKey(string key)
        {
             double value = 0.0;
            string sql = "select top 1 base_value from base_table where base_key='"+key+"'";
            DataTable dt = Data.getDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                value = double.Parse(dt.Rows[0]["base_value"].ToString());
            }
            return value;
        }
    }
}
