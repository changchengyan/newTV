using System;
using System.Collections;
using System.Web;
using System.Web.Security;

using System.Text;
using Redsz;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using Redsz.BO;

/// <summary>
/// upload 的摘要说明
/// </summary>
namespace Redsz.DAO
{
    public class upload
    {
        //文件上传虚拟路径
        public static string USER_FILE_PATH = "/upload/";

        /// <summary>
        /// 单个文件上传,如果是图片则自动返回大图和小图
        /// //作为文本编辑器中的图片上传处理
        /// </summary>
        public static string upfile(HttpPostedFile myFile)
        {
            string returnstr = "";
            if (myFile!= null)
            {
                //定义一些变量
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i);
                string filetype = nam.Substring(i+1).ToLower();
                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(USER_FILE_PATH + newnm));
              
                //System.Web.HttpContext.Current.Server.CreateObject("Persits.Jpeg");


                //取得上传文件的各种属性。 
                //fname.Text=myFile.FileName; 
                //fenc.Text=myFile.ContentType ; 
                //fsize.Text=myFile.ContentLength.ToString();

                               
                    if ("gif".Equals(filetype) || "jpeg".Equals(filetype) || "bmp".Equals(filetype) || "png".Equals(filetype) || "jpg".Equals(filetype))
                    {
                       //作为文本编辑器中的图片上传处理
                        returnstr = formatPhoto(USER_FILE_PATH + newnm, 1024, 3000, "_max", true, false) + "|" + formatPhoto(USER_FILE_PATH + newnm, 650, 3000, "_min", true, false);//打水印并返回两个图片路径，一大一小

                        //删除原图
                        sys.deleteFile(System.Web.HttpContext.Current.Server.MapPath(USER_FILE_PATH + newnm));


                    }
                    else
                    {
                        returnstr = USER_FILE_PATH + newnm;
                    }
               
            }

            return returnstr;
        }



        /// <summary>
        /// 单个文件上传,如果是图片则自动返回大图和小图
        /// //作为文本编辑器中的图片上传处理
        /// </summary>
        public static string mBlogUpfile(HttpRequest req, HttpPostedFile myFile)
        {
            string s = "{\"success\":false}";
            string returnstr = "";
            if (myFile != null)
            {
                //定义一些变量
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i);
                string filetype = nam.Substring(i + 1).ToLower();

                string folder = "/u/upload/file/" + sys.getYearMonthStr() + "/";
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folder)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folder));
                }

                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(folder + newnm));
                returnstr = folder + newnm;
                s = "{\"success\":true,\"path\":\"" + returnstr + "\"}";

            }

            return s;
        }


        /// <summary>
        /// 单个文件上传,如果是图片则自动返回大图和小图
        /// //作为文本编辑器中的图片上传处理
        /// </summary>
        public static string mBlogUppic(HttpRequest req, HttpPostedFile myFile)
        {
            string s = "{\"success\":false}";
            string returnstr = "";
            if (myFile != null)
            {
                //定义一些变量
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i);
                string filetype = nam.Substring(i + 1).ToLower();

                string folder = "/u/upload/pic/" + sys.getYearMonthStr() + "/";
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folder)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folder));
                }

                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(folder + newnm));

                if ("gif".Equals(filetype) || "jpeg".Equals(filetype) || "bmp".Equals(filetype) || "png".Equals(filetype) || "jpg".Equals(filetype))
                {
                    string p1 = formatPhoto(folder + newnm, 120, 3000, "_a", false, false);
                    string p2 = formatPhoto(folder + newnm, 150, 3000, "_a1", false, false);
                    string p3 = formatPhoto(folder + newnm, 180, 3000, "_a2", false, false);
                    string p4 = formatPhoto(folder + newnm, 440, 3000, "_b", false, false);
                   

                    //删除原图
                   // sys.deleteFile(System.Web.HttpContext.Current.Server.MapPath(folder + newnm));


                }

                returnstr = folder + newnm;

                s = "{\"success\":true,\"path\":\"" + returnstr + "\"}";
            }

            return s;
        }


        /// <summary>
        /// 上传个人头像
        /// </summary>
        public static string mBlogUploadFace(HttpRequest req,HttpPostedFile myFile)
        {
            string returnstr = "";
            if (myFile != null && req.Cookies["iuserInfo"] != null)
           {
               string userid = req.Cookies["iuserInfo"].Values["id"].ToString();
               string path = "/u/userface/" + (int.Parse(userid) + (100 - int.Parse(userid) % 100)+"/");
               if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
               {
                   System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));
               }
                //定义一些变量
                string nam = myFile.FileName;
                //int i = nam.LastIndexOf(".");
                string newnm = userid  + ".jpg";// nam.Substring(i);
               // string filetype = nam.Substring(i + 1).ToLower();
                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path + newnm));
                returnstr = path + newnm;
               

            }

            return returnstr;
        }


        /// <summary>
        /// 数据流上传图片
        /// </summary>
        public static string UploadImgByte(HttpRequest req, HttpPostedFile myFile)
        {
            string returnstr = "";
            byte[] ibyte = req.BinaryRead(req.TotalBytes);
            //Stream outStream = req.g


            return returnstr;
        }






        /// <summary>
        /// 弹出上传窗口通用界面使用，其它请从业务方法中调用此类的其它方法
        /// 文件上传、图片片上传(isDouble:是否需要返回两个图片地址，即一大一小)
        /// </summary>
        public static string uploadfile(HttpPostedFile myFile, int maxWidth, int maxHeight, bool isDouble)
        {
            string returnstr = "";
            if (myFile != null)
            {
                //定义一些变量
                string nam = myFile.FileName; //设置文件的路径
                int i = nam.LastIndexOf(".");   //倒序查询
                string newnm = sys.getRandomStr() + nam.Substring(i); 
                string filetype = nam.Substring(i + 1).ToLower();
                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(USER_FILE_PATH + newnm));

                //System.Web.HttpContext.Current.Server.CreateObject("Persits.Jpeg");


                //取得上传文件的各种属性。 
                //fname.Text=myFile.FileName; 
                //fenc.Text=myFile.ContentType ; 
                //fsize.Text=myFile.ContentLength.ToString();


                if (maxWidth > 0 && maxHeight>0 &&("gif".Equals(filetype) || "jpeg".Equals(filetype) || "bmp".Equals(filetype) || "png".Equals(filetype) || "jpg".Equals(filetype)))
                {
                    
                    if (isDouble)
                    {
                        returnstr = formatPhoto(USER_FILE_PATH + newnm, maxWidth, maxHeight, "_min", false) + "|" + formatPhoto(USER_FILE_PATH + newnm, 240, 240, "_max", true);//打水印并返回两个图片路径，一大一小
                    }
                    else 
                    {
                        returnstr = formatPhoto(USER_FILE_PATH + newnm, maxWidth, maxHeight, "_note", false);//打水印并返回两个图片路径，一大一小
                    }
                    
                    
                    
                }
                else
                {
                    returnstr = USER_FILE_PATH + newnm;
                }

            }

            return returnstr;
        }






        /// <summary>
        /// 业务类专用方法
        /// 图片上传,返回路径包括 格式化的大图、小图和原图 用"|"分割
        /// </summary>
        public static string uploadPic(HttpPostedFile myFile, int max_w, int max_h, int min_w, int min_h, bool maxshuiyin, bool minshuiyin)
        {
            string returnstr = "";
            if (myFile != null)
            {
                //定义一些变量
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i);
                string filetype = nam.Substring(i + 1).ToLower();
                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(USER_FILE_PATH + newnm));

                    if ("gif".Equals(filetype) || "jpeg".Equals(filetype) || "bmp".Equals(filetype) || "png".Equals(filetype) || "jpg".Equals(filetype))
                    {
                        returnstr = formatPhoto(USER_FILE_PATH + newnm, max_w, max_h, "_max", maxshuiyin) + "|" + formatPhoto(USER_FILE_PATH + newnm, min_w, min_h, "_min", minshuiyin) + "|" + USER_FILE_PATH + newnm;//打水印并返回两个图片路径，一大一小
                    }
                    else
                    {
                        returnstr = USER_FILE_PATH + newnm + "||";
                    }
                
            }

            return returnstr;
        }



        /// <summary>
        /// 业务类专用方法
        /// 图片上传,返回路径包括 格式化的大图、中图、小图和原图 用"|"分割
        /// </summary>
        public static string uploadPic(HttpPostedFile myFile, int max_w, int max_h, int mid_w, int mid_h, int min_w, int min_h, bool maxshuiyin, bool minshuiyin, bool midshuiyin)
        {
            string returnstr = "";
            if (myFile != null)
            {
                //定义一些变量
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i);
                string filetype = nam.Substring(i + 1).ToLower();
                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(USER_FILE_PATH + newnm));

                if ("gif".Equals(filetype) || "jpeg".Equals(filetype) || "bmp".Equals(filetype) || "png".Equals(filetype) || "jpg".Equals(filetype))
                {
                    returnstr = formatPhoto(USER_FILE_PATH + newnm, max_w, max_h, "_max", maxshuiyin) + "|" + formatPhoto(USER_FILE_PATH + newnm, mid_w, mid_h, "_mid", midshuiyin) + "|" + formatPhoto(USER_FILE_PATH + newnm, min_w, min_h, "_min", minshuiyin) + "|" + USER_FILE_PATH + newnm;//打水印并返回两个图片路径，一大一小
                }
                else
                {
                    returnstr = USER_FILE_PATH + newnm + "||";
                }

            }

            return returnstr;
        }

       

        
            /// <summary>
        /// 专供 HTML编辑器的 Flash  editorUpload.swf 上传用 
        /// </summary>
        public static string editFlashUploadFile(HttpRequest req, HttpPostedFile myFile)
        {

            string s = "{\"success\":false}";
            string returnstr = "";
            if (myFile != null)
            {
                //定义一些变量
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i);
                string filetype = nam.Substring(i + 1).ToLower();

                string folder = "/upload/editor/" + sys.getDateStr() + "/";
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folder)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folder));
                }

                //保存
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(folder + newnm));

                if ("jpeg".Equals(filetype) || "bmp".Equals(filetype) || "png".Equals(filetype) || "jpg".Equals(filetype) || "gif".Equals(filetype))
                {
                    string p1 = upload.formatPhoto(folder + newnm, 500, 1000, "_a", false, false);
                    string p3 = upload.formatPhoto(folder + newnm, 1000, 2000, "_b", false, false);

                }


                returnstr = folder + newnm;

                s = "{\"success\":true,\"path\":\"" + returnstr + "\"}";
            }

            return s;
        }



        //格式化，并返回格式化的图片路径
        public static Hashtable getFormatWidth_Height(int _w_max, int _h_max,int _w_img,int _h_img)
        {
            

            int _w_s = 0;
            int _h_s = 0;

            int _w = _w_img;
            int _h = _h_img;

            //如果上传较宽的图片，则按照比例缩小
            if (_w / _h >= _w_max / _h_max)
            {

                if (_w <= _w_max)//原图比需要的格式还小
                {
                    _w_s = _w;
                    _h_s = _h;
                }
                else
                {
                    _w_s = _w_max;
                    _h_s = _h * _w_max / _w;
                }

            }
            //如果上传较高的图片，则按照比例缩小
            else
            {
                if (_h <= _h_max)//原图比需要的格式还小
                {
                    _w_s = _w;
                    _h_s = _h;
                }
                else
                {

                    _h_s = _h_max;
                    _w_s = _w * _h_max / _h;
                }
            }


            //防止计算出为0的宽或高
            if (_w_s == 0)
            {
                _w_s = 1;

            }
            if (_h_s == 0)
            {
                _h_s = 1;
            }


            Hashtable ht = new Hashtable();
            ht.Add("width", _w_s);
            ht.Add("height", _h_s);

            return ht;
        }



        //格式化，并返回格式化的图片路径
        public static string formatPhoto(string web_path, int _w_max, int _h_max, string addstr, bool shuiyin)
        {
            return formatPhoto(web_path, _w_max, _h_max, addstr, shuiyin, false);
        }

        //格式化，并返回格式化的图片路径
        public static string formatPhoto(string web_path, int _w_max, int _h_max, string addstr, bool shuiyin, bool isFormatArea)
        {
            return formatPhoto(web_path, _w_max, _h_max, addstr, shuiyin, isFormatArea, "");
        }


        //格式化，并返回格式化的图片路径
        //watermark为水印信息
        public static string formatPhoto(string web_path, int _w_max, int _h_max, string addstr, bool shuiyin, bool isFormatArea, string shuiyin_path)
        {

            if (web_path.IndexOf(".gif") > -1)
            {
                return formatPhotoGIF(System.Web.HttpContext.Current,web_path, _w_max, _h_max, addstr, shuiyin, isFormatArea, shuiyin_path);

            }
            else
            {
                return formatPhotoJPG(System.Web.HttpContext.Current,web_path, _w_max, _h_max, addstr, shuiyin, isFormatArea, shuiyin_path);
            }
        }


        //格式化，并返回格式化的图片路径
        public static string eventFormatPhoto(HttpContext context, string web_path, int _w_max, int _h_max, string addstr, bool shuiyin, bool isFormatArea)
        {
            if (web_path.IndexOf(".gif") > -1)
            {
                return formatPhotoGIF(context,web_path, _w_max, _h_max, addstr, shuiyin, isFormatArea, "");

            }
            else
            {
                return formatPhotoJPG(context,web_path, _w_max, _h_max, addstr, shuiyin, isFormatArea, "");
            }
        }



        public static string formatPhotoJPG(HttpContext context,string web_path, int _w_max, int _h_max, string addstr, bool shuiyin, bool isFormatArea, string shuiyin_path)
        {            
            string pc_path = context.Server.MapPath(web_path);

            int i = pc_path.LastIndexOf("\\");
            string filename = pc_path.Substring(i);                                    //原图名称
            int j = filename.LastIndexOf(".");
            string filetype = filename.Substring(j);
            string filename_min = filename.Substring(1, j - 1) + addstr + filetype;     //新图名称


            System.Drawing.Image image = System.Drawing.Image.FromFile(pc_path);
            int _w = image.Width;
            int _h = image.Height;

            //找到生成制定大小的图片的 合适的宽高
            Hashtable ht = getFormatWidth_Height(_w_max, _h_max, image.Width, image.Height);
            int _w_s = int.Parse(ht["width"].ToString());
            int _h_s = int.Parse(ht["height"].ToString());


            System.Drawing.Image bitmap = null;
            //指定画布大小
            if (isFormatArea)
            {
                //指定为最大的区域，图片宽高比例不够的部分用空白填充
                bitmap = new System.Drawing.Bitmap(_w_max, _h_max);
            }
            else
            {
                //指定为宽和高都不超出最大区域的范围
                bitmap = new System.Drawing.Bitmap(_w_s, _h_s);

            }


            System.Drawing.Graphics gh = System.Drawing.Graphics.FromImage(bitmap);

            /*
            //设置高质量插值法  
            gh.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置质量,速度  
            gh.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并设置背景色
            gh.Clear(System.Drawing.ColorTranslator.FromHtml("#ffffff")); 
            */


            //设置高质量插值法  
            gh.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            gh.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            gh.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置质量,速度  
            gh.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            //清空画布并设置背景色
            gh.Clear(System.Drawing.ColorTranslator.FromHtml("#ffffff"));


            int to_x = 0;
            int to_y = 0;
            //在成预期的白色图片上填充一定比例的上传的图
            if (isFormatArea)
            {
                to_x = (_w_max - _w_s) / 2;
                to_y = (_h_max - _h_s) / 2;
            }
            //gh.DrawImage(image, new System.Drawing.Rectangle(0, 0, _w_s, _h_s), new System.Drawing.Rectangle(0, 0, _w, _h), System.Drawing.GraphicsUnit.Pixel);
            gh.DrawImage(image, new System.Drawing.Rectangle(to_x, to_y, _w_s, _h_s), new System.Drawing.Rectangle(0, 0, _w, _h), System.Drawing.GraphicsUnit.Pixel);


            gh.Dispose();
            //*******
            if (shuiyin)
            {
                System.Drawing.Image x_image_max = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(shuiyin_path));
                System.Drawing.Graphics g_max = System.Drawing.Graphics.FromImage(bitmap);

                if (false)
                {
                    //多个水印
                    int logo_width = x_image_max.Width;
                    int logo_height = x_image_max.Height;

                    int logo_x = 10;
                    int logo_y = 10;
                    int cell = bitmap.Width / logo_width;//Convert.ToInt32(bitmap.Width / ((logo_width) * 2));
                    int row = bitmap.Height / logo_height;//Convert.ToInt32( / (()));

                    for (int icell = 0; icell < cell; icell++)
                    {
                        for (int irow = 0; irow < row; irow++)
                        {
                            logo_x = icell * logo_width + ((bitmap.Width - (logo_width * cell)) / 2);
                            logo_y = irow * logo_height + ((bitmap.Height - (logo_height * row)) / 2);

                            if ((icell + irow) % 2 == 0)
                            {
                                g_max.DrawImage(x_image_max, new System.Drawing.Rectangle(logo_x, logo_y, x_image_max.Width, x_image_max.Height), 0, 0, x_image_max.Width, x_image_max.Height, System.Drawing.GraphicsUnit.Pixel);
                            }
                        }

                    }


                }
                else
                {
                    //中心点一个水印
                    g_max.DrawImage(x_image_max, new System.Drawing.Rectangle((bitmap.Width - x_image_max.Width) / 2, (bitmap.Height - x_image_max.Height) / 2, x_image_max.Width, x_image_max.Height), 0, 0, x_image_max.Width, x_image_max.Height, System.Drawing.GraphicsUnit.Pixel);
                }


            }
            //*******

            string newPath_min = context.Server.MapPath(web_path.Substring(0, web_path.LastIndexOf("/") + 1) + filename_min);
            // bitmap.Save(newPath_min);
            bitmap.Save(newPath_min, System.Drawing.Imaging.ImageFormat.Jpeg);
            bitmap.Dispose();
            image.Dispose();


            int k = web_path.LastIndexOf("/");
            string left = web_path.Substring(0, k + 1);


            return left + filename_min;

        }


        public static string formatPhotoGIF(HttpContext context,string web_path, int _w_max, int _h_max, string addstr, bool shuiyin, bool isFormatArea, string shuiyin_path)
        {


            string pc_path = context.Server.MapPath(web_path);

            int i = pc_path.LastIndexOf("\\");
            string filename = pc_path.Substring(i);                                    //原图名称
            int j = filename.LastIndexOf(".");
            string filetype = filename.Substring(j);
            string filename_min = filename.Substring(1, j - 1) + addstr + filetype;     //新图名称
            string newPath_min = System.Web.HttpContext.Current.Server.MapPath(web_path.Substring(0, web_path.LastIndexOf("/") + 1) + filename_min);


            //获取原始图像信息
            System.Drawing.Image image = System.Drawing.Image.FromFile(pc_path);
            int _w = image.Width;
            int _h = image.Height;

            //找到生成制定大小的图片的 合适的宽高
            Hashtable ht = getFormatWidth_Height(_w_max, _h_max, image.Width, image.Height);
            int _w_s = int.Parse(ht["width"].ToString());
            int _h_s = int.Parse(ht["height"].ToString());


            ///////////////////////////////////////////////////////////////////////////
            Image gif = new Bitmap(_w_s, _h_s);
            Image frame = new Bitmap(_w_s, _h_s);
            Image res = Image.FromFile(pc_path);
            Graphics g = Graphics.FromImage(gif);
            Rectangle rg = new Rectangle(0, 0, _w_s, _h_s);
            Graphics gFrame = Graphics.FromImage(frame);

            foreach (Guid gd in res.FrameDimensionsList)
            {
                FrameDimension fd = new FrameDimension(gd);

                //因为是缩小GIF文件所以这里要设置为Time，如果是TIFF这里要设置为PAGE，因为GIF以时间分割，TIFF为页分割 
                FrameDimension f = FrameDimension.Time;
                int count = res.GetFrameCount(fd);
                ImageCodecInfo codecInfo = GetEncoder(ImageFormat.Gif);
                
                
                System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.SaveFlag;
                EncoderParameters eps = null;

                for (int n = 0; n < count; n++)
                {
                    res.SelectActiveFrame(f, n);
                    if (0 == n)
                    {

                        g.DrawImage(res, rg);

                        eps = new EncoderParameters(1);

                        //第一帧需要设置为MultiFrame 

                        eps.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.MultiFrame);
                        bindProperty(res, gif);
                       // try
                       // {

                        gif.Save(newPath_min, codecInfo, eps);
                           
                        //}
                        //catch (Exception ex)
                       // {
                        //    Console.WriteLine(ex.StackTrace);
                       // }
                    }
                    else
                    {

                        gFrame.DrawImage(res, rg);

                        eps = new EncoderParameters(1);

                        //如果是GIF这里设置为FrameDimensionTime，如果为TIFF则设置为FrameDimensionPage 

                        eps.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.FrameDimensionTime);

                        bindProperty(res, frame);
                        try
                        {
                            gif.SaveAdd(frame, eps);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

                eps = new EncoderParameters(1);
                eps.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.Flush);
                try
                {
                    gif.SaveAdd(eps);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }

            //返回图片文件路径
            int k = web_path.LastIndexOf("/");
            string left = web_path.Substring(0, k + 1);


            return left + filename_min;
    

    }

        private static void bindProperty(Image a, Image b)
        {

            //这个东西就是每一帧所拥有的属性，可以用GetPropertyItem方法取得这里用为完全复制原有属性所以直接赋值了 

            //顺便说一下这个属性里包含每帧间隔的秒数和透明背景调色板等设置，这里具体那个值对应那个属性大家自己在msdn搜索GetPropertyItem方法说明就有了 

            for (int i = 0; i < a.PropertyItems.Length; i++)
            {
                b.SetPropertyItem(a.PropertyItems[i]);
            }
        }


        private static ImageCodecInfo GetEncoder(ImageFormat format)
         {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        //
        public static string cutPhoto(string web_path, int x0, int x1,int y0,int y1, string addstr, bool shuiyin)
        {
            string pc_path = System.Web.HttpContext.Current.Server.MapPath(web_path);

            int i = pc_path.LastIndexOf("\\");
            string filename = pc_path.Substring(i);                                    //原图名称
            int j = filename.LastIndexOf(".");
            string filetype = filename.Substring(j);
            string filename_min = filename.Substring(1, j - 1) + addstr + filetype;     //新图名称
            
            
            System.Drawing.Image image = System.Drawing.Image.FromFile(pc_path);

            //指定画布大小
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(x1-x0, y1-y0);
            System.Drawing.Graphics gh = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法  
            gh.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            gh.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            gh.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置质量,速度  
            gh.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并设置背景色
            gh.Clear(System.Drawing.ColorTranslator.FromHtml("#ffffff"));


            int to_x = 0;
            int to_y = 0;

            //gh.DrawImage(image, new System.Drawing.Rectangle(0, 0, _w_s, _h_s), new System.Drawing.Rectangle(0, 0, _w, _h), System.Drawing.GraphicsUnit.Pixel);
            gh.DrawImage(image, new System.Drawing.Rectangle(to_x, to_y, x1-x0,y1-y0), new System.Drawing.Rectangle(x0, y0, x1-x0,y1-y0), System.Drawing.GraphicsUnit.Pixel);


            gh.Dispose();
            //*******
            string newPath_min = System.Web.HttpContext.Current.Server.MapPath(web_path.Substring(0, web_path.LastIndexOf("/") + 1) + filename_min);
            bitmap.Save(newPath_min);
            bitmap.Dispose();
            image.Dispose();


            int k = web_path.LastIndexOf("/");
            string left = web_path.Substring(0, k + 1);
			return left + filename_min;

        }

        /// <summary>
        /// 通用的图片裁切与上传一次完成的接口
        /// </summary>
        public static string uploadAndCutPhoto(HttpRequest req, HttpPostedFile myFile)
        {
            string s = "{\"success\":false}";
            string returnstr = "";
            string cutsize = req["cutsize"];
            string cutname = req["cutname"];
            string folder = req["folder"];
            if (myFile != null && cutsize != null)
            {
                string nam = myFile.FileName;
                int i = nam.LastIndexOf(".");
                string newnm = sys.getRandomStr() + nam.Substring(i).ToLower();
                string filetype = nam.Substring(i + 1).ToLower();
                string path = "/upload/cutphoto/" + folder + "/" + sys.getDateStr() + "/";
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));
                }
                //保存原图
                myFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path + newnm));

                /*
                if (!string.IsNullOrEmpty(cutname))
                {
                    returnstr = UtilBO.getServerPath(req) + path + newnm.Replace(".", "_cut.");
                }
                else
                {
                    returnstr = UtilBO.getServerPath(req) + path + newnm;
                }*/

                returnstr = path + newnm;//UtilBO.getServerPath(req) + path + newnm;

                //开始裁切
                string x0 = req["x0"];
                string x1 = req["x1"];
                string y0 = req["y0"];
                string y1 = req["y1"];
                string imageWidth = req["imageWidth"];
                string imageHeight = req["imageHeight"];
                string cutPath = cutPhoto(path + newnm, int.Parse(x0), int.Parse(x1), int.Parse(y0), int.Parse(y1), "_cut", false);

                string[] cutsizes = cutsize.Split(';');
                string[] cutnames = cutname.Split(';');
                for (int v = 0; v < cutsizes.Length; v++)
                {
                    string[] formatsize = cutsizes[v].ToString().Split('*');
                    string appendName = "_" + cutsizes[v].ToString().Replace("*", "_");
                    if (cutnames.Length == cutsizes.Length)
                    {
                        appendName = "_" + cutnames[v];
                    }
                    formatPhoto(cutPath, int.Parse(formatsize[0].ToString()), int.Parse(formatsize[1].ToString()), appendName, false, false);
                }
                s = "{\"success\":true,\"path\":\"" + returnstr + "\"}";

            }
            return s;
        }
    }
}
