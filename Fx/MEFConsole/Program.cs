namespace XuanLibrary.Fx.MEFConsole
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using XuanLibrary.Fx.MEFContainer;
    using XuanLibrary.Fx.MEFPlugins;

    class Program
    {
        public static void Main(string[] args)
        {
            // copy plugin dll to output folder
            string pluginDir = Path.Combine(Directory.GetCurrentDirectory(), "_plugins");
            Directory.CreateDirectory(pluginDir);
            string filename = typeof(PrinterA).Assembly.Location;
            File.Copy(filename, Path.Combine(pluginDir, Path.GetFileName(filename)), true);

            var container = new Container(pluginDir);
            Debug.Assert(container.PluggedPrinters.Count() == 2);
            var messages = container.PluggedPrinters.Select(p => p.Value.Print("hi"));
            Debug.Assert(messages.Except(new[] { "---------hi", "*********hi" }).Count() == 0);
            var printerA = container.PluggedPrinters.Any(p => p.Metadata.MessageType == nameof(PrinterA));
            Debug.Assert(printerA);

            var container2 = new Container2(pluginDir);
            Debug.Assert(container2.PluggedPrinters.Count() == 1);
            messages = container2.PluggedPrinters.Select(p => p.Value.Print("hi"));
            Debug.Assert(messages.Except(new[] { "#########hi" }).Count() == 0);
            var printerC = container2.PluggedPrinters.Any(p => p.Metadata.MessageType == nameof(PrinterC));
            Debug.Assert(printerC);
        }
    }
}
