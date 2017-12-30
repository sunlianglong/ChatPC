using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Client
{
    public interface IPopWindowAction : IMetaData
    {
        /// <summary>
        ///     从弹出窗口集合中移除本窗口
        /// </summary>
        void DisposeToManager();
    }
}
