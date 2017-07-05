namespace XuanLibrary.Fx
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class RollingFileListener : TraceListener
    {
        #region Arguments
        private const string ENVIRONMENT_PROVIDER = "EnvironmentProvider";
        private const string TRACE_PATH = "TracePath";
        private const string ROLLING_SIZEMB = "RollingSizeMB";
        private const string ROLLING_INTERVAL_SECOND = "RollingIntervalS";
        private const string BUFFER_SIZEMB = "BufferSizeMB";
        private const string FILE_RETENTION_SIZEMB = "FileRetentionSizeMB";
        private const string FILE_RETENTIONTIME_MINUTE = "FileRetentionTimeMinute";
        private const string ENTRY_COLUMNS = "EntryColumns";
        #endregion

        private const string TempRelativePath = "temp";
        private const string CommitedRelativePath = "commited";
        private const string Delimeter = "\t";
        private readonly Dictionary<string, object> _environmentVariables = new Dictionary<string, object>();
        private string _tracePath = Directory.GetCurrentDirectory();
        private TextWriter _writer;
        private int _streamPosition;
        private DateTime _createUtcTime;
        // need this as the Attributes collection is not populated from the configuration file until after the instance of your listener is fully constructed.
        // ref https://stackoverflow.com/questions/11559916/loading-trace-listener-attributes
        private bool _initialized = false;
        private string[] _columns = new[] { "DeploymentId", "Role", "RoleInstance", "Timestamp", "EventTickCount", "EventId", "Level", "Pid", "Tid", "Message" };
        private double _rollingSizeMB = 128;
        private double _rollingIntervalSec = 60;
        private double _bufferSizeMB = 5 * 1024;
        private double _fileRentionSizeMB = 1024;
        private double _fileRentionMinute = 20;

        private string Header
        {
            get { return string.Join(Delimeter, _columns) + Environment.NewLine; }
        }

        private string TempFolder
        {
            get { return Path.Combine(_tracePath, TempRelativePath); }
        }

        private string CommitedFolder
        {
            get { return Path.Combine(_tracePath, CommitedRelativePath); }
        }

        public override void Write(string message)
        {
            TraceEvent(new TraceEventCache(), nameof(RollingFileListener), TraceEventType.Verbose, 0, message);
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }

        // the method would be called by Trace.TraceXX() (Observer pattern: enumerate all registered tracelisteners and invoke their TraceEvent method)
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            try
            {
                if (!_initialized)
                {
                    Init();
                }
                if (Filter != null && !Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
                {
                    return;
                }
                InternalWrite(eventCache, eventType, id, message);
            }
            catch (Exception)
            {
                // silently
            }
        }

        public override void Close()
        {
            if (_writer != null)
            {
                _writer.Close();
                _writer = null;
            }
            base.Close();
        }

        public override void Flush()
        {
            if (_writer != null)
            {
                _writer.Flush();
            }
            base.Flush();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (_writer != null)
                {
                    _writer.Close();
                }
            }
            catch (Exception)
            {
                // silently
            }
            finally
            {
                _writer = null;
            }
            base.Dispose(disposing);
        }

        // must override the method if you have self-defined attributes
        protected override string[] GetSupportedAttributes()
        {
            return new[] { ENVIRONMENT_PROVIDER, TRACE_PATH, ROLLING_SIZEMB, ROLLING_INTERVAL_SECOND, ENTRY_COLUMNS, FILE_RETENTION_SIZEMB, BUFFER_SIZEMB, FILE_RETENTIONTIME_MINUTE };
        }

        private void InternalWrite(TraceEventCache eventCache, TraceEventType traceType, int id, string message)
        {
            string entry = GetEntry(eventCache, traceType, id, message);
            if (_writer == null)
            {
                _writer = new StreamWriter(File.Create(Path.Combine(TempFolder, DateTime.UtcNow.Ticks.ToString() + ".tsv")));
                _writer.Write(Header);
                _streamPosition += Header.Length;
                _createUtcTime = DateTime.UtcNow;
            }
            if (_streamPosition + entry.Length < _rollingSizeMB * 1024 * 1024 &&
                (DateTime.UtcNow - _createUtcTime).Seconds < _rollingIntervalSec)
            {
                _writer.Write(entry);
                _streamPosition += entry.Length;
                return;
            }
            CommitTempFile();
        }

        private void CommitTempFile()
        {
            try
            {
                if (_writer != null)
                {
                    _writer.Close();
                }
                foreach (var file in Directory.GetFiles(TempFolder))
                {
                    File.Move(file, Path.Combine(CommitedFolder, Path.GetFileName(file)));
                }
                RetentionFiles();
            }
            catch (Exception)
            {
                // silently
            }
            finally
            {
                _writer = null;
                _streamPosition = 0;
            }
        }

        private void RetentionFiles()
        {
            try
            {
                long deleted = 0;
                foreach (var file in new DirectoryInfo(CommitedFolder).GetFiles("*.*").OrderBy(f => f.CreationTimeUtc))
                {
                    if ((DateTime.UtcNow - file.CreationTimeUtc).Minutes < _fileRentionMinute)
                    {
                        break;
                    }
                    File.Delete(file.FullName);
                    deleted += file.Length;
                    if (deleted >= _fileRentionSizeMB * 1024 * 1024)
                    {
                        return;
                    }
                }
                var files = new DirectoryInfo(CommitedFolder).GetFiles("*.*").OrderBy(f => f.LastWriteTime);
                var length = files.Sum(f => f.Length);
                if (length > _bufferSizeMB * 1024 * 1024)
                {
                    foreach (var file in files)
                    {
                        deleted += file.Length;
                        File.Delete(file.FullName);
                        if (deleted >= _fileRentionSizeMB * 1024 * 1024)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // silently
            }
        }

        private string GetEntry(TraceEventCache eventCache, TraceEventType traceType, int id, string message)
        {
            return string.Join(Delimeter, _columns.Select(c => GetColumn(c, eventCache, traceType, id, message))) + Environment.NewLine;
        }

        private string GetColumn(string column, TraceEventCache eventCache, TraceEventType traceType, int id, string message)
        {
            switch (column)
            {
                case "Timestamp":
                    return eventCache.DateTime.ToUniversalTime().ToString();
                case "EventTickCount":
                    return eventCache.DateTime.ToUniversalTime().Ticks.ToString();
                case "EventId":
                    return id.ToString();
                case "Level":
                    return traceType.ToString();
                case "Pid":
                    return eventCache.ProcessId.ToString();
                case "Tid":
                    return eventCache.ThreadId.ToString();
                case "Message":
                    return message;
                default:
                    object content;
                    if (_environmentVariables.TryGetValue(column, out content))
                    {
                        return content.ToString();
                    }
                    return string.Empty;
            }
        }

        private void Init()
        {
            if (Attributes.ContainsKey(TRACE_PATH))
            {
                _tracePath = Attributes[TRACE_PATH];
            }
            if (Attributes.ContainsKey(ENVIRONMENT_PROVIDER))
            {
                Type envProviderType = Type.GetType(Attributes[ENVIRONMENT_PROVIDER], true);
                var instance = Activator.CreateInstance(envProviderType);
                foreach (var prop in envProviderType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    _environmentVariables[prop.Name] = prop.GetValue(instance);
                }
            }
            if (Attributes.ContainsKey(ROLLING_SIZEMB))
            {
                _rollingSizeMB = Convert.ToDouble(Attributes[ROLLING_SIZEMB]);
            }
            if (Attributes.ContainsKey(ROLLING_INTERVAL_SECOND))
            {
                _rollingIntervalSec = Convert.ToDouble(Attributes[ROLLING_INTERVAL_SECOND]);
            }
            if (Attributes.ContainsKey(BUFFER_SIZEMB))
            {
                _bufferSizeMB = Convert.ToDouble(Attributes[BUFFER_SIZEMB]);
            }
            if (Attributes.ContainsKey(FILE_RETENTION_SIZEMB))
            {
                _fileRentionSizeMB = Convert.ToDouble(Attributes[FILE_RETENTION_SIZEMB]);
            }
            if (Attributes.ContainsKey(FILE_RETENTIONTIME_MINUTE))
            {
                _fileRentionMinute = Convert.ToDouble(Attributes[FILE_RETENTIONTIME_MINUTE]);
            }
            if (Attributes.ContainsKey(ENTRY_COLUMNS))
            {
                _columns = Attributes[ENTRY_COLUMNS].Split(new[] { Delimeter }, StringSplitOptions.None);
            }
            Directory.CreateDirectory(TempFolder);
            Directory.CreateDirectory(CommitedFolder);
            CommitTempFile();
            _initialized = true;
        }
    }
}
