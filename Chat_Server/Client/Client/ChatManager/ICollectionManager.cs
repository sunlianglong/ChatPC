using System;

namespace Client
{
    public interface ICollectionManager : IMetaData
    {
        /// <summary>
        ///     �򼯺�����ӳ�Ա����
        /// </summary>
        /// <param name="CollectionMember" type="Hi_Baidu.IMetaData">
        ///     <para>
        ///         Ҫ��ӵĳ�Ա
        ///     </para>
        /// </param>
        /// <returns>
        ///     ����true , ���ʾ��ӳɹ�
        /// </returns>
        bool Add(IMetaData CollectionMember);
        /// <summary>
        ///     �Ƴ�������ָ����Ա����
        /// </summary>
        /// <param name="CollectionMember" type="Hi_Baidu.IMetaData">
        ///     <para>
        ///         ָ����Ա
        ///     </para>
        /// </param>
        /// <returns>
        ///     ����true , ���ʾ�Ƴ��ɹ�
        /// </returns>
        bool Remove(IMetaData CollectionMember);
        /// <summary>
        ///     �Ƴ�������ָ����Ա����
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///         ָ����ԱKey
        ///     </para>
        /// </param>
        /// <returns>
        ///     ����true , ���ʾ�Ƴ��ɹ�
        /// </returns>
        bool Remove(String Key);
        /// <summary>
        ///     ͨ��ָ��Key�ҵ������еĶ�Ӧ��Ա
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///         ָ��Key
        ///     </para>
        /// </param>
        /// <returns>
        ///     �����ҵ��Ķ�Ӧ��Ա
        /// </returns>
        IMetaData GetMemberByKey(String Key);
        /// <summary>
        ///     �õ���Ա����
        /// </summary>
        /// <returns>
        ///     ���ظü����еĳ�Ա����
        /// </returns>
        int Count();
    }
}