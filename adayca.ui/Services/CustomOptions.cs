using adayca.ui.BaseInterface;
using adayca.ui.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.Services
{
    public class CustomOptions<T> : ICustomOptions<T> where T : class, new()
    {
        [Obsolete]
        private readonly IHostingEnvironment _environment;
        private readonly IOptionsMonitor<T> _options;
        private readonly IConfigurationRoot _configuration;
        private readonly string _section;
        private readonly string _file;

        JObject JObjectFile = null;
        string physicalPath = "";

        [Obsolete]
        public CustomOptions(
            IHostingEnvironment environment,
            IOptionsMonitor<T> options,
            IConfigurationRoot configuration,
            string section,
            string file)
        {
            _environment = environment;
            _options = options;
            _configuration = configuration;
            _section = section;
            _file = file;

            var fileProvider = _environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo(_file);
            physicalPath = fileInfo.PhysicalPath;
            JObjectFile = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
        }

        public T Value => _options.CurrentValue;
        public T Get(string name) => _options.Get(name);

        public T GetUrl(string param)
        {
            CustomConfiguration priority = GetValue(param) as CustomConfiguration;
            T url = GetValue(priority.option);
            return url;
        }

        public T GetValue(string sectionName)
        {
            //T data = Get(sectionName);

            return JObjectFile.TryGetValue(sectionName, out JToken section) ?
                           JsonConvert.DeserializeObject<T>(section.ToString()) : (Value ?? new T());
                           //as CustomConfiguration;
        }

        [Obsolete]
        public void Update(Action<T> applyChanges)
        {
            var sectionObject = JObjectFile.TryGetValue(_section, out JToken section) ?
                JsonConvert.DeserializeObject<T>(section.ToString()) : (Value ?? new T());

            applyChanges(sectionObject);

            JObjectFile[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
            File.WriteAllText(physicalPath, JsonConvert.SerializeObject(JObjectFile, Formatting.Indented));
            _configuration.Reload();
        }
    }
}
