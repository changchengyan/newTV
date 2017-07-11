using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json.Linq;
using Redsz.BO;


namespace Redsz
{



    /// <summary>
    /// common 的摘要说明
    /// </summary>
    public class Common
    {
        /// <summary>  
        /// 对List进行随机排序  
        /// </summary>  
        /// <param name="listT"></param>  
        /// <returns></returns>  
        public  static List<T> RandomSortList<T>(List<T> listT)
        {
            Random random = new Random();
            List<T> newList = new List<T>();
            foreach (T item in listT)
            {
                newList.Insert(random.Next(newList.Count), item);
            }
            return newList;
        }

        /// <summary>
        /// 保存Excel文件
        /// <para>Excel的导入导出都会在服务器生成一个文件</para>
        /// <para>路径：UpFiles/ExcelFiles</para>
        /// </summary>
        /// <param name="file">传入的文件对象</param>
        /// <returns>如果保存成功则返回文件的位置;如果保存失败则返回空</returns>
        public static string SaveExcelFile(HttpPostedFile file)
        {
            try
            {
                var fileName = file.FileName.Insert(file.FileName.LastIndexOf('.'), "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/upload/ExcelFiles"), fileName);
                string directoryName = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                file.SaveAs(filePath);
                return filePath;
            }
            catch
            {
                return string.Empty;
            }
        }
        //获取蓝海真实数据,成交金额，用户总数，总浏览量和成交订单总数
        public static Hashtable getRealData()
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            object real_data = objCache["real_data"];
            Hashtable ht = (Hashtable)real_data;
            if (real_data != null && ht.Count > 0)
            {
                return (Hashtable)real_data;
            }
            else
            {
                if (ht == null)
                {
                    ht = new Hashtable();
                }
                try
                {
                    string ret = UtilBO.MethodGET("http://api.chubanyun.net/exapi/v1.0/tv/getTotalData", "utf-8");
                    JObject json = (JObject) JObject.Parse(ret);
                    if ("0".Equals(json["errCode"].ToString()))
                    {
                        ht.Add("real_total_money", json["data"]["totalMoney"].ToString());
                        ht.Add("real_user_count", json["data"]["totalUserCount"].ToString());
                        ht.Add("real_browser_count", json["data"]["totalBrowserCount"].ToString());
                    }

                    ret = UtilBO.MethodGET("http://api.chubanyun.net/exapi/v1.0/tv/getTotalTradeCount", "utf-8");
                    json = (JObject) JObject.Parse(ret);
                    if ("0".Equals(json["errCode"].ToString()))
                    {
                        ht.Add("real_orderform_count", json["data"]["totalOrderformCount"].ToString());
                    }

                    objCache.Insert("real_data", ht, null, DateTime.Now.AddMinutes(1),
                        System.Web.Caching.Cache.NoSlidingExpiration);
                }
                catch (Exception ex)
                {
                    ht.Add("real_total_money", 0);
                    ht.Add("real_user_count", 0);
                    ht.Add("real_browser_count", 0);
                    ht.Add("real_orderform_count", 0);
                    return ht;
                }
            }
            return (Hashtable)objCache["real_data"];
        }

    }
}