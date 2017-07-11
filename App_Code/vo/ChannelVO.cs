/// <summary>
/// channel表ChannelVO 值对象,创建时间:2016/11/16 17:09:03(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：渠道值对象
/// </summary>

namespace Redsz.VO
{

	public class ChannelVO
	{
        private int _id; //      int,length:4,
        private int _space_id; //      int,length:4,
        private string _loginname; //      nvarchar,length:100,
        private string _password; //      nvarchar,length:100,
        private string _channel_name; //      nvarchar,length:100,
        private string _channel_desc; //      nvarchar,length:1000,
        private string _channel_logo; //      nvarchar,length:510,
        private string _channel_pic; //      nvarchar,length:510,
        private string _channel_address; //      nvarchar,length:1000,
        private string _channel_tel; //      nvarchar,length:100,
        private double _lat; //      float,length:8,
        private double _lng; //      float,length:8,
        private int _china_province_id; //      int,length:4,
        private int _china_city_id; //      int,length:4,
        private int _china_area_id; //      int,length:4,
        private double _adviser_money_proportion; //顾问提成比例 0-1，提成 = ( 销售价 - 协议价 ) * 提成比例      float,length:8,
        private double _channel_money_proportion; //渠道提成比例 0-1，提成 = ( 销售价 - 协议价 ) * 提成比例      float,length:8,
        private double _space_money_proportion; //代理提成比例 0-1，提成 = ( 销售价 - 协议价 ) * 提成比例      float,length:8,
        private int _examine; //审核状态，1：已审核，0：未审核，-1：审核未通过      int,length:4,
        private int _binding_uid; //绑定的uid，通过uid可找到微信用户      int,length:4,
        private int _seed_member_mode; //      int,length:4,
        private string _seed_member_pic; //      nvarchar,length:510,
        private string _seed_member_button_txt; //      nvarchar,length:100,
        private string _createtime; //      datetime,length:8,
        private string _updatetime; //      datetime,length:8,
        private int _isdelete; //      int,length:4,
        private string _random; //      nvarchar,length:100,


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
        public string channel_name
        {
          get{return _channel_name;}
          set{_channel_name = value;}
        }

        /// <summary>
        ///      nvarchar,length:1000
        /// </summary>
        public string channel_desc
        {
          get{return _channel_desc;}
          set{_channel_desc = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string channel_logo
        {
          get{return _channel_logo;}
          set{_channel_logo = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string channel_pic
        {
          get{return _channel_pic;}
          set{_channel_pic = value;}
        }

        /// <summary>
        ///      nvarchar,length:1000
        /// </summary>
        public string channel_address
        {
          get{return _channel_address;}
          set{_channel_address = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string channel_tel
        {
          get{return _channel_tel;}
          set{_channel_tel = value;}
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
        ///顾问提成比例 0-1，提成 = ( 销售价 - 协议价 ) * 提成比例      float,length:8
        /// </summary>
        public double adviser_money_proportion
        {
          get{return _adviser_money_proportion;}
          set{_adviser_money_proportion = value;}
        }

        /// <summary>
        ///渠道提成比例 0-1，提成 = ( 销售价 - 协议价 ) * 提成比例      float,length:8
        /// </summary>
        public double channel_money_proportion
        {
          get{return _channel_money_proportion;}
          set{_channel_money_proportion = value;}
        }

        /// <summary>
        ///代理提成比例 0-1，提成 = ( 销售价 - 协议价 ) * 提成比例      float,length:8
        /// </summary>
        public double space_money_proportion
        {
          get{return _space_money_proportion;}
          set{_space_money_proportion = value;}
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
        ///      int,length:4
        /// </summary>
        public int seed_member_mode
        {
          get{return _seed_member_mode;}
          set{_seed_member_mode = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string seed_member_pic
        {
          get{return _seed_member_pic;}
          set{_seed_member_pic = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string seed_member_button_txt
        {
          get{return _seed_member_button_txt;}
          set{_seed_member_button_txt = value;}
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
        ///      int,length:4
        /// </summary>
        public int isdelete
        {
          get{return _isdelete;}
          set{_isdelete = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string random
        {
          get{return _random;}
          set{_random = value;}
        }
	}

}