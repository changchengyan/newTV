/// <summary>
/// label_sales表LabelSalesVO 值对象,创建时间:2016/11/17 17:47:45(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：标签关联值对象
/// </summary>

namespace Redsz.VO
{

	public class LabelSalesVO
	{
        private int _id; //      int,length:4,
        private int _sales_id; //      int,length:4,
        private string _sales_name; //      nvarchar,length:510,
        private int _label_subject_id; //      int,length:4,
        private int _label_subject_level_id; //      int,length:4,
        private int _label_purpose_id; //      int,length:4,
        private string _createtime; //      datetime,length:8,
        private int _label_unified; //      int,length:4,


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
        public int sales_id
        {
          get{return _sales_id;}
          set{_sales_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string sales_name
        {
          get{return _sales_name;}
          set{_sales_name = value;}
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
        ///      int,length:4
        /// </summary>
        public int label_subject_level_id
        {
          get{return _label_subject_level_id;}
          set{_label_subject_level_id = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int label_purpose_id
        {
          get{return _label_purpose_id;}
          set{_label_purpose_id = value;}
        }

        /// <summary>
        ///      datetime,length:8
        /// </summary>
        public string createtime
        {
          get{return _createtime;}
          set{_createtime = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int label_unified
        {
          get{return _label_unified;}
          set{_label_unified = value;}
        }
	}

}