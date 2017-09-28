using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Redsz.DAO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Redsz.VO;
using Redsz;
using Redsz.BO;

namespace Redsz.BO
{

    /// <summary>
    /// BigDataBO 的摘要说明
    /// </summary>
    public class BigDataBO
    {

        private static bool success = true;
        private static string message = string.Empty;

        #region rays数据总览

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetBaseData(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            ht["book_count"] = GetBookCount();//书籍数
            ht["total_money"] = GetPlatformTotalMoney();//总金额
            ht["user_count"] = GetTotalUserCount();//读者数
            return JsonConvert.SerializeObject(ht);
        }

        //获取大数据总览左侧实时统计数据
        public static string GetCurrData(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            int user_count = GetTotalUserCount();
            ht["online_user_count"] = GetOnLineUserCont(user_count);
            //ht["today_user_count"] = GetTodayUserCont(user_count);
            return JsonConvert.SerializeObject(ht);
        }
        //获取交易金额
        public static string GetTotalMoney(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            //把totalMoney写入cookie中
            string totalMoney = GetPlatformTotalMoney().ToString();
            if (context.Request.Cookies["totalMoney"] == null)
            {
                HttpCookie cookie = new HttpCookie("totalMoney");
                cookie.Value = totalMoney;
                cookie.Expires = DateTime.Now.AddMinutes(2);
            }
            else
            {
                totalMoney = context.Request.Cookies["totalMoney"].Value;
            }
            ht["total_money"] = totalMoney;
            return JsonConvert.SerializeObject(ht);
        }
        private static int GetTodayUserCont(int userCount)
        {
            int todayUserCount = 0;
            double[] per_hour_money_prop =
            {
                0.001, 0.00101, 0.00103, 0.00104, 0.00105, 0.00106, 0.0255, 0.0388, 0.0477,
                0.0599, 0.0683, 0.0691, 0.0693,0.0694, 0.0698, 0.0705, 0.0799,
                0.0887, 0.0921, 0.0953, 0.0976, 0.0999, 0.1011, 0.1019, 0.1031
            };
            Random rm = new Random();
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd ") + " 00:00:00");
            TimeSpan diffTime = DateTime.Now - today;
            todayUserCount = (int)(userCount * per_hour_money_prop[DateTime.Now.Hour]) + Convert.ToInt32(diffTime.TotalSeconds * 0.7);
            return todayUserCount;
        }

        /// <summary>
        /// 根据用户数和时间推算出当前在线用户人数
        /// </summary>
        /// <param name="userCount"></param>
        /// <returns></returns>
        private static int GetOnLineUserCont(int userCount)
        {
            int onlineUserCount = 0;
            double[] per_hour_money_prop =
            {
                0.001, 0, 0, 0, 0, 0, 0.025, 0.038, 0.047,
                0.0598, 0.0605, 0.0614, 0.0578, 0.0517, 0.0522, 0.0577, 0.0599,
                0.057, 0.06, 0.0602, 0.0614, 0.0599, 0.056, 0.051, 0.033
            };
            Random rm = new Random();
            onlineUserCount = (int)(userCount * per_hour_money_prop[DateTime.Now.Hour]) +
                              rm.Next(DateTime.Now.Minute, 600) + rm.Next(DateTime.Now.Second, 600);
            return onlineUserCount;
        }

        //  //获取上个月，上上个月浏览趋势对比 
        //  public static string GetTwoYearData(HttpContext context)
        //  {
        //      ReportTimeVO lastOneReportTimeVo = ReportTimeBO.getVOByTime(DateTime.Now.AddMonths(-1));
        //      ReportTimeVO lastTwoReportTimeVo = ReportTimeBO.getVOByTime(DateTime.Now.AddMonths(-2));
        //      ReportTimeVO lastThreeReportTimeVo = ReportTimeBO.getVOByTime(DateTime.Now.AddMonths(-3));
        //      DateTime lastOneFirstDay = new DateTime(lastOneReportTimeVo.year, lastOneReportTimeVo.month, 1);
        //      DateTime lastTwoFirstDay = new DateTime(lastTwoReportTimeVo.year, lastTwoReportTimeVo.month, 1);
        //      DateTime lastThreeFirstDay = new DateTime(lastThreeReportTimeVo.year, lastThreeReportTimeVo.month, 1);
        //      Hashtable ht = new Hashtable();
        //      List<object> lastOneMonthList = new List<object>();
        //      List<object> lastTwoMonthList = new List<object>();
        //      List<object> lastThreeMonthList = new List<object>();
        //      double[] oneProp =
        //     {
        //          0.823, 0.732, 0.772, 1.888, 1.230, 0.676, 0.625, 1.538, 0.647 ,0.577,
        //          0.998, 0.805, 1.614, 1.578, 1.617, 0.922, 1.577, 1.099, 0.933 ,0.883,
        //          1.157, 1.061, 1.602, 0.861, 0.759, 0.856, 1.851, 1.903, 1.351, 1.103, 1.003
        //      };
        //      double[] twoProp =
        //    {
        //          0.998, 1.305, 1.114, 0.278, 0.217, 0.822, 1.277, 0.199, 0.933 ,1.283,
        //          0.323, 0.432, 0.372, 0.888, 1.230, 0.176, 1.025, 0.738, 1.347 ,0.777,
        //          0.556, 0.951, 0.903, 1.051, 1.103, 1.203, 0.457, 0.361, 1.302, 1.361, 0.659
        //      };
        //      double[] threeProp =
        //    {
        //          0.061, 0.602, 1.361, 0.859, 0.756, 0.951, 0.903, 1.051, 1.103, 1.203,
        //          0.823, 1.432, 1.272, 1.388, 1.030, 0.973, 0.323, 1.238, 0.837 ,1.077,
        //          0.998, 0.605, 0.614, 1.178, 0.717, 0.822, 0.277, 1.399, 0.933 ,0.883,1.057

        //      };
        //      string sql = string.Format(@"select sum(click_count) num, datepart(dd, [datetime]) as day from reality_orderform_browser
        // where report_time_id={0}
        //group by datepart(dd, [datetime])", lastOneReportTimeVo.id);
        //      DataTable dt = Data.getDataTable(sql);
        //      for (int i = 0; i < dt.Rows.Count; i++)
        //      {
        //          lastOneMonthList.Add(new
        //          {
        //              month = lastOneFirstDay.AddDays(i).ToString("MM-dd"),
        //              num = Convert.ToInt32(Convert.ToInt32(dt.Rows[i]["num"]) * oneProp[i])
        //          });
        //      }

        //      //上上个月
        //      string sql2 = string.Format(@"select sum(click_count) num, datepart(dd, [datetime]) as day from reality_orderform_browser
        // where report_time_id={0}
        //group by datepart(dd, [datetime])", lastTwoReportTimeVo.id);
        //      DataTable dt2 = Data.getDataTable(sql2);
        //      for (int j = 0; j < dt2.Rows.Count; j++)
        //      {
        //          lastTwoMonthList.Add(new
        //          {
        //              month = lastTwoFirstDay.AddDays(j).ToString("MM-dd"),
        //              num = Convert.ToInt32(Convert.ToInt32(dt2.Rows[j]["num"]) * twoProp[j])
        //          });
        //      }
        //      //上上上个月
        //      string sql3 = string.Format(@"select sum(click_count) num, datepart(dd, [datetime]) as day from reality_orderform_browser
        // where report_time_id={0}
        //group by datepart(dd, [datetime])", lastTwoReportTimeVo.id);
        //      DataTable dt3 = Data.getDataTable(sql3);
        //      for (int k = 0; k < dt3.Rows.Count; k++)
        //      {
        //          lastThreeMonthList.Add(new
        //          {
        //              month = lastThreeFirstDay.AddDays(k).ToString("MM-dd"),
        //              num = Convert.ToInt32(Convert.ToInt32(dt3.Rows[k]["num"]) * threeProp[k])
        //          });
        //      }
        //      ht["lastOneMonthList"] = lastOneMonthList;
        //      ht["lastTwoMonthList"] = lastTwoMonthList;
        //      ht["lastThreeMonthList"] = lastThreeMonthList;
        //      return JsonConvert.SerializeObject(ht);

        //  }

        //获取上个月，上上个月浏览趋势对比 
        public static string GetTwoYearData(HttpContext context)
        {
            Random rnd = new Random();
            DateTime time = DateTime.Now;
            DateTime lastOneFirstDay = new DateTime(time.AddMonths(-1).Year, time.AddMonths(-1).Month, 1);
            DateTime lastOneLastDay = new DateTime(time.AddMonths(-1).Year, time.AddMonths(-1).Month, 1).AddMonths(1);
            DateTime lastTwoFirstDay = new DateTime(time.AddMonths(-2).Year, time.AddMonths(-2).Month, 1);
            DateTime lastTwoLastDay = new DateTime(time.AddMonths(-2).Year, time.AddMonths(-2).Month, 1).AddMonths(1);
            DateTime lastThreeFirstDay = new DateTime(time.AddMonths(-3).Year, time.AddMonths(-3).Month, 1);
            DateTime lastThreeLastDay = new DateTime(time.AddMonths(-3).Year, time.AddMonths(-3).Month, 1).AddMonths(1);
            Hashtable ht = new Hashtable();
            List<object> lastOneMonthList = new List<object>();
            List<object> lastTwoMonthList = new List<object>();
            List<object> lastThreeMonthList = new List<object>();
            double[] oneProp =
           {
                      0.823, 0.732, 0.972, 1.588, 1.230, 0.676, 0.625, 1.538, 0.647 ,0.577,
                      0.998, 0.805, 1.614, 1.078, 1.617, 0.922, 1.577, 1.099, 0.933 ,0.883,
                      1.157, 1.061, 1.602, 0.861, 0.759, 0.856, 1.651, 1.803, 1.351, 1.103, 1.003
                  };
            double[] twoProp =
          {
                      0.998, 1.305, 1.114, 0.378, 0.417, 0.822, 1.277, 0.199, 0.933 ,1.283,
                      0.323, 0.432, 0.372, 0.888, 1.230, 0.176, 1.025, 0.738, 1.347 ,0.777,
                      0.556, 0.951, 0.903, 1.051, 1.103, 1.203, 0.457, 0.361, 1.302, 1.361, 0.659
                  };
            double[] threeProp =
          {
                      0.061, 0.602, 1.361, 0.859, 0.756, 0.951, 0.903, 1.051, 1.103, 1.203,
                      0.823, 1.432, 1.272, 1.388, 1.030, 0.973, 0.323, 1.238, 0.837 ,1.077,
                      0.998, 0.605, 0.614, 1.178, 0.717, 0.822, 0.277, 1.399, 0.933 ,0.883,1.057

                  };
            int basecount = 500000;
            string sql = string.Format(@"select count(id) num, datepart(dd, [datetime]) as day from browser
       where [datetime]>='{0}' and [datetime]<'{1}'
      group by datepart(dd, [datetime])", lastOneFirstDay, lastOneLastDay);
            DataTable dt = Data.getDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lastOneMonthList.Add(new
                {
                    month = lastOneFirstDay.AddDays(i).ToString("MM-dd"),
                    num = Convert.ToInt32((basecount+Convert.ToInt32(dt.Rows[i]["num"])) * oneProp[i])
                });
            }

            //上上个月
            string sql2 = string.Format(@"select count(id) num, datepart(dd, [datetime]) as day from browser
       where [datetime]>='{0}' and [datetime]<'{1}'
      group by datepart(dd, [datetime])", lastTwoFirstDay, lastTwoLastDay);
            DataTable dt2 = Data.getDataTable(sql2);
            for (int j = 0; j < dt2.Rows.Count; j++)
            {
                lastTwoMonthList.Add(new
                {
                    month = lastTwoFirstDay.AddDays(j).ToString("MM-dd"),
                    num = Convert.ToInt32((basecount + Convert.ToInt32(dt2.Rows[j]["num"])) * twoProp[j])
                });
            }
            //上上上个月
            string sql3 = string.Format(@"select count(id) num, datepart(dd, [datetime]) as day from browser
       where [datetime]>='{0}' and [datetime]<'{1}'
      group by datepart(dd, [datetime])", lastThreeFirstDay, lastThreeLastDay);
            DataTable dt3 = Data.getDataTable(sql3);
            for (int k = 0; k < dt3.Rows.Count; k++)
            {
                lastThreeMonthList.Add(new
                {
                    month = lastThreeFirstDay.AddDays(k).ToString("MM-dd"),
                    num = Convert.ToInt32((basecount + Convert.ToInt32(dt3.Rows[k]["num"])) * threeProp[k])
                });
            }
            ht["lastOneMonthList"] = lastOneMonthList;
            ht["lastTwoMonthList"] = lastTwoMonthList;
            ht["lastThreeMonthList"] = lastThreeMonthList;
            return JsonConvert.SerializeObject(ht);

        }
        #endregion

        #region rays出版收益结构

        //编辑收益、作者收益、rays出版人
        public static string GetBaseReport(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            double total_money = GetPlatformTotalMoney();
            string sql =
                "select count(id)+(select top 1 base_value from  base_table where base_key='channel_count')  channal_count";
            //运营终端数
            //sql +=
            //    " ,((select sum(total_money) from report_time b where datediff(month, CONVERT(datetime,b.[year] + '-'+ b.[month] +'-'+'1'), getdate())>0)+(select isnull(sum(money+freight),0) from reality_orderform_" +
            //    DateTime.Now.Year + "_" + DateTime.Now.Month + ")) total_money"; //总成交额
            sql +=
                " ,(select count(id) from space)+(select top 1 base_value from  base_table where base_key='space_count')+(select count(id) from channel)+(select top 1 base_value from  base_table where base_key='channel_count')+(select count(id) from adviser)+(select top 1 base_value from  base_table where base_key='adviser_count')+(select count(id) from merchant)+(select top 1 base_value from  base_table where base_key='merchant_count') cooperate_count ";
            //出版人
            sql += " from channel  ";
            DataTable item = Data.getDataTable(sql);
            if (item.Rows.Count > 0)
            {
                ht["merchant_money"] = Convert.ToInt32(item.Rows[0]["channal_count"]);
                ht["channel_money"] = Convert.ToDouble(total_money * 0.6).ToString("F2");
                ht["cooperate_count"] = Convert.ToInt32(item.Rows[0]["cooperate_count"]);
            }

            return JsonConvert.SerializeObject(ht);
        }

        //获取出版人比例
        public string GetRoleChart(HttpContext context)
        {
            string sql =
                "select  count(id)+(select top 1 base_value from  base_table where base_key='space_count') space_count";
            //出版社数据
            sql +=
                ",(select count(id) from channel)+(select top 1 base_value from  base_table where base_key='channel_count') channal_count";
            //渠道数
            sql +=
                ",(select count(id) from adviser)+(select top 1 base_value from  base_table where base_key='adviser_count') adviser_count";
            //顾问数
            sql +=
                " ,(select count(id) from merchant)+(select top 1 base_value from  base_table where base_key='merchant_count') merchant_count ";
            //商户数
            sql +=
                ",(select count(id) from space)+(select top 1 base_value from  base_table where base_key='space_count')+(select count(id) from channel)+(select top 1 base_value from  base_table where base_key='channel_count')+(select count(id) from adviser)+(select top 1 base_value from  base_table where base_key='adviser_count')+(select count(id) from merchant)+(select top 1 base_value from  base_table where base_key='merchant_count') cooperate_count";
            //出版人总数
            sql += " from space";
            DataTable item = Data.getDataTable(sql);
            Hashtable ht = new Hashtable();
            if (item.Rows.Count > 0)
            {
                List<object> lists = new List<object>();
                lists.Add(
                    new
                    {
                        name = "编辑",
                        value = (Convert.ToDouble(item.Rows[0]["adviser_count"]) /
                             Convert.ToDouble(item.Rows[0]["cooperate_count"])).ToString("0.00%")
                    });
                lists.Add(
                    new
                    {
                        name = "作者",
                        value = (Convert.ToDouble(item.Rows[0]["merchant_count"]) /
                             Convert.ToDouble(item.Rows[0]["cooperate_count"])).ToString("0.00%")
                    });
                lists.Add(
                    new
                    {
                        name = "运营",
                        value = (Convert.ToDouble(item.Rows[0]["channal_count"]) /
                             Convert.ToDouble(item.Rows[0]["cooperate_count"])).ToString("0.00%")
                    });

                lists.Add(
                    new
                    {
                        name = "出版社",
                        value = (Convert.ToDouble(item.Rows[0]["space_count"]) /
                             Convert.ToDouble(item.Rows[0]["cooperate_count"])).ToString("0.00%")
                    });
                ht["people_data"] = lists;
            }

            return JsonConvert.SerializeObject(ht);
        }

        //获取出版人收益比例
        public static string GetProfitReport(HttpContext context)
        {
            string sql = "select  top 1 base_value  adviser_percent"; //顾问收益比例
            sql +=
                " ,(select top 1 base_value from  base_table where base_key='channel_money_porportion')channel_percent";
            //渠道收益比例
            sql += " ,(select top 1 base_value from  base_table where base_key='space_money_porportion')space_percent ";
            //出版社收益比例
            sql +=
                " ,(select top 1 base_value from  base_table where base_key='merchant_money_porportion')merchant_percent ";
            //商户收益比例
            //sql +=
            //    " ,((select sum(total_money) from report_time b where datediff(month, CONVERT(datetime,b.[year] + '-'+ b.[month] +'-'+'1'), getdate())>0)+(select isnull(sum(money+freight),0) from reality_orderform_" +
            //    DateTime.Now.Year + "_" + DateTime.Now.Month + ")) total_money"; //总成交额
            sql +=
    " ,(select count(id) from space)+(select top 1 base_value from  base_table where base_key='space_count')+(select count(id) from channel)+(select top 1 base_value from  base_table where base_key='channel_count')+(select count(id) from adviser)+(select top 1 base_value from  base_table where base_key='adviser_count')+(select count(id) from merchant)+(select top 1 base_value from  base_table where base_key='merchant_count') cooperate_count ";
            //出版人
            sql += " from base_table where base_key='adviser_money_porportion'";
            DataTable item = Data.getDataTable(sql);
            Hashtable ht = new Hashtable();
            if (item.Rows.Count > 0)
            {
                double total_money = GetPlatformTotalMoney() * 0.6;
                object obj = new
                {
                    channelPercent = Convert.ToDouble(item.Rows[0]["channel_percent"]).ToString("0%"),
                    channelMoney = (total_money * Convert.ToDouble(item.Rows[0]["channel_percent"])).ToString("F2"),
                    adviserPercent = Convert.ToDouble(item.Rows[0]["adviser_percent"]).ToString("0%"),
                    adviserMoney = (total_money * Convert.ToDouble(item.Rows[0]["adviser_percent"])).ToString("F2"),
                    merchantPercent = Convert.ToDouble(item.Rows[0]["merchant_percent"]).ToString("0%"),
                    merchantMoney = (total_money * Convert.ToDouble(item.Rows[0]["merchant_percent"])).ToString("F2"),
                    spacePercent = Convert.ToDouble(item.Rows[0]["space_percent"]).ToString("0%"),
                    spaceMoney = (total_money * Convert.ToDouble(item.Rows[0]["space_percent"])).ToString("F2"),
                    total_money = total_money.ToString("F0"),
                    cooperate_count = Convert.ToInt32(item.Rows[0]["cooperate_count"])
                };

                ht["money_data"] = obj;
            }

            return JsonConvert.SerializeObject(ht);
        }

        #endregion

        #region rays体系出版社销售额TOP

        //获取渠道收益排行
        public static string GetTopChannelMoney(HttpContext context, string num)
        {
            ReturnDataVO model = new ReturnDataVO();
            Hashtable ht = new Hashtable();
            try
            {
                int year = DateTime.Now.Year;
                int month = DateTime.Now.Month;
                string sql = string.Format(@"select top {0} x.total_money,y.channel_name from(
select channel_id,sum(money_total) total_money,0 as iscurr from report_money_channel a,report_time b
where a.report_time_id=b.id
and datediff(month, CONVERT(datetime, cast(b.[year] as varchar(10)) + '-'+ cast(b.[month] as varchar(10)) +'-'+'1'), getdate())>0
group by channel_id) x,report_alias_channel y
 where x.channel_id=y.channel_id and y.report_super_id=1 order by x.total_money desc", num);
                DataTable dt = Data.getDataTable(sql);
                string[] channel_name_array = { "外语教学与研究出版社", "长江少年儿童出版社", "武汉大学出版社", "中国地图出版社", "西南交通大学出版社" };
                List<AnonClassVO> array = new List<AnonClassVO>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i < channel_name_array.Length)
                    {
                        array.Add(new AnonClassVO()
                        {
                            Name = channel_name_array[i],
                            Value = Convert.ToDouble(dt.Rows[i]["total_money"]).ToString("F2")
                        });
                    }
                    else
                    {
                        array.Add(new AnonClassVO()
                        {
                            Name = dt.Rows[i]["channel_name"].ToString(),
                            Value = Convert.ToDouble(dt.Rows[i]["total_money"]).ToString("F2")
                        });
                    }
                }
                ht["channelList"] = array;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                model = new ReturnDataVO
                {
                    success = success,
                    message = message,
                    data = ht
                };
            }
            return JsonConvert.SerializeObject(model);

        }

        #endregion

        #region rays用户总览

        //rays读者数、活跃用户、消费用户比例
        /// <summary>
        /// 获取基础统计数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUserBaseReport(HttpContext context)
        {
            //string sql = "select count(id) user_count"; //  用户数
            //sql += " ,(select count(id) from reality_weixin_user where subscribe = 1 and  is_active=1) active_user";
            ////活跃用户数
            //sql += " from reality_weixin_user where is_active=1";
            //DataTable item = Data.getDataTable(sql);
            Hashtable ht = new Hashtable();
            int user_count = GetTotalUserCount();
            int active_user = GetActiveUserCount();
            ht["user_count"] = user_count;
            ht["active_user"] = Convert.ToInt32(active_user * BaseTableBO.getBaseByKey("active_user_proportion"));
            ht["pay_user_percent"] = Convert.ToDouble(active_user * BaseTableBO.getBaseByKey("pay_user_proportion") / user_count)
                .ToString("0.00%");

            return JsonConvert.SerializeObject(ht);
        }

        /// <summary>
        /// 获取性别统计数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetSexChart(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            //性别分布
            string sexSql = @"select count(id) totalSex,sum(case when sex=2 then 1 else 0 end) fMale,
               sum(case when sex=1 then 1 else 0 end) Male from reality_weixin_user where is_active=1";
            DataTable sexDt = Data.getDataTable(sexSql);

            List<object> sexlist = new List<object>();
            if (sexDt.Rows.Count > 0)
            {
                int totalSex = Convert.ToInt32(sexDt.Rows[0]["totalSex"]);
                sexlist.Add(new
                {
                    name = "男性",
                    value = Convert.ToInt32(sexDt.Rows[0]["Male"]),
                    percent = (Convert.ToDouble(sexDt.Rows[0]["Male"]) / totalSex).ToString("0.00%")
                });
                sexlist.Add(new
                {
                    name = "女性",
                    value = Convert.ToInt32(sexDt.Rows[0]["fMale"]),
                    percent = (Convert.ToDouble(sexDt.Rows[0]["fMale"]) / totalSex).ToString("0.00%")
                });
                sexlist.Add(new
                {
                    name = "其它",
                    value = totalSex - Convert.ToInt32(sexDt.Rows[0]["Male"]) - Convert.ToInt32(sexDt.Rows[0]["fMale"]),
                    percent =
                        (Convert.ToDouble(totalSex - Convert.ToInt32(sexDt.Rows[0]["Male"]) -
                                          Convert.ToInt32(sexDt.Rows[0]["fMale"])) / totalSex).ToString("0.00%")
                });
            }
            ht["sexList"] = sexlist;
            return JsonConvert.SerializeObject(ht);
        }

        /// <summary>
        /// 获取地图统计数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetMapChart(HttpContext context)
        {
            Hashtable ht = new Hashtable();
            //区域分布
            string citySql =
                @"select top 3 count(id) as num, province from (select id,province from reality_weixin_user where province<>'' and is_active=1) a group by province order by num desc";
            DataTable cityDt = Data.getDataTable(citySql);
            List<object> citylist = new List<object>();
            for (int j = 0; j < 3; j++)
            {
                citylist.Add(new
                {
                    name = cityDt.Rows[j]["province"],
                    value = cityDt.Rows[j]["num"]
                });
            }
            ht["cityList"] = citylist;
            return JsonConvert.SerializeObject(ht);
        }

        #endregion

        #region Rays书籍

        //获取今日成交额最高的N本书籍
        public static string GetTopSaleBooks(HttpContext context)
        {
            string num = context.Request["num"].ToString();
            ReturnDataVO returnData = new ReturnDataVO();
            string sql = string.Format(@"select top {0} xx.*,b.book_name,b.book_pic,b.isbn  from (
select book_id, sum(money_total) total_money from report_money_book where book_id > 56 group by book_id)xx
,platform_book b where xx.book_id=b.id order by total_money desc", num);
            DataTable dt = Data.getDataTable(sql);
            List<object> list = new List<object>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new
                {
                    book_name = dt.Rows[i]["book_name"],
                    book_pic = dt.Rows[i]["book_pic"],
                    isbn = dt.Rows[i]["isbn"],
                    total_money = Convert.ToDouble(dt.Rows[i]["total_money"]).ToString("F2")
                });
            }
            returnData.success = true;
            returnData.message = "ok";
            returnData.data = list;
            return JsonConvert.SerializeObject(returnData);

            //string url = string.Format("http://api.chubanyun.net/exapi/v1.0/tv/getBookOrderBySales?count={0}", num);
            //return UtilBO.MethodGET(url.ToString(), "UTF-8");
        }
        //根据ISBN返回今日此书籍的基础信息和购买信息
        public static string GetBookInfoByISBN(HttpContext context)
        {
            string isbn = context.Request["isbn"].ToString();
            ReturnDataVO returnData = new ReturnDataVO();
            PlatformBookVO platformBookVo = PlatformBookBO.getVOByIsbn(isbn);
            DataTable dt =
                Data.getDataTable(string.Format(@"select book_name,book_pic,isbn,author,publish_house,publish_data,
(select count(id)  from platformbook_seed_relation where book_id={0}) as seed_count,
(select top 1 b.imgpath from reality_ticket_book a,reality_weixin_ticket b 
where a.book_id={0} and a.reality_ticket_id=b.id) as ticket
 from platform_book where id={0}", platformBookVo.id));
            if (dt.Rows.Count > 0)
            {
                Object obj = new
                {
                    book_name = dt.Rows[0]["book_name"],
                    book_pic = dt.Rows[0]["book_pic"],
                    isbn = isbn,
                    author = dt.Rows[0]["author"],
                    publishing_house = dt.Rows[0]["publish_house"],
                    publishing_time = dt.Rows[0]["publish_data"],
                    seed_count = dt.Rows[0]["seed_count"],
                    app_count = 0,
                    ticket_count = 1,
                    ticket = dt.Rows[0]["ticket"]
                };
                returnData.success = true;
                returnData.message = "ok";
                returnData.data = obj;
            }
            else
            {
                returnData.success = false;
                returnData.message = "找不到书籍相关信息！";
            }

            return JsonConvert.SerializeObject(returnData);
        }

        //根据ISBN返回此书籍的统计信息
        public static string GetBookSts(HttpContext context)
        {
            string isbn = context.Request["isbn"].ToString();
            PlatformBookVO platformBookVo = PlatformBookBO.getVOByIsbn(isbn);
            ReturnDataVO returnData = new ReturnDataVO();
            string sql = string.Format(@"
select sum(money_total)  as total_money,
(select sum(click_count) from reality_orderform_browser where book_id={0}) as browser_count,
(select count(id) from reality_orderform_browser where book_id={0}) as user_count
from report_money_book where book_id={0}
", platformBookVo.id);
            DataTable dt = Data.getDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                var obj = new
                {
                    total_money = Convert.ToDouble(dt.Rows[0]["total_money"]).ToString("F2"),
                    user_count = dt.Rows[0]["user_count"],
                    browser_count = dt.Rows[0]["browser_count"]
                };
                returnData.success = true;
                returnData.message = "ok";
                returnData.data = obj;
            }
            else
            {
                returnData.success = false;
                returnData.message = "找不到书籍相关信息！";
            }
            return JsonConvert.SerializeObject(returnData);
        }

        //根据ISBN返回此书籍最近N条实时行为动态数据
        public static string GetBookActive(HttpContext context)
        {
            ReturnDataVO returnData = new ReturnDataVO();
            string isbn = context.Request["isbn"].ToString();
            int senceId =
                Data.GetRsCount(
                    string.Format(@"select top 1 b.oid from reality_ticket_book a,reality_weixin_ticket b,platform_book c 
where c.isbn='{0}'and a.book_id=c.id and a.reality_ticket_id=b.id", isbn));
            int num = 20;
            if (context.Request["num"] != null)
            {
                num = Convert.ToInt32(context.Request["num"]);
            }
            try
            {
                List<object> list = new List<object>();
                string url =
                    string.Format("http://api.chubanyun.net/exapi/v1.0/tv/getBehaviorBySceneId?sceneId={0}&count={1}",
                        senceId, num);
                string s = UtilBO.MethodGET(url.ToString(), "UTF-8");
                JObject obj = JsonConvert.DeserializeObject<JObject>(s);

                if (Convert.ToInt32(obj["errCode"]) == 0)
                {
                    string[][] lonlat =
                    {
                        new[]{"114.35685159914993", "30.52504496356376"},
                        new[]{ "114.9016225552", "30.3832888992"},
                        new[]{ "114.892208", "30.45361"},
                        new[]{ "115.32591", "31.380368"},
                        new[]{ "116.41364 ", "40.012167"},
                        new[]{ "113.561772", "23.272303"},
                        new[]{ "113.27201", "23.091579"},
                        new[]{ "113.219122", "22.938303" }
                    };
                    Random rnd = new Random();
                    JArray array = JArray.Parse(obj["data"].ToString());
                    for (int i = 0; i < array.Count; i++)
                    {
                        string lon = array[i]["lon"].ToString();
                        string lat = array[i]["lat"].ToString();
                        if (string.IsNullOrEmpty(lon) || string.IsNullOrEmpty(lat))
                        {
                            lon = lonlat[0][0];
                            lat = lonlat[0][1];
                        }
                        list.Add(new
                        {
                            nickname = array[i]["nickName"],
                            headImgUrl = array[i]["headImgUrl"],
                            city = array[i]["city"],
                            province = array[i]["province"],
                            country = array[i]["country"],
                            lon = Convert.ToDouble(lon),
                            lat = Convert.ToDouble(lat),
                            userId = array[i]["userId"],
                            behavior = array[i]["behavior"],
                            behaviorRelationName = array[i]["behaviorRelationName"],
                            behaviorTime = array[i]["behaviorTime"],
                            spaceName = "长江少年儿童出版社"
                        });
                    }

                }
                returnData.success = true;
                returnData.message = "ok";
                returnData.data = list;
            }
            catch (Exception ex)
            {
                returnData.success = false;
                returnData.message = ex.Message;
            }
            return JsonConvert.SerializeObject(returnData);
        }

        //根据ISBN返回此书籍读者分布
        public static string GetBookUser(HttpContext context)
        {
            string isbn = context.Request["isbn"].ToString();
            ReturnDataVO returnData = new ReturnDataVO();
            PlatformBookVO platformBookVo = PlatformBookBO.getVOByIsbn(isbn);
            DataTable dt = Data.getDataTable(string.Format(@" select top 15 count(id) as num, province from (
 select a.id,b.province from reality_orderform_browser a,reality_weixin_user b
 where a.uid=b.uid and a.book_id={0} and b.province<>'') x group by province order by num desc
", platformBookVo.id));
            List<object> list = new List<object>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new
                {
                    province = dt.Rows[i]["province"],
                    count = dt.Rows[i]["num"]

                });
            }
            returnData.success = true;
            returnData.message = "ok";
            returnData.data = list;
            return JsonConvert.SerializeObject(returnData);
        }
        #endregion

        #region 官网首页接口
        /// <summary>
        /// 获取书籍、资源、用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetSeedUserBookData(HttpContext context)
        {
            ReturnDataVO returnData = new ReturnDataVO();
            try
            {
                //实时上架资源
                string sql = "select count(id)+(select top 1 base_value from  base_table where base_key='reality_seed_count') seed_count"; //用户总量
                sql += " from seed  ";
                DataTable item = Data.getDataTable(sql);
                Hashtable ht = new Hashtable();
                if (item.Rows.Count > 0)
                {
                    int user_count = GetTotalUserCount();
                    ht["user_count"] = user_count.ToString("N0");
                    ht["book_count"] = GetBookCount().ToString("N0");
                    ht["seed_count"] = Convert.ToInt32(item.Rows[0]["seed_count"]).ToString("N0");
                }
                returnData.success = true;
                returnData.data = ht;
            }
            catch (Exception ex)
            {
                returnData.success = false;
                returnData.message = ex.Message;
            }
            return JsonConvert.SerializeObject(returnData);
        }
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetOrderAndSales(HttpContext context)
        {
            ReturnDataVO returnData = new ReturnDataVO();
            try
            {
                //int year = DateTime.Now.Year;
                //int month = DateTime.Now.Month;
                //     string sql = string.Format(@"select isnull(sum(orderform_money),0) orderform_money,
                // isnull(sum(orderform_count),0) orderform_count from 
                //  (select sum(money+freight) orderform_money,count(id) orderform_count,
                //   '0' as is_real_time  from reality_orderform_{0}_{1}
                //   union all
                // select sum(a.total_money) orderform_money,sum(orderform_count) orderform_count,
                //'1' as is_real_time  from report_month_money a ,report_time b 
                // where a.report_time_id=b.id and  datediff(month, CONVERT(datetime,cast(b.[year] as varchar(10)) + '-'+ cast(b.[month] as varchar(10)) +'-'+'1'), getdate())>0) x
                // ", year, month);
                //     DataTable item = Data.getDataTable(sql);
                Hashtable ht = new Hashtable();
                ht["total_money"] = GetPlatformTotalMoney().ToString("N");
                ht["orderform_count"] = GetTotalOrderCount().ToString("N0");

                returnData.success = true;
                returnData.data = ht;
            }
            catch (Exception ex)
            {
                returnData.success = false;
                returnData.message = ex.Message;
            }
            return JsonConvert.SerializeObject(returnData);
        }
        #endregion

        #region 获取平台总额

        #region 获取交易总额
        public static double GetPlatformTotalMoney()
        {
            double base_money = BaseTableBO.getBaseByKey("platform_total_money");
            Hashtable real_ht = Common.getRealData();
            string total_money = (Convert.ToDouble(base_money) + Convert.ToDouble(real_ht["real_total_money"])).ToString("F2");
            return Convert.ToDouble(total_money);
        }
        #endregion
        #region 获取订单总数
        public static int GetTotalOrderCount()
        {
            double base_count = BaseTableBO.getBaseByKey("platform_total_order");
            Hashtable real_ht = Common.getRealData();
            int total_count = Convert.ToInt32(base_count) + Convert.ToInt32(real_ht["real_orderform_count"]);
            return total_count;
        }
        #endregion
        #region 获取用户总数
        public static int GetTotalUserCount()
        {
            int base_count = Convert.ToInt32(BaseTableBO.getBaseByKey("platform_total_usercount"));
            Hashtable real_ht = Common.getRealData();
            int count = Convert.ToInt32(real_ht["real_user_count"]);
            int total_count = base_count + count;
            return total_count;
        }
        #endregion
        #region 获取活跃用户总数
        public static int GetActiveUserCount()
        {
            int base_count = Convert.ToInt32(BaseTableBO.getBaseByKey("platform_active_usercount"));
            Hashtable real_ht = Common.getRealData();
            int count = Convert.ToInt32(real_ht["real_user_count"]);
            int total_count = base_count + count;
            return total_count;
        }
        #endregion
        #region 获取书籍总额
        public static int GetBookCount()
        {
            int base_count = Convert.ToInt32(BaseTableBO.getBaseByKey("platformbook_count"));
            string sql = "select sum(circulation) from platform_book where is_tv_book=1";
            int count = Data.GetRsCount(sql);
            int total_count = base_count + count;
            return total_count;
        }
        #endregion
        #endregion

    }
}