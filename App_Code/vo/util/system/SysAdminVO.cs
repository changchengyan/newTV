/// <summary>
/// admin表SysAdminVO 值对象,创建时间:2013/11/6 14:08:45(此文件由框架自动生成 v2.0)
/// 深圳红网 www.redsz.com QQ3968666 email:leijiangbo@163.com
///描述：管理员值对象
/// </summary>

namespace Redsz.VO
{

	public class SysAdminVO
	{
        private int _id; //      int,length:10,default:
        private int _topsize; //排序值      int,length:10,default:(0)
        private int _roleid; //关联角色id      int,length:10,default:(0)
        private string _username; //用户明      nvarchar,length:50,default:
        private string _password; //密码      nvarchar,length:50,default:
        private string _zname; //真实姓名      nvarchar,length:50,default:
        private string _datetime; //注册日期      datetime,length:23,default:
        private string _lastdatetime; //最后登录时间      datetime,length:23,default:
        private int _loginsize; //登录次数      int,length:10,default:(0)
        private int _ispass; //是否审核通过      int,length:10,default:(1)
        private int _org_id; //组织机构标识      int,length:10,default:(0)
        private string _org_code; //组织机构编码      nvarchar,length:50,default:
        private string _org_name; //组织机构名称      nvarchar,length:50,default:
        private string _email; //邮箱      nvarchar,length:50,default:
        private string _tel; //电话      nvarchar,length:50,default:
        private string _n_tel; //备用电话      nvarchar,length:50,default:
        private string _mob; //手机      nvarchar,length:50,default:
        private string _msn; //MSN号      nvarchar,length:50,default:
        private string _address; //联系地址      nvarchar,length:50,default:
        private string _info; //其他信息      ntext,length:1073741823,default:


        /// <summary>
        ///      int,length:10,default:
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///排序值      int,length:10,default:(0)
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }

        /// <summary>
        ///关联角色id      int,length:10,default:(0)
        /// </summary>
        public int roleid
        {
          get{return _roleid;}
          set{_roleid = value;}
        }

        /// <summary>
        ///用户明      nvarchar,length:50,default:
        /// </summary>
        public string username
        {
          get{return _username;}
          set{_username = value;}
        }

        /// <summary>
        ///密码      nvarchar,length:50,default:
        /// </summary>
        public string password
        {
          get{return _password;}
          set{_password = value;}
        }

        /// <summary>
        ///真实姓名      nvarchar,length:50,default:
        /// </summary>
        public string zname
        {
          get{return _zname;}
          set{_zname = value;}
        }

        /// <summary>
        ///注册日期      datetime,length:23,default:
        /// </summary>
        public string datetime
        {
          get{return _datetime;}
          set{_datetime = value;}
        }

        /// <summary>
        ///最后登录时间      datetime,length:23,default:
        /// </summary>
        public string lastdatetime
        {
          get{return _lastdatetime;}
          set{_lastdatetime = value;}
        }

        /// <summary>
        ///登录次数      int,length:10,default:(0)
        /// </summary>
        public int loginsize
        {
          get{return _loginsize;}
          set{_loginsize = value;}
        }

        /// <summary>
        ///是否审核通过      int,length:10,default:(1)
        /// </summary>
        public int ispass
        {
          get{return _ispass;}
          set{_ispass = value;}
        }

        /// <summary>
        ///组织机构标识      int,length:10,default:(0)
        /// </summary>
        public int org_id
        {
          get{return _org_id;}
          set{_org_id = value;}
        }

        /// <summary>
        ///组织机构编码      nvarchar,length:50,default:
        /// </summary>
        public string org_code
        {
          get{return _org_code;}
          set{_org_code = value;}
        }

        /// <summary>
        ///组织机构名称      nvarchar,length:50,default:
        /// </summary>
        public string org_name
        {
          get{return _org_name;}
          set{_org_name = value;}
        }

        /// <summary>
        ///邮箱      nvarchar,length:50,default:
        /// </summary>
        public string email
        {
          get{return _email;}
          set{_email = value;}
        }

        /// <summary>
        ///电话      nvarchar,length:50,default:
        /// </summary>
        public string tel
        {
          get{return _tel;}
          set{_tel = value;}
        }

        /// <summary>
        ///备用电话      nvarchar,length:50,default:
        /// </summary>
        public string n_tel
        {
          get{return _n_tel;}
          set{_n_tel = value;}
        }

        /// <summary>
        ///手机      nvarchar,length:50,default:
        /// </summary>
        public string mob
        {
          get{return _mob;}
          set{_mob = value;}
        }

        /// <summary>
        ///MSN号      nvarchar,length:50,default:
        /// </summary>
        public string msn
        {
          get{return _msn;}
          set{_msn = value;}
        }

        /// <summary>
        ///联系地址      nvarchar,length:50,default:
        /// </summary>
        public string address
        {
          get{return _address;}
          set{_address = value;}
        }

        /// <summary>
        ///其他信息      ntext,length:1073741823,default:
        /// </summary>
        public string info
        {
          get{return _info;}
          set{_info = value;}
        }
	}

}