/// <summary>
/// label_subject表LabelSubjectVO 值对象,创建时间:2016/11/17 9:52:53(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：分类标签值对象
/// </summary>

namespace Redsz.VO
{

	public class LabelSubjectVO
	{
        private int _id; //      int,length:4,
        private int _industry_id; //      int,length:4,
        private string _subject_name; //      nvarchar,length:510,
        private int _topsize; //      int,length:4,


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
        public int industry_id
        {
          get{return _industry_id;}
          set{_industry_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string subject_name
        {
          get{return _subject_name;}
          set{_subject_name = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int topsize
        {
            get { return _topsize; }
            set { _topsize = value; }
        }

    }

}