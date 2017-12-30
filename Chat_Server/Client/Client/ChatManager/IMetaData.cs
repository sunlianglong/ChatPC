using System;

namespace Client
{
    /// <summary>
    ///     元数据接口
    /// </summary>
    public interface IMetaData
    {
        /// <summary>
        ///     唯一标示
        /// </summary>
        String Key
        {
            get; set;
        }
    }
}