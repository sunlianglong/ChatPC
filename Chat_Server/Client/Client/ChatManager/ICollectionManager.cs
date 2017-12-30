using System;

namespace Client
{
    public interface ICollectionManager : IMetaData
    {
        /// <summary>
        ///     向集合中添加成员方法
        /// </summary>
        /// <param name="CollectionMember" type="Hi_Baidu.IMetaData">
        ///     <para>
        ///         要添加的成员
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true , 则表示添加成功
        /// </returns>
        bool Add(IMetaData CollectionMember);
        /// <summary>
        ///     移除集合中指定成员方法
        /// </summary>
        /// <param name="CollectionMember" type="Hi_Baidu.IMetaData">
        ///     <para>
        ///         指定成员
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true , 则表示移除成功
        /// </returns>
        bool Remove(IMetaData CollectionMember);
        /// <summary>
        ///     移除集合中指定成员方法
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///         指定成员Key
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true , 则表示移除成功
        /// </returns>
        bool Remove(String Key);
        /// <summary>
        ///     通过指定Key找到集合中的对应成员
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///         指定Key
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回找到的对应成员
        /// </returns>
        IMetaData GetMemberByKey(String Key);
        /// <summary>
        ///     得到成员数量
        /// </summary>
        /// <returns>
        ///     返回该集合中的成员数量
        /// </returns>
        int Count();
    }
}