/// <summary>
/// dictionary_industry��DictionaryIndustryVO ֵ����,����ʱ��:2016/11/14 16:52:48(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ֵ����
/// </summary>

namespace Redsz.VO
{

	public class DictionaryIndustryVO
	{
        private int _id; //      int,length:4,
        private int _base_id; //      int,length:4,
        private int _industry_id; //      int,length:4,
        private string _industry_word; //      nvarchar,length:1024,


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
        public int base_id
        {
          get{return _base_id;}
          set{_base_id = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int industry_id
        {
          get{return _industry_id;}
          set{_industry_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:1024
        /// </summary>
        public string industry_word
        {
          get{return _industry_word;}
          set{_industry_word = value;}
        }
	}

}