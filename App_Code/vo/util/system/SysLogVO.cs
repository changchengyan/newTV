/// <summary>
/// syslog表SysLogVO 值对象,创建时间:2013/11/1 22:17:50(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：系统日志值对象
/// </summary>

namespace Redsz.VO
{

    public class SysLogVO
    {
        private int _id; //      int,length:10,default:
        private string _ip; //      nvarchar,length:50,default:
        private string _code; //      nvarchar,length:50,default:
        private string _title; //      nvarchar,length:200,default:
        private string _info; //      nvarchar,length:1000,default:
        private string _username; //      nvarchar,length:50,default:
        private string _datetime; //      datetime,length:23,default:
        private string _bo; //      nvarchar,length:50,default:
        private string _state; //操作状态：成功、失败       nvarchar,length:50,default:


        /// <summary>
        ///      int,length:10,default:
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        ///      nvarchar,length:200,default:
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        ///      nvarchar,length:1000,default:
        /// </summary>
        public string info
        {
            get { return _info; }
            set { _info = value; }
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        ///      datetime,length:23,default:
        /// </summary>
        public string datetime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string bo
        {
            get { return _bo; }
            set { _bo = value; }
        }

        /// <summary>
        ///操作状态：成功、失败       nvarchar,length:50,default:
        /// </summary>
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
    }

}