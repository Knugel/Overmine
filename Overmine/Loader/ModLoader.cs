using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using Newtonsoft.Json;
using Overmine.API;
using UnityEngine;

namespace Overmine.Loader
{
    public class ModLoader
    {
        private readonly IDictionary<string, ModContainer> _mods = new Dictionary<string, ModContainer>();
        private readonly ManualLogSource _logger;

        public IEnumerable<ModContainer> Mods => _mods.Values;

        public ModResources Resources { get; private set; }
        
        public ModLoader(ManualLogSource logger)
        {
            _logger = logger;
        }
        
        public void Load(string path)
        {
            var directories = Directory.EnumerateDirectories(path).ToList();
            
            _logger.LogInfo($"Searching {directories.Count} directories at {Path.GetFullPath(path)}.");
            
            foreach (var directory in directories)
            {
                var mod = LoadMod(directory);
                if(mod != null)
                    _mods.Add(mod.Info.Id, mod);
            }
            Resources = new ModResources(Mods.SelectMany(x => x.Resources.Bundles));
            
            _logger.LogInfo($"Found {_mods.Count} valid mods.");
        }

        public void InitializeMods()
        {
            foreach (var container in Mods)
            {
                if(container.Assembly == null)
                    continue;

                var initializer = container.Assembly
                    .GetExportedTypes()
                    .FirstOrDefault(x => typeof(Mod).IsAssignableFrom(x));
                
                if (initializer != null)
                {
                    try
                    {
                        container.Instance = Activator.CreateInstance(initializer, container.Resources) as Mod;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"Error while trying to initialize Mod {container.Info.Name}!");
                        _logger.LogError(e);
                    }
                }
            }
        }

        private ModContainer LoadMod(string path)
        {
            if (!Directory.Exists(path))
                return null;
            
            var infoPath = Path.Combine(path, "mod.json");
            if (!File.Exists(infoPath))
                return null;
            
            var contents = File.ReadAllText(infoPath);
            var info = JsonConvert.DeserializeObject<ModInfo>(contents);
            if (info == null)
            {
                _logger.LogWarning($"{infoPath} was not a valid ModInfo!");
                return null;
            }
            
            _logger.LogInfo($"Loading {info.Name} [{info.Version}]");

            var container = new ModContainer
            {
                Info = info
            };
            
            if (!string.IsNullOrEmpty(info.EntryDLL))
            {
                container.Assembly = Assembly.LoadFrom(Path.Combine(path, info.EntryDLL));
            }

            var bundles = new List<AssetBundle>();
            foreach (var entry in info.Bundles)
            {
                var bundlePath = Path.Combine(path, entry);
                if (!File.Exists(bundlePath))
                {
                    _logger.LogWarning($"Couldn't find AssetBundle {bundlePath}!");
                    continue;
                }
                bundles.Add(AssetBundle.LoadFromFile(bundlePath));
            }
            container.Resources = new ModResources(bundles);

            return container;
        }
    }
}