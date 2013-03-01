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
    public class SourceParameterRepository : IRepository<ISourceParameter[]>
    {
        #region Constants and Fields

        private static readonly Dictionary<string, List<ISourceParameter>> Cache;

        #endregion

        #region Constructors and Destructors

        static SourceParameterRepository()
        {
            Cache = new Dictionary<string, List<ISourceParameter>>();
        }

        #endregion

        #region Public Methods

        public ISourceParameter[][] Get(Expression<Func<ISourceParameter[], bool>> filter)
        {
            throw new NotImplementedException();
        }

        public ISourceParameter[] Get(object key)
        {
            string assemblyQualifiedTypeName = (string)key;

            if (!Cache.ContainsKey(assemblyQualifiedTypeName))
            {
                Type type = Type.GetType(assemblyQualifiedTypeName);
                if (type == null)
                {
                    return new ISourceParameter[0];
                }

                ConstructorInfo constructorInfo = type.GetConstructors().FirstOrDefault();
                if (constructorInfo == null)
                {
                    return new ISourceParameter[0];
                }

                List<ISourceParameter> parameters =
                    constructorInfo.GetParameters().Select(
                        p => new SourceParameter
                            {
                                Name = p.Name
                            }).Cast<ISourceParameter>().ToList();

                Cache.Add(assemblyQualifiedTypeName, parameters);
            }

            return Cache[assemblyQualifiedTypeName].ToArray();
        }

        public ISourceParameter[][] GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}