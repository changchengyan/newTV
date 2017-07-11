/// <summary>
/// sitetag表SitetagVO 值对象,创建时间:2011-3-10 23:31:17(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：栏目标签分类值对象
/// </summary>

namespace Redsz.VO
{

    public class QueryVO
	{
        private System.Data.DataTable _list; //查询结果:
        private string _html; //查询结果分页对象
        private int _total;
        /// <summary>
        ///  当前页数据list
        /// </summary>
        public System.Data.DataTable list
        {
            get { return _list; }
            set { _list = value; }
        }

        /// <summary>
        ///  分页HTML
        /// </summary>
        public string html
        {
            get { return _html; }
            set { _html = value; }
        }


        /// <summary>
        /// 总记录数
        /// </summary>
        public int total
        {
            get { return _total; }
            set { _total = value; }
        }
    }

}