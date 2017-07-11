/// <summary>
/// sys_role表SysRoleVO 值对象,创建时间:2016/11/22 16:00:38(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：系统角色值对象
/// </summary>

namespace Redsz.VO
{

	public class SysRoleVO
	{
        private int _id; //      int,length:4,
        private string _rolename; //角色名称      nvarchar,length:100,
        private string _roledesc; //      nvarchar,length:400,
        private int _topsize; //排序值      int,length:4,
        private int _issuper; //是否具备系统设置的管理权限，1：是，0：否，默认：0      int,length:4,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///角色名称      nvarchar,length:100
        /// </summary>
        public string rolename
        {
          get{return _rolename;}
          set{_rolename = value;}
        }

        /// <summary>
        ///      nvarchar,length:400
        /// </summary>
        public string roledesc
        {
          get{return _roledesc;}
          set{_roledesc = value;}
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
        ///是否具备系统设置的管理权限，1：是，0：否，默认：0      int,length:4
        /// </summary>
        public int issuper
        {
          get{return _issuper;}
          set{_issuper = value;}
        }
	}

}