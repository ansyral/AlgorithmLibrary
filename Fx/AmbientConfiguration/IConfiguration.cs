namespace XuanLibrary.Fx
{
    using System;
    public interface IConfiguration<T>
    {
        T Value { get; }
        event Action OnChanged;
    }
}
