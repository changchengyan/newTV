/// <summary>
/// sys_role��SysRoleVO ֵ����,����ʱ��:2016/11/22 16:00:38(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ϵͳ��ɫֵ����
/// </summary>

namespace Redsz.VO
{

	public class SysRoleVO
	{
        private int _id; //      int,length:4,
        private string _rolename; //��ɫ����      nvarchar,length:100,
        private string _roledesc; //      nvarchar,length:400,
        private int _topsize; //����ֵ      int,length:4,
        private int _issuper; //�Ƿ�߱�ϵͳ���õĹ���Ȩ�ޣ�1���ǣ�0����Ĭ�ϣ�0      int,length:4,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///��ɫ����      nvarchar,length:100
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
        ///����ֵ      int,length:4
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }

        /// <summary>
        ///�Ƿ�߱�ϵͳ���õĹ���Ȩ�ޣ�1���ǣ�0����Ĭ�ϣ�0      int,length:4
        /// </summary>
        public int issuper
        {
          get{return _issuper;}
          set{_issuper = value;}
        }
	}

}