/// <summary>
/// dictionary_base��DictionaryBaseVO ֵ����,����ʱ��:2016/11/14 16:52:13(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ֵ����
/// </summary>

namespace Redsz.VO
{

	public class CopyDataResouceVO
    {
        private int _count; //      int,length:4,
        private int _step; //      nvarchar,length:1024,
        private string _message; //      nvarchar,length:512,
        private bool _success; //      nvarchar,length:100,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int count
        {
          get{return _count; }
          set{ _count = value;}
        }

        /// <summary>
        ///      nvarchar,length:1024
        /// </summary>
        public int step
        {
          get{return _step; }
          set{ _step = value;}
        }

        /// <summary>
        ///      nvarchar,length:512
        /// </summary>
        public string message
        {
          get{return _message; }
          set{ _message = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public bool success
        {
          get{return _success; }
          set{ _success = value;}
        }
	}

}