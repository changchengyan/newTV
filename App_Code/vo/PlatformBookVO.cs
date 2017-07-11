/// <summary>
/// platform_book表PlatformBookVO 值对象,创建时间:2017/6/6 11:22:41(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：值对象
/// </summary>

namespace Redsz.VO
{

	public class PlatformBookVO
	{
        private int _id; //      int,length:4,
        private string _book_name; //书籍名称      nvarchar,length:600,
        private string _book_pic; //书籍图片      nvarchar,length:510,
        private string _book_desc; //      nvarchar,length:2000,
        private string _book_info; //      ntext,length:16,
        private string _isbn; //书号      nvarchar,length:100,
        private string _author; //作者      nvarchar,length:1000,
        private string _author_pic; //作者照片      nvarchar,length:512,
        private string _author_info; //作者简介      ntext,length:16,
        private string _publish_house; //出版社      nvarchar,length:400,
        private string _publish_data; //出版时间      nvarchar,length:100,
        private string _create_sales_name; //除平台之外，一般都为 adviser | merchant （谁创建的，谁有管理权）      nvarchar,length:100,
        private int _create_sales_id; //      int,length:4,
        private string _createtime; //      datetime,length:8,
        private string _random; //      nvarchar,length:100,
        private int _circulation; //发行量      int,length:4,
        private int _answer_count; //此书（作者或者编辑）回答了多少个问题      int,length:4,
        private int _answer_user_count; //此书（作者或者编辑）给多少个读者回答了问题      int,length:4,
        private string _book_info_mp3_url; //书籍详细信息book_info转MP3      nvarchar,length:510,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///书籍名称      nvarchar,length:600
        /// </summary>
        public string book_name
        {
          get{return _book_name;}
          set{_book_name = value;}
        }

        /// <summary>
        ///书籍图片      nvarchar,length:510
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
        ///书号      nvarchar,length:100
        /// </summary>
        public string isbn
        {
          get{return _isbn;}
          set{_isbn = value;}
        }

        /// <summary>
        ///作者      nvarchar,length:1000
        /// </summary>
        public string author
        {
          get{return _author;}
          set{_author = value;}
        }

        /// <summary>
        ///作者照片      nvarchar,length:512
        /// </summary>
        public string author_pic
        {
          get{return _author_pic;}
          set{_author_pic = value;}
        }

        /// <summary>
        ///作者简介      ntext,length:16
        /// </summary>
        public string author_info
        {
          get{return _author_info;}
          set{_author_info = value;}
        }

        /// <summary>
        ///出版社      nvarchar,length:400
        /// </summary>
        public string publish_house
        {
          get{return _publish_house;}
          set{_publish_house = value;}
        }

        /// <summary>
        ///出版时间      nvarchar,length:100
        /// </summary>
        public string publish_data
        {
          get{return _publish_data;}
          set{_publish_data = value;}
        }

        /// <summary>
        ///除平台之外，一般都为 adviser | merchant （谁创建的，谁有管理权）      nvarchar,length:100
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
        ///发行量      int,length:4
        /// </summary>
        public int circulation
        {
          get{return _circulation;}
          set{_circulation = value;}
        }

        /// <summary>
        ///此书（作者或者编辑）回答了多少个问题      int,length:4
        /// </summary>
        public int answer_count
        {
          get{return _answer_count;}
          set{_answer_count = value;}
        }

        /// <summary>
        ///此书（作者或者编辑）给多少个读者回答了问题      int,length:4
        /// </summary>
        public int answer_user_count
        {
          get{return _answer_user_count;}
          set{_answer_user_count = value;}
        }

        /// <summary>
        ///书籍详细信息book_info转MP3      nvarchar,length:510
        /// </summary>
        public string book_info_mp3_url
        {
          get{return _book_info_mp3_url;}
          set{_book_info_mp3_url = value;}
        }
	}

}