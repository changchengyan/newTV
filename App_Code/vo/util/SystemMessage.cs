using System.Collections.Generic;
/// <summary>
/// 系统消息对象
/// </summary>
namespace Redsz.VO
{

    public class SystemMessage
    {
        private bool _success;//成功标识
        private List<string> _massge;//提示信息

        /// <summary>
        ///  消息列表
        /// </summary>
        public List<string> massge
        {
            get { return _massge; }
            set { _massge = value; }
        }

        /// <summary>
        /// 成功标识
        /// </summary>
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }
	}

}