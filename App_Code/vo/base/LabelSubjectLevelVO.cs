/// <summary>
/// label_subject_level��LabelSubjectLevelVO ֵ����,����ʱ��:2016/11/17 10:31:47(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ֵ����
/// </summary>

namespace Redsz.VO
{

	public class LabelSubjectLevelVO
	{
        private int _id; //      int,length:4,
        private int _label_subject_id; //      int,length:4,
        private string _subject_level_name; //      nvarchar,length:510,
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
        public int label_subject_id
        {
          get{return _label_subject_id;}
          set{_label_subject_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string subject_level_name
        {
          get{return _subject_level_name;}
          set{_subject_level_name = value;}
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