using System;
using System.Collections.Generic;

namespace Client
{
    public interface IManager : ICollectionManager
    {
        bool Equals(IMetaData MetaData);
        bool Equals(String Key);
        List<IMetaData> GetMember();
    }
}