using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Net;
using System.Text;
using Redsz;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;


/// <summary>
/// 图片处理值对象
/// </summary>
namespace Redsz
{
    public class PhotoFormat
    {

        private int _width;
        private int _height;
        private string _append;
        private bool _isFormatArea = false;
        private bool _isShuiYin = false;
        private string _shuiYinImagePath;


        /// <summary>
        /// 格式化图片的宽度
        /// </summary>
        public int width
        {
            get { return _width; }
            set { _width = value; }
        }


        /// <summary>
        /// 格式化图片的高度
        /// </summary>
        public int height
        {
            get { return _height; }
            set { _height = value; }
        }
    

        /// <summary>
        /// 格式化后的文件名 需要增加的字符 如：123.jpg 变为 123_a.jpg ，则 append = "_a"
        /// </summary>
        public string append
        {
            get { return _append; }
            set { _append = value; }
        }

        /// <summary>
        /// 是否强制生成当前长宽值的图片，强制后可能图片会出现白色背景，一般为false
        /// </summary>
        public bool isFormatArea
        {
            get { return _isFormatArea; }
            set { _isFormatArea = value; }
        }

        /// <summary>
        /// 水印图片的本地web虚拟路径
        /// </summary>
        public string shuiYinImagePath
        {
            get { return _shuiYinImagePath; }
            set { _shuiYinImagePath = value; }
        }

        /// <summary>
        /// 是否要添加水印
        /// </summary>
        public bool isShuiYin
        {
            get { return _isShuiYin; }
            set { _isShuiYin = value; }
        }




    }
}
