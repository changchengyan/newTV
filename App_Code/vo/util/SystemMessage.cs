using System.Collections.Generic;
/// <summary>
/// ϵͳ��Ϣ����
/// </summary>
namespace Redsz.VO
{

    public class SystemMessage
    {
        private bool _success;//�ɹ���ʶ
        private List<string> _massge;//��ʾ��Ϣ

        /// <summary>
        ///  ��Ϣ�б�
        /// </summary>
        public List<string> massge
        {
            get { return _massge; }
            set { _massge = value; }
        }

        /// <summary>
        /// �ɹ���ʶ
        /// </summary>
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }
	}

}