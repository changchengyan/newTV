/// <summary>
/// admin��SysAdminVO ֵ����,����ʱ��:2013/11/6 14:08:45(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///����������Աֵ����
/// </summary>

namespace Redsz.VO
{

	public class SysAdminVO
	{
        private int _id; //      int,length:10,default:
        private int _topsize; //����ֵ      int,length:10,default:(0)
        private int _roleid; //������ɫid      int,length:10,default:(0)
        private string _username; //�û���      nvarchar,length:50,default:
        private string _password; //����      nvarchar,length:50,default:
        private string _zname; //��ʵ����      nvarchar,length:50,default:
        private string _datetime; //ע������      datetime,length:23,default:
        private string _lastdatetime; //����¼ʱ��      datetime,length:23,default:
        private int _loginsize; //��¼����      int,length:10,default:(0)
        private int _ispass; //�Ƿ����ͨ��      int,length:10,default:(1)
        private int _org_id; //��֯������ʶ      int,length:10,default:(0)
        private string _org_code; //��֯��������      nvarchar,length:50,default:
        private string _org_name; //��֯��������      nvarchar,length:50,default:
        private string _email; //����      nvarchar,length:50,default:
        private string _tel; //�绰      nvarchar,length:50,default:
        private string _n_tel; //���õ绰      nvarchar,length:50,default:
        private string _mob; //�ֻ�      nvarchar,length:50,default:
        private string _msn; //MSN��      nvarchar,length:50,default:
        private string _address; //��ϵ��ַ      nvarchar,length:50,default:
        private string _info; //������Ϣ      ntext,length:1073741823,default:


        /// <summary>
        ///      int,length:10,default:
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///����ֵ      int,length:10,default:(0)
        /// </summary>
        public int topsize
        {
          get{return _topsize;}
          set{_topsize = value;}
        }

        /// <summary>
        ///������ɫid      int,length:10,default:(0)
        /// </summary>
        public int roleid
        {
          get{return _roleid;}
          set{_roleid = value;}
        }

        /// <summary>
        ///�û���      nvarchar,length:50,default:
        /// </summary>
        public string username
        {
          get{return _username;}
          set{_username = value;}
        }

        /// <summary>
        ///����      nvarchar,length:50,default:
        /// </summary>
        public string password
        {
          get{return _password;}
          set{_password = value;}
        }

        /// <summary>
        ///��ʵ����      nvarchar,length:50,default:
        /// </summary>
        public string zname
        {
          get{return _zname;}
          set{_zname = value;}
        }

        /// <summary>
        ///ע������      datetime,length:23,default:
        /// </summary>
        public string datetime
        {
          get{return _datetime;}
          set{_datetime = value;}
        }

        /// <summary>
        ///����¼ʱ��      datetime,length:23,default:
        /// </summary>
        public string lastdatetime
        {
          get{return _lastdatetime;}
          set{_lastdatetime = value;}
        }

        /// <summary>
        ///��¼����      int,length:10,default:(0)
        /// </summary>
        public int loginsize
        {
          get{return _loginsize;}
          set{_loginsize = value;}
        }

        /// <summary>
        ///�Ƿ����ͨ��      int,length:10,default:(1)
        /// </summary>
        public int ispass
        {
          get{return _ispass;}
          set{_ispass = value;}
        }

        /// <summary>
        ///��֯������ʶ      int,length:10,default:(0)
        /// </summary>
        public int org_id
        {
          get{return _org_id;}
          set{_org_id = value;}
        }

        /// <summary>
        ///��֯��������      nvarchar,length:50,default:
        /// </summary>
        public string org_code
        {
          get{return _org_code;}
          set{_org_code = value;}
        }

        /// <summary>
        ///��֯��������      nvarchar,length:50,default:
        /// </summary>
        public string org_name
        {
          get{return _org_name;}
          set{_org_name = value;}
        }

        /// <summary>
        ///����      nvarchar,length:50,default:
        /// </summary>
        public string email
        {
          get{return _email;}
          set{_email = value;}
        }

        /// <summary>
        ///�绰      nvarchar,length:50,default:
        /// </summary>
        public string tel
        {
          get{return _tel;}
          set{_tel = value;}
        }

        /// <summary>
        ///���õ绰      nvarchar,length:50,default:
        /// </summary>
        public string n_tel
        {
          get{return _n_tel;}
          set{_n_tel = value;}
        }

        /// <summary>
        ///�ֻ�      nvarchar,length:50,default:
        /// </summary>
        public string mob
        {
          get{return _mob;}
          set{_mob = value;}
        }

        /// <summary>
        ///MSN��      nvarchar,length:50,default:
        /// </summary>
        public string msn
        {
          get{return _msn;}
          set{_msn = value;}
        }

        /// <summary>
        ///��ϵ��ַ      nvarchar,length:50,default:
        /// </summary>
        public string address
        {
          get{return _address;}
          set{_address = value;}
        }

        /// <summary>
        ///������Ϣ      ntext,length:1073741823,default:
        /// </summary>
        public string info
        {
          get{return _info;}
          set{_info = value;}
        }
	}

}