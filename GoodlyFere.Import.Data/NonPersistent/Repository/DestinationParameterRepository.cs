#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GoodlyFere.Import.Data.Model;
using GoodlyFere.Import.Data.NonPersistent.Model;

#endregion

namespace GoodlyFere.Import.Data.NonPersistent.Repository
{
    public class DestinationParameterRepository : IRepository<IDestinationParameter[]>
    {
        #region Constants and Fields

        private static readonly Dictionary<string, List<IDestinationParameter>> Cache;

        #endregion

        #region Constructors and Destructors

        static DestinationParameterRepository()
        {
            Cache = new Dictionary<string, List<IDestinationParameter>>();
        }

        #endregion

        #region Public Methods

        public IDestinationParameter[][] Get(Expression<Func<IDestinationParameter[], bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IDestinationParameter[] Get(object key)
        {
            string assemblyQualifiedTypeName = (string)key;

            if (!Cache.ContainsKey(assemblyQualifiedTypeName))
            {
                Type type = Type.GetType(assemblyQualifiedTypeName);
                if (type == null)
                {
                    return new IDestinationParameter[0];
                }

                ConstructorInfo constructorInfo = type.GetConstructors().FirstOrDefault();
                if (constructorInfo == null)
                {
                    return new IDestinationParameter[0];
                }

                List<IDestinationParameter> parameters =
                    constructorInfo.GetParameters().Select(
                        p => new DestinationParameter
                            {
                                Name = p.Name
                            }).Cast<IDestinationParameter>().ToList();

                Cache.Add(assemblyQualifiedTypeName, parameters);
            }

            return Cache[assemblyQualifiedTypeName].ToArray();
        }

        public IDestinationParameter[][] GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}