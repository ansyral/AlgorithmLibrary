namespace XuanLibrary.Fx
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    public class RollingFileListener : TraceListener
    {
        #region Arguments
        private const string ENVIRONMENT_PROVIDER = "EnvironmentProvider";
        private const string TRACE_PATH = "TracePath";
        private const string ROLLING_SIZEMB = "RollingSizeMB";
        private const string ROLLING_INTERVAL_SECOND = "RollingIntervalS";
        private const string BUFFER_SIZEMB = "BufferSizeMB";
        private const string FILE_RENTION_SIZEMB = "FileRentionSizeMB";
        private const string FILE_RENTIONTIME_MINUTE = "FileRentionTimeMinute";
        #endregion

        private const string TempRelativePath = "temp";
        private const string CommitedRelativePath = "commited";
        private const string Delimeter = "\t";
        private readonly string[] _columns = new[] { "DeploymentId", "Role", "RoleInstance", "Timestamp", "EventTickCount", "EventId", "Level", "Pid", "Tid", "Message" };
        private readonly Dictionary<string, object> _environmentVariables = new Dictionary<string, object>();
        private string _tracePath;
        private TextWriter _writer;
        private int _rollingSizeMB = 128;
        private int _rollingIntervalSec = 60;
        private int _bufferSizeMB = 5 * 1024;
        private int _fileRentionSizeMB = 1024;
        private int _fileRentionMinute = 20;

        private string Header
        {
            get { return string.Join(Delimeter, _columns); }
        }

        public RollingFileListener()
        {
            Init();
        }
        public override void Write(string message)
        {
            throw new NotImplementedException();
        }

        public override void WriteLine(string message)
        {
            throw new NotImplementedException();
        }

        private void Init()
        {
            if (!Attributes.ContainsKey(TRACE_PATH))
            {
                throw new ArgumentNullException("No TracePath defined!");
            }
            _tracePath = Attributes[TRACE_PATH];
            if (Attributes.ContainsKey(ENVIRONMENT_PROVIDER))
            {
                Type envProviderType = Type.GetType(Attributes[ENVIRONMENT_PROVIDER], true);
                var instance = Activator.CreateInstance(envProviderType);
                foreach (var prop in envProviderType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    _environmentVariables.Add(prop.Name, prop.GetValue(instance));
                }
            }
            if (Attributes.ContainsKey(ROLLING_SIZEMB))
            {
                _rollingSizeMB = Convert.ToInt32(Attributes[ROLLING_SIZEMB]);
            }
            if (Attributes.ContainsKey(ROLLING_INTERVAL_SECOND))
            {
                _rollingIntervalSec = Convert.ToInt32(Attributes[ROLLING_INTERVAL_SECOND]);
            }
            if (Attributes.ContainsKey(BUFFER_SIZEMB))
            {
                _bufferSizeMB = Convert.ToInt32(Attributes[BUFFER_SIZEMB]);
            }
            if (Attributes.ContainsKey(FILE_RENTION_SIZEMB))
            {
                _fileRentionSizeMB = Convert.ToInt32(Attributes[FILE_RENTION_SIZEMB]);
            }
            if (Attributes.ContainsKey(FILE_RENTIONTIME_MINUTE))
            {
                _fileRentionMinute = Convert.ToInt32(Attributes[FILE_RENTIONTIME_MINUTE]);
            }
        }
    }
}
