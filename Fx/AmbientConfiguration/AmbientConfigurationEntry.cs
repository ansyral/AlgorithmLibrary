namespace XuanLibrary.Fx
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class AmbientConfigurationEntry<T> : IConfiguration<T>
    {
        private Func<object, T> _converter;

        public string Key { get; set; }

        public T Value { get; private set; }

        public event Action OnChanged;

        public AmbientConfigurationEntry(AmbientConfiguration ac, string key, T defaultValue, Func<object, T> converter, bool silent)
        {
            if (ac == null)
            {
                throw new ArgumentNullException(nameof(ac));
            }
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Key = key;
            Value = defaultValue;
            _converter = converter;
            Refresh(ac, false);
            ac.OnChanged += () => Refresh(ac, silent);
        }

        protected void Refresh(AmbientConfiguration ac, bool silent)
        {
            object value;
            if (!ac.TryGetValue(Key, out value))
            {
                string message = $"Cannot find key {Key} in configuration!";
                TraceExt.TraceError(message, null);
                if (!silent)
                {
                    throw new KeyNotFoundException(message);
                }
                return;
            }
            try
            {
                T typedValue = _converter != null ? _converter(value) : (T)value;
                if (!object.Equals(Value, typedValue))
                {
                    TraceExt.TraceInfo($"Configuration '{Key}' is changed from '{Value}' to '{typedValue}'.", null);
                    Value = typedValue;
                    if (OnChanged != null)
                    {
                        OnChanged();
                    }
                }
            }
            catch (InvalidCastException ex)
            {
                TraceExt.TraceError($"Cannot cast value '{value}' to type '{typeof(T)}', original value is kept.", ex);
                if (!silent)
                {
                    throw;
                }
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator T(AmbientConfigurationEntry<T> entry)
        {
            return entry.Value;
        }
    }
}
