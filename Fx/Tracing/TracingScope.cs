namespace XuanLibrary.Fx
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    using System.Threading.Tasks;

    public class TracingScope : TracingItem, IDisposable
    {
        private const string SCOPE_NAME = "SCOPE_NAME";
        private readonly Stopwatch _watcher;
        private static readonly FieldInfo WrapperField = typeof(BitArray).GetField("_syncRoot", BindingFlags.Instance | BindingFlags.NonPublic);
        private string _originalScope;

        public TimeSpan Duration
        {
            get { return this._watcher.Elapsed; }
        }

        public int StatusCode { get; set; }

        public TracingScope(string scopeName)
        {
            _originalScope = GetScopeName();
            Scope = string.IsNullOrEmpty(_originalScope) ? scopeName : $"{_originalScope}.{scopeName}";
            SetScopeName(Scope);
            var context = AmbientContextCrossServices.TryGetCurrent();
            if (context != null)
            {
                CorrelationId = context.GenerateNextCorrelationId();
            }
            else
            {
                CorrelationId = Guid.NewGuid().ToString().ToUpperInvariant();
            }
            Level = TracingLevel.Info;
            Timestamp = DateTime.UtcNow;
            _watcher = Stopwatch.StartNew();
        }

        public async Task<TResult> RunAsync<TResult>(Task<TResult> task)
        {
            StatusCode = 1;
            var result = await task;
            StatusCode = 0;
            return result;
        }

        public void Dispose()
        {
            _watcher.Stop();
            Message = $"{Scope} completed in {Duration.Milliseconds} milliseconds with status code {StatusCode}.";
            TraceExt.TraceInfo(this);
            if (_originalScope != null)
            {
                SetScopeName(_originalScope);
            }
        }

        public static string GetScopeName()
        {
            var bits = CallContext.LogicalGetData(SCOPE_NAME) as BitArray;
            if (bits == null)
            {
                return null;
            }
            return WrapperField.GetValue(bits) as string;
        }

        private static void SetScopeName(string scopeName)
        {
            var bits = new BitArray(0);
            WrapperField.SetValue(bits, scopeName);
            CallContext.LogicalSetData(SCOPE_NAME, bits);
        }
    }
}
