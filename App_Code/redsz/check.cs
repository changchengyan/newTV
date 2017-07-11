
using System.Web;
using Redsz.BO;
namespace Redsz
{

    /// <summary>
    /// check进行校验
    /// </summary>
    public class check
    {

        public static void requestCheck(HttpRequest req, HttpResponse res)
        {
            bool feifa = false;

            string key = "'|\"|!|(|)";
            string[] keys = key.Split('|');

            for (int i = 0; i < req.Form.Count; i++)
            {
                // res.Write(req.Form[i] + "非法字符串<br>");

                for (int j = 0; j < keys.Length; j++)
                {
                    if (req.Form[i].IndexOf(keys[j]) > -1)
                    {
                        res.Write(req.Form.GetKey(i) + " = " + req.Form[i] + " Form 非法字符串“<font color=#ff0000>" + keys[j] + "</font>”<br>");
                        feifa = true;
                    }
                }

            }

            for (int i = 0; i < req.QueryString.Count; i++)
            {
                for (int j = 0; j < keys.Length; j++)
                {
                    if (req.QueryString[i].IndexOf(keys[j]) > -1)
                    {
                        res.Write(req.QueryString.GetKey(i) + " = " + req.QueryString[i] + " QueryString 非法字符串“<font color=#ff0000>" + keys[j] + "</font>”<br>");
                        feifa = true;
                    }
                }

            }



            if (feifa) { res.End(); }


        }
        
        /// <summary>
        /// 检查是否为非法数字，非法时停止页面输出
        /// </summary>
        public static void numberCheck(HttpRequest req, HttpResponse res, string s, bool notNull)
        {
            if (notNull)
            {

                try
                {
                    int i = int.Parse(s);
                }
                catch
                {
                    res.Write("<b>很抱歉，没有您需要的信息！</b><br><br>造成错误的原因有：<ul><li>缺少某些数字型参数</li><li>某些数字型参数没有正确赋值</li></ul>");
                    res.End();
                }

            }
            else if(s!=null && !"".Equals(s)){

                try
                {
                    int i = int.Parse(s);
                }
                catch
                {
                    res.Write("<b>很抱歉，没有您需要的信息！</b><br><br>造成错误的原因有：<ul><li>缺少某些数字型参数的值</li><li>某些数字型参数没有正确赋值</li></ul>");                    
                    res.End();
                }
            }
        }

        /// <summary>
        /// 检查是否为非法数字，非法时停止页面输出
        /// </summary>
        public static void valueCheck(HttpRequest req, HttpResponse res, object o)
        {
            if(o==null)
            {
                res.Write("<b>很抱歉，没有您需要的信息！</b><br><br>造成错误的原因有：<ul><li>记录不存在</li><li>原有的数据信息已被管理员删除</li></ul>");
                res.End(); 
            }
        }

        /// <summary>
        /// 检查是否为数字，返回true false
        /// </summary>
        public static bool isnumber(string s)
        {
            bool b = false;

                try
                {
                    int i = int.Parse(s);
                    b = true;
                }
                catch
                {
                    b = false;
                }
                return b;
        }

        /// <summary>
        /// 格式化查询字符串，防止sql语句出错
        /// </summary>
        public static string formatSearchString(string s)
        {
           return s.Replace("'","");
        }



        /// <summary>
        /// 检查个人用户是否已经登录
        /// </summary>
        public static void checkIuserLogin(HttpRequest req, HttpResponse res)
        {
            if (req.Cookies["iuserInfo"] != null)
            {


            }
            else
            {

                res.Redirect("login.aspx?passurl=" + UtilBO.formatPassurl(req.Url.ToString()));
                res.End();
            }
        }

        /// <summary>
        /// 检查企业用户是否已经登录
        /// </summary>
        public static void checkComLogin(HttpRequest req, HttpResponse res)
        {
            if (req.Cookies["comInfo"] != null)
            {


            }
            else
            {

                res.Redirect("login_com.aspx?passurl=" + UtilBO.formatPassurl(req.Url.ToString()));
                res.End();
            }
        }


        /// <summary>
        /// 检查管理员用户是否已经登录
        /// </summary>
        public static void checkAdimnLogin(HttpRequest req, HttpResponse res)
        {
            if (req.Cookies["adminInfo"] != null)
            {
                //禁止缓存页面内容
                res.Cache.SetNoStore();
                res.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches); 

            }
            else
            {

                res.Write("<script>alert('需要重新登录！');window.top.location='/system/login/';</script>");
                res.End();
            }
        }

    }
}