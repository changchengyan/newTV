using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;
using Redsz.DAO;
using Redsz.BO;
using Redsz.VO;
using Redsz;
/// <summary>
/// 题库 Services
/// </summary>
/// 
namespace Redsz.Services
{

    public class DuoyueServices
    {

        public DuoyueServices()
        {            


        }




        /// <summary> 
        /// 由页面发起，执行业务方法
        /// </summary> 
        /// <returns>Object </returns> 
        public string RequestExecutive(HttpRequest req)
        {
            string returnString = "";
            string reqMethod = req["method"];
            string className = reqMethod.Split('.')[0];
            string methodName = reqMethod.Split('.')[1];
            string[] reqParameter = req.Form.GetValues("parameter");
            object[] parameter = null;
            if (reqParameter != null)
            {
                parameter = new object[reqParameter.Length + 1];
            }
            else
            {
                parameter = new object[1];            
            }
            parameter[0] = req;
            for (int i = 1; i < parameter.Length; i++)
            {
                parameter[i] = reqParameter[i - 1];
            }
            Type myType = Type.GetType("Redsz.BO." + className);//抽象基类
            Object myObject = System.Activator.CreateInstance(myType);//实例化类
            MethodInfo[] methodInfo = myType.GetMethods();//获得基类的方法集
            foreach (MethodInfo info in methodInfo)
            {
                if (info.Name.Equals(methodName))
                {
                    returnString = info.Invoke(myObject, parameter).ToString();
                    break;
                }
            }
            return returnString;
        }


        /// <summary> 
        /// 由流程（类）发起，执行业务方法
        /// </summary> 
        /// <returns>Object </returns> 
        public Object Executive(string method, object[] parameter)
        {
            Object returnObject = new object();
            string className = method.Split('.')[0];
            Type myType = Type.GetType("Redsz.BO." + className);//抽象基类
            Object myObject = System.Activator.CreateInstance(myType);//实例化类
            MethodInfo[] methodInfo = myType.GetMethods();//获得基类的方法集
            foreach (MethodInfo info in methodInfo)
            {
                if (info.Name.Equals(method))
                {
                    returnObject = info.Invoke(myObject, parameter);
                    break;
                }
            }
            return returnObject;
        }


        /// <summary>
        /// 新增记录
        /// </summary>
        public static void Insert(string tableName, Hashtable ht,string logUsername,string logTitle )
        {

            
            

            //日志
            StackTrace st = new StackTrace(true);
            SysLogVO syslogvo = new SysLogVO();
            syslogvo.code = "Insert";
            syslogvo.datetime = DateTime.Now.ToString();
            syslogvo.title = logTitle;
            syslogvo.username = logUsername;
            syslogvo.state = "成功";
            syslogvo.ip = UtilBO.GetWebClientIp();
            syslogvo.bo = st.GetFrame(1).GetMethod().ReflectedType.Name + "." + st.GetFrame(1).GetMethod().Name;
            try
            {
                //新增
                Data.Insert(tableName, ht);
            }
            catch(Exception e) 
            {
                syslogvo.state = "失败";
                syslogvo.info = e.InnerException + "\n" + e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\n" + e.TargetSite;
            }           
            
            SysLogBO.Add(syslogvo);

            sys.createTextFile("/log/insert.txt", logUsername + ":" + logTitle);

        }


        /// <summary>
        /// 修改记录
        /// </summary>
        public static void Update(string tableName, string where, Hashtable ht, string logUsername, string logTitle)
        {

            //日志
            StackTrace st = new StackTrace(true);
            SysLogVO syslogvo = new SysLogVO();
            syslogvo.code = "Update";
            syslogvo.datetime = DateTime.Now.ToString();
            syslogvo.title = logTitle;
            syslogvo.username = logUsername;
            syslogvo.state = "成功";
            syslogvo.ip = UtilBO.GetWebClientIp();
            syslogvo.bo = st.GetFrame(1).GetMethod().ReflectedType.Name + "." + st.GetFrame(1).GetMethod().Name;
            try
            {
                //修改
                Data.Update(tableName,where, ht);
            }
            catch(Exception e) 
            {
                syslogvo.state = "失败";
                syslogvo.info = e.InnerException + "\n" + e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\n" + e.TargetSite;
            }           
            
            SysLogBO.Add(syslogvo);

            sys.createTextFile("/log/insert.txt", logUsername + ":" + logTitle);

        }


    }
}
