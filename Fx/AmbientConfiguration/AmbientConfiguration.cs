namespace XuanLibrary.Fx
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Threading;

    using XuanLibrary.Utility;

    [Serializable]
    public class AmbientConfiguration : IConfiguration<IReadOnlyDictionary<string, object>>, IDisposable, IReadOnlyDictionary<string, object>
    {
        private const string ConfigKey_RefreshInterval = "refresh_interval_milliseconds";
        private readonly Timer _timer;
        private readonly Uri _uri;
        private readonly string _password;
        private string _configurationStr;
        private int _progress;

        public IReadOnlyDictionary<string, object> Value { get; private set; }
        public event Action OnChanged;

        public AmbientConfiguration(Uri uri, string password, bool silent)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }
            _uri = uri;
            _password = password;
            _timer = new Timer(_ => Refresh(silent), null, Timeout.Infinite, Timeout.Infinite);
            Refresh(false);
        }

        public AmbientConfiguration(string uri, string passward, bool silent) : this(new Uri(uri), passward, silent)
        {
        }

        protected AmbientConfiguration(AmbientConfiguration another)
        {
            _uri = another._uri;
            Value = another.Value;
        }

        public AmbientConfiguration GetSnapShot()
        {
            return new AmbientConfiguration(this);
        }

        public AmbientConfigurationEntry<T> CreateItem<T>(string key, T defaultValue, Func<object, T> converter, bool silent)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return new AmbientConfigurationEntry<T>(this, key, defaultValue, converter, silent);
        }

        protected void Refresh(bool silent)
        {
            if (Interlocked.CompareExchange(ref _progress, 1, 0) != 0)
            {
                return;
            }

            WebResponse response = null;

            try
            {
                var request = WebRequest.Create(_uri);
                response = request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var configurationStr = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(_password))
                        {
                            configurationStr = Regex.Replace(configurationStr, @"<SecretAmbientConfiguration:([A-Za-z0-9/+=]+):SecretAmbientConfiguration>", (match) =>
                            {
                                return EnDecriptUtility.Decrypt(match.Groups[1].Value, _password);
                            });
                        }
                        if (configurationStr == _configurationStr)
                        {
                            return;
                        }
                        _configurationStr = configurationStr;
                        var traits = JsonUtility.Deserialize<IReadOnlyDictionary<string, object>>(_configurationStr);
                        if (traits == null)
                        {
                            throw new InvalidOperationException("The configuration string is not a valid JSON dictionary.");
                        }
                        Value = traits;
                    }
                }

                object refreshIntervalMilliseconds = null;
                if (Value.TryGetValue(ConfigKey_RefreshInterval, out refreshIntervalMilliseconds))
                {
                    int interval = (int)refreshIntervalMilliseconds;
                    _timer.Change(interval, interval);
                }
                else
                {
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                }

                var pfnConfigurationRefreshed = OnChanged;
                if (pfnConfigurationRefreshed != null)
                {
                    pfnConfigurationRefreshed();
                }
            }
            catch (Exception ex)
            {
                // TODO: tracer
                if (silent)
                {
                    var wex = ex as WebException;
                    if (wex != null)
                    {
                        response = wex.Response;
                    }
                    return;
                }
                throw;
            }
            finally
            {
                using (response)
                {
                    _progress = 0;
                }
            }
        }

        public override string ToString()
        {
            return JsonUtility.Serialize(Value);
        }

        public void Dispose()
        {
            using (_timer)
            {
                OnChanged = null;
            }
        }



        #region IReadOnlyDictionary
        public object this[string key] => throw new NotImplementedException();

        public IEnumerable<string> Keys => throw new NotImplementedException();

        public IEnumerable<object> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
