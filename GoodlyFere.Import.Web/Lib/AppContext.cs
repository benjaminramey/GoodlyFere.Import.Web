#region Usings

using System;
using System.Linq;
using Autofac;

#endregion

namespace GoodlyFere.Import.Web.Lib
{
    public static class AppContext
    {
        #region Public Properties

        public static IContainer Container { get; set; }

        #endregion
    }
}