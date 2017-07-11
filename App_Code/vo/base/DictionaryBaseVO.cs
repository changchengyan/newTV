/// <summary>
/// dictionary_base表DictionaryBaseVO 值对象,创建时间:2016/11/14 16:52:13(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：值对象
/// </summary>

namespace Redsz.VO
{

	public class DictionaryBaseVO
	{
        private int _id; //      int,length:4,
        private string _base_word; //      nvarchar,length:1024,
        private string _system; //      nvarchar,length:512,
        private string _code; //      nvarchar,length:100,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:1024
        /// </summary>
        public string base_word
        {
          get{return _base_word;}
          set{_base_word = value;}
        }

        /// <summary>
        ///      nvarchar,length:512
        /// </summary>
        public string system
        {
          get{return _system;}
          set{_system = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string code
        {
          get{return _code;}
          set{_code = value;}
        }
	}

}