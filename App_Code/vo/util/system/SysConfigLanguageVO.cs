/// <summary>
/// sys_config_language表SysConfigLanguageVO 值对象,创建时间:2014/5/26 16:07:02(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：语言版本值对象
/// </summary>

namespace Redsz.VO
{

	public class SysConfigLanguageVO
	{
        private int _id; //      int,length:4,
        private int _topsize; //      int,length:4,
        private string _language; //语言名称说明      nvarchar,length:100,
        private string _code; //识别编码      nvarchar,length:100,
        private string _web_index_page; //PCweb的代码目录      nvarchar,length:100,
        private string _mobile_index_page; //手机web的代码目录      nvarchar,length:100,
        private string _web_static_folder; //PCweb的静态页目录      nvarchar,length:100,
        private string _mobile_static_folder; //手机web的静态页目录      nvarchar,length:100,


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
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }

        /// <summary>
        ///语言名称说明      nvarchar,length:100
        /// </summary>
        public string language
        {
          get{return _language;}
          set{_language = value;}
        }

        /// <summary>
        ///识别编码      nvarchar,length:100
        /// </summary>
        public string code
        {
          get{return _code;}
          set{_code = value;}
        }

        /// <summary>
        ///PCweb的代码目录      nvarchar,length:100
        /// </summary>
        public string web_index_page
        {
          get{return _web_index_page;}
          set{_web_index_page = value;}
        }

        /// <summary>
        ///手机web的代码目录      nvarchar,length:100
        /// </summary>
        public string mobile_index_page
        {
          get{return _mobile_index_page;}
          set{_mobile_index_page = value;}
        }

        /// <summary>
        ///PCweb的静态页目录      nvarchar,length:100
        /// </summary>
        public string web_static_folder
        {
          get{return _web_static_folder;}
          set{_web_static_folder = value;}
        }

        /// <summary>
        ///手机web的静态页目录      nvarchar,length:100
        /// </summary>
        public string mobile_static_folder
        {
          get{return _mobile_static_folder;}
          set{_mobile_static_folder = value;}
        }
	}

}