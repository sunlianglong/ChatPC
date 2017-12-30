using System.Threading;

namespace Client
{
    public interface IListener : IMetaData
    {
        /// <summary>
        ///     ��ʼ��������
        /// </summary>
        /// <returns>
        ///     ����true, ������ɹ�
        /// </returns>
        bool Start();
        /// <summary>
        ///     ֹͣ��������
        /// </summary>
        /// <returns>
        ///     ����true, ��ֹͣ�ɹ�
        /// </returns>
        bool Stop();
        /// <summary>
        ///     ���ļ���״̬
        /// </summary>
        /// <param name="State" type="bool">
        ///     <para>
        ///         ����״̬
        ///     </para>
        /// </param>
        void ChanageState(bool State);
        /// <summary>
        ///     ��ʼ������
        /// </summary>
        void Init();
        /// <summary>
        ///     ��������
        /// </summary>
        void Dispose();
        /// <summary>
        ///     �õ���ǰ�����߳�
        /// </summary>
        /// <returns>
        ///     ���ص�ǰ�����߳�
        /// </returns>
        Thread GetCurrentThread();
    }
}