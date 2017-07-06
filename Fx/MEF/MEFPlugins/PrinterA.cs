namespace XuanLibrary.Fx.MEFPlugins
{
    using System.ComponentModel.Composition;
    using XuanLibrary.Fx.MEFContainer;

    [Export(nameof(Container), typeof(IPrinter))]
    [ExportMetadata("MessageType", nameof(PrinterA))]
    public class PrinterA : IPrinter
    {
        public string Print(string message)
        {
            return $"---------{message}";
        }
    }
}
