#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Xsl;
using Common.Logging;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data;
using GoodlyFere.Import.Data.Model;
using Ionic.Zip;

#endregion

namespace GoodlyFere.Import.Business
{
    public class PluginService : GenericService<IPlugin>, IPluginService
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger<PluginService>();
        private readonly string _webSiteRoot;

        #endregion

        #region Constructors and Destructors

        public PluginService(IWriteRepository<IPlugin> repository, string webSiteRoot)
            : base(repository)
        {
            _webSiteRoot = webSiteRoot;
        }

        #endregion

        #region Public Methods

        public override void Delete(IEnumerable<IPlugin> entitiesToDelete)
        {
            foreach (IPlugin plugin in entitiesToDelete)
            {
                RemovePlugin(plugin);
            }
            base.Delete(entitiesToDelete);
        }

        public void LoadAll()
        {
            var plugins = GetAll();
            //LoadAssemblies(plugins);
        }

        public bool Reload(IPlugin plugin)
        {
            //LoadAssemblies(plugin);
            return true;
        }

        public override IEnumerable<IPlugin> Save(IEnumerable<IPlugin> newEntities)
        {
            foreach (IPlugin plugin in newEntities)
            {
                string folder = UnpackPlugin(plugin);
                string oldWebConfigName = TransformWebConfig(folder);
                CopyFiles(folder, oldWebConfigName);
                //LoadAssemblies(newEntities);
            }

            return base.Save(newEntities);
        }

        public override IEnumerable<IPlugin> Update(IEnumerable<IPlugin> entities)
        {
            //LoadAssemblies(entities);
            return base.Update(entities);
        }

        #endregion

        #region Methods

        private static void CreatePluginFolder(string pluginFolderPath)
        {
            if (Directory.Exists(pluginFolderPath))
            {
                Directory.Delete(pluginFolderPath, true);
            }
            Directory.CreateDirectory(pluginFolderPath);
        }

        private static void ExtractPackage(IPlugin plugin, string pluginFolderPath)
        {
            using (var zip = ZipFile.Read(plugin.PackagePath))
            {
                foreach (var entry in zip)
                {
                    entry.Extract(pluginFolderPath);
                }
            }
        }

        private static string GetPluginFolderPath(IPlugin plugin)
        {
            string path = Path.GetDirectoryName(plugin.PackagePath);
            string pluginFolderName = Path.GetFileNameWithoutExtension(plugin.PackagePath);
            return Path.Combine(path, pluginFolderName);
        }

        private void CopyFiles(string pluginFolder, string oldWebConfigFile)
        {
            string manifest = Path.Combine(pluginFolder, "manifest.txt");
            using (StreamWriter manifestWriter = File.CreateText(manifest))
            {
                List<string> directoriesAdded = new List<string>();
                foreach (string file in Directory.EnumerateFiles(pluginFolder, "*.*", SearchOption.AllDirectories))
                {
                    if (file.Contains("web.config.xslt")
                        || file.Contains("manifest.txt"))
                    {
                        continue;
                    }

                    string destination = Path.Combine(_webSiteRoot, file.Replace(pluginFolder + "\\", string.Empty));
                    string destFolder = Path.GetDirectoryName(destination);
                    if (!Directory.Exists(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                        directoriesAdded.Add(destFolder);
                    }

                    if (!File.Exists(destination))
                    {
                        File.Copy(file, destination, true);
                        manifestWriter.WriteLine(destination);
                    }
                }

                foreach (var destFolder in directoriesAdded)
                {
                    manifestWriter.WriteLine(destFolder);
                }

                if (!string.IsNullOrEmpty(oldWebConfigFile))
                {
                    manifestWriter.WriteLine(oldWebConfigFile);
                }
            }
        }

        private void LoadAssemblies(IPlugin plugin)
        {
            try
            {
                string fileName = Path.GetFileName(plugin.PackagePath);
                string binLocation = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, fileName);
                File.Copy(plugin.PackagePath, binLocation, true);
                AssemblyName name = AssemblyName.GetAssemblyName(binLocation);
                Assembly.Load(name);
            }
            catch (ReflectionTypeLoadException rtle)
            {
                Log.ErrorFormat("Could not load plugin assembly.", rtle);
                foreach (var loaderEx in rtle.LoaderExceptions)
                {
                    Log.ErrorFormat("Type load exception: {0}", loaderEx, loaderEx.Message);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Could not load plugin assembly.", ex);
            }
        }

        private void RemovePlugin(IPlugin plugin)
        {
            try
            {
                string pluginFolder = GetPluginFolderPath(plugin);
                string manifest = Path.Combine(pluginFolder, "manifest.txt");

                string[] lines = File.ReadAllLines(manifest);
                foreach (string line in lines)
                {
                    if (line.Contains("web.config"))
                    {
                        File.Copy(line, Path.Combine(_webSiteRoot, "web.config"), true);
                    }

                    if ((File.GetAttributes(line) & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        Directory.Delete(line, true);
                    }
                    else if (File.Exists(line))
                    {
                        File.Delete(line);   
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Could not unload plugin.", ex);
            }
        }

        private string TransformWebConfig(string pluginFolder)
        {
            if (string.IsNullOrEmpty(_webSiteRoot))
            {
                return null;
            }

            string transformPath = Path.Combine(pluginFolder, "web.config.xslt");

            if (!File.Exists(transformPath))
            {
                return null;
            }

            string webConfigPath = Path.Combine(_webSiteRoot, "web.config");
            string oldWebConfig = string.Concat(webConfigPath, ".", DateTime.Now.Ticks, ".bak");

            File.Copy(webConfigPath, oldWebConfig);

            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(transformPath);
            transform.Transform(oldWebConfig, webConfigPath);

            return oldWebConfig;
        }

        private string UnpackPlugin(IPlugin plugin)
        {
            string pluginFolderPath = GetPluginFolderPath(plugin);
            CreatePluginFolder(pluginFolderPath);
            ExtractPackage(plugin, pluginFolderPath);
            return pluginFolderPath;
        }

        #endregion
    }
}