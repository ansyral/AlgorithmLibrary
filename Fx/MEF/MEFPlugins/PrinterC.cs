namespace XuanLibrary.Fx.MEFPlugins
{
    using System.ComponentModel.Composition;
    using XuanLibrary.Fx.MEFContainer;

    [Export(typeof(IPrinter))]
    [ExportMetadata("MessageType", nameof(PrinterC))]
    public class PrinterC : IPrinter
    {
        public string Print(string message)
        {
            return $"#########{message}";
        }
    }
}
