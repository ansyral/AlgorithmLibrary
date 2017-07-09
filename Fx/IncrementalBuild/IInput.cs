namespace XuanLibrary.Fx
{
    using System;
    using System.Collections.Generic;

    public interface IInput : IChangePropertyProvider, IDependencyProvider
    {
    }

    public class InputKeyEqualityComparer : EqualityComparer<IInput>
    {
        public override bool Equals(IInput x, IInput y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x != null && y != null)
            {
                return x.Key == y.Key;
            }
            return false;
        }

        public override int GetHashCode(IInput obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.Key.GetHashCode();
        }
    }
}
