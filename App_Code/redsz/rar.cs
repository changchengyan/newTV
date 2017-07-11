using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Collections;
using Redsz;

namespace Redsz.BO
{
    /// <summary>
    /// rar 的摘要说明
    /// </summary>
    public class rar
    {
        public rar()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        /// <summary>
        /// 压缩打包文件到rar
        /// </summary>
        /// <param name="unRarPatch">文件解压到哪个目录</param>
        /// <param name="rarPatch">rar位于哪个目录下</param>
        /// <param name="rarName">rar的文件名</param>
        /// <returns></returns>
        public void RARsave(string patch, string rarPatch, string rarName)
        {

        }


        /// <summary>
        /// 解压rar文件到指定的文件夹，并返回多个文件的名称hastable
        /// </summary>
        /// <param name="unRarPatch">文件解压到哪个目录</param>
        /// <param name="rarPatch">rar位于哪个目录下</param>
        /// <param name="rarName">rar的文件名</param>
        /// <returns></returns>
        public static bool unRAR(string unRarPatch, string rarPatch, string rarName)
        {
            bool b = false;
            DataTable table = new DataTable();
            String the_rar;
            RegistryKey the_Reg;
            Object the_Obj;
            String the_Info;
            ProcessStartInfo the_StartInfo;
            Process the_Process;
            try
            {

                //解压的文件夹不存在则创建
                if (Directory.Exists(unRarPatch) == false)
                {
                    Directory.CreateDirectory(unRarPatch);
                }

                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(unRarPatch));
                the_Info = "x  \"" + rarName + "\"  \"" + System.Web.HttpContext.Current.Server.MapPath(unRarPatch) + "\" -y";
                the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = System.Web.HttpContext.Current.Server.MapPath("/bin/WinRAR.exe");// @"E:\WebWork\酷派手机\bin\WinRAR.exe";
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                the_StartInfo.WorkingDirectory = System.Web.HttpContext.Current.Server.MapPath(rarPatch);//获取压缩包路径
                the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
                the_Process.WaitForExit();
                the_Process.Close();

                b = true;

            }
            catch
            {

            }
            return b;
        }

        
        /*

         RAR参数：
            一、压缩命令
            1、将temp.txt压缩为temp.rarrar a temp.rar temp.txt 
            2、将当前目录下所有文件压缩到temp.rarrar a temp.rar *.* 
            3、将当前目录下所有文件及其所有子目录压缩到temp.rarrar a temp.rar *.* -r 
            4、将当前目录下所有文件及其所有子目录压缩到temp.rar，并加上密码123rar a temp.rar *.* -r -p123

            二、解压命令
            1、将temp.rar解压到c:\temp目录rar e temp.rar c:\temprar e *.rar c:\temp(支持批量操作) 
            2、将temp.rar解压到c:\temp目录，并且解压后的目录结构和temp.rar中的目录结构一


            压缩目录test及其子目录的文件内容 
            Wzzip test.zip test -r -P 
            WINRAR A test.rar test -r 

            删除压缩包中的*.txt文件 
            Wzzip test.zip *.txt -d 
            WinRAR d test.rar *.txt 


            刷新压缩包中的文件，即添加已经存在于压缩包中但更新的文件 
            Wzzip test.zip test -f 
            Winrar f test.rar test 


            更新压缩包中的文件，即添加已经存在于压缩包中但更新的文件以及新文件 
            Wzzip test.zip test -u 
            Winrar u test.rar test 


            移动文件到压缩包，即添加文件到压缩包后再删除被压缩的文件 
            Wzzip test.zip -r -P -m 
            Winrar m test.rar test -r 


            添加全部 *.exe 文件到压缩文件，但排除有 a或b 
            开头名称的文件 
            Wzzip test *.exe -xf*.* -xb*.* 
            WinRAR a test *.exe -xf*.* -xb*.* 


            加密码进行压缩 
            Wzzip test.zip test 
            -s123。注意密码是大小写敏感的。在图形界面下打开带密码的压缩文件，会看到+号标记（附图1）。 
            WINRAR A test.rar test -p123 
            -r。注意密码是大小写敏感的。在图形界面下打开带密码的压缩文件，会看到*号标记（附图2）。 


            按名字排序、以简要方式列表显示压缩包文件 
            Wzzip test.zip -vbn 
            Rar l test.rar 


            锁定压缩包，即防止未来对压缩包的任何修改 
            无对应命令 
            Winrar k test.rar 


            创建360kb大小的分卷压缩包 
            无对应命令 
            Winrar a -v360 test 


            带子目录信息解压缩文件 
            Wzunzip test -d 
            Winrar x test -r 


            不带子目录信息解压缩文件 
            Wzunzip test 
            Winrar e test 


            解压缩文件到指定目录，如果目录不存在，自动创建 
            Wzunzip test newfolder 
            Winrar x test newfolder 


            解压缩文件并确认覆盖文件 
            Wzunzip test -y 
            Winrar x test -y 


            解压缩特定文件 
            Wzunzip test *.txt 
            Winrar x test *.txt 


            解压缩现有文件的更新文件 
            Wzunzip test -f 
            Winrar x test -f 


            解压缩现有文件的更新文件及新文件 
            Wzunzip test -n 
            Winrar x test -u 


            批量解压缩文件 
            Wzunzip *.zip 
            WinRAR e *.rar


         */
    }
}