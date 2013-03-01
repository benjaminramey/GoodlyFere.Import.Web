#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data;
using GoodlyFere.Import.Data.Model;
using GoodlyFere.Import.Interfaces;

#endregion

namespace GoodlyFere.Import.Business
{
    public class RunConfigurationService : GenericService<IRunConfiguration>, IRunConfigurationService
    {
        #region Constants and Fields

        private readonly IRepository<IProject> _projectRepository;

        #endregion

        #region Constructors and Destructors

        public RunConfigurationService(
            IWriteRepository<IRunConfiguration> repository, IRepository<IProject> projectRepository)
            : base(repository)
        {
            _projectRepository = projectRepository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<IRunConfiguration> GetWithNames()
        {
            return Repository.Get(config => !string.IsNullOrEmpty(config.Name));
        }

        public IEnumerable<IRunConfiguration> GetWithNamesForProject(int projectId)
        {
            return Repository.Get(config => config.ProjectId == projectId && !string.IsNullOrEmpty(config.Name));
        }

        public bool Run(IRunConfiguration config)
        {
            IProject project = _projectRepository.Get(config.ProjectId);

            Type sourceType = Type.GetType(project.SourceDefinition.Type, ResolveAssembly, ResolveType);
            Type converterType = Type.GetType(project.ConverterDefinition.Type, ResolveAssembly, ResolveType);
            Type destinationType = Type.GetType(project.DestinationDefinition.Type, ResolveAssembly, ResolveType);

            EnsureTypesAreFound(sourceType, converterType, destinationType);

            ISource source = CreateImporterObjectInstance<ISource>(config, sourceType, "source");
            IConverter converter = CreateImporterObjectInstance<IConverter>(config, converterType, "converter");
            IDestination destination = CreateImporterObjectInstance<IDestination>(
                config, destinationType, "destination");

            Importer importer = new Importer(source, converter, destination);
            return importer.Run();
        }

        #endregion

        #region Methods

        private static T CreateImporterObjectInstance<T>(
            IRunConfiguration config, Type objectType, string parameterType)
        {
            return (T)Activator.CreateInstance(
                objectType,
                config.ParameterInstances
                      .Where(pi => pi.Type == parameterType)
                      .Select(dp => dp.Value)
                      .Cast<Object>().ToArray());
        }

        private static void EnsureTypesAreFound(Type sourceType, Type converterType, Type destinationType)
        {
            if (sourceType == null)
            {
                throw new Exception("Could not find source type. Please make sure all expected plugins are loaded.");
            }
            if (converterType == null)
            {
                throw new Exception("Could not find converter type. Please make sure all expected plugins are loaded.");
            }
            if (destinationType == null)
            {
                throw new Exception(
                    "Could not find destination type. Please make sure all expected plugins are loaded.");
            }
        }

        private Assembly ResolveAssembly(AssemblyName name)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.FirstOrDefault(assembly => assembly.FullName == name.FullName);
        }

        private Type ResolveType(Assembly assembly, string typeName, bool ignoreCase)
        {
            return assembly.GetType(typeName, false, ignoreCase);
        }

        #endregion
    }
}