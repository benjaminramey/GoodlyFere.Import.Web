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
    public class ConverterParameterRepository : IRepository<IConverterParameter[]>
    {
        #region Constants and Fields

        private static readonly Dictionary<string, List<IConverterParameter>> Cache;

        #endregion

        #region Constructors and Destructors

        static ConverterParameterRepository()
        {
            Cache = new Dictionary<string, List<IConverterParameter>>();
        }

        #endregion

        #region Public Methods

        public IConverterParameter[][] Get(Expression<Func<IConverterParameter[], bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IConverterParameter[] Get(object key)
        {
            string assemblyQualifiedTypeName = (string)key;

            if (!Cache.ContainsKey(assemblyQualifiedTypeName))
            {
                Type type = Type.GetType(assemblyQualifiedTypeName);
                if (type == null)
                {
                    return new IConverterParameter[0];
                }

                ConstructorInfo constructorInfo = type.GetConstructors().FirstOrDefault();
                if (constructorInfo == null)
                {
                    return new IConverterParameter[0];
                }

                List<IConverterParameter> parameters =
                    constructorInfo.GetParameters().Select(
                        p => new ConverterParameter
                            {
                                Name = p.Name
                            }).Cast<IConverterParameter>().ToList();

                Cache.Add(assemblyQualifiedTypeName, parameters);
            }

            return Cache[assemblyQualifiedTypeName].ToArray();
        }

        public IConverterParameter[][] GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}