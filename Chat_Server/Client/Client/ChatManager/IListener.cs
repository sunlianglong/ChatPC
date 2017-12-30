using System.Threading;

namespace Client
{
    public interface IListener : IMetaData
    {
        /// <summary>
        ///     开始监听方法
        /// </summary>
        /// <returns>
        ///     返回true, 则监听成功
        /// </returns>
        bool Start();
        /// <summary>
        ///     停止监听方法
        /// </summary>
        /// <returns>
        ///     返回true, 则停止成功
        /// </returns>
        bool Stop();
        /// <summary>
        ///     更改监听状态
        /// </summary>
        /// <param name="State" type="bool">
        ///     <para>
        ///         监听状态
        ///     </para>
        /// </param>
        void ChanageState(bool State);
        /// <summary>
        ///     初始化方法
        /// </summary>
        void Init();
        /// <summary>
        ///     结束方法
        /// </summary>
        void Dispose();
        /// <summary>
        ///     得到当前监听线程
        /// </summary>
        /// <returns>
        ///     返回当前监听线程
        /// </returns>
        Thread GetCurrentThread();
    }
}