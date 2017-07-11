using System;
using System.Collections;
using System.Web;
using System.Web.Security;

using System.Text;
using System.IO;
using Redsz;
using Redsz.BO;
using Redsz.VO;
using Redsz.DAO;
using System.Timers;


/// <summary>
/// 定时器类
/// </summary>
namespace Redsz
{
    public class Timer
    {


        public static System.Timers.Timer aTimer = null;
        public static DateTime runtime;
        public static DateTime inittime;
        public static HttpContext httpcontext;

        public static void Init(HttpContext context)
        {
            httpcontext = context;

            if (aTimer == null)
            {
                inittime = DateTime.Now;
                aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                aTimer.Interval = 60000;//毫秒，1分钟1次
                aTimer.Enabled = true;
                GC.KeepAlive(aTimer);
            }
            else
            { 
                
            
            }
            
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            runtime = DateTime.Now;
            //此处写定时要调用的方法        
        }

        public static void Stop()
        {
            aTimer.Stop();
        }

        public static void Start()
        {
            aTimer.Start();
        }



    }
}
