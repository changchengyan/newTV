/// <summary>
/// sys_menu��SysMenuVO ֵ����,����ʱ��:2016/11/25 21:42:14(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ƽ̨ϵͳ�˵�ֵ����
/// </summary>

namespace Redsz.VO
{

	public class SysMenuVO
	{
        private int _id; //      int,length:4,
        private int _parentid; //����ʶ      int,length:4,
        private string _parentname; //���˵�����      nvarchar,length:100,
        private string _menuname; //�˵�����      nvarchar,length:100,
        private string _menucode; //�˵�����      nvarchar,length:100,
        private string _url; //���ӵ�ַ      nvarchar,length:510,
        private string _url_parameter; //      nvarchar,length:200,
        private string _target; //Ŀ�괰��      nvarchar,length:100,
        private string _menulevel; //�˵�����      nvarchar,length:100,
        private int _topsize; //����ֵ      int,length:4,
        private int _ismenu; //�Ƿ�Ϊ�˵�      int,length:4,
        private int _iswork; //�Ƿ�Ϊ��ݲ˵�      int,length:4,
        private int _child_count; //�Ӳ˵�����      int,length:4,
        private string _menudesc; //      nvarchar,length:100,
        private string _font_awesome; //      nvarchar,length:100,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///����ʶ      int,length:4
        /// </summary>
        public int parentid
        {
          get{return _parentid;}
          set{_parentid = value;}
        }

        /// <summary>
        ///���˵�����      nvarchar,length:100
        /// </summary>
        public string parentname
        {
          get{return _parentname;}
          set{_parentname = value;}
        }

        /// <summary>
        ///�˵�����      nvarchar,length:100
        /// </summary>
        public string menuname
        {
          get{return _menuname;}
          set{_menuname = value;}
        }

        /// <summary>
        ///�˵�����      nvarchar,length:100
        /// </summary>
        public string menucode
        {
          get{return _menucode;}
          set{_menucode = value;}
        }

        /// <summary>
        ///���ӵ�ַ      nvarchar,length:510
        /// </summary>
        public string url
        {
          get{return _url;}
          set{_url = value;}
        }

        /// <summary>
        ///      nvarchar,length:200
        /// </summary>
        public string url_parameter
        {
          get{return _url_parameter;}
          set{_url_parameter = value;}
        }

        /// <summary>
        ///Ŀ�괰��      nvarchar,length:100
        /// </summary>
        public string target
        {
          get{return _target;}
          set{_target = value;}
        }

        /// <summary>
        ///�˵�����      nvarchar,length:100
        /// </summary>
        public string menulevel
        {
          get{return _menulevel;}
          set{_menulevel = value;}
        }

        /// <summary>
        ///����ֵ      int,length:4
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }

        /// <summary>
        ///�Ƿ�Ϊ�˵�      int,length:4
        /// </summary>
        public int ismenu
        {
          get{return _ismenu;}
          set{_ismenu = value;}
        }

        /// <summary>
        ///�Ƿ�Ϊ��ݲ˵�      int,length:4
        /// </summary>
        public int iswork
        {
          get{return _iswork;}
          set{_iswork = value;}
        }

        /// <summary>
        ///�Ӳ˵�����      int,length:4
        /// </summary>
        public int child_count
        {
          get{return _child_count;}
          set{_child_count = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string menudesc
        {
          get{return _menudesc;}
          set{_menudesc = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string font_awesome
        {
          get{return _font_awesome;}
          set{_font_awesome = value;}
        }
	}

}