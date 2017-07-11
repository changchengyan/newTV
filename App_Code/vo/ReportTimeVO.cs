/// <summary>
/// report_time��ReportTimeVO ֵ����,����ʱ��:2017/5/24 11:11:30(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ֵ����
/// </summary>

namespace Redsz.VO
{

	public class ReportTimeVO
	{
        private int _id; //      int,length:4,
        private int _report_super_id; //      int,length:4,
        private string _time_name; //      nvarchar,length:100,
        private int _year; //      int,length:4,
        private int _month; //      int,length:4,
        private double _total_money; //      float,length:8,
        private double _wechat_total_money; //      float,length:8,
        private double _company_total_moeny; //      float,length:8,
        private string _createtime; //      datetime,length:8,
        private string _updatetime; //      datetime,length:8,
        private string _random; //      nvarchar,length:100,
        private string _reality_run_number; //ִ�ж���������ţ��޸�ֵ֮�������в�����ʱ�����������ٲ�⣩      nvarchar,length:100,
        private int _reality_last_orderform_id; //      int,length:4,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int report_super_id
        {
          get{return _report_super_id;}
          set{_report_super_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string time_name
        {
          get{return _time_name;}
          set{_time_name = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int year
        {
          get{return _year;}
          set{_year = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int month
        {
          get{return _month;}
          set{_month = value;}
        }

        /// <summary>
        ///      float,length:8
        /// </summary>
        public double total_money
        {
          get{return _total_money;}
          set{_total_money = value;}
        }

        /// <summary>
        ///      float,length:8
        /// </summary>
        public double wechat_total_money
        {
          get{return _wechat_total_money;}
          set{_wechat_total_money = value;}
        }

        /// <summary>
        ///      float,length:8
        /// </summary>
        public double company_total_moeny
        {
          get{return _company_total_moeny;}
          set{_company_total_moeny = value;}
        }

        /// <summary>
        ///      datetime,length:8
        /// </summary>
        public string createtime
        {
          get{return _createtime;}
          set{_createtime = value;}
        }

        /// <summary>
        ///      datetime,length:8
        /// </summary>
        public string updatetime
        {
          get{return _updatetime;}
          set{_updatetime = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string random
        {
          get{return _random;}
          set{_random = value;}
        }

        /// <summary>
        ///ִ�ж���������ţ��޸�ֵ֮�������в�����ʱ�����������ٲ�⣩      nvarchar,length:100
        /// </summary>
        public string reality_run_number
        {
          get{return _reality_run_number;}
          set{_reality_run_number = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int reality_last_orderform_id
        {
          get{return _reality_last_orderform_id;}
          set{_reality_last_orderform_id = value;}
        }
	}

}