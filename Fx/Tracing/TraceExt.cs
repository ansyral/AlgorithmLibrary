namespace XuanLibrary.Fx
{
    using System;
    using System.Diagnostics;

    using XuanLibrary.Utility;

    public static class TraceExt
    {
        public static void TraceError(string message, object data, string scope = null, string correlationId = null)
        {
            TraceError(new TracingItem
            {
                Message = message,
                Data = data,
                Scope = scope ?? TracingScope.GetScopeName(),
                Level = TracingLevel.Error,
                Timestamp = DateTime.UtcNow,
                CorrelationId = correlationId ?? GenerateCorrelationId(),
            });
        }

        public static void TraceWarning(string message, object data, string scope = null, string correlationId = null)
        {
            TraceWarning(new TracingItem
            {
                Message = message,
                Data = data,
                Scope = scope ?? TracingScope.GetScopeName(),
                Level = TracingLevel.Warning,
                Timestamp = DateTime.UtcNow,
                CorrelationId = correlationId ?? GenerateCorrelationId(),
            });
        }

        public static void TraceInfo(string message, object data, string scope = null, string correlationId = null)
        {
            TraceInfo(new TracingItem
            {
                Message = message,
                Data = data,
                Scope = scope ?? TracingScope.GetScopeName(),
                Level = TracingLevel.Info,
                Timestamp = DateTime.UtcNow,
                CorrelationId = correlationId ?? GenerateCorrelationId(),
            });
        }

        public static void TraceError(TracingItem item)
        {
            if (item.Level != TracingLevel.Error)
            {
                return;
            }
            Trace.TraceError(JsonUtility.ToJsonString(item));
        }

        public static void TraceWarning(TracingItem item)
        {
            if (item.Level != TracingLevel.Warning)
            {
                return;
            }
            Trace.TraceWarning(JsonUtility.ToJsonString(item));
        }

        public static void TraceInfo(TracingItem item)
        {
            if (item.Level != TracingLevel.Info)
            {
                return;
            }
            Trace.TraceInformation(JsonUtility.ToJsonString(item));
        }

        private static string GenerateCorrelationId()
        {
            var context = AmbientContextCrossServices.TryGetCurrent();
            if (context != null)
            {
                return context.GenerateNextCorrelationId();
            }
            return null;
        }
    }
}
