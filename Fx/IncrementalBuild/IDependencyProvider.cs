namespace XuanLibrary.Fx
{
    public interface IDependencyProvider
    {
        void ReportDependencyTo(IDependencyProvider to, BuildContext context);
        void ReportDependencyFrom(IDependencyProvider from, BuildContext context);
    }
}
