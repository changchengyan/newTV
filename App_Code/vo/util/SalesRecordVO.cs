/// <summary>
/// sitetag��SitetagVO ֵ����,����ʱ��:2011-3-10 23:31:17(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///��������Ŀ��ǩ����ֵ����
/// </summary>

namespace Redsz.VO
{

    public class SalesRecordVO
    {
        private string _title;
        private string _desc;
        private string _pic;
        private string _info;

        /// <summary>
        /// ����
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public string desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        /// <summary>
        /// ͼƬ
        /// </summary>
        public string pic
        {
            get { return _pic; }
            set { _pic = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string info
        {
            get { return _info; }
            set { _info = value; }
        }



    }

}