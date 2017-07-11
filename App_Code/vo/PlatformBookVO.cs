/// <summary>
/// platform_book��PlatformBookVO ֵ����,����ʱ��:2017/6/6 11:22:41(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ֵ����
/// </summary>

namespace Redsz.VO
{

	public class PlatformBookVO
	{
        private int _id; //      int,length:4,
        private string _book_name; //�鼮����      nvarchar,length:600,
        private string _book_pic; //�鼮ͼƬ      nvarchar,length:510,
        private string _book_desc; //      nvarchar,length:2000,
        private string _book_info; //      ntext,length:16,
        private string _isbn; //���      nvarchar,length:100,
        private string _author; //����      nvarchar,length:1000,
        private string _author_pic; //������Ƭ      nvarchar,length:512,
        private string _author_info; //���߼��      ntext,length:16,
        private string _publish_house; //������      nvarchar,length:400,
        private string _publish_data; //����ʱ��      nvarchar,length:100,
        private string _create_sales_name; //��ƽ̨֮�⣬һ�㶼Ϊ adviser | merchant ��˭�����ģ�˭�й���Ȩ��      nvarchar,length:100,
        private int _create_sales_id; //      int,length:4,
        private string _createtime; //      datetime,length:8,
        private string _random; //      nvarchar,length:100,
        private int _circulation; //������      int,length:4,
        private int _answer_count; //���飨���߻��߱༭���ش��˶��ٸ�����      int,length:4,
        private int _answer_user_count; //���飨���߻��߱༭�������ٸ����߻ش�������      int,length:4,
        private string _book_info_mp3_url; //�鼮��ϸ��Ϣbook_infoתMP3      nvarchar,length:510,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///�鼮����      nvarchar,length:600
        /// </summary>
        public string book_name
        {
          get{return _book_name;}
          set{_book_name = value;}
        }

        /// <summary>
        ///�鼮ͼƬ      nvarchar,length:510
        /// </summary>
        public string book_pic
        {
          get{return _book_pic;}
          set{_book_pic = value;}
        }

        /// <summary>
        ///      nvarchar,length:2000
        /// </summary>
        public string book_desc
        {
          get{return _book_desc;}
          set{_book_desc = value;}
        }

        /// <summary>
        ///      ntext,length:16
        /// </summary>
        public string book_info
        {
          get{return _book_info;}
          set{_book_info = value;}
        }

        /// <summary>
        ///���      nvarchar,length:100
        /// </summary>
        public string isbn
        {
          get{return _isbn;}
          set{_isbn = value;}
        }

        /// <summary>
        ///����      nvarchar,length:1000
        /// </summary>
        public string author
        {
          get{return _author;}
          set{_author = value;}
        }

        /// <summary>
        ///������Ƭ      nvarchar,length:512
        /// </summary>
        public string author_pic
        {
          get{return _author_pic;}
          set{_author_pic = value;}
        }

        /// <summary>
        ///���߼��      ntext,length:16
        /// </summary>
        public string author_info
        {
          get{return _author_info;}
          set{_author_info = value;}
        }

        /// <summary>
        ///������      nvarchar,length:400
        /// </summary>
        public string publish_house
        {
          get{return _publish_house;}
          set{_publish_house = value;}
        }

        /// <summary>
        ///����ʱ��      nvarchar,length:100
        /// </summary>
        public string publish_data
        {
          get{return _publish_data;}
          set{_publish_data = value;}
        }

        /// <summary>
        ///��ƽ̨֮�⣬һ�㶼Ϊ adviser | merchant ��˭�����ģ�˭�й���Ȩ��      nvarchar,length:100
        /// </summary>
        public string create_sales_name
        {
          get{return _create_sales_name;}
          set{_create_sales_name = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int create_sales_id
        {
          get{return _create_sales_id;}
          set{_create_sales_id = value;}
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
        ///      nvarchar,length:100
        /// </summary>
        public string random
        {
          get{return _random;}
          set{_random = value;}
        }

        /// <summary>
        ///������      int,length:4
        /// </summary>
        public int circulation
        {
          get{return _circulation;}
          set{_circulation = value;}
        }

        /// <summary>
        ///���飨���߻��߱༭���ش��˶��ٸ�����      int,length:4
        /// </summary>
        public int answer_count
        {
          get{return _answer_count;}
          set{_answer_count = value;}
        }

        /// <summary>
        ///���飨���߻��߱༭�������ٸ����߻ش�������      int,length:4
        /// </summary>
        public int answer_user_count
        {
          get{return _answer_user_count;}
          set{_answer_user_count = value;}
        }

        /// <summary>
        ///�鼮��ϸ��Ϣbook_infoתMP3      nvarchar,length:510
        /// </summary>
        public string book_info_mp3_url
        {
          get{return _book_info_mp3_url;}
          set{_book_info_mp3_url = value;}
        }
	}

}