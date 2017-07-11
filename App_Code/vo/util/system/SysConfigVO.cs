/// <summary>
/// sys_config��SysConfigVO ֵ����,����ʱ��:2015/10/12 19:49:51(���ļ��ɿ���Զ����� v2.0)
/// ���ں��� www.redsz.com QQ3968666 email:leijiangbo@163.com
///������ֵ����
/// </summary>

namespace Redsz.VO
{

	public class SysConfigVO
	{
        private int _id; //      int,length:4,
        private string _webtitle; //��վ����      nvarchar,length:400,
        private string _webemail; //ϵͳ����Ա����      nvarchar,length:100,
        private string _webcopyright; //��ȨHTML      nvarchar,length:2000,
        private string _webtongji; //��վ����ʽ      nvarchar,length:400,
        private string _mail_from; //�����ַ      nvarchar,length:100,
        private string _mail_smtp; //SMTP������      nvarchar,length:100,
        private string _mail_user; //SMTP����      nvarchar,length:100,
        private string _mail_password; //SMTP����      nvarchar,length:100,
        private string _weixin_name; //      nvarchar,length:100,
        private string _weixin_url; //      nvarchar,length:100,
        private string _weixin_token; //      nvarchar,length:100,
        private string _weixin_appid; //      nvarchar,length:100,
        private string _weixin_appsecret; //      nvarchar,length:100,
        private string _weixin_access_token; //��Ȩ��ȡ�� access_token ����Ҫʱ����΢��API������£�ʧЧʱ��Ϊ7200��      nvarchar,length:400,
        private string _weixin_access_token_time; //��Ȩ��ȡ�� access_token ����Ч��ֹʱ��      datetime,length:8,
        private string _weixin_openid; //�ٷ�΢�ŵ��û�openid      nvarchar,length:400,
        private string _sina_client_id; //      nvarchar,length:100,
        private string _sina_client_secret; //      nvarchar,length:100,
        private string _qqweibo_app_key; //      nvarchar,length:100,
        private string _qqweibo_app_secret; //      nvarchar,length:100,
        private int _isstatic; //�Ƿ�����̬ҳ      int,length:4,
        private int _page_count_web; //�б�ÿҳ��¼������PC��      int,length:4,
        private int _page_count_mobile; //�б�ÿҳ��¼�������ֻ���      int,length:4,
        private string _website_keyword; //      nvarchar,length:1000,
        private string _website_desc; //      nvarchar,length:1000,
        private string _weblogo_pic; //      nvarchar,length:510,
        private string _webservice_pic; //      nvarchar,length:510,
        private string _webaboutus; //      nvarchar,length:2000,
        private string _version; //      nvarchar,length:100,
        private int _invite; //������ģʽ��0��ʾ�رգ�1��ʾ������ע���û�ʱ��Ҫ invitecode ���е� code      int,length:4,
        private int _space_id; //      int,length:4,


        /// <summary>
        ///      int,length:4
        /// </summary>
        public int id
        {
          get{return _id;}
          set{_id = value;}
        }

        /// <summary>
        ///��վ����      nvarchar,length:400
        /// </summary>
        public string webtitle
        {
          get{return _webtitle;}
          set{_webtitle = value;}
        }

        /// <summary>
        ///ϵͳ����Ա����      nvarchar,length:100
        /// </summary>
        public string webemail
        {
          get{return _webemail;}
          set{_webemail = value;}
        }

        /// <summary>
        ///��ȨHTML      nvarchar,length:2000
        /// </summary>
        public string webcopyright
        {
          get{return _webcopyright;}
          set{_webcopyright = value;}
        }

        /// <summary>
        ///��վ����ʽ      nvarchar,length:400
        /// </summary>
        public string webtongji
        {
          get{return _webtongji;}
          set{_webtongji = value;}
        }

        /// <summary>
        ///�����ַ      nvarchar,length:100
        /// </summary>
        public string mail_from
        {
          get{return _mail_from;}
          set{_mail_from = value;}
        }

        /// <summary>
        ///SMTP������      nvarchar,length:100
        /// </summary>
        public string mail_smtp
        {
          get{return _mail_smtp;}
          set{_mail_smtp = value;}
        }

        /// <summary>
        ///SMTP����      nvarchar,length:100
        /// </summary>
        public string mail_user
        {
          get{return _mail_user;}
          set{_mail_user = value;}
        }

        /// <summary>
        ///SMTP����      nvarchar,length:100
        /// </summary>
        public string mail_password
        {
          get{return _mail_password;}
          set{_mail_password = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string weixin_name
        {
          get{return _weixin_name;}
          set{_weixin_name = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string weixin_url
        {
          get{return _weixin_url;}
          set{_weixin_url = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string weixin_token
        {
          get{return _weixin_token;}
          set{_weixin_token = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string weixin_appid
        {
          get{return _weixin_appid;}
          set{_weixin_appid = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string weixin_appsecret
        {
          get{return _weixin_appsecret;}
          set{_weixin_appsecret = value;}
        }

        /// <summary>
        ///��Ȩ��ȡ�� access_token ����Ҫʱ����΢��API������£�ʧЧʱ��Ϊ7200��      nvarchar,length:400
        /// </summary>
        public string weixin_access_token
        {
          get{return _weixin_access_token;}
          set{_weixin_access_token = value;}
        }

        /// <summary>
        ///��Ȩ��ȡ�� access_token ����Ч��ֹʱ��      datetime,length:8
        /// </summary>
        public string weixin_access_token_time
        {
          get{return _weixin_access_token_time;}
          set{_weixin_access_token_time = value;}
        }

        /// <summary>
        ///�ٷ�΢�ŵ��û�openid      nvarchar,length:400
        /// </summary>
        public string weixin_openid
        {
          get{return _weixin_openid;}
          set{_weixin_openid = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string sina_client_id
        {
          get{return _sina_client_id;}
          set{_sina_client_id = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string sina_client_secret
        {
          get{return _sina_client_secret;}
          set{_sina_client_secret = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string qqweibo_app_key
        {
          get{return _qqweibo_app_key;}
          set{_qqweibo_app_key = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string qqweibo_app_secret
        {
          get{return _qqweibo_app_secret;}
          set{_qqweibo_app_secret = value;}
        }

        /// <summary>
        ///�Ƿ�����̬ҳ      int,length:4
        /// </summary>
        public int isstatic
        {
          get{return _isstatic;}
          set{_isstatic = value;}
        }

        /// <summary>
        ///�б�ÿҳ��¼������PC��      int,length:4
        /// </summary>
        public int page_count_web
        {
          get{return _page_count_web;}
          set{_page_count_web = value;}
        }

        /// <summary>
        ///�б�ÿҳ��¼�������ֻ���      int,length:4
        /// </summary>
        public int page_count_mobile
        {
          get{return _page_count_mobile;}
          set{_page_count_mobile = value;}
        }

        /// <summary>
        ///      nvarchar,length:1000
        /// </summary>
        public string website_keyword
        {
          get{return _website_keyword;}
          set{_website_keyword = value;}
        }

        /// <summary>
        ///      nvarchar,length:1000
        /// </summary>
        public string website_desc
        {
          get{return _website_desc;}
          set{_website_desc = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string weblogo_pic
        {
          get{return _weblogo_pic;}
          set{_weblogo_pic = value;}
        }

        /// <summary>
        ///      nvarchar,length:510
        /// </summary>
        public string webservice_pic
        {
          get{return _webservice_pic;}
          set{_webservice_pic = value;}
        }

        /// <summary>
        ///      nvarchar,length:2000
        /// </summary>
        public string webaboutus
        {
          get{return _webaboutus;}
          set{_webaboutus = value;}
        }

        /// <summary>
        ///      nvarchar,length:100
        /// </summary>
        public string version
        {
          get{return _version;}
          set{_version = value;}
        }

        /// <summary>
        ///������ģʽ��0��ʾ�رգ�1��ʾ������ע���û�ʱ��Ҫ invitecode ���е� code      int,length:4
        /// </summary>
        public int invite
        {
          get{return _invite;}
          set{_invite = value;}
        }

        /// <summary>
        ///      int,length:4
        /// </summary>
        public int space_id
        {
          get{return _space_id;}
          set{_space_id = value;}
        }
	}

}