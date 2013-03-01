#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data.Model
{
    public interface IProject : IModelBase
    {
        #region Public Properties

        IConverterDefinition ConverterDefinition { get; set; }
        int ConverterDefinitionId { get; set; }
        IDestinationDefinition DestinationDefinition { get; set; }
        int DestinationDefinitionId { get; set; }
        string Name { get; set; }
        ISourceDefinition SourceDefinition { get; set; }
        int SourceDefinitionId { get; set; }

        #endregion
    }
}