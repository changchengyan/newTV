/// <summary>
/// sys_menu表SysMenuVO 值对象,创建时间:2016/11/25 21:42:14(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：平台系统菜单值对象
/// </summary>

namespace Redsz.VO
{

	public class SysMenuVO
	{
        private int _id; //      int,length:4,
        private int _parentid; //父标识      int,length:4,
        private string _parentname; //父菜单名称      nvarchar,length:100,
        private string _menuname; //菜单名称      nvarchar,length:100,
        private string _menucode; //菜单编码      nvarchar,length:100,
        private string _url; //链接地址      nvarchar,length:510,
        private string _url_parameter; //      nvarchar,length:200,
        private string _target; //目标窗口      nvarchar,length:100,
        private string _menulevel; //菜单级别      nvarchar,length:100,
        private int _topsize; //排序值      int,length:4,
        private int _ismenu; //是否为菜单      int,length:4,
        private int _iswork; //是否为快捷菜单      int,length:4,
        private int _child_count; //子菜单数量      int,length:4,
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
        ///父标识      int,length:4
        /// </summary>
        public int parentid
        {
          get{return _parentid;}
          set{_parentid = value;}
        }

        /// <summary>
        ///父菜单名称      nvarchar,length:100
        /// </summary>
        public string parentname
        {
          get{return _parentname;}
          set{_parentname = value;}
        }

        /// <summary>
        ///菜单名称      nvarchar,length:100
        /// </summary>
        public string menuname
        {
          get{return _menuname;}
          set{_menuname = value;}
        }

        /// <summary>
        ///菜单编码      nvarchar,length:100
        /// </summary>
        public string menucode
        {
          get{return _menucode;}
          set{_menucode = value;}
        }

        /// <summary>
        ///链接地址      nvarchar,length:510
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
        ///目标窗口      nvarchar,length:100
        /// </summary>
        public string target
        {
          get{return _target;}
          set{_target = value;}
        }

        /// <summary>
        ///菜单级别      nvarchar,length:100
        /// </summary>
        public string menulevel
        {
          get{return _menulevel;}
          set{_menulevel = value;}
        }

        /// <summary>
        ///排序值      int,length:4
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }

        /// <summary>
        ///是否为菜单      int,length:4
        /// </summary>
        public int ismenu
        {
          get{return _ismenu;}
          set{_ismenu = value;}
        }

        /// <summary>
        ///是否为快捷菜单      int,length:4
        /// </summary>
        public int iswork
        {
          get{return _iswork;}
          set{_iswork = value;}
        }

        /// <summary>
        ///子菜单数量      int,length:4
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