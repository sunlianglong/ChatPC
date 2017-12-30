using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Client
{
    /// <summary>
    ///     窗口管理器
    /// </summary>
    class PopWindowManager : Manager
    {
        /// <summary>
        ///     激活具有指定KEY的窗体
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///         窗体的唯一标示
        ///     </para>
        /// </param>
        /// <returns>
        ///     返回 false, 标示窗体不存在,或者窗体激活失败
        /// </returns>
        public bool ActiveWindow(String Key)
        {
            IMetaData iMetaData = this.GetMemberByKey(Key);
            if (iMetaData != null)
            {
                return ((Window)iMetaData).Activate();
            }
            return false;
        }

        /// <summary>
        ///     从窗口集合中移除并销毁具有指定KEY的窗体
        /// </summary>
        /// <param name="Key" type="string">
        ///     <para>
        ///          窗体的唯一标示
        ///     </para>
        /// </param>
        public void Dispose(String Key)
        {
            this.Remove(Key);
        }  
    }
}
