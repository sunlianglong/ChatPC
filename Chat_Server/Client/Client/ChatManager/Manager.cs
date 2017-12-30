using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    /// <summary>
    ///     管理基类
    /// </summary>
    public class Manager : IManager
    {
        protected List<IMetaData> Collection = new List<IMetaData>();

        #region IManager 成员
        /// <summary>
        ///     防止重复添加方法
        /// </summary>
        /// <param name="MetaData" type="UM_SERVER.IMetaData">
        ///     <para>
        ///         将要添加的IMetaData对象
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true ， 则表示已经存在了
        /// </returns>
        public bool Equals(IMetaData MetaData)
        {
            foreach (IMetaData member in Collection)
            {
                if (MetaData.Key == member.Key)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     防止重复添加方法
        /// </summary>
        /// <param name="Key" type="String">
        ///     <para>
        ///         将要添加IMetaData对象的KEY
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true ， 则表示已经存在了
        /// </returns>
        public bool Equals(string Key)
        {
            foreach (IMetaData member in Collection)
            {
                if (member.Key == Key)
                {
                    return true;
                }
            }
            return false;
        }

        public List<IMetaData> GetMember()
        {
            return this.Collection;
        }

        #endregion

        #region ICollectionManager 成员

        /// <summary>
        ///     向当前集合中添加成员
        ///    [自动检测是否要加入的成员已经存在]
        /// </summary>
        /// <param name="CollectionMember" type="UM_SERVER.IMetaData">
        ///     <para>
        ///         要添加的成员
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true ， 则表示添加成功
        /// </returns>
        public bool Add(IMetaData CollectionMember)
        {
            //如果不重复
            if (!this.Equals(CollectionMember))
            {
                this.Collection.Add(CollectionMember);
                return true;
            }
            return false;
        }

        /// <summary>
        ///     从成员集合中移除指定成员
        ///    [自动检测是否要移除的成员已经存在]
        /// </summary>
        /// <param name="CollectionMember" type="UM_SERVER.IMetaData">
        ///     <para>
        ///         要移除的成员
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true ， 则表示移除成功
        /// </returns>
        public bool Remove(IMetaData CollectionMember)
        {
            //如果存在
            if (this.Equals(CollectionMember))
            {
                this.Collection.Remove(CollectionMember);
                return true;
            }
            return false;
        }

        /// <summary>
        ///     根据指定唯一标示，从成员集合中移除
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///         唯一标示
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回true ， 则表示移除成功
        /// </returns>
        public bool Remove(string Key)
        {
            foreach (IMetaData member in Collection)
            {
                if (member.Key == Key)
                {
                    Collection.Remove(member);
                    if (member.GetType() == typeof(IListener))
                    {
                        ((IListener) member).Stop();
                    }
               
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     通过指定唯一标示, 得到成员
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///         唯一标示
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回指定成员
        /// </returns>
        public IMetaData GetMemberByKey(string Key)
        {
            foreach (IMetaData member in Collection)
            {
                if (member.Key == Key)
                {
                    return member;
                }
            }

            return null;
        }

        /// <summary>
        ///     得到当前成员集合数量
        /// </summary>
        /// <returns>
        ///     返回当前成员集合数量
        /// </returns>
        public int Count()
        {
            return this.Collection.Count;
        }

        #endregion

        #region IMetaData 成员

        private String key;
        /// <summary>
        ///     唯一标示
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        #endregion
    }
}
