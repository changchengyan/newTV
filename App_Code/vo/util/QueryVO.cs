/// <summary>
/// sitetag��SitetagVO ֵ����,����ʱ��:2011-3-10 23:31:17(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///��������Ŀ��ǩ����ֵ����
/// </summary>

namespace Redsz.VO
{

    public class QueryVO
	{
        private System.Data.DataTable _list; //��ѯ���:
        private string _html; //��ѯ�����ҳ����
        private int _total;
        /// <summary>
        ///  ��ǰҳ����list
        /// </summary>
        public System.Data.DataTable list
        {
            get { return _list; }
            set { _list = value; }
        }

        /// <summary>
        ///  ��ҳHTML
        /// </summary>
        public string html
        {
            get { return _html; }
            set { _html = value; }
        }


        /// <summary>
        /// �ܼ�¼��
        /// </summary>
        public int total
        {
            get { return _total; }
            set { _total = value; }
        }
    }

}