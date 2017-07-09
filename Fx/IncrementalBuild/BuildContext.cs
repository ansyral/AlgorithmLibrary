namespace XuanLibrary.Fx
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using XuanLibrary.Utility;

    public class BuildContext
    {
        private BuildCache _lastCache;
        private BuildCache _currentCache;
        private string _cacheFolder;

        public DependencyGraph DG { get { return _currentCache.DG; } }

        public bool CanIncremental { get; private set; }

        public BuildContext(string cacheFolder = null, bool forceBuild = false)
        {
            _cacheFolder = cacheFolder ?? Path.Combine(Directory.GetCurrentDirectory(), ".cache");
            Directory.CreateDirectory(_cacheFolder);
            _lastCache = BuildCache.Load(_cacheFolder);
            CanIncremental = !forceBuild;
            if (_lastCache == null)
            {
                CanIncremental = false;
                _currentCache = new BuildCache();
            }
            else
            {
                _currentCache = new BuildCache(_lastCache);
            }
        }

        public void UpdateInputCache(IEnumerable<IChangePropertyProvider> inputs)
        {
            foreach (var input in inputs)
            {
                var properties = input.GetChangeProperties();
                _currentCache.InputCache[input.Key] = JsonUtility.ToJsonString(properties);
            }
        }

        public void UpdateOutputCache<Output>(IChangePropertyProvider input, Output output)
        {
            string filename = IncrementalUtility.CreateRandomFileName(_cacheFolder);
            using (var fs = File.Create(Path.Combine(_cacheFolder, filename)))
            {
                new BinaryFormatter().Serialize(fs, output);
            }
            _currentCache.OutputCacheIndex[input.Key] = filename;
        }

        public List<IInput> GetChangesWithDependencies(IEnumerable<IInput> inputs)
        {
            var res = new List<IInput>();
            foreach (var input in inputs)
            {
                string lastProperties;
                if (!_lastCache.InputCache.TryGetValue(input.Key, out lastProperties) ||
                    _currentCache.InputCache[input.Key] != lastProperties)
                {
                    res.Add(input);
                    res.AddRange(DG.GetDependencyFrom(input).OfType<IInput>());
                }
            }
            return res;
        }

        public List<Output> GetCachedOutput<Output>(IEnumerable<IChangePropertyProvider> inputs)
        {
            var res = new List<Output>();
            var binaryFormatter = new BinaryFormatter();
            foreach (var input in inputs)
            {
                string outputFile;
                if (!_currentCache.OutputCacheIndex.TryGetValue(input.Key, out outputFile))
                {
                    throw new BuildCacheException($"output cache doesn't have output for input {input.Key}.");
                }
                using (var fs = File.OpenRead(Path.Combine(_cacheFolder, outputFile)))
                {
                    res.Add((Output)binaryFormatter.Deserialize(fs));
                }
            }
            return res;
        }

        public void SaveCache()
        {
            _currentCache.Save(_cacheFolder);
        }
    }
}
