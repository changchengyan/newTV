/// <summary>
/// util_dept��SysDeptVO ֵ����,����ʱ��:2008-7-14 22:44:03(���ļ��ɿ���Զ����� v1.0)
/// ���ں��� www.redsz.com QQ3968666
/// </summary>

namespace Redsz.VO
{

	public class SysDeptVO
	{
        private int _id; //      int,length:10,default:
        private string _name; //��������      nvarchar,length:50,default:
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
        ///��������      nvarchar,length:50,default:
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