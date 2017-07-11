/// <summary>
/// util_dept表SysDeptVO 值对象,创建时间:2008-7-14 22:44:03(此文件由框架自动生成 v1.0)
/// 深圳红网 www.redsz.com QQ3968666
/// </summary>

namespace Redsz.VO
{

	public class SysDeptVO
	{
        private int _id; //      int,length:10,default:
        private string _name; //部门名称      nvarchar,length:50,default:
        private int _topsize; //      int,length:10,default:(0)


        /// <summary>
        ///      int,length:10,default:
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///部门名称      nvarchar,length:50,default:
        /// </summary>
        public string name
        {
          get{return _name;}
          set{_name = value;}
        }

        /// <summary>
        ///      int,length:10,default:(0)
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }
	}

}