namespace XuanLibrary.Fx
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    using System.Threading;

    using XuanLibrary.Utility;

    /// <summary>
    /// AmbientContext that is designed to cross services like flowing through http calls by use of http headers.
    /// For each service, it would only contain one AmbientContext, so logs in a service would share the same prefix, say "A.B.C", namely log ids are like "A.B.C.100", "A.B.C.5000"
    /// code inside the service could access it by `AmbientContextCrossService.TryGetCurrent()`.
    /// </summary>
    public class AmbientContextCrossServices : ConcurrentDictionary<string, object>, IDisposable
    {
        private const string AMBIENT_CONTEXT = "AMBIENT_CONTEXT";
        private const string AC_CORRELATIONId = "AC_CORRELATIONID";
        private static readonly FieldInfo WrapperField = typeof(BitArray).GetField("_syncRoot", BindingFlags.Instance | BindingFlags.NonPublic);
        private long _counter = 0;

        public string CorrelationID
        {
            get
            {
                return (string)this[AC_CORRELATIONId];
            }
            private set
            {
                this[AC_CORRELATIONId] = value;
            }
        }

        public AmbientContextCrossServices()
        {
            CorrelationID = Guid.NewGuid().ToString().ToUpperInvariant();
        }

        public AmbientContextCrossServices(string serializedJson) : base(JsonUtility.FromJsonString<Dictionary<string, object>>(serializedJson))
        {
        }

        public AmbientContextCrossServices(AmbientContextCrossServices other) : base(other)
        {
        }

        public AmbientContextCrossServices CreateBranch()
        {
            var branch = new AmbientContextCrossServices(this);
            branch.CorrelationID = GenerateNextCorrelationId();
            return branch;
        }

        public string GenerateNextCorrelationId()
        {
            return $"{CorrelationID}.{Interlocked.Increment(ref _counter)}";
        }

        public static AmbientContextCrossServices GetOrCreateCurrent()
        {
            var context = TryGetCurrent();
            if (context == null)
            {
                context = Initialize(null);
            }
            return context;
        }

        public static AmbientContextCrossServices TryGetCurrent()
        {
            BitArray bitArray = CallContext.LogicalGetData(AMBIENT_CONTEXT) as BitArray;
            if (bitArray == null)
            {
                return null;
            }
            return WrapperField.GetValue(bitArray) as AmbientContextCrossServices;
        }

        public static AmbientContextCrossServices Initialize(string serializedJson)
        {
            var context = string.IsNullOrEmpty(serializedJson) ? new AmbientContextCrossServices() : new AmbientContextCrossServices(serializedJson);
            var bitArray = new BitArray(0);
            WrapperField.SetValue(bitArray, context);
            CallContext.LogicalSetData(AMBIENT_CONTEXT, bitArray);
            return context;
        }

        public void Dispose()
        {
            var context = TryGetCurrent();
            if (object.ReferenceEquals(this, context))
            {
                CallContext.FreeNamedDataSlot(AMBIENT_CONTEXT);
            }
        }

        public override string ToString()
        {
            return JsonUtility.ToJsonString(this);
        }
    }
}
