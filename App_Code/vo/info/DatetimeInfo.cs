using System.Collections.Generic;
/// <summary>
/// 浏览统计综合信息
/// </summary>

namespace Redsz.VO
{

	public class DatetimeInfo
    {
      

        private int _today_count; //      int,length:4,
        private int _yesterday_count; //      int,length:4,
        private int _month_count; //      int,length:4,

        private double _today_total; //      int,length:4,
        private double _yesterday_total; //      int,length:4,
        private double _month_total; //      int,length:4,



        /// <summary>
        /// 今日
        /// </summary>
        public int today_count
        {
            get { return _today_count; }
            set { _today_count = value; }
        }

        /// <summary>
        ///昨日
        /// </summary>
        public int yesterday_count
        {
            get { return _yesterday_count; }
            set { _yesterday_count = value; }
        }

        /// <summary>
        /// 30日
        /// </summary>
        public int month_count
        {
            get { return _month_count; }
            set { _month_count = value; }
        }


        /// <summary>
        /// 今日
        /// </summary>
        public double today_total
        {
            get { return _today_total; }
            set { _today_total = value; }
        }

        /// <summary>
        ///昨日
        /// </summary>
        public double yesterday_total
        {
            get { return _yesterday_total; }
            set { _yesterday_total = value; }
        }

        /// <summary>
        /// 30日
        /// </summary>
        public double month_total
        {
            get { return _month_total; }
            set { _month_total = value; }
        }




    }

}