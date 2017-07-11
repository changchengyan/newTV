/// <summary>
/// organise表SysOrganiseVO 值对象,创建时间:2013/11/5 19:31:15(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：组织结构值对象
/// </summary>

namespace Redsz.VO
{

	public class SysOrganiseVO
	{
        private int _id; //      int,length:10,default:
        private int _topsize; //      int,length:10,default:(0)
        private int _parentid; //      int,length:10,default:(0)
        private string _parentcode; //      nvarchar,length:50,default:
        private string _parentname; //      nvarchar,length:50,default:
        private string _org_code; //      nvarchar,length:50,default:
        private string _org_name; //      nvarchar,length:50,default:
        private string _org_desc; //      nvarchar,length:200,default:
        private int _isdelete; //      int,length:10,default:(0)


        /// <summary>
        ///      int,length:10,default:
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///      int,length:10,default:(0)
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }

        /// <summary>
        ///      int,length:10,default:(0)
        /// </summary>
        public int parentid
        {
          get{return _parentid;}
          set{_parentid = value;}
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string parentcode
        {
          get{return _parentcode;}
          set{_parentcode = value;}
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string parentname
        {
          get{return _parentname;}
          set{_parentname = value;}
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string org_code
        {
          get{return _org_code;}
          set{_org_code = value;}
        }

        /// <summary>
        ///      nvarchar,length:50,default:
        /// </summary>
        public string org_name
        {
          get{return _org_name;}
          set{_org_name = value;}
        }

        /// <summary>
        ///      nvarchar,length:200,default:
        /// </summary>
        public string org_desc
        {
          get{return _org_desc;}
          set{_org_desc = value;}
        }

        /// <summary>
        ///      int,length:10,default:(0)
        /// </summary>
        public int isdelete
        {
          get{return _isdelete;}
          set{_isdelete = value;}
        }
	}

}