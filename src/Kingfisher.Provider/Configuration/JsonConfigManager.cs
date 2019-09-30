using System;
using System.Collections.Concurrent;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kingfisher.Provider.Configuration
{
    public class JsonConfigManager : IConfigManager
    {
        public static IConfigManager Default { get; } = new JsonConfigManager();

        private readonly ConcurrentDictionary<Type, Config> _configs = new ConcurrentDictionary<Type, Config>();

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            Converters =
            {
                new StringEnumConverter(),
                new IsoDateTimeConverter()
            },
            Formatting = Formatting.Indented
        };

        private string GetConfigurationPath(Type type)
        {
            var configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MiP/Kingfisher");

            Directory.CreateDirectory(configPath);

            var filename = $"{type.Name}.json";

            return Path.Combine(configPath, filename);
        }

        public T Get<T>() where T : new()
        {
            return (T) _configs.GetOrAdd(typeof(T), t => LoadConfig<T>()).Instance;
        }

        private Config LoadConfig<T>() where T : new()
        {
            var path = GetConfigurationPath(typeof(T));

            var instance = DeserializeFromFile<T>(path);

            return new Config
            {
                Type = typeof(T),
                Path = path,
                Instance = instance
            };
        }

        public void SaveAll()
        {
            foreach (var config in _configs.Values)
            {
                var json = JsonConvert.SerializeObject(config.Instance, _serializerSettings);

                File.WriteAllText(config.Path, json);
            }
        }

        private T DeserializeFromFile<T>(string path) where T : new()
        {
            if (!File.Exists(path))
                return new T();

            var json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json);
        }

        private class Config
        {
            public Type Type { get; set; }
            public object Instance { get; set; }
            public string Path { get; set; }
        }
    }
}