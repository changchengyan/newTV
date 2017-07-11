/// <summary>
/// merchant表VO 值对象,创建时间:2016/11/16 17:09:15(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：值对象
/// </summary>

namespace Redsz.VO
{

	public class VO
	{
        private int _id; //      int,length:4,
        private int _space_id; //      int,length:4,
        private string _loginname; //      nvarchar,length:100,
        private string _password; //      nvarchar,length:100,
        private string _merchant_name; //      nvarchar,length:100,
        private string _merchant_desc; //      nvarchar,length:2000,
        private string _merchant_logo; //      nvarchar,length:510,
        private string _merchant_pic; //      nvarchar,length:510,
        private string _merchant_business_license; //营业执照图片      nvarchar,length:510,
        private string _merchant_address; //      nvarchar,length:1000,
        private string _merchant_tel; //      nvarchar,length:100,
        private string _services_begintime; //营业时间      time,length:5,
        private string _services_endtime; //营业时间      time,length:5,
        private double _freight_money; //配送费      float,length:8,
        private int _freight_minute_time; //送达时长（分钟）      int,length:4,
        private double _lowest_money; //最低消费、起送金额      float,length:8,
        private double _freight_free_money; //满 多少免运费      float,length:8,
        private double _lat; //      float,length:8,
        private double _lng; //      float,length:8,
        private int _china_province_id; //      int,length:4,
        private int _china_city_id; //      int,length:4,
        private int _china_area_id; //      int,length:4,
        private double _eval; //根据各种规则计算出的商户服务评分，0-5      float,length:8,
        private int _examine; //审核状态，1：已审核，0：未审核，-1：审核未通过      int,length:4,
        private int _binding_uid; //绑定的uid，通过uid可找到微信用户      int,length:4,
        private string _createtime; //      datetime,length:8,
        private string _updatetime; //      datetime,length:8,


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
        public int space_id
        {
          get{return _space_id;}
          set{_space_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string loginname
        {
          get{return _loginname;}
          set{_loginname = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string password
        {
          get{return _password;}
          set{_password = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string merchant_name
        {
          get{return _merchant_name;}
          set{_merchant_name = value;}
        }

        /// <summary>
        ///      nvarchar,length:2000
        /// </summary>
        public string merchant_desc
        {
          get{return _merchant_desc;}
          set{_merchant_desc = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string merchant_logo
        {
          get{return _merchant_logo;}
          set{_merchant_logo = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string merchant_pic
        {
          get{return _merchant_pic;}
          set{_merchant_pic = value;}
        }

        /// <summary>
        ///营业执照图片      nvarchar,length:510
        /// </summary>
        public string merchant_business_license
        {
          get{return _merchant_business_license;}
          set{_merchant_business_license = value;}
        }

        /// <summary>
        ///      nvarchar,length:1000
        /// </summary>
        public string merchant_address
        {
          get{return _merchant_address;}
          set{_merchant_address = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string merchant_tel
        {
          get{return _merchant_tel;}
          set{_merchant_tel = value;}
        }

        /// <summary>
        ///营业时间      time,length:5
        /// </summary>
        public string services_begintime
        {
          get{return _services_begintime;}
          set{_services_begintime = value;}
        }

        /// <summary>
        ///营业时间      time,length:5
        /// </summary>
        public string services_endtime
        {
          get{return _services_endtime;}
          set{_services_endtime = value;}
        }

        /// <summary>
        ///配送费      float,length:8
        /// </summary>
        public double freight_money
        {
          get{return _freight_money;}
          set{_freight_money = value;}
        }

        /// <summary>
        ///送达时长（分钟）      int,length:4
        /// </summary>
        public int freight_minute_time
        {
          get{return _freight_minute_time;}
          set{_freight_minute_time = value;}
        }

        /// <summary>
        ///最低消费、起送金额      float,length:8
        /// </summary>
        public double lowest_money
        {
          get{return _lowest_money;}
          set{_lowest_money = value;}
        }

        /// <summary>
        ///满 多少免运费      float,length:8
        /// </summary>
        public double freight_free_money
        {
          get{return _freight_free_money;}
          set{_freight_free_money = value;}
        }

        /// <summary>
        ///      float,length:8
        /// </summary>
        public double lat
        {
          get{return _lat;}
          set{_lat = value;}
        }

        /// <summary>
        ///      float,length:8
        /// </summary>
        public double lng
        {
          get{return _lng;}
          set{_lng = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int china_province_id
        {
          get{return _china_province_id;}
          set{_china_province_id = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int china_city_id
        {
          get{return _china_city_id;}
          set{_china_city_id = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int china_area_id
        {
          get{return _china_area_id;}
          set{_china_area_id = value;}
        }

        /// <summary>
        ///根据各种规则计算出的商户服务评分，0-5      float,length:8
        /// </summary>
        public double eval
        {
          get{return _eval;}
          set{_eval = value;}
        }

        /// <summary>
        ///审核状态，1：已审核，0：未审核，-1：审核未通过      int,length:4
        /// </summary>
        public int examine
        {
          get{return _examine;}
          set{_examine = value;}
        }

        /// <summary>
        ///绑定的uid，通过uid可找到微信用户      int,length:4
        /// </summary>
        public int binding_uid
        {
          get{return _binding_uid;}
          set{_binding_uid = value;}
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
	}

}