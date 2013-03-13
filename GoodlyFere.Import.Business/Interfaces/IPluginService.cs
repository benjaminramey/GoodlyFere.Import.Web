#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Business.Interfaces
{
    public interface IPluginService : IGenericService<IPlugin>
    {
        #region Public Methods
        
        bool Reload(IPlugin plugin);

        #endregion
    }
}