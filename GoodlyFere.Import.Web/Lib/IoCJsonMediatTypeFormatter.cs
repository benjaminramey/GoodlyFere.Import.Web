#region Usings

using System.IO;
using System.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Autofac;
using Newtonsoft.Json.Serialization;

#endregion

namespace GoodlyFere.Import.Web.Lib
{
    internal class IoCJsonMediatTypeFormatter : JsonMediaTypeFormatter
    {
        #region Constructors and Destructors

        public IoCJsonMediatTypeFormatter()
        {
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion

        #region Public Methods

        public override Task<object> ReadFromStreamAsync(
            Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            if (AppContext.Container.IsRegistered(type))
            {
                type = AppContext.Container.Resolve(type).GetType();
            }

            return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
        }

        #endregion
    }
}