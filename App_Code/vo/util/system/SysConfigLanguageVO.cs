/// <summary>
/// sys_config_language��SysConfigLanguageVO ֵ����,����ʱ��:2014/5/26 16:07:02(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///���������԰汾ֵ����
/// </summary>

namespace Redsz.VO
{

	public class SysConfigLanguageVO
	{
        private int _id; //      int,length:4,
        private int _topsize; //      int,length:4,
        private string _language; //��������˵��      nvarchar,length:100,
        private string _code; //ʶ�����      nvarchar,length:100,
        private string _web_index_page; //PCweb�Ĵ���Ŀ¼      nvarchar,length:100,
        private string _mobile_index_page; //�ֻ�web�Ĵ���Ŀ¼      nvarchar,length:100,
        private string _web_static_folder; //PCweb�ľ�̬ҳĿ¼      nvarchar,length:100,
        private string _mobile_static_folder; //�ֻ�web�ľ�̬ҳĿ¼      nvarchar,length:100,


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
        ///��������˵��      nvarchar,length:100
        /// </summary>
        public string language
        {
          get{return _language;}
          set{_language = value;}
        }

        /// <summary>
        ///ʶ�����      nvarchar,length:100
        /// </summary>
        public string code
        {
          get{return _code;}
          set{_code = value;}
        }

        /// <summary>
        ///PCweb�Ĵ���Ŀ¼      nvarchar,length:100
        /// </summary>
        public string web_index_page
        {
          get{return _web_index_page;}
          set{_web_index_page = value;}
        }

        /// <summary>
        ///�ֻ�web�Ĵ���Ŀ¼      nvarchar,length:100
        /// </summary>
        public string mobile_index_page
        {
          get{return _mobile_index_page;}
          set{_mobile_index_page = value;}
        }

        /// <summary>
        ///PCweb�ľ�̬ҳĿ¼      nvarchar,length:100
        /// </summary>
        public string web_static_folder
        {
          get{return _web_static_folder;}
          set{_web_static_folder = value;}
        }

        /// <summary>
        ///�ֻ�web�ľ�̬ҳĿ¼      nvarchar,length:100
        /// </summary>
        public string mobile_static_folder
        {
          get{return _mobile_static_folder;}
          set{_mobile_static_folder = value;}
        }
	}

}