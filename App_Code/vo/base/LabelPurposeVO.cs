/// <summary>
/// label_purpose��LabelPurposeVO ֵ����,����ʱ��:2016/11/17 13:05:12(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������Ŀ�ı�ǩֵ����
/// </summary>

namespace Redsz.VO
{

	public class LabelPurposeVO
	{
        private int _id; //      int,length:4,
        private int _industry_id; //      int,length:4,
        private string _purpose_name; //      nvarchar,length:510,
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
        public string purpose_name
        {
          get{return _purpose_name;}
          set{_purpose_name = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }
	}

}