namespace XuanLibrary.Fx
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Remoting.Messaging;

    /// <summary>
    /// AmbientContext that is designed for one service. So each API/Perf scope could have its own AmbientContext.
    /// logs in the service could have prefixes with different length, say "A.B.100", "A.B.C.500", "A.B.C.D.4000"
    /// </summary>
    public class AmbientContextInwardService : ConcurrentDictionary<string, object>, IDisposable
    {
        private const string AMBIENT_CONTEXT = "AMBIENT_CONTEXT";
        private const string AC_CORRELATIONId = "AC_CORRELATIONID";
        private static readonly FieldInfo WrapperField = typeof(BitArray).GetField("_syncRoot", BindingFlags.Instance | BindingFlags.NonPublic);
        private readonly AmbientContextInwardService _original;
        private StrongBox<long> _counter = new StrongBox<long>();

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

        public AmbientContextInwardService(string id)
        {
            CorrelationID = id;
            _original = TryGetCurrent();
            SetCurrent(this);
        }

        public static AmbientContextInwardService TryGetCurrent()
        {
            BitArray bitArray = CallContext.LogicalGetData(AMBIENT_CONTEXT) as BitArray;
            if (bitArray == null)
            {
                return null;
            }
            return WrapperField.GetValue(bitArray) as AmbientContextInwardService;
        }

        public static void SetCurrent(AmbientContextInwardService context)
        {
            var bitArray = new BitArray(0);
            WrapperField.SetValue(bitArray, context);
            CallContext.LogicalSetData(AMBIENT_CONTEXT, bitArray);
        }

        public void Dispose()
        {
            if (_original == null)
            {
                CallContext.FreeNamedDataSlot(AMBIENT_CONTEXT);
            }
            else
            {
                SetCurrent(_original);
            }
        }
    }
}
