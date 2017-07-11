using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Redsz.VO
{
    [Serializable]
    public class ReturnDataVO
    {
        /// <summary>
        /// 返回成功与否
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 返回信息，如果失败则是错误提示
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 基础数据
        /// </summary>
        public object data { get; set; }
    }
}