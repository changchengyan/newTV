/// <summary>
/// web_error��WebErrorVO ֵ����,����ʱ��:2015/12/8 20:15:20(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ֵ����
/// </summary>

namespace Redsz.VO
{

	public class WebErrorVO
	{
        private int _id; //      int,length:4,
        private string _error_code; //      nvarchar,length:100,
        private string _error_desc; //      nvarchar,length:400,
        private string _error_path; //      nvarchar,length:400,
        private string _createtime; //      datetime,length:8,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string error_code
        {
          get{return _error_code;}
          set{_error_code = value;}
        }

        /// <summary>
        ///      nvarchar,length:400
        /// </summary>
        public string error_desc
        {
          get{return _error_desc;}
          set{_error_desc = value;}
        }

        /// <summary>
        ///      nvarchar,length:400
        /// </summary>
        public string error_path
        {
          get{return _error_path;}
          set{_error_path = value;}
        }

        /// <summary>
        ///      datetime,length:8
        /// </summary>
        public string createtime
        {
          get{return _createtime;}
          set{_createtime = value;}
        }
	}

}